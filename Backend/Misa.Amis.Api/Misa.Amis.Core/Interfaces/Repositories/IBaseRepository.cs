using Misa.Amis.Core.Entities;

namespace Misa.Amis.Core.Interfaces.Repositories
{
  /// <summary>
  /// Interface repository base
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  /// Created by: nvchung-08/02/2022
  public interface IBaseRepository<T> where T : BaseEntity
  {
    /// <summary>
    /// Lấy tất cả bản ghi <typeparamref name="T"/>
    /// </summary>
    /// <returns></returns>
    IEnumerable<T> GetAll();
    /// <summary>
    /// Lấy một bản ghi <typeparamref name="T"/> theo khóa chính
    /// </summary>
    /// <param name="id">Khóa chính</param>
    /// <returns></returns>
    T GetById(Guid id);
    /// <summary>
    /// Lấy một bản ghi <typeparamref name="T"/> theo mã
    /// </summary>
    /// <param name="code">Mã</param>
    /// <returns></returns>
    T GetByCode(string code);
    /// <summary>
    /// Thêm mới một bản ghi <typeparamref name="T"/>
    /// </summary>
    /// <param name="entity">Dữ liệu thêm mới</param>
    /// <returns>Số bản ghi được thêm mới</returns>
    int Insert(T entity);
    /// <summary>
    /// Cập nhật bản ghi <typeparamref name="T"/>
    /// </summary>
    /// <param name="id">Khóa chính</param>
    /// <param name="entity">Dữ liệu cập nhật</param>
    /// <returns>Số bản ghi được cập nhật</returns>
    int Update(Guid id, T entity);
    /// <summary>
    /// Xóa một bản ghi <typeparamref name="T"/>
    /// </summary>
    /// <param name="id">Khóa chính</param>
    /// <returns>Số bản ghi bị xóa</returns>
    int Delete(Guid id);
  }
}
