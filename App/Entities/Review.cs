using System.ComponentModel.DataAnnotations.Schema;

namespace RekomBackend.App.Entities;

public class Review : EntityBase
{
   #region Columns

   [Column(TypeName = "varchar(500)")]
   public string Content { get; set; } = null!;
   
   #endregion

   #region ForeignKeys

   public string RestaurantId { get; set; } = null!;

   public string RekomerId { get; set; } = null!;

   public string RatingId { get; set; } = null!;
   
   #endregion

   #region Relationships

   public Restaurant? Restaurant { get; set; }
   
   public Rekomer? Rekomer { get; set; }

   public Rating? Rating { get; set; }

   public IEnumerable<ReviewMedia>? Medias { get; set; }

   public IEnumerable<ReviewReaction>? ReviewReactions { get; set; }
   
   #endregion

   #region Methods

   #endregion
}