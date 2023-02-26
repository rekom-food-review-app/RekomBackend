using Microsoft.EntityFrameworkCore;

namespace RekomBackend.App.Entities;

[Keyless]
public class RatingResultView
{
   public string RestaurantId { get; set; } = null!;

   public float Average { get; set; } = 0;

   public int Amount { get; set; }

   public float PercentFive { get; set; } = 0;

   public float PercentFour { get; set; } = 0;

   public float PercentThree { get; set; } = 0;

   public float PercentTwo { get; set; } = 0;

   public float PercentOne { get; set; } = 0;
}