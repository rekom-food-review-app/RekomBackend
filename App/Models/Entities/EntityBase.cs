namespace RekomBackend.App.Models.Entities;

public abstract class EntityBase
{
   #region Columns

   public string Id { get; set; } = null!;
   
   public DateTime CreatedAt { get; set; }
   
   public DateTime UpdatedAt { get; set; }

   #endregion

   #region Columns

   #endregion

   #region ForeignKeys

   #endregion

   #region Relationships

   #endregion

   #region Methods

   #endregion
}