namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute đánh dấu lớp là một bảng trong csdl
  /// </summary>
  /// Created by: nvchung - 08/02/2022
  [AttributeUsage(AttributeTargets.Class)]
  public class TableAttribute : Attribute
  {
    /// <summary>
    /// Tên bảng trong csdl
    /// </summary>
    public string Name { get; set; } = string.Empty;
  }
}
