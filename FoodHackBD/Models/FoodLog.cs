using FoodHackBD.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class FoodLog
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ItemName { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = string.Empty;

    [Required]
    public int Quantity { get; set; }

    [Required]
    public DateTime LogDate { get; set; } = DateTime.UtcNow;

    [ForeignKey("ApplicationUser")]
    public Guid UserId { get; set; }
    public ApplicationUser? User { get; set; }
    // New field to associate uploaded file
    [ForeignKey("Upload")]
    public int? UploadId { get; set; }
    public Upload? Upload { get; set; }
}
