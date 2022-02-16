using Microsoft.AspNetCore.Mvc;
using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Exceptions;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;
using static Misa.Amis.Core.Utils.EntityUtils;

namespace Misa.Amis.Api.Controllers
{
  /// <summary>
  /// Base controller
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  /// Created by: nvchung (11/02/2022)
  public class BaseController<T> : ControllerBase where T : BaseEntity
  {
    #region Fields
    protected readonly IBaseRepository<T> Repo;
    protected readonly IBaseService<T> Service;
    #endregion

    #region Constructor
    public BaseController(IBaseRepository<T> repo, IBaseService<T> service)
    {
      Repo = repo;
      Service = service;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Trả về bản ghi theo khóa chính hoặc throw <see cref="ResourceNotFoundException"/> nếu null
    /// </summary>
    /// <param name="id">Khóa chính</param>
    /// <returns></returns>
    /// <exception cref="ResourceNotFoundException"></exception>
    /// Created by: nvchung (11/02/2022)
    protected T GetOrThrow(Guid id)
    {
      var entity = Repo.GetById(id);
      if (entity == null)
      {
        throw new ResourceNotFoundException(GetDisplayName(typeof(T)));
      }
      return entity;
    }
    /// <summary>
    /// Lây tất cả bản ghi
    /// </summary>
    /// <returns></returns>
    /// Created by: nvchung (11/02/2022)
    [HttpGet]
    public IActionResult GetAll()
    {
      return Ok(Repo.GetAll());
    }
    /// <summary>
    /// Lấy một bản ghi theo khóa chính
    /// </summary>
    /// <param name="id">Khóa chính</param>
    /// <returns></returns>
    /// Created by: nvchung (11/02/2022)
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
      var entity = GetOrThrow(id);
      return Ok(entity);
    }
    /// <summary>
    /// Tạo mới một bản ghi
    /// </summary>
    /// <param name="entity">Dữ liệu tạo mới</param>
    /// <returns>Số bản ghi được tạo</returns>
    /// Created by: nvchung (11/02/2022)
    [HttpPost]
    public IActionResult Create(T entity)
    {
      return Created("", Service.Insert(entity));
    }
    /// <summary>
    /// Cập nhật một bản ghi theo khóa chính
    /// </summary>
    /// <param name="id">Khóa chính</param>
    /// <param name="entity">Dữ liệu cập nhật</param>
    /// <returns>Số bản ghi được cập nhật</returns>
    /// Created by: nvchung (11/02/2022)
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, T entity)
    {
      GetOrThrow(id);
      return Ok(Service.Update(id, entity));
    }
    /// <summary>
    /// Xóa một bản ghi theo khóa chính
    /// </summary>
    /// <param name="id">Khóa chính</param>
    /// <returns>Số bản ghi bị xóa</returns>
    /// Created by: nvchung (11/02/2022)
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
      GetOrThrow(id);
      return Ok(Repo.Delete(id));
    }
    #endregion
  }
}
