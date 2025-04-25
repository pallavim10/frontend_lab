<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ETMF_View_Docs.aspx.cs" Inherits="CTMS.ETMF_View_Docs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>

    <link href="CommonStyles/eTMF/eTMF_GrdLayers.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_OpenDoc.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_History.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_DivExpandCollapse.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%">
                <asp:Label runat="server" ID="lblHeader" Text="View Documents" />
            </h3>
            <div class="pull-right" style="display: none;">
                <asp:ImageButton ID="btnRefresh" runat="server" Style="height: 27px;" ImageUrl="img/Sync.png"
                    OnClick="btnRefresh_Click" ToolTip="Refresh"></asp:ImageButton>&nbsp;&nbsp;&nbsp;
            </div>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="label col-md-2">
                    View By :&nbsp;
                            <asp:Label ID="lblView" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*">
                            </asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlViewby" Width="250px" runat="server" class="form-control drpControl required"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlViewby_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="eTMF"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="label col-md-2">
                    <asp:Label ID="lblViewType" runat="server" Text="Select :"></asp:Label>
                    <asp:Label ID="lblStructure" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="drpDocType" Width="250px" runat="server" class="form-control drpControl required"
                        AutoPostBack="true" OnSelectedIndexChanged="drpDocType_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div id="divCountry" runat="server">
                    <div class="label col-md-2">
                        Zones :&nbsp;
                        <%--<asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>--%>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlZone" Width="250px" runat="server" class="form-control drpControl"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="divINVID" runat="server">
                    <div class="label col-md-2">
                        Sections : &nbsp;
                        <%--<asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>--%>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlSections" Width="250px" runat="server" class="form-control drpControl">
                        </asp:DropDownList>
                    </div>
                </div>

            </div>
        </div>
        <br />


        <div class="row">
            <div class="col-md-12">
                <div id="divCountries" runat="server">
                    <div class="label col-md-2">
                        Country :&nbsp;
                        <%--<asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>--%>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpCountry" Width="250px" runat="server" class="form-control drpControl"
                            AutoPostBack="true" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="divSiteID" runat="server">
                    <div class="label col-md-2">
                        Site ID: &nbsp;
                        <%--<asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>--%>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="drpInvID" Width="250px" runat="server" class="form-control drpControl">
                        </asp:DropDownList>
                    </div>
                </div>

            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="label col-md-2">
                    File Name : &nbsp;
                        <%--<asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>--%>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDocumentName" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnShowData" runat="server" Text="Show Data" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnShowData_Click" />
                </div>
                <div class="col-md-3">
                    <div id="divTCount" runat="server" visible="false" class="label col-md-12" style="color: Blue; display: inline-flex;">
                        Total Documents: &nbsp;&nbsp;
                         <asp:Label ID="lblCount" CssClass="form-control width50px label txt_center" ForeColor="Blue"
                             Text="0" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

        </div>
        <br />
    </div>
    <asp:GridView ID="gvArtifact" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped layer3" OnRowDataBound="gvArtifact_RowDataBound" Width="100%">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                HeaderStyle-CssClass="txt_center" ItemStyle-Width="5%">
                <HeaderTemplate>
                    <a href="JavaScript:ManipulateAll('_Docs');" id="_Folder" style="color: #333333"><i
                        id="img_Docs" class="icon-plus-sign-alt"></i></a>
                </HeaderTemplate>
                <ItemTemplate>
                    <div runat="server" id="anchor">
                        <a href="JavaScript:divexpandcollapse('_Docs<%# Eval("ID") %>');" style="color: #333333">
                            <i id="img_Docs<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                ItemStyle-CssClass="disp-none" HeaderText="MainRefNo">
                <ItemTemplate>
                    <asp:Label ID="MainRefNo" runat="server" Text='<%# Bind("MainRefNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                ItemStyle-CssClass="disp-none" HeaderText="DocTypeId">
                <ItemTemplate>
                    <asp:Label ID="DocTypeId" runat="server" Text='<%# Bind("DocTypeId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ref." ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                <ItemTemplate>
                    <asp:Label ID="lbl_RefNo" Width="100%" ToolTip='<%# Bind("RefNo") %>' CssClass="label"
                        runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Zones" ItemStyle-Width="25%">
                <ItemTemplate>
                    <asp:Label ID="lbl_Zones" Width="100%" ToolTip='<%# Bind("Zones") %>'
                        CssClass="label" runat="server" Text='<%# Bind("Zones") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Sections" ItemStyle-Width="25%">
                <ItemTemplate>
                    <asp:Label ID="lbl_Sections" Width="100%" ToolTip='<%# Bind("Sections") %>'
                        CssClass="label" runat="server" Text='<%# Bind("Sections") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Artifacts" ItemStyle-Width="25%">
                <ItemTemplate>
                    <asp:Label ID="lbl_Artifact" Width="100%" ToolTip='<%# Bind("Artifact_Name") %>'
                        CssClass="label" runat="server" Text='<%# Bind("Artifact_Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Documents" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                <ItemTemplate>
                    <asp:Label ID="lblCount" Text="0" CssClass="label" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <tr>
                        <td colspan="100%" style="padding: 2px;">
                            <div style="float: right; font-size: 13px;">
                            </div>
                            <div>
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div id="_Docs<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                            <asp:GridView ID="gvDocs" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                OnRowDataBound="gvDocs_RowDataBound" CssClass="table table-bordered table-striped layer1">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                        HeaderStyle-CssClass="txt_center">
                                                        <HeaderTemplate>
                                                            <a href="JavaScript:ManipulateAll('_Files');" id="_Folder" style="color: #333333"><i
                                                                id="img_Files" class="icon-plus-sign-alt"></i></a>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div runat="server" id="anchor">
                                                                <a href="JavaScript:divexpandcollapse('_Files<%# Eval("ID") %>');" style="color: #333333">
                                                                    <i id="img_Files<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                        ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ref." ItemStyle-Width="15%" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_RefNo" Width="100%" ToolTip='<%# Bind("RefNo") %>' CssClass="label"
                                                                runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unique Ref." HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                        ItemStyle-CssClass="disp-none" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_UniqueRefNo" Width="100%" ToolTip='<%# Bind("UniqueRefNo") %>'
                                                                CssClass="label" runat="server" Text='<%# Bind("UniqueRefNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Document" ItemStyle-Width="65%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DocName" Width="100%" ToolTip='<%# Bind("DocName") %>' CssClass="label"
                                                                runat="server" Text='<%# Bind("DocName") %>'></asp:Label>
                                                            <div id="divcomment" runat="server">
                                                                <asp:Label ID="lblComment" Text='<%# Bind("Comment") %>' Width="100%" CssClass="label"
                                                                    runat="server" ForeColor="Red"></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Documents" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCount" Text="0" CssClass="label" runat="server"></asp:Label>
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
                                                                                <div id="_Files<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                                    <asp:GridView ID="gvFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                        Width="100%" OnRowCommand="gvFiles_RowCommand" CssClass="table table-bordered table-striped layerFiles Datatable"
                                                                                        OnPreRender="grd_data_PreRender" OnRowDataBound="gvFiles_RowDataBound">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                                                                ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                                                                ItemStyle-CssClass="disp-none" HeaderText="SysFileName">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="SysFileName" runat="server" Text='<%# Bind("SysFileName") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Country">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Country" Width="100%" ToolTip='<%# Bind("Country") %>' CssClass="label"
                                                                                                        runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="SIte Id">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="SiteID" Width="100%" ToolTip='<%# Bind("SiteID") %>' CssClass="label"
                                                                                                        runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Spec.">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Spec" Width="100%" ToolTip='<%# Bind("SPEC_CONCAT") %>' CssClass="label"
                                                                                                        runat="server" Text='<%# Bind("SPEC_CONCAT") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                                                                ItemStyle-CssClass="disp-none">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_Name" Width="100%" ToolTip='<%# Bind("AutoNomenclature") %>' CssClass="label"
                                                                                                        runat="server" Text='<%# Bind("AutoNomenclature") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="File Name">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                                                                                                        CssClass="label" runat="server" Text='<%# Bind("UploadFileName") %>'></asp:Label>
                                                                                                    <asp:LinkButton ID="lbtn_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                                                                                                        runat="server" CssClass="label" OnClientClick="return OpenDoc(this);" Text='<%# Bind("UploadFileName") %>'></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="File Type" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbtnFileType" runat="server" Font-Size="Larger"><i id="ICONCLASS"
                                                                                                        runat="server" class="fas fa-file-text"></i></asp:Label>
                                                                                                    <asp:Label ID="lbl_FileSize" Width="100%" CssClass="label" Font-Size="X-Small" runat="server" Text='<%# Bind("CAL_Size") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="System Version">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="SysVERSION" Width="100%" ToolTip='<%# Bind("SysVERSION") %>' CssClass="label"
                                                                                                        runat="server" Text='<%# Bind("SysVERSION") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label runat="server" ID="lbtnStatus" CssClass="label" Text='<%# Eval("Status") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Document Version" ItemStyle-CssClass="txt_center">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="DocVERSION" Width="100%" ToolTip='<%# Bind("DocVERSION") %>'
                                                                                                        CssClass="label" runat="server" Text='<%# Bind("DocVERSION") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Document Date" ItemStyle-CssClass="txt_center">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="DocDATE" Width="100%" ToolTip='<%# Bind("DocDATE") %>' CssClass="label"
                                                                                                        runat="server" Text='<%# Bind("DocDATE") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Note" ItemStyle-CssClass="txt_center">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="NOTE" Width="100%" ToolTip='<%# Bind("NOTE") %>' CssClass="label"
                                                                                                        runat="server" Text='<%# Bind("NOTE") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Received Date" ItemStyle-CssClass="txt_center">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="ReceivedDate" Width="100%" ToolTip='<%# Bind("RECEIPTDAT") %>' CssClass="label"
                                                                                                        runat="server" Text='<%# Bind("RECEIPTDAT") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Expiry Date" ItemStyle-CssClass="txt_center">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="ExpiryDate" Width="100%" ToolTip='<%# Bind("ExpiryDate") %>' CssClass="label"
                                                                                                        runat="server" Text='<%# Bind("ExpiryDate") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <label>Uploading Details</label><br />
                                                                                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Uploaded By]</label><br />
                                                                                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <div>
                                                                                                        <div>
                                                                                                            <asp:Label ID="UPLOADBYNAME" runat="server" Text='<%# Bind("UPLOADBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                                                        </div>
                                                                                                        <div>
                                                                                                            <asp:Label ID="UPLOAD_CAL_DAT" runat="server" Text='<%# Bind("UPLOAD_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                                                        </div>
                                                                                                        <div>
                                                                                                            <asp:Label ID="UPLOAD_CAL_TZDAT" runat="server" Text='<%# Eval("UPLOAD_CAL_TZDAT") +" "+ Eval("UPLOAD_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtnDocumentHistory" runat="server" ToolTip="Document History"
                                                                                                        OnClientClick="return DOCUMENT_HISTORY(this);" CommandArgument='<%# Eval("ID") %>'>
                                                                                                        <i id="iconDochistory" runat="server" class="fa fa-history" style="color: #333333;"
                                                                                                            aria-hidden="true"></i>
                                                                                                    </asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtnversionHistory" runat="server" ToolTip="Version History"
                                                                                                        OnClientClick="return VERSION_HISTORY(this);" CommandArgument='<%# Eval("ID") %>'>
                                                                                                        <i id="iconhistory" runat="server" class="fa fa-files-o" style="color: #333333;"
                                                                                                            aria-hidden="true"></i>
                                                                                                    </asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtnDownloadDoc" runat="server" ToolTip="Download" CommandName="Download" CssClass="btn"
                                                                                                        CommandArgument='<%# Eval("ID") %>'><i class="fa fa-download" style="color:#333333;" aria-hidden="true"></i>
                                                                                                    </asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblQC" runat="server" ToolTip="QC Document" CommandArgument='<%# Eval("ID") %>'
                                                                                                        Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconQC" runat="server"
                                                                                                            class="fa fa-check"></i></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
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
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
