<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SB_RateMaster.aspx.cs" Inherits="CTMS.SB_RateMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable1").dataTable({ "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: false
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning txt_center">
        <div class="box-header">
            <h3 class="box-title">
                Manage Task Rates</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="txt_center">
            <asp:GridView ID="gvRates" runat="server" AutoGenerateColumns="True" CssClass="table table-bordered table-striped Datatable1"
                OnPreRender="gvRates_PreRender" OnRowCreated="gvRates_RowCreated">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                ValidationGroup="section" />
            <br />
            <br />
        </div>
    </div>
</asp:Content>
