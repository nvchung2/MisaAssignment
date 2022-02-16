namespace Misa.Amis.Core.Exceptions
{
  /// <summary>
  /// Exception không tìm thấy resource
  /// </summary>
  /// Created by: nvchung (11/02/2022)
  public class ResourceNotFoundException : Exception
  {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="resourceName">Tên resource</param>
    public ResourceNotFoundException(string resourceName) : base(string.Format(Resources.Text.EMResourceNotFound, resourceName)) { }
  }
}
