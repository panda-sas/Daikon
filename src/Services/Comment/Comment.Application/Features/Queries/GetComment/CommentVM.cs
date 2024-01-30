using CQRS.Core.Domain;

namespace Comment.Application.Features.Queries.GetComment
{
    public class CommentVM : DocMetadata
    {

        public Guid ResourceId { get; set; }
        public string? Reference { get; set; }
        public string? Section { get; set; }
        public DVariable<string> Topic { get; set; }
        public DVariable<string>? Description { get; set; }
        public string? PostedBy { get; set; }
        
    }
}