using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{DepStuId}")]
        public async Task<ActionResult<List<TakenCourseVM>>> Get(
            int DepStuId, 
            [FromQuery] int? year = null, 
            [FromQuery] string? guzBaharYaz = null,
            [FromQuery] bool onlyShowActive = false)
        {
            var depStu = await _context.DepartmentStudents.FirstOrDefaultAsync(ds => ds.Id == DepStuId);

            var lst = _context.TakenCourses.Where(tc => tc.DepStudentId == DepStuId);

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

        [Authorize(Roles = "Manager,Teacher")]
        [HttpGet("courseStudentsList/{DepSemCourseId}")]
        public async Task<ActionResult<List<TakenCourseVM>>> GetStudentsWhoTookACourse(int DepSemCourseId)
        {
            var depSemCourse = await _context.DepartmentSemesterCourses.FirstOrDefaultAsync(x => x.Id == DepSemCourseId);
            if (depSemCourse == null)
                return BadRequest("wrong id");

            return Ok(await _context.TakenCourses.Where(x => x.DepSemCourseId == DepSemCourseId).ProjectTo<TakenCourseVM>(_mapper.ConfigurationProvider).ToListAsync());
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("teacher/{SemCourTeacherId}")]  //teacher gets all students (taken courses) who took a course that he teaches
        public async Task<ActionResult<List<TakenCourseVM>>> Get(int SemCourTeacherId)
        {
            var semCourTeacher = await _context.SemesterCourseTeachers.FirstOrDefaultAsync(x => x.Id == SemCourTeacherId);
            if (semCourTeacher == null)
                return BadRequest("you don't teach this course");

            var result = await _context.TakenCourses.Where(x => x.SemCourseTeacherId == semCourTeacher.Id).ToListAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Manager,Student")]
        [HttpPost("{DepStuId}/{SemCourseTeacherId}")]
        public async Task<ActionResult<int>> Post(int DepStuId, int SemCourseTeacherId)
        {
            var sem = await _context.Semesters.FirstOrDefaultAsync(x => x.CourseTakingStartDate <= DateTime.Now && x.CourseTakingEndDate >= DateTime.Now);
            if (sem == null)
                return BadRequest("course taking time hasn't started or has ended");

            var depStu = await _context.DepartmentStudents.Include(ds => ds.TakenCourses).Include(ds => ds.Department).FirstOrDefaultAsync(s => s.Id == DepStuId);
            if (depStu == null)
                return NotFound("student not found");

            var SemCourTeacher = await _context.SemesterCourseTeachers.Include(x => x.DepSemCourse).FirstOrDefaultAsync(x => x.Id == SemCourseTeacherId);
            if (SemCourTeacher == null)
                return NotFound("this teacher doesn't teach this course");
            if (SemCourTeacher.NumberOfStudents >= SemCourTeacher.MaxStudents)
                return BadRequest("this teacher has reached their max student count");

            var depSemCourse = SemCourTeacher.DepSemCourse;
            if (depSemCourse == null)
                return NotFound("the course you are looking for is not found");
            if (depSemCourse.EndDate < DateTime.Now)
                return NotFound("the course you are looking for is not available this semester");
            if (depSemCourse.NumberOfStudents >= depSemCourse.MaxStudents)
                return BadRequest("this course has reached its student limit");
            if (depSemCourse.DepartmentId != depStu.DepartmentId)
                return BadRequest("you are not enrolled in this department");

            var stuTakenCourses = await _context.TakenCourses.FirstOrDefaultAsync(x => x.DepSemCourseId == depSemCourse.Id && x.DepStudentId == DepStuId);
            if (stuTakenCourses != null)
                return BadRequest("you already have this course");

            var takenCourse = new TakenCourse
            {
                DepStudentId = DepStuId,
                DepSemCourseId = depSemCourse.Id,
                SemCourseTeacherId = SemCourseTeacherId
            };
            await _context.TakenCourses.AddAsync(takenCourse);
            await _context.SaveChangesAsync();

            depSemCourse.NumberOfStudents++;
            await _context.SaveChangesAsync();

            SemCourTeacher.NumberOfStudents++;
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

        [HttpPut("approve/{DepStuId}")]
        public async Task<ActionResult<bool>> Approve(int DepStuId)
        {
            var depStu = await _context.DepartmentStudents.Include(x => x.DepStuPayments).FirstOrDefaultAsync(x => x.Id == DepStuId);
            if (depStu == null)
                return NotFound("worng student id");

            var payment = depStu.DepStuPayments.LastOrDefault();
            if (payment == null)
                return BadRequest("no payment found");
            if (!payment.IsPayed)
                return BadRequest("the payment isn't complete");

            var takenCourses = await _context.TakenCourses.Where(x => x.DepStudentId == DepStuId).ToListAsync();
            foreach(var takenCourse in takenCourses)
            {
                takenCourse.IsApproved = true;
            }
            await _context.SaveChangesAsync();

            return Ok(true);
        }
    }
}
