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

        [HttpGet("{DepId}")]
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

        //[HttpGet("{Id}")]
        //public async Task<ActionResult<DepartmentSemesterCourseVM>> Get(int Id)
        //{
        //    var c = await _context.Courses.FirstOrDefaultAsync(c => c.Id == Id);
        //    if (c == null)
        //        return NotFound("Id not found");

        //    return Ok(await _context.DepartmentSemesterCourses.FirstOrDefaultAsync(c => c.CourseId == Id));
        //}

        //[HttpPost] // only one body can be sent to a request
        //public async Task<ActionResult<bool>> Post(
        //    CourseVM courseVM,
        //    int credit,
        //    int depId,
        //    DateTime startDate,
        //    DateTime endDate,
        //    int maxStudents,
        //    List<int> teacherIds,
        //    List<WeekProgramVM> weekPrograms)
        //{
        //    var teachers = new List<SemesterCourseTeacherVM>();
        //    foreach (var id in teacherIds)
        //    {
        //        var teacher = await _context.SemesterCourseTeachers.ProjectTo<SemesterCourseTeacherVM>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(t => t.TeacherId == id);
        //        if (teacher != null)
        //            teachers.Add(teacher);
        //    }
        //    var DSCourseVM = new DepartmentSemesterCourseVM
        //    {
        //        CourseId = courseVM.Id,
        //        Course = courseVM.Name,
        //        Credit = credit,
        //        DepartmentId = depId,
        //        StartDate = startDate,
        //        EndDate = endDate,
        //        SemesterCourseTeachers = teachers,
        //        StudentsAmount = maxStudents,
        //        WeekPrograms = weekPrograms
        //    };
        //    var DSCourse = _mapper.Map<DepartmentSemesterCourse>(DSCourseVM);
        //    await _context.DepartmentSemesterCourses.AddAsync(DSCourse);
        //    await _context.SaveChangesAsync();
        //    return Ok(DSCourse.Id);
        //}
    }
}
