<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="eTMF_ForReview.aspx.cs" Inherits="CTMS.eTMF_ForReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />


    <script language="javascript" type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });
        }

        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }

        function DownloadDoc(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').find('input').val();

            var test = "CTMS_DownloadDoc.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=520,width=900";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'icon-plus-sign-alt') {
                img.className = 'icon-minus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
            } else {
                img.className = 'icon-plus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
            }
        }

        function checkReviewAll(element) {
            $('input[type=checkbox][id*=CheckReView]').each(function () {
                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }
            });
        }

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

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--") {
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
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">For Review 
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnID" runat="server" />
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-1">
                            Zones :
                        </div>
                        <div class="col-md-2" style="display: flex">
                            <asp:DropDownList ID="ddlZone" Width="200px" runat="server" class="form-control drpControl required1"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-1">
                            Sections :
                        </div>
                        <div class="col-md-2" style="display: flex">
                            <asp:DropDownList ID="ddlSections" Width="200px" runat="server" class="form-control drpControl required1">
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-1" style="width: 120px;">
                            Review Status :
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlReviewstatus" Width="120px" runat="server" class="form-control drpControl required1">
                                <asp:ListItem Value="-1">--Select--</asp:ListItem>
                                <asp:ListItem Value="1"> Reviewed </asp:ListItem>
                                <asp:ListItem Value="0"> Non-Reviewed</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="display: inline-flex">
                            <asp:Button ID="btnGetData" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1" Text="Get Data" OnClick="btnGetData_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
    <div id="tabscontainer" class="nav-tabs-custom" runat="server">
        <ul class="nav nav-tabs">
            <li id="li1" runat="server" class="active"><a href="#tab-1" data-toggle="tab">Review</a></li>
            <li id="li2" runat="server"><a href="#tab-2" data-toggle="tab">History</a></li>
        </ul>
        <div class="tab">
            <div id="tab-1" class="tab-content current">
                <div id="MianContain" runat="server" visible="false">
                    <div class="box-header box box-primary">
                        <div class="col-md-12">
                            <h5>
                                <label>Important Points to Note :</label>
                                <ul>
                                    <li>
                                        <label>Please click on Select All to Check(<i class="icon-check" style="color: #333333;" aria-hidden="true"></i>) or UnCheck(<i class="icon-check-empty" style="color: #333333;" aria-hidden="true"></i>)&nbsp; multiple Sub-Artifacts at once.</label>
                                    </li>
                                    <li>
                                        <label>Please click on Submit Review button after you Check(<i class="icon-check" style="color: #333333;" aria-hidden="true"></i>) or UnCheck(<i class="icon-check-empty" style="color: #333333;" aria-hidden="true"></i>) all applicable Sub-Artifacts.</label>
                                    </li>
                                    <li>
                                        <label>Reviewed Sub-Artifacts will not be available for editing in Manage Expected Documents (i.e. Configuration will be locked).</label>
                                    </li>
                                    <li>
                                        <label>Sub-Artifacts which are Pending for Review will be available for editing in Manage Expected Documents.</label>
                                    </li>
                                    <li>
                                        <label>Reviewed Sub-Artifacts which are UnChecked(<i class="icon-check-empty" style="color: #333333;" aria-hidden="true"></i>) while reviewing will be available for editing in Manage Expected Documents (i.e. Configuration will be unlocked).</label>
                                    </li>
                                </ul>
                            </h5>
                        </div>
                    </div>
                    <div style="text-align: center">
                        <asp:Button ID="btnReview" runat="server" CssClass="btn btn-primary" OnClick="btnReview_Click" Text="Submit Review" />
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-11" style="text-align: right">
                                <asp:Label Font-Bold="true" runat="server">  
                                    Select All :
                                </asp:Label>
                            </div>
                            <div class="col-md-1">
                                <asp:CheckBox ID="ChkAllReview" runat="server" AutoPostBack="false" OnClick="checkReviewAll(this)" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="box-body">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvArtifact" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="gvArtifact_RowDataBound"
                                    CssClass="table table-bordered table-striped layer3">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                            HeaderStyle-CssClass="txt_center">
                                            <HeaderTemplate>
                                                <a href="JavaScript:ManipulateAll('_Docs');" id="_SubFolder" style="color: #333333"><i
                                                    id="img_Docs" class="icon-plus-sign-alt"></i></a>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server" id="anchor">
                                                    <a href="JavaScript:divexpandcollapse('_Docs<%# Eval("ID") %>');" style="color: #333333">
                                                        <i id="img_Docs<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                            ItemStyle-CssClass="disp-none" HeaderText="MainRefNo">
                                            <ItemTemplate>
                                                <asp:Label ID="MainRefNo" runat="server" CssClass="form-control" Text='<%# Bind("MainRefNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                            ItemStyle-CssClass="disp-none" HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" runat="server" CssClass="form-control" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                            ItemStyle-CssClass="disp-none" HeaderText="DocTypeId">
                                            <ItemTemplate>
                                                <asp:Label ID="DocTypeId" runat="server" CssClass="form-control" Text='<%# Bind("DocTypeId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref." ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_RefNo" Width="100%" onclick="ShowDocs(this);"
                                                    CssClass="label" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Artifacts" ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Artifact" Width="100%"
                                                    CssClass="label" runat="server" Text='<%# Bind("Artifact_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Definition">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Definition" Width="100%"
                                                    CssClass="label" runat="server" Text='<%# Bind("DEFINITION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%" style="padding: 2px;">
                                                        <div style="float: right; font-size: 13px;">
                                                        </div>
                                                        <div>
                                                            <div class="rows">
                                                                <div class="col-md-12">
                                                                    <div id="_Docs<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                        <br />
                                                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                <asp:HiddenField ID="lblReview" runat="server" Value='<%# Eval("REVIEW") %>' />
                                                                                <div class="panel panel-primary">
                                                                                    <div class="panel-heading">
                                                                                        <h5 class="box-title" style="margin-bottom: unset; margin-top: unset;"><a href="JavaScript:divexpandcollapse('_SubDocs<%# Eval("ID") %>');" style="color: #333333">
                                                                                            <i id="img_SubDocs<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                                                                <b>Document :</b> &nbsp;
                                                                                                                                                                <asp:Label ID="Label17" runat="server" Font-Bold="true" Font-Size="17px" Text='<%# Bind("DocName") %>'></asp:Label>
                                                                                            <asp:CheckBox ID="CheckReView" CssClass="eTMFCheckbox" runat="server" Style="float: right" />
                                                                                        </h5>
                                                                                    </div>
                                                                                    <div class="box-body" id="_SubDocs<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                                        <br />
                                                                                        <div class="form-group">
                                                                                            <div class="row">
                                                                                                <div class="col-md-4">
                                                                                                    <div class="col-md-5">
                                                                                                        <asp:Label Font-Bold="true" runat="server">
                                                                                                                                                                                Ref No.:
                                                                                                        </asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-7">
                                                                                                        <asp:Label ID="lblRefNo" runat="server" CssClass="form-control" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4 disp-none">
                                                                                                    <div class="col-md-5">
                                                                                                        <asp:Label Font-Bold="true" runat="server">
                                                                                                                                                                                Unique RefNo. :
                                                                                                        </asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-7">
                                                                                                        <asp:Label ID="lblUniqueRefNo" runat="server" CssClass="form-control" Text='<%# Bind("UniqueRefNo") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    <div class="col-md-5">
                                                                                                        <asp:Label Font-Bold="true" runat="server">
                                                                                                                                                                             Replace Superseded Version?:
                                                                                                        </asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-7">
                                                                                                        <asp:Label ID="Label2" runat="server" CssClass="form-control" Text='<%# Bind("AutoReplace") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-md-4 disp-none">
                                                                                                    <div class="col-md-5">
                                                                                                        <asp:Label Font-Bold="true" runat="server">
                                                                                                                                                                                Email Enable :
                                                                                                        </asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-7">
                                                                                                        <asp:Label ID="Label14" runat="server" CssClass="form-control" Text='<%# (Boolean.Parse(Eval("SetEmail").ToString())) ? "Yes" : "No" %>'></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    <div class="col-md-5">
                                                                                                        <asp:Label Font-Bold="true" runat="server">
                                                                                                                                                                            Document Level :
                                                                                                        </asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-7">
                                                                                                        <asp:Label ID="Label3" runat="server" CssClass="form-control" Text='<%# Bind("VerTYPE") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    <div class="col-md-5">
                                                                                                        <asp:Label Font-Bold="true" runat="server">
                                                                                                                                                                                Ver. Date :
                                                                                                        </asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-7">
                                                                                                        <asp:Label ID="Label4" runat="server" CssClass="form-control" Text='<%# (Boolean.Parse(Eval("VerDate").ToString())) ? "Yes" : "No" %>'></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-md-4">
                                                                                                    <div class="col-md-5">
                                                                                                        <asp:Label Font-Bold="true" runat="server">
                                                                                                                                                                             Ver. Spec. :
                                                                                                        </asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-7">
                                                                                                        <asp:Label ID="Label5" runat="server" CssClass="form-control" Text='<%# (Boolean.Parse(Eval("VerSPEC").ToString())) ? "Yes" : "No" %>'></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    <div class="col-md-5">
                                                                                                        <asp:Label Font-Bold="true" runat="server">
                                                                                                                                                                            Spec. Convention :
                                                                                                        </asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-7">
                                                                                                        <asp:Label ID="Label6" runat="server" CssClass="form-control" Text='<%# Bind("SPECtitle") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    <div class="col-md-5">
                                                                                                        <asp:Label Font-Bold="true" runat="server">
                                                                                                                                                                             Date Convention :
                                                                                                        </asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-7">
                                                                                                        <asp:Label ID="Label7" runat="server" CssClass="form-control" Text='<%# Bind("DateTitle") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-md-4">
                                                                                                    <div class="col-md-5">
                                                                                                        <asp:Label Font-Bold="true" runat="server">
                                                                                                                                                                                Access Control :
                                                                                                        </asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-7">
                                                                                                        <asp:Label ID="Label13" runat="server" CssClass="form-control" Text='<%# Bind("Unblind") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    &nbsp;
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="col-md-1" style="width: 10.333333%;">
                                                                                                        <asp:Label Font-Bold="true" runat="server">Instruction :
                                                                                                        </asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-11" style="width: 96.666667%;">
                                                                                                        <asp:Label ID="Label1" CssClass="form-control" Style="min-height: 20px; min-width: 966px; width: auto; height: auto" runat="server" Text='<%# Bind("Info") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="col-md-1" style="width: 10.333333%;">
                                                                                                        <asp:Label Font-Bold="true" runat="server">
                                                                                                                                                                                Comment :
                                                                                                        </asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-11" style="width: 96.666667%;">
                                                                                                        <asp:Label ID="Label8" CssClass="form-control" Style="min-height: 20px; min-width: 966px; width: auto; height: auto" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <br />
                    <div class="row">
                        <div class=" col-md-12">
                            <div style="text-align: center">
                                <asp:Button ID="BtnReview1" runat="server" CssClass="btn btn-primary" OnClick="btnReview_Click" Text="Submit Review" />
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
            <div id="tab-2" class="tab-content">
                <div class="row box box-primary">
                    <div class="col-md-12">
                        <div class="box-header with-border">
                            <h4 class="box-title" align="left"></h4>
                            <div class="pull-right">
                                <asp:LinkButton ID="lbnsExport" runat="server" OnClick="lbnsExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export History&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <div class="box-body">
                            <div class="row">
                                <div style="width: 96%; overflow: auto; margin-left: 5px;">
                                    <asp:GridView ID="Grd_Review_Log" runat="server" AutoGenerateColumns="True" Width="98%"
                                        CssClass="table table-bordered table-striped Datatable" OnPreRender="Grd_Review_Log_PreRender">
                                    </asp:GridView>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
