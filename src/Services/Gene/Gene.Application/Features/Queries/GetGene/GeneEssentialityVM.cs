
namespace Gene.Application.Features.Queries.GetGene
{
    public class GeneEssentialityVM
    {
        public Guid EssentialityId { get; set; }
        public object Classification { get; set; }
        public object Condition { get; set; }
        public object Method { get; set; }
        public object Reference { get; set; }
        public object Note { get; set; }

    }
}