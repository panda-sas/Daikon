using AutoMapper;
using CQRS.Core.Handlers;
using Daikon.Events.HitAssessment;
using HitAssessment.Application.Contracts.Persistence;
using HitAssessment.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;


namespace HitAssessment.Application.Features.Commands.NewHitAssessment
{
    public class NewHitAssessmentCommandHandler : IRequestHandler<NewHitAssessmentCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<NewHitAssessmentCommandHandler> _logger;
        private readonly IHitAssessmentRepository _haRepository;
        private readonly IEventSourcingHandler<HaAggregate> _haEventSourcingHandler;

        public NewHitAssessmentCommandHandler(ILogger<NewHitAssessmentCommandHandler> logger,
            IEventSourcingHandler<HaAggregate> haEventSourcingHandler,
            IHitAssessmentRepository haRepository,
            IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _haRepository = haRepository ?? throw new ArgumentNullException(nameof(haRepository));
            _haEventSourcingHandler = haEventSourcingHandler ?? throw new ArgumentNullException(nameof(haEventSourcingHandler));
        }

        public async Task<Unit> Handle(NewHitAssessmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling NewHitAssessmentCommand: {request}");

                // check if name exists
                var existingHitAssessment = await _haRepository.ReadHaByName(request.Name);
                if (existingHitAssessment != null)
                {
                    _logger.LogWarning("HitAssessment name already exists: {Name}", request.Name);
                    throw new InvalidOperationException("HitAssessment name already exists");
                }

                var newHitAssessmentCreatedEvent = _mapper.Map<HaCreatedEvent>(request);

                var aggregate = new HaAggregate(newHitAssessmentCreatedEvent);

                await _haEventSourcingHandler.SaveAsync(aggregate);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling NewHitAssessmentCommand");
                throw;
            }
        }
    }
}
