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
    public class SemesterCourseTeacherController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SemesterCourseTeacherController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet] // works
        public async Task<ActionResult<List<SemesterCourseTeacherVM>>> SearchCourseTeacher(int STCId)
        {
            var Courseid = await _context.SemesterCourseTeachers.FirstOrDefaultAsync(c => c.Id == STCId);
            if (Courseid == null)
                return NotFound("NO Course with id like this");

            return Ok(Courseid);
            //_context.SemesterCourseTeachers.ProjectTo<SemesterCourseTeacherVM>(_mapper.ConfigurationProvider).Where(c => c.CourseId == Courseid)
        }

        [HttpPost("ADD new Course to a teacher for this semester")] // it works
        public async Task<ActionResult<int>> AddCourseTeacher([FromBody] SemesterCourseTeacherVM SemesterCourseTeacherVM)
        {
            var NewCourse = _mapper.Map<SemesterCourseTeacher>(SemesterCourseTeacherVM);
            await _context.SemesterCourseTeachers.AddAsync(NewCourse);
            await _context.SaveChangesAsync();
            return Ok(NewCourse.Id);
        }

        [HttpPut("{ID}")] // for some reason this not working 
        public async Task<ActionResult<bool>> UpdateCourseTeacher(int ID, [FromBody] SemesterCourseTeacherVM SemesterCourseTeacherVM)
        {
            var EditCourse = await _context.SemesterCourseTeachers.FirstOrDefaultAsync(c => c.Id == ID);
            if (EditCourse == null) return NotFound("nothing found with this ID");

            //EditCourse.CourseId = SemesterCourseTeacherVM.CourseId;
            //EditCourse.TeacherId = SemesterCourseTeacherVM.TeacherId;
            //EditCourse.StartDate = SemesterCourseTeacherVM.StartDate;   // this 4 work just fine
            //EditCourse.EndDate = SemesterCourseTeacherVM.EndDate; 
            EditCourse = _mapper.Map<SemesterCourseTeacher>(SemesterCourseTeacherVM); // this saves in a new record
            //EditCourse = _mapper.Map<SemesterCourseTeacher>(EditCourse); // this not updateing at all 
            if (EditCourse.CourseId == 0)
                return NotFound("can't set");
            if (EditCourse.TeacherId == 0)
                return NotFound("can't set");
            _context.SemesterCourseTeachers.Update(EditCourse);
            await _context.SaveChangesAsync();
            return Ok(EditCourse);
        }

        [HttpDelete("{ID}")]   //works
        public async Task<ActionResult<bool>> Delete(int ID)
        {
            var DeleteCourse = await _context.SemesterCourseTeachers.FirstOrDefaultAsync(c => c.Id == ID);
            if(DeleteCourse == null) return NotFound("there is no courses with this ID ");
            _context.SemesterCourseTeachers.Remove(DeleteCourse);
            await _context.SaveChangesAsync();
            return Ok(true);
        }


    }
}
