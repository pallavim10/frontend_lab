<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ePRO_Subjects.aspx.cs" Inherits="CTMS.ePRO_Subjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrptmng" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <div runat="server" style="display: inline-flex; padding: 0px; margin: 4px 0px 0px 10px;"
                id="anchor">
                <a href="JavaScript:ManipulateAll('_HEADER_');" style="color: #333333"><i id="img_HEADER_"
                    class="icon-plus-sign-alt"></i></a>
                <h3 class="box-title" style="width: 100%;">
                    <asp:Label ID="lblHeader" runat="server" Font-Size="12px" Font-Bold="true" Font-Names="Arial"
                        Text="Subjects"></asp:Label>
                </h3>
            </div>
        </div>
        <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300;"></asp:Label>
    </div>
    <asp:Repeater runat="server" ID="repeatDATA" OnItemDataBound="repeatDATA_ItemDataBound">
        <ItemTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <div runat="server" style="display: inline-flex; padding: 0px; margin: 4px 0px 0px 10px;"
                        id="anchor">
                        <a href="JavaScript:divexpandcollapse('_HEADER_<%# Eval("MODULEID") %>');" style="color: #333333">
                            <i id="img_HEADER_<%# Eval("MODULEID") %>" class="icon-plus-sign-alt"></i></a>
                        <h3 class="box-title" style="padding-top: 0px;">
                            <asp:Label ID="lblHeader" runat="server" Font-Size="12px" Font-Bold="true" Font-Names="Arial"
                                Text='<%# Bind("HEADER") %>'></asp:Label>
                        </h3>
                    </div>
                </div>
                <asp:HiddenField runat="server" ID="hfTablename" Value='<%# Bind("EPRO_TABLENAME") %>' />
                <asp:HiddenField runat="server" ID="hfMODULEID" Value='<%# Bind("MODULEID") %>' />
                <asp:HiddenField runat="server" ID="hfDM_Tablename" Value='<%# Bind("TABLENAME") %>' />
                <div id="_HEADER_<%# Eval("MODULEID") %>" style="display: none; position: relative;
                    overflow: auto;">
                    <div class="box-body">
                        <div class="form-group">
                            <asp:GridView ID="gvDATA" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="false"
                                CssClass="table table-bordered table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="Question" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("VARIABLENAME") %>'
                                                Text='<%# Bind("VARIABLENAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Patient's Assessment" HeaderStyle-CssClass="align-left"
                                        ItemStyle-CssClass="align-left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPatDATA" Font-Size="Small" ToolTip='<%# Bind("DATA") %>' Text='<%# Bind("DATA") %>'
                                                Style="text-align: LEFT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Investigator's Assessment" HeaderStyle-CssClass="align-left"
                                        ItemStyle-CssClass="align-left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvDATA" Font-Size="Small" ToolTip='<%# Bind("InvDATA") %>' Text='<%# Bind("InvDATA") %>'
                                                Style="text-align: LEFT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate">
                            </asp:GridView>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
