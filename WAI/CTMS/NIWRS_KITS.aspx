<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_KITS.aspx.cs" Inherits="CTMS.NIWRS_KITS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .fontBlue {
            color: Blue;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript" language="javascript">

        function ShowKitDetails(element) {

            var VISIT = $(element).closest('tr').find('td:eq(6)').text().trim();
            var KITNO = $(element).context.innerText;
            var SUBJID = $(element).closest('tr').find('td:eq(3)').text().trim();
            var DISPENSE_IDX = $(element).closest('tr').find('td:eq(0)').find('input').val();

            if ($('#MainContent_hfSponsorApp').val() == '') {
                $('#MainContent_hfSponsorApp').val('No');
            }

            var Approval = $('#MainContent_hfSponsorApp').val();

            window.location = "NIWRS_KITS_REQBAK.aspx?VISIT=" + VISIT + "&SUBJID=" + SUBJID + "&KITNO=" + KITNO + "&DISPENSE_IDX=" + DISPENSE_IDX + "&Spons=" + Approval;
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">Dispensing Summary - Last Dose
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
            <asp:HiddenField runat="server" ID="hfSTEPID" />
            <asp:HiddenField runat="server" ID="hfApproval" />
            <asp:HiddenField runat="server" ID="hfKitWithApprov" />
            <asp:HiddenField runat="server" ID="hfKitWithoutApprov" />
            <asp:HiddenField runat="server" ID="hfSponsorApp" />
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div class="box-body">
                            <div class="rows">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                            CssClass="table table-bordered txt_center table-striped notranslate" OnRowDataBound="gridData_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSponsorApp" runat="server"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfDispenseIDX" Value='<%# Eval("DISPENSE_IDX") %>' />
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKitCount" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                                        
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
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
