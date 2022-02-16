using Misa.Amis.Core.Attributes;

namespace Misa.Amis.Core.Entities
{
  /// <summary>
  /// Thực thể đơn vị
  /// </summary>
  /// Created by: nvchung-08/02/2022
  [Table]
  [DisplayName("DPNDepartment")]
  public class Department : BaseEntity
  {
    /// <summary>
    /// Khóa chính
    /// </summary>
    [PrimaryKey]
    public Guid DepartmentId { get; set; }
    /// <summary>
    /// Mã đơn vị
    /// </summary>
    [Column]
    [Required]
    [DisplayName("DPNDepartmentCode")]
    [RegularExpression(@"^DV-\d{1,22}$")]
    public string? DepartmentCode { get; set; }
    /// <summary>
    /// Tên đơn vị
    /// </summary>
    [Column]
    [Required]
    [DisplayName("DPNDepartmentName")]
    [StringLength(128)]
    public string? DepartmentName { get; set; }
    /// <summary>
    /// Mô tả
    /// </summary>
    [Column]
    [StringLength(255)]
    public string? Description { get; set; }

  }
}
