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

        Task<SolarParkListModel> GetSolarPark(int solarParkId);

        Task<SolarParkListModel> CreateSolarPark(SolarParkModel model);

        Task<SolarParkListModel> UpdateSolarPark(int solarParkId, SolarParkModel model);

        Task DeleteSolarPark(int solarParkId);

        Task<OperatorModel[]> GetSolarParkOperators();
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
            var solarParks = await _dbContext
                .SolarParks
                .Include(x => x.Operator)
                .ToListAsync();

            return solarParks.Select(_mapper.Map<SolarParkListModel>).ToArray();
        }

        public async Task<SolarParkListModel> GetSolarPark(int solarParkId)
        {
            var solarPark = await _dbContext
                .SolarParks
                .Include(x => x.Operator)
                .FirstOrDefaultAsync(x => x.Id == solarParkId);

            if (solarPark == null)
            {
                throw new NotFoundException($"Could not find solarpark with id '{solarParkId}'!");
            }

            return _mapper.Map<SolarParkListModel>(solarPark);
        }

        private async Task VerifyOperator(int? operatorId)
        {
            var op = await _dbContext.Operators.FirstOrDefaultAsync(x => x.Id == operatorId);

            if (op == null)
            {
                throw new NotFoundException($"Could not find operator with id '{operatorId}'!");
            }
        }

        public async Task<SolarParkListModel> CreateSolarPark(SolarParkModel model)
        {
            await VerifyOperator(model?.OperatorId);

            var solarPark = _mapper.Map<SolarPark>(model);

            await _dbContext.SolarParks.AddAsync(solarPark);

            await _dbContext.SaveChangesAsync();

            return await GetSolarPark(solarPark.Id);
        }

        public async Task<SolarParkListModel> UpdateSolarPark(int solarParkId, SolarParkModel model)
        {
            await VerifyOperator(model?.OperatorId);

            var solarPark = _dbContext
                .SolarParks
                .FirstOrDefaultAsync(x => x.Id == solarParkId);

            if (solarPark == null)
            {
                throw new NotFoundException($"Could not find solarpark with id '{solarParkId}'!");
            }

            await _mapper.Map(model, solarPark);

            await _dbContext.SaveChangesAsync();

            return await GetSolarPark(solarParkId);
        }

        public async Task DeleteSolarPark(int solarParkId)
        {
            var solarPark = await _dbContext.SolarParks.FirstOrDefaultAsync(x => x.Id == solarParkId);

            if (solarPark == null)
            {
                throw new NotFoundException($"Could not find solarpark with id '{solarParkId}'!");
            }

            _dbContext.SolarParks.Remove(solarPark);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<OperatorModel[]> GetSolarParkOperators()
        {
            var operators = await _dbContext.Operators.ToListAsync();

            return operators.Select(_mapper.Map<OperatorModel>).ToArray();
        }
    }
}
