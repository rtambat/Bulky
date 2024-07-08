using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Bulky.Razor.Model
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Enter display order between 1 to 100")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
