<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NIWRS_KITS_COUNTRY_CENTRAL.aspx.cs" Inherits="CTMS.NIWRS_KITS_COUNTRY_CENTRAL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function CheckAll() {
            var element = document.getElementById('MainContent_gvKITS_Chk_Sel_All');

            if ($(element).prop('checked') == true) {
                $('.KitChk').find('input[type="checkbox"]').prop('checked', true);
            }
            else if ($(element).prop('checked') == false) {
                $('.KitChk').find('input[type="checkbox"]').prop('checked', false);
            }
        }

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Order To Central</h3>
                </div>
                <div class="form-group">
                    <div class="has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="rows">
                        <div class="row">
                           <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Select Country :</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control required width200px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2">
                                <label>
                                </label>
                            </div>
                            <div class="col-md-2">
                                <label>
                                </label>
                            </div>
                            <div class="col-md-2" style="padding-left: 0; color: red;">
                                <label>
                                    Available Kits
                                </label>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Select Treatment :</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:Repeater runat="server" ID="repeatTreat">
                                        <ItemTemplate>
                                            <div class="col-md-12" style="padding-left: 0; display: inline-flex;">
                                                <div class="col-md-2" style="padding-left: 0;">
                                                    <asp:Label runat="server" ID="lblTreatment" Text='<%# Bind("TREAT_GRP") %>' CssClass="form-control txt_center width100px">
                                                    </asp:Label>
                                                </div>
                                                <div class="col-md-2" style="padding-left: 0;">
                                                    <asp:Label runat="server" ID="lblKitCount" Text='<%# Bind("KIT_COUNT") %>' CssClass="form-control txt_center width100px" ForeColor="Blue" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                                <asp:TextBox ID="txtNoKits" runat="server" CssClass="form-control numeric txt_center width100px "
                                                    MaxLength="5"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Order ID :</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox runat="server" ID="txtOrderID" CssClass="form-control width200px required"
                                        Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row disp-none">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Shipment ID :</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox runat="server" ID="txtShipmentID" CssClass="form-control width200px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    &nbsp;
                                </div>
                                <div class="col-md-10">
                                    <asp:Button ID="btnGetKits" Text="Get Kits" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        OnClick="btnGetKits_Click" />
                                    &nbsp; &nbsp; &nbsp;
                                    <asp:Button ID="btnGenOrder" Visible="false" Text="Generate Order" runat="server"
                                        CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnGenOrder_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12" id="KitCount" runat="server" visible="false">
                                <div class="col-md-11" style="text-align: right">
                                    <asp:Label Font-Bold="true" runat="server">  
                                    Total Number Of Kits :
                                    </asp:Label>
                                </div>
                                <div class="col-md-1">
                                    <asp:Label ID="lblKitcount" runat="server" Font-Bold="true" ></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvKITS" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-striped txt_center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Kit Number">
                                            <ItemTemplate>
                                                <asp:Label ID="KITNO" runat="server" Text='<%# Bind("KITNO") %>'></asp:Label>
                                                <asp:HiddenField runat="server" Value='<%# Bind("ID") %>' ID="ID" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false" HeaderText="Treatment Code">
                                            <ItemTemplate>
                                                <asp:Label ID="TREAT_GRP" runat="server" Text='<%# Bind("TREAT_GRP") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false" HeaderText="Treatment Arms">
                                            <ItemTemplate>
                                                <asp:Label ID="TREAT_GRP_NAME" runat="server" Text='<%# Bind("TREAT_GRP_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kit Expiry Date">
                                            <ItemTemplate>
                                                <asp:Label ID="dtExpire" runat="server" Text='<%# Bind("dtExpire") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lot No.">
                                            <ItemTemplate>
                                                <asp:Label ID="LOTNO" runat="server" Text='<%# Bind("LOTNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="width80px txtCenter">
                                            <HeaderTemplate>
                                                Select All
                                                <br />
                                                <asp:CheckBox ID="Chk_Sel_All" onchange="CheckAll();" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Chk_Sel" CssClass="KitChk" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGenOrder" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
