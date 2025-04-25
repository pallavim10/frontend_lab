<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NSAE_MODULE_DATA_LOGS_SDV.aspx.cs" Inherits="CTMS.NSAE_MODULE_DATA_LOGS_SDV" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script src="CommonFunctionsJs/DisableButton.js"></script>
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/SAE/SAE_Grid_Queries.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_Grid_Comments.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_DelayReason.js"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
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
    <script type="text/javascript">

        function ConfirmMsg() {
            var newLine = "\r\n"

            var error_msg = "Are you sure, you want to close this SAE?..";

            error_msg += newLine;
            error_msg += newLine;

            error_msg += "Note : Press OK to Proceed.";

            if (confirm(error_msg)) {

                $find('modalpnlCloseSAE').show();
                return false;

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
                    &nbsp;
                </div>
            </h3>
            <div class="pull-right" style="margin-top: 3px;">
                <div style="display: inline-flex;">
                    <asp:LinkButton ID="lbtnSupportingDocs" runat="server" Text="Supporting Document" ToolTip="Supporting Document" Height="27px" ForeColor="White" CssClass="btn btn-sm btn-success" OnClick="lbtnSupportingDocs_Click">
                        <i class="fa fa-files-o"></i>&nbsp;&nbsp;Supporting Document&nbsp;&nbsp;<asp:Label ID="lblCount" runat="server" CssClass="badge badge-info" Font-Bold="true" ForeColor="Black" Font-Size="13px" Text=""></asp:Label>
                    </asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDelayedReason" runat="server" Text="Delayed Reason" Visible="false" ToolTip="Delayed Reason" CssClass="btn btn-sm btn-warning" OnClientClick="return OpenDelayedReason();" />
                </div>
            </div>
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
    </div>

    <div class="box-body">
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
            </div>
            <div class="box">
                <asp:GridView ID="Grd_OpenCRF" runat="server" AutoGenerateColumns="False" OnRowDataBound="Grd_OpenCRF_RowDataBound"
                    CssClass="table table-bordered table-striped" OnRowCommand="Grd_OpenCRF_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="MODULE NAME">
                            <ItemTemplate>
                                <asp:Label ID="MODULEID" runat="server" Text='<%# Bind("MODULEID") %>' CssClass="disp-none"></asp:Label>
                                <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'
                                    Font-Bold="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="STATUS">
                            <ItemTemplate>
                                <asp:Label ID="STATUS" runat="server" Text='<%# Bind("STATUS") %>'
                                    Font-Bold="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GO TO PAGE" ItemStyle-CssClass="txt_center">
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
                        <asp:TemplateField HeaderText="QUERY" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkQuery" ToolTip="Open Query" OnClientClick="return SAE_ShowOpenQuery_SAEID(this)" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:23px;color:maroon"></i>
                                </asp:LinkButton>
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
                        <asp:TemplateField HeaderText="Internal Comment" ItemStyle-CssClass="txt_center" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnInternalComment" ToolTip="Internal Comments" OnClientClick="return SAE_Show_Internal_Comments_All(this);" CssClass="btn btn-info btn-sm" runat="server" class="disp-none Comments">
                                    <i class="fa fa-comment-medical fa-2x" style="color: white;"></i>
                                    <asp:Label class="badge badge-info right" ID="lblInternalComment_Count" runat="server" Text="0"
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
                                        <asp:Label ID="REVIEWEDBYNAME" runat="server" Font-Bold="true" Text='<%# Bind("REVIEWEDBYNAME") %>' ForeColor="Blue"></asp:Label>
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
                                        <asp:Label ID="SDV_STATUSNAME" runat="server" Font-Bold="true" Text='<%# Bind("SDV_STATUSNAME") %>' ForeColor="Brown"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="lblSDVBYNAME" runat="server" Font-Bold="true" Text='<%# Bind("SDVBYNAME") %>' ForeColor="Blue"></asp:Label>
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
                <div class="fixTableHead">
                    <asp:GridView ID="gridsigninfo" HeaderStyle-CssClass="text_center" HeaderStyle-ForeColor="Maroon" runat="server" OnPreRender="gridsigninfo_PreRender" CssClass="table table-bordered Datatable table-striped" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div class="pull-left">
        <asp:Button ID="btnBacktoHomePage" runat="server" Text="Back to Main Page" CssClass="btn btn-sm btn-primary"
            OnClick="btnBacktoHomePage_Click"></asp:Button>
    </div>
    <cc2:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="pnlSAESupportdoc" TargetControlID="Button2"
        BackgroundCssClass="Background">
    </cc2:ModalPopupExtender>
    <asp:Panel ID="pnlSAESupportdoc" runat="server" Style="display: none;" CssClass="Popup2">
        <asp:Button runat="server" ID="Button2" Style="display: none" />
        <div>
            <h5 class="heading">SAE Supporting Document
                <div class="align-right" style="display: -webkit-inline-box; margin-left: 80%;">
                    <asp:LinkButton D="btnSuportDocCancel" runat="server" Text="Closed" OnClick="btnSuportDocCancel_Click" Style="font-size: 20px; color: white;"><i class="fa fa-times"></i></asp:LinkButton>
                </div>
            </h5>
        </div>
        <div class="modal-body2" runat="server" style="height: 264px;">
            <div class=" box box-primary" runat="server">
                <div class="col-md-12">
                    <div style="width: 100%; height: 264px; overflow: auto; margin-top: 10px;">
                        <asp:GridView ID="grdSupport_Doc" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped Datatable" OnPreRender="grdSupport_Doc_PreRender" OnRowCommand="grdSupport_Doc_RowCommand">
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
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
