using Microsoft.EntityFrameworkCore;

namespace RekomBackend.App.Entities;

[Index(nameof(FollowerId), nameof(FollowingId), IsUnique = true)]
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