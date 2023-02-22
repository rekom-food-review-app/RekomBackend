using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RekomBackend.App.Models.Entities;

public class Rekomer : EntityBase
{
   #region Columns

   [Column(TypeName = "varchar(200)"), MinLength(1)]
   public string? FullName { get; set; }

   [Column(TypeName = "varchar(200)")]
   public string AvatarUrl { get; set; } = null!;

   public DateTime? Dob { get; set; }

   [Column(TypeName = "varchar(100)")]
   public string? Description { get; set; }

   #endregion

   #region ForeignKeys

   public string AccountId { get; set; } = null!;

   #endregion

   #region Relationships

   public Account? Account { get; set; }

   public virtual IEnumerable<Follow>? Followers { get; set; }
   
   public virtual IEnumerable<Follow>? Followings { get; set; }
   
   public IEnumerable<Review>? Reviews { get; set; }
   
   #endregion

   #region Methods

   #endregion
}