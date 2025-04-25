<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CTMS_MonitoringMatrix.aspx.cs" Inherits="CTMS.CTMS_MonitoringMatrix" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    function Print() {
        if ($("#<%=Drp_Project.ClientID%>").val() == "0") {
            $("#<%=Drp_Project.ClientID%>").addClass("brd-1px-redimp");
            return false;
        }
//        if ($("#<%=drp_INVID.ClientID%>").val() == "99") {
//            $("#<%=drp_INVID.ClientID%>").addClass("brd-1px-redimp");
//            return false;
//        }

//        if ($("#<%=drpVisitType.ClientID%>").val() == "99") {
//            $("#<%=drpVisitType.ClientID%>").addClass("brd-1px-redimp");
//            return false;
//        }
        var ProjectId = $("#<%=Drp_Project.ClientID%>").val();
        var INVID = $("#<%=drp_INVID.ClientID%>").val();
        var VISITTYPE = $("#<%=drpVisitType.ClientID%>").val();
        var action = "GET_MONITORING_MATRIX";
        var test = "ReportCTMS_MonitoringMatrix.aspx?Action=" + action + "&ProjectId=" + ProjectId + "&INVID=" + INVID + "&VISITTYPE=" + VISITTYPE;

        var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1500px";
        window.open(test, '_blank', strWinProperty);
        return false;
    }
 </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:UpdatePanel ID="Upd_Pan_Sel_Dept" runat="server">
                    <ContentTemplate>  
                      <div class="box box-warning"><div class="box-header"> <h3 class="box-title">Monitoring Visit Matrix
                      <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()" CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
      </h3></div> <!-- /.box-header --><!-- text input --> <div class="box-body"><form role="form">
       
     <div class="row">
        <div class="lblError">
             <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
    </div> <div class="">
      <table>
                        <tr>
                            <td class="label">
                                Select Project                                
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Lbl_Dept" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                             <ContentTemplate>
                                <asp:DropDownList ID="Drp_Project" class="form-control drpControl required" runat="server"
                                    AutoPostBack="True" OnSelectedIndexChanged="Drp_Project_SelectedIndexChanged">
                                </asp:DropDownList>                            
                                    </ContentTemplate>
                                 </asp:UpdatePanel>
                            </td>
                            
 
                            <td class="label">
                                Select Visit Type:</td>
                            <td class="requiredSign">
                                <asp:Label ID="Label6" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                                <asp:DropDownList ID="drpVisitType" runat="server" AutoPostBack="True" 
                                    class="form-control drpControl" 
                                    onselectedindexchanged="drpVisitType_SelectedIndexChanged">
                     
                                </asp:DropDownList>
                             
                            </td>
                           
                            <td class="label">
                                Select Site Id:
                            </td>
                            <td class="requiredSign">
                                <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                            </td>
                            <td class="Control">
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                             <ContentTemplate>
                                <asp:DropDownList ID="drp_INVID" runat="server" 
                                     class="form-control drpControl required" AutoPostBack="True" onselectedindexchanged="drp_INVID_SelectedIndexChanged" 
                                   >
                                </asp:DropDownList>                               
                                        </ContentTemplate>
                                 </asp:UpdatePanel>
                            </td>
                            <td class="style10">
                            </td>
                        </tr>
        </table>
        </div>
           </form>
    </div>
    </div>
        <div class="box">
<asp:GridView ID="grd"   runat="server" AutoGenerateColumns=" False" 
     CssClass="table table-bordered table-striped Datatable"    onprerender="grdISSUES_PreRender"
              >
        <Columns>              
      

              <asp:TemplateField HeaderText="Site Id" HeaderStyle-HorizontalAlign="Center"   ItemStyle-CssClass="txt_center" >
                <ItemTemplate>
                    <asp:label ID="INVID" runat="server" 
                        Text='<%# Bind("INVID") %>'   />
                   
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Site Name" HeaderStyle-HorizontalAlign="Center"   ItemStyle-CssClass="txt_center" >
                <ItemTemplate>
                    <asp:label ID="INVNAM" runat="server"  Width="170px"
                        Text='<%# Bind("INVNAM") %>'   />
                   
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Visit Id" HeaderStyle-HorizontalAlign="Center"  ItemStyle-CssClass="txt_center" >
                <ItemTemplate>
                    <asp:linkbutton ID="MVID" runat="server"  CommandArgument='<%#Eval("MVID") %>' 
                            CommandName="MVID"
                        Text='<%# Bind("MVID") %>' />
                   
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Visit Type" HeaderStyle-HorizontalAlign="Center"  ItemStyle-CssClass="txt_center" >
                <ItemTemplate>
                    <asp:linkbutton ID="VISITTYPE" runat="server"  CommandArgument='<%#Eval("VISITTYPE") %>' 
                            CommandName="VISITTYPE"
                        Text='<%# Bind("VISITTYPE") %>' />
                   
                </ItemTemplate>
            </asp:TemplateField>

           <asp:TemplateField HeaderText=" Planned  Date" HeaderStyle-HorizontalAlign="Center"  ItemStyle-CssClass="txt_center" >
                <ItemTemplate>
                    <asp:label ID="PlannedDate" runat="server" 
                        Text='<%# Bind("PlannedDate") %>'  />
                   
                </ItemTemplate>
            </asp:TemplateField>    

            <asp:TemplateField HeaderText=" Schedule Start Date" HeaderStyle-HorizontalAlign="Center"  ItemStyle-CssClass="txt_center" >
                <ItemTemplate>
                    <asp:label ID="FROMDAT" runat="server" 
                        Text='<%# Bind("FROMDAT") %>'  />
                   
                </ItemTemplate>
            </asp:TemplateField>    

            <asp:TemplateField HeaderText=  " Schedule End Date" HeaderStyle-HorizontalAlign="Center"  ItemStyle-CssClass="txt_center" >
                <ItemTemplate>
                    <asp:label ID="TODAT" runat="server" 
                        Text='<%# Bind("TODAT") %>'  />
                   
                </ItemTemplate>
            </asp:TemplateField>    

           
              <%-- <asp:TemplateField HeaderText="Reviewed" HeaderStyle-HorizontalAlign="Center"  ItemStyle-CssClass="txt_center" >
                <ItemTemplate>
                    <asp:label ID="REVIEWED" runat="server" 
                        Text='<%# Bind("REVIEWED") %>'  />
                   
                </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Reviewed By" HeaderStyle-HorizontalAlign="Center"  ItemStyle-CssClass="txt_center" >
                <ItemTemplate>
                    <asp:label ID="REVIEWEDBY" runat="server" 
                        Text='<%# Bind("REVIEWEDBY") %>'  />
                   
                </ItemTemplate>
            </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Completion Date" HeaderStyle-HorizontalAlign="Center"  ItemStyle-CssClass="txt_center" >
                <ItemTemplate>
                    <asp:label ID="dtApprovetCompletion" runat="server"   Text='<%# Bind("DTCOMPLETION") %>' 
                          />
                   
                </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Reviewed Date" HeaderStyle-HorizontalAlign="Center"  ItemStyle-CssClass="txt_center" >
                <ItemTemplate>
                    <asp:label ID="DTREVIEWED" runat="server" 
                        Text='<%# Bind("DTREVIEWED") %>'  />
                   
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Approved Date" HeaderStyle-HorizontalAlign="Center"  ItemStyle-CssClass="txt_center" >
                <ItemTemplate>
                    <asp:label ID="dtApprove" runat="server"  Text='<%# Bind("DTAPPROVED") %>' 
                          />
                   
                </ItemTemplate>
            </asp:TemplateField>
                                       
           <asp:TemplateField HeaderText="CRA" HeaderStyle-HorizontalAlign="Center"  ItemStyle-CssClass="txt_center" >
                <ItemTemplate>
                    <asp:label ID="CRA" runat="server" 
                        Text='<%# Bind("CRA") %>'  />
                   
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>       
    </asp:GridView>  
    </div>
 
    </ContentTemplate>
    </asp:UpdatePanel>                                 
</asp:Content>

