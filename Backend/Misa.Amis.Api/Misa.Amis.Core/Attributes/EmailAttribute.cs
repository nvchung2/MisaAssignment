namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute kiểm tra hợp lệ email
  /// </summary>
  /// Created by: nvchung-08/02/2022
  public class EmailAttribute : RegularExpressionAttribute
  {
    public EmailAttribute() : base(@"^[^\s@]+@[^\s@]+\.[^\s@]+$")
    {
      ErrorMessageFormat = Resources.Text.VEMEmail;
    }
  }
}
