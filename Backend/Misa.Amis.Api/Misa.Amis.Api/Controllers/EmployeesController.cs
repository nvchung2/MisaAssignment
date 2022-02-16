using Microsoft.AspNetCore.Mvc;
using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;

namespace Misa.Amis.Api.Controllers
{
  /// <summary>
  /// Employee api
  /// </summary>
  /// Created by: nvchung-10/02/2022
  [Route("api/v1/[controller]")]
  [ApiController]
  public class EmployeesController : BaseController<Employee>
  {
    #region Constructor
    public EmployeesController(IEmployeeRepository repo, IEmployeeService service) : base(repo, service)
    {
    }
    #endregion

    #region Methods
    /// <summary>
    /// Phân trang và tìm kiếm theo mã nhân viên, họ tên, số điện thoại
    /// </summary>
    /// <param name="pageSize">Số bản ghi/trang</param>
    /// <param name="pageNumber">Số trang</param>
    /// <param name="search">Từ khóa</param>
    /// <returns></returns>
    [HttpGet("Filter")]
    public IActionResult GetPaged(int pageSize = 10, int pageNumber = 0, string? search = null)
    {
      return Ok((Repo as IEmployeeRepository).GetPaged(pageNumber, pageSize, search));
    }
    /// <summary>
    /// Lấy mã nhân viên mới tự động
    /// </summary>
    /// <returns></returns>
    [HttpGet("NextCode")]
    public IActionResult GetNextCode()
    {
      return Ok((Repo as IEmployeeRepository).GetNextEmployeeCode());
    }
    /// <summary>
    /// Xóa nhiều nhân viên
    /// </summary>
    /// <param name="ids">Danh sách khóa chính</param>
    /// <returns>Số bản ghi bị xóa</returns>
    [HttpPost("BulkDelete")]
    public IActionResult BulkDelete(IEnumerable<Guid> ids)
    {
      return Ok((Repo as IEmployeeRepository).BulkDelete(ids));
    }
    /// <summary>
    /// Export dữ liệu dạng excel
    /// </summary>
    /// <returns>File excel</returns>
    [HttpGet("ExcelFile")]
    public IActionResult GetExcelFile()
    {
      using var excel = Service.ExportExcel(new HashSet<string> { "EmployeeId", "DepartmentId" });
      var stream = new MemoryStream();
      excel.SaveAs(stream);
      stream.Position = 0;
      return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "danh_sach_nhan_vien.xlsx");
    }
    #endregion
  }
}
