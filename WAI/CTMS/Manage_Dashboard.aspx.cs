using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Web.UI.HtmlControls;

namespace CTMS
{
    public partial class Manage_Dashboard : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Gridview();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Gridview()
        {
            try
            {
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                if (Request.QueryString["Section"].ToString() == "HOMEPAGETABS")
                {
                    ds = dal.DASHBOARD_ASSIGNING(Action: "GET_List_HOMEPAGETABS",
                    USERID: Session["User_ID"].ToString(),
                    PROJECTID: Session["PROJECTID"].ToString(),
                    TYPENAME: Request.QueryString["Section"].ToString(),
                    TYPE: Request.QueryString["Type"].ToString(),
                    ID: Request.QueryString["TABID"].ToString()
                    );

                    ds1 = dal.DASHBOARD_ASSIGNING(Action: "GET_Dashboard_HOMEPAGETABS",
                    USERID: Session["User_ID"].ToString(),
                    PROJECTID: Session["PROJECTID"].ToString(),
                    TYPENAME: Request.QueryString["Section"].ToString(),
                    TYPE: Request.QueryString["Type"].ToString(),
                    ID: Request.QueryString["TABID"].ToString()
                    );
                }
                else
                {
                    ds = dal.DASHBOARD_ASSIGNING(Action: "GET_List",
                    USERID: Session["User_ID"].ToString(),
                    PROJECTID: Session["PROJECTID"].ToString(),
                    TYPENAME: Request.QueryString["Section"].ToString(),
                    TYPE: Request.QueryString["Type"].ToString()
                    );

                    ds1 = dal.DASHBOARD_ASSIGNING(Action: "GET_Dashboard",
                    USERID: Session["User_ID"].ToString(),
                    PROJECTID: Session["PROJECTID"].ToString(),
                    TYPENAME: Request.QueryString["Section"].ToString(),
                    TYPE: Request.QueryString["Type"].ToString()
                    );
                }

                gvAvailableCharts.DataSource = ds.Tables[0];
                gvAvailableCharts.DataBind();

                gvAddedCharts.DataSource = ds.Tables[1];
                gvAddedCharts.DataBind();

                repeatDashboard.DataSource = ds1.Tables[0];
                repeatDashboard.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Add()
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvAvailableCharts.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvAvailableCharts.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblID = (Label)gvAvailableCharts.Rows[i].FindControl("lblID");
                        Label lblChartId = (Label)gvAvailableCharts.Rows[i].FindControl("lblChartId");
                        string ProjectID = Session["PROJECTID"].ToString();

                        DataSet ds1 = new DataSet();
                        if (Request.QueryString["Section"].ToString() == "HOMEPAGETABS")
                        {
                            ds1 = dal.DASHBOARD_ASSIGNING(Action: "INSERT_HOMEPAGETABS",
                               PROJECTID: Session["PROJECTID"].ToString(),
                               USERGROUPID: Session["UserGroupID"].ToString(),
                               USERID: Session["User_ID"].ToString(),
                               TYPE: Request.QueryString["Type"].ToString(),
                               TYPEID: lblChartId.Text,
                               ENTEREDBY: Session["User_ID"].ToString(),
                               TYPENAME: Request.QueryString["Section"].ToString(),
                               ID: Request.QueryString["TABID"].ToString()
                               );
                        }
                        else
                        {
                            ds1 = dal.DASHBOARD_ASSIGNING(Action: "INSERT",
                                PROJECTID: Session["PROJECTID"].ToString(),
                                USERGROUPID: Session["UserGroupID"].ToString(),
                                USERID: Session["User_ID"].ToString(),
                                TYPE: Request.QueryString["Type"].ToString(),
                                TYPEID: lblChartId.Text,
                                ENTEREDBY: Session["User_ID"].ToString(),
                                TYPENAME: Request.QueryString["Section"].ToString()
                                );
                        }
                    }
                }
                bind_Gridview();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void Remove()
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvAddedCharts.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvAddedCharts.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblID = (Label)gvAddedCharts.Rows[i].FindControl("lblID");
                        Label lblChartId = (Label)gvAddedCharts.Rows[i].FindControl("lblChartId");
                        string ProjectID = Session["PROJECTID"].ToString();

                        DataSet ds1 = new DataSet();

                        if (Request.QueryString["Section"].ToString() == "HOMEPAGETABS")
                        {
                            ds1 = dal.DASHBOARD_ASSIGNING(Action: "Remove_HOMEPAGETABS",
                               ID: lblID.Text
                               );
                        }
                        else
                        {
                            ds1 = dal.DASHBOARD_ASSIGNING(Action: "Remove",
                                ID: lblID.Text
                                );
                        }
                    }
                }
                bind_Gridview();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        protected void lbtnRemove_Click(object sender, EventArgs e)
        {
            Remove();
        }

        public int col = 0;
        protected void repeatDashboard_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    string uName = row["Usercontrol_Name"].ToString();
                    string Method = row["Method"].ToString();
                    string X = row["X"].ToString();
                    string Y = row["Y"].ToString();
                    string Width = row["Width"].ToString();
                    string Height = row["Height"].ToString();

                    HtmlGenericControl divMain = e.Item.FindControl("divMain") as HtmlGenericControl;
                    HtmlGenericControl divBox = (HtmlGenericControl)e.Item.FindControl("divBox");
                    Label lblVal = e.Item.FindControl("lblVal") as Label;
                    Label lblName = e.Item.FindControl("lblName") as Label;
                    Label lblScore = e.Item.FindControl("lblScore") as Label;
                    HiddenField hfIndicID = e.Item.FindControl("hfIndicID") as HiddenField;
                    PlaceHolder placeHolder = e.Item.FindControl("placeHolder") as PlaceHolder;

                    int itemIndex = e.Item.ItemIndex;

                    if (X != null && X.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-x");
                        divMain.Attributes.Add("data-gs-x", X);
                    }
                    if (Y != null && Y.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-y");
                        divMain.Attributes.Add("data-gs-y", Y);
                    }
                    if (Width != null && Width.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-width");
                        divMain.Attributes.Add("data-gs-width", Width);
                    }
                    if (Height != null && Height.ToString() != "")
                    {
                        divMain.Attributes.Remove("data-gs-height");
                        divMain.Attributes.Add("data-gs-height", Height);
                    }

                    if (uName != "")
                    {
                        string usercontrolName = "~/DashBoard_Master/" + uName;
                        UserControl uc = (UserControl)Page.LoadControl(usercontrolName);
                        placeHolder.Controls.Add(uc);
                        divBox.Visible = false;
                    }
                    else
                    {
                        placeHolder.Visible = false;
                        string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };

                        if (col == 13)
                        {
                            col = 0;
                        }
                        divBox.Attributes.Add("class", color[col]);
                        col++;

                        if (Method != "")
                        {
                            ds = dal.Dashboard_SP(Action: Method, Project_ID: Session["PROJECTID"].ToString(), INVID: Session["DASHBOARD_SITE"].ToString(),
                               COUNTRYID: Session["DASHBOARD_COUNTRYID"].ToString(), User_ID: Session["User_ID"].ToString());


                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0][0].ToString() != "")
                                {
                                    lblVal.Text = dt.Rows[0][0].ToString();
                                }
                                else
                                {
                                    lblVal.Text = "0";
                                }

                                lblName.Text = row["Chart_Name"].ToString();
                            }
                            else
                            {
                                lblVal.Text = "0";
                                lblName.Text = row["Chart_Name"].ToString();
                            }
                        }
                        else
                        {
                            lblVal.Text = "0";
                            lblName.Text = row["Chart_Name"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}