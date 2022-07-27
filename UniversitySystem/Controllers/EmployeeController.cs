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

        [HttpGet ("{EmployeeId}")]
        public async Task<ActionResult<List<EmployeeVM>>> Get(int EmployeeId)
        {
            return await _context.Employees.ProjectTo<EmployeeVM>(_mapper.ConfigurationProvider).Where(x => x.Id == EmployeeId).ToListAsync();
        }

        [HttpPost]  // authentaction required   //add new teacher and employee
        public async Task<ActionResult<int>> AddTeacher(EmployeeVM EmployeeVM, int SalaryId)
        {
            var employee = _mapper.Map<Employee>(EmployeeVM);            
            _context.Employees.Add(employee);            
            _context.Teachers.Add(new Teacher { EmployeeId = employee.Id , SalaryId  = SalaryId});
            await _context.SaveChangesAsync();
            return Ok(employee.Id);
        }

        [HttpPost]  // authentaction required   //add new manager and employee
        public async Task<ActionResult<int>> AddManager(EmployeeVM EmployeeVM , int SalaryID)
        {
            var employee = _mapper.Map<Employee>(EmployeeVM);
            _context.Employees.Add(employee);
            _context.ManagementStaffs.Add(new ManagementStaff { EmployeeId = employee.Id , SalaryId = SalaryID});
            await _context.SaveChangesAsync();
            return Ok(employee.Id);
        }

        [HttpPut ("{EmployeeId}")]
        public async Task<ActionResult<bool>> Put(int EmployeeId, [FromBody] EmployeeVM EmployeeVM)
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

        [HttpDelete ("{EmployeeId}")]

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
