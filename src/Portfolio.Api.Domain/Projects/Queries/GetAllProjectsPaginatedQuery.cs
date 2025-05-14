using MediatR;
using Portfolio.Api.Domain.Extensions;
using Portfolio.Api.Domain.Projects.DTOs;
using Portfolio.Api.Domain.Repositories;

namespace Portfolio.Api.Domain.Projects.Queries
{
    public class GetAllProjectsPaginatedQuery : IRequest<PagedList<ProjectDto>>
    {
        public GetAllProjectsPaginatedQuery(PageFilterModel pageFilterModel)
        {
            PageFilterModel = pageFilterModel;
        }
        public PageFilterModel PageFilterModel { get;}
    }

    public class
        GetAllProjectsPaginatedQueryHandler : IRequestHandler<GetAllProjectsPaginatedQuery, PagedList<ProjectDto>>
    {
        private readonly IProjectReadOnlyRepository _projectReadOnlyRepository;

        public GetAllProjectsPaginatedQueryHandler(IProjectReadOnlyRepository projectReadOnlyRepository)
        {
            _projectReadOnlyRepository = projectReadOnlyRepository;
        }

        public async Task<PagedList<ProjectDto>> Handle(GetAllProjectsPaginatedQuery request,
            CancellationToken cancellationToken)
        {
            return await _projectReadOnlyRepository.GetAllProjectsPaginated(request.PageFilterModel, cancellationToken);
        }
    }
}