using AutoMapper;
using SpotLights.Identity;
using SpotLights.Shared;

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
