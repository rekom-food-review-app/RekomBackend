using System.ComponentModel.DataAnnotations.Schema;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Models.Dto;

namespace RekomBackend.App.Models.Entities;

public class Restaurant : EntityBase
{
   #region Columns

   [Column(TypeName = "varchar(200)")]
   public string Name { get; set; } = null!;

   [Column(TypeName = "varchar(200)")]
   public string CoverImageUrl { get; set; } = null!;

   [Column(TypeName = "varchar(200)")] 
   public string Coordinates { get; set; } = null!;
   
   [Column(TypeName = "varchar(500)")] 
   public string Description { get; set; } = null!;
   
   #endregion

   #region ForeignKeys

   public string AccountId { get; set; } = null!;
   
   #endregion

   #region Relationships

   public Account? Account { get; set; }

   public IEnumerable<Food>? Menu { get; set; }

   public IEnumerable<Review>? Reviews { get; set; }
   
   #endregion

   #region Methods

   public RatingResultDto CalculateRatingResult()
   {
      if (Reviews is null) throw new NotIncludeRelationshipException();

      var result = new RatingResultDto();
      uint total = 0;
      uint amountFive = 0, amountFour = 0, amountThree = 0, amountTwo = 0, amountOne = 0;
      
      foreach (var review in Reviews)
      {
         result.Amount ++;
         var point = review.Rating!.Point;
         total += point;
         switch (point)
         {
            case 5:
               amountFive ++;
               break;
            case 4:
               amountFour ++;
               break;
            case 3:
               amountThree ++;
               break;
            case 2:
               amountTwo ++;
               break;
            case 1:
               amountOne ++;
               break;
         }
      }
      
      result.Average = result.Amount > 0 ? total / result.Amount : 0;
      result.PercentFive = result.Amount > 0 ? amountFive / result.Amount : 0;
      result.PercentFour = result.Amount > 0 ? amountFour / result.Amount : 0;
      result.PercentThree = result.Amount > 0 ? amountThree / result.Amount : 0;
      result.PercentTwo = result.Amount > 0 ? amountTwo / result.Amount : 0;
      result.PercentOne = result.Amount > 0 ? amountOne / result.Amount : 0;
      
      return result;
   }
   

   #endregion
}