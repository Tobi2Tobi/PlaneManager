using PM.Data.Entity;
using PM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Buisness.Repositories.IRepositories
{
    public interface IPlaneRepositorie
    {
        Task<IEnumerable<PlaneDTO>> GetAllAsync();
        Task<PlaneDTO> GetAsync(int id);
        Task<Plane> GetAsync(string couponName);
        Task CreateAsync(PlaneDTO coupon);
        Task UpdateAsync(PlaneDTO coupon);
        Task<int> RemoveAsync(int id);
    }
}
