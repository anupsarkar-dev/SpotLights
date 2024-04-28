using AutoMapper;
using SpotLights.Data.Storages;
using SpotLights.Shared;

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
