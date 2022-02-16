using Misa.Amis.Core.Attributes;
using System.Reflection;

namespace Misa.Amis.Core.Enums
{
  /// <summary>
  /// Lớp mở rộng cho kiểu Enum
  /// </summary>
  public static class EnumExtensions
  {
    /// <summary>
    /// Lấy tên hiển thị của enum member
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static string GetDisplayName(this Enum val)
    {
      var f = val.GetType().GetField(val.ToString());
      if (f != null && f.IsDefined(typeof(DisplayNameAttribute)))
      {
        return f.GetCustomAttribute<DisplayNameAttribute>().Name;
      }
      return string.Empty;
    }
  }
}
