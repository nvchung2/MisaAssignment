using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Interfaces.Repositories;
using MySqlConnector;

namespace Misa.Amis.Infrastructure.Repositories
{
  /// <summary>
  /// Created by: nvchung (11/02/2022)
  /// </summary>
  public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
  {
    public DepartmentRepository(Func<MySqlConnection> getConnection) : base(getConnection)
    {
    }
  }
}
