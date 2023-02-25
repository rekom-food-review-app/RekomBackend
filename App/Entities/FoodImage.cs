using System.ComponentModel.DataAnnotations.Schema;

namespace RekomBackend.App.Entities;

public class FoodImage : EntityBase
{
   #region Columns

   [Column(TypeName = "varchar(200)")]
   public string ImageUrl { get; set; } = null!;

   #endregion

   #region ForeignKeys

   public string FoodId { get; set; } = null!;

   #endregion

   #region Relationships

   public Food? Food { get; set; }
   
   #endregion

   #region Methods

   #endregion
}