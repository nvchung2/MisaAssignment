using Dapper;
using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Interfaces.Repositories;
using MySqlConnector;

namespace Misa.Amis.Infrastructure.Repositories
{
  public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
  {
    public EmployeeRepository(Func<MySqlConnection> getConnection) : base(getConnection)
    {
    }
    /// <inheritdoc/>
    /// <summary>
    /// Phân trang và tìm kiếm theo mã, họ tên, số điện thoại
    /// </summary>
    public Page<Employee> GetPaged(int pageNumber, int pageSize, string? search = null)
    {
      var parameters = new DynamicParameters(new { pageNumber, pageSize });
      string? condition = null;
      //khởi tạo điều kiện tìm kiếm
      if (search != null)
      {
        condition = "EmployeeCode like concat('%',@search,'%') or FullName like concat('%',@search,'%') or PhoneNumber like concat('%',@search,'%')";
        parameters.Add("search", search);
      }
      return GetPaged(pageSize, pageNumber, condition, parameters);
    }
    public string GetNextEmployeeCode()
    {
      //lệnh truy vấn mã nhân viên lớn nhất
      var sql = "select max(cast(substr(EmployeeCode,4) as int)) from Employee";
      using var db = GetConnection();
      //tăng mã thêm một
      var nextIndex = db.QueryFirstOrDefault<int>(sql) + 1;
      return "NV-" + nextIndex;
    }
    public int BulkDelete(IEnumerable<Guid> ids)
    {
      var sql = "delete from Employee where EmployeeId in @ids";
      using var db = GetConnection();
      return db.Execute(sql, new { ids });
    }
  }
}
