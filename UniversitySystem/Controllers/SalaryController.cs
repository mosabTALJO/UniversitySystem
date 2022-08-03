using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels.SalaryVMs;

namespace UniversitySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    ///////////////////////////////////////////////////////////// all works /////////////////////////////////////////////////////////////
    public class SalaryController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SalaryController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("CurrentSalary/{EmployeeId}")]//works
        public async Task<ActionResult<SalaryVM>> GetSalary(int EmployeeId)// THIS GETS THE CURRENT SALARY
        {
            var curntsalry = await _context.Salaries.ProjectTo<SalaryVM>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(slry => slry.EmployeeId == EmployeeId && slry.EndDate == null);
            if (curntsalry == null) return NotFound("no corrent salary for this employee");
            return Ok(curntsalry);
        }

        [HttpGet("employeeOldSalaryRecords/{EmployeeId}")] // GET All SALAREIS 
        public async Task<ActionResult<List<SalaryVM>>> GetAllSalaries(int EmployeeId)
        {
            var allsalrys = await _context.Salaries.Where(slry => slry.EmployeeId == EmployeeId).ToListAsync();
            if (allsalrys == null) return NotFound("no salary for this employee");

            var allsalryVMlist = new List<SalaryVM>();
            foreach (var salary in allsalrys)
            {
                allsalryVMlist.Add(_mapper.Map<SalaryVM>(salary));
            }
            return Ok(allsalryVMlist);
        }

        [HttpPut("EndSalary")] //works //sets an end date for the salary
        public async Task<ActionResult<bool>> UpdateSalary(int EmployeeId)
        {
            var salry = await _context.Salaries.FirstOrDefaultAsync(s => s.EmployeeId == EmployeeId && s.EndDate == null);
            if (salry == null) return NotFound("No employee Found");
            
            salry.EndDate = DateTime.Today;
            await _context.SaveChangesAsync();
            return Ok("true");
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteSlryRec(int slryID)
        {
            var slry = await _context.Salaries.FirstOrDefaultAsync(s => s.Id == slryID && s.EndDate == null);
            if (slry == null) return NotFound("no record with this id");

            _context.Salaries.Remove(slry);
            await _context.SaveChangesAsync();
            return Ok("true");
        }

        [HttpPost("NewSalaryRecord")] // works  adds new salary record
        public async Task<ActionResult<int>> AddNewSalaryRecord(SalaryVM salaryVM)
        {
            var emp = await _context.Employees.Include(e => e.Salaries).FirstOrDefaultAsync(e => e.Id == salaryVM.EmployeeId);
            if (emp == null) return NotFound("no emloyee with this id");

            var currentSalary = emp.Salaries.FirstOrDefault(e => e.EndDate == null);
            if (currentSalary != null)
                currentSalary.EndDate = DateTime.Today;

            salaryVM.Id = 0;
            salaryVM.EndDate = null;
            var slry = _mapper.Map<Salary>(salaryVM);
            await _context.Salaries.AddAsync(slry);
            await _context.SaveChangesAsync();
            return Ok(slry.Id);
        }
    
    }
}
