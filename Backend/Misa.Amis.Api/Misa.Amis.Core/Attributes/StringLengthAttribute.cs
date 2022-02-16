namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute kiểm tra độ dài tối đa của chuỗi
  /// </summary>
  /// Created by: nvchung - 08/02/2022
  [AttributeUsage(AttributeTargets.Property)]
  public class StringLengthAttribute : ValidationAttribute
  {
    #region Properties

    /// <summary>
    /// Độ dài tối đa
    /// </summary>
    public int MaxLength { get; private set; }
    public override string ErrorMessageFormat { get; set; } = Resources.Text.VEMStringLength;
    #endregion

    #region Constructor

    public StringLengthAttribute(int maxLength)
    {
      MaxLength = maxLength;
    }
    #endregion

    #region Methods

    public override string FormatErrorMessage(string displayName)
    {
      return string.Format(ErrorMessageFormat, displayName, MaxLength);
    }

    public override bool IsValid(object? value)
    {
      if (value == null || value is not string val)
      {
        return true;
      }
      return val.Length <= MaxLength;
    }
    #endregion
  }
}
