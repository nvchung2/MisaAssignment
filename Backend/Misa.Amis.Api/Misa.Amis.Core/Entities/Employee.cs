using Misa.Amis.Core.Attributes;
using Misa.Amis.Core.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Misa.Amis.Core.Entities
{
  /// <summary>
  /// Thực thể nhân viên
  /// </summary>
  /// Created by: nvchung-08/02/2022
  [Table]
  [DisplayName("DPNEmployee")]
  public class Employee : BaseEntity
  {
    /// <summary>
    /// Khóa chính
    /// </summary>
    [PrimaryKey]
    public Guid EmployeeId { get; set; }
    /// <summary>
    /// Mã nhân viên
    /// </summary>
    [Column]
    [DisplayName("DPEmployeeCode")]
    [Required]
    [RegularExpression(@"^NV-\d{1,22}$")]
    public string? EmployeeCode { get; set; }
    /// <summary>
    /// Họ tên nhân viên
    /// </summary>
    [Column]
    [DisplayName("DPNFullName")]
    [Required]
    [StringLength(200)]
    public string? FullName { get; set; }
    /// <summary>
    /// Giới tính
    /// </summary>
    [Column]
    [DisplayName("DPNGender")]
    public Gender? Gender { get; set; }
    /// <summary>
    /// Ngày sinh
    /// </summary>
    [Column]
    [DisplayName("DPNDateOfBirth")]
    public DateTime? DateOfBirth { get; set; }
    /// <summary>
    /// Số điện thoại di động
    /// </summary>
    [Column]
    [DisplayName("DPNPhoneNumber")]
    [PhoneNumber]
    public string? PhoneNumber { get; set; }
    /// <summary>
    /// Số điện thoại cố định
    /// </summary>
    [Column]
    [DisplayName("DPNTelephoneNumber")]
    [PhoneNumber]
    public string? TelephoneNumber { get; set; }
    /// <summary>
    /// Email
    /// </summary>
    [Column]
    [DisplayName("DPNEmail")]
    [Email]
    [StringLength(255)]
    public string? Email { get; set; }
    /// <summary>
    /// Đia chỉ
    /// </summary>
    [Column]
    [DisplayName("DPNAddress")]
    [StringLength(255)]
    public string? Address { get; set; }
    /// <summary>
    /// Số CMND
    /// </summary>
    [Column]
    [RegularExpression(@"^\d+$", ErrorMessageFormat = "Thông tin {0} phải là các chữ số")]
    [DisplayName("DPNIndetityNumber")]
    [StringLength(20)]
    public string? IdentityNumber { get; set; }
    /// <summary>
    /// Nơi cấp
    /// </summary>
    [Column]
    [DisplayName("DPNIndentityPlace")]
    [StringLength(255)]
    public string? IdentityPlace { get; set; }
    /// <summary>
    /// Ngày cấp
    /// </summary>
    [Column]
    [DisplayName("DPNIndetityDate")]
    public DateTime? IdentityDate { get; set; }
    /// <summary>
    /// Mã đơn vị
    /// </summary>
    [Column]
    [Required]
    public Guid? DepartmentId { get; set; }
    /// <summary>
    /// Chức vụ
    /// </summary>
    [Column]
    [DisplayName("DPNEmployeePosition")]
    [StringLength(128)]
    public string? EmployeePosition { get; set; }
    /// <summary>
    /// Số Tk
    /// </summary>
    [Column]
    [RegularExpression(@"^\d+$", ErrorMessageFormat = "Thông tin {0} phải là các chữ số")]
    [DisplayName("DPNBankAccountNumber")]
    [StringLength(50)]
    public string? BankAccountNumber { get; set; }
    /// <summary>
    /// Tên ngân hàng
    /// </summary>
    [Column]
    [DisplayName("DPNBankName")]
    [StringLength(120)]
    public string? BankName { get; set; }
    /// <summary>
    /// Tên chi nhánh
    /// </summary>
    [Column]
    [DisplayName("DPNBankBranchName")]
    [StringLength(255)]
    public string? BankBranchName { get; set; }
    /// <summary>
    /// Tên đơn vị
    /// </summary>
    /// 
    [DisplayName("DPNDepartmentName")]
    public string? DepartmentName { get; set; }
    /// <summary>
    /// Mã đơn vị
    /// </summary>
    /// 
    [DisplayName("DPNDepartmentCode")]
    public string? DepartmentCode { get; set; }
  }
}
