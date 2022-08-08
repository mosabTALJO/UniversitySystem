using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;

namespace UniversitySystem.Controllers
{ ///////////////////////////////////////////////////////////// all works /////////////////////////////////////////////////////////////
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class DepartmentController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public DepartmentController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("NewDepartment")] // works
        public async Task<ActionResult<bool>> AddDep([FromBody] DepartmentVM departmentVM)
        {
            departmentVM.Id = 0;
            var department = _mapper.Map<Department>(departmentVM);
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return Ok(department.Id);
        }

        [HttpGet] // works
        public async Task<ActionResult<List<DepartmentVM>>> GetDep(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? search = null)
        {
            if (!string.IsNullOrEmpty(search))
                return Ok(await _context.Departments.Where(x => !x.DeletedAt.HasValue).Where(c => c.Name.Contains(search)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<DepartmentVM>(_mapper.ConfigurationProvider).ToListAsync());
            return Ok(await _context.Departments.Where(x => !x.DeletedAt.HasValue).Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<DepartmentVM>(_mapper.ConfigurationProvider).ToListAsync());
        }

        [HttpPut] // works
        public async Task<ActionResult<string>> UpdateDep(            
            [FromBody] DepartmentVM departmentVM,
            [FromQuery] string? DepName = null)
        {
            var dep = await _context.Departments.Where(x => !x.DeletedAt.HasValue).FirstOrDefaultAsync(dp => dp.Name.Contains(DepName));
            if (dep == null)
                return BadRequest("no department with this name");
            
            dep.Name = departmentVM.Name;
            await _context.SaveChangesAsync();
            return Ok(dep.Name);
        }

        [HttpDelete]  // works
        public async Task<ActionResult<bool>> DltDep(            
            [FromQuery] string? DepName = null)
        {
            var dep = await _context.Departments.Where(x => !x.DeletedAt.HasValue).FirstOrDefaultAsync(dp => dp.Name.Contains(DepName));
            if (dep == null)
                return BadRequest("no department with this name");
            _context.Departments.Remove(dep);
            await _context.SaveChangesAsync();
            return Ok("true");
        }


    }
}
