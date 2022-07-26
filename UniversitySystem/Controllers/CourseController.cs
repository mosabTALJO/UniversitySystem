using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
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

        [HttpGet("{DepId}")]
        public async Task<ActionResult<List<DepartmentSemesterCourseVM>>> Get(int DepId)
        {
            return await _context.DepartmentSemesterCourses.ProjectTo<DepartmentSemesterCourseVM>(_mapper.ConfigurationProvider).Where(x => x.DepartmentId == DepId).ToListAsync();
        }
    }
}
