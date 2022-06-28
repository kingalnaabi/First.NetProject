using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SHRS.Models;

public class Category
{

    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
    [DisplayName("DisplayOrder")]
    [Range(1, 50, ErrorMessage = "Display Order must be between 1 and 50!!")]

    public int DisplayOrder { get; set; }

    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    public string? NameAr { get; set; } = String.Empty;
}
