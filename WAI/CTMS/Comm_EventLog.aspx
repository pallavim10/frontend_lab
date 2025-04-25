<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Comm_EventLog.aspx.cs" Inherits="CTMS.Comm_EventLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false
            });

            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050]
                });
            });

            $('.txtDateNoFuture').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050],
                    maxDate: new Date()
                });
            });

        }

        function AddNewEvent() {

            var test = "Comm_AddEvent.aspx";

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=380,width=900";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Print() {

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var ORIGINS = $("#<%=drpOrigin.ClientID%>").val();
            var Type = $("#<%=drpType.ClientID%>").val();
            var Nature = $("#<%=drpNature.ClientID%>").val();
            var Reference = $("#<%=drpReference.ClientID%>").val();
            var Department = $("#<%=drpDepartment.ClientID%>").val();
            var Event = $("#<%=drpEvent.ClientID%>").val();
            var test = "ReportComm_EventLog.aspx?&ORIGINS=" + ORIGINS + "&Type=" + Type + "&Reference=" + Reference + "&Department=" + Department + "&Event=" + Event + "&Nature=" + Nature;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ShowMail(element) {
            var lblText = element.innerText;
            var CommID = $(element).closest('td').find('input:eq(0)').val();

            if (lblText == "Communication" || lblText == "Import Communication") {
                $(location).attr('href', "Comm_Trails.aspx?ComID=" + CommID);
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Event Log
                        <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()"
                            CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
                    </h3>
                    <div class="pull-right" style="margin-top: 5px; margin-right: 5%;">
                        <asp:LinkButton runat="server" ID="lbtnAdd" Text="Add New Event" ForeColor="White"
                            CssClass="btn btn-primary btn-sm" OnClientClick="AddNewEvent();" />
                    </div>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="" style="display: inline-flex">
                            <label class="label width80px">
                                Origin:
                            </label>
                            <div class="Control">
                                <asp:DropDownList runat="server" ID="drpOrigin" CssClass="form-control required"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="" style="display: inline-flex">
                            <label class="label width80px">
                                Type:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpType" runat="server" CssClass="form-control" AutoPostBack="true">
                                    <asp:ListItem Selected="True" Text="--All--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Internal" Value="Internal"></asp:ListItem>
                                    <asp:ListItem Text="External" Value="External"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div runat="server" id="DivINV" class="" style="display: inline-flex">
                            <div class="" style="display: inline-flex">
                                <label class="label width80px">
                                    Nature:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpNature" runat="server" AutoPostBack="True" CssClass="form-control required">
                                        <asp:ListItem Selected="True" Text="--All--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Approval" Value="Approval"></asp:ListItem>
                                        <asp:ListItem Text="Decision" Value="Decision"></asp:ListItem>
                                        <asp:ListItem Text="Deviation" Value="Deviation"></asp:ListItem>
                                        <asp:ListItem Text="Document Review" Value="Document Review"></asp:ListItem>
                                        <asp:ListItem Text="General Communication" Value="General Communication"></asp:ListItem>
                                        <asp:ListItem Text="Risk" Value="Risk"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="" style="display: inline-flex">
                            <label class="label width80px">
                                Reference:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpReference" runat="server" AutoPostBack="True" CssClass="form-control required">
                                    <asp:ListItem Selected="True" Text="--All--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="CRO Personnel" Value="CRO Personnel"></asp:ListItem>
                                    <asp:ListItem Text="Data Integrity" Value="Data Integrity"></asp:ListItem>
                                    <asp:ListItem Text="Data Quality" Value="Data Quality"></asp:ListItem>
                                    <asp:ListItem Text="Data Review" Value="Data Review"></asp:ListItem>
                                    <asp:ListItem Text="Document Management" Value="Document Management"></asp:ListItem>
                                    <asp:ListItem Text="Ethics Committee" Value="Ethics Committee"></asp:ListItem>
                                    <asp:ListItem Text="Medical Management" Value="Medical Management"></asp:ListItem>
                                    <asp:ListItem Text="Process Deviation" Value="Process Deviation"></asp:ListItem>
                                    <asp:ListItem Text="Protocol Deviation" Value="Protocol Deviation"></asp:ListItem>
                                    <asp:ListItem Text="Regulatory" Value="Regulatory"></asp:ListItem>
                                    <asp:ListItem Text="Risk Management" Value="Risk Management"></asp:ListItem>
                                    <asp:ListItem Text="Safety-AE" Value="Safety-AE"></asp:ListItem>
                                    <asp:ListItem Text="Safety-SAE" Value="Safety-SAE"></asp:ListItem>
                                    <asp:ListItem Text="Site Personnel" Value="Site Personnel"></asp:ListItem>
                                    <asp:ListItem Text="SOP" Value="SOP"></asp:ListItem>
                                    <asp:ListItem Text="Sponsor Decision" Value="Sponsor Decision"></asp:ListItem>
                                    <asp:ListItem Text="Training" Value="Training"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="Div2" class="" style="display: inline-flex" runat="server">
                            <label class="label width80px">
                                Department:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpDepartment" runat="server" AutoPostBack="true" CssClass="form-control"
                                    OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="Div1" class="" style="display: inline-flex" runat="server">
                            <label class="label width80px">
                                Event:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpEvent" runat="server" AutoPostBack="true" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="" style="display: inline-flex">
                            <label class="label width80px">
                                User Name:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpUserName" runat="server" AutoPostBack="True" CssClass="form-control required">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="Div3" class="" style="display: inline-flex" runat="server">
                            <label class="label width80px">
                                From Date:
                            </label>
                            <div class="Control">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtDate form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div id="Div4" class="" style="display: inline-flex" runat="server">
                            <label class="label width80px">
                                Till Date:
                            </label>
                            <div class="Control">
                                <asp:TextBox ID="txtTillDate" runat="server" CssClass="txtDate form-control"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary"
                            OnClick="btngetdata_Click" />
                    </div>
                </div>
                <asp:GridView ID="grdLog" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped Datatable"
                    OnPreRender="grd_data_PreRender">
                    <Columns>
                        <asp:TemplateField HeaderText="No." HeaderStyle-Width="2%" ItemStyle-Width="2%" HeaderStyle-CssClass="txt_center"
                            ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_ID" ToolTip='<%# Bind("ID") %>' runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User" HeaderStyle-Width="4%" ItemStyle-Width="4%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_User" ToolTip='<%# Bind("User_Name") %>' runat="server" Text='<%# Bind("User_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Project" HeaderStyle-Width="8%" ItemStyle-Width="9%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_PROJNAME" ToolTip='<%# Bind("PROJNAME") %>' runat="server" Text='<%# Bind("PROJNAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Origin" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Origin" onclick="return ShowMail(this);" ToolTip='<%# Eval("Origin") %>'
                                    runat="server" Text='<%# Eval("Origin") %>'></asp:Label>
                                <asp:HiddenField runat="server" ID="hfCommID" Value='<%# Eval("Comm_ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Type" ToolTip='<%# Bind("Type") %>' runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nature" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nature" ToolTip='<%# Bind("Nature") %>' runat="server" Text='<%# Bind("Nature") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Reference" ToolTip='<%# Bind("Reference") %>' runat="server" Text='<%# Bind("Reference") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Department" ToolTip='<%# Bind("Department") %>' runat="server"
                                    Text='<%# Bind("Department") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Event" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Event" ToolTip='<%# Bind("Event") %>' runat="server" Text='<%# Bind("Event") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Notes" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Notes" ToolTip='<%# Bind("Notes") %>' runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EventDate" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_EventDate" ToolTip='<%# Bind("EventDate") %>' runat="server" Text='<%# Bind("EventDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created Date" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_CreatiedDate" ToolTip='<%# Bind("CreateDate") %>' runat="server"
                                    Text='<%# Bind("CreateDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
