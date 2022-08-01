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

        [HttpGet("{Id}")]
        public async Task<ActionResult<List<TakenCourseVM>>> Get(
            int Id, 
            [FromQuery] int? year = null, 
            [FromQuery] string? guzBaharYaz = null,
            [FromQuery] bool onlyShowActive = false)
        {
            var stu = await _context.Students.FirstOrDefaultAsync(s => s.Id == Id);
            var depStu = await _context.DepartmentStudents.FirstOrDefaultAsync(ds => ds.StudentId == Id);

            var lst = _context.TakenCourses.Where(tc => tc.StudentId == Id);

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

        [HttpPost("{StuId}/{DepSemCourseId}")]
        public async Task<ActionResult<int>> Post(int StuId, int DepSemCourseId)
        {
            var stu = await _context.Students.FirstOrDefaultAsync(s => s.Id == StuId);
            if (stu == null)
                return NotFound("student not found");

            var depSemCourse = await _context.DepartmentSemesterCourses.FirstOrDefaultAsync(dsc => dsc.Id == DepSemCourseId);
            if (depSemCourse == null || depSemCourse.EndDate != null)
                return NotFound("the course you are looking for is not found or is not available this semester");

            var takenCourse = new TakenCourse
            {
                StudentId = StuId,
                DepSemCourseId = DepSemCourseId
            };
            await _context.TakenCourses.AddAsync(takenCourse);
            await _context.SaveChangesAsync();

            return Ok(takenCourse.Id);
        }

        
    }
}
