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
    ///////////////////////////////////////////////////////////////// all works /////////////////////////////////////////////////////////////////
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
        public async Task<ActionResult<List<SemesterCourseTeacherVM>>> SearchCourseTeacher(int teacherId,
                                                                                           [FromQuery] int pageNumber = 1,
                                                                                           [FromQuery] int pageSize = 20)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(c => c.Id == teacherId);
            if (teacher == null)
                return NotFound("NO teacher with id like this");

            var semCourTeacherVMList = await _context.SemesterCourseTeachers.Where(sct => sct.TeacherId == teacherId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<SemesterCourseTeacherVM>(_mapper.ConfigurationProvider).ToListAsync();
            return Ok(semCourTeacherVMList);
        }
        
        [HttpGet("onlyActives/{teacherId}")] // works
        public async Task<ActionResult<List<SemesterCourseTeacherVM>>> SearchActiveCourseTeacher(int teacherId)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(c => c.Id == teacherId);
            if (teacher == null)
                return NotFound("NO teacher with id like this");

            var semCourTeacherVMList = await _context.SemesterCourseTeachers.Where(sct => sct.TeacherId == teacherId && sct.EndDate > DateTime.Now).ProjectTo<SemesterCourseTeacherVM>(_mapper.ConfigurationProvider).ToListAsync();
            return Ok(semCourTeacherVMList);
        }

        [HttpPost("AddCourseToTeacher")] // it works
        public async Task<ActionResult<int>> AddCourseTeacher([FromBody] SemesterCourseTeacherPostVM SemesterCourseTeacherVM)
        {
            var DepSemcourse = await _context.DepartmentSemesterCourses.Where(dsm => dsm.Id == SemesterCourseTeacherVM.DepSemCourseId).FirstOrDefaultAsync(x => x.EndDate > DateTime.Now);
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

        [HttpPut("{Id}")] // it works
        public async Task<ActionResult<bool>> UpdateCourseTeacher(int Id, [FromBody] SemesterCourseTeacherPostVM semesterCourseTeacherVM)
        {
            var EditCourse = await _context.SemesterCourseTeachers.FirstOrDefaultAsync(c => c.Id == Id);
            if (EditCourse == null) return NotFound("nothing found with this ID");

            EditCourse.StartDate = semesterCourseTeacherVM.StartDate;
            EditCourse.EndDate = semesterCourseTeacherVM.EndDate;
                        
            //_context.SemesterCourseTeachers.Update(EditCourse);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpDelete("{Id}")]   //works
        public async Task<ActionResult<bool>> Delete(int Id)
        {
            var DeleteCourse = await _context.SemesterCourseTeachers.FirstOrDefaultAsync(c => c.Id == Id);
            if (DeleteCourse == null) return NotFound("there is no courses with this ID ");
            _context.SemesterCourseTeachers.Remove(DeleteCourse);
            await _context.SaveChangesAsync();
            return Ok(true);
        }


    }
}
