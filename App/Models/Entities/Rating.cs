using Microsoft.EntityFrameworkCore;

namespace RekomBackend.App.Models.Entities;

[Index(nameof(Tag))]
public class Rating : EntityBase
{
   #region Columns

   public string Tag { get; set; } = null!;

   public uint Point { get; set; }
   
   #endregion

   #region ForeignKeys

   #endregion

   #region Relationships
   
   public IEnumerable<Review>? Reviews { get; set; }
   
   #endregion

   #region Methods

   #endregion
}