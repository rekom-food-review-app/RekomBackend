using System.ComponentModel.DataAnnotations.Schema;

namespace RekomBackend.App.Entities;

public class Food : EntityBase
{
   #region Columns
   
   [Column(TypeName = "varchar(200)")]
   public string Name { get; set; } = null!;
   
   public float Price { get; set; }
   
   [Column(TypeName = "varchar(200)")]
   public string ImageUrl { get; set; } = null!;
   
   #endregion
   
   #region ForeignKeys

   public string RestaurantId { get; set; } = null!;

   #endregion

   #region Relationships

   public Restaurant? Restaurant { get; set; }
   
   public IEnumerable<FoodImage>? Images { get; set; }
   
   #endregion

   #region Methods

   #endregion
}