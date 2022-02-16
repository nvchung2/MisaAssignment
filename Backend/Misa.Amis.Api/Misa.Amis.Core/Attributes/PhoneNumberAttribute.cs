namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute kiểm tra hợp lệ số điện thoại
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class PhoneNumberAttribute : RegularExpressionAttribute
  {
    public PhoneNumberAttribute() : base(@"^\d{10,12}$")
    {
      ErrorMessageFormat = Resources.Text.VEMPhoneNumber;
    }
  }
}
