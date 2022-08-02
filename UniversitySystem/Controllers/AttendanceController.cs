using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;

namespace UniversitySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AttendanceController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{StuId}/{CourseId}")]
        public async Task<ActionResult<AttendanceVM>> Get(int StuId, int CourseId)
        {
            var depStu = await _context.DepartmentStudents.FirstOrDefaultAsync(x => x.StudentId == StuId);
            if(depStu == null)
                return NotFound("no student record for this id");

            var takenCourse = await _context.TakenCourses.FirstOrDefaultAsync(x => x.DepStudentId == depStu.Id && x.DepSemCourse.CourseId == CourseId && x.Grade == null);
            if (takenCourse == null)
                return NotFound();
            return Ok(takenCourse.Attendances.ToList());
        }
        
        //[HttpPost]
        //public async Task<ActionResult<AttendanceVM>> Post(Attendance attendance)
        //{
            
        //}
    }
}
