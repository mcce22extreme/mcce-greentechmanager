using AutoMapper;
using GreenTechManager.Core.Enums;
using GreenTechManager.Core.Exceptions;
using GreenTechManager.Core.Messages;
using GreenTechManager.Core.Services;
using GreenTechManager.WindParks.Entities;
using GreenTechManager.WindParks.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenTechManager.Operators.Managers
{
    public interface IOperatorManager
    {
        Task<OperatorModel[]> GetOperators();

        Task<OperatorModel> GetOperator(int operatorId);

        Task<OperatorModel> CreateOperator(SaveOperatorModel model);

        Task<OperatorModel> UpdateOperator(int operatorId, SaveOperatorModel model);

        Task DeleteOperator(int operatorId);
    }

    public class OperatorManager : IOperatorManager
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMessageBusService _messageBusService;

        public OperatorManager(AppDbContext dbContext, IMapper mapper, IMessageBusService messageBusService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _messageBusService = messageBusService;
        }

        public async Task<OperatorModel[]> GetOperators()
        {
            var operators = await _dbContext.Operators.ToListAsync();

            return operators.Select(_mapper.Map<OperatorModel>).ToArray();
        }

        public async Task<OperatorModel> GetOperator(int operatorId)
        {
            var op = await _dbContext.Operators.FirstOrDefaultAsync(x => x.Id == operatorId);

            if (op == null)
            {
                throw new NotFoundException($"Could not find operator with id '{operatorId}'!");
            }

            return _mapper.Map<OperatorModel>(op);
        }

        public async Task<OperatorModel> CreateOperator(SaveOperatorModel model)
        {
            var op = _mapper.Map<Operator>(model);

            await _dbContext.Operators.AddAsync(op);

            await _dbContext.SaveChangesAsync();

            _messageBusService.PublishMessage(_mapper.Map(op, new EntityMessage(EventType.EntityCreated)));

            return await GetOperator(op.Id);
        }

        public async Task<OperatorModel> UpdateOperator(int operatorId, SaveOperatorModel model)
        {
            var op = await _dbContext.Operators.FirstOrDefaultAsync(x => x.Id == operatorId);

            if (op == null)
            {
                throw new NotFoundException($"Could not find operator with id '{operatorId}'!");
            }

            _mapper.Map(model, op);

            await _dbContext.SaveChangesAsync();

            _messageBusService.PublishMessage(_mapper.Map(op, new EntityMessage(EventType.EntityModified)));

            return await GetOperator(operatorId);
        }

        public async Task DeleteOperator(int operatorId)
        {
            var op = await _dbContext.Operators.FirstOrDefaultAsync(x => x.Id == operatorId);

            if (op == null)
            {
                throw new NotFoundException($"Could not find operator with id '{operatorId}'!");
            }

            _dbContext.Operators.Remove(op);

            await _dbContext.SaveChangesAsync();

            _messageBusService.PublishMessage(_mapper.Map(op, new EntityMessage(EventType.EntityDeleted)));
        }
    }
}
