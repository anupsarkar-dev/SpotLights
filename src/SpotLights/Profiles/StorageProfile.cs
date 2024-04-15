using AutoMapper;
using SpotLights.Shared;
using SpotLights.Storages;

namespace SpotLights.Profiles;

public class StorageProfile : Profile
{
  public StorageProfile()
  {
    CreateMap<Storage, StorageDto>().ReverseMap();
    //CreateMap<StorageReference, StorageDto>()
    //  .IncludeMembers(m => m.Storage)
    //  .ReverseMap();
  }
}
