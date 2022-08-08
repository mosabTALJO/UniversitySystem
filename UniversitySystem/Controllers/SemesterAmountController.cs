using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;
using static UniversitySystem.Controllers.DepStuPaymentController;

namespace UniversitySystem.Controllers
{///////////////////////////////////////////////////////////// all works /////////////////////////////////////////////////////////////
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class SemesterAmountController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SemesterAmountController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]  // when this get executed all studedents gets a new records  
        public async Task<ActionResult<int>> AddSemAmnt(SemesterAmountVM semesterAmountVM , int SemId, int DepId)
        {
            var semamnt = _mapper.Map<SemesterAmount>(semesterAmountVM);
            semamnt.SemId = SemId;
            semamnt.DepId = DepId;
            await _context.SemesterAmounts.AddAsync(semamnt);
            await _context.SaveChangesAsync();

            var depStuList = await _context.DepartmentStudents.Include(dest => dest.Department)
                                                              .ThenInclude(dest => dest.Students)
                                                              .ThenInclude(dest => dest.Supervisor).Where(ds => ds.DepartmentId == DepId && ds.IsActive).ToListAsync();
            
            foreach (var depStu in depStuList)
            {
                var depStuPayment = new DepStuPayment
                {
                    DepStuId = depStu.Id,
                    IsPayed = false,
                    SemAmntID = semamnt.Id,
                    Total = semamnt.Price
                };
                await AutoAddStudentPayment(depStu.Id, SemId, DepId, semamnt.Price , _context, _mapper, depStuPayment);
            } 
            await _context.SaveChangesAsync();  
            return Ok(semamnt.Id);
        }

        [HttpGet]
        public async Task<ActionResult<List<SemesterAmountVM>>> SearchSemAmnt(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] int Dep = 0)
        {
            if (Dep != 0)
                return Ok(await _context.SemesterAmounts.Where(x => !x.DeletedAt.HasValue).Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<SemesterAmountVM>(_mapper.ConfigurationProvider).Where(c => c.SemId == Dep).ToListAsync());
            return Ok(await _context.SemesterAmounts.Where(x => !x.DeletedAt.HasValue).Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<SemesterAmountVM>(_mapper.ConfigurationProvider).ToListAsync());

        }

        [HttpPut]
        public async Task<ActionResult<bool>> EditSemAmnt(int DepId , int SemId , SemesterAmountVM semesterAmountVM)
        {
            var edtSemAmnt = _context.SemesterAmounts.Where(x => !x.DeletedAt.HasValue).FirstOrDefault(c => c.SemId == SemId && c.DepId == DepId);
            if (edtSemAmnt == null) return NotFound("There is no semester like this");
            edtSemAmnt.Price = semesterAmountVM.Price;
            await _context.SaveChangesAsync();
            return Ok("True");
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DltSemAmnt(int DepId, int SemId)
        {
            var dltSemAmnt = await _context.SemesterAmounts.Where(x => !x.DeletedAt.HasValue).FirstOrDefaultAsync(sa => sa.SemId == SemId && sa.DepId == DepId );
            if (dltSemAmnt == null) return NotFound("Nothing found");
            _context.SemesterAmounts.Remove(dltSemAmnt);
            await _context.SaveChangesAsync();
            return Ok("True");
        }
    }
}
