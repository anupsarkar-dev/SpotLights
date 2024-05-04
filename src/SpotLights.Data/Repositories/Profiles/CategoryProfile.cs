using AutoMapper;
using SpotLights.Data.Model.Posts;
using SpotLights.Shared;

namespace SpotLights.Profiles;

public class CategoryProfile : Profile
{
  public CategoryProfile()
  {
    CreateMap<Category, CategoryDto>().ReverseMap();
    CreateMap<PostCategory, CategoryDto>()
      .IncludeMembers(m => m.Category)
      .ReverseMap();
  }
}
