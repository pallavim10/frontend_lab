<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_DefineSnapshot.aspx.cs" Inherits="CTMS.eTMF_DefineSnapshot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <style>
        .layer1
        {
            color: #0000ff;
        }
        .layer2
        {
            color: #800000;
        }
        .layer3
        {
            color: #008000;
        }
        .layer4
        {
            color: Black;
        }
        .layerFiles
        {
            color: #800000;
            font-style: italic;
        }
    </style>
    <script>

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
    <asp:ScriptManager ID="ScriptManager11" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Create Snapshots</h3>
                </div>
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" id="div11" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">
                                    Define Snapshots</h3>
                            </div>
                            <div class="rows">
                                <div style="height: 264px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Snapshot Name :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtSnapshot" runat="server" CssClass="form-control required width200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Publish To :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkSPONSOR" />&nbsp;&nbsp;
                                                <label>
                                                    Sponsor</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkSITE" />&nbsp;&nbsp;
                                                <label>
                                                    Site Management</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-4">
                                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkETMF" />&nbsp;&nbsp;
                                                <label>
                                                    eTMF</label>
                                            </div>
                                            <div class="col-md-4">
                                                &nbsp;
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Button ID="btnSubmitSnapshot" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    OnClick="btnSubmitSnapshot_Click" />
                                                <asp:Button ID="btnUpdateSnapshot" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    OnClick="btnUpdateSnapshot_Click" />&nbsp;&nbsp;
                                                <asp:Button ID="btnCancelSnapshot" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btnCancelSnapshot_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" id="div12" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">
                                    Snapshots</h3>
                            </div>
                            <div class="rows">
                                <div style="width: 100%; height: 264px; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="grdSnapshot" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdSnapshot_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Snapshot" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSnapshot" runat="server" Text='<%# Bind("Snapshot") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="EditSnapshot" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="DeleteSnapshot" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-warning">
                <div class="box-body">
                    <div class="box-header">
                        <h3 class="box-title">
                            Manage Snapshots
                        </h3>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label>
                                        Select Snapshot :</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpSnapshot" runat="server" CssClass="form-control width300px"
                                        OnSelectedIndexChanged="drpSnapshot_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                &nbsp;
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
            <div runat="server" id="divOthers" visible="false">
                <div class="box-body row">
                    <div class="col-md-5">
                        <div class="box box-primary">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">
                                    Add Zones
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvNewZones" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Zones">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblZone" runat="server" Text='<%# Bind("Zone") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="box-body">
                            <div style="min-height: 300px;">
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnAddZones" ForeColor="White" Text="Add" runat="server" CssClass="btn btn-primary btn-sm"
                                        ValidationGroup="Grp" OnClick="lbtnAddZones_Click" />
                                </div>
                                <div class="row txtCenter">
                                    &nbsp;
                                </div>
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnRemoveZones" ForeColor="White" Text="Remove" runat="server"
                                        ValidationGroup="Grp" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveZones_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="box box-primary">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">
                                    Added Zones
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvAddedZones" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Zones">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblZone" runat="server" Text='<%# Bind("Zone") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="box-body row">
                    <div class="col-md-5">
                        <div class="box box-primary">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">
                                    Add Sections
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvNewSections" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sections">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblZoneID" runat="server" Visible="false" Text='<%# Bind("ZoneID") %>'></asp:Label>
                                                                <asp:Label ID="lblSection" runat="server" Text='<%# Bind("Section") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="box-body">
                            <div style="min-height: 300px;">
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnAddSections" ForeColor="White" Text="Add" runat="server"
                                        CssClass="btn btn-primary btn-sm" ValidationGroup="Grp" OnClick="lbtnAddSections_Click" />
                                </div>
                                <div class="row txtCenter">
                                    &nbsp;
                                </div>
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnRemoveSections" ForeColor="White" Text="Remove" runat="server"
                                        ValidationGroup="Grp" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveSections_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="box box-primary">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">
                                    Added Sections
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvAddedSections" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sections">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblZoneID" runat="server" Visible="false" Text='<%# Bind("ZoneID") %>'></asp:Label>
                                                                <asp:Label ID="lblSection" runat="server" Text='<%# Bind("Section") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="box-body row">
                    <div class="col-md-5">
                        <div class="box box-primary">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">
                                    Add Artifacts
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvNewArtifacts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Artifacts">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblZoneID" runat="server" Visible="false" Text='<%# Bind("ZoneID") %>'></asp:Label>
                                                                <asp:Label ID="lblSectionID" runat="server" Visible="false" Text='<%# Bind("SectionID") %>'></asp:Label>
                                                                <asp:Label ID="lblArtifact" runat="server" Text='<%# Bind("Artifact") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="box-body">
                            <div style="min-height: 300px;">
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnAddArtifacts" ForeColor="White" Text="Add" runat="server"
                                        CssClass="btn btn-primary btn-sm" ValidationGroup="Grp" OnClick="lbtnAddArtifacts_Click" />
                                </div>
                                <div class="row txtCenter">
                                    &nbsp;
                                </div>
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnRemoveArtifacts" ForeColor="White" Text="Remove" runat="server"
                                        ValidationGroup="Grp" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveArtifacts_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="box box-primary">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">
                                    Added Artifacts
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvAddedArtifacts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sections">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblZoneID" runat="server" Visible="false" Text='<%# Bind("ZoneID") %>'></asp:Label>
                                                                <asp:Label ID="lblSectionID" runat="server" Visible="false" Text='<%# Bind("SectionID") %>'></asp:Label>
                                                                <asp:Label ID="lblArtifacts" runat="server" Text='<%# Bind("Artifact") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="box-body row">
                    <div class="col-md-5">
                        <div class="box box-primary">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">
                                    Add Documents
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvNewDocuments" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unique Ref. No." ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUniqueRefNo" runat="server" Text='<%# Bind("UniqueRefNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Documents" ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblDocument" runat="server" Text='<%# Bind("DocName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="box-body">
                            <div style="min-height: 300px;">
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnAddDocuments" ForeColor="White" Text="Add" runat="server"
                                        CssClass="btn btn-primary btn-sm" ValidationGroup="Grp" OnClick="lbtnAddDocuments_Click" />
                                </div>
                                <div class="row txtCenter">
                                    &nbsp;
                                </div>
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnRemoveDocuments" ForeColor="White" Text="Remove" runat="server"
                                        ValidationGroup="Grp" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveDocuments_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="box box-primary">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">
                                    Added Documents
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvAddedDocuments" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref. No." ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unique Ref. No." ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUniqueRefNo" runat="server" Text='<%# Bind("UniqueRefNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Documents" ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblDocument" runat="server" Text='<%# Bind("DocName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="box-body row">
                    <div class="col-md-5">
                        <div class="box box-primary">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">
                                    Add Files
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="grdFiles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Country" ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="COUNTRY" runat="server" Text='<%# Bind("COUNTRYNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DOCID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                                            <ItemTemplate>
                                                                <asp:Label ID="DOCID" runat="server" Text='<%# Bind("DOCID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Site Id" ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="SiteID" runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File Name" ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="UploadFileName" runat="server" Text='<%# Bind("UploadFileName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="box-body">
                            <div style="min-height: 300px;">
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnAddFiles" ForeColor="White" Text="Add" runat="server" CssClass="btn btn-primary btn-sm"
                                        ValidationGroup="Grp" OnClick="lbtnAddFiles_Click" />
                                </div>
                                <div class="row txtCenter">
                                    &nbsp;
                                </div>
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnRemoveFiles" ForeColor="White" Text="Remove" runat="server"
                                        ValidationGroup="Grp" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveFiles_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="box box-primary">
                            <div class="box-header ">
                                <h4 class="box-title" align="left">
                                    Added Documents
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="grdAddedFiles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Country" ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="COUNTRY" runat="server" Text='<%# Bind("COUNTRYNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DOCID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                                            <ItemTemplate>
                                                                <asp:Label ID="DOCID" runat="server" Text='<%# Bind("DOCID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Site Id" ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="SiteID" runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File Name" ItemStyle-CssClass="txtCenter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="UploadFileName" runat="server" Text='<%# Bind("UploadFileName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmitSnapshot" />
            <asp:PostBackTrigger ControlID="btnUpdateSnapshot" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
