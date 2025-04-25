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
    public partial class RM_Mng_RiskIndicators : System.Web.UI.Page
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
                string Action = "";

                if (Request.QueryString["User"] != null)
                {
                    Action = "GET_Mng_RiskIndicator_User";
                }
                else
                {
                    Action = "GET_Mng_RiskIndicator";
                }

                DataSet ds = dal.Risk_Indicator_SP(Action: Action, Result: Session["User_ID"].ToString());

                gvAvailableCharts.DataSource = ds.Tables[0];
                gvAvailableCharts.DataBind();

                gvAddedCharts.DataSource = ds.Tables[1];
                gvAddedCharts.DataBind();

                repeatDashboard.DataSource = ds.Tables[1];
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
                string Action = "";

                if (Request.QueryString["User"] != null)
                {
                    Action = "Activate_RiskIndicator_User";
                }
                else
                {
                    Action = "Activate_RiskIndicator";
                }

                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvAvailableCharts.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvAvailableCharts.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblID = (Label)gvAvailableCharts.Rows[i].FindControl("lblID");
                        dal.Risk_Indicator_SP(Action: Action, ID: lblID.Text, Result: Session["User_ID"].ToString());
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
                string Action = "";

                if (Request.QueryString["User"] != null)
                {
                    Action = "Deactivate_RiskIndicator_User";
                }
                else
                {
                    Action = "Deactivate_RiskIndicator";
                }

                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvAddedCharts.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvAddedCharts.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblID = (Label)gvAddedCharts.Rows[i].FindControl("lblID");
                        dal.Risk_Indicator_SP(Action: Action, ID: lblID.Text, Result: Session["User_ID"].ToString());
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
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    string X = row["X"].ToString();
                    string Y = row["Y"].ToString();
                    string Width = row["Width"].ToString();
                    string Height = row["Height"].ToString();

                    HtmlGenericControl divBox = (HtmlGenericControl)e.Item.FindControl("divBox");
                    HtmlGenericControl divMain = e.Item.FindControl("divMain") as HtmlGenericControl;
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

                    int itemIndex = e.Item.ItemIndex;
                    string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };

                    if (col == 13)
                    {
                        col = 0;
                    }
                    divBox.Attributes.Add("class", color[col]);
                    col++;

                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }
    }
}