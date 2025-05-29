using Microsoft.AspNetCore.Mvc.Rendering;

namespace WashWise.Web.Models
{
    public class BuildingSelectionViewModel
    {
        public Guid SelectedBuildingId { get; set; }

        public IEnumerable<SelectListItem> Buildings { get; set; } = new List<SelectListItem>();
    }
}