<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_MODULE_FIELD_COMMENTS.aspx.cs" Inherits="CTMS.DM_MODULE_FIELD_COMMENTS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">
                <asp:Label ID="lblHeader" runat="server" Font-Size="12px" Font-Bold="true" Font-Names="Arial"
                    Text="Field Comments Report"></asp:Label>
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: 15px;"></asp:Label>
                <div class="box-body">
                    <div style="display: inline-flex;">
                        <div runat="server" id="divDDLS">
                            <div style="display: inline-flex">
                                <label class="label" style="color: Maroon;">
                                    Select Site Id:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpInvID" runat="server" ForeColor="Blue" AutoPostBack="True" OnSelectedIndexChanged="drpInvID_SelectedIndexChanged"
                                        CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="display: inline-flex">
                                <label class="label" style="color: Maroon;">
                                    Select Subject Id:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpSubID" runat="server" ForeColor="Blue" AutoPostBack="True" OnSelectedIndexChanged="drpSubID_SelectedIndexChanged"
                                        CssClass="form-control select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="display: inline-flex">
                                <label class="label" style="color: Maroon;">
                                    Select Visit:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpVisit" runat="server" ForeColor="Blue" AutoPostBack="True" OnSelectedIndexChanged="drpVisit_SelectedIndexChanged"
                                        CssClass="form-control ">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="display: inline-flex">
                                <label class="label" style="color: Maroon;">
                                    Select Module:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpModule" runat="server" ForeColor="Blue" AutoPostBack="True" OnSelectedIndexChanged="drpModule_SelectedIndexChanged"
                                        CssClass="form-control ">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave" Style="margin-left: 9%;"
                        OnClick="btngetdata_Click" />&nbsp&nbsp&nbsp&nbsp
                         <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel" CssClass="btn btn-info btn-sm"
                             Text="" Style="color: white" Font-Bold="true"> Export Field Comment&nbsp;&nbsp;<i class="fa fa-download"></i>
                         </asp:LinkButton>
                    <br />
                    <br />
                    <div class="box-body">
                        <div class="rows">
                            <div class="fixTableHead">
                                <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate">
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
