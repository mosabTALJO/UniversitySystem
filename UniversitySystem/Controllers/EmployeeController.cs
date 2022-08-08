using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;
using UniversitySystem.ViewModels.EmployeeVM;
using UniversitySystem.ViewModels.SalaryVMs;

namespace UniversitySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class EmployeeController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public EmployeeController(DataContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = "Manager,Teacher")]
        [HttpGet("EmployeeSignIn")]//works
        public async Task<ActionResult<List<EmployeeInfoVM>>> EmployeeSignIn()
        {
            var c = await _context.Employees.FirstOrDefaultAsync(c => c.UserId == User.Identity.Name);
            if (c == null)
                return NotFound("There is no empolyee with this ID");
            return Ok(c);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("{EmployeeId}")]//works
        public async Task<ActionResult<List<EmployeeInfoVM>>> Get(int EmployeeId)
        {
            var c = await _context.Employees.FirstOrDefaultAsync(c => c.Id == EmployeeId);
            if (c == null)
                return NotFound("There is no empolyee with this ID");
            return Ok(c);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("teacher")]  // authentaction required   //add new teacher and employee //this works
        public async Task<ActionResult<int>> AddTeacher(EmployeeInfoVM EmployeeVM , int amount, string rank, string email, string password)
        {
            EmployeeVM.Id = 0;            
            var employee = _mapper.Map<Employee>(EmployeeVM);            
            _context.Employees.Add(employee);            
            await _context.SaveChangesAsync();

            _context.Salaries.Add(new Salary { StartDate = DateTime.Now, EndDate = null, EmployeeId = employee.Id, Amount = amount, Rank = rank });
            _context.Teachers.Add(new Teacher { EmployeeId = employee.Id });
            await _context.SaveChangesAsync();

            var newUser = new User
            {
                Email = email,
                PasswordHash = password,
                UserName = employee.Id.ToString(),
                PhoneNumber = employee.PhoneNumber.ToString(),
                DisplayName = employee.FirstName + " " + employee.LastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserType = UserTypes.Teacher
            };

            var result = await _userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, newUser.UserType.ToString());
            }
            await _context.SaveChangesAsync();
            employee.UserId = newUser.Id;
            await _context.SaveChangesAsync();

            return Ok(employee.Id);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("manager")]  // authentaction required   //add new manager and employee
        public async Task<ActionResult<int>> AddManager(EmployeeInfoVM EmployeeVM, int amount, string rank, string email, string password)
        {
            var employee = _mapper.Map<Employee>(EmployeeVM);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            var temp = User.Claims;
            _context.Salaries.Add(new Salary { StartDate = DateTime.Now, EndDate = null, EmployeeId = employee.Id, Amount = amount, Rank = rank });
            _context.ManagementStaffs.Add(new ManagementStaff { EmployeeId = employee.Id });
            await _context.SaveChangesAsync();

            var newUser = new User
            {
                Email = email,
                PasswordHash = password,
                UserName = employee.Id.ToString(),
                PhoneNumber = employee.PhoneNumber.ToString(),
                DisplayName = employee.FirstName + " " + employee.LastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserType = UserTypes.Manager
            };

            var result = await _userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, newUser.UserType.ToString());
            }
            await _context.SaveChangesAsync();
            employee.UserId = newUser.Id;
            await _context.SaveChangesAsync();

            return Ok(employee.Id);
        }

        [Authorize(Roles = "Manager")]
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
            //c.Email = EmployeeVM.Email;
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [Authorize(Roles = "Manager")]
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
