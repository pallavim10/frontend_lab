<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_Group_Logs.aspx.cs" Inherits="CTMS.CTMS_Group_Logs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
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

        function Print() {

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var Action = "get_Group_SubTask_Logs_Data";
            var GroupID = $("#<%= hfv_lblGrpID.ClientID %>").val();
            var test = "ReportCTMS_Group_Logs.aspx?ProjectId=" + ProjectId + "&Action=" + Action + "&GroupID=" + GroupID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1500px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">
                Group Name : &nbsp;<asp:Label runat="server" ID="lblGrpName" Font-Bold="true"></asp:Label>
              <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()" CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
            </h3>
        </div>
        <asp:HiddenField runat="server" ID="hfv_lblGrpID" />
        <div class="row">
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
                        <div class="inner" style="height: 115px;">
                            <div style="font-size: small;">
                                <div class="row">
                                    <div class="col-md-12 txt_center" style="font-weight: bolder;">
                                        <asp:Label runat="server" ID="lblSub_Task_Name" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-6 pull-left">
                                            <asp:Label runat="server" ID="Label2" Text="Total :"></asp:Label>
                                        </div>
                                        <div class="col-md-6 pull-right">
                                            <asp:Label runat="server" ID="lblSite" Text='<%# Bind("Site") %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-6 pull-left">
                                            <asp:Label runat="server" ID="lbl1" Text="Planned :"></asp:Label>
                                        </div>
                                        <div class="col-md-6 pull-right">
                                            <asp:Label runat="server" ID="lblPlan" Text='<%# Bind("Plan") %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-6 pull-left">
                                            <asp:Label runat="server" ID="lbl2" Text="Actual :"></asp:Label>
                                        </div>
                                        <div class="col-md-6 pull-right">
                                            <asp:Label runat="server" ID="lblActual" Text='<%# Bind("Actual") %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="small-box-footer">
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <asp:GridView ID="gvTasks" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped" OnRowDataBound="gvTasks_RowDataBound">
            <%--OnPreRender="GridView_PreRender">--%>
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                    HeaderStyle-CssClass="txt_center">
                    <HeaderTemplate>
                        <a href="JavaScript:ManipulateAll('_Task');" id="_Folder" style="color: #333333"><i
                            id="img_Task" class="icon-plus-sign-alt"></i></a>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div runat="server" id="anchor">
                            <a href="JavaScript:divexpandcollapse('_Task<%# String.Format("{0},{1}", DataBinder.Eval(Container.DataItem, "Task_ID"), DataBinder.Eval(Container.DataItem, "Sub_Task_ID"))%>');"
                                style="color: #333333"><i id="img_Task<%# String.Format("{0},{1}", DataBinder.Eval(Container.DataItem, "Task_ID"), DataBinder.Eval(Container.DataItem, "Sub_Task_ID"))%>"
                                    class="icon-plus-sign-alt"></i></a>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                    HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lbl_TaskId" runat="server" Text='<%# Bind("Task_Id") %>'></asp:Label>
                        <asp:HiddenField ID="hf_TaskID" runat="server" Value='<%# Bind("Task_Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                    HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lbl_SubTaskId" runat="server" Text='<%# Bind("Sub_Task_Id") %>'></asp:Label>
                        <asp:HiddenField ID="hf_SubTaskID" runat="server" Value='<%# Bind("Sub_Task_Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sub-Tasks">
                    <ItemTemplate>
                        <asp:Label ID="lbl_TaskName" Width="100%" ToolTip='<%# Bind("Sub_Task_Name") %>'
                            CssClass="label" runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Plan Date" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label Style="width: 150px; text-align: center;" ID="txtDtPlan1" Text='<%# Bind("DtPlan1") %>'
                            runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Date" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label Style="width: 150px; text-align: center;" ID="txtDtActual1" Text='<%# Bind("DtActual1") %>'
                            runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Original Plan Date" ItemStyle-CssClass="txt_center"
                    ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label Style="text-align: center; width: 150px;" ID="txtDtOgPlan" Text='<%# Bind("DtOgPlan1") %>'
                            runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Variance(A Vs P)" ItemStyle-CssClass="txt_center"
                    ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label Style="width: 150px; text-align: center;" ID="txtDifference" Text='<%# Bind("Difference1") %>'
                            runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Variance(A Vs O)" ItemStyle-CssClass="txt_center"
                    ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label Style="width: 150px; text-align: center;" ID="txtDifference2" Text='<%# Bind("Difference2") %>'
                            runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Comment" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label Style="width: 150px; text-align: center;" ID="txtComment" Text='<%# Bind("Comment") %>'
                            runat="server"></asp:Label>
                        <asp:HiddenField ID="hf_Comment" runat="server" Value='<%# Bind("Comment") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <tr>
                            <td colspan="100%" style="padding: 2px;">
                                <div style="float: right; font-size: 13px;">
                                </div>
                                <div>
                                    <div class="rows">
                                        <div class="col-md-12">
                                            <div id="_Task<%# String.Format("{0},{1}", DataBinder.Eval(Container.DataItem, "Task_ID"), DataBinder.Eval(Container.DataItem, "Sub_Task_ID"))%>"
                                                style="display: none; position: relative; overflow: auto;">
                                                <asp:GridView ID="gvSites" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-striped">
                                                    <%--OnPreRender="GridView_PreRender">--%>
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                            HeaderText="ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TaskId" runat="server" Text='<%# Bind("Task_Id") %>'></asp:Label>
                                                                <asp:HiddenField ID="hf_TaskID" runat="server" Value='<%# Bind("Task_Id") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                            HeaderText="ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_SubTaskId" runat="server" Text='<%# Bind("Sub_Task_Id") %>'></asp:Label>
                                                                <asp:HiddenField ID="hf_SubTaskID" runat="server" Value='<%# Bind("Sub_Task_Id") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Site No." ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_SiteID" runat="server" Text='<%# Bind("INVID") %>'></asp:Label>
                                                                <asp:HiddenField ID="hf_SiteID" runat="server" Value='<%# Bind("INVID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Sub-Tasks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TaskName" Width="60%" ToolTip='<%# Bind("Sub_Task_Name") %>' CssClass="label"
                                                                    runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Plan Date" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label Style="text-align: center; width: 150px;" ID="txtDtPlan1" Text='<%# Bind("DtPlan1") %>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actual Date" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label Style="text-align: center; width: 150px;" ID="txtDtActual1" Text='<%# Bind("DtActual1") %>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Original Plan Date" ItemStyle-CssClass="txt_center"
                                                            ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label Style="text-align: center; width: 150px;" ID="txtDtOgPlan1" Text='<%# Bind("DtOgPlan1") %>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Variance(A Vs P)" ItemStyle-CssClass="txt_center"
                                                            ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label Style="width: 150px; text-align: center;" ID="txtDifference" Text='<%# Bind("Difference1") %>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Variance(A Vs O)" ItemStyle-CssClass="txt_center"
                                                            ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label Style="width: 150px; text-align: center;" ID="txtDifference2" Text='<%# Bind("Difference2") %>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Comment" ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label Style="width: 150px; text-align: center;" ID="txtComment" Text='<%# Bind("Comment") %>'
                                                                    runat="server"></asp:Label>
                                                                <asp:HiddenField ID="hf_Comment" runat="server" Value='<%# Bind("Comment") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle ForeColor="Blue" />
                                                    <HeaderStyle ForeColor="Blue" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">
            </h3>
        </div>
        <div class="row">
            <asp:ListView ID="lstComapre" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstComapre_ItemDataBound">
                <GroupTemplate>
                    <div class="col-lg-3 col-xs-6">
                        <asp:LinkButton ID="itemPlaceholder" runat="server" />
                    </div>
                </GroupTemplate>
                <ItemTemplate>
                    <!-- small box -->
                    <div id="divcol" runat="server">
                        <div class="inner" style="height: 115px;">
                            <div style="font-size: small;">
                                <div class="row">
                                    <div class="col-md-12 txt_center" style="font-weight: bolder;">
                                        <asp:Label runat="server" ID="lblSub_Task_Name" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-6 pull-left">
                                            <asp:Label runat="server" ID="lbl1" Text="Min :"></asp:Label>
                                        </div>
                                        <div class="col-md-6 pull-right">
                                            <asp:Label runat="server" ID="lbl_Min"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-6 pull-left">
                                            <asp:Label runat="server" ID="lbl2" Text="Avg :"></asp:Label>
                                        </div>
                                        <div class="col-md-6 pull-right">
                                            <asp:Label runat="server" ID="lbl_Avg"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-6 pull-left">
                                            <asp:Label runat="server" ID="Label1" Text="Max :"></asp:Label>
                                        </div>
                                        <div class="col-md-6 pull-right">
                                            <asp:Label runat="server" ID="lbl_Max"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="small-box-footer">
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <br />
        <asp:GridView ID="gvCompTasks" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped" OnRowDataBound="gvCompTasks_RowDataBound">
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                    HeaderStyle-CssClass="txt_center">
                    <HeaderTemplate>
                        <a href="JavaScript:ManipulateAll('_Compare');" id="_Folder" style="color: #333333">
                            <i id="img_Compare" class="icon-plus-sign-alt"></i></a>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div runat="server" id="anchor">
                            <a href="JavaScript:divexpandcollapse('_Compare<%# Eval("ID") %>');" style="color: #333333">
                                <i id="img_Compare<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sub-Tasks">
                    <ItemTemplate>
                        <asp:Label ID="lbl_TaskName" Width="100%" ToolTip='<%# Bind("Sub_Task_Name") %>'
                            CssClass="label" runat="server" Text='<%# Bind("Sub_Task_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Min" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Min" Width="100%" CssClass="label" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Avg" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Avg" Width="100%" CssClass="label" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Max" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Max" Width="100%" CssClass="label" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <tr>
                            <td colspan="100%" style="padding: 2px;">
                                <div style="float: right; font-size: 13px;">
                                </div>
                                <div>
                                    <div class="rows">
                                        <div class="col-md-12">
                                            <div id="_Compare<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                <asp:GridView ID="gvSites" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-striped">
                                                    <%--OnPreRender="GridView_PreRender">--%>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Site No." ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_SiteID" runat="server" Text='<%# Bind("INVID") %>'></asp:Label>
                                                                <asp:HiddenField ID="hf_SiteID" runat="server" Value='<%# Bind("INVID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Plan" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label Style="text-align: center; width: 150px;" ID="txtDtPlan1" Text='<%# Bind("Plan") %>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actual" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label Style="text-align: center; width: 150px;" ID="txtDtActual1" Text='<%# Bind("Actual") %>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle ForeColor="Blue" />
                                                    <HeaderStyle ForeColor="Blue" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
