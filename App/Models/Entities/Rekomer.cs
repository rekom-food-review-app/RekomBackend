using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RekomBackend.App.Models.Entities;

public class Rekomer : EntityBase
{
   #region Columns

   [Column(TypeName = "varchar(200)"), MinLength(1)]
   public string FullName { get; set; } = null!;

   [Column(TypeName = "varchar(200)")]
   public string AvatarUrl { get; set; } = null!;

   #endregion

   #region ForeignKeys

   public string AccountId { get; set; } = null!;

   #endregion

   #region Relationships

   public Account? Account { get; set; }

   public virtual ICollection<Follow>? Followers { get; set; }
   
   public virtual ICollection<Follow>? Followings { get; set; }
   
   #endregion

   #region Methods

   #endregion
}