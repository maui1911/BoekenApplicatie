using BoekenApplicatie.Web.Options;

namespace BoekenApplicatie.Web.ViewModels
{
  public abstract class BaseListViewModel
  {
    protected BaseListViewModel()
    {
      SortFilterPageData = new SortFilterPageOptions();
    }

    public SortFilterPageOptions SortFilterPageData { get; set; }
  }
}