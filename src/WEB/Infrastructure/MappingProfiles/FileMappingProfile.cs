using AutoMapper;
using Domain;
using Infrastructure.Common;

namespace WEB.Infrastructure.MappingProfiles;

/// <summary>
/// File mapping profile.
/// </summary>
public class FileMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public FileMappingProfile()
    {
        CreateMap<UseCases.Common.StoredFileDto, StoredFile>().ReverseMap();
        CreateMap<StoredFileDto, StoredFile>();
        CreateMap<UseCases.Common.StoredFileDto, WEB.Infrastructure.Models.StoredFileDto>();
    }
}
