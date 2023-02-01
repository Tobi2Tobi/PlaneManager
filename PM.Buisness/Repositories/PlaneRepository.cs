using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PM.Buisness.Repositories.IRepositories;
using PM.Data;
using PM.Data.Entity;
using PM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Buisness.Repositories
{
    public class PlaneRepository:IPlaneRepositorie
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public PlaneRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task CreateAsync(PlaneDTO objDTO)
        {
            var obj = mapper.Map<PlaneDTO, Plane>(objDTO);
            db.Plane.Add(obj);
            await db.SaveChangesAsync();
        }


        public async Task<Plane> GetAsync(string couponName)
        {
            return await db.Plane.FirstOrDefaultAsync(u => u.Name.ToLower() == couponName.ToLower());
        }

        public async Task<IEnumerable<PlaneDTO>> GetAllAsync()
        {
            var obj = await db.Plane.ToListAsync();
            return mapper.Map<IEnumerable<Plane>, IEnumerable<PlaneDTO>>(obj);
        }

        public async Task<PlaneDTO> GetAsync(int id)
        {
            var obj = await db.Plane.FirstOrDefaultAsync(u => u.Id == id);
            return mapper.Map<Plane, PlaneDTO>(obj);
        }

        public async Task<int> RemoveAsync(int id)
        {
            var obj = await db.Plane.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                db.Plane.Remove(obj);
                return await db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task UpdateAsync(PlaneDTO objDTO)
        {
            var objFromDb = await db.Plane.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                objFromDb.Seats = objDTO.Seats;
                objFromDb.IsActive = objDTO.IsActive;

                db.Plane.Update(objFromDb);
                await db.SaveChangesAsync();
            }
        }
    }
}
