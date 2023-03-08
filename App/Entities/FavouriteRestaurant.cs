using Microsoft.EntityFrameworkCore;

namespace RekomBackend.App.Entities;

[Index(nameof(RekomerId), nameof(RestaurantId), IsUnique = true)]
public class FavouriteRestaurant : EntityBase
{
   #region Columns
   
   #endregion

   #region ForeignKeys

   public string RekomerId { get; set; } = null!;

   public string RestaurantId { get; set; } = null!;

   #endregion

   #region Relationships

   public Rekomer? Rekomer { get; set; }

   public Restaurant? Restaurant { get; set; }
   
   #endregion

   #region Methods

   #endregion
}