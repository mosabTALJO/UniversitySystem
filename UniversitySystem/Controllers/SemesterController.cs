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
    /////////////////////////////////////////////////////////////    all works        "but when cahge don't forget to edit" /////////////////////////////////////////////////////////////
    public class SemesterController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SemesterController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("NewSemester")]
        public async Task<ActionResult<bool>> AddSem([FromBody] SemesterVM semesterVM)
        {
            semesterVM.Id = 0;
            var semester = _mapper.Map<Semester>(semesterVM);
            await _context.Semesters.AddAsync(semester);
            await _context.SaveChangesAsync();
            return Ok(semester.Id);
        }

        [HttpGet] 
        public async Task<ActionResult<List<SemesterVM>>> GetSem(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] int year = 0)
        {
            if (year != 0)
                return Ok(await _context.Semesters.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<SemesterVM>(_mapper.ConfigurationProvider).Where(c => c.Year == year).ToListAsync());
            return Ok(await _context.Semesters.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<SemesterVM>(_mapper.ConfigurationProvider).ToListAsync());
        }

        [HttpPut] // when u add the course adding date change this
        public async Task<ActionResult<string>> UpdateSem(            
            [FromBody] SemesterVM semesterVM,
            int SemesterId)
        {
            var sem = await _context.Semesters.FirstOrDefaultAsync(dp => dp.Id == SemesterId);
            if (sem == null)
                return BadRequest("no semester like this");
                        
            
            await _context.SaveChangesAsync();
            return Ok(sem.GuzBaharYaz);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DltSem(            
            [FromQuery] int? semId = null)
        {
            var sem = await _context.Semesters.FirstOrDefaultAsync(dp => dp.Id == semId);
            if (sem == null)
                return BadRequest("no department with this name");
            _context.Semesters.Remove(sem);
            await _context.SaveChangesAsync();
            return Ok("true");
        }


    }
}
