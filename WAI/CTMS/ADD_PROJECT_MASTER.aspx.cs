
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPT;
using System.Data.SqlClient;

namespace CTMS
{
    public partial class ADD_PROJECT_MASTER : System.Web.UI.Page
    {
        DAL dal = new DAL();
        CommonFunction.CommonFunction Confun = new CommonFunction.CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_Class();
                    bind_Sponsor();
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

        private void bind_Sponsor()
        {
            try
            {
                DataSet ds = dal.Sponsor_SP(Action: "GET_Sponsors");
                lstSponsor.DataSource = ds.Tables[0];
                lstSponsor.DataValueField = "ID";
                lstSponsor.DataTextField = "Company";
                lstSponsor.DataBind();
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
                //DataTable dt1 = ds.Tables[1];

                //txtSponsor.Text = dt.Rows[0]["SPONSOR"].ToString();
                txtStudyAwardDate.Text = dt.Rows[0]["STUDYAWARDDATE"].ToString();
                //txtProjectID.Text = dt.Rows[0]["Project_ID"].ToString();
                txtStudyID.Text = dt.Rows[0]["PROJNAME"].ToString();
                txtTitle.Text = dt.Rows[0]["PROJTITLE"].ToString();

                bind_Sponsor();

                string[] Sponsor = dt.Rows[0]["SPONSOR"].ToString().Split(',').ToArray();
                lstIndic.ClearSelection();
                if (Sponsor != null && Sponsor.Length > 0)
                {
                    for (int i = 0; i < Sponsor.Length; i++)
                    {
                        if (Sponsor[i] != "")
                        {
                            ListItem itm1 = lstSponsor.Items.FindByText(Sponsor[i]);
                            if (itm1 != null)
                                itm1.Selected = true;
                        }
                    }
                }

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
                            ListItem itm2 = lstIndic.Items.FindByValue(Indic[i]);
                            if (itm2 != null)
                                itm2.Selected = true;
                        }
                    }
                }



                txtPhase.Text = dt.Rows[0]["PHASE"].ToString();
                txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
                txtComparator.Text = dt.Rows[0]["ComparatorName"].ToString();
                txtPrimaryOBJ.Text = dt.Rows[0]["PrimaryOBJ"].ToString();
                txtStudySTDAT.Text = dt.Rows[0]["PROJECTSTARTDATE"].ToString();
                txtStudyENDAT.Text = dt.Rows[0]["PROJECTENDDATE"].ToString();
                txtStudyACTSTDAT.Text = dt.Rows[0]["ACTUALSTUDYSTARTDATE"].ToString();
                txtStudyACTENDAT.Text = dt.Rows[0]["ACTUALSTUDYENDDATE"].ToString();
                txtScreened.Text = dt.Rows[0]["NoOfScreened"].ToString();
                txtRandomized.Text = dt.Rows[0]["NoOfRandom"].ToString();
                txtEvaluable.Text = dt.Rows[0]["NoOfEval"].ToString();
                txtSites.Text = dt.Rows[0]["NoOfSites"].ToString();
                txtPatientsPSPM.Text = dt.Rows[0]["NoOfPatient"].ToString();
                txtEnrollRate.Text = dt.Rows[0]["EnrollRate"].ToString();
                txtEnrolSTDAT.Text = dt.Rows[0]["ENROLLSTARTDATE"].ToString();
                txtEnrolENDAT.Text = dt.Rows[0]["ENROLLENDDATE"].ToString();
                txtEnrollACSTDAT.Text = dt.Rows[0]["ENROLLACTUALSTARTDT"].ToString();
                txtEnrollACENDAT.Text = dt.Rows[0]["ENROLLACTUALENDDT"].ToString();
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

                if (dt.Rows[0]["CTMS"].ToString() == "1")
                {
                    chkCTMS.Checked = true;
                }

                if (dt.Rows[0]["SITE_MGMT"].ToString() == "1")
                {
                    chkSiteMgmt.Checked = true;
                }
                if (dt.Rows[0]["SAFETY"].ToString() == "1")
                {
                    chkSafety.Checked = true;
                }
                if (dt.Rows[0]["eSource"].ToString() == "True")
                {
                    ChkeSource.Checked = true;
                }
                if (dt.Rows[0]["eTMF"].ToString() == "True")
                {
                    ChkeTMF.Checked = true;
                }

                bind_PrimaryOBJ();
                bind_SecondOBJ();
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

        protected void lbtnSavePrimaryOBJ_Click(object sender, EventArgs e)
        {
            try
            {
                save_PrimaryOBJ();
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
                    view_ProjDetails();
                    //Response.Redirect("ViewProjects.aspx");
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

        private void insert_Proj()
        {
            try
            {
                //string CHILDDBNAME = "WAD_MASTER_361_DUMMY_1";
                string CHILDDBNAME = "";
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

                string Sponsor = null;

                foreach (ListItem item in lstSponsor.Items)
                {
                    if (item.Selected == true)
                    {
                        if (Sponsor != null)
                        {
                            Sponsor += "," + item.Text.ToString();
                        }
                        else
                        {
                            Sponsor += item.Text.ToString();
                        }
                    }
                }


                string IWRS = "", DM = "", LBD = "", CTMS = "", SM = "", SAFETY = "",eSource = "", eTMF = "";
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
                if (chkCTMS.Checked == true)
                {
                    CTMS = "1";
                }
                else
                {
                    CTMS = "0";
                }

                if (chkSiteMgmt.Checked == true)
                {
                    SM = "1";
                }
                else
                {
                    SM = "0";
                }

                if (chkSafety.Checked == true)
                {
                    SAFETY = "1";
                }
                else
                {
                    SAFETY = "0";
                }
                if (ChkeSource.Checked == true)
                {
                    eSource = "1";
                }
                else
                {
                    eSource = "0";
                }
                if (ChkeTMF.Checked == true)
                {
                    eTMF = "1";
                }
                else
                {
                    eTMF = "0";
                }
                DataSet ds = dal.GetSetPROJECTDETAILS
                (
                Action: "INSERT_Proj",
                SPONSOR: Sponsor,
                StudyAwardDate: txtStudyAwardDate.Text,
                // Project_ID: Convert.ToInt32(txtProjectID.Text),
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
                IWRS: IWRS,
                DM: DM,
                LBD: LBD,
                CTMS: CTMS,
                SITE_MGMT: SM,
                SAFETY: SAFETY,
                ENTEREDBY: Session["User_ID"].ToString(),
                ChildDBName: CHILDDBNAME,
                eSource: eSource,
                eTMF: eTMF,
                IPADDRESS: Confun.GetIpAddress()
                );

                string PROJECTID = ds.Tables[0].Rows[0][0].ToString();

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
                        Project_ID: Convert.ToInt32(PROJECTID),
                        PRODUCTID: IndicationID,
                        INDC: Indication,
                        ENTEREDBY: Session["User_ID"].ToString(),
                        ChildDBName: CHILDDBNAME
                        );
                    }
                }

                //clear();

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

                string Sponsor = null;

                foreach (ListItem item in lstSponsor.Items)
                {
                    if (item.Selected == true)
                    {
                        if (Sponsor != null)
                        {
                            Sponsor += "," + item.Text.ToString();
                        }
                        else
                        {
                            Sponsor += item.Text.ToString();
                        }
                    }
                }

                string IWRS = "", DM = "", LBD = "", CTMS = "", SM = "", SAFETY = "", eSource = "", eTMF = "";
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
                if (chkCTMS.Checked == true)
                {
                    CTMS = "1";
                }
                else
                {
                    CTMS = "0";
                }

                if (chkSiteMgmt.Checked == true)
                {
                    SM = "1";
                }
                else
                {
                    SM = "0";
                }

                if (chkSafety.Checked == true)
                {
                    SAFETY = "1";
                }
                else
                {
                    SAFETY = "0";
                }
                if (ChkeSource.Checked == true)
                {
                    eSource = "1";
                }
                else
                {
                    eSource = "0";
                }
                if (ChkeTMF.Checked == true)
                {
                    eTMF = "1";
                }
                else
                {
                    eTMF = "0";
                }


                string projectid = "";
                if (Request.QueryString["ProjectId"] != null)
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
                SPONSOR: Sponsor,
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
                LBD: LBD,
                CTMS: CTMS,
                SITE_MGMT: SM,
                SAFETY: SAFETY,
                eSource: eSource,
                eTMF: eTMF,
                ENTEREDBY: Session["User_ID"].ToString(),
                IPADDRESS: Confun.GetIpAddress()
                );;

                //DataSet ds1 = dal.AddUserProfile(Action: "CheckDBName", PROJECTID: projectid);
                //string CON = ds1.Tables[0].Rows[0]["ConnectionString"].ToString();
                //string[] parts = CON.Split(';');
                //string CHILDDBNAME = "";
                //for (int i = 0; i < parts.Length; i++)
                //{
                //    string part = parts[i].Trim();

                //    if (part.StartsWith("Initial Catalog="))
                //    {
                //        CHILDDBNAME = part.Replace("Initial Catalog=", "");

                //    }
                //}

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
                        Action: "UPDATE_Indic",
                        Project_ID: Convert.ToInt32(projectid),
                        PRODUCTID: IndicationID,
                        INDC: Indication,
                        ENTEREDBY: Session["User_ID"].ToString()
                        );

                        //dal.GetSetPROJECTDETAILS
                        //   (
                        //   Action: "INSERT_IndicTOCHILDDB",
                        //   Project_ID: Convert.ToInt32(projectid),
                        //   PRODUCTID: IndicationID,
                        //   INDC: Indication,
                        //   ChildDBName: CHILDDBNAME,
                        //   ENTEREDBY: Session["User_ID"].ToString()
                        //   );
                    }
                    else
                    {
                        string Indication = null;
                        string IndicationID = null;

                        IndicationID = item.Value.ToString();
                        Indication = item.Text.ToString();

                        //dal.GetSetPROJECTDETAILS
                        //    (
                        //    Action: "DELETE_Indic",
                        //    Project_ID: Convert.ToInt32(projectid),
                        //    PRODUCTID: IndicationID,
                        //    INDC: Indication,
                        //    ENTEREDBY: Session["User_ID"].ToString()
                        //    );

                        //dal.GetSetPROJECTDETAILS
                        //      (
                        //      Action: "DELETE_IndicTOCHILDDB",
                        //      Project_ID: Convert.ToInt32(projectid),
                        //      PRODUCTID: IndicationID,
                        //      INDC: Indication,
                        //      ChildDBName: CHILDDBNAME,
                        //      ENTEREDBY: Session["User_ID"].ToString()
                        //      );
                    }
                }

                clear();

                Response.Write("<script> alert('Project Updated successfully.'); window.location.href='ViewProjects.aspx'</script>");
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
                txtStudyAwardDate.Text = "";
                //txtProjectID.Text = "";
                txtStudyID.Text = "";
                txtTitle.Text = "";
                bind_Class();
                bind_SubClass();
                bind_Indic();
                bind_Sponsor();
                lstSponsor.ClearSelection();
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
                ChkeSource.Checked = false;
                ChkeTMF.Checked = false;

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        protected void gvPrimaryOBJ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                if (e.CommandName == "EditPrimaryObj")
                {
                    hdnPrimaryID.Value = ID;
                    EditPrimaryObj(ID);
                }
                else if (e.CommandName == "DeletePrimaryObj")
                {
                    delete_PrimaryOBJ(ID);
                }
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
                if (e.CommandName == "EditSecObj")
                {
                    hdnSecObjID.Value = ID;
                    EditSecObj(ID);
                }
                else if (e.CommandName == "DeleteSecObj")
                {
                    delete_SecondOBJ(ID);
                }
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
                    if (hdnSecObjID.Value == "")
                    {
                        dal.GetSetPROJECTDETAILS
                        (
                        Action: "Insert_SecObj",
                        PrimaryOBJ: txtSecOBJ.Text, Project_ID: Convert.ToInt32(Session["PROJECTID"].ToString())
                        );
                    }
                    else
                    {
                        dal.GetSetPROJECTDETAILS
                        (
                        Action: "update_SecObj",
                        PrimaryOBJ: txtSecOBJ.Text,
                        PRODUCTID: hdnSecObjID.Value
                        );

                        hdnSecObjID.Value = "";
                    }
                }
                else
                {
                    dal.GetSetPROJECTDETAILS
                    (
                    Action: "INSERT_Temp_SecondOBJ",
                    PrimaryOBJ: txtSecOBJ.Text,
                    ObjectiveType: "Secondary"
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

        private void save_PrimaryOBJ()
        {
            try
            {
                if (Request.QueryString["Action"] != null)
                {
                    if (hdnPrimaryID.Value == "")
                    {
                        dal.GetSetPROJECTDETAILS
                        (
                        Action: "Insert_PrimaryObj",
                        PrimaryOBJ: txtPrimaryOBJ.Text,
                        Project_ID: Convert.ToInt32(Session["PROJECTID"].ToString())
                        );
                    }
                    else
                    {
                        dal.GetSetPROJECTDETAILS
                        (
                        Action: "update_PrimaryObj",
                        PrimaryOBJ: txtPrimaryOBJ.Text,
                        PRODUCTID: hdnPrimaryID.Value
                        );
                    }

                    hdnPrimaryID.Value = "";
                }
                else
                {
                    dal.GetSetPROJECTDETAILS
                    (
                    Action: "INSERT_Temp_SecondOBJ",
                    PrimaryOBJ: txtPrimaryOBJ.Text,
                    ObjectiveType: "Primary"
                    );
                }

                txtPrimaryOBJ.Text = "";
                bind_PrimaryOBJ();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void bind_PrimaryOBJ()
        {
            try
            {
                if (Request.QueryString["PROJECTID"] != null)
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "Select_PrimaryObj", Project_ID: Convert.ToInt32(Request.QueryString["PROJECTID"].ToString()));
                    gvPrimaryOBJ.DataSource = ds.Tables[0];
                    gvPrimaryOBJ.DataBind();
                }
                else
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "GET_Temp_PrimaryOBJ", ObjectiveType: "Primary");
                    gvPrimaryOBJ.DataSource = ds.Tables[0];
                    gvPrimaryOBJ.DataBind();
                }
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
                if (Request.QueryString["PROJECTID"] != null)
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "Select_SecObj", Project_ID: Convert.ToInt32(Request.QueryString["PROJECTID"].ToString()));
                    gvSecondOBJ.DataSource = ds.Tables[0];
                    gvSecondOBJ.DataBind();
                }
                else
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "GET_Temp_SecondOBJ", ObjectiveType: "Secondary");
                    gvSecondOBJ.DataSource = ds.Tables[0];
                    gvSecondOBJ.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EditSecObj(string ID)
        {
            try
            {
                if (Request.QueryString["Action"] != null)
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "Select_SecObj_BYID", PrimaryOBJ: ID);

                    txtSecOBJ.Text = ds.Tables[0].Rows[0]["DATA"].ToString();
                }
                else
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "Select_Temp_BYID", PrimaryOBJ: ID);

                    txtSecOBJ.Text = ds.Tables[0].Rows[0]["DATA"].ToString();
                }

                bind_PrimaryOBJ();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

        private void EditPrimaryObj(string ID)
        {
            try
            {
                if (Request.QueryString["Action"] != null)
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "Select_PrimaryObj_BYID", PrimaryOBJ: ID);

                    txtPrimaryOBJ.Text = ds.Tables[0].Rows[0]["DATA"].ToString();
                }
                else
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "Select_Temp_BYID", PrimaryOBJ: ID);

                    txtPrimaryOBJ.Text = ds.Tables[0].Rows[0]["DATA"].ToString();
                }

                bind_SecondOBJ();
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

        private void delete_PrimaryOBJ(string ID)
        {
            try
            {
                if (Request.QueryString["Action"] != null)
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "delete_PrimaryObj", PrimaryOBJ: ID, ENTEREDBY: Session["User_ID"].ToString());
                }
                else
                {
                    DataSet ds = dal.GetSetPROJECTDETAILS(Action: "DELETE_Temp_PrimaryOBJ", PrimaryOBJ: ID, ENTEREDBY: Session["User_ID"].ToString());
                }

                bind_PrimaryOBJ();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }

       

    }
}
