using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Common.Enums;

namespace RekomBackend.App.Entities;

[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class Account : EntityBase
{
   #region Columns
   
   [Column(TypeName = "varchar(100)"), MinLength(1), MaxLength(100)]
   public string Username { get; init; } = null!;
   
   [Column(TypeName = "varchar(200)"), MinLength(1), MaxLength(200)]
   public string Email { get; init; } = null!;

   [Column(TypeName = "varchar(500)")]
   public string PasswordHash { get; set; } = null!;

   [Column(TypeName = "enum('Rekomer', 'Restaurant', 'Admin')")]
   public Role Role { get; init; }

   public bool IsConfirmed { get; set; } = false;

   #endregion

   #region ForeignKeys

   #endregion

   #region Relationships

   public Otp? Otp { get; set; }

   public Rekomer? Rekomer { get; set; }
   
   public Restaurant? Restaurant { get; set; }
   
   #endregion

   #region Methods

   #endregion
}