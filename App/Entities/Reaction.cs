namespace RekomBackend.App.Entities;

public class Reaction : EntityBase
{
   #region Columns

   public string Tag { get; set; } = null!;

   public uint Point { get; set; }
   
   #endregion

   #region ForeignKeys

   #endregion

   #region Relationships

   public IEnumerable<ReviewReaction>? ReviewReactions { get; set; }
   
   #endregion

   #region Methods

   #endregion
}