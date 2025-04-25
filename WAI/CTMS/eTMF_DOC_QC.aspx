<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_DOC_QC.aspx.cs" Inherits="CTMS.eTMF_DOC_QC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script src="js/plugins/moment/moment.min.js" type="text/javascript"></script>
    <script src="js/plugins/moment/datetime-moment.js" type="text/javascript"></script>
    <link href="js/plugins/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />

    <link href="CommonStyles/eTMF/eTMF_GrdLayers.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_OpenDoc.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_History.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_DivExpandCollapse.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true, "ordering": true,
                "bDestroy": true, stateSave: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>

    <style type="text/css">
        .drpwidth{
            width: 90%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label runat="server" ID="lblHeader" Text="Pending for QC"></asp:Label>
            </h3>
        </div>
        <div class="form-group" style="margin-left: 15px">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div runat="server" id="Div8" class="col-md-3">
                    <label class="label width100px">Using  :&nbsp;
                            <asp:Label ID="lblView" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*">
                            </asp:Label></label>
                    <asp:DropDownList ID="ddlAction" runat="server" class="form-control drpControl drpwidth required"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="eTMF"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div runat="server" id="Div2" class="col-md-3">
                    <asp:Label ID="lblViewType" runat="server" Font-Bold="true">Select :</asp:Label>&nbsp;
                            <asp:Label ID="lblbll" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*">
                            </asp:Label>
                    <asp:DropDownList ID="drpDocType" runat="server" class="form-control drpControl drpwidth required"
                        AutoPostBack="true" OnSelectedIndexChanged="drpDocType_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div runat="server" id="Div1" class="col-md-3">
                    <label class="label">Zones :</label>
                    <asp:DropDownList ID="ddlZone" runat="server" class="form-control drpControl drpwidth" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div runat="server" id="Div3" class="col-md-3">
                    <label class="label width70px">Sections :</label>
                    <asp:DropDownList ID="ddlSections" runat="server" class="form-control drpControl drpwidth"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlSections_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">

                <div runat="server" id="Div4" class="col-md-3">
                    <label class="label">Artifacts :</label>
                    <asp:DropDownList ID="ddlArtifacts" runat="server" class="form-control drpControl drpwidth"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlArtifacts_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div runat="server" id="Div5" class="col-md-3">
                    <label class="label">
                        Documents :
                    </label>
                    <asp:DropDownList ID="drpDocument" runat="server" class="form-control drpControl drpwidth"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </div>

                <div runat="server" id="Div10" class="col-md-3">
                    <label class="label">Country :</label>
                    <asp:DropDownList ID="drpCountry" runat="server" class="form-control drpControl drpwidth"
                        AutoPostBack="true" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div runat="server" id="Div11" class="col-md-3">
                    <label class="label">Site ID:</label>
                    <asp:DropDownList ID="drpInvID" runat="server" class="form-control drpControl drpwidth"></asp:DropDownList>
                </div>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col-md-12">
                <div runat="server" id="Div7" class="col-md-3">
                    <label class="label"></label>
                    <br />
                    <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                        OnClick="btngetdata_Click" />
                </div>
            </div>
        </div>
        <br />
    </div>

    <asp:GridView ID="gvFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped layerFiles Datatable" OnPreRender="grd_data_PreRender"
        Width="100%" OnRowDataBound="gvFiles_RowDataBound" OnRowCommand="gvFiles_RowCommand1">
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
            <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnEdit" runat="server" ToolTip="Click to QC" CommandName="QcDoccument" CommandArgument='<%# Eval("ID") %>'
                        Style="font-size: initial; font-weight: bold;">
                        <i id="iconEdit" runat="server" class="fa fa-edit"></i>
                    </asp:LinkButton>
                    <asp:Label ID="lblEdit" runat="server" Text="Uploaded by You" Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location">
                <ItemTemplate>
                    <asp:Label ID="Location" Width="100%" ToolTip='<%# Bind("Location") %>' CssClass="label"
                        runat="server" Text='<%# Bind("Location") %>'></asp:Label>
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
                    <label>Uploaded By</label><br />
                    <%--  <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Uploaded By]</label><br />
                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>--%>
                </HeaderTemplate>
                <ItemTemplate>
                    <div>
                        <div>
                            <asp:Label ID="UPLOADBYNAME" runat="server" Text='<%# Bind("UPLOADBYNAME") %>' ForeColor="Blue"></asp:Label>
                        </div>
                        <%--  <div>
                                <asp:Label ID="UPLOAD_CAL_DAT" runat="server" Text='<%# Bind("UPLOAD_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="UPLOAD_CAL_TZDAT" runat="server" Text='<%# Eval("UPLOAD_CAL_TZDAT") +" "+ Eval("UPLOAD_TZVAL") %>' ForeColor="Red"></asp:Label>
                            </div>--%>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>
                    <label>Uploaded Datetime(Server)</label><br />
                </HeaderTemplate>
                <ItemTemplate>
                    <div>
                        <div>
                            <asp:Label ID="UPLOAD_CAL_DAT" runat="server" Text='<%# Bind("UPLOAD_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>
                    <label>Uploaded Datetime(User),(Timezone)</label><br />
                </HeaderTemplate>
                <ItemTemplate>
                    <div>
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
</asp:Content>
