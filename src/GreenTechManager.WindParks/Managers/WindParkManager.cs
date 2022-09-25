using AutoMapper;
using GreenTechManager.Core.Exceptions;
using GreenTechManager.WindParks.Entities;
using GreenTechManager.WindParks.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.WindParks.Managers
{
    public interface IWindParkManager
    {
        Task<WindParkListModel[]> GetWindParks();

        Task<WindParkListModel> GetWindPark(int windParkId);

        Task<WindParkListModel> CreateWindPark(WindParkModel model);

        Task<WindParkListModel> UpdateWindPark(int windParkId, WindParkModel model);

        Task DeleteWindPark(int windParkId);
    }

    public class WindParkManager : IWindParkManager
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public WindParkManager(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<WindParkListModel[]> GetWindParks()
        {
            var suppliers = await _dbContext.WindParks
                .Select(x => new
                {
                    WindPark = x,
                    MaxPowerOutput = x.WindTurbines.Sum(t => t.PowerOutput),
                    NumberOfTurbines = x.WindTurbines.Count
                })
                .ToListAsync();

            return suppliers
                .Select(x => _mapper.Map(x.WindPark, new WindParkListModel
                {
                    MaxPowerOuput = x.MaxPowerOutput,
                    NumberOfTurbines = x.NumberOfTurbines
                })).ToArray();
        }

        public async Task<WindParkListModel> GetWindPark(int windParkId)
        {
            var supplier = await _dbContext.WindParks.FirstOrDefaultAsync(x => x.Id == windParkId);

            if (supplier == null)
            {
                throw new NotFoundException($"Could not find supplier with id '{windParkId}'!");
            }

            return _mapper.Map<WindParkListModel>(supplier);
        }

        public async Task<WindParkListModel> CreateWindPark(WindParkModel model)
        {
            var supplier = _mapper.Map<WindPark>(model);

            await _dbContext.WindParks.AddAsync(supplier);

            await _dbContext.SaveChangesAsync();
            
            return await GetWindPark(supplier.Id);
        }

        public async Task<WindParkListModel> UpdateWindPark(int windParkId, WindParkModel model)
        {
            var supplier = _dbContext.WindParks.FirstOrDefaultAsync(x => x.Id == windParkId);

            await _mapper.Map(model, supplier);

            await _dbContext.SaveChangesAsync();

            return await GetWindPark(windParkId);
        }

        public async Task DeleteWindPark(int windParkId)
        {
            var supplier = await _dbContext.WindParks.FirstOrDefaultAsync(x => x.Id == windParkId);

            _dbContext.WindParks.Remove(supplier);

            await _dbContext.SaveChangesAsync();
        }
    }
}
