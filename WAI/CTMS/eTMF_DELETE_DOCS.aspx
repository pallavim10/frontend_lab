<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_DELETE_DOCS.aspx.cs" Inherits="CTMS.eTMF_DELETE_DOCS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script language="javascript" type="text/javascript">

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                fixedHeader: true
            });

        }

        $(document).ready(function () {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                fixedHeader: true
            });

        });

    </script>
    <style>
        .layer1 {
            color: #0000ff;
        }

        .layer2 {
            color: #800000;
        }

        .layer3 {
            color: #008000;
        }

        .layer4 {
            color: Black;
        }

        .layerFiles {
            color: #800000;
            font-style: italic;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Delete Documents</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row" id="div2" runat="server">
            <div class="col-md-12">
                <div class="col-md-2">
                    <label>Zone:</label>
                </div>
                <div class="col-md-8">
                    <asp:Label ID="lblZone" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div3" runat="server">
            <div class="col-md-12">
                <div class="col-md-2">
                    <label>Section:</label>
                </div>
                <div class="col-md-8">
                    <asp:Label ID="lblSection" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div4" runat="server">
            <div class="col-md-12">
                <div class="col-md-2">
                    <label>Artifact: </label>
                </div>
                <div class="col-md-8">
                    <asp:Label ID="lblArtifact" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div5" runat="server">
            <div class="col-md-12">
                <div class="col-md-2">
                    <label>Sub-Artifact:</label>
                </div>
                <div class="col-md-8">
                    <asp:Label ID="lblSubArtifact" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div6" runat="server">
            <div class="col-md-12">
                <div class="col-md-2">
                    <label>File Name:</label>
                </div>
                <div class="col-md-8">
                    <asp:Label ID="lblFilename" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div1" runat="server">
            <div class="col-md-12">
                <div class="col-md-2">
                    <label>Reason:</label>
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="txtReason" runat="server" CssClass="form-control required" TextMode="MultiLine"
                        Height="60" Width="300"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div12" runat="server" style="padding-top: 15px; padding-bottom: 15px;">
            <div class="col-md-12">
                <div class="col-md-3">
                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-primary btn-sm"
                        OnClick="btnback_Click" />
                </div>
                <div class="col-md-8">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                        OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
