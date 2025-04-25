<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RM_Risk_List.aspx.cs" Inherits="CTMS.RM_Risk_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <script type="text/javascript">
     function ViewRECIDDETAILS(element) {
         var RiskId = $(element).closest('tr').find('td:eq(0)').find('span').attr('commandargument');
         var TYPE = "UPDATE";
         var test = "RM_Risk_MitigationDetails.aspx?RiskId=" + RiskId + "&TYPE=" + TYPE;



         var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=900";
         window.open(test, '_blank', strWinProperty);
         return false;
     }
     function AddRisk(element) {
         var RiskId = "";
         var TYPE = "NEW";
         var test = "RM_Risk_MitigationDetails.aspx?RiskId=" + RiskId + "&TYPE=" + TYPE;
         var strWinProperty = "toolbar=no,menubar=no,scrollbars=yes,titlebar=no,height=700,width=900";
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
            <h3 class="box-title">
                Risk List</h3>
        </div>
        <div class="row">
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="form-horizontal">            
        <div class="box-body">
            <%--<div class="form-group">
                <label class="col-lg-3 width150px label">
                    Select Department:</label>
                <div class="col-lg-8">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                             <ContentTemplate>
                             
                                
                    <asp:DropDownList ID="Drp_Department" class="form-control drpControl" 
                        AutoPostBack="true"  runat="server" 
                        onselectedindexchanged="Drp_Department_SelectedIndexChanged">
                    </asp:DropDownList>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group">
                <label class="col-lg-3 width150px label">
                    Select Status:</label>
                <div class="col-lg-8">
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                             <ContentTemplate>                          
                    <asp:DropDownList ID="drpStatus" class="form-control drpControl"  
                        AutoPostBack="true" runat="server" 
                        onselectedindexchanged="drpStatus_SelectedIndexChanged">
                    </asp:DropDownList>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>--%>
               <asp:Button ID="btnAdd" runat="server" Text="Get Data"   Visible="false"
                CssClass="btn btn-primary btn-sm cls-btnSave margin-left10" 
                OnClientClick="return ViewRECIDDETAILS(this);" onclick="btnAdd_Click" />
              <asp:Button ID="btnAddRisk" runat="server" Text="Add Risk"  Visible="false"
                CssClass="btn btn-primary btn-sm margin-left10"  
                OnClientClick="return AddRisk(this);" onclick="btnAddRisk_Click" />
        </div>
          </div>
        <div class="box">
         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                             <ContentTemplate>                           
            <asp:GridView ID="GrdRisk_list" runat="server" AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped" onrowcommand="GrdRisk_list_RowCommand">
                <Columns>
                   <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="txtCenter">
                        <ItemTemplate>
                            <asp:Label  runat="server" ID="lblID"  CssClass="disp-none" CommandArgument='<%#Eval("id") %>' ></asp:Label>
                             <asp:LinkButton ID="ID"  Text='<%# Bind("id") %>' CommandArgument='<%#Eval("id") %>' OnClientClick="return ViewRECIDDETAILS(this);" CommandName="Risk_ID"
                                runat="server"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Risk ID" ItemStyle-CssClass="txtCenter">
                        <ItemTemplate>
                            <asp:Label  runat="server" ID="lblIssue"  CssClass="disp-none" CommandArgument='<%#Eval("RiskActualId") %>' ></asp:Label>
                             <asp:LinkButton ID="RISK_ID"  Text='<%# Bind("RiskActualId") %>' CommandArgument='<%#Eval("RiskActualId") %>' OnClientClick="return ViewRECIDDETAILS(this);" CommandName="Risk_ID"
                                runat="server"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Department">
                        <ItemTemplate>
                            <asp:Label ID="lblRole" Text='<%# Bind("Department") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Risk Description">
                        <ItemTemplate>
                            <asp:Label ID="lblRECID" runat="server"  Text='<%# Bind("Risk_Description") %>'  ></asp:Label>                         
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Risk Impact">
                        <ItemTemplate>
                            <asp:Label ID="RiskImpact" Text='<%# Bind("Impacts") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="P" ItemStyle-CssClass="txtCenter">
                        <ItemTemplate>
                         <asp:Label ID="P" Text='<%# Bind("P") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="S" ItemStyle-CssClass="txtCenter">
                        <ItemTemplate>
                       <asp:Label ID="S" Text='<%# Bind("S") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="D" ItemStyle-CssClass="txtCenter">
                        <ItemTemplate>
                        <asp:Label ID="D" Text='<%# Bind("D") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="RPN" ItemStyle-CssClass="txtCenter">
                        <ItemTemplate>
                            <asp:Label ID="RPN" Text='<%# Bind("RPN") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Risk Nature" ItemStyle-CssClass="txtCenter">
                        <ItemTemplate>
                            <asp:Label ID="RiskCategory" Text='<%# Bind("RiskNature") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <%--<asp:TemplateField HeaderText="Risk Status" ItemStyle-CssClass="txtCenter">
                        <ItemTemplate>
                             <asp:Label ID="RiskStatus" Text='<%# Bind("Risk_Status") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Date Identified" ItemStyle-CssClass="txtCenter">
                        <ItemTemplate>
                            <asp:Label ID="DateIdentified" Text='<%# Bind("RDateIdentified") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
           </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
