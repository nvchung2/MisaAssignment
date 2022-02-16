namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute kiểm tra dữ liệu trong khoảng min-max
  /// </summary>
  /// Created by: nvchung-08/02/2022
  [AttributeUsage(AttributeTargets.Property)]
  public class RangeAttribute : ValidationAttribute
  {
    #region MyRegion

    /// <summary>
    /// Giá trị tối thiểu
    /// </summary>
    public object? Min { get; set; }
    /// <summary>
    /// Giá trị tối đa
    /// </summary>
    public object? Max { get; set; }
    public override string ErrorMessageFormat { get; set; } = Resources.Text.VEMRange;
    #endregion

    #region Methods

    public override string FormatErrorMessage(string displayName)
    {
      return string.Format(ErrorMessageFormat, displayName, Min, Max);
    }
    /// Created by: nvchung (11/02/2022)
    public override bool IsValid(object? value)
    {
      if (value is not IComparable valueIC) return true;
      bool isValid = true;
      if (Min is IComparable minIC)
      {
        isValid = valueIC.CompareTo(minIC) >= 0;
      }
      if (Max is IComparable maxIC)
      {
        isValid = valueIC.CompareTo(maxIC) <= 0;
      }
      return isValid;
    }
    #endregion
  }
}
