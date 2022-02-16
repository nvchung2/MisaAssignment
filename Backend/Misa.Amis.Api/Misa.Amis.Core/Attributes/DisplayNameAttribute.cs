namespace Misa.Amis.Core.Attributes
{
  /// <summary>
  /// Attribute chứa thông tin hiển trị tên property/class/enum member
  /// </summary>
  /// Created by: nvchung-08/02/2022
  [AttributeUsage(AttributeTargets.All)]
  public class DisplayNameAttribute : Attribute
  {
    string _name;
    public string Name { get => Resources.Text.ResourceManager.GetString(_name); }

    public DisplayNameAttribute(string name)
    {
      _name = name;
    }
  }
}
