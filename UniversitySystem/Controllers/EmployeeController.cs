using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;
using UniversitySystem.ViewModels.EmployeeVM;

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

        [HttpPost("teacher")]  // authentaction required   //add new teacher and employee
        public async Task<ActionResult<int>> AddTeacher(EmployeeInfoVM EmployeeVM, int SalaryId)
        {
            EmployeeVM.Id = 0;
            var employee = _mapper.Map<Employee>(EmployeeVM);            
            _context.Employees.Add(employee);            
            await _context.SaveChangesAsync();
            _context.Teachers.Add(new Teacher { EmployeeId = employee.Id });
            await _context.SaveChangesAsync();
            return Ok(employee.Id);
        }

        [HttpPost("manager")]  // authentaction required   //add new manager and employee
        public async Task<ActionResult<int>> AddManager(EmployeeInfoVM EmployeeVM , int SalaryID)
        {
            var employee = _mapper.Map<Employee>(EmployeeVM);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            _context.ManagementStaffs.Add(new ManagementStaff { EmployeeId = employee.Id });
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
