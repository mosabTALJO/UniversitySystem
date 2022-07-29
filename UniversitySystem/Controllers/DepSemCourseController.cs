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
    public class DepSemCourseController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public DepSemCourseController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetList/{DepId}")]
        public async Task<ActionResult<List<DepartmentSemesterCourseVM>>> Get(
            int DepId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? search = null)
        {
            if (!string.IsNullOrEmpty(search))
                return Ok(await _context.DepartmentSemesterCourses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<DepartmentSemesterCourseVM>(_mapper.ConfigurationProvider).Where(c => c.Course.Contains(search)).ToListAsync());
            return Ok(await _context.DepartmentSemesterCourses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<DepartmentSemesterCourseVM>(_mapper.ConfigurationProvider).Where(x => x.DepartmentId == DepId).ToListAsync());
        }

        [HttpGet("{Id}")] //gets departments that opened a certain course
        public async Task<ActionResult<List<DepartmentSemesterCourseVM>>> Get(int Id)
        {
            var c = await _context.Courses.FirstOrDefaultAsync(c => c.Id == Id);
            if (c == null)
                return NotFound("Id not found");

            return Ok(_context.DepartmentSemesterCourses.ProjectTo<DepartmentSemesterCourseVM>(_mapper.ConfigurationProvider).Where(c => c.CourseId == Id));
        }

        [HttpPost("New Department Semester Course")] // only one body can be sent to a request
        public async Task<ActionResult<bool>> Post([FromBody] DepartmentSemesterCourseVM DepSemCourseVM)
        {
            var DepSemCourse = _mapper.Map<DepartmentSemesterCourse>(DepSemCourseVM);
            await _context.DepartmentSemesterCourses.AddAsync(DepSemCourse);
            await _context.SaveChangesAsync();
            return Ok(DepSemCourse.Id);
        }

        [HttpPost("New Department")]
        public async Task<ActionResult<bool>> AddDep([FromBody] DepartmentVM DepartmentVM)
        {
            var Department = _mapper.Map<Department>(DepartmentVM);
            await _context.Departments.AddAsync(Department);
            await _context.SaveChangesAsync();
            return Ok();   
        }
    }
}
