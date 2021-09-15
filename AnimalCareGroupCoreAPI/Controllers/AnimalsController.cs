﻿using System;
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
    public class AnimalsController : ControllerBase
    {
        private readonly animalcaregroupContext _context;
        private readonly IWebHostEnvironment _env;


        public AnimalsController(animalcaregroupContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            return await _context.Animals.ToListAsync();
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(long id)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(long id, Animal animal)
        {
            if (id != animal.Id)
            {
                return BadRequest();
            }

            string filePath = _env.WebRootPath + $"/images/animals/";
            animal.Image = Tools.ConvertBase64ToFile(animal.Image, filePath);
            Tools.DeleteFile(filePath + _context.Animals.AsNoTracking().FirstOrDefault(x => x.Id == id).Image);

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
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

        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            animal.Image = Tools.ConvertBase64ToFile(animal.Image, _env.WebRootPath + $"/images/animals/");

            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(long id)
        {
            var animal = await _context.Animals.FindAsync(id);

            string filePath = _env.WebRootPath + $"/images/animals/";
            Tools.DeleteFile(filePath + animal.Image);

            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimalExists(long id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }
    }
}