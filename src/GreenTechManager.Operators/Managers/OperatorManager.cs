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
        Task<OperatorListModel[]> GetOperators();

        Task<OperatorListModel> GetOperator(int operatorId);

        Task<OperatorListModel> CreateOperator(OperatorModel model);

        Task<OperatorListModel> UpdateOperator(int operatorId, OperatorModel model);

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

        public async Task<OperatorListModel[]> GetOperators()
        {
            var operators = await _dbContext.Operators.ToListAsync();

            return operators.Select(_mapper.Map<OperatorListModel>).ToArray();
        }

        public async Task<OperatorListModel> GetOperator(int operatorId)
        {
            var op = await _dbContext.Operators.FirstOrDefaultAsync(x => x.Id == operatorId);

            if (op == null)
            {
                throw new NotFoundException($"Could not find operator with id '{operatorId}'!");
            }

            return _mapper.Map<OperatorListModel>(op);
        }

        public async Task<OperatorListModel> CreateOperator(OperatorModel model)
        {
            var op = _mapper.Map<Operator>(model);

            await _dbContext.Operators.AddAsync(op);

            await _dbContext.SaveChangesAsync();

            _messageBusService.PublishMessage(_mapper.Map(op, new EntityMessage(EventType.EntityCreated)));

            return await GetOperator(op.Id);
        }

        public async Task<OperatorListModel> UpdateOperator(int operatorId, OperatorModel model)
        {
            var op = _dbContext.Operators.FirstOrDefaultAsync(x => x.Id == operatorId);

            await _mapper.Map(model, op);

            await _dbContext.SaveChangesAsync();

            _messageBusService.PublishMessage(_mapper.Map(op, new EntityMessage(EventType.EntityModified)));

            return await GetOperator(operatorId);
        }

        public async Task DeleteOperator(int operatorId)
        {
            var op = await _dbContext.Operators.FirstOrDefaultAsync(x => x.Id == operatorId);

            _dbContext.Operators.Remove(op);

            await _dbContext.SaveChangesAsync();

            _messageBusService.PublishMessage(_mapper.Map(op, new EntityMessage(EventType.EntityDeleted)));
        }
    }
}
