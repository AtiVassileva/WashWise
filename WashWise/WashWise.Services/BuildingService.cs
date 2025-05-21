using Microsoft.EntityFrameworkCore;
using WashWise.Data;
using WashWise.Models;
using WashWise.Services.Contracts;

namespace WashWise.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly WashWiseDbContext _dbContext;

        public BuildingService(WashWiseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Building>> GetAll() => await _dbContext.Buildings.ToListAsync();

        public async Task<Building?> GetById(Guid id) => await _dbContext.Buildings.FirstOrDefaultAsync(b => b.Id == id);

        public async Task<bool> Exists(string name, string address, string city)
            => await _dbContext.Buildings.AnyAsync(b => b.Name == name && b.Address == address && b.City == city);

        public async Task<bool> SaveAsync(Building building)
        {
            try
            {
                await _dbContext.AddAsync(building);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> Edit(Building building, string name, string address, string city)
        {
            building.Name = name;
            building.Address = address;
            building.City = city;

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task Delete(Guid id)
        {
            var building = await GetById(id);

            if (building == null)
            {
                return;
            }

            _dbContext.Buildings.Remove(building);
            await _dbContext.SaveChangesAsync();
        }
    }
}