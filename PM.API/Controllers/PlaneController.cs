using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PM.Data;
using PM.Data.Entity;

namespace PM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaneController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlaneController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Plane>>> GetSuperHeroes()
        {
            return Ok(await _context.Planes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Plane>>> CreateSuperHero(Plane hero)
        {
            _context.Planes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Planes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Plane>>> UpdateSuperHero(Plane hero)
        {
            var dbHero = await _context.Planes.FindAsync(hero.Id);
            if (dbHero == null)
                return BadRequest("Hero not found.");

            dbHero.Name = hero.Name;
            dbHero.Seats = hero.Seats;
            dbHero.IsActive = hero.IsActive;

            await _context.SaveChangesAsync();

            return Ok(await _context.Planes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Plane>>> DeleteSuperHero(int id)
        {
            var dbHero = await _context.Planes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found.");

            _context.Planes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Planes.ToListAsync());
        }
    }
}
