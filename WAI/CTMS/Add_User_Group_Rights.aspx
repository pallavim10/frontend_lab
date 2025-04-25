<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Add_User_Group_Rights.aspx.cs" Inherits="PPT.Add_User_Group_Rights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="box box-warning">
        <div class="box-header">
            <div><h3 class="box-title">
                Add User Groups Functions</h3>
                </div>
             <div id="Div2" class="dropdown" runat="server" style="display: inline-flex"><h3 class="box-title">
               <asp:LinkButton runat="server" ID="lbassignGroupFunExport" OnClick="lbassignGroupFunExport_Click" ToolTip="Export to Excel"
                 Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
		      </h3>
            </div>

        </div>
        <!-- /.box-header -->
        <!-- text input -->
        <div class="box-body">
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="form-group has-warning">
                                <asp:Label ID="Label1" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                            </div>
                            <div class="form-group" style="display: inline-flex">
                                <label class="label width100px">
                                    Select Project:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="Drp_Project_Name" runat="server" AutoPostBack="True" class="form-control drpControl required"
                                        OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group" style="display: inline-flex">
                                <label class="label width100px">
                                    Select User Group:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="Drp_User_Group" runat="server" class="form-control drpControl required">
                                    </asp:DropDownList>
                                </div>
                            </div>                            
                            <div class="form-group" style="display: inline-flex">
                                <label class="label width100px">
                                    Parent Functions Name:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="ddlFunctions" runat="server" CssClass="form-control required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:Button ID="Btn_Get_Fun" runat="server" OnClick="Btn_Get_Fun_Click" Text="Get Functions"
                                CssClass="btn btn-primary btn-sm cls-btnSave" />
                            <div class="box">
                            </div>
                        </div>
                    </div>
                    </div>
                    <div class="">
                        <div class="form-group pull-right" style="display: inline-flex" id="divselectchk"
                            runat="server" visible="false">
                            <label class="label">
                                Select All
                            </label>
                            <div class="Control">
                                <asp:CheckBox ID="Chk_Select_All" runat="server" AutoPostBack="True" OnCheckedChanged="Chk_Select_All_CheckedChanged"
                                    Style="font-size: x-small" />
                            </div>
                        </div>
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-CssClass="txtCenter width40px" ControlStyle-CssClass="txt_center"
                                    HeaderStyle-CssClass="txt_center width20px">
                                    <HeaderTemplate>
                                        <a href="JavaScript:ManipulateAll('FunctionID1');" id="_Folder" style="color: #333333">
                                            <i id="imgFunctionID1" class="icon-plus-sign-alt"></i></a>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div runat="server" id="anchor">
                                            <a href="JavaScript:divexpandcollapse('FunctionID1<%# Eval("FunctionID") %>');" style="color: #333333">
                                                <i id="imgFunctionID1<%# Eval("FunctionID") %>" class="icon-plus-sign-alt"></i>
                                            </a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Function ID" ItemStyle-CssClass="width100px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Fun_ID" runat="server" BorderStyle="None" Text='<%# Bind("FunctionID") %>'
                                            Enabled="False" CssClass="form-control txt_center"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parent Function" ItemStyle-CssClass="">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Parent" runat="server" Text='<%# Bind("Parent") %>' BorderStyle="None"
                                            Enabled="False" CssClass="form-control" Visible="false"></asp:TextBox>
                                        <asp:Label ID="lblSerious" runat="server" Text='<%# Eval("Parent") %>' CssClass=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Function Name" ItemStyle-CssClass="">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Fun_Name" runat="server" Text='<%# Bind("FunctionName") %>'
                                            BorderStyle="None" Visible="false" CssClass="form-control" Enabled="False" Font-Bold="True"></asp:TextBox>
                                        <asp:Label ID="FunctionName" runat="server" Text='<%# Eval("FunctionName") %>' CssClass=""></asp:Label>
                                        &nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="width30px">
                                    <HeaderTemplate>
                                        <asp:Button ID="Btn_Add_Fun" runat="server" OnClick="Btn_Add_Fun_Click" Text="Add"
                                            CssClass="btn btn-primary btn-sm cls-btnSave" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                <div style="float: right; font-size: 13px;">
                                                </div>
                                                <div>
                                                    <div id="FunctionID1<%# Eval("FunctionID") %>" style="display: none; position: relative;
                                                        overflow: auto;">
                                                        <asp:GridView ID="GridView2" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                            CssClass="table table-bordered table-striped table-striped1" OnRowDataBound="GridView2_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-CssClass="txtCenter width40px" ControlStyle-CssClass="txt_center"
                                                                    HeaderStyle-CssClass="txt_center width40px">
                                                                    <HeaderTemplate>
                                                                        <a href="JavaScript:ManipulateAll('FunctionID2');" id="_Folder" style="color: #333333">
                                                                            <i id="imgFunctionID2" class="icon-plus-sign-alt"></i></a>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <div runat="server" id="anchor">
                                                                            <a href="JavaScript:divexpandcollapse('FunctionID2<%# Eval("FunctionID") %>');" style="color: #333333">
                                                                                <i id="imgFunctionID2<%# Eval("FunctionID") %>" class="icon-plus-sign-alt"></i>
                                                                            </a>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Function ID" ItemStyle-CssClass="width100px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt_Fun_ID" runat="server" BorderStyle="None" Text='<%# Bind("FunctionID") %>'
                                                                            Enabled="False" CssClass="form-control txt_center"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Parent Function" ItemStyle-CssClass="">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt_Parent" runat="server" Text='<%# Bind("Parent") %>' BorderStyle="None"
                                                                            Enabled="False" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                        <asp:Label ID="lblSerious" runat="server" Text='<%# Eval("Parent") %>' CssClass=""></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Function Name" ItemStyle-CssClass="">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt_Fun_Name" runat="server" Text='<%# Bind("FunctionName") %>'
                                                                            BorderStyle="None" Visible="false" CssClass="form-control" Enabled="False" Font-Bold="True"></asp:TextBox>
                                                                        <asp:Label ID="FunctionName" runat="server" Text='<%# Eval("FunctionName") %>' CssClass=""></asp:Label>
                                                                        &nbsp;
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="width30px">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                                                <div style="float: right; font-size: 13px;">
                                                                                </div>
                                                                                <div>
                                                                                    <div id="FunctionID2<%# Eval("FunctionID") %>" style="display: none; position: relative;
                                                                                        overflow: auto;">
                                                                                        <asp:GridView ID="GridView3" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                                            CssClass="table table-bordered table-striped table-striped1" OnRowDataBound="GridView3_RowDataBound">
                                                                                            <Columns>
                                                                                                <asp:TemplateField ItemStyle-CssClass="txtCenter width40px" ControlStyle-CssClass="txt_center"
                                                                                                    HeaderStyle-CssClass="txt_center width40px">
                                                                                                    <HeaderTemplate>
                                                                                                        <a href="JavaScript:ManipulateAll('FunctionID3');" id="_Folder" style="color: #333333">
                                                                                                            <i id="imgFunctionID3" class="icon-plus-sign-alt"></i></a>
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <div runat="server" id="anchor">
                                                                                                            <a href="JavaScript:divexpandcollapse('FunctionID3<%# Eval("FunctionID") %>');" style="color: #333333">
                                                                                                                <i id="imgFunctionID3<%# Eval("FunctionID") %>" class="icon-plus-sign-alt"></i>
                                                                                                            </a>
                                                                                                        </div>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Function ID" ItemStyle-CssClass="width100px">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txt_Fun_ID" runat="server" BorderStyle="None" Text='<%# Bind("FunctionID") %>'
                                                                                                            Enabled="False" CssClass="form-control"></asp:TextBox>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Parent Function" ItemStyle-CssClass="">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txt_Parent" runat="server" Text='<%# Bind("Parent") %>' BorderStyle="None"
                                                                                                            Enabled="False" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                                        <asp:Label ID="lblSerious" runat="server" Text='<%# Eval("Parent") %>' CssClass=""></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Function Name" ItemStyle-CssClass="">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txt_Fun_Name" runat="server" Text='<%# Bind("FunctionName") %>'
                                                                                                            BorderStyle="None" Visible="false" CssClass="form-control" Enabled="False" Font-Bold="True"></asp:TextBox>
                                                                                                        <asp:Label ID="FunctionName" runat="server" Text='<%# Eval("FunctionName") %>' CssClass=""></asp:Label>
                                                                                                        &nbsp;
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-CssClass="width30px">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>
                                                                                                            <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                                                                                <div style="float: right; font-size: 13px;">
                                                                                                                </div>
                                                                                                                <div>
                                                                                                                    <div id="FunctionID3<%# Eval("FunctionID") %>" style="display: none; position: relative;
                                                                                                                        overflow: auto;">
                                                                                                                        <asp:GridView ID="GridView4" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                                                                            CssClass="table table-bordered table-striped table-striped1" OnRowDataBound="GridView4_RowDataBound">
                                                                                                                            <Columns>
                                                                                                                                <asp:TemplateField HeaderText="Function ID" ItemStyle-CssClass="width100px">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:TextBox ID="txt_Fun_ID" runat="server" BorderStyle="None" Text='<%# Bind("FunctionID") %>'
                                                                                                                                            Enabled="False" CssClass="form-control txt_center"></asp:TextBox>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField HeaderText="Parent Function" ItemStyle-CssClass="">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:TextBox ID="txt_Parent" runat="server" Text='<%# Bind("Parent") %>' BorderStyle="None"
                                                                                                                                            Enabled="False" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                                                                        <asp:Label ID="lblSerious" runat="server" Text='<%# Eval("Parent") %>' CssClass=""></asp:Label>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField HeaderText="Function Name" ItemStyle-CssClass="">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:TextBox ID="txt_Fun_Name" runat="server" Text='<%# Bind("FunctionName") %>'
                                                                                                                                            BorderStyle="None" Visible="false" CssClass="form-control" Enabled="False" Font-Bold="True"></asp:TextBox>
                                                                                                                                        <asp:Label ID="FunctionName" runat="server" Text='<%# Eval("FunctionName") %>' CssClass=""></asp:Label>
                                                                                                                                        &nbsp;
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField ItemStyle-CssClass="width30px">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                            </Columns>
                                                                                                                        </asp:GridView>
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
                                                                                </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
   
 
 </div>
   
</asp:Content>
