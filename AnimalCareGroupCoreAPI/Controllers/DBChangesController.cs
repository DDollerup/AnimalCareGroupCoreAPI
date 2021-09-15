using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalCareGroupCoreAPI.Models;

namespace AnimalCareGroupCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBChangesController : ControllerBase
    {
        private readonly animalcaregroupContext _context;

        public DBChangesController(animalcaregroupContext context)
        {
            _context = context;
        }

        // GET: api/DBChanges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DBChanges>>> GetDBChanges()
        {
            return await _context.DBChanges.ToListAsync();
        }

        // GET: api/DBChanges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DBChanges>> GetDBChanges(long id)
        {
            var dBChanges = await _context.DBChanges.FindAsync(id);

            if (dBChanges == null)
            {
                return NotFound();
            }

            return dBChanges;
        }

        // PUT: api/DBChanges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDBChanges(long id, DBChanges dBChanges)
        {
            if (id != dBChanges.Id)
            {
                return BadRequest();
            }

            _context.Entry(dBChanges).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DBChangesExists(id))
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

        // POST: api/DBChanges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DBChanges>> PostDBChanges(DBChanges dBChanges)
        {
            _context.DBChanges.Add(dBChanges);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DBChangesExists(dBChanges.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDBChanges", new { id = dBChanges.Id }, dBChanges);
        }

        // DELETE: api/DBChanges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDBChanges(long id)
        {
            var dBChanges = await _context.DBChanges.FindAsync(id);
            if (dBChanges == null)
            {
                return NotFound();
            }

            _context.DBChanges.Remove(dBChanges);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DBChangesExists(long id)
        {
            return _context.DBChanges.Any(e => e.Id == id);
        }
    }
}
