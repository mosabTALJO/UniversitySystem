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

        [HttpGet ("{DepId}")]  // Search for students by department id and year 
        public async Task<ActionResult<List<DepartmentStudentVM>>> Get(int DepId, int? year = null)
        {
            return await _context.DepartmentStudents.ProjectTo<DepartmentStudentVM>(_mapper.ConfigurationProvider).Where(x => x.DepartmentId == DepId && x.Year == year).ToListAsync();
        }

        [HttpPost]  // authentaction required      //add new student
        public async Task<ActionResult<int>> Post(StudentVM studentVM, int DepId ,bool year, int supervisorId)
        {
            int strtyr;
            var student = _mapper.Map<Student>(studentVM);            
            _context.Students.Add(student);
            if (year = true)
                strtyr = 1;
            else strtyr = 0;
            _context.DepartmentStudents.Add(
                new DepartmentStudent
                {
                    StudentId = student.Id,
                    DepartmentId = DepId,
                    Year = strtyr,
                    SupervisorId = supervisorId,
                    EnrollmentDate = DateTime.Today,
                    IsActive = true
                });
            await _context.SaveChangesAsync();
            return Ok(student.Id);
        }

        [HttpPut ("{StudentId}")] // authentaction required   //Edit student info by id
        public async Task<ActionResult<bool>> Put(int StudentId, [FromBody] StudentVM studentVM, [FromBody] DepartmentStudent DepartmentStudent)
        {
            var stdntup = await _context.Students.FirstOrDefaultAsync(c => c.Id == StudentId);
            var depstdntup = await _context.DepartmentStudents.FirstOrDefaultAsync(c => c.StudentId == StudentId);
            if(stdntup == null)
                return NotFound("ID not found");

            stdntup.FirstName = studentVM.FirstName;
            stdntup.LastName = studentVM.LastName;
            stdntup.PhoneNumber = studentVM.PhoneNumber;
            stdntup.Address = studentVM.Address;
            stdntup.Email = studentVM.Email;
            depstdntup.DepartmentId = DepartmentStudent.DepartmentId;
            depstdntup.Year = DepartmentStudent.Year;
            depstdntup.SupervisorId = DepartmentStudent.SupervisorId;
            depstdntup.IsActive = DepartmentStudent.IsActive;
            depstdntup.GraduationDate = DepartmentStudent.GraduationDate;
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpDelete ("{StudentId}")] // authentaction required //delete a student by id

        public async Task<ActionResult<bool>> Delete(int StudentId)
        {
            var c = await _context.Students.FirstOrDefaultAsync(c => c.Id == StudentId);
            if (c == null)
                return NotFound("ID not found");
            _context.Students.Remove(c);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        //[HttpPost]

        //public async Task<ActionResult<int>> AddSeconedMajorStudent (int DepId , DepartmentStudentVM DepartmentStudentVM)
        //{
        //    var major = _mapper.Map<DepartmentStudent>(DepartmentStudentVM);
        //    _context.DepartmentStudents.Add(major);

        //    return Ok();   

        //}
    }
}
