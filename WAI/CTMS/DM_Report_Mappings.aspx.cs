using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Web.UI.HtmlControls;
using System.Data;

namespace CTMS
{
    public partial class DM_Report_Mappings : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetStructure();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void GetStructure()
        {
            try
            {
                //lblModuleName.Text = Request.QueryString["MODULENAME"].ToString();
                lblModuleName.Text = "";

                DAL dal;
                dal = new DAL();
                DataSet ds;
                ds = new DataSet();

                ds = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_Report", PROJECTID: Session["PROJECTID"].ToString(), MODULEID: "14");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    repeater_Data.DataSource = ds.Tables[0];
                    repeater_Data.DataBind();
                }
                else
                {
                    repeater_Data.DataSource = null;
                    repeater_Data.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
                throw;
            }
        }

        protected void repeater_Data_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DAL dal;
                    dal = new DAL();

                    DataRowView dr = e.Item.DataItem as DataRowView;

                    string ID = dr["ID"].ToString();
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    string CLASS = dr["CLASS"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();
                    string DEFULTVAL = dr["DEFAULTVAL"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";
                    }
                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Font.Underline = true;
                    }
                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Item.FindControl("TXT_FIELD");
                        btnEdit.Visible = true;
                        //btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (MAXLEN != "" && MAXLEN != "0")
                        {
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);
                        }
                        if (READYN == "True")
                        {
                            btnEdit.ReadOnly = true;
                        }
                        if (DEFULTVAL != "")
                        {
                            btnEdit.Text = DEFULTVAL;
                        }
                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Attributes.Add("style", "width: 150px;");
                        }
                        if (CLASS.Contains("txtTime"))
                        {
                            btnEdit.Attributes.Add("onchange", "ValidTime(this)");
                        }
                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }
                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }
                    }
                    //if (CONTROLTYPE == )
                    //{
                    //    DropDownList btnEdit = (DropDownList)e.Item.FindControl("DRP_FIELD");
                    //    btnEdit.Visible = true;
                    //    //btnEdit.ID = VARIABLENAME;
                    //    btnEdit.CssClass = CLASS;
                    //    btnEdit.ToolTip = FIELDNAME;

                    //    DataSet ds;
                    //    ds = new DataSet();
                    //    ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                    //    btnEdit.DataSource = ds;
                    //    btnEdit.DataTextField = "TEXT";
                    //    btnEdit.DataValueField = "VALUE";

                    //    btnEdit.DataBind();

                    //    if (AnsColor != "")
                    //    {
                    //        btnEdit.Style.Add("color", AnsColor);
                    //    }
                    //}
                    if (CONTROLTYPE == "CHECKBOX")
                    {

                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        Repeater repeat_CHK = (Repeater)e.Item.FindControl("repeat_CHK");
                        repeat_CHK.Visible = true;

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        //DataTable newDataTable = dt.AsEnumerable().Where(r => !ListLinkedIds.Contains(r.Field<string>("IDCOLUMN"))).CopyToDataTable();

                        Repeater repeat_RAD = (Repeater)e.Item.FindControl("repeat_RAD");
                        repeat_RAD.Visible = true;

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    Repeater repeater_Data1 = e.Item.FindControl("repeater_Data1") as Repeater;
                    DataSet ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_Report", PROJECTID: Session["PROJECTID"].ToString(), MODULEID: "14", ID: ID);
                    repeater_Data1.DataSource = ds1.Tables[0];
                    repeater_Data1.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeater_Data1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DAL dal;
                    dal = new DAL();

                    DataRowView dr = e.Item.DataItem as DataRowView;

                    string ID = dr["ID"].ToString();
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    string CLASS = dr["CLASS"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();
                    string DEFULTVAL = dr["DEFAULTVAL"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";
                    }
                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Font.Underline = true;
                    }
                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Item.FindControl("TXT_FIELD");
                        btnEdit.Visible = true;
                        //btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (MAXLEN != "" && MAXLEN != "0")
                        {
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);
                        }
                        if (READYN == "True")
                        {
                            btnEdit.ReadOnly = true;
                        }
                        if (DEFULTVAL != "")
                        {
                            btnEdit.Text = DEFULTVAL;
                        }
                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Attributes.Add("style", "width: 150px;");
                        }
                        if (CLASS.Contains("txtTime"))
                        {
                            btnEdit.Attributes.Add("onchange", "ValidTime(this)");
                        }
                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }
                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }
                    }
                    //if (CONTROLTYPE == )
                    //{
                    //    DropDownList btnEdit = (DropDownList)e.Item.FindControl("DRP_FIELD");
                    //    btnEdit.Visible = true;
                    //    //btnEdit.ID = VARIABLENAME;
                    //    btnEdit.CssClass = CLASS;
                    //    btnEdit.ToolTip = FIELDNAME;

                    //    DataSet ds;
                    //    ds = new DataSet();
                    //    ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                    //    btnEdit.DataSource = ds;
                    //    btnEdit.DataTextField = "TEXT";
                    //    btnEdit.DataValueField = "VALUE";

                    //    btnEdit.DataBind();

                    //    if (AnsColor != "")
                    //    {
                    //        btnEdit.Style.Add("color", AnsColor);
                    //    }
                    //}
                    if (CONTROLTYPE == "CHECKBOX")
                    {

                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        Repeater repeat_CHK = (Repeater)e.Item.FindControl("repeat_CHK");
                        repeat_CHK.Visible = true;

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        //DataTable newDataTable = dt.AsEnumerable().Where(r => !ListLinkedIds.Contains(r.Field<string>("IDCOLUMN"))).CopyToDataTable();

                        Repeater repeat_RAD = (Repeater)e.Item.FindControl("repeat_RAD");
                        repeat_RAD.Visible = true;

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    Repeater repeater_Data2 = e.Item.FindControl("repeater_Data2") as Repeater;
                    DataSet ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_Report", PROJECTID: Session["PROJECTID"].ToString(), MODULEID: "14", ID: ID);
                    repeater_Data2.DataSource = ds1.Tables[0];
                    repeater_Data2.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeater_Data2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DAL dal;
                    dal = new DAL();

                    DataRowView dr = e.Item.DataItem as DataRowView;

                    string ID = dr["ID"].ToString();
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    string CLASS = dr["CLASS"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();
                    string DEFULTVAL = dr["DEFAULTVAL"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";
                    }
                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Font.Underline = true;
                    }
                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Item.FindControl("TXT_FIELD");
                        btnEdit.Visible = true;
                        //btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (MAXLEN != "" && MAXLEN != "0")
                        {
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);
                        }
                        if (READYN == "True")
                        {
                            btnEdit.ReadOnly = true;
                        }
                        if (DEFULTVAL != "")
                        {
                            btnEdit.Text = DEFULTVAL;
                        }
                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Attributes.Add("style", "width: 150px;");
                        }
                        if (CLASS.Contains("txtTime"))
                        {
                            btnEdit.Attributes.Add("onchange", "ValidTime(this)");
                        }
                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }
                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }
                    }
                    //if (CONTROLTYPE == )
                    //{
                    //    DropDownList btnEdit = (DropDownList)e.Item.FindControl("DRP_FIELD");
                    //    btnEdit.Visible = true;
                    //    //btnEdit.ID = VARIABLENAME;
                    //    btnEdit.CssClass = CLASS;
                    //    btnEdit.ToolTip = FIELDNAME;

                    //    DataSet ds;
                    //    ds = new DataSet();
                    //    ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                    //    btnEdit.DataSource = ds;
                    //    btnEdit.DataTextField = "TEXT";
                    //    btnEdit.DataValueField = "VALUE";

                    //    btnEdit.DataBind();

                    //    if (AnsColor != "")
                    //    {
                    //        btnEdit.Style.Add("color", AnsColor);
                    //    }
                    //}
                    if (CONTROLTYPE == "CHECKBOX")
                    {

                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        Repeater repeat_CHK = (Repeater)e.Item.FindControl("repeat_CHK");
                        repeat_CHK.Visible = true;

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        //DataTable newDataTable = dt.AsEnumerable().Where(r => !ListLinkedIds.Contains(r.Field<string>("IDCOLUMN"))).CopyToDataTable();

                        Repeater repeat_RAD = (Repeater)e.Item.FindControl("repeat_RAD");
                        repeat_RAD.Visible = true;

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    Repeater repeater_Data3 = e.Item.FindControl("repeater_Data3") as Repeater;
                    DataSet ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_Report", PROJECTID: Session["PROJECTID"].ToString(), MODULEID: "14", ID: ID);
                    repeater_Data3.DataSource = ds1.Tables[0];
                    repeater_Data3.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeater_Data3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DAL dal;
                    dal = new DAL();

                    DataRowView dr = e.Item.DataItem as DataRowView;

                    string ID = dr["ID"].ToString();
                    string CONTROLTYPE = dr["CONTROLTYPE"].ToString();
                    string VARIABLENAME = dr["VARIABLENAME"].ToString();
                    string FIELDNAME = dr["FIELDNAME"].ToString();
                    string CLASS = dr["CLASS"].ToString();

                    string MAXLEN = dr["MAXLEN"].ToString();
                    string UPPERCASE = dr["UPPERCASE"].ToString();
                    string DEFULTVAL = dr["DEFAULTVAL"].ToString();
                    string BOLDYN = dr["BOLDYN"].ToString();
                    string UNLNYN = dr["UNLNYN"].ToString();
                    string READYN = dr["READYN"].ToString();
                    string MULTILINEYN = dr["MULTILINEYN"].ToString();

                    string FieldColor = dr["FieldColor"].ToString();
                    string AnsColor = dr["AnsColor"].ToString();

                    if (BOLDYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.CssClass = lblField.CssClass + " fontbold";
                    }
                    if (UNLNYN == "True")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Font.Underline = true;
                    }
                    if (CONTROLTYPE == "TEXTBOX")
                    {
                        TextBox btnEdit = (TextBox)e.Item.FindControl("TXT_FIELD");
                        btnEdit.Visible = true;
                        //btnEdit.ID = VARIABLENAME;
                        btnEdit.CssClass = CLASS;
                        btnEdit.ToolTip = FIELDNAME;

                        if (MAXLEN != "" && MAXLEN != "0")
                        {
                            btnEdit.Attributes.Add("MaxLength", MAXLEN);
                        }
                        if (READYN == "True")
                        {
                            btnEdit.ReadOnly = true;
                        }
                        if (DEFULTVAL != "")
                        {
                            btnEdit.Text = DEFULTVAL;
                        }
                        if (MULTILINEYN == "True")
                        {
                            btnEdit.TextMode = TextBoxMode.MultiLine;
                            btnEdit.Attributes.Add("style", "width: 150px;");
                        }
                        if (CLASS.Contains("txtTime"))
                        {
                            btnEdit.Attributes.Add("onchange", "ValidTime(this)");
                        }
                        if (UPPERCASE == "True")
                        {
                            btnEdit.CssClass = btnEdit.CssClass + " txtuppercase";
                        }
                        if (AnsColor != "")
                        {
                            btnEdit.Style.Add("color", AnsColor);
                        }
                    }
                    //if (CONTROLTYPE == )
                    //{
                    //    DropDownList btnEdit = (DropDownList)e.Item.FindControl("DRP_FIELD");
                    //    btnEdit.Visible = true;
                    //    //btnEdit.ID = VARIABLENAME;
                    //    btnEdit.CssClass = CLASS;
                    //    btnEdit.ToolTip = FIELDNAME;

                    //    DataSet ds;
                    //    ds = new DataSet();
                    //    ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);
                    //    btnEdit.DataSource = ds;
                    //    btnEdit.DataTextField = "TEXT";
                    //    btnEdit.DataValueField = "VALUE";

                    //    btnEdit.DataBind();

                    //    if (AnsColor != "")
                    //    {
                    //        btnEdit.Style.Add("color", AnsColor);
                    //    }
                    //}
                    if (CONTROLTYPE == "CHECKBOX")
                    {

                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        Repeater repeat_CHK = (Repeater)e.Item.FindControl("repeat_CHK");
                        repeat_CHK.Visible = true;

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_CHK.DataSource = ds;
                        repeat_CHK.DataBind();
                    }
                    if (CONTROLTYPE == "RADIOBUTTON" || CONTROLTYPE == "DROPDOWN")
                    {
                        if (AnsColor == "")
                        {
                            AnsColor = "#000000";
                        }

                        //DataTable newDataTable = dt.AsEnumerable().Where(r => !ListLinkedIds.Contains(r.Field<string>("IDCOLUMN"))).CopyToDataTable();

                        Repeater repeat_RAD = (Repeater)e.Item.FindControl("repeat_RAD");
                        repeat_RAD.Visible = true;

                        DataSet ds;
                        ds = new DataSet();

                        ds = dal.GetDropDownData(Action: "DM_DM_DRP_DWN_MASTER", VariableName: VARIABLENAME);

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("color", typeof(string));
                        newColumn.DefaultValue = AnsColor;
                        ds.Tables[0].Columns.Add(newColumn);

                        repeat_RAD.DataSource = ds;
                        repeat_RAD.DataBind();
                    }

                    if (FieldColor != "")
                    {
                        Label lblField = (Label)e.Item.FindControl("lblFieldName");
                        lblField.Style.Add("color", FieldColor);
                    }

                    //Repeater repeater_Data1 = e.Item.FindControl("repeater_Data1") as Repeater;
                    //DataSet ds1 = dal.DM_ADD_UPDATE(ACTION: "GET_MAPPING_CHILD_Report", PROJECTID: Session["PROJECTID"].ToString(), MODULEID: "14", ID: ID);
                    //repeater_Data1.DataSource = ds1.Tables[0];
                    //repeater_Data1.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}