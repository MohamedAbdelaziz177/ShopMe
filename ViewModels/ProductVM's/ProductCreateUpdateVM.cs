using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce2.ViewModels.ProductVM_s
{
    public class ProductCreateUpdateVM
    {

        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } 

        [Required, MaxLength(100)]
        public string Brand { get; set; } 

        [Required, MaxLength(100)]
        public string Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        public IFormFile? ImageFile { get; set; }

        public string? ImageFileName { get; set; } 

        public DateTime? CreatedAt { get; set; }

        public IEnumerable<SelectListItem> categories { get; set; } = Enumerable.Empty<SelectListItem>();

        
    }
}
