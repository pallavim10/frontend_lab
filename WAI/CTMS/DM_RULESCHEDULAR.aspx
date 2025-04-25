<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_RULESCHEDULAR.aspx.cs" Inherits="CTMS.DM_RULESCHEDULAR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--" || value == "0") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrpt" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Rule Schedular
            </h3>
        </div>
        <div class="lblError">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: small;"></asp:Label>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <div class="label col-md-8">
                                Run Rules Periods(in Days) : &nbsp;
                            <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtrulePeriod" CssClass="form-control required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="label col-md-8">
                                Start Date :&nbsp;
                            <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtdate" CssClass="form-control txtDate required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="label col-md-8">
                                Start Time :&nbsp;
                            <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txttime" CssClass="form-control txtTime required"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <br />
                <div class="row txt_center">
                    <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lbnUpdate" Text="Delete" ForeColor="White" CssClass="btn btn-danger btn-sm" Visible ="false" OnClick="lbnUpdate_Click"></asp:LinkButton>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
