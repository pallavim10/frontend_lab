<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_Deleted_Docs.aspx.cs" Inherits="CTMS.eTMF_Deleted_Docs" %>

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
    <script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_ChangeStatus.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                $(".Datatable").dataTable({
                    "bSort": true, "ordering": true,
                    "bDestroy": true, stateSave: true
                });

                $(".Datatable").parent().parent().addClass('fixTableHead');
            }
    </script>

    <style type="text/css">
        .btn-info {
            background-repeat: repeat-x;
            border-color: #28a4c9;
            /*background-image: linear-gradient(to bottom, #5bc0de 0%, #2aabd2 100%);*/
        }

        .prevent-refresh-button {
            display: inline-block;
            padding: 5px 5px;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: normal;
            line-height: 1.428571429;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            border: 1px solid transparent;
            border-radius: 4px;
            width: 70pt;
            height: 20pt;
        }

        .drpwidth {
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
                <asp:Label runat="server" ID="lblHeader" Text="Deleted Documents"></asp:Label>
            </h3>
            <div class="pull-right">
                <%--<asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Report&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="btnExportPDF" Text="Export as PDF" OnClick="btnExportPDF_Click"></asp:LinkButton>--%>
                <a href="#" class="dropdown-toggle btn-info prevent-refresh-button" data-toggle="dropdown" style="color: #FFFFFF">Export&nbsp;&nbsp;<span class="glyphicon glyphicon-download"></span></a>
                <ul class="dropdown-menu dropdown-menu-sm">
                    <li>
                        <asp:LinkButton runat="server" ID="btnExportExcel" OnClick="btnExportExcel_Click" CommandName="Excel" ToolTip="Excel"
                            Text="Excel" CssClass="dropdown-item" Style="color: #333333;">
                        </asp:LinkButton></li>
                    <hr style="margin: 5px;" />
                    <li>
                        <asp:LinkButton runat="server" ID="btnExportPDF" OnClick="btnExportPDF_Click" CssClass="dropdown-item"
                            ToolTip="PDF" Text="PDF" Style="color: #333333;">
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </div>
        <div class="row">
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
                    <asp:DropDownList ID="ddlAction" runat="server" class="form-control drpControl drpwidth required" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="eTMF"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div runat="server" id="Div2" class="col-md-3">
                    <asp:Label ID="lblViewType" runat="server" Font-Bold="true">Select :</asp:Label>&nbsp;
                            <asp:Label ID="lblbll" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*">
                            </asp:Label>
                    <asp:DropDownList ID="drpDocType" runat="server" class="form-control drpControl drpwidth required" AutoPostBack="true" OnSelectedIndexChanged="drpDocType_SelectedIndexChanged"></asp:DropDownList>
                </div>

                <div runat="server" id="Div1" class="col-md-3">
                    <label class="label width100px">Zones :</label>
                    <asp:DropDownList ID="ddlZone" runat="server" class="form-control drpControl drpwidth" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                </div>

                <div runat="server" id="Div3" class="col-md-3">
                    <label class="label width100px">Sections :</label>
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
                    <label class="label width100px">Artifacts :</label>
                    <asp:DropDownList ID="ddlArtifacts" runat="server" class="form-control drpControl drpwidth"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlArtifacts_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div runat="server" id="Div5" class="col-md-3">
                    <label class="label width100px">Documents :</label>
                    <asp:DropDownList ID="drpDocument" runat="server" class="form-control drpControl drpwidth"></asp:DropDownList>
                </div>

                <div runat="server" id="Div6" class="col-md-3">
                    <label class="label width100px">Status :</label>
                    <asp:DropDownList ID="drpStatus" runat="server" class="form-control drpControl drpwidth">
                        <asp:ListItem Text="--Select--" Value="All" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Current" Value="Current"></asp:ListItem>
                        <asp:ListItem Text="Superseded" Value="Superseded"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div runat="server" id="Div10" class="col-md-3">
                    <label class="label">Country :</label>
                    <asp:DropDownList ID="drpCountry" runat="server" class="form-control drpControl drpwidth"
                        AutoPostBack="true" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col-md-12">
                <div runat="server" id="Div11" class="col-md-3">
                    <label class="label">Site ID:</label>
                    <asp:DropDownList ID="drpInvID" runat="server" class="form-control drpControl drpwidth"></asp:DropDownList>
                </div>

                <div runat="server" id="Div9" class="col-md-3">
                    <label class="label"></label>
                    <br />
                    <asp:Button ID="btnShowFiles" runat="server" Text="Get Data" OnClick="btnShowFiles_Click"
                        CssClass="btn btn-primary btn-sm cls-btnSave" />
                </div>

            </div>
        </div>
        <br />
        <%--  <div id="divDownload" class="dropdown" runat="server" style="display: inline-flex">
                            <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                                style="color: #333333" title="Export"></a>
                            <ul class="dropdown-menu dropdown-menu-sm">
                                <li>
                                    <asp:LinkButton runat="server" ID="btnExportExcel" OnClick="btnExportExcel_Click" ToolTip="Export to Excel"
                                        Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                                    </asp:LinkButton></li>
                                <hr style="margin: 5px;" />
                            </ul>
                        </div>--%>
    </div>
    <br />
    <asp:GridView ID="gvFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        Width="100%" CssClass="table table-bordered table-striped layerFiles Datatable"
        OnPreRender="grd_data_PreRender"
        OnRowDataBound="gvFiles_RowDataBound" OnRowCommand="gvFiles_RowCommand">
        <Columns>
            <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                ItemStyle-CssClass="disp-none" HeaderText="ID">
                <ItemTemplate>
                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location ID">
                <ItemTemplate>
                    <asp:Label ID="LOCATION" ToolTip='<%# Bind("LOCATION") %>' runat="server" Text='<%# Bind("LOCATION") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Zone">
                <ItemTemplate>
                    <asp:Label ID="Zone" ToolTip='<%# Bind("Zone") %>' runat="server" Text='<%# Bind("Zone") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Section">
                <ItemTemplate>
                    <asp:Label ID="Section" ToolTip='<%# Bind("Section") %>' runat="server" Text='<%# Bind("Section") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Artifact">
                <ItemTemplate>
                    <asp:Label ID="Artifact" ToolTip='<%# Bind("Artifact") %>' runat="server" Text='<%# Bind("Artifact") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sub-Artifact">
                <ItemTemplate>
                    <asp:Label ID="SubArtifact" ToolTip='<%# Bind("SubArtifact") %>' runat="server" Text='<%# Bind("SubArtifact") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Country">
                <ItemTemplate>
                    <asp:Label ID="Country" ToolTip='<%# Bind("Country") %>' runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Site ID">
                <ItemTemplate>
                    <asp:Label ID="SiteID" ToolTip='<%# Bind("SiteID") %>' runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Subject ID">
                <ItemTemplate>
                    <asp:Label ID="SUBJID" ToolTip='<%# Bind("SUBJID") %>' runat="server" Text='<%# Bind("SUBJID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Spec.">
                <ItemTemplate>
                    <asp:Label ID="Spec" Width="100%" ToolTip='<%# Bind("SPEC") %>' runat="server" Text='<%# Bind("SPEC_CONCAT") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="File Name">
                <ItemTemplate>
                    <asp:Label ID="UploadFileName" ToolTip='<%# Bind("UploadFileName") %>'
                        runat="server" Text='<%# Bind("UploadFileName") %>'></asp:Label>
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
                    <asp:Label ID="SysVERSION" Width="100%" ToolTip='<%# Bind("SysVERSION") %>' runat="server" Text='<%# Bind("SysVERSION") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="Status" Width="100%" ToolTip='<%# Bind("Status") %>' runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Document Version" ItemStyle-CssClass="txt_center">
                <ItemTemplate>
                    <asp:Label ID="DocVERSION" ToolTip='<%# Bind("DocVERSION") %>' runat="server" Text='<%# Bind("DocVERSION") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Document Date">
                <ItemTemplate>
                    <asp:Label ID="DocDATE" ToolTip='<%# Bind("DocDATE") %>' runat="server" Text='<%# Bind("DocDATE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Note">
                <ItemTemplate>
                    <asp:Label ID="NOTE" ToolTip='<%# Bind("NOTE") %>' runat="server" Text='<%# Bind("NOTE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Received Date">
                <ItemTemplate>
                    <asp:Label ID="ReceivedDate" Width="100%" ToolTip='<%# Bind("RECEIPTDAT") %>' CssClass="label"
                        runat="server" Text='<%# Bind("RECEIPTDAT") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Expiry Date">
                <ItemTemplate>
                    <asp:Label ID="ExpiryDate" Width="100%" ToolTip='<%# Bind("ExpiryDate") %>' CssClass="label"
                        runat="server" Text='<%# Bind("ExpiryDate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Blinded / Unblinded">
                <ItemTemplate>
                    <asp:Label ID="Unblind" ToolTip='<%# Bind("Unblind") %>' runat="server" Text='<%# Bind("Unblind") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No. of Days to Upload">
                <ItemTemplate>
                    <asp:Label ID="UPLOAD_DAYS" ToolTip='<%# Bind("UPLOAD_DAYS") %>' runat="server" Text='<%# Bind("UPLOAD_DAYS") %>'></asp:Label>
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
            <asp:TemplateField HeaderText="Reason for Delete">
                <ItemTemplate>
                    <asp:Label ID="DELETE_REASON" ToolTip='<%# Bind("DELETE_REASON") %>' runat="server" Text='<%# Bind("DELETE_REASON") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <label>Deletion Details</label><br />
                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Deleted By]</label><br />
                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                </HeaderTemplate>
                <ItemTemplate>
                    <div>
                        <div>
                            <asp:Label ID="DELETEBYNAME" runat="server" Text='<%# Bind("DELETEBYNAME") %>' ForeColor="Blue"></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="DELETE_CAL_DAT" runat="server" Text='<%# Bind("DELETE_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="DELETE_CAL_TZDAT" runat="server" Text='<%# Eval("DELETE_CAL_TZDAT")+" "+ Eval("DELETE_TZVAL") %>' ForeColor="Red"></asp:Label>
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
            <asp:TemplateField ItemStyle-CssClass="txt_center disp-none" HeaderStyle-CssClass="disp-none" ItemStyle-Width="1%">
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
</asp:Content>
