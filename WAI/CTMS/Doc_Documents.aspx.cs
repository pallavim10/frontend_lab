using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;

namespace CTMS
{
    public partial class Doc_Documents : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Doc();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void bind_Doc()
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "GET_PROJECT_Doc", ProjectID: Session["PROJECTID"].ToString());
                drpPlan.DataSource = ds.Tables[0];
                drpPlan.DataValueField = "DocID";
                drpPlan.DataTextField = "DocName";
                drpPlan.DataBind();
                drpPlan.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


        protected void drpPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GetData()
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "GET_Documents", ProjectID: Session["PROJECTID"].ToString(), DocID: drpPlan.SelectedValue);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        repeat.DataSource = ds;
                        repeat.DataBind();
                        divDoc.Visible = true;
                        lblHeader.Text = drpPlan.SelectedItem.Text;
                    }
                    else
                    {
                        repeat.DataSource = null;
                        repeat.DataBind();
                        divDoc.Visible = false;
                    }
                }
                else
                {
                    repeat.DataSource = null;
                    repeat.DataBind();
                    divDoc.Visible = false;
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void repeat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    int Count = Convert.ToInt32(row["Count"]);

                    ImageButton imgTrack = (ImageButton)e.Item.FindControl("imgTrack");

                    if (Count > 0)
                    {
                        imgTrack.Visible = true;
                    }
                    else
                    {
                        imgTrack.Visible = false;
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