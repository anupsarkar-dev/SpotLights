using AutoMapper;
using SpotLights.Data.Model.Blogs;
using SpotLights.Shared;

namespace SpotLights.Profiles;

public class BlogProfile : Profile
{
  public BlogProfile()
  {
    CreateMap<BlogData, MainDto>().ReverseMap();
    CreateMap<BlogData, BlogEitorDto>().ReverseMap();
  }
}
