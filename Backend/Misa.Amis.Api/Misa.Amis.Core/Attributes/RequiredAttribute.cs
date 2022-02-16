namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute đánh dấu property là bắt buộc
  /// </summary>
  /// Created by: nvchung - 08/02/2022
  [AttributeUsage(AttributeTargets.Property)]
  public class RequiredAttribute : ValidationAttribute
  {
    #region Properties
    public override string ErrorMessageFormat { get; set; } = Resources.Text.VEMRequired;

    #endregion

    #region Methods
    public override string FormatErrorMessage(string displayName)
    {
      return string.Format(ErrorMessageFormat, displayName);
    }

    public override bool IsValid(object? value)
    {
      if (value is string s)
      {
        return !string.IsNullOrEmpty(s);
      }
      return value != null;
    }
    #endregion

  }
}
