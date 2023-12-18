
using CQRS.Core.Command;
using MediatR;

namespace Screen.Application.Features.Commands.DeleteScreen
{
    public class DeleteScreenCommand : BaseCommand, IRequest<Unit>
    {
        public Guid StrainId { get; set; }
        public required string Name { get; set; }


    }
}