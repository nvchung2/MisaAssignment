using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Exceptions;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;

namespace Misa.Amis.Core.Services
{
  /// <summary>
  /// Lớp department service
  /// </summary>
  /// Created by: nvchung-07/02/2022
  public class DepartmentService : BaseService<Department>, IDepartmentService
  {
    #region Constructors
    public DepartmentService(IBaseRepository<Department> repo) : base(repo)
    {
    }
    #endregion

    #region Methods
    //Created by: nvchung (11/02/2022)
    protected override void Validate(Department entity, bool isUpdate)
    {
      //kiểm tra hợp lệ mã đơn vị
      var dpm = Repo.GetById(entity.DepartmentId);
      if (!(dpm == null || (isUpdate && dpm.DepartmentId == entity.DepartmentId)))
      {
        var msg = string.Format(Resources.Text.VEMExisted, Resources.Text.DPNDepartmentCode);
        throw new ValidationException(msg, "DepartmentCode");
      }
    }
    #endregion

  }
}
