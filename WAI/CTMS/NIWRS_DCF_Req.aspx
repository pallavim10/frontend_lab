<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_DCF_Req.aspx.cs" Inherits="CTMS.NIWRS_DCF_Req" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();
        }
    </script>
    <script type="text/javascript">
        function SelectOther(element) {

            if ($("#MainContent_drpReason").val() == 'Others') {
                $('#MainContent_divOthers').removeClass('disp-none');
            }
            else {
                $('#MainContent_divOthers').addClass('disp-none');
            }

        }
    </script>
   <%--  <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Raise Data Correction Form Request
            </h3>
        </div>
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
                                Select Site :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlSite" runat="server" Width="80%" AutoPostBack="true" class="form-control drpControl required"
                                    OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="disp-none label col-md-2">
                                Select Sub-Site :
                            </div>
                            <div class="disp-none col-md-3">
                                <asp:DropDownList ID="ddlSubSite" runat="server" Width="80%" AutoPostBack="true"
                                    class="form-control drpControl" OnSelectedIndexChanged="ddlSubSite_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select &nbsp;<asp:Label runat="server" ID="SUBJECTTEXT"></asp:Label>&nbsp; :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlSubject" runat="server" SelectionMode="Single" Width="80%" AutoPostBack="true"
                                    class="form-control drpControl required select" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Visit
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlVisit" runat="server" Width="80%" AutoPostBack="true" class="form-control drpControl required"
                                    OnSelectedIndexChanged="ddlVisit_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Form :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlForm" runat="server" Width="80%" AutoPostBack="true" class="form-control drpControl required"
                                    OnSelectedIndexChanged="ddlForm_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Field :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlField" runat="server" Width="80%" AutoPostBack="true" class="form-control drpControl required"
                                    OnSelectedIndexChanged="ddlField_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div runat="server" id="divData" visible="false" class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Old Value :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="DRP_FIELD_old" runat="server" onchange="checkSpecs(this);"
                                    Visible="false" Width="80%">
                                </asp:DropDownList>
                                <asp:TextBox ID="TXT_FIELD_old" runat="server" Width="80%" autocomplete="off" Visible="false"
                                    onchange="checkSpecs(this);"></asp:TextBox>
                                <asp:Repeater runat="server" ID="repeat_CHK_old">
                                    <ItemTemplate>
                                        <div class="col-md-4">
                                            <asp:CheckBox ID="CHK_FIELD_old" runat="server" onchange="checkSpecs(this);" CssClass="checkbox"
                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater runat="server" ID="repeat_RAD_old">
                                    <ItemTemplate>
                                        <div class="col-md-4">
                                            <asp:RadioButton ID="RAD_FIELD_old" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                onchange="checkSpecs(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                Text='<%# Bind("TEXT") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select/Enter New Value :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="DRP_FIELD_new" runat="server" onchange="checkSpecs(this);"
                                    Visible="false" Width="80%">
                                </asp:DropDownList>
                                <asp:TextBox ID="TXT_FIELD_new" runat="server" Width="80%" autocomplete="off" Visible="false"
                                    onchange="checkSpecs(this);"></asp:TextBox>
                                <asp:Repeater runat="server" ID="repeat_CHK_new">
                                    <ItemTemplate>
                                        <div class="col-md-4">
                                            <asp:CheckBox ID="CHK_FIELD_new" runat="server" onchange="checkSpecs(this);" CssClass="checkbox"
                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater runat="server" ID="repeat_RAD_new">
                                    <ItemTemplate>
                                        <div class="col-md-4">
                                            <asp:RadioButton ID="RAD_FIELD_new" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                onchange="checkSpecs(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                Text='<%# Bind("TEXT") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Enter Description :
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtDesc" Width="80%" Height="60px" TextMode="MultiLine"
                                    CssClass="form-control required"> 
                                </asp:TextBox>
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
                                <asp:DropDownList runat="server" Width="80%" onchange="return SelectOther();" ID="drpReason"
                                    CssClass="form-control required">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div runat="server" id="divOthers" class="disp-none">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Please Specify Others :
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtReason" Height="50px" TextMode="MultiLine" CssClass="form-control width200px"> 
                                    </asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    &nbsp;
                                </div>
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-2">
                        </div>
                        <div class="col-lg-10">
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
    </div>
</asp:Content>
