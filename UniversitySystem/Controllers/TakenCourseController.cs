using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels.DepartmentStudentVMs;
using UniversitySystem.ViewModels.TakenCourseVMs;

namespace UniversitySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //add approving 
    //add teachers
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

            return Ok(await lst.ProjectTo<TakenCourseVM>(_mapper.ConfigurationProvider).ToListAsync());
        }

        [HttpGet("courseStudentsList/{DepSemCourseId}")]
        public async Task<ActionResult<List<TakenCourseVM>>> GetStudentsWhoTookACourse(int DepSemCourseId)
        {
            var depSemCourse = await _context.DepartmentSemesterCourses.FirstOrDefaultAsync(x => x.Id == DepSemCourseId);
            if (depSemCourse == null)
                return BadRequest("wrong id");

            return Ok(await _context.TakenCourses.Where(x => x.DepSemCourseId == DepSemCourseId).ProjectTo<TakenCourseVM>(_mapper.ConfigurationProvider).ToListAsync());
        }

        [HttpGet("teacher/{TeacherId}/{CourseId}")] //change the ids************** //teacher gets all students (taken courses) who took a course that he teaches
        public async Task<ActionResult<List<TakenCourseVM>>> Get(int TeacherId, int CourseId)
        {
            var depSemCourse = await _context.DepartmentSemesterCourses.FirstOrDefaultAsync(x => x.CourseId == CourseId && x.EndDate == null);
            if (depSemCourse == null)
                return NotFound("this course is not open this semester");

            var semCourTeacher = await _context.SemesterCourseTeachers.FirstOrDefaultAsync(x => x.TeacherId == TeacherId && x.DepSemCourseId == depSemCourse.Id);
            if (semCourTeacher == null)
                return BadRequest("you don't teach this course");

            var result = await _context.TakenCourses.Where(x => x.SemCourseTeacherId == semCourTeacher.Id && x.DepSemCourseId == depSemCourse.Id).ToListAsync();
            return Ok(result);
        }

        [HttpPost("{DepStuId}/{DepSemCourseId}/{SemCourseTeacherId}")]
        public async Task<ActionResult<int>> Post(int DepStuId, int DepSemCourseId, int SemCourseTeacherId)
        {
            var depStu = await _context.DepartmentStudents.Include(ds => ds.TakenCourses).Include(ds => ds.Department).FirstOrDefaultAsync(s => s.Id == DepStuId);
            if (depStu == null)
                return NotFound("student not found");

            var stuTakenCourses = await _context.TakenCourses.FirstOrDefaultAsync(x => x.DepSemCourseId == DepSemCourseId && x.DepStudentId == DepStuId);
            if (stuTakenCourses != null)
                return BadRequest("you already have this course");

            var depSemCourse = await _context.DepartmentSemesterCourses.FirstOrDefaultAsync(dsc => dsc.Id == DepSemCourseId);
            if (depSemCourse == null)
                return NotFound("the course you are looking for is not found");
            if (depSemCourse.EndDate < DateTime.Now)
                return NotFound("the course you are looking for is not available this semester");
            if (depSemCourse.NumberOfStudents >= depSemCourse.MaxStudents)
                return BadRequest("this course has reached its student limit");
            if (depSemCourse.DepartmentId != depStu.DepartmentId)
                return BadRequest("you are not enrolled in this department");

            var SemCourTeacher = await _context.SemesterCourseTeachers.FirstOrDefaultAsync(x => x.Id == SemCourseTeacherId);
            if (SemCourTeacher == null)
                return NotFound("this teacher doesn't teach this course");
            if (SemCourTeacher.NumberOfStudents >= SemCourTeacher.MaxStudents)
                return BadRequest("this teacher has reached their max student count");

            var takenCourse = new TakenCourse
            {
                DepStudentId = DepStuId,
                DepSemCourseId = DepSemCourseId,
                SemCourseTeacherId = SemCourseTeacherId
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
            var takenCourse = await _context.TakenCourses.Include(tc => tc.DepSemCourse).FirstOrDefaultAsync(x => x.Id == TakenCourseId);
            if (takenCourse == null)
                return NotFound("Id not found");

            _context.TakenCourses.Remove(takenCourse);
            await _context.SaveChangesAsync();

            //var temp = takenCourse.DepSemCourse;
            var depSemCour = await _context.DepartmentSemesterCourses.FirstOrDefaultAsync(x => x == takenCourse.DepSemCourse);
            depSemCour.NumberOfStudents--;
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        
    }
}
