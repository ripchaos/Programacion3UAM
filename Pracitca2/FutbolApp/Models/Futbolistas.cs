using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Futbolista
{
    [Key]
    public int FutbolistaId { get; set; }

    [Required]
    [StringLength(100)]
    public required string NombreCompleto { get; set; }

    public int EdadActual { get; set; }

    [Required]
    public required string Rol { get; set; }

    public int CamisetaNumero { get; set; }

    [ForeignKey("ClubId")]
    public int ClubId { get; set; }

    public Club Club { get; set; } = null!;
}