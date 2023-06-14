using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        public StudentController(DataContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("List/{DepId}")] //works // Search for students by department id and year 
        [Authorize(Roles = "Manager,Teacher")]
        public async Task<ActionResult<List<DepartmentStudentVM>>> Get(int DepId, int? year = null)
        {
            if (year != null)
                return await _context.DepartmentStudents.Where(x => !x.DeletedAt.HasValue).ProjectTo<DepartmentStudentVM>(_mapper.ConfigurationProvider).Where(x => x.DepartmentId == DepId && x.Year == year).ToListAsync();
            return await _context.DepartmentStudents.Where(x => !x.DeletedAt.HasValue).ProjectTo<DepartmentStudentVM>(_mapper.ConfigurationProvider).Where(x => x.DepartmentId == DepId).ToListAsync();
        }

        [HttpGet("StudentSignIn")]  //works // change to DepStu
        [Authorize]
        public async Task<ActionResult<DepartmentStudentAllInfoVM>> StudentSignIn()
        {
            var depStu = await _context.DepartmentStudents.Where(x => !x.DeletedAt.HasValue).Include(x => x.Student).FirstOrDefaultAsync(x => x.Student.UserId == User.Identity.Name);
            if (depStu == null)
                return NotFound("No student with this id");
            return Ok(_mapper.Map<DepartmentStudentAllInfoVM>(depStu));
        }

        [HttpGet("byName/{StdnNm}")] //works
        [Authorize(Roles = "Manager,Teacher")]
        public async Task<ActionResult<List<StudentVM>>> GetStudntByName(string StdnNm)
        {
            var stu = await _context.DepartmentStudents.Where(x => !x.DeletedAt.HasValue).ProjectTo<DepartmentStudentAllInfoVM>(_mapper.ConfigurationProvider).Where(x => x.FirstName.Contains(StdnNm)).ToListAsync();
            if (stu == null)
                return NotFound("No student with this name");
            return Ok(stu);
        }

        [HttpGet("byId/{DepStuId}")] //works
        [Authorize(Roles = "Manager,Teacher")]
        public async Task<ActionResult<List<StudentVM>>> GetStudntById(int DepStuId)
        {
            var stu = await _context.DepartmentStudents.Where(x => !x.DeletedAt.HasValue).ProjectTo<DepartmentStudentAllInfoVM>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == DepStuId);
            if (stu == null)
                return NotFound("No student with this name");
            return Ok(stu);
        }

        [HttpPost("NewStudent")] //works // authentaction required      //add new student
        //[Authorize(Roles = "Manager")]
        public async Task<ActionResult<int>> Post(StudentPostVM studentVM, int DepId, bool year, int supervisorId, string email, string password)
        {
            studentVM.Id = 0;
            int strtyr;
            var student = _mapper.Map<Student>(studentVM);
            _context.Students.Add(student);
            //await _context.SaveChangesAsync();
            if (year == true)
                strtyr = 1;
            else strtyr = 0;
            var depStu = new DepartmentStudent
            {
                Student = student,
                DepartmentId = DepId,
                Year = strtyr,
                SupervisorId = supervisorId,
                EnrollmentDate = DateTime.Today,
                IsActive = true,
                Balance = 0
            };
            _context.DepartmentStudents.Add(depStu);
            await _context.SaveChangesAsync();

            var newUser = new User
            {
                Email = email,
                PasswordHash = password,
                UserName = depStu.Id.ToString(),
                PhoneNumber = student.PhoneNumber.ToString(),
                DisplayName = student.FirstName + " " + student.LastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserType = UserTypes.Student
            };

            var result = await _userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, newUser.UserType.ToString());
            }
            student.User = newUser;
            await _context.SaveChangesAsync();

            return Ok(depStu.Id);
        }

        [HttpPut("{StudentId}")]  //works // authentaction required   //Edit student info by id
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<bool>> Put(int StudentId, [FromBody] StudentPostVM studentVM,
                                                                 int depId,
                                                                 DateTime graduationDate,
                                                                 bool isActive,
                                                                 int year,
                                                                 int supervisorId)
        {
            var student = await _context.Students.Where(x => !x.DeletedAt.HasValue).FirstOrDefaultAsync(s => s.Id == StudentId);
            var DepStu = await _context.DepartmentStudents.Where(x => !x.DeletedAt.HasValue).FirstOrDefaultAsync(ds => ds.StudentId == StudentId);

            if (student == null)
                return NotFound("Id was not found");
            if (DepStu == null)
                return NotFound("Id was not found");
            studentVM.Id = student.Id;


            student.FirstName = studentVM.FirstName;
            student.LastName = studentVM.LastName;
            student.PhoneNumber = studentVM.PhoneNumber;
            student.Address = studentVM.Address;
            //student.Email = studentVM.Email;
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
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<bool>> Delete(int StudentId)
        {
            var c = await _context.Students.Where(x => !x.DeletedAt.HasValue).FirstOrDefaultAsync(c => c.Id == StudentId);
            if (c == null)
                return NotFound("ID not found");
            _context.Students.Remove(c);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpPost("DoubleMajorStudent")] // this is not working  "the problem is he is seting the student adress null "
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<int>> AddSeconedMajorStudent(DepartmentStudentPostVM departmentStudentVM)
        {
            var stu = await _context.Students.Where(x => !x.DeletedAt.HasValue).Include(dest => dest.DepartmentStudents).FirstOrDefaultAsync(x => x.Id == departmentStudentVM.StudentId);
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

        [HttpPut("AddBalance")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<bool>> PutBalance(int DepStudentId,
                                                        double Amount)
        {
            var student = await _context.DepartmentStudents.Where(x => !x.DeletedAt.HasValue).FirstOrDefaultAsync(s => s.Id == DepStudentId);
            if (student == null) return NotFound("no student with this id");
            student.Balance += Amount;

            await _context.SaveChangesAsync();

            var pay = await _context.DepStuPayments.Where(x => !x.DeletedAt.HasValue).Where(x => x.DepStuId == DepStudentId && x.IsPayed == false).ToListAsync();

            List<int>? bills = new List<int>();
            foreach (var payPayment in pay)
            {
                if (student.Balance >= payPayment.Total)
                {
                    student.Balance -= payPayment.Total;
                    payPayment.IsPayed = true;
                }
                else
                    bills.Add(payPayment.Id);
            }

            string? notPiad = "the bills with these ids couldn't be paid :";
            foreach (int bill in bills)
            {
                notPiad += " " + bill.ToString();
            }

            await _context.SaveChangesAsync();

            if (pay.Count == 0)
                notPiad = "No bills found your balance is : " + student.Balance;
            return Ok(notPiad);
            if (bills.Count > 0)
                return Ok(notPiad);
            return Ok(student.Balance);
        }
    }
}
