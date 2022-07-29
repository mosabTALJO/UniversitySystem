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
    public class EmployeeController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public EmployeeController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{EmployeeId}")]//works
        public async Task<ActionResult<List<EmployeeInfoVM>>> Get(int EmployeeId)
        {
            var c = await _context.Employees.FirstOrDefaultAsync(c => c.Id == EmployeeId);
            if (c == null)
                return NotFound("There is no empolyee with this ID");
            return Ok(await _context.Employees.ProjectTo<EmployeeInfoVM>(_mapper.ConfigurationProvider).Where(x => x.Id == EmployeeId).ToListAsync());
        }

        [HttpGet("SalaryList/{SalaryId}")]//works
        public async Task<ActionResult<List<SalaryVM>>> GetSalary(int SalaryId)
        {
            var salry = await _context.Salaries.FirstOrDefaultAsync(slry => slry.Id == SalaryId);
            if (salry == null) return NotFound("no salary with this ID");
            return Ok(salry);
        }

        [HttpPost("teacher")]  // authentaction required   //add new teacher and employee   // this may need change from employeeinfovm to employeevm "evenualay they need changening"
        public async Task<ActionResult<int>> AddTeacher(EmployeeInfoVM EmployeeVM, int SalaryId)
        {
            var employee = _mapper.Map<Employee>(EmployeeVM);            
            _context.Employees.Add(employee);            
            _context.Teachers.Add(new Teacher { EmployeeId = employee.Id , SalaryId  = SalaryId});
            await _context.SaveChangesAsync();
            return Ok(employee.Id);
        }

        [HttpPost("manager")]  // authentaction required   //add new manager and employee   // this too
        public async Task<ActionResult<int>> AddManager(EmployeeInfoVM EmployeeVM , int SalaryID)
        {
            var employee = _mapper.Map<Employee>(EmployeeVM);
            _context.Employees.Add(employee);
            _context.ManagementStaffs.Add(new ManagementStaff { EmployeeId = employee.Id , SalaryId = SalaryID});
            await _context.SaveChangesAsync();
            return Ok(employee.Id);
        }

        [HttpPut("{EmployeeId}")] // works
        public async Task<ActionResult<bool>> Put(int EmployeeId, [FromBody] EmployeeInfoVM EmployeeVM)
        {
            var c = await _context.Employees.FirstOrDefaultAsync(c => c.Id == EmployeeId);
            if(c == null)
                return NotFound("ID not found");

            c.FirstName = EmployeeVM.FirstName;
            c.LastName = EmployeeVM.LastName;
            c.PhoneNumber = EmployeeVM.PhoneNumber;
            c.Address = EmployeeVM.Address;
            c.Email = EmployeeVM.Email;
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpPut("Update Salary")] // this need changeng as  the one below "has to much info to fill" //but it's working
        public async Task<ActionResult<bool>> UpdateSalary(int SalaryId, [FromBody] SalaryVM SalaryVM)
        {
            var Salry = await _context.Salaries.FirstOrDefaultAsync(s => s.Id == SalaryId);
            if (Salry == null) return NotFound("No Salary Found");
            
            Salry.Rank = SalaryVM.Rank;
            Salry.Amount = SalaryVM.Amount;
            Salry.StartDate = SalaryVM.StartDate;
            Salry.EndDate = SalaryVM.EndDate;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("Salary")]    //this not working // error with maping
        public async Task<ActionResult<int>> AddSalary(SalaryVM SalaryVM)
        {
            var Salary = _mapper.Map<Salary>(SalaryVM);
            _context.Salaries.Add(Salary);
            await _context.SaveChangesAsync();
            return Ok(Salary.Id);
        }

        [HttpDelete("{EmployeeId}")]
        public async Task<ActionResult<bool>> Delete(int EmployeeId)
        {
            var c = await _context.Employees.FirstOrDefaultAsync(c => c.Id == EmployeeId);
            if (c == null)
                return NotFound("ID not found");
            _context.Employees.Remove(c);
            await _context.SaveChangesAsync();
            return Ok(true);
        }     

        

    }
}
