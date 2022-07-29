using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels.SemesterCourseTeacherVMs;

namespace UniversitySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterCourseTeacherController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SemesterCourseTeacherController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{teacherId}")] // works
        public async Task<ActionResult<List<SemesterCourseTeacherVM>>> SearchCourseTeacher(int teacherId)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(c => c.Id == teacherId);
            if (teacher == null)
                return NotFound("NO Course with id like this");

            var semCourTeacherList = await _context.SemesterCourseTeachers.Where(sct => sct.TeacherId == teacherId).ToListAsync();
            var semCourTeacherVMList = new List<SemesterCourseTeacherVM>();
            foreach (var item in semCourTeacherList)
            {
                semCourTeacherVMList.Add(_mapper.Map<SemesterCourseTeacherVM>(item));
            }
            return Ok(semCourTeacherVMList);
        }

        [HttpPost("AddCourseToTeacher")] // it works
        public async Task<ActionResult<int>> AddCourseTeacher([FromBody] SemesterCourseTeacherPostVM SemesterCourseTeacherVM)
        {
            var DepSemcourse = await _context.DepartmentSemesterCourses.Where(dsm => dsm.CourseId == SemesterCourseTeacherVM.CourseId).FirstOrDefaultAsync(x => x.EndDate > DateTime.Now);
            if (DepSemcourse == null)
                return BadRequest("this course doesn't exist or its time has ended");

            var temp = await _context.SemesterCourseTeachers.Where(x => x.TeacherId == SemesterCourseTeacherVM.TeacherId).FirstOrDefaultAsync(x => x.DepSemCourseId == DepSemcourse.Id && x.EndDate < DateTime.Now);
            if (temp != null)
                return BadRequest("this teacher already teaches this course");

            var NewCourseTeacher = new SemesterCourseTeacher
            {
                DepSemCourseId = DepSemcourse.Id,
                TeacherId = SemesterCourseTeacherVM.TeacherId,
                StartDate = SemesterCourseTeacherVM.StartDate,
                EndDate = SemesterCourseTeacherVM.EndDate
            };
            await _context.SemesterCourseTeachers.AddAsync(NewCourseTeacher);
            await _context.SaveChangesAsync();
            return Ok(NewCourseTeacher.Id);
        }

        [HttpPut("{ID}")] // for some reason this not working 
        public async Task<ActionResult<bool>> UpdateCourseTeacher(int ID, [FromBody] SemesterCourseTeacherVM SemesterCourseTeacherVM)
        {
            var EditCourse = await _context.SemesterCourseTeachers.FirstOrDefaultAsync(c => c.Id == ID);
            if (EditCourse == null) return NotFound("nothing found with this ID");

            EditCourse.StartDate = SemesterCourseTeacherVM.StartDate;
            EditCourse.EndDate = SemesterCourseTeacherVM.EndDate;
                        
            //_context.SemesterCourseTeachers.Update(EditCourse);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpDelete("{ID}")]   //works
        public async Task<ActionResult<bool>> Delete(int ID)
        {
            var DeleteCourse = await _context.SemesterCourseTeachers.FirstOrDefaultAsync(c => c.Id == ID);
            if (DeleteCourse == null) return NotFound("there is no courses with this ID ");
            _context.SemesterCourseTeachers.Remove(DeleteCourse);
            await _context.SaveChangesAsync();
            return Ok(true);
        }


    }
}
