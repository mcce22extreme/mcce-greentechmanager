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

        Task<OperatorModel[]> GetWindParkOperators();
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
            var windparks = await _dbContext
                .WindParks
                .Include(x => x.Operator)
                .ToListAsync();

            return windparks
                .Select(_mapper.Map<WindParkListModel>)
                .ToArray();
        }

        public async Task<WindParkListModel> GetWindPark(int windParkId)
        {
            var windPark = await _dbContext
                .WindParks
                .Include(x => x.Operator)
                .FirstOrDefaultAsync(x => x.Id == windParkId);

            if (windPark == null)
            {
                throw new NotFoundException($"Could not find windpark with id '{windParkId}'!");
            }

            return _mapper.Map<WindParkListModel>(windPark);
        }

        private async Task VerifyOperator(int? operatorId)
        {
            var op = await _dbContext.Operators.FirstOrDefaultAsync(x => x.Id == operatorId);

            if (op == null)
            {
                throw new NotFoundException($"Could not find operator with id '{operatorId}'!");
            }
        }

        public async Task<WindParkListModel> CreateWindPark(WindParkModel model)
        {
            await VerifyOperator(model.OperatorId);

            var windPark = _mapper.Map<WindPark>(model);

            await _dbContext.WindParks.AddAsync(windPark);

            await _dbContext.SaveChangesAsync();
            
            return await GetWindPark(windPark.Id);
        }

        public async Task<WindParkListModel> UpdateWindPark(int windParkId, WindParkModel model)
        {
            await VerifyOperator(model.OperatorId);

            var windPark = await _dbContext.WindParks.FirstOrDefaultAsync(x => x.Id == windParkId);

            if (windPark == null)
            {
                throw new NotFoundException($"Could not find windpark with id '{windParkId}'!");
            }

            _mapper.Map(model, windPark);

            await _dbContext.SaveChangesAsync();

            return await GetWindPark(windParkId);
        }

        public async Task DeleteWindPark(int windParkId)
        {
            var windPark = await _dbContext.WindParks.FirstOrDefaultAsync(x => x.Id == windParkId);

            if (windPark == null)
            {
                throw new NotFoundException($"Could not find windpark with id '{windParkId}'!");
            }

            _dbContext.WindParks.Remove(windPark);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<OperatorModel[]> GetWindParkOperators()
        {
            var operators = await _dbContext.Operators.ToListAsync();

            return operators.Select(_mapper.Map<OperatorModel>).ToArray();
        }
    }
}
