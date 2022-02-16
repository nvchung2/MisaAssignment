using Misa.Amis.Core.Entities;

namespace Misa.Amis.Core.Interfaces.Repositories
{
  /// Created by: nvchung (11/02/2022)
  public interface IEmployeeRepository : IBaseRepository<Employee>, IPageableRepository<Employee>
  {
    /// <summary>
    /// Sinh mã nhân viên tự động
    /// </summary>
    /// <returns></returns>
    string GetNextEmployeeCode();
    /// <summary>
    /// Xóa nhiều nhân viên
    /// </summary>
    /// <param name="ids">Danh sách khóa chính</param>
    /// <returns>Số bản ghi bị xóa</returns>
    int BulkDelete(IEnumerable<Guid> ids);
  }
}
