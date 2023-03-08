using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace RekomBackend.App.Entities;

[Index(nameof(Location))]
public class Restaurant : EntityBase
{
   #region Columns

   [Column(TypeName = "varchar(200)")]
   public string Name { get; set; } = null!;

   [Column(TypeName = "varchar(200)")]
   public string CoverImageUrl { get; set; } = null!;

   [Column(TypeName = "varchar(500)")]
   public string Address { get; set; } = null!;

   [Column(TypeName="geometry(point)")]
   public Point Location { get; set; } = null!;

   [Column(TypeName = "varchar(500)")] 
   public string Description { get; set; } = null!;
   
   #endregion

   #region ForeignKeys

   public string AccountId { get; set; } = null!;
   
   #endregion

   #region Relationships

   public Account? Account { get; set; }

   public IEnumerable<Food>? Menu { get; set; }

   public IEnumerable<Review>? Reviews { get; set; }

   public IEnumerable<FavouriteRestaurant>? FavouriteRestaurants { get; set; }

   #endregion

   #region Methods

   #endregion
}