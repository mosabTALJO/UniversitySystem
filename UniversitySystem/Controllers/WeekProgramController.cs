using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels.WeekProgramVMs;

namespace UniversitySystem.Controllers
{///////////////////////////////////////////////////////////// all works /////////////////////////////////////////////////////////////
    [Route("api/[controller]")]
    [ApiController]
    public class WeekProgramController : Controller
    {


        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public WeekProgramController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<ActionResult<List<WeekProgramVM>>> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] int Sem = 0)

        {
            if (Sem != 0)
                return Ok(await _context.WeekPrograms.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<WeekProgramVM>(_mapper.ConfigurationProvider).Where(c => c.DepSemCourseId == Sem).ToListAsync());
            return Ok(await _context.WeekPrograms.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<WeekProgramVM>(_mapper.ConfigurationProvider).ToListAsync());
            
        }

        [HttpPost] 
        public async Task<ActionResult<int>> AddToWeekProgram(WeekProgramPostVM weekProgramPostVM)
        {
            var endd = await _context.DepartmentSemesterCourses.Where(d => d.StartDate > DateTime.Now && d.Id == weekProgramPostVM.DepSemCourseId).FirstOrDefaultAsync();
            if (endd == null)
                return BadRequest("Can't add a course in this period of time");
            var wkprog = _mapper.Map<WeekProgram>(weekProgramPostVM);
            await _context.WeekPrograms.AddAsync(wkprog);
            await _context.SaveChangesAsync();
            return Ok(wkprog.Id);
        }


        [HttpPut("{depSemCourseId}")]
        public async Task<ActionResult<bool>> UpdateProg(int depSemCourseId, [FromBody] WeekProgramPostVM weekProgramPostVM)
        {
            var editprog = await _context.WeekPrograms.FirstOrDefaultAsync(pg => pg.DepSemCourseId == depSemCourseId);
            if (editprog == null)
                return NotFound("no Course with this id");
            editprog.DepSemCourseId = weekProgramPostVM.DepSemCourseId;
            editprog.Day = weekProgramPostVM.Day;
            editprog.Start = weekProgramPostVM.Start;
            editprog.End = weekProgramPostVM.End;
            await _context.SaveChangesAsync();
            return Ok("true");
        }

        [HttpDelete("{depSemCourseId}")]
        public async Task<ActionResult<bool>> DltProg(int depSemCourseId,
                                                       string? day = null )
        {
            var dltprog = await _context.WeekPrograms.FirstOrDefaultAsync(pg => pg.DepSemCourseId == depSemCourseId && pg.Day.Contains(day));
            if (dltprog == null)
                return NotFound("nothig found with this id");
            _context.WeekPrograms.Remove(dltprog);
            await _context.SaveChangesAsync();
            return Ok("true");
        }
    
    }
        
}
