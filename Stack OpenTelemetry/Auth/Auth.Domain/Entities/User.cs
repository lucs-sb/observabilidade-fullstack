using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Entities;

[Table("tb_user")]
[Index(nameof(Identifier), IsUnique = true)]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("identifier")]
    public string? Identifier { get; set; }

    [Column("password")]
    public string? Password { get; set; }
}