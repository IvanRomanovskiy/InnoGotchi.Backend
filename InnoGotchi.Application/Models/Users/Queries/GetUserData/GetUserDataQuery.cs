using MediatR;

namespace InnoGotchi.Application.Models.Users.Queries.GetUserData
{
    public class GetUserDataQuery: IRequest<UserDataVm>
    {
        public Guid Id { get; set; }
    }
}
