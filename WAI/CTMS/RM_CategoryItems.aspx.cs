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
    public partial class RM_CategoryItems : System.Web.UI.Page
    {
        DAL dal = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindCategory();
                BindSubcategory();
                BindFactors();
                btnupdate.Visible = false;
                btnupdatesubcat.Visible = false;
                btnupdatefactor.Visible = false;
            }

        }

        public void BindCategory()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "Category");
                if (dt.Rows.Count > 1)
                {
                    gridcategory.DataSource = dt;
                    gridcategory.DataBind();
                    gridcategory.Rows[0].Visible = false;
                    ddlcategory.DataSource = dt;
                    ddlcategory.DataTextField = "Description";
                    ddlcategory.DataValueField = "id";
                    ddlcategory.DataBind();

                }
                else
                {
                    gridcategory.DataSource = null;
                    gridcategory.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void BindSubcategory()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "SubCategory", Categoryvalue: ddlcategory.SelectedValue, SubCategoryname: txtsubcategory.Text);
                if (dt.Rows.Count > 1)
                {
                    gridsubcategory.DataSource = dt;
                    gridsubcategory.DataBind();
                    gridsubcategory.Rows[0].Visible = false;
                    ddlsubcategory.DataSource = dt;
                    ddlsubcategory.DataTextField = "Description";
                    ddlsubcategory.DataValueField = "id";
                    ddlsubcategory.DataBind();
                    
                }
                else
                {
                    gridsubcategory.DataSource = null;
                    gridsubcategory.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void BindFactors()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.getcategory(Action: "Factor", SubCategoryvalue: ddlsubcategory.SelectedValue, Factorname: txtfactor.Text);
                if (dt.Rows.Count > 1)
                {
                    gridfactor.DataSource = dt;
                    gridfactor.DataBind();
                    gridfactor.Rows[0].Visible = false;
                }
                else
                {
                    gridfactor.DataSource = null;
                    gridfactor.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnsubmitcate_Click(object sender, EventArgs e)
        {
            try
            {
                string type_id = "";
                int typeid;
                string chkcategory=dal.checkcategory(Action:"Category",Categoryname: txtcategory.Text);
                if (chkcategory != "Present")
                {
                    DataTable dt = dal.checktypeid(Action: "Category", Categoryname: txtcategory.Text);
                    if (dt.Rows.Count == 0)
                    {
                         type_id = "001";
                    }
                    else
                    {
                         typeid = Convert.ToInt32(dt.Rows[0]["typeid"].ToString()) + 1;
                        if (typeid.ToString().Length == 1)
                        {
                            type_id = "00" + typeid.ToString();
                        }
                        else if (typeid.ToString().Length == 2)
                        {
                            type_id = "0" + typeid.ToString();
                        }
                        else
                        {
                            type_id = typeid.ToString();
                        }
                    }
                    string msg = dal.insertmasterdata(Action: "Category", Categoryname: txtcategory.Text, Typeid: type_id);
                    if (msg == "Success")
                    {
                        BindCategory();
                        txtcategory.Text = "";
                    }
                    else
                    {

                    }

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtcategory.Text = "";
                btnupdate.Visible = false;
                btnsubmitcate.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnsubcategory_Click(object sender, EventArgs e)
        {
            try
            {
                string type_id = "";
                string chkcategory = dal.checkcategory(Action: "SubCategory", Categoryvalue: ddlcategory.SelectedValue, SubCategoryname: txtsubcategory.Text);
                if (chkcategory != "Present")
                {
                    DataTable dt = dal.checktypeid(Action: "SubCategory", Categoryvalue: ddlcategory.SelectedValue, SubCategoryname: txtsubcategory.Text);
                    if (dt.Rows.Count == 0)
                    {
                        type_id = "001";
                    }
                    else
                    {
                        int typeid = Convert.ToInt32(dt.Rows[0]["typeid"].ToString()) + 1;
                        if (typeid.ToString().Length == 1)
                        {
                            type_id = "00" + typeid.ToString();
                        }
                        else if (typeid.ToString().Length == 2)
                        {
                            type_id = "0" + typeid.ToString();
                        }
                        else
                        {
                            type_id = typeid.ToString();
                        }
                    }
                    string msg = dal.insertmasterdata(Action: "SubCategory", Categoryvalue: ddlcategory.SelectedValue, SubCategoryname: txtsubcategory.Text, Typeid: type_id);
                    if (msg == "Success")
                    {
                        dt = dal.getcategory(Action: "Category");
                        if (dt.Rows.Count > 0)
                        {
                            gridcategory.DataSource = dt;
                            gridcategory.DataBind();
                            gridcategory.Rows[0].Visible = false;
                        }
                        else
                        {
                            gridcategory.DataSource = null;
                            gridcategory.DataBind();
                        }
                        BindSubcategory();
                        txtsubcategory.Text = "";
                    }
                    else
                    {

                    }

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btncancelsubcategory_Click(object sender, EventArgs e)
        {
            try
            {
                txtsubcategory.Text = "";
                btnupdatesubcat.Visible = false;
                btnsubcategory.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnsubmitfactor_Click(object sender, EventArgs e)
        {
            try
            {
                string type_id = "";
                string chkcategory = dal.checkcategory(Action: "Factor", SubCategoryvalue: ddlsubcategory.SelectedValue, Factorname: txtfactor.Text);
                if (chkcategory != "Present")
                {
                    DataTable dt = dal.checktypeid(Action: "Factor", SubCategoryvalue: ddlsubcategory.SelectedValue, Factorname: txtfactor.Text);
                    if (dt.Rows.Count == 0)
                    {
                        type_id = "001";
                    }
                    else
                    {
                        int typeid = Convert.ToInt32(dt.Rows[0]["typeid"].ToString()) + 1;
                        if (typeid.ToString().Length == 1)
                        {
                            type_id = "00" + typeid.ToString();
                        }
                        else if (typeid.ToString().Length == 2)
                        {
                            type_id = "0" + typeid.ToString();
                        }
                        else
                        {
                            type_id = typeid.ToString();
                        }
                    }
                    string msg = dal.insertmasterdata(Action: "Factor", SubCategoryvalue: ddlsubcategory.SelectedValue, Factorname: txtfactor.Text, Typeid: type_id);
                    if (msg == "Success")
                    {
                        BindFactors();
                        txtfactor.Text = "";
                        dt = dal.getcategory(Action: "SubCategory", Categoryvalue: ddlcategory.SelectedValue, SubCategoryname: txtsubcategory.Text);
                        if (dt.Rows.Count > 1)
                        {
                            gridsubcategory.DataSource = dt;
                            gridsubcategory.DataBind();
                            gridsubcategory.Rows[0].Visible = false;
                        }
                        else
                        {
                            gridsubcategory.DataSource = null;
                            gridsubcategory.DataBind();
                        }
                    }
                    else
                    {

                    }

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btncancelfactor_Click(object sender, EventArgs e)
        {
            try
            {
                txtfactor.Text = "";
                btnupdatefactor.Visible = false;
                btnsubmitfactor.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindSubcategory();
                BindFactors();

            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                BindFactors();
                dt = dal.getcategory(Action: "SubCategory", Categoryvalue: ddlcategory.SelectedValue, SubCategoryname: txtsubcategory.Text);
                if (dt.Rows.Count > 1)
                {
                    gridsubcategory.DataSource = dt;
                    gridsubcategory.DataBind();
                    gridsubcategory.Rows[0].Visible = false;
                }
                else
                {
                    gridsubcategory.DataSource = null;
                    gridsubcategory.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = dal.updatemasterdata(Action: "Category", Categoryname: txtcategory.Text, Id: Session["categoryid"].ToString());
                if(msg!=null)
                {
                    BindCategory();
                    txtcategory.Text="";
                    btnupdate.Visible=false;
                    btnsubmitcate.Visible=true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnupdatesubcat_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = dal.updatemasterdata(Action: "SubCategory", Categoryvalue: ddlcategory.SelectedValue, SubCategoryname: txtsubcategory.Text, Id: Session["subcategoryid"].ToString());
                if(msg!=null)
                {
                    BindSubcategory();
                    txtsubcategory.Text="";
                    btnupdatesubcat.Visible = false;
                    btnsubcategory.Visible=true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnupdatefactor_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = dal.updatemasterdata(Action: "Factor", SubCategoryvalue: ddlsubcategory.SelectedValue, Factorname: txtfactor.Text, Id: Session["factorid"].ToString());
                if (msg != null)
                {
                    BindFactors();
                    txtfactor.Text = "";
                    btnupdatefactor.Visible = false;
                    btnsubmitfactor.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gridcategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["categoryid"] = id;
                if (e.CommandName == "Edit1")
                {
                    EditCategory(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    DeleteCategory(id);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void EditCategory(string id)
        {
            try
            {
                DataTable dt = dal.Bindmaster(Action: "Category", Id: id);
                if (dt.Rows.Count > 0)
                {
                    txtcategory.Text = dt.Rows[0]["description"].ToString();
                    btnupdate.Visible = true;
                    btnsubmitcate.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void DeleteCategory(string id)
        {
            try
            {
                string msg = dal.DeleteMaster(Action: "Category", Id: id);
                if (msg != null)
                {
                    BindCategory();
                    this.ddlcategory_SelectedIndexChanged(null, EventArgs.Empty);
                    btnupdate.Visible = false;
                    btnsubcategory.Visible = true;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gridsubcategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["subcategoryid"] = id;
                if (e.CommandName == "Edit1")
                {
                    EditSubCategory(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    DeleteSubCategory(id);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void EditSubCategory(string id)
        {
            try
            {
                DataTable dt = dal.Bindmaster(Action: "SubCategory", Id: id);
                if (dt.Rows.Count > 0)
                {
                    txtsubcategory.Text = dt.Rows[0]["description"].ToString();
                    btnupdatesubcat.Visible = true;
                    btnsubcategory.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void DeleteSubCategory(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                string msg = dal.DeleteMaster(Action: "SubCategory", Id: id);
                if (msg != null)
                {
                    BindSubcategory();
                     dt = dal.getcategory(Action: "Category");
                     if (dt.Rows.Count > 1)
                     {
                         gridcategory.DataSource = dt;
                         gridcategory.DataBind();
                         gridcategory.Rows[0].Visible = false;
                     }
                     else
                     {
                         gridcategory.DataSource = null;
                         gridcategory.DataBind();
                     }
                    btnupdatesubcat.Visible = false;
                    btnsubcategory.Visible = true;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gridfactor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string id = e.CommandArgument.ToString();
                Session["factorid"] = id;
                if (e.CommandName == "Edit1")
                {
                    EditFactor(id);
                }
                else if (e.CommandName == "Delete1")
                {
                    DeleteFactor(id);
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void EditFactor(string id)
        {
            try
            {
                DataTable dt = dal.Bindmaster(Action: "Factor", Id: id);
                if (dt.Rows.Count > 0)
                {
                    txtfactor.Text = dt.Rows[0]["description"].ToString();
                    btnupdatefactor.Visible = true;
                    btnsubmitfactor.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void DeleteFactor(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                string msg = dal.DeleteMaster(Action: "Factor", Id: id);
                if (msg != null)
                {
                    BindFactors();
                    btnupdatefactor.Visible = false;
                    btnsubmitfactor.Visible = true;
                    dt = dal.getcategory(Action: "SubCategory", Categoryvalue: ddlcategory.SelectedValue, SubCategoryname: txtsubcategory.Text);
                    if (dt.Rows.Count > 1)
                    {
                        gridsubcategory.DataSource = dt;
                        gridsubcategory.DataBind();
                        gridsubcategory.Rows[0].Visible = false;

                    }
                    else
                    {
                        gridsubcategory.DataSource = null;
                        gridsubcategory.DataBind();
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gridcategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    string id = drv["id"].ToString();
                    LinkButton lbtndelete = (e.Row.FindControl("lbtndelete") as LinkButton);
                    DataTable dt = dal.checkRisk_Master(Action: "SubCategory", Id: id);
                    if (dt.Rows.Count > 1)
                    {
                        lbtndelete.Visible = false;
                    }
                    else
                    {
                        lbtndelete.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gridsubcategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                string id = drv["id"].ToString();
                LinkButton lbtndeletesubcate = (e.Row.FindControl("lbtndeletesubcate") as LinkButton);
                DataTable dt = dal.checkRisk_Master(Action: "Factor", Id: id);
                if (dt.Rows.Count > 1)
                {
                    lbtndeletesubcate.Visible = false;
                }
                else
                {
                    lbtndeletesubcate.Visible = true;
                }
            }
        }


    }
}