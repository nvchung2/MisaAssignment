namespace Misa.Amis.Core.Exceptions
{
  /// <summary>
  /// Exception không tìm thấy resource
  /// </summary>
  public class ResourceNotFoundException : Exception
  {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="resourceName">Tên resource</param>
    public ResourceNotFoundException(string resourceName) : base(string.Format(Resources.Text.EMResourceNotFound, resourceName)) { }
  }
}
