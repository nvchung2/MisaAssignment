using Misa.Amis.Core.Attributes;
using Misa.Amis.Core.Entities;
using System.Reflection;

namespace Misa.Amis.Core.Utils
{
  /// <summary>
  /// Lớp tiện ích Entity
  /// </summary>
  /// Created by: nvchung-07/02/2022
  public static class EntityUtils
  {
    /// <summary>
    /// Lấy tên bảng mà thực thể đại diện
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// Created by: nvchung (11/02/2022)
    public static string GetTableName<T>() where T : BaseEntity
    {
      var type = typeof(T);
      if (Attribute.GetCustomAttribute(type, typeof(TableAttribute)) is TableAttribute tbAttr)
      {
        return string.IsNullOrEmpty(tbAttr.Name) ? type.Name : tbAttr.Name;
      }
      throw new Exception($"{type.Name} must be annotated with TableAttribute");
    }
    /// <summary>
    /// Lấy tên các cột
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="includePrimaryKey">Có bao gồm khóa chính không?</param>
    /// <returns></returns>
    /// Created by: nvchung (11/02/2022)
    public static IEnumerable<string> GetColumnNames<T>(bool includePrimaryKey = true) where T : BaseEntity
    {
      return typeof(T).GetProperties().Where(p => p.IsDefined(typeof(ColumnAttribute)) || (includePrimaryKey && p.IsDefined(typeof(PrimaryKeyAttribute)))).Select(p => p.Name);
    }
    /// <summary>
    /// Lấy prop khóa chính
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <returns></returns>
    /// Created by: nvchung (11/02/2022)
    public static PropertyInfo GetPrimaryKeyProperty<T>() where T : BaseEntity
    {
      return typeof(T).GetProperties().First(p => p.IsDefined(typeof(PrimaryKeyAttribute)));
    }
    /// <summary>
    /// Lấy tên hiển thị
    /// </summary>
    /// <param name="member">Property/Class</param>
    /// <returns></returns>
    /// Created by: nvchung (11/02/2022)
    public static string GetDisplayName(MemberInfo member)
    {
      var dpNameAttr = member.GetCustomAttribute<DisplayNameAttribute>();
      return dpNameAttr?.Name ?? member.Name;
    }
  }
}
