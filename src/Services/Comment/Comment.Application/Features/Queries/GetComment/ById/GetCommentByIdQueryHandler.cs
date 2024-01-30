using AutoMapper;
using CQRS.Core.Exceptions;
using Comment.Application.Contracts.Persistence;
using MediatR;

namespace Comment.Application.Features.Queries.GetComment.ById
{
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentVM>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentByIdQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CommentVM> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.ReadCommentById(request.Id);

            if (comment == null)
            {
                throw new ResourceNotFoundException(nameof(Comment), request.Id);
            }

            var commentVm = _mapper.Map<CommentVM>(comment, opts => opts.Items["WithMeta"] = request.WithMeta);

            return commentVm;
        }

    
        
    }
}