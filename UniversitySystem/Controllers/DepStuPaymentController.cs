using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;

namespace UniversitySystem.Controllers
{///////////////////////////////////////////////////////////// all works /////////////////////////////////////////////////////////////
    [Route("api/[controller]")]
        [ApiController]
    public class DepStuPaymentController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public DepStuPaymentController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost] // it works
        public async Task<ActionResult<bool>> AddStudentPayment(int StdId , int SemId , int DepId , DepStuPaymentVM depStuPaymentVM)
        {
            int ttlcrdt = 0;

            var record = _mapper.Map<DepStuPayment>(depStuPaymentVM);
            
            var student = await _context.DepartmentStudents.FirstOrDefaultAsync(s => s.StudentId == StdId && s.IsActive == true);
            var amount = await _context.SemesterAmounts.Include(sem => sem.Sem).FirstOrDefaultAsync(a => a.SemId == SemId && a.DepId == DepId);
            var courses = await _context.TakenCourses.Include(dsc => dsc.DepSemCourse).Where(c => c.DepStudentId == StdId && c.Grade == null).ToListAsync();
            
            foreach (var course in courses)
            {
                ttlcrdt += course.DepSemCourse.Credit;
            }
            
            if (student == null || amount == null) 
            {
                return NotFound("Somthing of the inputs is wrong");                
            }

            record.Total = amount.Price;
            if (amount.Sem.GuzBaharYaz.Contains("yaz")) 
                record.Total = amount.Price * ttlcrdt;                
                        
            record.IsPayed = false;
            if (student.Balance >= record.Total)
            {
                student.Balance -= record.Total;
                record.IsPayed = true;
            }

            record.DepStuId = StdId;
            record.SemAmntID = SemId;

            await _context.DepStuPayments.AddAsync(record);
            await _context.SaveChangesAsync();
            return Ok(record.Id);
        }

        [HttpGet] // it works
        public async Task<ActionResult<List<DepStuPaymentVM>>> GetStudentPayment(int StdId = 0 )
        {
            if (StdId != 0)
                return Ok(await _context.DepStuPayments.ProjectTo<DepStuPaymentVM>(_mapper.ConfigurationProvider).Where(c => c.DepStuId == StdId).ToListAsync());
            return Ok(await _context.DepStuPayments.ProjectTo<DepStuPaymentVM>(_mapper.ConfigurationProvider).ToListAsync());
        }

        [HttpDelete] // it works
        public async Task<ActionResult<bool>> DltPayment(int studId , int semId)
        {
            var delt = await _context.DepStuPayments.FirstOrDefaultAsync(d => d.DepStuId == studId && d.SemAmntID == semId);
            if (delt == null) return NotFound("NO PAYMENT WITH THIS ID");
            _context.DepStuPayments.Remove(delt);
            await _context.SaveChangesAsync();
            return Ok("true");
        }

        [HttpPut]
        public async Task<ActionResult<bool>> IsPiad(int DepstudId, int semId )// add a discount => "u can add it as a double between 0 and 100 then divaid ie on 100" <==
        {
            var stu = await _context.DepStuPayments.FirstOrDefaultAsync(d => d.DepStuId == DepstudId && d.SemAmntID == semId && d.IsPayed == false);
            var depstu = await _context.DepartmentStudents.FirstOrDefaultAsync(ds => ds.Id == DepstudId);
            
            if (stu == null) return NotFound("no not paid bills for this student with this id");

            if (stu.Total > depstu.Balance) return BadRequest("the balance is not enough");

            depstu.Balance -= stu.Total;
            stu.IsPayed = true;

            await _context.SaveChangesAsync();
            return Ok(stu.IsPayed);
        }

        public static async Task<ActionResult<int>>  AutoAddStudentPayment(int StdId, int SemId, int DepId, double Price ,DataContext _context,IMapper _mapper , DepStuPayment depStuPaymentVM)
        {
            int ttlcrdt = 0;
            var record = _mapper.Map<DepStuPayment>(depStuPaymentVM);

            var student = await _context.DepartmentStudents.FirstOrDefaultAsync(s => s.StudentId == StdId && s.IsActive == true);
            var amount = await _context.SemesterAmounts.Include(sem => sem.Sem).FirstOrDefaultAsync(a => a.SemId == SemId && a.DepId == DepId);
            var courses = await _context.TakenCourses.Include(dsc => dsc.DepSemCourse).Where(c => c.DepStudentId == StdId && c.Grade == null).ToListAsync();

            foreach (var course in courses)
            {
                ttlcrdt += course.DepSemCourse.Credit;
            }

            if (student == null )
            {
                return 0;
            }
            
            if (amount.Sem.GuzBaharYaz.Contains("yaz"))
                record.Total = Price * ttlcrdt;

            record.IsPayed = false;
            if (student.Balance >= record.Total)
            {
                student.Balance -= record.Total;
                record.IsPayed = true;
            }

            record.DepStuId = StdId;
            record.SemAmntID = SemId;

            await _context.DepStuPayments.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }


    }
}
