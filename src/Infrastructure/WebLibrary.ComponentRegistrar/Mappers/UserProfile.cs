using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Contracts.Users;
using WebLibrary.Domain.Users.Entity;

namespace WebLibrary.ComponentRegistrar.Mappers
{
    public class UserProfile : Profile
    {
        /// <summary>
        /// Профиль работы с пользователями.
        /// </summary>
        public UserProfile() 
        {
            CreateMap<RegistratinRequest, User>()
                .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>();

        }
    }
}
