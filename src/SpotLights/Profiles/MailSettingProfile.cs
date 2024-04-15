using AutoMapper;
using SpotLights.Newsletters;
using SpotLights.Shared;

namespace SpotLights.Profiles;

public class MailSettingProfile : Profile
{
  public MailSettingProfile()
  {
    CreateMap<MailSettingData, MailSettingDto>().ReverseMap();
  }
}
