using Misa.Amis.Core.Entities;

namespace Misa.Amis.Core.Interfaces.Repositories
{
  public interface IPageableRepository<T> where T : BaseEntity
  {
    /// <summary>
    /// Phân trang kết hợp tìm kiếm
    /// </summary>
    /// <param name="pageNumber">Số bản ghi/trang</param>
    /// <param name="pageSize">Số trang</param>
    /// <param name="search">Từ khóa</param>
    /// <returns></returns>
    Page<T> GetPaged(int pageNumber, int pageSize, string? search = null);
  }
}
