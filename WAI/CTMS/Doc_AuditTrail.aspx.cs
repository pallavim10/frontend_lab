using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPT;
using System.Data;
using System.Text;


namespace CTMS
{
    public partial class Doc_AuditTrail : System.Web.UI.Page
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

        public void bind_Sec()
        {
            try
            {
                DataSet ds = dal.Documents_SP(Action: "GET_PROJECT_Sec", ProjectID: Session["PROJECTID"].ToString(), DocID: drpPlan.SelectedValue);
                drpSection.DataSource = ds.Tables[0];
                drpSection.DataValueField = "SecID";
                drpSection.DataTextField = "SectionName";
                drpSection.DataBind();
                drpSection.Items.Insert(0, new ListItem("--Select--", "0"));
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
                bind_Sec();
                GetData();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
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
                DataSet ds = dal.Documents_SP(Action: "GET_AuditTrail", ProjectID: Session["PROJECTID"].ToString(), SecID: drpSection.SelectedValue, DocID: drpPlan.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvAuditTrail.DataSource = ds;
                    gvAuditTrail.DataBind();
                }
                else
                {
                    gvAuditTrail.DataSource = null;
                    gvAuditTrail.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        //protected void repeat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //        {
        //            StringBuilder sb = new StringBuilder();
        //            DataRowView row = (DataRowView)e.Item.DataItem;
        //            string NewData = row["NewData"].ToString();
        //            string OldData = row["OldData"].ToString();

        //            Literal litOld = (Literal)e.Item.FindControl("litOld");
        //            Literal litDiff = (Literal)e.Item.FindControl("litDiff");
        //            Literal litNew = (Literal)e.Item.FindControl("litNew");

        //            var d = new Differ();
        //            var builder = new InlineDiffBuilder(d);
        //            var result = builder.BuildDiffModel(OldData, NewData);

        //            foreach (var line in result.Lines)
        //            {
        //                if (line.Type == ChangeType.Inserted)
        //                {
        //                    sb.Append("+ ");
        //                }
        //                else if (line.Type == ChangeType.Deleted)
        //                {
        //                    sb.Append("- ");
        //                }
        //                else if (line.Type == ChangeType.Modified)
        //                {
        //                    sb.Append("* ");
        //                }
        //                else if (line.Type == ChangeType.Imaginary)
        //                {
        //                    sb.Append("? ");
        //                }
        //                else if (line.Type == ChangeType.Unchanged)
        //                {
        //                    sb.Append("  ");
        //                }

        //                sb.Append(line.Text + "<br/>");
        //            }

        //            litOld.Text = OldData;
        //            litDiff.Text = sb.ToString();
        //            litNew.Text = NewData;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorMsg.Text = ex.Message.ToString();
        //    }
        //}

    }
}