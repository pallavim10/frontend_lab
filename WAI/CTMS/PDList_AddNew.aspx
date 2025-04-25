<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDList_AddNew.aspx.cs"
    Inherits="CTMS.PDList_AddNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>Diagonsearch</title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon">
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <%-- <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />--%>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <!-- for jquery Datatable.1.10.15//-->
    <script src="Scripts/Datatable.1.10.15.UI.js" type="text/javascript"></script>
    <script src="Scripts/Datatable1.10.15.js" type="text/javascript"></script>
    <!-- for jquery Datatable.1.10.15//-->
    <script src="js/AdminLTE/app.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <style>
        
    </style>
    <script type="text/javascript">



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
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });



        $(document).ready(function () {
            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        });

        function pageLoad() {
            $(".Datatable").DataTable();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    // trigger: $(element).closest('div').find('.datepicker-button').get(0), // <<<<
                    // firstDay: 1,
                    //position: 'top right',
                    // minDate: new Date('2000-01-01'),
                    // maxDate: new Date('9999-12-31'),
                    format: 'DD-MMM-YYYY',
                    //  defaultDate: new Date(''),
                    //setDefaultDate: false,
                    yearRange: [1910, 2050]
                });
            });
        }

        
    </script>
    <style>
        .txt_center
        {
            text-align: center;
        }
        .style1
        {
        }
        .style2
        {
            width: 377px;
        }
        
        .label
        {
            max-width: 100%;
            font-weight: bold;
            font-size: 11px;
            margin-left: 9px;
            width: 91px;
        }
        .form-control
        {
            width: 120px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">
                    Protocol Deviation</h3>
            </div>
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <table>
                <tr>
                    <td class="label">
                        Project
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Drp_Project" runat="server" ForeColor="Blue" class="form-control drpControl required"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Site ID
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_InvID" runat="server" ForeColor="Blue" AutoPostBack="true"
                                    class="form-control drpControl required" OnSelectedIndexChanged="drp_InvID_SelectedIndexChanged1">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Subject ID
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_SUBJID" runat="server" ForeColor="Blue" AutoPostBack="true"
                                    class="form-control drpControl required ">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Department
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_DEPT" runat="server" ForeColor="Blue" AutoPostBack="true"
                                    class="form-control drpControl required">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Visit No.
                    </td>
                    <td>
                        <asp:TextBox ID="txtVISITNUM" runat="server" ForeColor="Blue" class="form-control"></asp:TextBox>
                    </td>
                    <td class="label">
                        Date of Identified
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateIdentified" ReadOnly="true" ForeColor="Blue" runat="server"
                            class="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Status
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_Status" runat="server" ForeColor="Blue" AutoPostBack="true"
                                    class="form-control drpControl required">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Source
                    </td>
                    <td>
                        <asp:TextBox ID="txtSource" runat="server" ForeColor="Blue" class="form-control"></asp:TextBox>
                    </td>
                    <td class="label">
                        Reference
                    </td>
                    <td>
                        <asp:TextBox ID="txtReference" runat="server" ForeColor="Blue" class="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Date of Ocuurence
                    </td>
                    <td>
                        <asp:TextBox ID="txtOCDate" runat="server" class="form-control txtDate"></asp:TextBox>
                    </td>
                    <td class="label">
                        Date of Report
                    </td>
                    <td>
                        <asp:TextBox ID="txtCloseDate" runat="server" class="form-control txtDate"></asp:TextBox>
                    </td>
                    <td class="label">
                        Count
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtPdmasterID" runat="server" Visible="false" class="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtCount" runat="server" Style="color: Red" class="form-control txt_center"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Description
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txtDescription" runat="server" class="form-control required" TextMode="MultiLine"
                            Width="700px" Height="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Category
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList runat="server" ID="drp_Nature" ForeColor="Maroon" CssClass="form-control required"
                                    AutoPostBack="true" OnSelectedIndexChanged="drp_Nature_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Sub Category
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList runat="server" ID="drp_PDCode1" ForeColor="Maroon" CssClass="form-control required"
                                    AutoPostBack="True" OnSelectedIndexChanged="drp_PDCode1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Factor
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList runat="server" ID="drp_PDCode2" ForeColor="Maroon" CssClass="form-control required"
                                    OnSelectedIndexChanged="drp_PDCode2_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Duplicacy
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList runat="server" ID="ddlDuplicacy" ForeColor="Maroon" CssClass="form-control">
                                    <asp:ListItem Selected="True" Text="New" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Possibly Duplicate" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Duplicate" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Classification
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Priority_Default" runat="server" ForeColor="Maroon" class="form-control">
                                </asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Classification by Medical
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_Priority_Med" runat="server" ForeColor="Maroon" class="form-control drpControl">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Classification by Statistician
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_Priority_Ops" runat="server" ForeColor="Maroon" class="form-control drpControl">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Final Classification
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_Priority_Final" runat="server" ForeColor="Maroon" class="form-control drpControl ">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        &nbsp;
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        Summary
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txtSummary" runat="server" CssClass=" form-control" TextMode="MultiLine"
                            Width="700px" Height="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Final Summary
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txtRationalise" runat="server" class="form-control" TextMode="MultiLine"
                            Width="700px" Height="50px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        <asp:Button ID="bntSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm cls-btnSave margin-left10 "
                            OnClick="bntSave_Click" />
                    </h3>
                </div>
            </div>
        </div>
        <div class="disp-none col-md-8">
            <div id="tabscontainer" class="nav-tabs-custom" runat="server">
                <ul class="nav nav-tabs">
                    <li class="active"><a href="#tab-1" data-toggle="tab">Comments</a></li>
                    <li><a href="#tab-2" data-toggle="tab">Impact</a></li>
                    <li><a href="#tab-3" data-toggle="tab">CAPA</a></li>
                </ul>
                <div class="tab">
                    <div id="tab-1" class="tab-content current">
                        <asp:GridView ID="grdCmts" runat="server" Width="100%" AutoGenerateColumns="false"
                            CssClass="Gtable table-bordered table-striped margin-top4" AlternatingRowStyle-CssClass="alt"
                            PagerStyle-CssClass="pgr" OnRowDataBound="grdCmts_RowDataBound">
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Comments" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Comment" runat="server" Text='<%# Bind("Comment") %>' CssClass="form-control"
                                            TextMode="MultiLine" Width="100%" Height="40px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EnteredDate" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="DTENTERED" Text='<%# Bind("DTENTERED") %>'> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EnteredBy" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="ENTEREDBY" Text='<%# Bind("ENTEREDBY") %>'> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UPDATE_FLAG_cmt" HeaderStyle-CssClass="disp-none"
                                    ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:TextBox ID="UPDATE_FLAG_cmt" runat="server" Font-Size="X-Small" Text='<%# Bind("UPDATE_FLAG_cmt") %>'
                                            Width="22px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="width30px" ItemStyle-CssClass="30px" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:Button ID="bntCommentAdd" runat="server" CssClass="btn btn-primary btn-sm" Text="Add"
                                            OnClick="bntCommentAdd_Click" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr"></PagerStyle>
                        </asp:GridView>
                    </div>
                    <div id="tab-2" class="tab-content">
                        <asp:GridView ID="grdImpact" runat="server" AutoGenerateColumns="false" CssClass="Gtable table-bordered table-striped margin-top4"
                            AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdImpact_RowDataBound" PagerStyle-CssClass="pgr">
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="PROTVOIL ID" ItemStyle-CssClass="txt_center width80px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="PROTVOIL_ID" runat="server" Text='<%# Bind("PROTVOIL_ID") %>' ReadOnly="true"
                                            Width="60px" Style="text-align: center"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Impact" ItemStyle-CssClass="txt_center width250px">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="Impact" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UPDATE_FLAG_Impact" HeaderStyle-CssClass="disp-none"
                                    ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:TextBox ID="UPDATE_FLAG_Impact" runat="server" Font-Size="X-Small" Text='<%# Bind("UPDATE_FLAG_Impact") %>'
                                            Width="22px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="width30px" ItemStyle-CssClass="txt_center">
                                    <HeaderTemplate>
                                        <asp:Button ID="Add" runat="server" CssClass="btn btn-primary btn-sm" Text="Add"
                                            OnClick="bntImpactAdd_Click" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr"></PagerStyle>
                        </asp:GridView>
                    </div>
                    <div id="tab-3" class="tab-content">
                        <asp:GridView ID="grdCAPA" runat="server" Width="100%" AutoGenerateColumns="false"
                            CssClass="Gtable table-bordered table-striped margin-top4" AlternatingRowStyle-CssClass="alt"
                            PagerStyle-CssClass="pgr" OnRowDataBound="grdCAPA_RowDataBound">
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="PROTVOIL ID" ItemStyle-CssClass="txt_center width80px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="PROTVOIL_ID" runat="server" Text='<%# Bind("PROTVOIL_ID") %>' ReadOnly="true"
                                            Width="60px" Style="text-align: center"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CAPA" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CAPA" runat="server" Text='<%# Bind("CAPA") %>' CssClass="form-control width250pximp" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Responsibility" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="Responsibility" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resolution Date" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Resolution_DT" runat="server" Text='<%# Bind("Resolution_DT") %>'
                                            CssClass="form-control txtDate" Width="100px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UPDATE_FLAG_CAPA" HeaderStyle-CssClass="disp-none"
                                    ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:TextBox ID="UPDATE_FLAG_CAPA" runat="server" Font-Size="X-Small" Text='<%# Bind("UPDATE_FLAG_CAPA") %>'
                                            Width="22px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="width30px" ItemStyle-CssClass="30px" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:Button ID="btnAction" runat="server" CssClass="btn btn-primary btn-sm" Text="Add"
                                            OnClick="bntCAPAAdd_Click" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr"></PagerStyle>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
