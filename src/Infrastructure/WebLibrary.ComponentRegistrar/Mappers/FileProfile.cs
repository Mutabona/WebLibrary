using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebLibrary.Contracts.Files;

namespace WebLibrary.ComponentRegistrar.Mappers
{
    /// <summary>
    /// Профиль работы с файлами.
    /// </summary>
    public class FileProfile : Profile
    {
        public FileProfile() 
        {
            CreateMap<FileDto, Domain.Files.Entity.File>()
                .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
                .ForMember(s => s.Length, map => map.MapFrom(s => s.Content.Length))
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

            CreateMap<Domain.Files.Entity.File, FileInfoDto>();

            CreateMap<Domain.Files.Entity.File, FileDto>();

        }
    }
}
