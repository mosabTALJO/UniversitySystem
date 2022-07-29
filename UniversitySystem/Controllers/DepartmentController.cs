using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;

namespace UniversitySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public DepartmentController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("NewDepartment")]
        public async Task<ActionResult<bool>> AddDep([FromBody] DepartmentVM departmentVM)
        {
            departmentVM.Id = 0;
            var department = _mapper.Map<Department>(departmentVM);
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return Ok(department.Id);
        }
    }
}
