using System.ComponentModel.DataAnnotations;

namespace RekomBackend.App.Dto.RekomerSideDtos.Request;

public class RekomerConfirmAccountRequest
{
   [MaxLength(4)]
   public string OtpCode { get; set; } = null!;
}