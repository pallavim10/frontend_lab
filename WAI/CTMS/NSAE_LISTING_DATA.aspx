<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NSAE_LISTING_DATA.aspx.cs" Inherits="CTMS.NSAE_LISTING_DATA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
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
    <script type="text/javascript">

        function pageLoad() {
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
                    stateSave: true
                });
            }
            
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label ID="lblHeader" runat="server"></asp:Label>
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div runat="server" id="DivINV" style="display: inline-flex">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label ">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control required"
                                        OnSelectedIndexChanged="drpInvID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div style="display: inline-flex">
                            <label class="label ">
                                Subject ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btngetdata_Click" />&nbsp&nbsp&nbsp&nbsp
                        <asp:HiddenField ID="hdnTablename" runat="server" />
                        <div class="box-body">
                            <div class="rows">
                                <div>
                                    <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                        OnPreRender="grd_data_PreRender" OnRowDataBound="gridData_RowDataBound" OnRowCommand="gridData_RowCommand"
                                        CssClass="table table-bordered Datatable table-striped notranslate">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnCreateSAELog" runat="server" Text="LOG SAE" ForeColor="Blue"
                                                        CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
