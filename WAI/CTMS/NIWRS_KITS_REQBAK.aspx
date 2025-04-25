<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_KITS_REQBAK.aspx.cs" Inherits="CTMS.NIWRS_KITS_REQBAK" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function SelectOther() {

            if ($('#MainContent_ddlReason').val() == 'Others') {
                $('#MainContent_divOthers').removeClass('disp-none');
            }
            else {
                $('#MainContent_divOthers').addClass('disp-none');
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <asp:HiddenField runat="server" ID="hfDispenseIDX" />
        <div class="box-header">
            <h3 class="box-title">
                Request Backup Kit
            </h3>
        </div>
        <asp:UpdatePanel runat="server" ID="updPanel">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                            <asp:HiddenField ID="hfControlType" runat="server" />
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="label col-md-2">
                                        Subject :
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblSubject" runat="server" Width="100%" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="label col-md-2">
                                        Visit :
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblVisit" runat="server" Width="100%" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="label col-md-2">
                                        Randomization No. :
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblRandNo" runat="server" Width="100%" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="label col-md-2">
                                        Replacement for Kit No. :
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblOldKitNo" runat="server" Width="100%" CssClass="form-control"></asp:Label>
                                        <asp:Label ID="lblNewKitNo" runat="server" Width="0%" Visible="false" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="label col-md-2">
                                        Select Reason :
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlReason" onchange="return SelectOther();"
                                            Width="100%" CssClass="form-control required">
                                        </asp:DropDownList>
                                    </div>
                                    <div runat="server" id="divOthers" class="disp-none">
                                        <div class=" label col-md-2">
                                            <label>
                                                Please Specify Others :</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtReason" Height="50px" Width="100%" TextMode="MultiLine"
                                                CssClass="form-control width200px"> 
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div runat="server" id="divSponsorApprov" visible="false">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Sponsor Approval :
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="drpSponsorApproval" Width="100%" CssClass="form-control required">
                                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                                <asp:ListItem Text="Disapproved" Value="Disapproved"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="label col-md-2">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-2">
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                                    OnClick="btnsubmit_Click" Style="margin-left: 20px;" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                                    OnClick="btnCancel_Click" Style="margin-left: 20px;" />
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsubmit" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
