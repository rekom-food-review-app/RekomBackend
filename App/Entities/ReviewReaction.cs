namespace RekomBackend.App.Entities;

public class ReviewReaction : EntityBase
{
   #region Columns

   #endregion

   #region ForeignKeys

   public string ReviewId { get; set; } = null!;

   public string RekomerId { get; set; } = null!;
   
   public string ReactionId { get; set; } = null!;

   #endregion

   #region Relationships

   public Review? Review { get; set; }
   
   public Rekomer? Rekomer { get; set; }
   
   public Reaction? Reaction { get; set; }
   
   #endregion

   #region Methods

   #endregion
}