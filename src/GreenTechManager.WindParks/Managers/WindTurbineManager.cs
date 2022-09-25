using AutoMapper;
using Microsoft.EntityFrameworkCore;
using GreenTechManager.Core.Exceptions;
using GreenTechManager.WindParks.Entities;
using GreenTechManager.WindParks.Models;

namespace GreenTechManager.WindParks.Managers
{
    public interface IWindTurbineManager
    {
        Task<WindTurbineListModel[]> GetWindTurbines();

        Task<WindTurbineListModel> GetWindTurbine(int turbineId);

        Task<WindTurbineListModel> CreateWindTurbine(WindTurbineModel model);

        Task<WindTurbineListModel> UpdateWindTurbine(int turbineId, WindTurbineModel model);

        Task DeleteWindTurbine(int turbineId);
    }

    public class WindTurbineManager : IWindTurbineManager
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public WindTurbineManager(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<WindTurbineListModel[]> GetWindTurbines()
        {
            var turbines = await _dbContext.WindTurbines.ToListAsync();

            return turbines.Select(_mapper.Map<WindTurbineListModel>).ToArray();
        }

        public async Task<WindTurbineListModel> GetWindTurbine(int turbineId)
        {
            var turbine = await _dbContext.WindTurbines.FirstOrDefaultAsync(x => x.Id == turbineId);

            if (turbine == null)
            {
                throw new NotFoundException($"Could not find article with articlenumber '{turbineId}'!");
            }

            return _mapper.Map<WindTurbineListModel>(turbine);
        }

        public async Task<WindTurbineListModel> CreateWindTurbine(WindTurbineModel model)
        {
            var turbine = _mapper.Map<WindTurbine>(model);

            await _dbContext.WindTurbines.AddAsync(turbine);

            await _dbContext.SaveChangesAsync();

            return await GetWindTurbine(turbine.Id);
        }

        public async Task<WindTurbineListModel> UpdateWindTurbine(int turbineId, WindTurbineModel model)
        {
            var article = _dbContext.WindTurbines.FirstOrDefaultAsync(x => x.Id == turbineId);

            await _mapper.Map(model, article);

            await _dbContext.SaveChangesAsync();

            return await GetWindTurbine(turbineId);
        }

        public async Task DeleteWindTurbine(int turbineId)
        {
            var turbine = await _dbContext.WindTurbines.FirstOrDefaultAsync(x => x.Id == turbineId);

            _dbContext.WindTurbines.Remove(turbine);

            await _dbContext.SaveChangesAsync();
        }
    }
}
