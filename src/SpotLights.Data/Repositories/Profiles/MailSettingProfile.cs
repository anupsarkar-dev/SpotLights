using AutoMapper;
using SpotLights.Data.Model.Newsletters;
using SpotLights.Shared;

namespace SpotLights.Profiles;

public class MailSettingProfile : Profile
{
  public MailSettingProfile()
  {
    CreateMap<MailSettingData, MailSettingDto>().ReverseMap();
  }
}
