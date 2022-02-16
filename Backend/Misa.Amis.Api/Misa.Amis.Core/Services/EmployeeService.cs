using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Exceptions;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;

namespace Misa.Amis.Core.Services
{
  /// <summary>
  /// Lớp employee service
  /// </summary>
  /// Created by: nvchung-07/02/2022
  public class EmployeeService : BaseService<Employee>, IEmployeeService
  {
    #region Fields
    readonly IDepartmentRepository _dpmRepo;

    #endregion

    #region Constructors
    public EmployeeService(IEmployeeRepository repo, IDepartmentRepository dpmRepo) : base(repo)
    {
      _dpmRepo = dpmRepo;
    }

    #endregion

    #region Methods
    //Created by: nvchung (11/02/2022)
    protected override void Validate(Employee entity, bool isUpdate)
    {
      //Ngày sinh không được lớn hơn ngày hiện tại
      if (entity.DateOfBirth != null && entity.DateOfBirth > DateTime.Now)
      {
        var msg = string.Format(Resources.Text.VEMLessThan, Resources.Text.DPNDateOfBirth, Resources.Text.DPNCurrentDate);
        throw new ValidationException(msg, "DateOfBirth");
      }
      //Ngày cấp không được lớn hơn ngày hiện tại
      if (entity.IdentityDate != null && entity.IdentityDate > DateTime.Now)
      {
        var msg = string.Format(Resources.Text.VEMLessThan, Resources.Text.DPNIndetityDate, Resources.Text.DPNCurrentDate);
        throw new ValidationException(msg, "DateOfBirth");
      }
      var emp = Repo.GetByCode(entity.EmployeeCode);
      //Kiểm tra hợp lệ mã nhân viên
      if (!(emp == null || (isUpdate && emp.EmployeeId == entity.EmployeeId)))
      {
        var msg = string.Format(Resources.Text.VEMExisted, Resources.Text.DPEmployeeCode);
        throw new ValidationException(msg, "EmployeeCode");
      }
      //Kiểm tra hợp lệ mã đơn vị
      if (entity.DepartmentId.HasValue && _dpmRepo.GetById(entity.DepartmentId.Value) == null)
      {
        var msg = string.Format(Resources.Text.VEMNotExisted, Resources.Text.DPNDepartmentCode);
        throw new ValidationException(msg, "DepartmentId");
      }
    }
    #endregion

  }
}
