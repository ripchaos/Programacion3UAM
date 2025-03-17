using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Club
{
    [Key]
    public int ClubId { get; set; }

    [Required]
    [StringLength(100)]
    public required string NombreClub { get; set; }

    public required string Ubicacion { get; set; }

    public int AñoFundado { get; set; }

    public required string Sede { get; set; }

    public ICollection<Futbolista> Futbolistas { get; set; } = new List<Futbolista>();
}