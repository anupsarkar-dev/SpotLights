using AutoMapper;
using SpotLights.Blogs;
using SpotLights.Shared;

namespace SpotLights.Profiles;

public class BlogProfile : Profile
{
  public BlogProfile()
  {
    CreateMap<BlogData, MainDto>();
    CreateMap<BlogData, BlogEitorDto>().ReverseMap();
  }
}
