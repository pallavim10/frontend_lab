<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DB_STATUS.aspx.cs" Inherits="CTMS.DB_STATUS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="CommonFunctionsJS/TabIndex.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DivExpandCollapse.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });
            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
        
     
    </script>

    <script type="text/javascript">
        function Check_Version() {
            if ($('#MainContent_txtVesion').val().trim() == '') {
                alert('Please enter DB  Version');
                return false;
            }
        };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <contenttemplate>
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">DB Status</h3>
            </div>
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div class="box-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="box box-success" id="div1" visible="false" style="margin-right: inherit;" runat="server">
                                        <div class="box-header">
                                            <h3 class="box-title">DB Migration</h3>
                                        </div>
                                        <div class="rows">
                                            <div style="height: 120px;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-5">
                                                            <label>Current DB Version Number :</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:Label ID="lblversion" runat="server" Text="" CssClass="form-control align-center" TabIndex="1"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-5">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:Button ID="btnmigrate" runat="server" CssClass="btn btn-primary btn-sm" Text="Migrate" OnClick="btnmigrate_Click" OnClientClick="return confirm('Do you want to migrate this DB?');" TabIndex="2" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box box-success" id="div2" visible="false" style="margin-right: inherit;" runat="server">
                                        <div class="box-header">
                                            <h3 class="box-title">DB Migration</h3>
                                        </div>
                                        <div class="rows">
                                            <div style="height: 120px;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-5">
                                                            <label>Current DB Version Number :</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:Label ID="lblver" runat="server" Text="" CssClass="form-control align-center" TabIndex="3"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-5">
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:Button ID="UnlockDBVersion" runat="server" CssClass="btn btn-primary btn-sm" Text="Unlock DB" OnClick="UnlockDBVersion_Click" OnClientClick="return confirm('Are you sure you want to unlock this version?');" TabIndex="4" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6" id="divgrid" runat="server" style="margin-left: inherit;" visible="false">
                                    <div class="box box-success">
                                        <div class="box-header">
                                            <h3 class="box-title">List of modules pending for review </h3>
                                        </div>
                                        <br />
                                        <div class="rows">
                                            <div style="height: 350px;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="rows">
                                                            <div class="col-md-12">
                                                                <label style="color: red">Note: Please mark the module below as frozen, then migrate the database.</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="col-md-12">
                                                        <div class="rows">
                                                            <div class="col-md-12">
                                                                <div style="height: 264px; overflow: auto;">
                                                                    <asp:GridView ID="grd_data" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                        CssClass="table table-bordered table-striped Datatable" OnPreRender="grd_data_PreRender">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMODULEID" Text='<%# Eval("MODULEID") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Module Name" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmodule" Text='<%# Eval("MODULENAME") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel5" TargetControlID="Button_STATUS"
                BackgroundCssClass="Background">
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel5" runat="server" Style="display: none;" CssClass="Popup1">
                <asp:Button runat="server" ID="Button_STATUS" Style="display: none" />
                <div class="heading">
                    <h5 class="box-title">Define Vision
                        <asp:LinkButton ID="btnclosed" runat="server" Font-Size="14px" ForeColor="White" OnClick="btnclosed_Click" style="margin-top: -9px;" CssClass="btn btn-danger pull-right">&nbsp;Close&nbsp;<span class="fa fa-times"></span></asp:LinkButton>

                    </h5>
                </div>
                <div class="modal-body" runat="server">
                    <div id="ModelPopupReturn">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-7">
                                    <asp:Label ID="Label3" runat="server" Text="Current DB Version Number :" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblverion" runat="server" CssClass="form-control-model align-center" Style="color: black; font-weight: 600; font-size: 14px; width: 130px;"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-10">
                                    <asp:Label ID="lbnote" runat="server" Text="Note :" Style="color: black; font-weight: 600; font-size: 14px;">Are you sure you want to continue with the old version?</asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="col-md-12" id="divNo" runat="server" visible="false">
                                <div class="col-md-7">
                                    <asp:Label ID="Label1" runat="server" Text="Entered New DB Version Number:" Style="color: black; font-weight: 600; font-size: 14px; width: 130px;"></asp:Label>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtVesion" CssClass="form-control-model align-center" ValidationGroup="VISION" runat="server" Style="width: 130px;"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-5">
                                        <asp:Button ID="btnYes" runat="server" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnYes_Click" CssClass="btn btn-DarkGreen"
                                            OnClientClick="return confirm('Are you sure want to continue this Version?');" Text="Yes" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnNo" runat="server" Style="height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-danger"
                                            OnClick="btnNo_Click" Text="No" />

                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-5">
                                        <asp:Button ID="btnSubmit" runat="server" Visible="false" Style="height: 34px; width: 71px; font-size: 14px;" ValidationGroup="VISION" CssClass="btn btn-DarkGreen"
                                            OnClientClick="return Check_Version();" Text="Submit" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnback" runat="server" Visible="false" Style="height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-danger"
                                            Text="Back" OnClick="btnback_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
         <div class="box box-cyan" id="divStatusLog" runat="server">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">
                <a href="JavaScript:divexpandcollapse('grdid');" id="_Folder"><i id="imggrdid" class="ion-plus-circled" style="font-size: larger; color: #666666"></i></a>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblHeader" runat="server" ForeColor="Blue" Text="Status Logs"></asp:Label>
            </h3>
        </div>
        <div class="box-body" id="grdid" style="display: none">
            <div class="box">
                <div class="fixTableHead">
                    <asp:GridView ID="gridstatuslogs" HeaderStyle-CssClass="text_center" HeaderStyle-ForeColor="Maroon" runat="server" OnPreRender="gridstatuslogs_PreRender" CssClass="table table-bordered Datatable table-striped" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    </contenttemplate>
</asp:Content>
