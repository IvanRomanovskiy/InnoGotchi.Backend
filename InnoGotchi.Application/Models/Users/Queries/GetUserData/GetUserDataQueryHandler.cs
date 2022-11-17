using AutoMapper;
using InnoGotchi.Application.Common.Exeptions;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Models.Users.Queries.GetUserData
{
    public class GetUserDataQueryHandler
        : IRequestHandler<GetUserDataQuery, UserDataVm>
    {
        private readonly IUsersDbContext dbContext;
        private readonly IMapper mapper;

        public GetUserDataQueryHandler(IUsersDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<UserDataVm> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);

            return mapper.Map<UserDataVm>(entity);
        }
    }
}
