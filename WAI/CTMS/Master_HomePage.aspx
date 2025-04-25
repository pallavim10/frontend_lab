<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Master_HomePage.aspx.cs" Inherits="CTMS.Master_HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="div1" runat="server">
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
          <asp:ListView ID="lstm" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstm_ItemDataBound">
                <GroupTemplate>
                    <div class="col-lg-3 col-xs-6">
                        <asp:LinkButton ID="itemPlaceholder" runat="server" />
                    </div>
                </GroupTemplate>
                <ItemTemplate>
                    <!-- small box -->
                    <div id="divcol" runat="server">
                        <div class="inner">
                            <div style="font-size: x-large;">
                                <a id="main" runat="server" title='<%# Eval("FunctionName") %>' style="color: white;">
                                    <%# Eval("FunctionName") %></a>
                            </div>
                            <p>
                                <br />
                            </p>
                            <br />
                        </div>
                        <div class="icon">
                            <i class="ion ion-bag"></i>
                        </div>
                        <div class="small-box-footer">
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>
