
using CQRS.Core.Domain;
using CQRS.Core.Event;

namespace Daikon.Events.Gene
{
    public class GeneProteinProductionAddedEvent : BaseEvent
    {
        public GeneProteinProductionAddedEvent() : base(nameof(GeneProteinProductionAddedEvent))
        {

        }

        public Guid GeneId { get; set; }
        public Guid ProteinProductionId { get; set; }

        public required DVariable<string> Production { get; set; }
        public DVariable<string>? Method { get; set; }
        public DVariable<string>? Purity { get; set; }
        public DVariable<DateTime>? DateProduced { get; set; }
        public DVariable<string>? PMID { get; set; }
        public DVariable<string>? Notes { get; set; }
        public DVariable<string>? URL { get; set; }
    }
}