namespace Misa.Amis.Core.Entities
{
  /// <summary>
  /// Định dạng json trả về khi có lỗi
  /// </summary>
  /// Created by: nvchung-08/02/2022
  public class ApiError
  {
    #region Properties
    /// <summary>
    /// Thông tin lỗi (User)
    /// </summary>
    public string UserMessage { get; set; }
    /// <summary>
    /// Thông tin lỗi (Developer)
    /// </summary>
    public string DevMessage { get; set; }
    /// <summary>
    /// Chi tiết lỗi
    /// </summary>
    public object? Data { get; set; }
    #endregion

    #region Constructors
    public ApiError(string userMessage, string devMessage, object? data)
    {
      UserMessage = userMessage;
      DevMessage = devMessage;
      Data = data;
    }
    public ApiError(string userMsg, Exception exc) : this(userMsg, exc.Message, exc.Data)
    {

    }
    public ApiError(Exception exc) : this(exc.Message, exc)
    {

    }
    #endregion
  }
}
