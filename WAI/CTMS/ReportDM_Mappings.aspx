<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportDM_Mappings.aspx.cs"
    Inherits="CTMS.ReportDM_Mappings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head id="Head1" runat="server">
    <title>WAI</title>
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .table-bordered > tbody > tr > td
        {
            border: none;
        }
        .fontbold
        {
            font-weight: bold;
        }
        
        .fontxsmall
        {
            font-size: x-small;
        }
        @media print
        {
            body *
            {
                visibility: hidden;
            }
            #section-to-print, #section-to-print *
            {
                visibility: visible;
            }
            #section-to-print
            {
                position: absolute;
                left: 0;
                top: 0;
                margin-left: 0.2px;
                margin-bottom: 0.2px;
                margin-right: 0.2px;
                margin-top: 0.2px;
            }
            .content-block, p
            {
                page-break-inside: avoid;
            }
            .footer
            {
                position: fixed;
                bottom: 0;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="box-body" style="margin-left: 2%; margin-right: 2%; margin-top: 1%">
        <div class="form-group">
            <div id="divprint">
                <asp:HiddenField ID="HDNMODULEID" runat="server" />
                <asp:HiddenField ID="hdnVISITID" runat="server" />
                <asp:HiddenField ID="HDMODULENAME" runat="server" />
                <asp:HiddenField ID="hdnid" runat="server" />
                <asp:HiddenField ID="hdnid1" runat="server" />
                <asp:HiddenField ID="hdnid2" runat="server" />
                <asp:HiddenField ID="hdnfieldname1" runat="server" />
                <asp:HiddenField ID="hdnfieldname2" runat="server" />
                <asp:HiddenField ID="hdnfieldname3" runat="server" />
                <div id="section-to-print">
                    <asp:Repeater runat="server" ID="repeat_AllModule" OnItemDataBound="repeat_AllModule_ItemDataBound">
                        <ItemTemplate>
                            <div style="margin-bottom: 20px; width: 100%;">
                                <div style="border-style: double; margin-bottom: 2px;">
                                    <table>
                                        <tr>
                                            <td style="font-weight: bold;">
                                                Form Name :&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblModuleName" runat="server" Font-Size="14px" Font-Bold="true" Text='<%# Bind("MODULENAME") %>'
                                                    Font-Names="Arial"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="grd_Data" runat="server" Width="100%" AutoGenerateColumns="False"
                                        CssClass="table table-striped" ShowHeader="false" CaptionAlign="Left" OnRowDataBound="grd_Data_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Height="10px" ItemStyle-CssClass="txt_center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblseqno" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                    <asp:Label ID="lblMandatory" ForeColor="Red" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                                    <asp:Label ID="lblCRTDP" ForeColor="DarkOrange" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT;
                                                        color: Blue;" runat="server"></asp:Label>
                                                    <asp:Label ID="lblReadOnly" ForeColor="Brown" Font-Bold="true" Text="𝓡" runat="server"
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="lblInvisible" ForeColor="DarkMagenta" Font-Bold="true" runat="server"
                                                        Visible="false"><i class="fa fa-eye-slash"></i></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCONTROLTYPE" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLTYPE") + "]" %>'
                                                        runat="server"></asp:Label>
                                                    <div class="col-md-12" style="padding-left: 0px;">
                                                        <asp:TextBox ID="TXT_FIELD" runat="server" Width="100px" Height="16px" ForeColor="Maroon"
                                                            Visible="false"></asp:TextBox>
                                                        <asp:Repeater runat="server" ID="repeat_CHK" OnItemDataBound="repeat_CHK_ItemDataBound">
                                                            <ItemTemplate>
                                                                <div class="col-md-12" style="padding-left: 0%; display: inline-flex;">
                                                                    <asp:Label ID="lblSEQNOc" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                    <asp:CheckBox ID="CHK_FIELD" Height="16px" runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' />
                                                                </div>
                                                                <div style="padding: 2px; padding-left: 2%;">
                                                                    <asp:Repeater runat="server" ID="repeat_CHK1" OnItemDataBound="repeat_CHK1_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <div class="col-md-12" style="display: inline-flex;">
                                                                                <div style="width: 200px;">
                                                                                    <asp:Label ID="lblheaderc1SEQNO" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                                    <asp:Label ID="lblheaderc1" runat="server" Style="width: 200px;" ForeColor="Maroon"></asp:Label>&nbsp
                                                                                    <asp:Label ID="lblMandatoryc1" ForeColor="Red" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblCRTDPc1" ForeColor="DarkOrange" Font-Bold="true" runat="server"
                                                                                        Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblReadOnly" ForeColor="Brown" Font-Bold="true" Text="𝓡" runat="server"
                                                                                        Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblInvisible" ForeColor="DarkMagenta" Font-Bold="true" runat="server"
                                                                                        Visible="false"><i class="fa fa-eye-slash"></i></asp:Label>
                                                                                </div>
                                                                                <asp:TextBox ID="TXT_FIELDc1" runat="server" Width="100px" Height="16px" ForeColor="DarkBlue"
                                                                                    Visible="false"></asp:TextBox>&nbsp
                                                                                <asp:Label ID="lblSEQNOc1" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                                <asp:RadioButton ID="RAD_FIELDc1" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;"
                                                                                    runat="server" CssClass="radio" Visible="false" ForeColor="DarkBlue" />
                                                                                <asp:CheckBox ID="CHK_FIELDc1" Style="height: auto; width: 200px; font-weight: bold;"
                                                                                    runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue"
                                                                                    Visible="false" />
                                                                            </div>
                                                                            <div style="padding: 2px; padding-left: 240px;">
                                                                                <asp:Repeater runat="server" ID="repeat_CHK2" OnItemDataBound="repeat_CHK2_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-md-12" style="display: inline-flex;">
                                                                                            <div style="width: 200px;">
                                                                                                <asp:Label ID="lblheaderc2SEQNO" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                                                <asp:Label ID="lblheaderc2" runat="server" Style="width: 200px;" ForeColor="Green"></asp:Label>&nbsp
                                                                                                <asp:Label ID="lblMandatoryc2" ForeColor="Red" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lblCRTDPc2" ForeColor="DarkOrange" Font-Bold="true" runat="server"
                                                                                                    Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lblReadOnly" ForeColor="Brown" Font-Bold="true" Text="𝓡" runat="server"
                                                                                                    Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lblInvisible" ForeColor="DarkMagenta" Font-Bold="true" runat="server"
                                                                                                    Visible="false"><i class="fa fa-eye-slash"></i></asp:Label>
                                                                                            </div>
                                                                                            <asp:TextBox ID="TXT_FIELDc2" runat="server" Width="100px" Height="16px" ForeColor="DarkBlue"
                                                                                                Visible="false"></asp:TextBox>&nbsp
                                                                                            <asp:Label ID="lblSEQNOc2" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                                            <asp:RadioButton ID="RAD_FIELDc2" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;"
                                                                                                runat="server" CssClass="radio" Visible="false" ForeColor="DarkBlue" />
                                                                                            <asp:CheckBox ID="CHK_FIELDc2" Style="height: auto; width: 200px; font-weight: bold;"
                                                                                                runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue"
                                                                                                Visible="false" />
                                                                                        </div>
                                                                                        <div style="padding: 2px; padding-left: 240px;">
                                                                                            <asp:Repeater runat="server" ID="repeat_CHK3" OnItemDataBound="repeat_CHK3_ItemDataBound">
                                                                                                <ItemTemplate>
                                                                                                    <div class="col-md-12" style="display: inline-flex;">
                                                                                                        <div style="width: 200px;">
                                                                                                            <asp:Label ID="lblheaderc3SEQNO" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                                                            <asp:Label ID="lblheaderc3" runat="server" ForeColor="Red"></asp:Label>&nbsp
                                                                                                            <asp:Label ID="lblMandatoryc3" ForeColor="Red" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                                                                                            <asp:Label ID="lblCRTDPc3" ForeColor="DarkOrange" Font-Bold="true" runat="server"
                                                                                                                Visible="false"></asp:Label>
                                                                                                            <asp:Label ID="lblReadOnly" ForeColor="Brown" Font-Bold="true" Text="𝓡" runat="server"
                                                                                                                Visible="false"></asp:Label>
                                                                                                            <asp:Label ID="lblInvisible" ForeColor="DarkMagenta" Font-Bold="true" runat="server"
                                                                                                                Visible="false"><i class="fa fa-eye-slash"></i></asp:Label>
                                                                                                        </div>
                                                                                                        <asp:TextBox ID="TXT_FIELDc3" runat="server" Width="100px" Height="16px" ForeColor="DarkBlue"
                                                                                                            Visible="false"></asp:TextBox>&nbsp
                                                                                                        <asp:Label ID="lblSEQNOc3" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                                                        <asp:RadioButton ID="RAD_FIELDc3" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;"
                                                                                                            runat="server" CssClass="radio" Visible="false" ForeColor="DarkBlue" />
                                                                                                        <asp:CheckBox ID="CHK_FIELDc3" Style="height: auto; font-weight: bold;" runat="server"
                                                                                                            CssClass="checkbox" Text='<%# Bind("TEXT") %>' Visible="false" ForeColor="DarkBlue" />
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:Repeater>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <asp:Repeater runat="server" ID="repeat_RAD" OnItemDataBound="repeat_RAD_ItemDataBound">
                                                            <ItemTemplate>
                                                                <div class="col-md-12" style="padding-left: 0%; display: inline-flex;">
                                                                    <asp:Label ID="lblSEQNO" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                    <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 0px; font-weight: bold;"
                                                                        runat="server" CssClass="radio" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                </div>
                                                                <div style="padding: 2px; padding-left: 2%;">
                                                                    <asp:Repeater runat="server" ID="repeat_RAD1" OnItemDataBound="repeat_RAD1_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <div class="col-md-12" style="display: inline-flex;">
                                                                                <div style="width: 200px;">
                                                                                    <asp:Label ID="lblheader1SEQNO" runat="server" ForeColor="Black" Visible="false"></asp:Label>&nbsp
                                                                                    <asp:Label ID="lblheader" runat="server" ForeColor="Maroon"></asp:Label>&nbsp
                                                                                    <asp:Label ID="lblMandatory1" ForeColor="Red" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblCRTDP1" ForeColor="DarkOrange" Font-Bold="true" runat="server"
                                                                                        Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblReadOnly" ForeColor="Brown" Font-Bold="true" Text="𝓡" runat="server"
                                                                                        Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblInvisible" ForeColor="DarkMagenta" Font-Bold="true" runat="server"
                                                                                        Visible="false"><i class="fa fa-eye-slash"></i></asp:Label>
                                                                                </div>
                                                                                <asp:TextBox ID="TXT_FIELD1" runat="server" Width="100px" Height="16px" Visible="false"
                                                                                    ForeColor="DarkBlue"></asp:TextBox>&nbsp
                                                                                <asp:Label ID="lblSEQNO1" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                                <asp:RadioButton ID="RAD_FIELD1" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;"
                                                                                    runat="server" CssClass="radio" Visible="false" ForeColor="DarkBlue" />
                                                                                <asp:CheckBox ID="CHK_FIELD1" Style="height: auto; width: 400px; font-weight: bold;"
                                                                                    runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue"
                                                                                    Visible="false" />
                                                                            </div>
                                                                            <div style="padding: 2px; padding-left: 240px;">
                                                                                <asp:Repeater runat="server" ID="repeat_RAD2" OnItemDataBound="repeat_RAD2_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-md-12" style="display: inline-flex;">
                                                                                            <div style="width: 200px;">
                                                                                                <asp:Label ID="lblheader2SEQNO" runat="server" ForeColor="Black" Visible="false"></asp:Label>&nbsp
                                                                                                <asp:Label ID="lblheader2" runat="server" ForeColor="Green"></asp:Label>&nbsp
                                                                                                <asp:Label ID="lblMandatory2" ForeColor="Red" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lblCRTDP2" ForeColor="DarkOrange" Font-Bold="true" runat="server"
                                                                                                    Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lblReadOnly" ForeColor="Brown" Font-Bold="true" Text="𝓡" runat="server"
                                                                                                    Visible="false"></asp:Label>
                                                                                                <asp:Label ID="lblInvisible" ForeColor="DarkMagenta" Font-Bold="true" runat="server"
                                                                                                    Visible="false"><i class="fa fa-eye-slash"></i></asp:Label>
                                                                                            </div>
                                                                                            <asp:TextBox ID="TXT_FIELD2" runat="server" Width="100px" Height="16px" ForeColor="DarkBlue"
                                                                                                Visible="false"></asp:TextBox>&nbsp
                                                                                            <asp:Label ID="lblSEQNO2" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                                            <asp:RadioButton ID="RAD_FIELD2" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;"
                                                                                                runat="server" CssClass="radio" Visible="false" ForeColor="DarkBlue" />
                                                                                            <asp:CheckBox ID="CHK_FIELD2" Style="height: auto; width: 400px; font-weight: bold;"
                                                                                                runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue"
                                                                                                Visible="false" />
                                                                                        </div>
                                                                                        <div style="padding: 2px; padding-left: 240px;">
                                                                                            <asp:Repeater runat="server" ID="repeat_RAD3" OnItemDataBound="repeat_RAD3_ItemDataBound">
                                                                                                <ItemTemplate>
                                                                                                    <div class="col-md-12" style="display: inline-flex;">
                                                                                                        <div style="width: 200px;">
                                                                                                            <asp:Label ID="lblheader3SEQNO" runat="server" ForeColor="Black" Visible="false"></asp:Label>&nbsp
                                                                                                            <asp:Label ID="lblheader3" runat="server" ForeColor="Red"></asp:Label>&nbsp
                                                                                                            <asp:Label ID="lblMandatory3" ForeColor="Red" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                                                                                            <asp:Label ID="lblCRTDP3" ForeColor="DarkOrange" Font-Bold="true" runat="server"
                                                                                                                Visible="false"></asp:Label>
                                                                                                            <asp:Label ID="lblReadOnly" ForeColor="Brown" Font-Bold="true" Text="𝓡" runat="server"
                                                                                                                Visible="false"></asp:Label>
                                                                                                            <asp:Label ID="lblInvisible" ForeColor="DarkMagenta" Font-Bold="true" runat="server"
                                                                                                                Visible="false"><i class="fa fa-eye-slash"></i></asp:Label>
                                                                                                        </div>
                                                                                                        <asp:TextBox ID="TXT_FIELD3" runat="server" Width="100px" Height="16px" ForeColor="DarkBlue"
                                                                                                            Visible="false"></asp:TextBox>&nbsp
                                                                                                        <asp:Label ID="lblSEQNO3" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                                                        <asp:RadioButton ID="RAD_FIELD3" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;"
                                                                                                            runat="server" CssClass="radio" Visible="false" ForeColor="DarkBlue" />
                                                                                                        <asp:CheckBox ID="CHK_FIELD3" Style="height: auto; width: 400px; font-weight: bold;"
                                                                                                            runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue"
                                                                                                            Visible="false" />
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:Repeater>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <table width="100%">
                                        <tr id="TR1" runat="server">
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Font-Size="14px" Font-Bold="true" Font-Names="Arial"
                                                    Width="100%" CssClass="list-group-item" ForeColor="DarkBlue" Style="border-color: Black;"><label>Key: [</label><label style="color:Red;">*</label><label>] = Item is required, [<label style="color:#ff8c00;">✔</label><label>] = Source verification required, [</label><label style="color:#a52a2a;">𝓡</label><label>] = Read Only Fields, [</label><i class="fa fa-eye-slash" style="color:#8b008b"></i><label>] = Invisible Fields</label></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <br />
            <br />
        </div>
    </div>
    </form>
</body>
</html>
