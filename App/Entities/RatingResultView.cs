using Microsoft.EntityFrameworkCore;

namespace RekomBackend.App.Entities;

[Keyless]
public class RatingResultView
{
   public string RestaurantId { get; set; } = null!;

   public float? Average { get; set; }

   public int Amount { get; set; }

   public float? PercentFive { get; set; }
   
   public float? PercentFour { get; set; }
   
   public float? PercentThree { get; set; }
   
   public float? PercentTwo { get; set; }
   
   public float? PercentOne { get; set; }
}