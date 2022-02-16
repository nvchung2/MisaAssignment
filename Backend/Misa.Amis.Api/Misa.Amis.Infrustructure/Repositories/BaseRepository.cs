using Dapper;
using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Interfaces.Repositories;
using MySqlConnector;
using static Misa.Amis.Core.Utils.EntityUtils;

namespace Misa.Amis.Infrastructure.Repositories
{
  /// <summary>
  /// Lớp base repository
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  /// Created by: nvchung-07/02/2022
  public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
  {
    #region Fields

    /// <summary>
    /// Connection factory function
    /// </summary>
    protected readonly Func<MySqlConnection> GetConnection;
    /// <summary>
    /// Tên bảng mà thực thể đại diện
    /// </summary>
    protected readonly string TableName;
    /// <summary>
    /// Tên các cột
    /// </summary>
    protected readonly IEnumerable<string> ColumnNames;
    #endregion

    #region Constructors
    public BaseRepository(Func<MySqlConnection> getConnection)
    {
      GetConnection = getConnection;
      TableName = GetTableName<T>();
      ColumnNames = GetColumnNames<T>();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Lấy DynamicParameters từ thực thể 
    /// </summary>
    /// <param name="entity">Entity type</param>
    /// <returns></returns>
    protected DynamicParameters BuildDynamicParameters(T entity)
    {
      var dp = new DynamicParameters();
      foreach (var col in ColumnNames)//duyệt qua các tên cột
      {
        //get value rồi add vào dp
        var val = typeof(T).GetProperty(col)!.GetValue(entity);
        dp.Add(col, val);
      }
      return dp;
    }
    /// <summary>
    /// Phương thức hỗ trợ phân trang và tìm kiếm theo điều kiện
    /// </summary>
    /// <param name="pageSize">Số bản ghi/trang</param>
    /// <param name="pageNumber">Số trang</param>
    /// <param name="condition">Điều kiện tìm kiếm</param>
    /// <param name="conditionParams">Parameters của điều kiện tìm kiếm</param>
    /// <returns></returns>
    protected Page<T> GetPaged(int pageSize, int pageNumber, string? condition = null, DynamicParameters? conditionParams = null)
    {
      var sql = $"create temporary table filteredTable select * from View_{TableName}{(condition != null ? $" where {condition}" : string.Empty)} order by CreatedDate desc; select * from filteredTable limit @take offset @skip; select count(*) from filteredTable; drop temporary table filteredTable;";//lệnh truy vấn tìm kiếm và phân trang
      //khởi tạo kết nối
      using var db = GetConnection();
      //khởi tạo các params
      var parameters = new DynamicParameters(conditionParams);
      var take = Math.Max(1, pageSize);//số bản ghi/trang
      var skip = Math.Max(0, (pageNumber - 1) * take);//sô bản ghi bị bỏ qua
      parameters.Add("take", take);
      parameters.Add("skip", skip);
      //thực hiện truy vấn
      var res = db.QueryMultiple(sql, parameters);
      var data = res.Read<T>();//các bản ghi
      var totalRecords = res.Read<int>().First();//tổng số bản ghi
      return new Page<T>((int)Math.Ceiling(totalRecords / (double)take), totalRecords, data);
    }
    public virtual int Delete(Guid id)
    {
      var sql = $"delete from {TableName} where {TableName}Id=@id";
      using var db = GetConnection();
      return db.Execute(sql, new { id });
    }

    public virtual IEnumerable<T> GetAll()
    {
      var sql = $"select * from View_{TableName}";
      using var db = GetConnection();
      return db.Query<T>(sql);
    }

    public virtual T GetByCode(string code)
    {
      var sql = $"select * from {TableName} where {TableName}Code=@code";
      using var db = GetConnection();
      return db.QueryFirstOrDefault<T>(sql, new { code });
    }

    public virtual T GetById(Guid id)
    {
      var sql = $"select * from View_{TableName} where {TableName}Id=@id";
      using var db = GetConnection();
      return db.QueryFirstOrDefault<T>(sql, new { id });
    }
    public virtual int Insert(T entity)
    {
      //set giá trị cho khóa chính, CreatedDate, ModifiedDate
      GetPrimaryKeyProperty<T>().SetValue(entity, Guid.NewGuid());
      entity.CreatedDate = DateTime.Now;
      entity.ModifiedDate = null;
      //câu truy vấn có dạng: insert into [TableName] (col1,col2,...) values (@var1,@var2,...)
      var vars = string.Join(",", ColumnNames.Select(c => $"@{c}"));//(@var1,@var2,...)
      var cols = string.Join(",", ColumnNames);//(col1,col2,...)
      var sql = $"insert into {TableName} ({cols}) values ({vars})";
      using var db = GetConnection();
      return db.Execute(sql, BuildDynamicParameters(entity));
    }

    public virtual int Update(Guid id, T entity)
    {
      //set giá trị ModifiedDate
      entity.ModifiedDate = DateTime.Now;
      //câu truy vấn có dạng: update [TableName] set col1=@var1, col2=@var2, ... where [TableName]Id=@id
      var sets = string.Join(",", GetColumnNames<T>(false).Select(c => $"{c}=@{c}"));//col1=@var1,col2=@var2,...
      var sql = $"update {TableName} set {sets} where {TableName}Id=@id";
      using var db = GetConnection();
      var dp = BuildDynamicParameters(entity);
      dp.Add("id", id);//thêm param khóa chính
      return db.Execute(sql, dp);
    }
    #endregion
  }
}
