namespace RekomBackend.App.Dto.Common.Request;

public class PaginationRequestDto
{
   public int Page { get; set; }
   
   public int Size { get; set; }

   public DateTime? LastTimestamp { get; set; }
   
   // [FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime? lastTimestamp
}