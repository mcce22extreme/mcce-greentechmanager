using AutoMapper;
using GreenTechManager.SolarParks.Entities;
using GreenTechManager.SolarParks.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
                Log.Information("Creating new solarpark operator.");

                op = _mapper.Map<Operator>(model);
                _dbContext.Operators.Add(op);
            }
            else
            {
                Log.Information("Updating existing solarpark operator.");

                _mapper.Map(model, op);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOperator(int windParkId)
        {
            Log.Information("Deleting existing solarpark operator.");

            var windPark = await _dbContext.Operators.FirstOrDefaultAsync(x => x.Id == windParkId);

            _dbContext.Operators.Remove(windPark);

            await _dbContext.SaveChangesAsync();
        }
    }
}
