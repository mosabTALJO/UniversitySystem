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
    public class StudentController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public StudentController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet ("{DepId}")]
        public async Task<ActionResult<List<DepartmentStudentVM>>> Get(int DepId, int? year = null)
        {
            return await _context.DepartmentStudents.ProjectTo<DepartmentStudentVM>(_mapper.ConfigurationProvider).Where(x => x.DepartmentId == DepId && x.Year == year).ToListAsync();
        }

        [HttpPost]  // authentaction required
        public async Task<ActionResult<int>> Post(StudentVM studentVM)
        {
            var student = _mapper.Map<Student>(studentVM);            
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(student.Id);
        }

        [HttpPut ("{StudentId}")]
        public async Task<ActionResult<bool>> Put(int StudentId, [FromBody] StudentVM studentVM)
        {
            var c = await _context.Students.FirstOrDefaultAsync(c => c.Id == StudentId);
            if(c == null)
                return NotFound("ID not found");

            c.FirstName = studentVM.FirstName;
            c.LastName = studentVM.LastName;
            c.PhoneNumber = studentVM.PhoneNumber;
            c.Address = studentVM.Address;
            c.Email = studentVM.Email;
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpDelete ("{StudentId}")]

        public async Task<ActionResult<bool>> Delete(int StudentId)
        {
            var c = await _context.Students.FirstOrDefaultAsync(c => c.Id == StudentId);
            if (c == null)
                return NotFound("ID not found");
            _context.Students.Remove(c);
            await _context.SaveChangesAsync();
            return Ok(true);
        }
    }
}
