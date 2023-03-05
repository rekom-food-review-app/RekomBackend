using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RekomBackend.App.Entities;

public class Rekomer : EntityBase
{
   #region Columns

   [Column(TypeName = "varchar(200)"), MinLength(1)]
   public string? FullName { get; set; }

   [Column(TypeName = "varchar(200)")]
   public string AvatarUrl { get; set; } = "https://images.unsplash.com/photo-1677213243600-08ecdd8547b8?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=715&q=80";

   public DateTime? Dob { get; set; }

   [Column(TypeName = "varchar(100)")]
   public string? Description { get; set; }

   #endregion

   #region ForeignKeys

   public string AccountId { get; set; } = null!;

   #endregion

   #region Relationships

   public Account? Account { get; set; }

   public virtual IEnumerable<Follow>? Followers { get; set; }
   
   public virtual IEnumerable<Follow>? Followings { get; set; }
   
   public IEnumerable<Review>? Reviews { get; set; }
   
   public IEnumerable<ReviewReaction>? ReviewReactions { get; set; }

   public IEnumerable<Comment>? Comments { get; set; }

   public IEnumerable<FavouriteRestaurant>? FavouriteRestaurants { get; set; }
   
   #endregion

   #region Methods

   #endregion
}