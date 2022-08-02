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
    public class DepStuPaymentController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public DepStuPaymentController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddStudentPayment()
        {
            var student = await _context.DepartmentStudents.FirstOrDefaultAsync();
            var semester = await _context.Semesters.FirstOrDefaultAsync();
            return Ok();
        }

    }
}
