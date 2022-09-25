using AutoMapper;
using GreenTechManager.Core.Exceptions;
using GreenTechManager.SolarParks.Entities;
using GreenTechManager.SolarParks.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.SolarParks.Managers
{
    public interface ISolarParkManager
    {
        Task<SolarParkListModel[]> GetSolarParks();

        Task<SolarParkListModel> GetSolarPark(int turbineId);

        Task<SolarParkListModel> CreateSolarPark(SolarParkModel model);

        Task<SolarParkListModel> UpdateSolarPark(int turbineId, SolarParkModel model);

        Task DeleteSolarPark(int turbineId);
    }

    public class SolarParkManager : ISolarParkManager
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public SolarParkManager(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SolarParkListModel[]> GetSolarParks()
        {
            var parks = await _dbContext.SolarParks.ToListAsync();

            return parks.Select(_mapper.Map<SolarParkListModel>).ToArray();
        }

        public async Task<SolarParkListModel> GetSolarPark(int parkId)
        {
            var park = await _dbContext.SolarParks.FirstOrDefaultAsync(x => x.Id == parkId);

            if (park == null)
            {
                throw new NotFoundException($"Could not find park with id '{parkId}'!");
            }

            return _mapper.Map<SolarParkListModel>(park);
        }

        public async Task<SolarParkListModel> CreateSolarPark(SolarParkModel model)
        {
            var park = _mapper.Map<SolarPark>(model);

            await _dbContext.SolarParks.AddAsync(park);

            await _dbContext.SaveChangesAsync();

            return await GetSolarPark(park.Id);
        }

        public async Task<SolarParkListModel> UpdateSolarPark(int parkId, SolarParkModel model)
        {
            var park = _dbContext.SolarParks.FirstOrDefaultAsync(x => x.Id == parkId);

            await _mapper.Map(model, park);

            await _dbContext.SaveChangesAsync();

            return await GetSolarPark(parkId);
        }

        public async Task DeleteSolarPark(int parkId)
        {
            var park = await _dbContext.SolarParks.FirstOrDefaultAsync(x => x.Id == parkId);

            _dbContext.SolarParks.Remove(park);

            await _dbContext.SaveChangesAsync();
        }
    }
}
