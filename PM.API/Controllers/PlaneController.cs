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
        public async Task<ActionResult<List<Plane>>> GetPlane()
        {
            return Ok(await _context.Planes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Plane>>> CreatePlane(Plane plane)
        {
            _context.Planes.Add(plane);
            await _context.SaveChangesAsync();

            return Ok(await _context.Planes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Plane>>> UpdatePlane(Plane plane)
        {
            var dbHero = await _context.Planes.FindAsync(plane.Id);
            if (dbHero == null)
                return BadRequest("Plane not found.");

            dbHero.Name = plane.Name;
            dbHero.Seats = plane.Seats;
            dbHero.IsActive = plane.IsActive;

            await _context.SaveChangesAsync();

            return Ok(await _context.Planes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Plane>>> DeletePlane(int id)
        {
            var dbplane = await _context.Planes.FindAsync(id);
            if (dbplane == null)
                return BadRequest("Plane not found.");

            _context.Planes.Remove(dbplane);
            await _context.SaveChangesAsync();

            return Ok(await _context.Planes.ToListAsync());
        }
    }
}
