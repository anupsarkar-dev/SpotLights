using AutoMapper;
using SpotLights.Data.Newsletters;
using SpotLights.Shared;

namespace SpotLights.Profiles;

public class SubscriberProfile : Profile
{
  public SubscriberProfile()
  {
    CreateMap<Subscriber, SubscriberDto>();
    CreateMap<SubscriberApplyDto, Subscriber>();
  }
}
