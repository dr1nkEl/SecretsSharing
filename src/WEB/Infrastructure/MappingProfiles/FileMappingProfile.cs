using AutoMapper;
using Domain;
using UseCases.Common;

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
        CreateMap<UseCases.Common.StoredFileDto, StoredFile>();
    }
}
