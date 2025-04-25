<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Code_Pending.aspx.cs" Inherits="CTMS.Code_Pending" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script type="text/javascript">
        function CODEPENIDNGPAGE(element) {
            var AutoCodedLIB = $("#<%= drpdictionary.ClientID %>").val();
            var AUTO_ID = $(element).closest('tr').find('td:eq(1)').text();
            var test = "Code_Action.aspx?AutoCodedLIB=" + AutoCodedLIB + "&AUTO_ID=" + AUTO_ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=330,width=1000";
            window.open(test, '_self');
            return false;
        }

        function Check_All_Approve(element) {

            $('input[type=radio][id*=Chek_Approve]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });

        }

        function Check_All_Disapprove(element) {

            $('input[type=radio][id*=Chek_Disapprove]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });

        }

        function Check_ApprovalReason() {

            if ($('#MainContent_txtApprovalComments').val().trim() == '') {
                alert('Please enter Comments');
                return false;
            }
        };

        function Check_DisapproveReasonEntered() {

            if ($('#MainContent_txtDisapproveComments').val().trim() == '') {
                alert('Please enter Comments');
                return false;
            }
        };

    </script>
     <script type="text/javascript">
         function pageLoad() {
             $(".Datatable").dataTable({
                 "bSort": false, "ordering": false,
                 "bDestroy": true, stateSave: true
             });

             $(".Datatable").parent().parent().addClass('fixTableHead');

         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="content">
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">Available for approval
                </h3>
            </div>
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="form-group">
                        <br />
                        <div style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label">
                                    Select Dictionary :
                                </label>
                                <div class="Control">
                                    <asp:DropDownList runat="server" ID="drpdictionary" CssClass="form-control required">
                                        <asp:ListItem Text="--Select--" Value="0">
                                        </asp:ListItem>
                                        <asp:ListItem Text="MedDRA" Value="MedDRA">
                                        </asp:ListItem>
                                        <asp:ListItem Text="WHODD" Value="WHODD">
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div style="display: inline-flex">
                            <div style="display: inline-flex">
                                <div class="Control">
                                    <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btngetdata_Click" />&nbsp&nbsp&nbsp&nbsp
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="box-body">
                        <div class="rows">
                            <div style="width: 100%; overflow: auto;">
                                <div>
                                    <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                        OnPreRender="gridData_PreRender" CssClass="table table-bordered Datatable table-striped" OnRowDataBound="gridData_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px txt_center">
                                                <HeaderTemplate>
                                                    <asp:Button ID="Approve" runat="server" CssClass="btn btn-primary btn-sm"
                                                        Text="Approve" OnClientClick="return Approve(this);" OnClick="Approve_Click" />
                                                    <br />
                                                    <asp:RadioButton ID="ChekAll_Approve" GroupName="All" runat="server" onclick="Check_All_Approve(this)" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="Chek_Approve" GroupName='<%# Bind("ID") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnID" Value='<%# Bind("ID") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px txt_center">
                                                <HeaderTemplate>
                                                    <asp:Button ID="Disapprove" runat="server" CssClass="btn btn-primary btn-sm"
                                                        Text="Disapprove" OnClientClick="return Disapprove(this);" OnClick="Disapprove_Click" />
                                                    <br />
                                                    <asp:RadioButton ID="ChekAll_Disapprove" GroupName="All" runat="server" onclick="Check_All_Disapprove(this)" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="Chek_Disapprove" GroupName='<%# Bind("ID") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <label>Coding Details</label><br />
                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Coded By]</label><br />
                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="RequestByName" runat="server" Text='<%# Bind("RequestByName") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="Request_CAL_DAT" runat="server" Text='<%# Bind("Request_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="Request_CAL_TZDAT" runat="server" Text='<%# Bind("Request_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="Request_TZVAL" runat="server" Text='<%# Bind("Request_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <br />
                        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="Button_Popup"
                            BackgroundCssClass="Background">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="Panel1" runat="server" Style="display: none;" CssClass="Popup1">
                            <asp:Button runat="server" ID="Button_Popup" Style="display: none" />
                            <h5 class="heading">Please Enter Comment</h5>
                            <div class="modal-body" runat="server">
                                <div id="ModelPopup">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="Label11" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtApprovalComments" CssClass="form-control-model" TextMode="MultiLine"
                                                runat="server" Style="width: 250px;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-9">
                                            <asp:Button ID="btnApproval" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="Commnet" CssClass="btn btn-DarkGreen"
                                                OnClientClick="return Check_ApprovalReason();" Text="Submit" OnClick="btnApproval_Click" />
                                            &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelApproval" runat="server" Text="Cancel" ValidationGroup="Commnet"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelApproval_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <br />
                        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel2" TargetControlID="Button_Disapprove"
                            BackgroundCssClass="Background">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="Panel2" runat="server" Style="display: none;" CssClass="Popup1">
                            <asp:Button runat="server" ID="Button_Disapprove" Style="display: none" />
                            <h5 class="heading">Please Enter Comment</h5>
                            <div class="modal-body" runat="server">
                                <div id="ModelPopupBlock">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="Label4" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtDisapproveComments" CssClass="form-control-model" ValidationGroup="DisapproveCommnet" TextMode="MultiLine"
                                                runat="server" Style="width: 250px;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-9">
                                            <asp:Button ID="btnSubmitDisapprove" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="DisapproveCommnet" CssClass="btn btn-DarkGreen"
                                                OnClientClick="return Check_DisapproveReasonEntered();" Text="Submit" OnClick="btnSubmitDisapprove_Click" />
                                            &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelDisapprove" runat="server" Text="Cancel" ValidationGroup="DisapproveCommnet"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelDisapprove_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
