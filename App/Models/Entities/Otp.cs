using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RekomBackend.App.Models.Entities;

public class Otp : EntityBase
{
   #region Columns

   [Column(TypeName = "varchar(4)"), MinLength(4)]
   public string Code { get; init; } = null!;

   public DateTime Expiration { get; init; }
   
   #endregion

   #region ForeignKeys

   public string AccountId { get; init; } = null!;

   #endregion

   public Account? Account { get; set; }
   
   #region Relationships

   #endregion

   #region Methods

   public bool IsStillValid()
   {
      return Expiration.CompareTo(DateTime.Now) >= 0;
   }

   #endregion
}