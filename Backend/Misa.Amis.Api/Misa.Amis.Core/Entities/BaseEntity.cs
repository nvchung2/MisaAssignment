using Misa.Amis.Core.Attributes;

namespace Misa.Amis.Core.Entities
{
  /// <summary>
  /// Lớp entity base
  /// </summary>
  /// Created by: nvchung-08/02/2022
  public class BaseEntity
  {
    /// <summary>
    /// Ngày tạo
    /// </summary>
    [Column]
    [DisplayName("DPNCreatedBy")]
    public string? CreatedBy { get; set; }
    /// <summary>
    /// Ngày sửa
    /// </summary>
    [Column]
    [DisplayName("DPNModifiedBy")]
    public string? ModifiedBy { get; set; }
    /// <summary>
    /// Người tạo
    /// </summary>
    [Column]
    [DisplayName("DPNCreatedDate")]
    public DateTime? CreatedDate { get; set; }
    /// <summary>
    /// Người sửa
    /// </summary>
    [Column]
    [DisplayName("DPNModifiedDate")]
    public DateTime? ModifiedDate { get; set; }
  }
}
