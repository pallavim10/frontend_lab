<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MM_IssueListPopup.aspx.cs" Inherits="CTMS.MM_IssueListPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon">
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <%-- <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />--%>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <!-- for jquery Datatable.1.10.15//-->
    <script src="Scripts/Datatable.1.10.15.UI.js" type="text/javascript"></script>
    <script src="Scripts/Datatable1.10.15.js" type="text/javascript"></script>
     <script type="text/javascript">
         function Issuedetails(element) {
             var IssueID = $(element).prev().attr('commandargument');
             var test = "IssueDetails.aspx?IssueID=" + IssueID;
             var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=950";
             window.open(test, '_blank', strWinProperty);
             return false;
         }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <div class="box box-warning">
    <div class="box-header">
            <h3 class="box-title">
                   MEDICAL ISSUE LIST</h3>
        </div>
      
     <div class="row">
        <div class="lblError">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
    </div>
     
                    </div>
                    
                    
                    <br />
      <div class="box">
       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                             <ContentTemplate>
    <asp:GridView ID="grdISSUES" runat="server"  AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                Width="100%" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" 
                                     onprerender="grdISSUES_PreRender" >
       
        <Columns>
            <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="txt_center width20px">
                <ItemTemplate>
                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ISSUES_ID") %>' CommandArgument='<%# Eval("ISSUES_ID") %>'  CssClass="disp-noneimp" />
                   
                    <asp:LinkButton ID="lnkID" runat="server" Text='<%# Bind("ISSUES_ID") %>'
                     CommandArgument='<%# Eval("ISSUES_ID") %>' OnClientClick="return Issuedetails(this);" ></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="txt_center width20px"  HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="SiteID" ItemStyle-CssClass="txt_center width20px">
                <ItemTemplate>
                    <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>' CssClass="txt_center" />
                </ItemTemplate>
                <ItemStyle CssClass="txt_center width20px"  HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>

            
            <asp:TemplateField HeaderText="SUBJID" ItemStyle-CssClass="txt_center width20px">
                <ItemTemplate>
                    <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>' />
                </ItemTemplate>
                <ItemStyle CssClass="txt_center width20px"  HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Summary" ItemStyle-CssClass=" width20px">
                <ItemTemplate>
                    <asp:Label ID="Summary" runat="server" Text='<%# Bind("Summary") %>' />
                </ItemTemplate>
                <ItemStyle CssClass=" width20px"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center width20px">
                <ItemTemplate>
                    <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>' />
                </ItemTemplate>
                <ItemStyle CssClass="txt_center width20px"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Department" ItemStyle-CssClass="txt_center width20px">
                <ItemTemplate>
                    <asp:Label ID="Department" runat="server" Text='<%# Bind("Department") %>' />
                </ItemTemplate>
                <ItemStyle CssClass="txt_center width20px"></ItemStyle>
            </asp:TemplateField>

            <%--<asp:TemplateField HeaderText="Project" ItemStyle-CssClass="txt_center width20px">
                <ItemTemplate>
                    <asp:Label ID="Project" runat="server" Text='<%# Bind("Project_ID") %>' />
                </ItemTemplate>
                <ItemStyle CssClass="txt_center width20px"  HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Opened Date" ItemStyle-CssClass="txt_center width20px">
                <ItemTemplate>
                    <asp:Label ID="OpenedDate" runat="server" Text='<%# Bind("ISSUEDate") %>' />
                </ItemTemplate>
                <ItemStyle CssClass="txt_center width50px"  HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Due Date" ItemStyle-CssClass="txt_center width20px">
                <ItemTemplate>
                    <asp:Label ID="DueDate" runat="server" Text='<%# Bind("DueDate") %>' />
                </ItemTemplate>
                <ItemStyle CssClass="txt_center width50px"  HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Resolution" ItemStyle-CssClass="txt_center width20px">
                <ItemTemplate>
                    <asp:Label ID="Resolution" runat="server" Text='<%# Bind("ResolutionDate") %>' />
                </ItemTemplate>
                <ItemStyle CssClass="txt_center width20px"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Opened By" ItemStyle-CssClass="txt_center width20px">
                <ItemTemplate>
                    <asp:Label ID="ISSUEby" runat="server" Text='<%# Bind("ISSUEby") %>' />
                </ItemTemplate>
                <ItemStyle CssClass="txt_center width20px"></ItemStyle>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="AssignedTo" ItemStyle-CssClass="txt_center width20px">
                <ItemTemplate>
                    <asp:Label ID="AssignedTo" runat="server" Text='<%# Bind("AssignedTo") %>' />
                </ItemTemplate>
                <ItemStyle CssClass="txt_center width20px"></ItemStyle>
            </asp:TemplateField>

         
        </Columns>

    </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div> 
                    </div>

    </form>
</body>
</html>
