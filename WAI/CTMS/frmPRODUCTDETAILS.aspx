<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmPRODUCTDETAILS.aspx.cs" Inherits="CTMS.frmPRODUCTDETAILS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="box box-warning">
 <div class="box-header">
            <h3 class="box-title">
        Product Details</h3></div>
  
     <div class="box-body">
        <div class="form-group">
            <div class="form-group has-warning">
             <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
            </div>  
                 </div>
      </div>
      <div class="box">
    <asp:GridView ID="PRODUCTDETAILS" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped width300px"
        AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowDataBound="PROJDETAILS_RowDataBound1">
        <Columns>
            <asp:TemplateField HeaderText="Product ID" ItemStyle-CssClass="width120px">
                <ItemTemplate>
                    <asp:TextBox ID="PRODUCTID" runat="server" 
                        Text='<%# Bind("PRODUCTID") %>' CssClass="numeric form-control" Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Product Name" ItemStyle-CssClass="width120px">
                <ItemTemplate>
                    <asp:TextBox ID="PRODUCTNAM" runat="server"
                        Text='<%# Bind("PRODUCTNAM") %>' CssClass="form-control" Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>     
             <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Button ID="bntAdd" runat="server" onclick="bntAdd_Click"  Cssclass="btn btn-primary btn-sm" 
                         Text="Add" />
                </HeaderTemplate>
            </asp:TemplateField>

        </Columns>      
    </asp:GridView>
    <table>
   <tr>
            <td class="label">
                <asp:Button ID="bntSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm cls-btnSave margin-top6" 
        OnClick="bntSave_Click" /></td>
            <td class="style10">
                &nbsp;</td>

            <td class="style18">
             
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td> &nbsp;</td>
        </tr>
        </table>
    </div>


</asp:Content>
