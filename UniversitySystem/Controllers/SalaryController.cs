using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels.SalaryVMs;

namespace UniversitySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SalaryController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("SalaryList/{SalaryId}")]//works
        public async Task<ActionResult<List<SalaryVM>>> GetSalary(int SalaryId)
        {
            var salry = await _context.Salaries.FirstOrDefaultAsync(slry => slry.Id == SalaryId);
            if (salry == null) return NotFound("no salary with this ID");
            return Ok(salry);
        }

        [HttpPut("Update Salary")] // this need changeng as  the one below "has to much info to fill" //but it's working
        public async Task<ActionResult<bool>> UpdateSalary(int SalaryId, [FromBody] SalaryPostVM salaryVM)
        {
            var salry = await _context.Salaries.FirstOrDefaultAsync(s => s.Id == SalaryId);
            if (salry == null) return NotFound("No Salary Found");

            salry.Rank = salaryVM.Rank;
            salry.Amount = salaryVM.Amount;
            salry.StartDate = salaryVM.StartDate;
            salry.EndDate = salaryVM.EndDate;
            await _context.SaveChangesAsync();
            return Ok(true);
        }
        [HttpPost("Salary")]    //this not working // error with maping
        public async Task<ActionResult<int>> AddSalary(SalaryPostVM salaryVM)
        {
            var salary = _mapper.Map<Salary>(salaryVM);
            _context.Salaries.Add(salary);
            await _context.SaveChangesAsync();
            return Ok(salary.Id);
        }
    }
}
