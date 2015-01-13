using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace OptionDropDownList
{
  /// <summary>
  /// Rappresent a single list item with its render method and the
  /// propterty to manege the select OptionGroup
  /// </summary>
  [DefaultProperty("Text")]
  public class OptionGroupItem : WebControl, ICloneable
  {
    #region Constructors
    public OptionGroupItem()
    {
    }

    public OptionGroupItem(String value, String text)
    {
      this.value = value;
      this.text = text;
    }

    public OptionGroupItem(String value, String text, String optionGroup)
      : this(value, text)
    {
      this.optionGroup = optionGroup;
    }

    public OptionGroupItem(String value, String text, String optionGroup, Object data)
      : this(value, text, optionGroup)
    {
      this.data = data;
    }
    #endregion

    #region Property
    private String text;
    /// <summary>
    /// Get or set the text property
    /// </summary>
    public String Text
    {
      get { return this.text; }
      set { this.text = value; }
    }

    private String value;
    /// <summary>
    /// Get or set the value property
    /// </summary>
    public String Value
    {
      get { return this.value; }
      set { this.value = value; }
    }

    private String optionGroup;
    /// <summary>
    /// Get or set the group property
    /// </summary>
    public String OptionGroup
    {
      get { return this.optionGroup; }
      set { this.optionGroup = value; }
    }

    private bool selected;
    /// <summary>
    /// Get or set the flag indicating that the item is selected
    /// </summary>
    public bool Selected
    {
      get { return this.selected; }
      set { this.selected = value; }
    }

    private Object data;
    /// <summary>
    /// Get or sert the data container used to store generic information
    /// </summary>
    public Object Data
    {
      get { return this.data; }
      set { this.data = value; }
    }
    #endregion

    #region Protected methods
    /// <summary>
    /// Render
    /// </summary>
    /// <param name="output"></param>
    protected override void Render(HtmlTextWriter output)
    {
      output.AddAttribute(HtmlTextWriterAttribute.Value, HttpUtility.HtmlEncode(this.Value));
      if (this.Selected) output.AddAttribute(HtmlTextWriterAttribute.Selected, "selected");

      output.RenderBeginTag(HtmlTextWriterTag.Option);
      output.WriteEncodedText(this.Text);
      output.RenderEndTag();
    }
    #endregion

    #region ICloneable Members
    /// <summary>
    /// Creates a new instance of a class with the same value as an existing instance
    /// </summary>
    /// <returns></returns>
    public Object Clone()
    {
      OptionGroupItem newOptionGroupItem = new OptionGroupItem();

      newOptionGroupItem.Value = this.Value;
      newOptionGroupItem.Text = this.Text;
      newOptionGroupItem.Selected = this.Selected;
      newOptionGroupItem.OptionGroup = this.OptionGroup;
      newOptionGroupItem.Data = this.Data;

      return newOptionGroupItem;
    }
    #endregion
  }
}
