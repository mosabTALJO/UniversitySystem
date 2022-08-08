using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Manager,Teacher")]
        [HttpGet("{teacherId}")] // works
        public async Task<ActionResult<List<SemesterCourseTeacherVM>>> SearchCourseTeacher(int teacherId,
                                                                                           [FromQuery] int pageNumber = 1,
                                                                                           [FromQuery] int pageSize = 20,
                                                                                           [FromQuery] bool activeOnly = false)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(c => c.Id == teacherId);
            if (teacher == null)
                return NotFound("NO teacher with id like this");

            if (activeOnly)
                return Ok(await _context.SemesterCourseTeachers.Where(x => !x.DeletedAt.HasValue).Where(sct => sct.TeacherId == teacherId && sct.EndDate > DateTime.Now).ProjectTo<SemesterCourseTeacherVM>(_mapper.ConfigurationProvider).ToListAsync());

            return Ok(await _context.SemesterCourseTeachers.Where(x => !x.DeletedAt.HasValue).Where(sct => sct.TeacherId == teacherId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<SemesterCourseTeacherVM>(_mapper.ConfigurationProvider).ToListAsync());
        }

        //[HttpGet("onlyActives/{teacherId}")] // works//*********************the query above does this now************************************
        //public async Task<ActionResult<List<SemesterCourseTeacherVM>>> SearchActiveCourseTeacher(int teacherId)
        //{
        //    var teacher = await _context.Teachers.FirstOrDefaultAsync(c => c.Id == teacherId);
        //    if (teacher == null)
        //        return NotFound("NO teacher with id like this");

        //    var semCourTeacherVMList = await _context.SemesterCourseTeachers.Where(sct => sct.TeacherId == teacherId && sct.EndDate > DateTime.Now).ProjectTo<SemesterCourseTeacherVM>(_mapper.ConfigurationProvider).ToListAsync();
        //    return Ok(semCourTeacherVMList);
        //}

        [Authorize]
        [HttpGet("teachers/{DepSemCourseId}")]
        public async Task<ActionResult<List<SemesterCourseTeacherVM>>> GetTeachersGivingACourse(int DepSemCourseId)
        {
            var depSemCourse = await _context.DepartmentSemesterCourses.Where(x => !x.DeletedAt.HasValue).Include(x => x.SemesterCourseTeachers).FirstOrDefaultAsync(x => x.Id == DepSemCourseId);
            if (depSemCourse == null)
                return NotFound("wrong id");

            if (depSemCourse.SemesterCourseTeachers == null)
                return BadRequest("this course has no teachers at the moment");

            var semCourTeachers = new List<SemesterCourseTeacherVM>();
            foreach (var x in depSemCourse.SemesterCourseTeachers)
            {
                semCourTeachers.Add(_mapper.Map<SemesterCourseTeacherVM>(x));
            }

            return Ok(semCourTeachers);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("AddCourseToTeacher")] // it works
        public async Task<ActionResult<int>> AddCourseTeacher([FromBody] SemesterCourseTeacherPostVM SemesterCourseTeacherVM)
        {
            var DepSemcourse = await _context.DepartmentSemesterCourses.Where(x => !x.DeletedAt.HasValue).Where(dsm => dsm.Id == SemesterCourseTeacherVM.DepSemCourseId).FirstOrDefaultAsync(x => x.EndDate > DateTime.Now);
            if (DepSemcourse == null)
                return BadRequest("this course doesn't exist or its time has ended");

            var temp = await _context.SemesterCourseTeachers.Where(x => !x.DeletedAt.HasValue).Where(x => x.TeacherId == SemesterCourseTeacherVM.TeacherId).FirstOrDefaultAsync(x => x.DepSemCourseId == DepSemcourse.Id && x.EndDate < DateTime.Now);
            if (temp != null)
                return BadRequest("this teacher already teaches this course");

            var NewCourseTeacher = _mapper.Map<SemesterCourseTeacher>(SemesterCourseTeacherVM);
            await _context.SemesterCourseTeachers.AddAsync(NewCourseTeacher);
            await _context.SaveChangesAsync();
            return Ok(NewCourseTeacher.Id);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("{Id}")] // it works
        public async Task<ActionResult<bool>> UpdateCourseTeacher(int Id, [FromBody] SemesterCourseTeacherPostVM semesterCourseTeacherVM)
        {
            var EditCourse = await _context.SemesterCourseTeachers.Where(x => !x.DeletedAt.HasValue).FirstOrDefaultAsync(c => c.Id == Id);
            if (EditCourse == null) return NotFound("nothing found with this ID");

            EditCourse.StartDate = semesterCourseTeacherVM.StartDate;
            EditCourse.EndDate = semesterCourseTeacherVM.EndDate;

            //_context.SemesterCourseTeachers.Update(EditCourse);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{Id}")]   //works
        public async Task<ActionResult<bool>> Delete(int Id)
        {
            var DeleteCourse = await _context.SemesterCourseTeachers.Where(x => !x.DeletedAt.HasValue).FirstOrDefaultAsync(c => c.Id == Id);
            if (DeleteCourse == null) return NotFound("there is no courses with this ID ");
            _context.SemesterCourseTeachers.Remove(DeleteCourse);
            await _context.SaveChangesAsync();
            return Ok(true);
        }


    }
}
