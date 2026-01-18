using Donation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donation.Domain.Entities;

[Table("tb_donation")]
//[Index(nameof(BagNumber), IsUnique = true)]
public sealed class Donation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("donor_id")]
    public Guid DonorId { get; set; }

    [Column("date_of_donation")]
    public DateTime DateOfDonation { get; set; }

    [Column("donation_type")]
    public DonationType DonationType { get; set; }

    [Column("volume_ml")]
    public int VolumeMl { get; set; }

    [Column("bag_number")]
    public string? BagNumber { get; set; }
}