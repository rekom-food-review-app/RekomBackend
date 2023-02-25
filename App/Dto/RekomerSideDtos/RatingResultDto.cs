namespace RekomBackend.App.Dto.RekomerSideDtos;

public class RatingResultDto
{
   public float Average { get; set; }

   public string Amount { get; set; } = null!;

   public float PercentFive { get; set; }
   
   public float PercentFour { get; set; }
   
   public float PercentThree { get; set; }
   
   public float PercentTwo { get; set; }
   
   public float PercentOne { get; set; }
}