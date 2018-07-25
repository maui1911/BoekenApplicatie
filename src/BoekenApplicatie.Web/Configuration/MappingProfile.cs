using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
  }
}