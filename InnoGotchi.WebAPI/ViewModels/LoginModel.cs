using InnoGotchi.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;
using InnoGotchi.Domain;
using AutoMapper;

namespace InnoGotchi.WebAPI.ViewModels
{
    public class LoginModel : IMapWith<User>
    {
        [Required(ErrorMessage = "Email not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, LoginModel>()
                .ForMember(user => user.Email, model => model.MapFrom(user => user.Email))
                .ForMember(user => user.Password, model => model.MapFrom(user => user.Password));
        }
    }
}
