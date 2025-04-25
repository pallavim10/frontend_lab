<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="User_Activity_Log.aspx.cs" Inherits="CTMS.User_Activity_Log" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <style>
        .select2-container .select2-selection--multiple {
            min-height: 20px;
        }
    </style>
    <script type="text/javascript">

        function pageLoad() {
            $('.select').select2();

            var transpose = $('#MainContent_hdntranspose').val();

            if (transpose == 'FieldNameVise') {

                $(".Datatable").dataTable({
                    "bSort": true,
                    "ordering": true,
                    "bDestroy": false,
                    stateSave: false,
                    aaSorting: [[1, 'asc']]
                });

            }
            else {

                $(".Datatable").dataTable({
                    "bSort": true,
                    "ordering": true,
                    "bDestroy": false,
                    stateSave: false
                });

            }

            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050]
                });
            });

            $('.txtTime').each(function (index, element) {
                $(element).inputmask(
                    "hh:mm", {
                    placeholder: "HH:MM",
                    insertMode: false,
                    showMaskOnHover: false,
                    hourFormat: "24"
                }
                );
            });

        }

        $(function () {
            $('.txtTime').inputmask(
                "hh:mm", {
                placeholder: "HH:MM",
                insertMode: false,
                showMaskOnHover: false,
                hourFormat: "24"
            }
            );
        });

    </script>
    <style type="text/css">
        .fontBlue {
            color: Blue;
            cursor: pointer;
        }

        .circleQueryCountRed {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Red;
        }

        .circleQueryCountGreen {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Green;
        }

        .YellowIcon {
            color: Yellow;
        }

        .GreenIcon {
            color: Green;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label ID="lblHeader" Text="User Activity Log" runat="server"></asp:Label>
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2" id="proj" runat="server">
                                    From:
                                </div>
                                <div class="col-md-3" style="display: inline-flex;" id="projname" runat="server">
                                    <asp:TextBox ID="txtDateFrom" CssClass="form-control required txtDate" runat="server"
                                        autocomplete="off" Width="100px"></asp:TextBox>
                                    <asp:TextBox ID="txtTimeFrom" CssClass="form-control required txtTime" runat="server"
                                        autocomplete="off" Width="60px" Text="00:00"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    To :
                                </div>
                                <div class="col-md-3" style="display: inline-flex;">
                                    <asp:TextBox ID="txtDateTo" CssClass="form-control required txtDate" runat="server"
                                        autocomplete="off" Width="100px"></asp:TextBox>
                                    <asp:TextBox ID="txtTimeTo" CssClass="form-control required txtTime" runat="server"
                                        autocomplete="off" Width="60px" Text="23:59"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Select User:
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpUser" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpUser_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    Select Section: &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpSection" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpSection_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Select  Function: &nbsp;
                                                <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpfunction" class="form-control" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    Select User Name: &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnGet" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        OnClick="btnGet_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                                        style="color: #333333" title="Export"></a>
                                    <ul class="dropdown-menu dropdown-menu-sm">
                                        <li>
                                            <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel"
                                                Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                                            </asp:LinkButton></li>
                                        <hr style="margin: 5px;" />
                                        <li>
                                            <asp:LinkButton runat="server" CssClass="dropdown-item" ID="btnPDF" OnClick="btnPDF_Click"
                                                ToolTip="Export to PDF" Text="Export to PDF" Style="color: #333333;">
                                            </asp:LinkButton></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <br />

                        <div class="box-body">
                            <div class="rows">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                            OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
                <asp:PostBackTrigger ControlID="btnPDF" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
