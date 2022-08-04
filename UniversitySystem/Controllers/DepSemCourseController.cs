using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;
using UniversitySystem.ViewModels.DepartmentSemesterCourseVM;

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

        ///////////////////////////////////////////////////all works//////////////////////////////////////////////////

        [HttpGet("GetList/{DepId}")]
        public async Task<ActionResult<List<DepartmentSemesterCourseVM>>> Get(
            int DepId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? search = null,
            [FromQuery] int? year = null,
            [FromQuery] string? guzBaharYaz = null)
        {
            if (!string.IsNullOrEmpty(search))
                return Ok(await _context.DepartmentSemesterCourses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<DepartmentSemesterCourseVM>(_mapper.ConfigurationProvider).Where(x => x.DepartmentId == DepId).Where(c => c.Course.Contains(search)).ToListAsync());
            if (year != null && !string.IsNullOrEmpty(guzBaharYaz))
                return Ok(await _context.DepartmentSemesterCourses.Skip((pageNumber - 1) * pageSize).Take(pageSize).Where(x => x.DepartmentId == DepId && x.Semester.Year == year && x.Semester.GuzBaharYaz == guzBaharYaz).ProjectTo<DepartmentSemesterCourseVM>(_mapper.ConfigurationProvider).ToListAsync());
            return Ok(await _context.DepartmentSemesterCourses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<DepartmentSemesterCourseVM>(_mapper.ConfigurationProvider).Where(x => x.DepartmentId == DepId).ToListAsync());
        }

        [HttpGet("{Id}")] //gets departments that opened a certain course //this seems unnecessary
        public async Task<ActionResult<List<DepartmentSemesterCourseVM>>> Get(int Id)
        {
            var c = await _context.Courses.FirstOrDefaultAsync(c => c.Id == Id);
            if (c == null)
                return NotFound("Id not found");

            return Ok(_context.DepartmentSemesterCourses.Where(c => c.CourseId == Id).ProjectTo<DepartmentSemesterCourseVM>(_mapper.ConfigurationProvider));
        }

        [HttpPost("NewDepartmentSemesterCourse")]  
        public async Task<ActionResult<int>> Post([FromBody] DepartmentSemesterCoursePostVM DepSemCourseVM)
        {
            DepSemCourseVM.NumberOfStudents = 0;
            var DepSemCourse = _mapper.Map<DepartmentSemesterCourse>(DepSemCourseVM);

            if(_context.DepartmentSemesterCourses.Any(x => x.CourseId == DepSemCourse.CourseId && x.DepartmentId == DepSemCourse.DepartmentId && x.EndDate != null))
                return BadRequest("this course is already open");

            await _context.DepartmentSemesterCourses.AddAsync(DepSemCourse);
            await _context.SaveChangesAsync();
            return Ok(DepSemCourse.Id);
        }

        [HttpPut("{DepSemCourseId}")]
        public async Task<ActionResult<bool>> Put(int DepSemCourseId, [FromBody] DepartmentSemesterCoursePostVM DepSemCourseVM)
        {
            var depSemCourse = await _context.DepartmentSemesterCourses.FirstOrDefaultAsync(dsm => dsm.Id == DepSemCourseId);
            if (depSemCourse == null)
                return NotFound("Id not found");

            depSemCourse.Credit = DepSemCourseVM.Credit;
            depSemCourse.MaxStudents = DepSemCourseVM.MaxStudents;
            depSemCourse.EndDate = DepSemCourseVM.EndDate;

            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpDelete("{DepSemCourseId}")]
        public async Task<ActionResult<bool>> Delete(int DepSemCourseId)
        {
            var depSemCourse = await _context.DepartmentSemesterCourses.FirstOrDefaultAsync(dsm => dsm.Id == DepSemCourseId);
            if (depSemCourse == null)
                return NotFound("Id not found");

            _context.DepartmentSemesterCourses.Remove(depSemCourse);
            await _context.SaveChangesAsync();
            return Ok(true);
        }
    }
}
