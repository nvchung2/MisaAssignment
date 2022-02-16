namespace Misa.Amis.Core.Exceptions
{
  /// <summary>
  /// Exception dữ liệu không hợp lệ
  /// </summary>
  /// Created by: nvchung-08/02/2022
  public class ValidationException : Exception
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message">Lỗi</param>
    /// <param name="errPropName">Tên property lỗi</param>
    public ValidationException(string message, string? errPropName = null) : base(message)
    {
      Data["ErrorProperty"] = errPropName;
    }
  }
}
