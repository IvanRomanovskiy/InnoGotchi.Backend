using AutoMapper;
using InnoGotchi.Application.Common.Exeptions;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Application.Users.Commands.FindUser;
using InnoGotchi.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.Application.Users.Queries.GetUserData
{
    public class GetUserDataQueryHandler
        : IRequestHandler<GetUserTokenQuery, UserDataVm>
    {
        private readonly IUsersDbContext dbContext;
        private readonly IMapper mapper;

        public GetUserDataQueryHandler(IUsersDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<UserDataVm> Handle(GetUserTokenQuery request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);

            if(entity == null || entity.Email != request.Email)
            {
                throw new NotFoundExeption(nameof(User),request.Email);
            }
            return mapper.Map<UserDataVm>(entity);
        }
    }
}
