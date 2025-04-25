<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_Query_Close_Bulk.aspx.cs" Inherits="CTMS.DM_Query_Close_Bulk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
     <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <link href="CommonStyles/Searchable_DropDown.css" rel="stylesheet" />    
    <script src="CommonFunctionsJs/DM/DataChange.js"></script>

    <script type="text/jscript">
        function pageLoad() {

            $('.select').select2();

            $(".Datatable").dataTable({
                "paging": true,
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[-1], ["All"]],
                stateSave: true,
                fixedHeader: true,
            });

       

            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        }


    </script>


            <script type="text/javascript">

                function ToggleCheckBox(element) {

                    if ($(element).attr('previousValue') == 'true') {

                        $(element).attr('checked', false)

                    } else {

                        $(element).attr('checked', true)

                    }
                    $(element).attr('previousValue', $(element).attr('checked'));
                }

                function Check_All_Closed(element) {

                    $('input[type=checkbox][id*=Chek_Closed]').each(function () {

                        if ($(element).prop('checked') == true) {
                            $(this).prop('checked', true);
                        }
                        else {
                            $(this).prop('checked', false);
                        }

                    });

                }


                function validateCheckboxSelection() {
                    var grid = document.getElementById('<%= grdQueryDetailReports.ClientID %>'); // get GridView element
                    var checkboxes = grid.getElementsByTagName('input');
                    var atLeastOneChecked = false;

                    for (var i = 0; i < checkboxes.length; i++) {
                        if (checkboxes[i].type === 'checkbox' && checkboxes[i].checked) {
                            atLeastOneChecked = true;
                            break;
                        }
                    }

                    if (!atLeastOneChecked) {
                        // Hide the modal if no checkbox is selected
                        $find('<%= ModalPopupExtender1.ClientID %>').hide();
                        alert('Please select at least one checkbox.');
                        return false; // Prevent further action
                    }
                    else {
                        // Show the modal
                       <%-- $find('<%= ModalPopupExtender1.ClientID %>').show();
                        return true;--%>
                    }

                  
                }
          
            </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <div class="box box-warning">
                <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
                  <div class="box-header">
                    <h3 class="box-title">Query Reports
                    </h3>
                </div>
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: small;"></asp:Label>
                </div>

                <div class="align-center">
                    <asp:Button ID="btnclosequery" runat="server" Text="Close Query" OnClientClick="return validateCheckboxSelection(); return false;"
                        CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnclosequery_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm"
                        OnClick="btnCancel_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnback" runat="server" CssClass="btn btn-info btn-sm"
                        Text="Back To Query Report" OnClick="btnback_Click" />
                    &nbsp;&nbsp;&nbsp;
                </div>
                <div class="rows">
                    <div class="fixTableHead">
                        <asp:GridView ID="grdQueryDetailReports" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-bordered Datatable" OnPreRender="grd_data_PreRender"
                            OnRowCommand="grdQueryDetailReports_RowCommand" DataKeyNames="ID"
                            OnRowDataBound="grdQueryDetailReports_RowDataBound">
                            <Columns>


                                <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="ChekAll_Closed" runat="server" onclick="Check_All_Closed(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chek_Closed" runat="server" onclick="ToggleCheckBox(this);" />
                                        <asp:HiddenField ID="hfRowId" Value='<%# Eval("ID") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                <asp:TemplateField HeaderText="Site ID">
                                    <ItemTemplate>
                                        <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject ID">
                                    <ItemTemplate>
                                        <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Record No." ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRECID" Font-Size="Small" Text='<%# Bind("RECID") %>' CssClass="disp-none" runat="server"></asp:Label>
                                        <asp:Label ID="Label5" Font-Size="Small" Text='<%# Bind("RECORDNO") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Visit" HeaderStyle-CssClass="align-left">
                                    <ItemTemplate>
                                        <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Module">
                                    <ItemTemplate>
                                        <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Field Name" HeaderStyle-CssClass="align-left">
                                    <ItemTemplate>
                                        <asp:Label ID="FIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query ID" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rule description" HeaderStyle-CssClass="align-left">
                                    <ItemTemplate>
                                        <asp:Label ID="Description" runat="server" Text='<%# Bind("Description") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query Text" HeaderStyle-CssClass="align-left">
                                    <ItemTemplate>
                                        <asp:Label ID="QUERYTEXT" runat="server" Text='<%# Bind("QUERYTEXT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query Status" HeaderStyle-CssClass="align-left">
                                    <ItemTemplate>
                                        <asp:Label ID="STATUSTEXT" runat="server" Text='<%# Bind("STATUSTEXT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query Type" HeaderStyle-CssClass="align-left">
                                    <ItemTemplate>
                                        <asp:Label ID="QUERYTYPE" runat="server" Text='<%# Bind("QUERYTYPE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="align-left">
                                    <HeaderTemplate>
                                        <label>Generated Details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Generated By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div runat="server" id="divGenerated">
                                            <div>
                                                <asp:Label ID="QRYGENBYNAME" runat="server" Text='<%# Bind("QRYGENBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="QRYGEN_CAL_DAT" runat="server" Text='<%# Bind("QRYGEN_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="QRYGEN_CAL_TZDAT" runat="server" Text='<%# Bind("QRYGEN_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="align-left">
                                    <HeaderTemplate>
                                        <label>Closed Details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Closed By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div runat="server" id="divResolved">
                                            <div>
                                                <asp:Label ID="QRYRESBYNAME" runat="server" Text='<%# Bind("QRYRESBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="QRYRES_CAL_DAT" runat="server" Text='<%# Bind("QRYRES_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="QRYRES_CAL_TZDAT" runat="server" Text='<%# Bind("QRYRES_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="MODULEID" runat="server" Text='<%# Bind("MODULEID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="MULTIPLEYN" runat="server" Text='<%# Bind("MULTIPLEYN") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
              
            </div>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="Button_Popup"
     BackgroundCssClass="Background">
</asp:ModalPopupExtender>
<asp:Panel ID="Panel1" runat="server" Style="display: none;" CssClass="Popup1">
     <asp:Button runat="server" ID="Button_Popup" Style="display: none" />
        <asp:UpdatePanel ID="updPnlIDDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <h5 class="heading">Reason for Close Query</h5>                 
                    <div class="modal-body" runat="server">            
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="Label2" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                            </div>
                            <div class="col-md-5">
                                <asp:TextBox ID="txt_Comments" CssClass="form-control" ValidationGroup="Update_CloseQuery" TextMode="MultiLine"
                                    runat="server" Style="width: 400px; height:70px;"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-3">
                                &nbsp;
                            </div>
                            <div class="col-md-9">
                                <asp:Button ID="btn_update" runat="server" Style="margin-left: 37px; height: 34px; font-size: 14px;" ValidationGroup="Update_CloseQuery" CssClass="btn btn-DarkGreen"
                                    OnClientClick="return Check_CommentEntered();" Text="Submit" OnClick="btn_update_Click" />
                                &nbsp;
                            &nbsp;
                            <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" ValidationGroup="Update_CloseQuery"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btn_Cancel_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
 
 
</asp:Panel>
<!-- ModalPopupExtender -->
</asp:Content>
