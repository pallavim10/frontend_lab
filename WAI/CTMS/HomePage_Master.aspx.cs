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
    public partial class HomePage_Master : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            GETCALENDAR();
            Get_Dashboard();
        }

        protected void GETCALENDAR()
        {
            try
            {
                string pieinfonsclc = "";

                DataSet ds = dal.Dashboard_SP(Action: "GET_Calendar_Data", User_ID: Session["User_ID"].ToString(), Project_ID: Session["PROJECTID"].ToString());
                DataTable dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };

                    if (col == 13)
                    {
                        col = 0;
                    }

                    pieinfonsclc += "{ 'title': '" + dt.Rows[i]["title"].ToString() + "', 'start': new Date('" + dt.Rows[i]["start"].ToString() + "'),'backgroundColor' :'" + dt.Rows[i]["COLOR"].ToString() + "','borderColor' : '" + dt.Rows[i]["BORDERCOLOR"].ToString() + "','allDay' : 'true','description':'" + dt.Rows[i]["Tooltip"].ToString() + "'},";
                    col++;
                }

                hfCode.Value = "[" + pieinfonsclc.TrimEnd(',') + "]";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void Get_Dashboard()
        {
            try
            {
                DataSet ds1 = dal.DASHBOARD_ASSIGNING(Action: "GETDASHBOARD");

                repeatTiles.DataSource = ds1.Tables[0];
                repeatTiles.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public int col = 0;
        protected void repeatTiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

                    HtmlGenericControl divMain = e.Item.FindControl("divMain") as HtmlGenericControl;
                    HtmlGenericControl divBox = (HtmlGenericControl)e.Item.FindControl("divBox");
                    Label lblVal = e.Item.FindControl("lblVal") as Label;
                    Label lblName = e.Item.FindControl("lblName") as Label;
                    Label lblScore = e.Item.FindControl("lblScore") as Label;
                    HiddenField hfIndicID = e.Item.FindControl("hfIndicID") as HiddenField;
                    PlaceHolder placeHolder = e.Item.FindControl("placeHolder") as PlaceHolder;

                    if (uName != "")
                    {
                        string usercontrolName = "~/Dashboard Charts/" + uName;
                        UserControl uc = (UserControl)Page.LoadControl(usercontrolName);
                        placeHolder.Controls.Add(uc);
                        divBox.Visible = false;
                    }
                    else
                    {
                        string[] color = { "small-box bg-red", "small-box bg-yellow", "small-box bg-aqua", "small-box bg-blue", "small-box bg-light-blue", "small-box bg-green", "small-box bg-navy", "small-box bg-teal", "small-box bg-olive", "small-box bg-lime", "small-box bg-orange", "small-box bg-fuchsia", "small-box bg-purple", "small-box bg-maroon" };

                        if (col == 13)
                        {
                            col = 0;
                        }
                        divBox.Attributes.Add("class", color[col]);
                        col++;

                        if (Method != "")
                        {
                            lblVal.Text = "0";

                            lblName.Text = row["Chart_Name"].ToString();
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