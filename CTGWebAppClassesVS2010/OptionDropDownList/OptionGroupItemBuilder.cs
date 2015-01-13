using System;
using System.Web.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OptionDropDownList
{
  /// <summary>
  /// Supports the page parser in building a control and the child controls it contains
  /// 
  /// By default, every control on a page is associated with a default ControlBuilder class. 
  /// During parsing, the ASP.NET page framework builds a tree of ControlBuilder objects 
  /// corresponding to the tree of controls for the page. The ControlBuilder tree is then used 
  /// to generate page code to create the control tree. In addition to child controls, 
  /// the ControlBuilder defines the behavior of how the content within control tags is parsed. 
  /// You can override this default behavior by defining your own custom control builder class. 
  /// This is done by applying a ControlBuilderAttribute attribute to your control builder 
  /// class as follows:
  /// [ControlBuilderAttribute(typeof(ControlBuilderType))] 
  /// </summary>
  class OptionGroupItemBuilder : ControlBuilder
  {
    /// <summary>
    /// GetChildControlType
    /// </summary>
    /// <param name="tagName"></param>
    /// <param name="attributes"></param>
    /// <returns></returns>
    public override Type GetChildControlType(String tagName, IDictionary attributes)
    {
      if (tagName.Contains("OptionGroupItem")) return typeof(OptionGroupItem);
      else return null;
    }

    /// <summary>
    /// AppendLiteralString
    /// </summary>
    /// <param name="s"></param>
    public override void AppendLiteralString(string s)
    {
      // Ignores literals between rows
    }
  }
}
