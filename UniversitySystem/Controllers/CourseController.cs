using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;

namespace UniversitySystem.Controllers
{///////////////////////////////////////////////////////////// all works /////////////////////////////////////////////////////////////
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class CourseController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CourseController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet] // search for a course or get them all
        public async Task<ActionResult<List<CourseVM>>> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? search = null)
        {
            if(!string.IsNullOrEmpty(search))
                return Ok(await _context.Courses.Where(c => c.Name.Contains(search)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<CourseVM>(_mapper.ConfigurationProvider).ToListAsync());
            return Ok(await _context.Courses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<CourseVM>(_mapper.ConfigurationProvider).ToListAsync());
        }

        [HttpPost]  //add new course
        public async Task<ActionResult<int>> Post(CourseVM courseVM)
        {
            courseVM.Id = 0;
            var course = _mapper.Map<Course>(courseVM);
            //var emp = User.HasClaim(ClaimTypes.Role, "Manager");
            //var empid = await _context.Employees.Include(x =>x.User).Where(c => c.User.Id == c.UserId).FirstOrDefaultAsync();
            //course.AddedByEmployeeId = empid.Id;
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return Ok(course.Id);
        }

        [HttpPut("{Id}")] // update info of a course
        public async Task<ActionResult<bool>> Put(int Id, [FromBody] CourseVM courseVM) 
        {
            var c = await _context.Courses.FirstOrDefaultAsync(c => c.Id == Id);
            if (c == null)
                return NotFound("Id not found");
            courseVM.Id = Id;

            c.Name = courseVM.Name;
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpDelete("{Id}")]   // delete course
        public async Task<ActionResult<bool>> Delete(int Id)
        {
            var c = await _context.Courses.FirstOrDefaultAsync(c => c.Id == Id);
            if (c == null)
                return NotFound("Id not found");

            _context.Courses.Remove(c);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

    }
}
