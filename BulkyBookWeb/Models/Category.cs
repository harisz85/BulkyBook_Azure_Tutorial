using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } 
        [DisplayName("Display Order Number")]
        [Range(1,100,ErrorMessage ="You have exceeded the minimum/maximum range available!")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
