using Misa.Amis.Core.Attributes;
using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Enums;
using Misa.Amis.Core.Exceptions;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Reflection;
using static Misa.Amis.Core.Utils.EntityUtils;
namespace Misa.Amis.Core.Services
{
  /// <summary>
  /// Lớp service base
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  /// Created by: nvchung-07/02/2022
  public class BaseService<T> : IBaseService<T> where T : BaseEntity
  {
    #region Fields
    protected readonly IBaseRepository<T> Repo;

    #endregion

    #region Constructors
    public BaseService(IBaseRepository<T> repo)
    {
      Repo = repo;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Custom validate
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="isUpdate">True nếu đang update</param>
    protected virtual void Validate(T entity, bool isUpdate) { }
    /// <summary>
    /// Validate các attribute
    /// </summary>
    /// <param name="entity"></param>
    /// <exception cref="ValidationException"></exception>
    /// Created by: nvchung (11/02/2022)
    private void CommonValidate(T entity)
    {
      foreach (var prop in typeof(T).GetProperties())//duyệt qua các props
      {
        var value = prop.GetValue(entity);//giá trị prop
        var dpName = GetDisplayName(prop);//tên hiển thị
        foreach (var attr in prop.GetCustomAttributes<ValidationAttribute>())//duyệt qua các validation attribute
        {
          if (!attr.IsValid(value))//kiểm tra hợp lệ
          {
            throw new ValidationException(attr.FormatErrorMessage(dpName), prop.Name);
          }
        }
      }
    }
    //Created by: nvchung (11/02/2022)
    public int Insert(T entity)
    {
      //thực hiện validate
      CommonValidate(entity);
      Validate(entity, false);
      //insert
      return Repo.Insert(entity);
    }
    //Created by: nvchung (11/02/2022)
    public int Update(Guid id, T entity)
    {
      GetPrimaryKeyProperty<T>().SetValue(entity, id);//set value cho khóa chính
      //validate
      CommonValidate(entity);
      Validate(entity, true);
      //update
      return Repo.Update(id, entity);
    }
    //Created by: nvchung (11/02/2022)
    public ExcelPackage ExportExcel(ISet<string>? excludedProps)
    {
      var props = typeof(T).GetProperties();//danh sách thuộc tính
      var entityName = GetDisplayName(typeof(T));//tên hiển thị
      if (excludedProps != null)
      {
        props = props.ToHashSet().ExceptBy(excludedProps, prop => prop.Name).ToArray();//loại bỏ các prop bị exclude
      }
      var excel = new ExcelPackage();
      var sheet = excel.Workbook.Worksheets.Add(entityName);
      //Render phần title
      var title = sheet.Cells[1, 1, 1, props.Length + 1];
      title.Value = string.Format(Resources.Text.ExcelTitle, entityName).ToUpper();
      title.Style.Font.Bold = true;
      title.Style.Font.Size = 16;
      title.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
      title.Merge = true;
      var blank = sheet.Cells[2, 1, 2, props.Length + 1];
      blank.Value = "";
      blank.Merge = true;
      sheet.Cells[3, 1].Value = "STT";
      //Render phần header
      for (int i = 0; i < props.Length; i++)
      {
        var cell = sheet.Cells[3, i + 2];
        cell.Value = GetDisplayName(props[i]);
      }
      var header = sheet.Cells[3, 1, 3, props.Length + 1];
      header.Style.Fill.PatternType = ExcelFillStyle.Solid;
      header.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
      header.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
      header.Style.Font.Bold = true;
      //Render phần dữ liệu
      var data = Repo.GetAll().ToList();
      for (var i = 0; i < data.Count; i++)//duyệt qua từng bản ghi
      {
        sheet.Cells[i + 4, 1].Value = i + 1;//stt
        for (var j = 0; j < props.Length; j++)//duyệt qua từng prop render các cột tương ứng
        {
          var cell = sheet.Cells[i + 4, j + 2];
          var val = props[j].GetValue(data[i]);
          switch (val)//format hiển thị đúng với kiểu dữ liệu
          {
            case DateTime:
              cell.Style.Numberformat.Format = "dd/mm/yyyy";
              cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
              break;
            case Enum e:
              val = e.GetDisplayName();
              break;
          }
          cell.Value = val;
        }
      }
      sheet.Cells.AutoFitColumns();
      return excel;
    }
    #endregion
  }
}
