using System.Collections.Generic;
using BoekenApplicatie.Domain.Models;

namespace BoekenApplicatie.Web.ViewModels
{
    public class PublisherListViewModel : BaseListViewModel
    {
      public PublisherListViewModel()
      {
        Publishers = new List<Publisher>();
      }
      public List<Publisher> Publishers { get; set; }
  }
}
