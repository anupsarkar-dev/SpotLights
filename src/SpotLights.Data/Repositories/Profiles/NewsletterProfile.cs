using AutoMapper;
using SpotLights.Data.Newsletters;
using SpotLights.Shared;

namespace SpotLights.Profiles;

public class NewsletterProfile : Profile
{
  public NewsletterProfile()
  {
    CreateMap<Newsletter, NewsletterDto>();
  }
}
