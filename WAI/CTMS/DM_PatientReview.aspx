<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_PatientReview.aspx.cs" Inherits="CTMS.DM_PatientReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/MM/MM_DivExpand.js"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Patient Review</h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpInvID" runat="server" OnSelectedIndexChanged="drpInvID_SelectedIndexChanged"
                                        AutoPostBack="True" CssClass="form-control required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Subject ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control required select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btngetdata_Click" />&nbsp&nbsp&nbsp <a href="JavaScript:ManipulateAll('_Pat_');"
                                id="_Folder" style="color: #333333; font-size: x-large; margin-top: 5px;"><i id="img_Pat_"
                                    class="icon-plus-sign-alt"></i></a>
                    </div>
                </div>
            </div>
            <asp:Repeater runat="server" OnItemDataBound="repeatData_ItemDataBound" ID="repeatData">
                <ItemTemplate>
                    <div class="box box-primary">
                        <div class="box-header">
                            <div runat="server" style="display: inline-flex; padding: 0px; margin: 4px 0px 0px 10px;"
                                id="anchor">
                                <a href="JavaScript:divexpandcollapse('_Pat_<%# Eval("ID") %>');" style="color: #333333">
                                    <i id="img_Pat_<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                <h3 class="box-title" style="padding-top: 0px;">
                                    <asp:Label ID="lblHeader" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                </h3>
                            </div>
                        </div>
                        <div id="_Pat_<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                            <div class="box-body">
                                <div class="rows">
                                    <div class="fixTableHead">
                                        <asp:HiddenField ID="hfLISTID" runat="server" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                        <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                            OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
            <asp:HiddenField ID="hdntranspose" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
