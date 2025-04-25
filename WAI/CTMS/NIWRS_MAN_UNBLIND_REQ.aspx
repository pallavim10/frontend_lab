<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_MAN_UNBLIND_REQ.aspx.cs" Inherits="CTMS.NIWRS_MAN_UNBLIND_REQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script>
        function SelectOther() {

            if ($('#MainContent_drpReason').val() == 'Others') {
                $('#MainContent_divOthers').removeClass('disp-none');
                $('#MainContent_txtReason').addClass('required');
            }
            else {
                $('#MainContent_divOthers').addClass('disp-none');
                $('#MainContent_txtReason').removeClass('required');
            }

        }


        $(document).ready(function () {

            $('.txtTime').each(function (index, element) {
                $(element).inputmask(
                    "hh:mm", {
                    placeholder: "HH:MM",
                    insertMode: false,
                    showMaskOnHover: false,
                    hourFormat: "24"
                });
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Manual Unblinding</h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="rows">
                                <div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label>
                                                    Select Site :</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList runat="server" ID="drpSites" CssClass="form-control required width200px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="drpSites_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row disp-none">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label>
                                                    Select Sub-Site :</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList runat="server" ID="drpSubSites" CssClass="form-control width200px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="drpSubSites_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label>
                                                    Select &nbsp;<asp:Label runat="server" ID="SUBJECTTEXT"></asp:Label>&nbsp; | Randomization No. :</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList runat="server" ID="drpSUBJID" CssClass="form-control required width200px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label>
                                                    Select Reason for Unblinding :</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList runat="server" onchange="return SelectOther();" ID="drpReason"
                                                    CssClass="form-control required width200px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divOthers" class="disp-none">
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-6">
                                                    <label>
                                                        Please Specify Others :</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox runat="server" ID="txtReason" Height="50px" TextMode="MultiLine" CssClass="form-control width200px"> 
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label>
                                                    Date of Unblinding :</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox runat="server" ID="txtUnblindDate" CssClass="form-control required txtDateNoFuture width200px"> 
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label>
                                                    Time of Unblinding :</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox runat="server" ID="txtUnblindTime" CssClass="form-control required txtTime width200px"> 
                                                </asp:TextBox>
                                                <asp:Label runat="server" ForeColor="Red" Text="[in 24 hour format]"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <label>
                                                    Treatment Arm provided by Unblinded Site Personnel :</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList runat="server" ID="ddlTreatmentArm" CssClass="form-control required width200px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Button ID="btnSubmitReq" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    OnClick="btnSubmitReq_Click" />&nbsp;&nbsp;
                                                <asp:Button ID="btnCancelREQ" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btnCancelREQ_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
