using System.Globalization;
using System.Text;

namespace RekomBackend.App.Helpers;

public class StringHelper : IStringHelper
{
   public static string ToEnglish(string? input)
   {
      if (input is null) { return string.Empty; }
      
      return new string(input.Normalize(NormalizationForm.FormD)
         .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
         .ToArray());
   }
}