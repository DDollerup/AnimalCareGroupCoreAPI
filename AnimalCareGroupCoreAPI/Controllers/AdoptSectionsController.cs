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
    public class AdoptSectionsController : ControllerBase
    {
        private readonly animalcaregroupContext _context;
        private readonly IWebHostEnvironment _env;


        public AdoptSectionsController(animalcaregroupContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        // GET: api/AdoptSections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdoptSection>>> GetAdoptSections()
        {
            return await _context.AdoptSections.ToListAsync();
        }

        // GET: api/AdoptSections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdoptSection>> GetAdoptSection(long id)
        {
            var adoptSection = await _context.AdoptSections.FindAsync(id);

            if (adoptSection == null)
            {
                return NotFound();
            }

            return adoptSection;
        }

        // PUT: api/AdoptSections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdoptSection(long id, AdoptSection adoptSection)
        {
            if (id != adoptSection.Id)
            {
                return BadRequest();
            }

            string filePath = _env.WebRootPath + $"/images/adopt/";
            adoptSection.Image = Tools.ConvertBase64ToFile(adoptSection.Image, filePath);
            Tools.DeleteFile(filePath + _context.AdoptSections.AsNoTracking().FirstOrDefault(x => x.Id == id).Image);

            _context.Entry(adoptSection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdoptSectionExists(id))
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

        // POST: api/AdoptSections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdoptSection>> PostAdoptSection(AdoptSection adoptSection)
        {
            adoptSection.Image = Tools.ConvertBase64ToFile(adoptSection.Image, _env.WebRootPath + $"/images/adopt/");
            _context.AdoptSections.Add(adoptSection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdoptSection", new { id = adoptSection.Id }, adoptSection);
        }

        // DELETE: api/AdoptSections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdoptSection(long id)
        {
            var adoptSection = await _context.AdoptSections.FindAsync(id);

            string filePath = _env.WebRootPath + $"/images/adopt/";
            Tools.DeleteFile(filePath + adoptSection.Image);

            if (adoptSection == null)
            {
                return NotFound();
            }

            _context.AdoptSections.Remove(adoptSection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdoptSectionExists(long id)
        {
            return _context.AdoptSections.Any(e => e.Id == id);
        }
    }
}
