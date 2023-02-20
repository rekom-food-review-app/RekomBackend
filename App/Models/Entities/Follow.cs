namespace RekomBackend.App.Models.Entities;

public class Follow : EntityBase
{
   #region Columns

   #endregion

   #region ForeignKeys

   public string FollowerId { get; set; } = null!;

   public string FollowingId { get; set; } = null!;

   #endregion

   #region Relationships

   public virtual Rekomer? Follower { get; set; }

   public virtual Rekomer? Following { get; set; }
   
   #endregion

   #region Methods

   #endregion
}