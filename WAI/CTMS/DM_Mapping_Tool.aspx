<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_Mapping_Tool.aspx.cs" Inherits="CTMS.DM_Mapping_Tool" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
    <script type="text/jscript">
        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.required').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
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

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content">
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">Mapping Tool
                </h3>
            </div>
            <div class="form-group has-warning">
                <asp:HiddenField runat="server" ID="hfSHEETNAME" />
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: 15px;"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">Upload Excel Sheet
                        </h3>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-6">
                            <asp:FileUpload runat="server" ID="fileUpload" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="fileUpload"
                                ErrorMessage="Only Excel files are allowed"
                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xls|.XLS|.xlsx|.XLSX)$">
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnUpload" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm"
                                OnClick="btnUpload_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="Label1" CssClass="warning" runat="server" ForeColor="Red" Text="[ Note : Excel Sheet Columns should not contain Space. ]"></asp:Label>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
            <div class="col-md-6" id="divModule" runat="server" visible="false">
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">Select Module
                        </h3>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList runat="server" ID="ddlModule" Width="90%" CssClass="form-control required1"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                        <%--<div class="col-md-4">
                            <asp:Button ID="btnModule" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" />
                        </div>--%>
                    </div>
                    <br />
                    <br />
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="upl" runat="server">
            <ContentTemplate>
                <div class="row" id="divMapping" runat="server" visible="false">
                    <div class="col-md-6">
                        <div class="box box-info">
                            <div class="box-header">
                                <h3 class="box-title">Manage Mappings
                                </h3>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Select Field
                                        </label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList runat="server" ID="ddlField" Width="100%" CssClass="form-control required1">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-8 txt_center">
                                        <label>
                                            ( Map with )
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Select Excel Sheet Column
                                        </label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList runat="server" ID="ddlExcelCol" Width="100%" CssClass="form-control required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnAddMoreChild" runat="server" OnClick="lbtnAddMoreChild_Click"
                                            CssClass="btn-sm cls-btnSave">
                                            <i id="I1" runat="server" class="fa fa-plus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild2" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo2" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo2" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo2_Click">
                                            <i id="I2" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild3" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo3" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo3" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo3_Click">
                                            <i id="I3" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild4" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo4" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo4" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo4_Click">
                                            <i id="I4" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild5" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo5" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo5" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo5_Click">
                                            <i id="I5" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild6" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo6" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo6" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo6_Click">
                                            <i id="I6" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild7" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo7" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo7" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo7_Click">
                                            <i id="I7" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild8" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo8" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo8" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo8_Click">
                                            <i id="I8" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild9" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo9" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo9" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo9_Click">
                                            <i id="I9" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild10" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo10" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo10" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo10_Click">
                                            <i id="I10" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild11" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo11" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo11" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo11_Click">
                                            <i id="I11" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild12" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo12" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo12" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo12_Click">
                                            <i id="I12" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild13" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo13" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo13" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo13_Click">
                                            <i id="I13" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild14" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo14" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo14" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo14_Click">
                                            <i id="I14" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild15" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo15" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo15" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo15_Click">
                                            <i id="I15" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild16" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo16" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo16" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo16_Click">
                                            <i id="I16" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild17" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo17" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo17" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo17_Click">
                                            <i id="I17" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild18" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo18" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo18" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo18_Click">
                                            <i id="I18" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild19" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo19" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo19" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo19_Click">
                                            <i id="I19" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divChild20" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            &nbsp;</label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:DropDownList ID="ddlChildNo20" Visible="false" runat="server" Width="100%" class="form-control drpControl required">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnCloseChildNo20" runat="server" CssClass="btn-sm" OnClick="lbtnCloseChildNo20_Click">
                                            <i id="I20" runat="server" class="fa fa-minus" style="color: #151515"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-8 txt_center">
                                        <label>
                                            OR
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Enter Default Value
                                        </label>
                                    </div>
                                    <div class="col-md-7" style="display: inline-flex;">
                                        <asp:TextBox runat="server" ID="txtDefaultVal" Width="100%" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:Label ID="Label2" CssClass="warning" runat="server" ForeColor="Red" Text="[ Note : SUBJECT No., VISIT and INVESTIGATOR ID has to be Mapped with given Sequence. ]"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-8 txt_center">
                                        <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                            OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-success" style="min-height: 284px;">
                            <div class="box-header with-border">
                                <h4 class="box-title" align="left">Mappings
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="rows">
                                            <div style="width: 100%; height: 284px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grdMap" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdMap_RowCommand"
                                                        OnRowDataBound="grdMap_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Set as Primary Field" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="width50px"
                                                                ItemStyle-CssClass="width50px">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnSetPrim" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="SetPrim" ToolTip="Set as Primary"><i class="fa fa-square-o"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtnRemovePrim" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="RemovePrim" ToolTip="Remove from Primary"><i class="fa fa-check-square-o"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Field" ItemStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="FIELDNAME" runat="server" Style="text-align: left" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Excel Column / Default Value" ItemStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ExcelCol" runat="server" Style="text-align: left" Text='<%# Bind("SHEET_COLUMN") %>'></asp:Label>
                                                                    <asp:Label ID="Val" runat="server" Style="text-align: left" Text='<%# Bind("Val") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="width10px"
                                                                ItemStyle-CssClass="width10px">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="DeleteMap" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
