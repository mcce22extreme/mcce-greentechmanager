using AutoMapper;
using GreenTechManager.SolarParks.Entities;
using GreenTechManager.SolarParks.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.SolarParks.Managers
{
    public interface IOperatorManager
    {
        Task CreateOrUpdateOperator(OperatorModel model);

        Task DeleteOperator(int operatorId);
    }

    public class OperatorManager : IOperatorManager
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public OperatorManager(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateOrUpdateOperator(OperatorModel model)
        {
            var op = await _dbContext.Operators.FirstOrDefaultAsync(x => x.Id == model.ExternalId);

            if (op == null)
            {
                op = _mapper.Map<Operator>(model);
                _dbContext.Operators.Add(op);
            }
            else
            {
                _mapper.Map(model, op);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOperator(int windParkId)
        {
            var windPark = await _dbContext.Operators.FirstOrDefaultAsync(x => x.Id == windParkId);

            _dbContext.Operators.Remove(windPark);

            await _dbContext.SaveChangesAsync();
        }
    }
}
