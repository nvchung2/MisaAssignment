using Misa.Amis.Core.Entities;
using OfficeOpenXml;

namespace Misa.Amis.Core.Interfaces.Services
{
  /// <summary>
  /// Interface service base
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  public interface IBaseService<T> where T : BaseEntity
  {
    /// <summary>
    /// Thêm mới một bản ghi <typeparamref name="T"/>
    /// </summary>
    /// <param name="entity">Dữ liệu thêm mới</param>
    /// <returns>Số bản ghi được tạo</returns>
    int Insert(T entity);
    /// <summary>
    /// Cập nhật một bản ghi
    /// </summary>
    /// <param name="id">Khóa chính</param>
    /// <param name="entity">Dữ kiệu cập nhật</param>
    /// <returns>Số bản ghi được cập nhật</returns>
    int Update(Guid id, T entity);
    /// <summary>
    /// Export danh sách bản ghi <typeparamref name="T"/> ra file excel
    /// </summary>
    /// <param name="excludedProps">Các property không được hiển thị trong file excel</param>
    /// <returns></returns>
    ExcelPackage ExportExcel(ISet<string>? excludedProps = null);
  }
}
