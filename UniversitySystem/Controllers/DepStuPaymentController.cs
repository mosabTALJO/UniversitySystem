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
    public class DepStuPaymentController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public DepStuPaymentController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddStudentPayment(int StdId , int SemId , int DepId , DepStuPaymentVM depStuPaymentVM)
        {
            var record = _mapper.Map<DepStuPayment>(depStuPaymentVM);
            var student = await _context.DepartmentStudents.FirstOrDefaultAsync(s => s.StudentId == StdId && s.IsActive == true);
            var amount = await _context.SemesterAmounts.FirstOrDefaultAsync(a => a.SemId == SemId && a.DepId == DepId);
            if (student == null || amount == null) return NotFound("Somthing of the inputs is wrong");
            record.DepStuId = StdId;
            record.SemAmntID = SemId;
            record.Total = amount.Price;
            await _context.DepStuPayments.AddAsync(record);
            await _context.SaveChangesAsync();
            return Ok(record.Id);
        }

        [HttpGet]
        public async Task<ActionResult<List<DepStuPaymentVM>>> GetStudentPayment(int StdId = 0 )
        {
            if (StdId != 0)
                return Ok(await _context.DepStuPayments.ProjectTo<DepStuPaymentVM>(_mapper.ConfigurationProvider).Where(c => c.DepStuId == StdId).ToListAsync());
            return Ok(await _context.DepStuPayments.ProjectTo<DepStuPaymentVM>(_mapper.ConfigurationProvider).ToListAsync());
        }

        

    }
}
