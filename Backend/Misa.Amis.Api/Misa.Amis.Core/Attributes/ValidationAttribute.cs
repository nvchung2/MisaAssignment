namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Lớp trừu tượng của các attribute validator
  /// </summary>
  /// Created by: nvchung-08/02/2022
  public abstract class ValidationAttribute : Attribute
  {
    /// <summary>
    /// Chuỗi format trong <see cref="string.Format(string, object?)"/>
    /// </summary>
    public abstract string ErrorMessageFormat { get; set; }
    /// <summary>
    /// Phương thức format chuỗi lỗi
    /// </summary>
    /// <param name="displayName">Tên hiển thị của property</param>
    /// <returns>Chuỗi đã được format</returns>
    public abstract string FormatErrorMessage(string displayName);
    /// <summary>
    /// Phương thức kiểm tra hợp lệ dữ liệu
    /// </summary>
    /// <param name="value">Giá trị cần kiểm tra</param>
    /// <returns>True nếu hợp lệ</returns>
    public abstract bool IsValid(object? value);
  }
}
