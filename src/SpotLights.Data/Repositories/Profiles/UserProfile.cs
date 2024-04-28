using AutoMapper;
using SpotLights.Shared;
using SpotLights.Shared.Identity;

namespace SpotLights.Profiles;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<UserInfo, UserDto>();
    CreateMap<UserInfo, UserInfoDto>().ReverseMap();
    CreateMap<UserEditorDto, UserInfoDto>();
  }
}
