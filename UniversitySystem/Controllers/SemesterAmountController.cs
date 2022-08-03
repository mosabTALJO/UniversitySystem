using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;

namespace UniversitySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterAmountController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SemesterAmountController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost] 
        public async Task<ActionResult<int>> AddSemAmnt(SemesterAmountVM semesterAmountVM)
        {
            var semamnt = _mapper.Map<SemesterAmount>(semesterAmountVM);
            await _context.SemesterAmounts.AddAsync(semamnt);
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
                return Ok(await _context.SemesterAmounts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<SemesterAmountVM>(_mapper.ConfigurationProvider).Where(c => c.SemId == Dep).ToListAsync());
            return Ok(await _context.SemesterAmounts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<SemesterAmountVM>(_mapper.ConfigurationProvider).ToListAsync());

        }

        [HttpPut]
        public async Task<ActionResult<bool>> EditSemAmnt(int DepId , int SemId , SemesterAmountVM semesterAmountVM)
        {
            var edtSemAmnt = _context.SemesterAmounts.FirstOrDefault(c => c.SemId == SemId && c.DepId == DepId);
            if (edtSemAmnt == null) return NotFound("There is no semester like this");
            edtSemAmnt.Price = semesterAmountVM.Price;
            await _context.SaveChangesAsync();
            return Ok("True");
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DltSemAmnt(int DepId, int SemId)
        {
            var dltSemAmnt = await _context.SemesterAmounts.FirstOrDefaultAsync(sa => sa.SemId == SemId && sa.DepId == DepId );
            if (dltSemAmnt == null) return NotFound("Nothing found");
            _context.SemesterAmounts.Remove(dltSemAmnt);
            await _context.SaveChangesAsync();
            return Ok("True");
        }
    }
}
