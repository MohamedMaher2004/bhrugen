using System.ComponentModel.DataAnnotations;

namespace bhrugen_webapi.Models.Dtos;

public class VillaUpdateNameDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    
}
    