using System.Collections.Generic;
using System.Web.Mvc;
using System.Windows.Documents;

namespace SportLife.Website.Areas.AdminOffice.Models {
    public class SportKindViewModel {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ImageId { get; set; }
        public int SportCategory { get; set; }
    }

    public class CreateSportKindViewModel { 
        public string Name { get; set; }
        public SelectList Categories { get; set; }
        public int SelectedCategoryId { get; set; }

        public CreateSportKindViewModel()
        {
            
        }

        public CreateSportKindViewModel(IEnumerable<CategoryDropDownViewModel> categories)
        {
            Categories = new SelectList(categories, "SportCategoryId", "SportCategoryName");
        }
    }

    public class SportDropDownViewModel
    {
        public int SportId { get; set; }
        public string SportName { get; set; }
    }
}