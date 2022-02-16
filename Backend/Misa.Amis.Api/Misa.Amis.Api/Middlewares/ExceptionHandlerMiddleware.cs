using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Exceptions;
using Misa.Amis.Core.Resources;

namespace Misa.Amis.Api.Middlewares
{
  /// <summary>
  /// Mdddleware xử lý ngoại lệ
  /// </summary>
  /// Created by: nvchung-09/02/2022
  public class ExceptionHandlerMiddleware
  {
    #region Field

    readonly RequestDelegate _next;
    #endregion

    #region Constructor

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
      _next = next;
    }
    #endregion

    #region Method

    /// <summary>
    /// Xử lý ngoại lệ từ các action controller
    /// </summary>
    /// <param name="http"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext http)
    {
      try
      {
        await _next(http);
      }
      catch (ValidationException validationExc)//Lỗi validate dữ liệu
      {
        http.Response.StatusCode = 400;
        await http.Response.WriteAsJsonAsync(new ApiError(validationExc));
      }
      catch (ResourceNotFoundException notFoundExc)//Lỗi không tìm thấy resource
      {
        http.Response.StatusCode = 404;
        await http.Response.WriteAsJsonAsync(new ApiError(notFoundExc));
      }
      catch (Exception exc)//Lỗi server
      {
        http.Response.StatusCode = 500;
        await http.Response.WriteAsJsonAsync(new ApiError(Text.EMServerError, exc));
      }
    }
    #endregion
  }
}
