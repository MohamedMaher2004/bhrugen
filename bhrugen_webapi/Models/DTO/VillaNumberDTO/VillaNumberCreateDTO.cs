using System.ComponentModel.DataAnnotations;

namespace bhrugen_webapi.Models.Dtos;

public class VillaNumberCreateDTO
{
    [Required]
    public int VillaNo { get; set; }
    public string  SpecialDetails { get; set; }
    [Required]
    public int VillaId { get; set; }
}
    