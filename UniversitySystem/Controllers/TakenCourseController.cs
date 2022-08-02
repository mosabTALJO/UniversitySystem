using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels.TakenCourseVMs;

namespace UniversitySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TakenCourseController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public TakenCourseController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{StuId}")]
        public async Task<ActionResult<List<TakenCourseVM>>> Get(
            int StuId, 
            [FromQuery] int? year = null, 
            [FromQuery] string? guzBaharYaz = null,
            [FromQuery] bool onlyShowActive = false)
        {
            var stu = await _context.Students.FirstOrDefaultAsync(s => s.Id == StuId);
            var depStu = await _context.DepartmentStudents.FirstOrDefaultAsync(ds => ds.StudentId == StuId);

            var lst = _context.TakenCourses.Where(tc => tc.DepStudentId == StuId);

            if(onlyShowActive)
                lst = lst.Where(tc => tc.Grade == null);

            if (year != null && guzBaharYaz != null)
            {
                lst = lst.Where(tc => tc.DepSemCourse.Semester.Year == year && tc.DepSemCourse.Semester.GuzBaharYaz == guzBaharYaz);
                if (lst.Count() == 0)
                    return BadRequest("no courses found in this semester");
            }

            return Ok(await lst.ToListAsync());
        }

        [HttpGet("teacher/{TeacherId}/{CourseId}")] //teacher gets all students (taken courses) who took a course that he teaches
        public async Task<ActionResult<List<TakenCourseVM>>> Get(int TeacherId, int CourseId)
        {
            var depSemCourse = await _context.DepartmentSemesterCourses.FirstOrDefaultAsync(x => x.CourseId == CourseId && x.EndDate == null);
            if (depSemCourse == null)
                return NotFound("this course is not open this semester");

            var semCourTeacher = await _context.SemesterCourseTeachers.FirstOrDefaultAsync(x => x.TeacherId == TeacherId && x.DepSemCourseId == depSemCourse.Id);
            if (semCourTeacher == null)
                return BadRequest("you don't teach this course");

            var result = await _context.TakenCourses.Where(x => x.TeacherId == semCourTeacher.Id && x.DepSemCourseId == depSemCourse.Id).ToListAsync();
            return Ok(result);
        }

        [HttpPost("{StuId}/{DepSemCourseId}")]
        public async Task<ActionResult<int>> Post(int StuId, int DepSemCourseId)
        {
            var stu = await _context.Students.FirstOrDefaultAsync(s => s.Id == StuId);
            if (stu == null)
                return NotFound("student not found");

            var depSemCourse = await _context.DepartmentSemesterCourses.FirstOrDefaultAsync(dsc => dsc.Id == DepSemCourseId);
            if (depSemCourse == null || depSemCourse.EndDate != null)
                return NotFound("the course you are looking for is not found or is not available this semester");
            if (depSemCourse.NumberOfStudents >= depSemCourse.MaxStudents)
                return BadRequest("this course has reached its student limit");

            var takenCourse = new TakenCourse
            {
                DepStudentId = StuId,
                DepSemCourseId = DepSemCourseId
            };
            await _context.TakenCourses.AddAsync(takenCourse);
            await _context.SaveChangesAsync();

            depSemCourse.NumberOfStudents++;
            await _context.SaveChangesAsync();

            return Ok(takenCourse.Id);
        }

        [HttpPut("updateGrade/{TakenCourseId}")]
        public async Task<ActionResult<bool>> UpdateGrade(int TakenCourseId, double grade)
        {
            var takenCourse = await _context.TakenCourses.FirstOrDefaultAsync(x => x.Id == TakenCourseId);
            if (takenCourse == null)
                return NotFound("Id not found");

            takenCourse.Grade = grade;
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpDelete("{TakenCourseId}")]
        public async Task<ActionResult<bool>> Delete(int TakenCourseId)
        {
            var takenCourse = await _context.TakenCourses.FirstOrDefaultAsync(x => x.Id == TakenCourseId);
            if (takenCourse == null)
                return NotFound("Id not found");

            _context.TakenCourses.Remove(takenCourse);
            await _context.SaveChangesAsync();

            var temp = takenCourse.DepSemCourse;
            var depSemCour = await _context.DepartmentSemesterCourses.FirstOrDefaultAsync(x => x == temp);
            depSemCour.NumberOfStudents--;
            await _context.SaveChangesAsync();
            return Ok(true);
        }
    }
}
