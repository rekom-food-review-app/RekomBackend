namespace RekomBackend.App.Entities;

public abstract class EntityBase
{
   #region Columns

   public string Id { get; set; } = Guid.NewGuid().ToString();

   public DateTime CreatedAt { get; set; } = DateTime.Now;

   public DateTime UpdatedAt { get; set; } = DateTime.Now;

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