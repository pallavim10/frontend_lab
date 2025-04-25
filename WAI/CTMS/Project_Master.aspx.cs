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
    public partial class Project_Master : System.Web.UI.Page
    {
        DAL dal = new DAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Class();
                    Session["dt"] = null;

                    if (Request.QueryString["Action"] != null)
                    {                        
                        view_ProjDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void view_ProjDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                if (Request.QueryString["ProjectId"] != null)
                {
                    ds = dal.GetSetPROJECTDETAILS(Action: "Select_Proj", Project_ID: Convert.ToInt32(Request.QueryString["ProjectId"].ToString()));
                }
                else
                {
                    ds = dal.GetSetPROJECTDETAILS(Action: "Select_Proj", Project_ID: Convert.ToInt32(Session["PROJECTID"].ToString()));
                }
                
                DataTable dt = ds.Tables[0];
                DataTable dt1 = ds.Tables[1];

                txtSponsor.Text = dt.Rows[0]["SPONSOR"].ToString();
                txtStudyAwardDate.Text = dt.Rows[0]["StudyAwardDate"].ToString();
                txtProjectID.Text = dt.Rows[0]["Project_ID"].ToString();
                txtStudyID.Text = dt.Rows[0]["PROJNAME"].ToString();
                txtTitle.Text = dt.Rows[0]["PROJTITLE"].ToString();
                bind_Class();

                string[] classs = dt.Rows[0]["PRODUCTID"].ToString().Split(',').ToArray();
                lstClass.ClearSelection();
                if (classs != null && classs.Length > 0)
                {
                    for (int i = 0; i < classs.Length; i++)
                    {
                        if (classs[i] != "")
                        {
                            ListItem itm = lstClass.Items.FindByValue(classs[i]);
                            if (itm != null)
                                itm.Selected = true;
                        }
                    }
                }

                bind_SubClass();

                string[] SubClass = dt.Rows[0]["Ther_SUBCLASS"].ToString().Split(',').ToArray();
                lstSubClass.ClearSelection();
                if (SubClass != null && SubClass.Length > 0)
                {
                    for (int i = 0; i < SubClass.Length; i++)
                    {
                        if (SubClass[i] != "")
                        {
                            ListItem itm = lstSubClass.Items.FindByValue(SubClass[i]);
                            if (itm != null)
                                itm.Selected = true;
                        }
                    }
                }

                bind_Indic();

                string[] Indic = dt.Rows[0]["Ther_INDIC"].ToString().Split(',').ToArray();
                lstIndic.ClearSelection();
                if (Indic != null && Indic.Length > 0)
                {
                    for (int i = 0; i < Indic.Length; i++)
                    {
                        if (Indic[i] != "")
                        {
                            ListItem itm = lstIndic.Items.FindByValue(Indic[i]);
                            if (itm != null)
                                itm.Selected = true;
                        }
                    }
                }
                txtPhase.Text = dt.Rows[0]["PHASE"].ToString();
                txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
                txtComparator.Text = dt.Rows[0]["ComparatorName"].ToString();
                txtPrimaryOBJ.Text = dt.Rows[0]["PrimaryOBJ"].ToString();
                txtStudySTDAT.Text = dt.Rows[0]["PROJSTDAT"].ToString();
                txtStudyENDAT.Text = dt.Rows[0]["PROJENDAT"].ToString();
                txtStudyACTSTDAT.Text = dt.Rows[0]["PROJACTSTDAT"].ToString();
                txtStudyACTENDAT.Text = dt.Rows[0]["PROJACTENDAT"].ToString();
                txtScreened.Text = dt.Rows[0]["NoOfScreened"].ToString();
                txtRandomized.Text = dt.Rows[0]["NoOfRandom"].ToString();
                txtEvaluable.Text = dt.Rows[0]["NoOfEval"].ToString();
                txtSites.Text = dt.Rows[0]["NoOfSites"].ToString();
                txtPatientsPSPM.Text = dt.Rows[0]["NoOfPatient"].ToString();
                txtEnrollRate.Text = dt.Rows[0]["EnrollRate"].ToString();
                txtEnrolSTDAT.Text = dt.Rows[0]["EnrollSTDAT"].ToString();
                txtEnrolENDAT.Text = dt.Rows[0]["EnrollENDAT"].ToString();
                txtEnrollACSTDAT.Text = dt.Rows[0]["EnrollACTSTDAT"].ToString();
                txtEnrollACENDAT.Text = dt.Rows[0]["EnrollACTENDAT"].ToString();
                txtEnrollDur.Text = dt.Rows[0]["EnrollDur"].ToString();

                if (dt.Rows[0]["IWRS"].ToString() == "1")
                {
                    chkIWRS.Checked = true;
                }

                if (dt.Rows[0]["DM"].ToString() == "1")
                {
                    chkDM.Checked = true;
                }

                if (dt.Rows[0]["LBD"].ToString() == "1")
                {
                    chkLD.Checked = true;
                }

                gvSecondOBJ.DataSource = dt1;
                gvSecondOBJ.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Class()
        {
            try
            {
                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "GET_DATA"
                    );

                lstClass.Items.Clear();
                lstClass.DataSource = ds;
                lstClass.DataValueField = "PRODUCTID";
                lstClass.DataTextField = "PRODUCTNAM";
                lstClass.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_SubClass()
        {
            try
            {
                string PRODUCTID = null;

                foreach (ListItem item in lstClass.Items)
                {
                    if (item.Selected == true)
                    {
                        if (PRODUCTID != null)
                        {
                            PRODUCTID += "," + item.Value.ToString();
                        }
                        else
                        {
                            PRODUCTID += item.Value.ToString();
                        }
                    }
                }

                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "GET_Sub",
                PRODUCTID: PRODUCTID
                    );

                lstSubClass.Items.Clear();
                lstSubClass.DataSource = ds;
                lstSubClass.DataValueField = "ID";
                lstSubClass.DataTextField = "Therapetic_SubClass";
                lstSubClass.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_Indic()
        {
            try
            {
                string SubClass = null;

                foreach (ListItem item in lstSubClass.Items)
                {
                    if (item.Selected == true)
                    {
                        if (SubClass != null)
                        {
                            SubClass += "," + item.Value.ToString();
                        }
                        else
                        {
                            SubClass += item.Value.ToString();
                        }
                    }
                }

                DataSet ds = dal.GetSetPRODUCTDETAILS(
                Action: "GET_Indic",
                ID: SubClass
                    );

                lstIndic.Items.Clear();
                lstIndic.DataSource = ds;
                lstIndic.DataValueField = "ID";
                lstIndic.DataTextField = "INDICATION";
                lstIndic.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void save_SecondOBJ()
        {
            try
            {
                if (Request.QueryString["Action"] != null)
                {
                    dal.GetSetPROJECTDETAILS
                        (
                        Action: "update_SecObj",
                        PrimaryOBJ: txtSecOBJ.Text
                        //Project_ID: Convert.ToInt32(txtProjectID.Text)
                        );
                }
                else
                {
                    dal.GetSetPROJECTDETAILS
                        (
                        Action: "INSERT_Temp_SecondOBJ",
                        PrimaryOBJ: txtSecOBJ.Text
                        //Project_ID: Convert.ToInt32(txtProjectID.Text)
                        );
                }

                txtSecOBJ.Text = "";
                bind_SecondOBJ();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_SecondOBJ()
        {
            try
            {
                if (Request.QueryString["Action"] != null)
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "Select_Proj", Project_ID: Convert.ToInt32(Session["PROJECTID"].ToString()));
                    gvSecondOBJ.DataSource = ds.Tables[1];
                    gvSecondOBJ.DataBind();
                }
                else
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "GET_Temp_SecondOBJ");
                    gvSecondOBJ.DataSource = ds.Tables[0];
                    gvSecondOBJ.DataBind();
                }

                if (gvSecondOBJ.Rows.Count > 0)
                {
                    txtProjectID.Enabled = false;
                }
                else
                {
                    txtProjectID.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void delete_SecondOBJ(string ID)
        {
            try
            {
                if (Request.QueryString["Action"] != null)
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "delete_SecObj", PrimaryOBJ: ID);
                }
                else
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "DELETE_Temp_SecondOBJ", PrimaryOBJ: ID);
                }

                bind_SecondOBJ();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void clear()
        {
            try
            {
                txtSponsor.Text = "";
                txtStudyAwardDate.Text = "";
                txtProjectID.Text = "";
                txtStudyID.Text = "";
                txtTitle.Text = "";
                bind_Class();
                bind_SubClass();
                bind_Indic();
                lstClass.ClearSelection();
                lstSubClass.ClearSelection();
                lstIndic.ClearSelection();
                txtPhase.Text = "";
                txtProductName.Text = "";
                txtComparator.Text = "";
                txtPrimaryOBJ.Text = "";
                txtStudySTDAT.Text = "";
                txtStudyENDAT.Text = "";
                txtEnrollACSTDAT.Text = "";
                txtEnrollACENDAT.Text = "";
                txtScreened.Text = "";
                txtRandomized.Text = "";
                txtEvaluable.Text = "";
                txtSites.Text = "";
                txtPatientsPSPM.Text = "";
                txtEnrollRate.Text = "";
                txtEnrolSTDAT.Text = "";
                txtEnrolENDAT.Text = "";
                txtEnrollACSTDAT.Text = "";
                txtEnrollACENDAT.Text = "";
                txtEnrollDur.Text = "";
                chkDM.Checked = false;
                chkIWRS.Checked = false;
                chkLD.Checked = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void insert_Proj()
        {
            try
            {
                string classs = null;

                foreach (ListItem item in lstClass.Items)
                {
                    if (item.Selected == true)
                    {
                        if (classs != null)
                        {
                            classs += "," + item.Value.ToString();
                        }
                        else
                        {
                            classs += item.Value.ToString();
                        }
                    }
                }

                string Subclasss = null;

                foreach (ListItem item in lstSubClass.Items)
                {
                    if (item.Selected == true)
                    {
                        if (Subclasss != null)
                        {
                            Subclasss += "," + item.Value.ToString();
                        }
                        else
                        {
                            Subclasss += item.Value.ToString();
                        }
                    }
                }

                string Indic = null;

                foreach (ListItem item in lstIndic.Items)
                {
                    if (item.Selected == true)
                    {
                        if (Indic != null)
                        {
                            Indic += "," + item.Value.ToString();
                        }
                        else
                        {
                            Indic += item.Value.ToString();
                        }
                    }
                }

                string IWRS = "", DM = "", LBD = "";
                if (chkIWRS.Checked == true)
                {
                    IWRS = "1";
                }
                else
                {
                    IWRS = "0";
                }
                if (chkDM.Checked == true)
                {
                    DM = "1";
                }
                else
                {
                    DM = "0";
                }
                if (chkLD.Checked == true)
                {
                    LBD = "1";
                }
                else
                {
                    LBD = "0";
                }

                dal.GetSetPROJECTDETAILS
                    (
                    Action: "INSERT_Proj",
                    SPONSOR: txtSponsor.Text,
                    StudyAwardDate: txtStudyAwardDate.Text,
                    Project_ID: Convert.ToInt32(txtProjectID.Text),
                    PROJNAME: txtStudyID.Text,
                    PROJTITLE: txtTitle.Text,
                    PrimaryOBJ: txtPrimaryOBJ.Text,
                    PROJSTDAT: txtStudySTDAT.Text,
                    PROJENDDAT: txtStudyENDAT.Text,
                    PROJACTSTDAT: txtStudyACTSTDAT.Text,
                    PROJACTENDAT: txtStudyACTENDAT.Text,
                    PRODUCTID: classs,
                    TherSubClass: Subclasss,
                    Ther_INDIC: Indic,
                    PHASE: txtPhase.Text,
                    ProductName: txtProductName.Text,
                    ComparatorName: txtComparator.Text,
                    NoOfScreened: txtScreened.Text,
                    NoOfRandom: txtRandomized.Text,
                    NoOfEval: txtEvaluable.Text,
                    NoOfSites: txtSites.Text,
                    EnrollDur: txtEnrollDur.Text,
                    NoOfPatients: txtPatientsPSPM.Text,
                    EnrollRate: txtEnrollRate.Text,
                    EnrollSTDAT: txtEnrolSTDAT.Text,
                    EnrollENDAT: txtEnrolENDAT.Text,
                    EnrollACTSTDAT: txtEnrollACSTDAT.Text,
                    EnrollACTENDAT: txtEnrollACENDAT.Text,
                    IWRS:IWRS,
                    DM:DM,
                    LBD:LBD
                    );

                //dal.GetSetPROJECTDETAILS(Action: "Delete_IndicOfProj", Project_ID: Convert.ToInt32(txtProjectID.Text));

                foreach (ListItem item in lstIndic.Items)
                {
                    if (item.Selected == true)
                    {

                        string Indication = null;
                        string IndicationID = null;

                        IndicationID = item.Value.ToString();
                        Indication = item.Text.ToString();

                        dal.GetSetPROJECTDETAILS
                            (
                            Action: "INSERT_Indic",
                            Project_ID: Convert.ToInt32(txtProjectID.Text),
                            PRODUCTID: IndicationID,
                            INDC: Indication
                            );
                    }
                }

                clear();

                Response.Write("<script> alert('Project Added successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void update_Proj()
        {
            try
            {
                string classs = null;

                foreach (ListItem item in lstClass.Items)
                {
                    if (item.Selected == true)
                    {
                        if (classs != null)
                        {
                            classs += "," + item.Value.ToString();
                        }
                        else
                        {
                            classs += item.Value.ToString();
                        }
                    }
                }

                string Subclasss = null;

                foreach (ListItem item in lstSubClass.Items)
                {
                    if (item.Selected == true)
                    {
                        if (Subclasss != null)
                        {
                            Subclasss += "," + item.Value.ToString();
                        }
                        else
                        {
                            Subclasss += item.Value.ToString();
                        }
                    }
                }

                string Indic = null;

                foreach (ListItem item in lstIndic.Items)
                {
                    if (item.Selected == true)
                    {
                        if (Indic != null)
                        {
                            Indic += "," + item.Value.ToString();
                        }
                        else
                        {
                            Indic += item.Value.ToString();
                        }
                    }
                }

                string IWRS = "", DM = "", LBD = "";
                if (chkIWRS.Checked == true)
                {
                    IWRS = "1";
                }
                else
                {
                    IWRS = "0";
                }
                if (chkDM.Checked == true)
                {
                    DM = "1";
                }
                else
                {
                    DM = "0";
                }
                if (chkLD.Checked == true)
                {
                    LBD = "1";
                }
                else
                {
                    LBD = "0";
                }

                string projectid = "";
                if (Request.QueryString["ProjectId"].ToString() != null)
                {
                    projectid = Request.QueryString["ProjectId"].ToString();
                }
                else
                {
                    projectid = Session["PROJECTID"].ToString();
                }

                dal.GetSetPROJECTDETAILS
                    (
                    Action: "update_Proj",
                    SPONSOR: txtSponsor.Text,
                    StudyAwardDate: txtStudyAwardDate.Text,
                    PROJNAME: txtStudyID.Text,
                    PROJTITLE: txtTitle.Text,
                    PrimaryOBJ: txtPrimaryOBJ.Text,
                    PROJSTDAT: txtStudySTDAT.Text,
                    PROJENDDAT: txtStudyENDAT.Text,
                    PROJACTSTDAT: txtStudyACTSTDAT.Text,
                    PROJACTENDAT: txtStudyACTENDAT.Text,
                    PRODUCTID: classs,
                    TherSubClass: Subclasss,
                    Ther_INDIC: Indic,
                    PHASE: txtPhase.Text,
                    ProductName: txtProductName.Text,
                    ComparatorName: txtComparator.Text,
                    NoOfScreened: txtScreened.Text,
                    NoOfRandom: txtRandomized.Text,
                    NoOfEval: txtEvaluable.Text,
                    NoOfSites: txtSites.Text,
                    EnrollDur: txtEnrollDur.Text,
                    NoOfPatients: txtPatientsPSPM.Text,
                    EnrollRate: txtEnrollRate.Text,
                    EnrollSTDAT: txtEnrolSTDAT.Text,
                    EnrollENDAT: txtEnrolENDAT.Text,
                    EnrollACTSTDAT: txtEnrollACSTDAT.Text,
                    EnrollACTENDAT: txtEnrollACENDAT.Text,
                    Project_ID: Convert.ToInt32(projectid),
                    IWRS: IWRS,
                    DM: DM,
                    LBD: LBD
                    );

                //dal.GetSetPROJECTDETAILS(Action: "Delete_IndicOfProj", Project_ID: Convert.ToInt32(txtProjectID.Text));

                foreach (ListItem item in lstIndic.Items)
                {
                    if (item.Selected == true)
                    {
                        string Indication = null;
                        string IndicationID = null;

                        IndicationID = item.Value.ToString();
                        Indication = item.Text.ToString();

                        dal.GetSetPROJECTDETAILS
                            (
                            Action: "INSERT_Indic",
                            Project_ID: Convert.ToInt32(txtProjectID.Text),
                            PRODUCTID: IndicationID,
                            INDC: Indication
                            );
                    }
                }

                clear();

                Response.Write("<script> alert('Project Updated successfully.')</script>");
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_SubClass();
                bind_Indic();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lstSubClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_Indic();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnSaveSecObj_Click(object sender, EventArgs e)
        {
            try
            {
                save_SecondOBJ();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Action"] != null)
                {
                    update_Proj();
                }
                else
                {
                    insert_Proj();
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvSecondOBJ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                delete_SecondOBJ(ID);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }


    }
}