using System.Text.RegularExpressions;

namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute kiểm tra theo biểu thức chính quy
  /// </summary>
  /// Created by: nvchung - 08/02/2022
  [AttributeUsage(AttributeTargets.Property)]
  public class RegularExpressionAttribute : ValidationAttribute
  {
    #region Properties
    /// <summary>
    /// Mẫu kiểm tra
    /// </summary>
    public string Pattern { get; private set; }
    public override string ErrorMessageFormat { get; set; } = Resources.Text.VEMRegular;
    #endregion

    #region Constructor
    public RegularExpressionAttribute(string pattern)
    {
      Pattern = pattern;
    }
    #endregion

    #region Methods
    /// Created by: nvchung (11/02/2022)
    public override bool IsValid(object? value)
    {
      if (value is string s && !string.IsNullOrEmpty(s))
      {
        return Regex.IsMatch(s, Pattern);
      }
      return true;
    }
    /// Created by: nvchung (11/02/2022)
    public override string FormatErrorMessage(string displayName)
    {
      return string.Format(ErrorMessageFormat, displayName, Pattern);
    }
    #endregion

  }
}
