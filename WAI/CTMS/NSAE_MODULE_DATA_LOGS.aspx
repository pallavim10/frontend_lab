<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NSAE_MODULE_DATA_LOGS.aspx.cs" Inherits="CTMS.NSAE_MODULE_DATA_LOGS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/DisableButton.js"></script>
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/SAE/SAE_Grid_Queries.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_Grid_Comments.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_DelayReason.js"></script>
    <script src="CommonFunctionsJs/btnSave_Required.js"></script>
    <%--<script src="js/MaxLength.min.js"></script>
    <script src="js/MaxLength_Limit.js"></script>--%>
    <style type="text/css">
        #btnReasonName {
            cursor: pointer;
            text-decoration: underline;
        }

            #btnReasonName:hover {
                color: darkblue;
            }
    </style>
    <style type="text/css">
        #Reason {
            background-color: #fff;
            padding: 100px;
        }

        .center {
            background-color: black;
            text-align: left;
            width: 100%;
            height: 30px;
            color: white;
            font-weight: 400;
            padding-top: 6px;
        }

        .close {
            position: absolute;
            right: 0px;
            top: 0px;
            width: 30px;
            height: 30px;
            opacity: 1;
            border: solid 1px black;
            background-color: black;
        }

            .close:hover {
                opacity: 1;
            }


        #PanelReason {
            background-color: dodgerblue;
            background-color: #fff;
        }

        #ModalPopupExtender3 {
            background-color: #fff;
            width: 400px;
        }

        .modal_PopupReason {
            border: 4px solid #ccc;
            position: absolute;
            top: 10px;
            right: 100px;
            bottom: 0px;
            left: 0px;
            z-index: 10040;
            width: 600px;
            max-height: 180px;
            overflow: auto;
            background-color: white;
            /* margin-top: -200px;*/
        }

        .modalBackgroundReason {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.9;
        }

        .modalPopup {
            background-color: #fff;
            border: 3px solid #ccc;
            padding: 10px;
            width: 450px;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>
    <style type="text/css">
        #popup_Chnage_STATUS {
            background-color: #fff;
        }

        #Panel101 {
            background-color: dodgerblue;
            background-color: #fff;
        }

        #ModalPopupExtender1 {
            background-color: #fff;
            width: 400px;
        }

        .modal_Popup {
            border: 4px solid #ccc;
            position: absolute;
            top: 10px;
            right: 100px;
            bottom: 0px;
            left: 0px;
            z-index: 10040;
            width: 400px;
            max-height: 110px;
            overflow: auto;
            background-color: white;
            /*   margin-top: -230px;*/
        }
    </style>
    <style type="text/css">
        .btn1:hover {
            background-color: #2e6da4 !important;
            color: white !important;
        }

        .input-group i {
            position: absolute !important;
            /*       margin-right:70px!important;*/
        }

        .icon {
            padding: 10px;
            color: white;
            min-width: 50px;
            background-color: #000080;
            border-top-right-radius: 18px;
            border-bottom-right-radius: 18px;
        }

        #txt_Pwd, #txt_UserName {
            width: 100%;
            padding: 0px;
            text-align: center !important;
        }
    </style>
    <style type="text/css">
        .Popup2 {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            min-width: 900px;
            max-width: 950px;
        }

        .modal-body2 {
            position: relative;
            padding: 0px;
        }
    </style>
    <script>
        function ConfirmMsg_Deleted() {

            if ($('#MainContent_hdn_DeletedReq').val() == "True") {

                OpenDeletedReq_Reason();

                return false;
            }
            else {

                var newLine = "\r\n"

                var error_msg = "Are you sure, you want to downgrading this SAE?..";

                error_msg += newLine;
                error_msg += newLine;

                error_msg += "Note : Press OK to Proceed.";

                if (confirm(error_msg)) {

                    $find('modalpnlDeleteSAE').show();
                    return false;

                } else {

                    return false;
                }
            }
        }

        var Msg;
        function ConfirmMsg(Msg) {
            var Confirmed = confirm(Msg);
            if (Confirmed) {
                return true;
            } else {
                return false;
            }
        }

    </script>
    <script type="text/javascript">

        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });

            $('.noSpace').keyup(function () {
                this.value = this.value.replace(/\s/g, '');
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%; display: inline-flex; color: Blue; font-size: medium;">SAE Forms
                <div id="DivSAEDetails" runat="server" visible="false">
                    &nbsp;||&nbsp;
                    <asp:Label runat="server" ForeColor="Blue" ID="lblSAEID" />
                    &nbsp;||&nbsp;
                    <asp:Label runat="server" ForeColor="Blue" ID="lblSAE" />
                    &nbsp;||&nbsp;
                    <asp:Label runat="server" ForeColor="Blue" ID="lblAESPID" />
                    &nbsp;||&nbsp;
                    <asp:Label runat="server" ID="lblAETERM" ForeColor="Blue" />
                    &nbsp;||&nbsp;
                    <asp:Label runat="server" ID="lblStatus" ForeColor="Blue" />
                </div>
            </h3>
            <div class="pull-right" style="margin-top: 3px;">
                <div style="display: inline-flex;">
                    <asp:LinkButton ID="lbtnSupportingDocs" runat="server" Text="Supporting Document" ToolTip="Supporting Document" Height="27px" ForeColor="White" CssClass="btn btn-sm btn-success" OnClick="lbtnSupportingDocs_Click">
                        <i class="fa fa-files-o"></i>&nbsp;&nbsp;Supporting Document&nbsp;&nbsp;<asp:Label ID="lblCount" runat="server" Visible="false" CssClass="badge badge-info" Font-Bold="true" ForeColor="Black" Font-Size="13px" Text=""></asp:Label>
                    </asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDelayedReason" runat="server" Text="Delayed Reason" Visible="false" ToolTip="Delayed Reason" CssClass="btn btn-sm btn-warning" OnClientClick="return OpenDelayedReason();" />&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnINVSignOff" runat="server" Text="Investigator verify and sign off" CssClass="btn btn-sm btn-DarkGreen" OnClick="btnINVSignOff_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDeleteSAE" runat="server" Text="Downgrading SAE" CssClass="btn btn-sm btn-danger"
                        OnClientClick="return ConfirmMsg_Deleted();" />&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
            </div>
            &nbsp&nbsp&nbsp
        </div>
        <asp:HiddenField runat="server" ID="hdnSITEID" />
        <asp:HiddenField runat="server" ID="hdnSubjectID" />
        <asp:HiddenField runat="server" ID="hdnSAE" />
        <asp:HiddenField runat="server" ID="hdnSAEID" />
        <asp:HiddenField runat="server" ID="hdnSTATUS" />
        <asp:HiddenField runat="server" ID="hdnINVSIGN" />
        <asp:HiddenField runat="server" ID="hdn_Delayed_Reason" />
        <asp:HiddenField runat="server" ID="hdn_Delayed_ReasonBy" />
        <asp:HiddenField runat="server" ID="hdn_Delayed_DTServer" />
        <asp:HiddenField runat="server" ID="hdn_Delayed_DTUser" />
        <asp:HiddenField runat="server" ID="hdn_DeletedReq" Value="0" />
        <asp:HiddenField runat="server" ID="hdn_DeletedReq_Reason" />
        <asp:HiddenField runat="server" ID="hdn_DeletedReq_ReasonBy" />
        <asp:HiddenField runat="server" ID="hdn_DeletedReq_DTServer" />
        <asp:HiddenField runat="server" ID="hdn_DeletedReq_DTUser" />
        <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel101" TargetControlID="btnDummy"
            BackgroundCssClass="modalBackground" CancelControlID="btnClose">
        </cc2:ModalPopupExtender>
        <asp:Panel ID="Panel101" runat="server" CssClass="modalPopup" align="center" Style="border: 5px solid #ccc; display: none;">
            <h1 style="color: #00001a!important; font-size: 30px;">
                <a href="#"></a>
                <b>Save this SAE as.....</b>
            </h1>
            <asp:Button ID="btnDummy" runat="server" Text="Yes" CssClass="disp-none" />
            <div id="popup_Chnage_STATUS" title="Information" runat="server">
                <div id="popup_Chnage_STATUS1" style="margin-top: 10px;" align="center">
                    <asp:Button ID="btnOldStatus" runat="server" Style="margin-left: 10px;" Text="Yes"
                        CssClass="btn btn-sm btn-info" OnClick="btnOldStatus_click" />

                    &nbsp;<span id="spanor" title="OR" runat="server"></span>
                    <asp:Button ID="btnNewStatus" runat="server" Style="margin-left: 10px;" Text="NO"
                        CssClass="btn btn-sm btn-info" OnClick="btnNewStatus_click" />
                </div>
                <br />
                <asp:Button ID="btnClose" runat="server" Style="margin-left: 10px;" Text="Close"
                    CssClass="btn btn-sm btn-danger" />
            </div>
        </asp:Panel>
        <asp:HiddenField runat="server" ID="hffirstClick" />
        <asp:HiddenField runat="server" ID="hfanyBlank" />
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                </div>
                <div class="box">
                    <asp:GridView ID="Grd_OpenCRF" runat="server" AutoGenerateColumns="False" OnRowDataBound="Grd_OpenCRF_RowDataBound"
                        CssClass="table table-bordered table-striped" OnRowCommand="Grd_OpenCRF_RowCommand" HeaderStyle-CssClass="align-left">
                        <Columns>
                            <asp:TemplateField HeaderText="Module Name">
                                <ItemTemplate>
                                    <asp:Label ID="MODULEID" runat="server" Text='<%# Bind("MODULEID") %>' CssClass="disp-none"></asp:Label>
                                    <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>' Font-Bold="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="STATUS" runat="server" Text='<%# Bind("STATUS") %>' Enabled="false"
                                        Font-Bold="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Go To Page" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lnkPAGENUM" runat="server" Style="height: 20px;" CommandName="GOTOPAGE"
                                        ImageUrl="Images/New_Page.png"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PAGESTATUS" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPAGESTATUS" runat="server" Text='<%# Bind("PAGESTATUS") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="text-center">
                                <HeaderTemplate>
                                    <label>Query</label><br />
                                    <label style="color: blue; font-weight: lighter;">[Open & Ans queries]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkQuery" ToolTip="Open Query" OnClientClick="return SAE_ShowOpenQuery_SAEID(this)" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:23px;color:maroon"></i>
                                    </asp:LinkButton>
                                    &nbsp;
                                                   <asp:LinkButton ID="lnkQUERYANS" ToolTip="Answered Open Query" OnClientClick="return SAE_ShowAnsQuery_SAEID(this);" runat="server">
                                                                     <i class="fa fa-question-circle" style="font-size:23px;color:blue;"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Page Comment" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnPageComment" CssClass="btn btn-warning btn-sm" runat="server"
                                        ToolTip="Page Comments" OnClientClick="return SAE_Show_Page_Comments_All(this);">
                                        <i class="fa fa-comments fa-2x" style="color: White;"></i>
                                        <asp:Label class="badge badge-info right" ID="lblPageComment" runat="server" Text="0"
                                            Style="margin-left: 5px;" Font-Bold="true"></asp:Label>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Reviewed Details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Reviewed By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server" id="divReviewedBy">
                                        <div>
                                            <asp:Label ID="REVIEWEDBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("REVIEWEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="REVIEWED_CAL_DAT" runat="server" Text='<%# Bind("REVIEWED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="REVIEWED_CAL_TZDAT" runat="server" Text='<%# Bind("REVIEWED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>SDV Details</label><br />
                                    <label style="color: brown; font-weight: lighter; margin-bottom: 0px;">[SDV Status]</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[SDV By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server" id="divSDVBY">
                                        <div>
                                            <asp:Label ID="SDV_STATUSNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("SDV_STATUSNAME") %>' ForeColor="Brown"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblSDVBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("SDVBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="SDV_CAL_DAT" runat="server" Text='<%# Bind("SDV_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="SDV_CAL_TZDAT" runat="server" Text='<%# Bind("SDV_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Medical Review Details</label><br />
                                    <label style="color: brown; font-weight: lighter; margin-bottom: 0px;">[Medical Review Status]</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Medical Review By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server" id="divMedicakBy">
                                        <div>
                                            <asp:Label ID="MR_STATUSNAME" runat="server" Text='<%# Bind("MR_STATUSNAME") %>' ForeColor="Brown"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblMRBYNAME" runat="server" Text='<%# Bind("MRBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="MR_CAL_DAT" runat="server" Text='<%# Bind("MR_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="MR_CAL_TZDAT" runat="server" Text='<%# Bind("MR_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Multiple YN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="MULTIPLEYN" runat="server" Text='<%# Bind("MULTIPLEYN") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="box box-cyan" id="divSignOff" runat="server">
            <div class="box-header" style="display: inline-flex; width: 100%;">
                <h3 class="box-title" style="width: 100%;">
                    <a href="JavaScript:divexpandcollapse('grdid');" id="_Folder"><i id="imggrdid" class="ion-plus-circled" style="font-size: larger; color: #666666"></i></a>
                    &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblHeader" runat="server" ForeColor="Blue" Text="Event Logs"></asp:Label>
                </h3>
            </div>
            <div class="box-body" id="grdid" style="display: none">
                <div class="box">
                    <asp:GridView ID="gridsigninfo" HeaderStyle-CssClass="text_center" HeaderStyle-ForeColor="Maroon" runat="server" OnPreRender="gridsigninfo_PreRender" CssClass="table table-bordered Datatable table-striped" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="pull-left">
            <asp:Button ID="btnBacktoHomePage" runat="server" Text="Back to Main Page" CssClass="btn btn-sm btn-primary"
                OnClick="btnBacktoHomePage_Click"></asp:Button>
        </div>
    </div>
    <asp:Label ID="Open_Request_temp" runat="server" ReadOnly="True" Text=""></asp:Label>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BehaviorID="mpe" PopupControlID="Panel11" TargetControlID="Open_Request_temp"
        CancelControlID="btnClose1" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel11" runat="server" CssClass="modalPopup" align="center" Style="border: 5px solid #ccc; display: none;">
        <div class="login-box">
            <div class="login-logo">
                <h1 style="color: #00001a!important; font-size: 30px;">
                    <a href="#"></a>
                    <b>Investigator Electronic Sign off</b>
                </h1>
                <br />
            </div>
            <div></div>
            <div></div>
            <div class="box">
                <div class="box-body login-box-body">
                    <asp:Label ID="Label2" runat="server" Style="color: #CC3300; font-size: 17px; font-weight: bold;"
                        ForeColor="Red"></asp:Label>
                    <div class="input-group mb-3" style="display: -ms-flexbox; display: flex; width: 100%; margin-bottom: 15px; border: 1px solid #008000; border-radius: 18px">
                        <asp:TextBox ID="txt_UserName" CssClass="noSpace" Style="width: 100%; height: 38px; padding: 10px; height: 38px; border: none; outline: none; border-radius: 0px; text-align: left; border-top-left-radius: 18px; border-bottom-left-radius: 18px;" runat="server" placeholder="User Id" autocomplete="off" class="numeric required"></asp:TextBox>
                        <span class="fa fa-user icon"></span>
                    </div>
                    <div class="input-group mb-3" style="display: -ms-flexbox; display: flex; width: 100%; margin-bottom: 15px; border: 1px solid #008000; border-radius: 18px">
                        <asp:TextBox ID="txt_Pwd" CssClass="noSpace" Style="width: 100%; height: 38px; padding: 10px; height: 38px; border: none; outline: none; border-radius: 0px; text-align: left; border-top-left-radius: 18px; border-bottom-left-radius: 18px;" runat="server" TextMode="Password" type="password" class="required" placeholder="Password" data-type="password" autocomplete="off"></asp:TextBox>
                        <span class="fa fa-lock icon"></span>
                    </div>
                    <div class="input-group mb-3" style="display: -ms-flexbox; display: flex; width: 100%; margin-bottom: 25px;">
                        <div class="col-1 text-center">
                            <asp:CheckBox ID="chkINVSIGN" runat="server" class="required" AutoPostBack="False" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                        <div class="col-11 text-center">
                            <div style="text-align: justify; margin-right: 10px;">By my dated signature below, I, confirm that I have reviewed the subject visit data in electronic case report form for completeness and accuracy.</div>
                        </div>
                    </div>
                    <div class="input-group mb-3" style="display: -ms-flexbox; display: flex; width: 100%; margin-bottom: 15px;">
                        <div class="col-6 text-center" style="text-align: center">
                            <asp:Button ID="Btn_Login1" runat="server" class="btn1" type="submit" Style="height: 38px; width: 120px; color: #fff; background-color: #0026ff; border-color: #2e6da4; left: 100px; top: 150px; margin-left: 70px; border-radius: 18px;" value="Submit" Text="Submit" OnClick="Btn_Login_Click" />

                        </div>
                        <div class="col-6 text-center" style="display: flex; justify-content: center">
                            <asp:Button ID="btnClose1" class="btn1" Style="height: 38px; color: #fff; width: 120px; background-color: #0026ff; border-color: #0026ff; position: absolute; right: 0; margin-right: 70px; border-radius: 18px;" runat="server" Text="Cancel" />
                        </div>
                        <!-- /.col -->
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <cc2:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlDeleteSAE" TargetControlID="Button1"
        BackgroundCssClass="Background" BehaviorID="modalpnlDeleteSAE">
    </cc2:ModalPopupExtender>
    <asp:Panel ID="pnlDeleteSAE" runat="server" Style="display: none;" CssClass="Popup1">
        <asp:Button runat="server" ID="Button1" Style="display: none" />
        <h5 class="heading">Reason for SAE Downgrading</h5>
        <div class="modal-body" runat="server">
            <div id="mdl">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <label>Note: Once you submit the downgrading request, this SAE will be send to medical team for approval. And, after approval, the SAE wiil get deleted automatically.</label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtDeleteReason" TextMode="MultiLine" CssClass="form-control required1" Style="width: 575px; height: 131px;"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-3">
                        &nbsp;
                    </div>
                    <div class="col-md-9">
                        <asp:Button ID="btnSubmitDeleteSAE" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-DarkGreen btn-sm cls-btnSave1"
                            Text="Submit" OnClick="btnSubmitDeleteSAE_Click" />
                        &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelDeleteSAE" runat="server" Text="Cancel"
                                CssClass="btn btn-danger" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelDeleteSAE_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <!-- /model popup  for Authentication end-->
    <cc2:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlSAESupportdoc" TargetControlID="Button2"
        BackgroundCssClass="Background">
    </cc2:ModalPopupExtender>
    <asp:Panel ID="pnlSAESupportdoc" runat="server" Style="display: none;" CssClass="Popup2">
        <asp:Button runat="server" ID="Button2" Style="display: none" />
        <h5 class="heading">SAE Supporting Document</h5>
        <div class="modal-body2" runat="server">
            <div class=" box box-warning">
                <div class="row">
                    <asp:Label ID="Label1" runat="server" Style="color: maroon; font-weight: bold; font-size: 17px; margin-left: 3%; text-decoration: underline;">Note: Please mask confidential information prior to uploading.</asp:Label>
                </div>
                <div class="row" style="margin-top: 5px;">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label>Select File :</label>
                        </div>
                        <div class="col-md-2">
                            <asp:FileUpload ID="UpldSupportingDoc" runat="server" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label>Notes :</label>
                        </div>
                        <div class="col-md-10" style="display: inline-flex;">
                            <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" CssClass="form-control required2" MaxLength="500" Style="width: 470px; height: 70px; margin-right: 10px;"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                        &nbsp;
                    </div>
                    <div class="col-md-9">
                        <asp:Button ID="BtnsubmitSupportingDoc" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-DarkGreen cls-btnSave2"
                            Text="Upload" OnClick="BtnsubmitSupportingDoc_Click" />
                        &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelSuportDoc" runat="server" Text="Cancel" OnClick="btnCancelSuportDoc_Click"
                                CssClass="btn btn-danger" Style="height: 34px; width: 71px; font-size: 14px;" />
                    </div>
                </div>
                <br />
            </div>
            <div class=" box box-primary" id="divgrdSupport_Doc" runat="server" visible="false">
                <div class="col-md-12">
                    <div style="width: 100%; height: 264px; overflow: auto; margin-top: 10px;">
                        <asp:GridView ID="grdSupport_Doc" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped" OnRowCommand="grdSupport_Doc_RowCommand" OnRowDataBound="grdSupport_Doc_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                    ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnDeleted" runat="server" Value='<%#Eval("DELETED") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblDownloadSupportDoc" runat="server" CommandArgument='<%# Bind("ID") %>' CssClass="btn"
                                            CommandName="DownloadSupportDoc" ToolTip="Download"><i class="fa fa-download"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileName" runat="server" Text='<%# Bind("FILENAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notes">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("NOTES") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="align-left">
                                    <HeaderTemplate>
                                        <label>Uploaded By Details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Uploaded By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <div>
                                                <asp:Label ID="lblUploaded" runat="server" Text='<%# Bind("UPLOADBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblUPLOAD_CAL_DAT" runat="server" Text='<%# Bind("UPLOAD_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblUPLOAD_CAL_TZDAT" runat="server" Text='<%# Bind("UPLOAD_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="align-left">
                                    <HeaderTemplate>
                                        <label>Deleted By Details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Deleted By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <div>
                                                <asp:Label ID="lblDELETEDBYNAME" runat="server" Text='<%# Bind("DELETEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblDELETED_CAL_DAT" runat="server" Text='<%# Bind("DELETED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblDELETED_CAL_TZDAT" runat="server" Text='<%# Bind("DELETED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblDeleteSupportDoc" runat="server" CommandArgument='<%# Bind("ID") %>' OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this file : ", Eval("FILENAME")) %>'
                                            CommandName="DeleteSupportDoc" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
