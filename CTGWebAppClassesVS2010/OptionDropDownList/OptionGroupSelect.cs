using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Drawing;

namespace OptionDropDownList
{
  [ToolboxBitmap(typeof(System.Web.UI.WebControls.DropDownList))]
  [DefaultProperty("SelectedValue")]
  [DefaultEvent("ValueChanged")]
  [ToolboxItem(true)]
  [ToolboxData("<{0}:OptionGroupSelect runat=server></{0}:OptionGroupSelect>")]
  [Description("Create a DropDownList control with option group HTML property")]
  [ControlBuilderAttribute(typeof(OptionGroupItemBuilder))]
  [Designer("OptionDropDownList.OptionGroupSelectControlDesigner")]
  [ParseChildren(false)]
  [ValidationProperty("SelectedValue")]
  [ControlValueProperty("SelectedValue", null)]
  public class OptionGroupSelect : DataBoundControl, INamingContainer, IPostBackDataHandler
  {
    #region Control's Events
    /// <summary>
    /// ValueChanged event
    /// </summary>
    public event EventHandler ValueChanged;
    #endregion

    #region Control's Member variables
    private const string OPTGROUP = "optgroup";
    private const string LABEL = "label";
    private bool autoPostBack = false;
    private bool appendDataBoundItems = true;
    private string optionGroupField = null;
    private string dataTextField = null;
    private string dataValueField = null;
    private string selectedValue = null;
    #endregion region

    #region Constructors
    /// <summary>
    /// Constructor
    /// </summary>
    public OptionGroupSelect()
    {
    }
    #endregion

    #region Properties
    /// <summary>
    /// Get or set OptionGroupField property
    /// </summary>
    [Category("Data")]
    public String OptionGroupField
    {
      get { return this.optionGroupField; }
      set { this.optionGroupField = value; }
    }

    /// <summary>
    /// Get or set DataValueField property
    /// </summary>
    [Category("Data")]
    public String DataTextField
    {
      get { return this.dataTextField; }
      set { this.dataTextField = value; }
    }

    /// <summary>
    /// Get or set DataValueField property
    /// </summary>
    [Category("Data")]
    public String DataValueField
    {
      get { return this.dataValueField; }
      set { this.dataValueField = value; }
    }

    /// <summary>
    /// Return the list of OptionGroupItem
    /// </summary>   
    [Bindable(true)]
    [Category("Data")]
    [RefreshProperties(RefreshProperties.All)]
    public ControlCollection Items
    {
      get { return this.Controls; }
    }

    /// <summary>
    /// Set or Get the AutoPostBack property
    /// </summary>
    [Category("Behaviour")]
    public bool AutoPostBack
    {
      get { return this.autoPostBack; }
      set { this.autoPostBack = value; }
    }

    /// <summary>
    /// Set or get the AppendDataBoundItems property
    /// </summary>
    [Category("Behaviour")]
    public bool AppendDataBoundItems
    {
      get { return this.appendDataBoundItems; }
      set { this.appendDataBoundItems = value; }
    }

    /// <summary>
    /// Get or set the selected value
    /// </summary>
    [DefaultValue(null)]
    public String SelectedValue
    {
      get { return this.selectedValue; }
      set { this.selectedValue = value; }
    }

    /// <summary>
    /// Get the selected item
    /// </summary>
    /// <returns></returns>
    public OptionGroupItem SelectedItem
    {
      get
      {
        if (!String.IsNullOrEmpty(this.SelectedValue))
        {
          OptionGroupItem currentItem = null;
          for (int i = 0; i < this.Items.Count; i++)
          {
            currentItem = this.Items[i] as OptionGroupItem;
            if (currentItem == null) continue;
            if (currentItem.Value == this.SelectedValue) return currentItem;
          }
        }

        return null;
      }
    }
    #endregion region

    #region Protected / Public methods
    /// <summary>
    /// Custom server controls that use control state must call the 
    /// RegisterRequiresControlState method on each request because registration for 
    /// control state is not carried over from request to request during a postback event
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
      this.Page.RegisterRequiresControlState(this);
      base.OnInit(e);
    }

    /// <summary>
    /// Retrieve the loading preview selected value
    /// </summary>
    /// <param name="savedState"></param>
    protected override void LoadControlState(Object savedState)
    {
      if (savedState != null)
      {
        Object[] ctrlState = (Object[])savedState;
        base.LoadControlState(ctrlState[0]);
        this.SelectedValue = (String)ctrlState[1];
      }
    }

    /// <summary>
    /// Restores view-state information from a previous page request that was 
    /// saved by the SaveViewState method
    /// </summary>
    /// <param name="savedState"></param>
    protected override void LoadViewState(Object savedState)
    {
      if (savedState != null)
      {
        Object[] viewStateDatas = (Object[])savedState;
        base.LoadViewState(viewStateDatas[0]);

        this.Items.Clear();

        List<String[]> itemDatas = (List<String[]>)viewStateDatas[1];
        foreach (String[] itemData in itemDatas)
        {
          OptionGroupItem item = new OptionGroupItem(itemData[0], itemData[1], itemData[2]);
          this.Items.Add(item);
        }
      }
    }

    /// <summary>
    /// To interact with postback data, your control must be able to access the data. To do this, it 
    /// implements the System.Web.IPostBackDataHandler interface. This interface allows your control 
    /// to examine the form data that is passed back to the server during the postback.
    /// 
    /// The IPostBackDataHandler interface requires that you implement two methods: LoadPostData and
    /// RaisePostBackDataChangedEvent. 
    /// 
    /// The LoadPostData method is called for all server controls on the
    /// page that have postback data. If a control does not have any postback data, the method is not called;
    /// however, you can explicitly ask for the method to be called by using the RegisterRequiresPostBack
    /// method.
    /// </summary>
    /// <param name="postDataKey"></param>
    /// <param name="postCollection"></param>
    /// <returns></returns>
    public virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
    {
      String previousValue = this.SelectedValue;
      String currentValue = postCollection[this.ClientID];

      if (previousValue == null || !previousValue.Equals(currentValue))
      {
        this.SelectedValue = currentValue;
        return true;
      }

      return false;
    }

    /// <summary>
    /// OnLoad handler
    /// </summary>
    /// <param name="e"></param>
    protected override void OnLoad(EventArgs e)
    {
    }

    /// <summary>
    /// Raise PostDataChanged Event due to return true of LoadPostData method
    /// </summary>
    public virtual void RaisePostDataChangedEvent()
    {
      this.OnValueChanged(EventArgs.Empty);
    }

    /// <summary>
    /// OnPreRender handler
    /// </summary>
    /// <param name="e"></param>
    /// <remarks>
    /// Page.RegisterRequiresPostBack Method 
    /// Registers a control as one that requires postback handling when the page is posted back to the server.
    /// The control to be registered must implement the IPostBackDataHandler interface or an HttpException 
    /// is raised. When implemented by a control, the IPostBackDataHandler interface enables handling of 
    /// post back data and raising of any post back data changed events.
    /// </remarks>     
    protected override void OnPreRender(EventArgs e)
    {
      this.Page.RegisterRequiresPostBack(this);
      this.SetSelectedValue();
      base.OnPreRender(e);
    }

    /// <summary>
    /// Save the selected value in control state
    /// </summary>
    /// <returns></returns>
    protected override Object SaveControlState()
    {
      if (String.IsNullOrEmpty(this.SelectedValue) && this.Items.Count > 0)
        this.SelectedValue = ((OptionGroupItem)this.Items[0]).Value;

      Object[] ctrlState = new Object[2];
      ctrlState[0] = base.SaveControlState();
      ctrlState[1] = this.SelectedValue;
      return ctrlState;
    }

    /// <summary>
    /// Saves any server control view-state changes that have occurred since the time 
    /// the page was posted back to the server
    /// </summary>
    /// <returns></returns>
    protected override object SaveViewState()
    {
      List<String[]> itemDatas = new List<String[]>();
      foreach (OptionGroupItem item in this.Items)
      {
        String[] itemValues = new String[3];
        itemValues[0] = item.Value;
        itemValues[1] = item.Text;
        itemValues[2] = item.OptionGroup;
        itemDatas.Add(itemValues);
      }

      object[] viewStateDatas = new object[2];
      viewStateDatas[0] = base.SaveViewState();
      viewStateDatas[1] = itemDatas;

      return viewStateDatas;
    }

    /// <summary>
    /// RenderBeginTag
    /// </summary>
    /// <param name="output"></param>
    /// <remarks>
    /// Because override the RenderBeginTag, every property that has to be render ad HTML property must
    /// be manage in the WriteStyleAttributes method.
    /// </remarks>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
      writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID);

      IEnumerator keys = this.Style.Keys.GetEnumerator();
      while (keys.MoveNext())
      {
        String styleName = (String)keys.Current;
        String styleValue = this.Style[styleName];
        writer.AddStyleAttribute(styleName, styleValue);
      }
            
      // If AutoPostBack propert is set, attach the js code to perfor the post back
      if (this.autoPostBack) writer.AddAttribute(HtmlTextWriterAttribute.Onchange, this.GetClientScriptAutoPostBack());
      writer.RenderBeginTag(HtmlTextWriterTag.Select);

      base.RenderBeginTag(writer);
    }

    /// <summary>
    /// RenderContents
    /// </summary>
    /// <param name="writer"></param>
    protected override void RenderContents(HtmlTextWriter writer)
    {
      this.RenderDropDownContent(writer);
    }

    /// <summary>
    /// RenderEndTag
    /// </summary>
    /// <param name="writer"></param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
      writer.RenderEndTag();
      base.RenderEndTag(writer);
    }

    /// <summary>
    /// Initiate Data Retrieval
    /// Data retrieval is initiated within an override of the PerformSelect method inherited by your 
    /// control's base data-bound control. Within this override you call to retrieve the data and 
    /// specify a callback method that will handle the data once it is returned.
    /// 
    /// To retrieve data, complete the following tasks in the overridden PerformSelect method:
    /// 1. Determine if the page developer used the DataSource property or the DataSourceID property 
    ///    to set the data to be bound to the control. This is done by verifying the IsBoundUsingDataSourceID 
    ///    property. For example, a false setting for the IsBoundUsingDataSourceID property indicates 
    ///    that the DataSource property was used to specify the data source.
    /// 2. If the page developer set the DataSource property, then an extra step is required: Call 
    ///    the OnDataBinding method. 
    /// 3. Call the GetData method to retrieve the DataSourceView object associated with the 
    ///    data-bound control.
    /// 4. Call the Select method of the retrieved DataSourceView to initiate data retrieval and 
    ///    specify the callback method that will handle the retrieved data. The Select method operates 
    ///    asynchronously, so other processing is allowed while the data is being retrieved.
    /// 5. Indicate the completion of the PerformSelect tasks by setting the RequiresDataBinding 
    ///    property to false and then calling the MarkAsDataBound method.
    /// 6. Raise the OnDataBound event.
    /// </summary>
    protected override void PerformSelect()
    {
      if (!this.IsBoundUsingDataSourceID) this.OnDataBinding(EventArgs.Empty);

      // The data is retrive by DataMember
      DataSourceView dsView = this.GetData();
      this.GetData().Select(this.CreateDataSourceSelectArguments(), this.OnDataSourceViewCallback);

      this.RequiresDataBinding = false;
      this.MarkAsDataBound();

      this.OnDataBound(EventArgs.Empty);
    }

    /// <summary>
    /// <b>Create the UI Objects Representing the Data</b>
    /// Within an override of the PerformDataBinding method, you create the child controls that will 
    /// represent the data. The data collection is enumerated, and the child controls are created and 
    /// their properties set based on each data item. By adding the new child controls to the control's 
    /// Controls collection, the child controls will be rendered for you. The control hierarchy 
    /// renders during the control's inherited Render method. You might choose to override the 
    /// Render method to do special rendering required by your custom control, such as including 
    /// additional HTML elements or special rendering for display during design mode.
    /// To create the UI objects representing the data, override the PerformDataBinding method 
    /// and complete the following tasks:
    /// 1. Call the PerformDataBinding method to allow any other code relying on this method to execute.
    /// 2. Enumerate through the data collection and create any child controls that will represent 
    ///    the data in the UI display. Add each child control to the control's collection by calling 
    ///    the control's Add method.
    /// 3. The following code example illustrates overriding the PerformDataBinding method. 
    ///    The PerformDataBinding method is called to allow any other code relying on this method to execute. 
    ///    The data collection is enumerated and child controls are created to represent the data in 
    ///    the UI display.
    /// </summary>
    /// <param name="retrievedData"></param>
    protected override void PerformDataBinding(IEnumerable retrievedData)
    {
      base.PerformDataBinding(retrievedData);

      // If appendDataBoundItems = false remove the item added from design or from view state
      if (!this.appendDataBoundItems) this.Items.Clear();

      if (retrievedData != null)
      {
        String dataText = String.Empty;
        String dataValue = String.Empty;
        String dataOptionGroup = String.Empty;

        foreach (object dataItem in retrievedData)
        {
          if (dataItem is System.Data.DataRowView)
          {
            // If the DataTextField was specified get the data
            // from that field, otherwise get the data from the first field. 
            if (this.DataTextField != null && this.DataTextField.Length > 0)
            {
              dataText = DataBinder.GetPropertyValue(dataItem, this.DataTextField, null);
              if (this.DataValueField != null && this.DataValueField.Length > 0)
                dataValue = DataBinder.GetPropertyValue(dataItem, this.DataValueField, null);
              else
                dataValue = dataText;

              if (this.OptionGroupField != null && this.OptionGroupField.Length > 0)
                dataOptionGroup = DataBinder.GetPropertyValue(dataItem, this.OptionGroupField, null);
            }
            else
            {
              PropertyDescriptorCollection props = TypeDescriptor.GetProperties(dataItem);
              if (props.Count >= 1)
                if (null != props[0].GetValue(dataItem)) dataText = props[0].GetValue(dataItem).ToString();
            }

            OptionGroupItem item = new OptionGroupItem(dataValue, dataText, dataOptionGroup);
            this.Items.Add(item);
          }
          else if (dataItem is OptionGroupItem)
          {
            OptionGroupItem item = ((OptionGroupItem)dataItem).Clone() as OptionGroupItem;
            this.Items.Add(item);
          }
          else
          {
            throw new ApplicationException("DataBind type not supported");
          }
        }
      }
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Handle the Retrieved Data
    /// Create your own callback method to accept the retrieved data. This is the callback method 
    /// you specified when the Select method was called within the override of the PerformSelect method. 
    /// The callback method is required to contain only a single parameter of the type IEnumerable. 
    /// In your callback method you can do any processing of the returned data, if required by your control. 
    /// As a last step, call the PerformDataBinding method.
    /// </summary>
    /// <param name="retrievedData"></param>
    private void OnDataSourceViewCallback(IEnumerable retrievedData)
    {
      // Call OnDataBinding only if it has not already been called in the PerformSelect method.
      if (this.IsBoundUsingDataSourceID) this.OnDataBinding(EventArgs.Empty);

      // The PerformDataBinding method binds the data in the retrievedData 
      // collection to elements of the data-bound control.
      this.PerformDataBinding(retrievedData);
    }

    /// <summary>
    /// If the this.Selected value is set use it to set the Selected=true for the Item and 
    /// set others OptionGroupItem's Selected to false so that exists only one Item as Selected = true in render HTML.
    /// Otherwise, set the OptionGroupItem' Selected property as the Selected Item in render HTML.
    /// </summary>
    private void SetSelectedValue()
    {
      if (this.SelectedValue != null)
      {
        foreach (OptionGroupItem item in this.Items)
        {
          if (this.SelectedValue == item.Value) item.Selected = true;
          else item.Selected = false;
        }
      }
      else
      {
        foreach (OptionGroupItem item in this.Items)
        {
          if (item.Selected) this.SelectedValue = item.Value;
          break;
        }
      }      
    }

    /// <summary>
    /// Render the DropDown Content
    /// </summary>
    /// <param name="output"></param>
    private void RenderDropDownContent(HtmlTextWriter output)
    {
      List<String> renderedOptionGroups = new List<String>();

      // Perform the grouping of items with the same option group
      Dictionary<String, List<OptionGroupItem>> renderingOptionGroups = this.PrepareItems();
      foreach (String currentOptionGroup in renderingOptionGroups.Keys)
      {
        List<OptionGroupItem> listItems = renderingOptionGroups[currentOptionGroup];
        foreach (OptionGroupItem listItem in listItems)
        {
          if (renderedOptionGroups.Contains(currentOptionGroup))
          {
            listItem.RenderControl(output);
          }
          else
          {
            if (renderedOptionGroups.Count > 0) this.RenderOptionGroupEndTag(output);
            if (!String.IsNullOrEmpty(currentOptionGroup))
            {
              this.RenderOptionGroupBeginTag(currentOptionGroup, output);
              renderedOptionGroups.Add(currentOptionGroup);
            }

            listItem.RenderControl(output);
          }
        }
      }

      if (renderedOptionGroups.Count > 0) this.RenderOptionGroupEndTag(output);
    }

    /// <summary>
    /// Raise the ValueChanged event
    /// </summary>
    /// <param name="args"></param>
    private void OnValueChanged(EventArgs args)
    {
      if (this.ValueChanged != null) this.ValueChanged(this, args);
    }

    /// <summary>
    /// Group the dropdown items by option value
    /// </summary>
    /// <returns></returns>
    private Dictionary<String, List<OptionGroupItem>> PrepareItems()
    {
      OptionGroupItem currentItem = null;
      Dictionary<String, List<OptionGroupItem>> renderedOptionGroups =
        new Dictionary<String, List<OptionGroupItem>>();

      for (int i = 0; i < this.Items.Count; i++)
      {
        currentItem = this.Items[i] as OptionGroupItem;

        if (currentItem == null) continue;

        if (String.IsNullOrEmpty(currentItem.OptionGroup)) currentItem.OptionGroup = String.Empty;
        if (!renderedOptionGroups.ContainsKey(currentItem.OptionGroup))
          renderedOptionGroups.Add(currentItem.OptionGroup, new List<OptionGroupItem>());
        renderedOptionGroups[currentItem.OptionGroup].Add(currentItem);
      }

      return renderedOptionGroups;
    }

    /// <summary>
    /// Render OptionGroup begin tag
    /// </summary>
    /// <param name="name"></param>
    /// <param name="writer"></param>
    private void RenderOptionGroupBeginTag(String name, HtmlTextWriter writer)
    {
      writer.WriteBeginTag(OptionGroupSelect.OPTGROUP);
      writer.WriteAttribute(OptionGroupSelect.LABEL, HttpUtility.HtmlEncode(name));
      writer.Write(HtmlTextWriter.TagRightChar);
      writer.WriteLine();
    }

    /// <summary>
    /// Rende rOptionGroup End Tag
    /// </summary>
    /// <param name="writer"></param>
    private void RenderOptionGroupEndTag(HtmlTextWriter writer)
    {
      writer.WriteEndTag(OptionGroupSelect.OPTGROUP);
      writer.WriteLine();
    }

    /// <summary>
    /// Get Client Script AutoPostBack reference to implement the AutoPostBack property
    /// </summary>
    /// <returns></returns>
    private String GetClientScriptAutoPostBack()
    {
      PostBackOptions p = new PostBackOptions(this);
      return this.Page.ClientScript.GetPostBackEventReference(p);
    }
    #endregion
  }
}