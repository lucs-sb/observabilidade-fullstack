using Donor.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donor.Domain.Entities;

[Table("tb_donor")]
[Index(nameof(Email), IsUnique = true)]
public sealed class Donor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("fullname")]
    public string? FullName { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("phonenumber")]
    public string? PhoneNumber { get; set; }

    [Column("dateofbirth")]
    public DateTime DateOfBirth { get; set; }

    [Column("gender")]
    public Gender Gender { get; set; }

    [Column("bloodtype")]
    public BloodType BloodType { get; set; }

    [Column("rhfactor")]
    public RhFactor RhFactor { get; set; }

    [Column("weighthg")]
    public decimal? WeightKg { get; set; }

    [Column("street")]
    public string? Street { get; set; }

    [Column("city")]
    public string? City { get; set; }

    [Column("state")]
    public string? State { get; set; }

    [Column("zipcode")]
    public string? ZipCode { get; set; }
}
