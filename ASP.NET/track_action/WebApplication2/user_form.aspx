<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_form.aspx.cs" Inherits="WebApplication2.user_form" validateRequest="false" EnableEventValidation = "false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ActionLogs</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous"/>
    <link href="C:\Users\pallavimisal\Documents\Task\track_action\WebApplication2\Resources\ClientFuncs.js" rel="text/javascript"/>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <link rel="stylesheet" href="C:\Users\pallavimisal\Documents\Task\track_action\WebApplication2\Resources\Style.css" />


</head>
<body>
    <form id="form1" runat="server" class="container">
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <asp:label ID="lbl_username" runat="server" Text="Enter User Name: "></asp:label>
                    <asp:textbox ID="txt_username" runat="server" class="form-control"></asp:textbox>
                </div>                
            </div>     
            <div class="col-md-6">
             <asp:button ID="btnContinue" runat="server" Text="Continue" OnClick="btnClicked_Continue" CssClass="btn btn-primary mt-4"></asp:button>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <table id="Employeeinfo" runat="server" class="table" visible="false">
                    <tr>
                        <td colspan="4">
                            <h3 id="heading2">Employee Information</h3>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2"><asp:Label ID="lbl_empname" runat="server" Text="Employee Name:"></asp:Label></td>
                        <td>
                            <asp:textbox ID="txt_empname" runat="server" class="form-control"></asp:textbox>
                            <asp:Label ID="lblid" for="txt_empname" runat="server" Visible="false"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_empname" ID="RequiredFieldValidator1" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td rowspan="6">
                            <asp:GridView ID="GrdvwEmployee" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:ButtonField ButtonType="button"  CommandName="Select" Text="Select" HeaderText="Select"/>
                                    <asp:BoundField DataField="Empid" HeaderText="ID"/>
                                    <asp:BoundField DataField="EmpName" HeaderText="Name"/>
                                    <asp:BoundField DataField="EmpAddr" HeaderText="Address"/>
                                    <asp:BoundField DataField="EmailId" HeaderText="EmailID"/>
                                    <asp:BoundField DataField="EmpEduc" HeaderText="Qualification"/>
                                    <asp:BoundField DataField="EmpAge" HeaderText="Age"/>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#333333" />  
                                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />  
                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />  
                                <RowStyle BackColor="White" ForeColor="#333333" />  
                                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />  
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />  
                                <SortedAscendingHeaderStyle BackColor="#487575" />  
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />  
                                <SortedDescendingHeaderStyle BackColor="#275353" />  
                            </asp:GridView>
                        </td>
                       

                    </tr>
                    <tr>
                        <td class="auto-style2"><asp:label runat="server" ID="lbl_addr">Employee Address:</asp:label></td>
                        <td>
                            <asp:textbox ID="txt_empaddr" runat="server" class="form-control"></asp:textbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2"><asp:label runat="server" ID="Lbl_emailid">Employee EmailID:</asp:label></td>
                        <td>
                            <asp:textbox ID="txt_emailid" runat="server" class="form-control"></asp:textbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2"><asp:label runat="server" ID="lbl_edu">Employee Qualification:</asp:label></td>
                        <td>
                            <asp:textbox ID="txt_empedu" runat="server" class="form-control"></asp:textbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2"><asp:label runat="server" ID="lbl_Age">Employee Age:</asp:label></td>
                        <td>
                            <asp:textbox ID="txt_empage" runat="server" class="form-control"></asp:textbox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="auto-style2"><asp:Label runat="server" ID="lbl_msg" Visible="false"></asp:Label></td>
                    </tr>
                    <tr class="col-md-3">
                        
                        <td class="auto-style2"><asp:Button runat="server" ID="btnInsert" Text="Insert" OnClick="btn_InsertClick" CssClass="btn btn-success"></asp:Button></td>
                        <td class="auto-style1"><asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btn_UpdateClick" CssClass="btn btn-warning"></asp:Button></td>
                        <td class="auto-style1"><asp:Button runat="server" ID="btnDelete" Text="Delete" OnClick="btn_DeleteClick" CssClass="btn btn-danger"></asp:Button></td>
                        <td class="auto-style1"><asp:Button runat="server" ID="btnExportExcel"  Text="Export to Excel" OnClick="btnExportExcel_Click" CssClass="btn btn-primary"></asp:Button></td>
                       <%--  <td class="auto-style1"><asp:Button runat="server" ID="btnClearValues"  Text="Clear Data" OnClick="cleardata" CssClass="btn btn-secondary"></asp:Button></td>--%>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row"><!-- <div class="row  mt-3">-->
            <div class="col-md-12">
                <h2>Action List By &nbsp;<asp:label runat="server" ID="lbluser"> </asp:label></h2>
                <asp:Gridview ID="gvTriggerView" runat="server" AutoGenerateColumns="false" CssClass="table">
                    <Columns>
                        <asp:BoundField DataField="Empid" HeaderText="Employee ID"/>
                        <asp:BoundField DataField="Command" HeaderText="Command"/>
                        <asp:BoundField DataField="Action" HeaderText="Action"/>
                        <asp:BoundField DataField="CreatedDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Date"/>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />  
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />  
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />  
                    <RowStyle BackColor="White" ForeColor="#333333" />  
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />  
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />  
                    <SortedAscendingHeaderStyle BackColor="#487575" />  
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />  
                    <SortedDescendingHeaderStyle BackColor="#275353" />  
                </asp:Gridview>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>

</body>
</html>
