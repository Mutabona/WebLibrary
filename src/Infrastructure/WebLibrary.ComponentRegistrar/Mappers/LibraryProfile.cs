using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Contracts.Files;
using WebLibrary.Contracts.Libraries;

namespace WebLibrary.ComponentRegistrar.Mappers
{
    /// <summary>
    /// Профиль работы с библиотеками.
    /// </summary>
    internal class LibraryProfile : Profile
    {
        public LibraryProfile() 
        {
            CreateMap<LibraryDto, Domain.Library.Entity.Library>()
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

            CreateMap<Domain.Library.Entity.Library, LibraryDto>();
        }
    }
}
