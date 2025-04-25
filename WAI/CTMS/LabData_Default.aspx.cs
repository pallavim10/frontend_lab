using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;

namespace CTMS
{
    public partial class LabData_Default : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GET_TestName();
                    GET_Item();
                    TestItems();
                    NonTestItems();
                    btnUpdateTest.Visible = false;
                    btnUpdateItems.Visible = false;
                    lbtnDeleteTest.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSubmitItems_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_Item();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateMat_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_Item();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnancelItems_Click(object sender, EventArgs e)
        {
            try
            {
                CANCEL_Item();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["ITEM_ID"] = id;
                if (e.CommandName == "Edit1")
                {
                    SELECT_Item(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    DELETE_Item(id);
                    GET_Item();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnAddToTest_Click(object sender, EventArgs e)
        {
            try
            {
                add_to_Test();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnRemoveFromTest_Click(object sender, EventArgs e)
        {
            try
            {
                remove_from_Test();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnAddTest_Click(object sender, EventArgs e)
        {
            try
            {
                INSERT_TestName();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateTest_Click(object sender, EventArgs e)
        {
            try
            {
                UPDATE_TestName();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TestItems();
                NonTestItems();
                showHide_Icons();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnUpdateTest_Click(object sender, EventArgs e)
        {
            try
            {
                SELECT_TestName();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnDeleteTest_Click(object sender, EventArgs e)
        {
            try
            {
                DELETE_TestName();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }






        private void showHide_Icons()
        {
            if (gvAddedItems.Rows.Count > 0)
            {
                lbtnDeleteTest.Visible = false;
            }
            else
            {
                lbtnDeleteTest.Visible = true;
            }
        }

        private void add_to_Test()
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvNewItems.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvNewItems.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblItemsID = (Label)gvNewItems.Rows[i].FindControl("lblItemsID");
                        string ProjectID = Session["PROJECTID"].ToString();

                        dal.Lab_Data_SP(Action: "INSERT_TestItem", TestID: ddlGroup.SelectedValue, ItemID: lblItemsID.Text);
                    }
                }
                TestItems();
                NonTestItems();
                showHide_Icons();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void remove_from_Test()
        {
            try
            {
                DAL dal;
                dal = new DAL();

                for (int i = 0; i < gvAddedItems.Rows.Count; i++)
                {
                    CheckBox ChAction;
                    ChAction = (CheckBox)gvAddedItems.Rows[i].FindControl("Chk_Sel_Fun");

                    if (ChAction.Checked)
                    {
                        Label lblItemsID = (Label)gvAddedItems.Rows[i].FindControl("lblItemsID");
                        string ProjectID = Session["PROJECTID"].ToString();

                        dal.Lab_Data_SP(Action: "DELETE_TestItem", ItemID: lblItemsID.Text, TestID: ddlGroup.SelectedValue);
                    }
                }
                TestItems();
                NonTestItems();
                showHide_Icons();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = ex.Message;
            }
        }

        public void INSERT_Item()
        {
            try
            {
                dal.Lab_Data_SP(Action: "INSERT_Item", Item: txtItems.Text);
                GET_Item();
                CANCEL_Item();
                TestItems();
                NonTestItems();
                showHide_Icons();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void UPDATE_Item()
        {
            try
            {
                dal.Lab_Data_SP(Action: "UPDATE_Item", Item: txtItems.Text, ID: Session["ITEM_ID"].ToString());
                CANCEL_Item();
                GET_Item();
                TestItems();
                NonTestItems();
                showHide_Icons();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void DELETE_Item(string ID)
        {
            try
            {
                dal.Lab_Data_SP(Action: "DELETE_Item", ID: ID);
                GET_Item();
                TestItems();
                NonTestItems();
                showHide_Icons();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_Item()
        {
            try
            {
                DataSet ds;
                ds = dal.Lab_Data_SP(Action: "GET_Item");
                gvItems.DataSource = ds.Tables[0];
                gvItems.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SELECT_Item(string ID)
        {
            try
            {
                DataSet ds;
                ds = dal.Lab_Data_SP(Action: "SELECT_Item", ID: ID);
                txtItems.Text = ds.Tables[0].Rows[0]["Item"].ToString();
                btnUpdateItems.Visible = true;
                btnSubmitItems.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void CANCEL_Item()
        {
            try
            {
                btnUpdateItems.Visible = false;
                btnSubmitItems.Visible = true;
                txtItems.Text = "";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void INSERT_TestName()
        {
            try
            {
                dal.Lab_Data_SP(Action: "INSERT_TestName", TestName: txtGrp.Text);
                CANCEL_TestName();
                GET_TestName();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void UPDATE_TestName()
        {
            try
            {
                dal.Lab_Data_SP(Action: "UPDATE_TestName", TestName: txtGrp.Text, ID: Session["TESTID"].ToString());
                CANCEL_TestName();
                GET_TestName();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void GET_TestName()
        {
            try
            {
                DataSet ds1 = dal.Lab_Data_SP(Action: "GET_TestName");
                ddlGroup.DataSource = ds1.Tables[0];
                ddlGroup.DataValueField = "ID";
                ddlGroup.DataTextField = "TestName";
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void SELECT_TestName()
        {
            try
            {
                Session["TESTID"] = ddlGroup.SelectedValue;
                DataSet ds1 = dal.Lab_Data_SP(Action: "SELECT_TestName", ID: ddlGroup.SelectedValue);
                txtGrp.Text = ds1.Tables[0].Rows[0]["TestName"].ToString();
                btnUpdateTest.Visible = true;
                btnAddTest.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void DELETE_TestName()
        {
            try
            {
                dal.Lab_Data_SP(Action: "DELETE_TestName", ID: ddlGroup.SelectedValue);
                GET_TestName();
                CANCEL_TestName();
                TestItems();
                NonTestItems();
                showHide_Icons();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void CANCEL_TestName()
        {
            try
            {
                txtGrp.Text = "";
                lbtnAddToTest.Visible = true;
                btnUpdateTest.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void TestItems()
        {
            try
            {
                DataSet ds = dal.Lab_Data_SP(Action: "TestItems", TestID: ddlGroup.SelectedValue);
                gvAddedItems.DataSource = ds.Tables[0];
                gvAddedItems.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        public void NonTestItems()
        {
            try
            {
                DataSet ds = dal.Lab_Data_SP(Action: "NonTestItems", TestID: ddlGroup.SelectedValue);
                gvNewItems.DataSource = ds.Tables[0];
                gvNewItems.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

    }
}