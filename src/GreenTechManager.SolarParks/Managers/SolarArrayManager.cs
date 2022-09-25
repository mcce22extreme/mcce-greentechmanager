using AutoMapper;
using GreenTechManager.Core.Exceptions;
using GreenTechManager.SolarParks;
using GreenTechManager.SolarParks.Entities;
using GreenTechManager.SolarParks.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.WindParks.Managers
{
    public interface ISolarArrayManager
    {
        Task<SolarArrayListModel[]> GetSolarArrays();

        Task<SolarArrayListModel> GetSolarArray(int arrayId);

        Task<SolarArrayListModel> CreateSolarArray(SolarParkModel model);

        Task<SolarArrayListModel> UpdateSolarArray(int arrayId, SolarParkModel model);

        Task DeleteSolarArray(int arrayId);
    }

    public class SolarArrayManager : ISolarArrayManager
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public SolarArrayManager(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SolarArrayListModel[]> GetSolarArrays()
        {
            var arrays = await _dbContext.SolarArrays.ToListAsync();

            return arrays.Select(_mapper.Map<SolarArrayListModel>).ToArray();
        }

        public async Task<SolarArrayListModel> GetSolarArray(int arrayId)
        {
            var array = await _dbContext.SolarArrays.FirstOrDefaultAsync(x => x.Id == arrayId);

            if (array == null)
            {
                throw new NotFoundException($"Could not find array with id '{arrayId}'!");
            }

            return _mapper.Map<SolarArrayListModel>(array);
        }

        public async Task<SolarArrayListModel> CreateSolarArray(SolarParkModel model)
        {
            var array = _mapper.Map<SolarArray>(model);

            await _dbContext.SolarArrays.AddAsync(array);

            await _dbContext.SaveChangesAsync();

            return await GetSolarArray(array.Id);
        }

        public async Task<SolarArrayListModel> UpdateSolarArray(int arrayId, SolarParkModel model)
        {
            var array = _dbContext.SolarArrays.FirstOrDefaultAsync(x => x.Id == arrayId);

            await _mapper.Map(model, array);

            await _dbContext.SaveChangesAsync();

            return await GetSolarArray(arrayId);
        }

        public async Task DeleteSolarArray(int arrayId)
        {
            var array = await _dbContext.SolarArrays.FirstOrDefaultAsync(x => x.Id == arrayId);

            _dbContext.SolarArrays.Remove(array);

            await _dbContext.SaveChangesAsync();
        }
    }
}
