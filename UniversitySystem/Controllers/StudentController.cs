using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels.DepartmentStudentVMs;
using UniversitySystem.ViewModels.StudentVMs;

namespace UniversitySystem.Controllers
{///////////////////////////////////////////////////////////// all works /////////////////////////////////////////////////////////////
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

        [HttpGet("{DepId}")] //works // Search for students by department id and year 
        public async Task<ActionResult<List<DepartmentStudentVM>>> Get(int DepId, int? year = null)
        {
            if (year != null)
                return await _context.DepartmentStudents.ProjectTo<DepartmentStudentVM>(_mapper.ConfigurationProvider).Where(x => x.DepartmentId == DepId && x.Year == year).ToListAsync();
            return await _context.DepartmentStudents.ProjectTo<DepartmentStudentVM>(_mapper.ConfigurationProvider).Where(x => x.DepartmentId == DepId).ToListAsync();
        }

        [HttpGet("byId/{StuId}")]  //works
        public async Task<ActionResult<StudentVM>> GetStudnt(int StuId)
        {
            var stu = await _context.Students.ProjectTo<StudentVM>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == StuId);
            if (stu == null)
                return NotFound("No student with this id");
            return Ok(stu);
        }

        [HttpGet("byName/{StdnNm}")] //works
        public async Task<ActionResult<List<StudentVM>>> GetStudnt(string StdnNm)
        {
            var stu = await _context.Students.ProjectTo<StudentVM>(_mapper.ConfigurationProvider).Where(x => x.FirstName.Contains(StdnNm)).ToListAsync();
            if (stu == null)
                return NotFound("No student with this name");
            return Ok(stu);
        }

        [HttpPost ("NewStudent")] //works // authentaction required      //add new student
        public async Task<ActionResult<int>> Post(StudentPostVM studentVM, int DepId, bool year, int supervisorId)
        {
            studentVM.Id = 0;
            int strtyr;
            var student = _mapper.Map<Student>(studentVM);
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            if (year == true)
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
                    IsActive = true,
                    Balance = 0
                });
            await _context.SaveChangesAsync();
            return Ok(student.Id);
        }

        [HttpPut("{StudentId}")]  //works // authentaction required   //Edit student info by id
        public async Task<ActionResult<bool>> Put(int StudentId, [FromBody] StudentPostVM studentVM, 
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
            DepStu.Balance = DepStu.Balance;
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

        [HttpPost("DoubleMajorStudent")] // this is not working  "the problem is he is seting the student adress null "
        public async Task<ActionResult<int>> AddSeconedMajorStudent(DepartmentStudentPostVM departmentStudentVM)
        {
            var stu = await _context.Students.Include(dest => dest.DepartmentStudents).FirstOrDefaultAsync(x => x.Id == departmentStudentVM.StudentId);
            if (stu == null)
                return NotFound("wrong id");
            if (stu.DepartmentStudents.Any(x => x.DepartmentId == departmentStudentVM.DepartmentId))
                return BadRequest("this student is already enrolled in this department");

            var major = _mapper.Map<DepartmentStudent>(departmentStudentVM);
            
            major.GraduationDate = null;
            _context.DepartmentStudents.Add(major);
            await _context.SaveChangesAsync();
            return Ok(major.StudentId);

        }

        [HttpPut("AddBalance")] // it works
        public async Task<ActionResult<int>> PutBalance(int StudentId, 
                                                        [FromBody] StudentPostVM studentVM,
                                                        double Amount)
        {
            var student = await _context.DepartmentStudents.FirstOrDefaultAsync(s => s.Id == StudentId);
            if (student == null) return NotFound("no student with this id");
            student.Balance += Amount;
            await _context.SaveChangesAsync();
            return Ok(student.Id);
        }
    }
}
