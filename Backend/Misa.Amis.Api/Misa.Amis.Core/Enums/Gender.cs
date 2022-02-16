using Misa.Amis.Core.Attributes;

namespace Misa.Amis.Core.Enums
{
  /// <summary>
  /// Enum giới tính
  /// </summary>
  /// Created by: nvchung-08/02/2022
  public enum Gender
  {
    /// <summary>
    /// Nam
    /// </summary>
    [DisplayName("DPGenderMale")]
    Male = 1,
    /// <summary>
    /// Nữ
    /// </summary>
    [DisplayName("DPGenderFemale")]
    Female = 0,
    /// <summary>
    /// Không xác định
    /// </summary>
    [DisplayName("DPGenderUnknown")]
    Unknown = 2
  }
}
