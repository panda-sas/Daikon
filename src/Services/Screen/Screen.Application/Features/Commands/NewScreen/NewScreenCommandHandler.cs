
using AutoMapper;
using CQRS.Core.Handlers;
using Daikon.Events.Screens;
using MediatR;
using Microsoft.Extensions.Logging;
using Screen.Application.Contracts.Persistence;
using Screen.Domain.Aggregates;

namespace Screen.Application.Features.Commands.NewScreen
{
    public class NewScreenCommandHandler : IRequestHandler<NewScreenCommand, Unit>
    {

        private readonly IMapper _mapper;
        private readonly ILogger<NewScreenCommandHandler> _logger;
        private readonly IScreenRepository _screenRepository;

        private readonly IEventSourcingHandler<ScreenAggregate> _screenEventSourcingHandler;


        public NewScreenCommandHandler(ILogger<NewScreenCommandHandler> logger, 
            IEventSourcingHandler<ScreenAggregate> screenEventSourcingHandler,
            IScreenRepository screenRepository,
            IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _screenRepository = screenRepository ?? throw new ArgumentNullException(nameof(screenRepository));
            _screenEventSourcingHandler = screenEventSourcingHandler ?? throw new ArgumentNullException(nameof(screenEventSourcingHandler));
          
        }


        public async Task<Unit> Handle(NewScreenCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling NewScreenCommand: {request}");
           
            // check if name exists
            var existingScreen = await _screenRepository.ReadScreenByName(request.Name);
            if (existingScreen != null)
            {
                _logger.LogError("Screen name already exists: {Name}", request.Name);
                throw new InvalidOperationException("Screen name already exists");
            }
            

            var newScreenCreatedEvent = _mapper.Map<ScreenCreatedEvent>(request);

            var aggregate = new ScreenAggregate(newScreenCreatedEvent);

            await _screenEventSourcingHandler.SaveAsync(aggregate);

            return Unit.Value;

        }
    }

}
