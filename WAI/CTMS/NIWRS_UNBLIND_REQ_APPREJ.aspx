<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_UNBLIND_REQ_APPREJ.aspx.cs" Inherits="CTMS.NIWRS_UNBLIND_REQ_APPREJ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <style>
        .btn1:hover {
            background-color: #2e6da4 !important;
            color: white !important;
        }

        .input-group i {
            position: absolute !important;
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
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #fff;
            border: 3px solid #ccc;
            padding: 10px;
            width: 450px;
        }
    </style>
<%--    <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Unblinding Request</h3>
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
                                <div style="height: 264px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Site :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label runat="server" ID="lblSite" CssClass="form-control width200px"> 
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row disp-none">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Sub-Site :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label runat="server" ID="lblSubSite" CssClass="form-control width200px"> 
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    &nbsp;<asp:Label runat="server" ID="SUBJECTTEXT"></asp:Label>&nbsp; / Randomization No. :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label runat="server" ID="lblSUB_RAND" CssClass="form-control width200px"> 
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Reason for Unblinding :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label runat="server" ID="lblReason" Height="50px" Width="50%" CssClass="form-control">
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Request Date-Time :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label runat="server" ID="lblREQDT" CssClass="form-control width200px"> 
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Approval Decision :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList runat="server" ID="drpDecision" CssClass="form-control required width200px">
                                                    <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                                    <asp:ListItem Text="Not Approved" Value="Not Approved"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Comment (if Any) :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox runat="server" ID="txtReason" Height="50px" Width="50%" TextMode="MultiLine"
                                                    CssClass="form-control"> 
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-7">
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

    <asp:Label ID="Open_Request_temp" Style="display: none;" runat="server" Text="">.</asp:Label>


    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BehaviorID="mpe" PopupControlID="Panel11" TargetControlID="Open_Request_temp"
        CancelControlID="btnClose1" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel11" runat="server" CssClass="modalPopup" align="center" Style="border: 5px solid #ccc">
        <div class="login-box">
            <div class="login-logo">
                <h1 style="color: #00001a!important; font-size: 45px;">
                    <a href="#"></a>
                    <b>Sign off</b>
                </h1>
            </div>
            <div></div>
            <div></div>
            <div class="box">
                <div class="box-body login-box-body">
                    <asp:Label ID="Label2" runat="server" Style="color: #CC3300; font-size: 17px; font-weight: bold;"
                        ForeColor="Red"></asp:Label>
                    <div class="input-group mb-3" style="display: -ms-flexbox; display: flex; width: 100%; margin-bottom: 15px; border: 1px solid #008000; border-radius: 18px">
                        <asp:TextBox ID="txt_UserName" Style="width: 100%; height: 38px; padding: 10px; height: 38px; border: none; outline: none; border-radius: 0px; text-align: left; border-top-left-radius: 18px; border-bottom-left-radius: 18px;" runat="server" placeholder="User Id" autocomplete="off" class="numeric"></asp:TextBox>
                        <span class="fa fa-user icon"></span>
                    </div>
                    <div class="input-group mb-3" style="display: -ms-flexbox; display: flex; width: 100%; margin-bottom: 15px; border: 1px solid #008000; border-radius: 18px">
                        <asp:TextBox ID="txt_Pwd" Style="width: 100%; height: 38px; padding: 10px; height: 38px; border: none; outline: none; border-radius: 0px; text-align: left; border-top-left-radius: 18px; border-bottom-left-radius: 18px;" runat="server" TextMode="Password" type="password" placeholder="Password" data-type="password" autocomplete="off"></asp:TextBox>
                        <span class="fa fa-lock icon"></span>
                    </div>
                    <div class="input-group mb-3" style="display: -ms-flexbox; display: flex; width: 100%; margin-bottom: 15px;">
                        <div class="col-1 text-center">
                            <asp:CheckBox ID="chkINVSIGN" runat="server" class="required" AutoPostBack="False" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                         
                        </div>
                        <div class="col-11 text-center">
                            <div style="text-align: justify; margin-right: 10px;">With re-entry of my login credentials, I confirm that I have checked the Participant Id and would want to approve the Unblinding request in the interest of subject safety.</div>
                        </div>
                    </div>
                    <div class="input-group mb-3" style="display: -ms-flexbox; display: flex; width: 100%; margin-bottom: 15px;">
                        <div class="col-6 text-center" style="text-align: center">
                            <asp:Button ID="Btn_Login1" runat="server" class="btn1" type="submit" Style="height: 38px; width: 120px; color: #fff; background-color: #0026ff; border-color: #2e6da4; left: 100px; top: 150px; margin-left: 70px; border-radius: 18px;" value="Submit" Text="Submit" OnClick="Btn_Login_Click" />
                        </div>
                        <div class="col-6 text-center" style="display: flex; justify-content: center">
                            <asp:Button ID="btnClose1" class="btn1" Style="height: 38px; color: #fff; width: 120px; background-color: #0026ff; border-color: #0026ff; position: absolute; right: 0; margin-right: 70px; border-radius: 18px;" runat="server" Text="Cancel" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
