using AutoMapper;
using BoekenApplicatie.Domain.Models;
using BoekenApplicatie.Web.ViewModels;

namespace BoekenApplicatie.Web.Configuration
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<ApplicationUser, UserEditViewModel>();
      CreateMap<UserEditViewModel, ApplicationUser>();
      CreateMap<BooksViewModel, Book>();
      CreateMap<BooksViewModel, Title>();
      CreateMap<Book, BooksViewModel>();
      CreateMap<Title, BooksViewModel>();
    }
  }
}