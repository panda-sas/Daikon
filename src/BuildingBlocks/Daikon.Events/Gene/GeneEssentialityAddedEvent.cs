
using CQRS.Core.Domain;
using CQRS.Core.Event;

namespace Daikon.Events.Gene
{
    public class GeneEssentialityAddedEvent : BaseEvent
    {
        public GeneEssentialityAddedEvent() : base(nameof(GeneEssentialityAddedEvent))
        {

        }


        public Guid EssentialityId { get; set; }
        public required DVariable<string> Classification { get; set; }
        public DVariable<string>? Condition { get; set; }
        public DVariable<string>? Method { get; set; }
        public DVariable<string>? Reference { get; set; }
        public DVariable<string>? Note { get; set; }
        
        public DateTime DateCreated { get; set; }

    }
}