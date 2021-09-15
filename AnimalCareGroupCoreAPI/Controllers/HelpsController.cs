using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalCareGroupCoreAPI.Models;
using Microsoft.AspNetCore.Hosting;

namespace AnimalCareGroupCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpsController : ControllerBase
    {
        private readonly animalcaregroupContext _context;
        private readonly IWebHostEnvironment _env;


        public HelpsController(animalcaregroupContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        // GET: api/Helps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Help>>> GetHelps()
        {
            return await _context.Helps.ToListAsync();
        }

        // GET: api/Helps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Help>> GetHelp(long id)
        {
            var help = await _context.Helps.FindAsync(id);

            if (help == null)
            {
                return NotFound();
            }

            return help;
        }

        // PUT: api/Helps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelp(long id, Help help)
        {
            if (id != help.Id)
            {
                return BadRequest();
            }

            string filePath = _env.WebRootPath + $"/images/help/";
            help.Image = Tools.ConvertBase64ToFile(help.Image, filePath);
            Tools.DeleteFile(filePath + _context.Helps.AsNoTracking().FirstOrDefault(x => x.Id == id).Image);

            _context.Entry(help).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HelpExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Helps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Help>> PostHelp(Help help)
        {
            help.Image = Tools.ConvertBase64ToFile(help.Image, _env.WebRootPath + $"/animals/help/");

            _context.Helps.Add(help);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHelp", new { id = help.Id }, help);
        }

        // DELETE: api/Helps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHelp(long id)
        {
            var help = await _context.Helps.FindAsync(id);

            string filePath = _env.WebRootPath + $"/images/help/";
            Tools.DeleteFile(filePath + help.Image);

            if (help == null)
            {
                return NotFound();
            }

            _context.Helps.Remove(help);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HelpExists(long id)
        {
            return _context.Helps.Any(e => e.Id == id);
        }
    }
}
