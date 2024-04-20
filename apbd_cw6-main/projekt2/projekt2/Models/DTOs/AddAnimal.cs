using System.ComponentModel.DataAnnotations;

namespace projekt2.Models.DTOs;

public class AddAnimal
{
    [MaxLength(20)]
    [MinLength(3)]
    [Required]
    public string Name { get; set; }
    public string? Description{ get; set; } //? - daje opcje ze nullable
    public string Category { get; set; }
    public string Area { get; set; }

}