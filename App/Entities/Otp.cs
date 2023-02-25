using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RekomBackend.App.Entities;

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
   
   #region Relationships
   
   public Account? Account { get; set; }

   #endregion

   #region Methods
   
   #endregion
}