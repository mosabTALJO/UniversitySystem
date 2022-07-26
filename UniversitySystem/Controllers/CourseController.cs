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
    public class CourseController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CourseController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CourseVM>>> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string search = null)
        {
            if(!string.IsNullOrEmpty(search))
                return Ok(await _context.Courses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<CourseVM>(_mapper.ConfigurationProvider).Where(c => c.Name.Contains(search)).ToListAsync());
            return Ok(await _context.Courses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<CourseVM>(_mapper.ConfigurationProvider).ToListAsync());
        }

        

        

        [HttpPost]
        public async Task<ActionResult<int>> Post(CourseVM courseVM)
        {
            var course = _mapper.Map<Course>(courseVM);
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return Ok(course.Id);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<bool>> Put(int Id, [FromBody] CourseVM courseVM)
        {
            var c = await _context.Courses.FirstOrDefaultAsync(c => c.Id == Id);
            if (c == null)
                return NotFound("Id not found");

            c.Name = courseVM.Name;
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpDelete("{Id}")]
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
