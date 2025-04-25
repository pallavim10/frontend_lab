<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Remove_AnticipatedProject_Risk.aspx.cs" Inherits="CTMS.Remove_AnticipatedProject_Risk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    function ViewRECIDDETAILS(element) {

        var RiskId = $(element).closest('tr').find('td:eq(0)').find('span').attr('commandargument');
        var TYPE = "UPDATE";

        var test = "Risk_Details.aspx?RiskId=" + RiskId + "&TYPE=" + TYPE;

        var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=900";
        window.open(test, '_blank', strWinProperty);
        return false;
    }

     </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title"> Remove Anticipate Project Risk
                </h3></div>  
     <div class="box-body">
       
        <div class="form-group">
                        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
    <table>
        <tr>
            <td class="label">
                Select Project:
            </td>
            <td class="requiredSign">
                <asp:Label ID="Lbl_Sel_Dept" runat="server" Text="*"></asp:Label>
            </td>
            <td class="Control">
                <asp:DropDownList ID="Drp_Project_Name" runat="server"  class="form-control drpControl required"
                  >
                </asp:DropDownList>
            </td>
            <td class="style10">
              <asp:Button ID="Btn_Get_Fun" runat="server" OnClick="Btn_Get_Fun_Click" Text="Get Risk"
                    CssClass="btn btn-primary btn-sm cls-btnSave" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
              
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="label" colspan="5">
        </tr>
    </table>
    </div>
    </div>
    </div>
      <div class="box">
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped">
        
        <Columns>
            <asp:TemplateField HeaderText="Risk ID" ItemStyle-CssClass="width20px">
                <ItemTemplate>
                    <asp:Label ID="lbl_RISK_ID" runat="server" CssClass="disp-none"  CommandArgument='<%#Eval("RISK_ID") %>'   Text='<%# Bind("RISK_ID") %>'></asp:Label>
                    <asp:LinkButton ID="lnk_RISK_ID"  Text='<%# Bind("RISK_ID") %>' CommandArgument='<%#Eval("RISK_ID") %>' OnClientClick="return ViewRECIDDETAILS(this);" CommandName="Risk_ID"  runat="server"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Risk Category">
                <ItemTemplate>
                    <asp:Label ID="lbl_Category" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Sub Category">
                <ItemTemplate>
                    <asp:Label ID="lbl_SubCategory" runat="server" Text='<%# Bind("SubCategory") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Factor">
                <ItemTemplate>
                    <asp:Label ID="lbl_Factor" runat="server" Text='<%# Bind("Factor") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Risk Considerations">
                <ItemTemplate>
                    <asp:Label ID="lbl_RiskCons" runat="server" Text='<%# Bind("RiskCons") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Impact">
                <ItemTemplate>
                    <asp:Label ID="lbl_Impact" runat="server" Text='<%# Bind("Impact") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Suggested Mitigation">
                <ItemTemplate>
                    <asp:Label ID="lbl_SugMitig" runat="server" Text='<%# Bind("SugMitig") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Suggested Risk category">
                <ItemTemplate>
                    <asp:Label ID="lbl_SugRiskCat" runat="server" Text='<%# Bind("SugRiskCat") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Risk Nature Static/Dynamic">
                <ItemTemplate>
                    <asp:Label ID="lbl_RiskNature" runat="server" Text='<%# Bind("RiskNature") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

                        <asp:TemplateField HeaderText="Transcelerate Category">
                <ItemTemplate>
                    <asp:Label ID="lbl_TransCat" runat="server" Text='<%# Bind("TransCat") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Transcelerate  Subcategory">
                <ItemTemplate>
                    <asp:Label ID="lbl_TransSubCat" runat="server" Text='<%# Bind("TransSubCat") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>



            <asp:TemplateField ItemStyle-CssClass="width30px">
                <HeaderTemplate>
                    <asp:Button ID="Btn_Add_Fun" runat="server" OnClick="Btn_Add_Fun_Click" Text="Remove"
                        CssClass="btn btn-primary btn-sm cls-btnSave" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
</asp:Content>
