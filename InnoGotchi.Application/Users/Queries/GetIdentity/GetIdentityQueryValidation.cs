using FluentValidation;

namespace InnoGotchi.Application.Users.Queries.GetIdentity
{
    public class GetIdentityQueryValidation : AbstractValidator<GetIdentityQuery>
    {
        public GetIdentityQueryValidation()
        {
            RuleFor(getIdentity => getIdentity.Email).NotEmpty().EmailAddress();
            RuleFor(getIdentity => getIdentity.Password).NotEmpty().MinimumLength(8).MaximumLength(30);
        }
    }
}
