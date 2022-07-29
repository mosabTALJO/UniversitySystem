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

        [HttpGet("{DepId}")]  // Search for students by department id and year 
        public async Task<ActionResult<List<DepartmentStudentVM>>> Get(int DepId, int? year = null)
        {
            if (year != null)
                return await _context.DepartmentStudents.ProjectTo<DepartmentStudentVM>(_mapper.ConfigurationProvider).Where(x => x.DepartmentId == DepId && x.Year == year).ToListAsync();
            return await _context.DepartmentStudents.ProjectTo<DepartmentStudentVM>(_mapper.ConfigurationProvider).Where(x => x.DepartmentId == DepId).ToListAsync();
        }

        [HttpGet("student name")]// this is not working whatt ever is the name he is going null
        public async Task<ActionResult<List<StudentVM>>> GetStudnt(string StdnNm)
        {
            var name = await _context.Students.ProjectTo<StudentVM>(_mapper.ConfigurationProvider).Where(nm => nm.FirstName == StdnNm).ToListAsync();
            if (name != null)
                return NotFound("No student with tis name");
            return Ok(name);
        }

        [HttpPost ("New student")]  // authentaction required      //add new student
        public async Task<ActionResult<int>> Post(StudentVM studentVM, int DepId, bool year, int supervisorId)
        {
            studentVM.Id = 0;
            int strtyr;
            var student = _mapper.Map<Student>(studentVM);
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
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

        [HttpPut("{StudentId}")] // authentaction required   //Edit student info by id
        public async Task<ActionResult<bool>> Put(int StudentId, [FromBody] StudentVM studentVM, 
                                                                 int depId,
                                                                 DateTime graduationDate,
                                                                 bool isActive,
                                                                 int year,
                                                                 int supervisorId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == StudentId);
            var DepStu = await _context.DepartmentStudents.FirstOrDefaultAsync(ds => ds.StudentId == StudentId);

            if (student == null)
                return NotFound("Id was not found");
            if (DepStu == null)
                return NotFound("Id was not found");
            studentVM.Id = student.Id;


            student.FirstName = studentVM.FirstName;
            student.LastName = studentVM.LastName;
            student.PhoneNumber = studentVM.PhoneNumber;
            student.Address = studentVM.Address;
            student.Email = studentVM.Email;
            DepStu.DepartmentId = depId;
            DepStu.Year = year;
            DepStu.SupervisorId = supervisorId;
            DepStu.IsActive = isActive;
            DepStu.GraduationDate = graduationDate;
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpDelete("{StudentId}")] // authentaction required //delete a student by id

        public async Task<ActionResult<bool>> Delete(int StudentId)
        {
            var c = await _context.Students.FirstOrDefaultAsync(c => c.Id == StudentId);
            if (c == null)
                return NotFound("ID not found");
            _context.Students.Remove(c);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpPost("Double major student")] // this is not working  "the problem is he is seting the student adress null "
        public async Task<ActionResult<int>> AddSeconedMajorStudent(DepartmentStudentVM DepartmentStudentVM)
        {
            var major = _mapper.Map<DepartmentStudent>(DepartmentStudentVM);
            _context.DepartmentStudents.Add(major);
            await _context.SaveChangesAsync();
            return Ok(major.StudentId);

        }
    }
}
