<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_DOC_QC_COMMENT.aspx.cs" Inherits="CTMS.eTMF_DOC_QC_COMMENT" %>

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

        .brd-1px-redimp {
            border: 2px solid !important;
            border-color: Red !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Add QC Comment</h3>
        </div>
        <asp:HiddenField ID="hffirstClick" runat="server" />
        <asp:HiddenField ID="hfanyNo" runat="server" />
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row" id="div6" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2" style="color: blue;">
                    File Name:
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblfilename" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div2" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2" style="color: blue;">
                    Correct Nomenclature :
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlDocNom" CssClass="form-control required">
                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div4" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2" style="color: blue;">
                    Legibility of Document :
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlDocLegible" CssClass="form-control required">
                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div8" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2" style="color: blue;">
                    Correct Orientation :
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlDocOrient" CssClass="form-control required">
                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div3" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2" style="color: blue;">
                    Filed in Correct Location :
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlDocLocate" CssClass="form-control required">
                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div10" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2" style="color: blue;">
                    Is the document attributable :
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlDocAttr" CssClass="form-control required">
                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div7" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2" style="color: blue;">
                    Is the document Complete :
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlDocComplete" CssClass="form-control required">
                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
      
        <div class="row" id="div5" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2" style="color: blue;">
                    Action :
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlAction" CssClass="form-control required">
                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approve" Value="Approved"></asp:ListItem>
                        <asp:ListItem Text="Reject" Value="Rejected"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div1" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2" style="color: blue;">
                    Comment:
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="MultiLine"
                        Height="60" Width="300"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div12" runat="server" style="padding-top: 15px; padding-bottom: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2" style="color: blue;">
                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-primary btn-sm"
                        OnClick="btnback_Click" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                        OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
