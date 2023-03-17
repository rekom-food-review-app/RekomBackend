namespace RekomBackend.App.Entities;

public class Comment : EntityBase
{
   #region Columns
   
   public string Content { get; set; } = null!;

   #endregion

   #region ForeignKeys

   public string ReviewId { get; set; } = null!;
   
   public string RekomerId { get; set; } = null!;

   #endregion

   #region Relationships

   public Review? Review { get; set; }
   
   public Rekomer? Rekomer { get; set; }
   
   #endregion

   #region Methods

   #endregion
}