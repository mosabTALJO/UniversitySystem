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

        [HttpGet("{TakenCourseId}")]
        public async Task<ActionResult<AttendanceVM>> Get(int TakenCourseId)
        {
            var takenCourse = await _context.TakenCourses.Include(x => x.Attendances).FirstOrDefaultAsync(x => x.Id == TakenCourseId);
            if (takenCourse == null)
                return NotFound("no student record for this id");

            if(takenCourse.Attendances == null || takenCourse.Attendances.Count == 0)
                return BadRequest("no attendance records for this course");

            return Ok(takenCourse.Attendances.ToList());
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> Post(AttendanceVM attendanceVM)
        {
            attendanceVM.Id = 0;
            var attendance = _mapper.Map<Attendance>(attendanceVM);
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
            return Ok(attendance.Id);
        }

        [HttpPost("deviceCheck/{DepStuId}/{DepSemCourseId}")]
        public async Task<ActionResult> Post(int DepStuId, int DepSemCourseId)
        {
            var takenCourse = await _context.TakenCourses.FirstOrDefaultAsync(x => x.DepStudentId == DepStuId && x.DepSemCourseId == DepSemCourseId);
            if (takenCourse == null)
                return BadRequest("you are not enrolled in this course");

            var attendance = new Attendance
            {
                Date = DateTime.Now,
                TakenCourseId = takenCourse.Id
            };
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
