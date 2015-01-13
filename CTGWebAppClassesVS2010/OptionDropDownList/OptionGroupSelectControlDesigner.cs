using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.Design;

namespace OptionDropDownList
{
  /// <summary>
  /// The ControlDesigner class provides a base control designer class that can be inherited 
  /// from and extended to provide design-time support for a Web server control in a design host, 
  /// such as Visual Studio 2005
  /// 
  /// IDataSourceProvider: This interface specifies the behavior required for designers 
  /// that interact with a data source. The interface provides two methods designed to convert a 
  /// data source to a more useful object. 
  /// 
  /// GetSelectedDataSource(): 
  /// retrieves the selected data source object as a loosely typed System.Object. 
  /// 
  /// GetResolvedSelectedDataSource():
  /// retrieves the resolved data source as a System.Collections.IEnumerable object, 
  /// like a System.Array or a System.Data.DataView instance
  /// </summary>
  public class OptionGroupSelectControlDesigner : ControlDesigner
  {
    OptionGroupSelect relatedControl = null;

    /// <summary>
    /// Initialize
    /// </summary>
    /// <param name="component"></param>
    public override void Initialize(System.ComponentModel.IComponent component)
    {
      this.relatedControl = (OptionGroupSelect)component;
      base.Initialize(component);
    }

    /// <summary>
    /// GetDesignTimeHtml
    /// </summary>
    /// <returns></returns>
    public override string GetDesignTimeHtml()
    {
      try
      {
        if (this.relatedControl.Items == null || this.relatedControl.Items.Count == 0)
        {
          return this.GetEmptyDesignTimeHtml();
        }
        else
        {
          foreach (OptionGroupItem item in this.relatedControl.Items)
            if (item.Selected) return this.DrawControl((item.Text == null ? item.Value : item.Text));

          OptionGroupItem firstitem = this.relatedControl.Items[0] as OptionGroupItem;
          return this.DrawControl(firstitem.Text);
        }
      }
      catch(Exception exception)
      {
        return this.GetErrorDesignTimeHtml(exception);
      }
    }

    /// <summary>
    /// GetEmptyDesignTimeHtml
    /// </summary>
    /// <returns></returns>
    protected override string GetEmptyDesignTimeHtml()
    {
      return this.DrawControl("Empty");
    }

    /// <summary>
    /// GetErrorDesignTimeHtml
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    protected override string GetErrorDesignTimeHtml(Exception e)
    {
      return "<table border='1' bgcolor='gray'><tr><td><font color='red'>" + e.Message + "</font></td></tr></table>";
    }

    /// <summary>
    /// Set to True so that the control can be resized on the form.
    /// </summary>
    public override bool AllowResize
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    /// DrawControl
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private String DrawControl(String value)
    {
      StringBuilder renderHtml = new StringBuilder("<select>");
      if(!String.IsNullOrEmpty(value)) renderHtml.Append(String.Format("<option value='{0}'>{0}</option>", value));
      renderHtml.Append("</select>");
      return renderHtml.ToString();
    }
  }
}