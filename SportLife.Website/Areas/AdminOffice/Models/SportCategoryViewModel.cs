using System.Collections.Generic;

namespace SportLife.Website.Areas.AdminOffice.Models {
    public class SportCategoryViewModel {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ImageId { get; set; }
        public IEnumerable<SportKindViewModel> SportKinds { get; set; }
    }

    public class CategoryDropDownViewModel {
        public int SportCategoryId { get; set; }
        public string SportCategoryName { get; set; } 
    }
}