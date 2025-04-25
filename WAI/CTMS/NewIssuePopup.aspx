<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewIssuePopup.aspx.cs"
    Inherits="CTMS.NewIssuePopup" %>

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
            $(".tabs-menu a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("current");
                $(this).parent().siblings().removeClass("current");
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
        .margin-right5
        {
            margin-right: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                New Issue</h3>
        </div>
        <div class="row">
            <div class="lblError margin-left20">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-horizontal col-sm-9">
        <div class="box-body">
            <table>
                <tr>
                    <td class="label margin-right5">
                        Project
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Drp_Project" runat="server" class="form-control drpControl required width120px"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label margin-right5">
                        Site ID
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_InvID" runat="server" AutoPostBack="true" class="form-control drpControl required width120px margin-left15"
                                    OnSelectedIndexChanged="drp_InvID_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="label margin-right5">
                        Subject ID
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_SUBJID" runat="server" AutoPostBack="true" class="form-control drpControl required width120px">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label margin-right5">
                        Department
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_DEPT" runat="server" AutoPostBack="true" class="form-control drpControl required width120px margin-left15">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="label margin-right5">
                        Source
                    </td>
                    <td>
                        <asp:TextBox ID="txtSource" runat="server" ReadOnly="True" class="form-control required width120px"></asp:TextBox>
                    </td>
                    <td class="label margin-right5">
                        Reference
                    </td>
                    <td>
                        <asp:TextBox ID="txtReference" runat="server" class="form-control width120px margin-left15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label margin-right5">
                        Event Code
                    </td>
                    <td>
                        <asp:TextBox ID="txtEventCode" runat="server" class="form-control  width120px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="label margin-right10">
                        Rule
                    </td>
                    <td>
                        <asp:TextBox ID="txtRule" runat="server" class="form-control  width120px" Style="margin-left: 16px"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label margin-right5">
                        Summary
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtSummary" runat="server" CssClass=" form-control required" TextMode="MultiLine"
                            Width="405px" Height="36px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label margin-right5">
                        Status
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_Status" runat="server" AutoPostBack="true" class="form-control drpControl required width120px">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label margin-right5">
                        Classification
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_Priority" runat="server" AutoPostBack="true" Width="120px"
                                    class="form-control drpControl required width120px margin-left15">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="label margin-right5">
                        Keywords
                    </td>
                    <td>
                        <asp:TextBox ID="txtKeywords" runat="server" class="form-control" Width="120px"></asp:TextBox>
                    </td>
                    <td class="label margin-right5">
                        Nature
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_Nature" runat="server" AutoPostBack="true" Width="120px"
                                    class="form-control drpControl required width120px margin-left15" OnSelectedIndexChanged="drp_Nature_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="label margin-right5">
                        Category
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_PDCode1" runat="server" AutoPostBack="true" Width="120px"
                                    class="form-control drpControl  width120px" OnSelectedIndexChanged="drp_PDCode1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label margin-right5">
                        Sub Category
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_PDCode2" runat="server" AutoPostBack="true" Width="120px"
                                    class="form-control drpControl  width120px margin-left15">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="label margin-right5">
                        Description
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtDescription" runat="server" class="form-control" TextMode="MultiLine"
                            Width="405px" Height="36px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label margin-right5">
                        Attachment
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" AllowMultiple="true" runat="server" EnableViewState="true"
                            Font-Size="X-Small" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="upload" Text="Upload" Font-Size="Small" OnClick="upload_Click"
                            CssClass="disp-none" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
            <div>
                <asp:GridView ID="grdAttachment" runat="server" Width="100%" AutoGenerateColumns="false"
                    CssClass="Gtable table-bordered table-striped margin-top4" AlternatingRowStyle-CssClass="alt"
                    PagerStyle-CssClass="pgr">
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Attachment" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="Name" runat="server" Text='<%# Bind("Name") %>' CssClass="form-control"
                                    TextMode="MultiLine" Width="100%" Height="40px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ContentType" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:TextBox ID="ContentType" runat="server" Font-Size="X-Small" Text='<%# Bind("ContentType") %>'
                                    Width="22px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Attachments" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:TextBox ID="Attachments" runat="server" Font-Size="X-Small" Text='<%# Bind("Attachments") %>'
                                    Width="22px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                </asp:GridView>
            </div>
        </div>
        <div class="txt_center">
            <asp:Button ID="bntSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm cls-btnSave"
                OnClick="bntSave_Click" />
        </div>
    </div>
    </form>
</body>
</html>
