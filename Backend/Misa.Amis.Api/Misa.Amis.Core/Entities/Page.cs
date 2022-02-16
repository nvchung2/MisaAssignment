namespace Misa.Amis.Core.Entities
{
  /// <summary>
  /// Định dạng json trả về khi phân trang
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  /// Created by: nvchung-08/02/2022
  public class Page<T> where T : BaseEntity
  {
    #region Properties

    /// <summary>
    /// Tổng số trang
    /// </summary>
    public int TotalPages { get; set; }
    /// <summary>
    /// Tổng só bản ghi
    /// </summary>
    public int TotalRecords { get; set; }
    /// <summary>
    /// Các bản ghi
    /// </summary>
    public IEnumerable<T> Data { get; set; }
    #endregion

    #region Constructors
    public Page(int totalPages, int totalRecords, IEnumerable<T> data)
    {
      TotalPages = totalPages;
      TotalRecords = totalRecords;
      Data = data;
    }
    #endregion

  }
}
