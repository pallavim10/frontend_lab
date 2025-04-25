<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DM_ANOTATED_CRF_VISIT_WISE.aspx.cs" Inherits="CTMS.DM_ANOTATED_CRF_VISIT_WISE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head id="Head1" runat="server">
    <title>WAI</title>
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .table-bordered > tbody > tr > td {
            border: none;
        }

        .fontbold {
            font-weight: bold;
        }

        .fontxsmall {
            font-size: x-small;
        }

        @media print {
            body * {
                visibility: hidden;
            }

            #section-to-print, #section-to-print * {
                visibility: visible;
            }

            #section-to-print {
                position: absolute;
                left: 0;
                top: 0;
                margin-left: 0.2px;
                margin-bottom: 0.2px;
                margin-right: 0.2px;
                margin-top: 0.2px;
            }

            .content-block, p {
                page-break-inside: avoid;
            }

            .footer {
                position: fixed;
                bottom: 0;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box-body" style="margin-left: 2%; margin-right: 2%; margin-top: 1%">
            <div class="form-group" runat="server">
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
                                                <td style="font-weight: bold;">Visit Name :&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblVisit" runat="server" Font-Size="14px" Font-Bold="true" Text='<%# Bind("VISIT") %>'
                                                        Font-Names="Arial"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold;">Form Name :&nbsp;
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
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <div class="col-md-12" style="padding-left: 0px;">
                                                            <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                                                <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;"
                                                    runat="server"></asp:Label>
                                                            <asp:Label ID="VARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                            <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>' ForeColor="Maroon"
                                                                runat="server"></asp:Label>
                                                            <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server"></asp:Label>
                                                            <asp:TextBox ID="TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                                Visible="false"></asp:TextBox>&nbsp;
                                                <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                    <asp:Repeater runat="server" ID="repeat_CHK" OnItemDataBound="repeat_CHK_ItemDataBound">
                                                        <ItemTemplate>
                                                            <div class="col-md-12" style="padding-left: 1%; display: inline-flex;">
                                                                <asp:Label ID="lblSEQNO" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                     <asp:CheckBox ID="CHK_FIELD" Style="height: auto; width: 200px; font-weight: bold;"
                                                                         runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue"
                                                                         Visible="false" />
                                                            </div>
                                                            <div style="padding-left: 25px;">
                                                                <asp:Repeater runat="server" ID="repeat_CHK1" OnItemDataBound="repeat_CHK1_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <div class="col-md-12" style="display: inline-flex;">
                                                                            <div style="width: 210px; margin-bottom: 5px;">
                                                                                <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                                            <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;" Visible="false"
                                                                                runat="server"></asp:Label>
                                                                                <asp:Label ID="lblVARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Visible="false"
                                                                                    Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                                                <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                            </div>
                                                                            <asp:TextBox ID="TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                                                Visible="false"></asp:TextBox>&nbsp;
                                                                            <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblOPTIONSEQNO" runat="server" ForeColor="DarkViolet" Text='<%# Eval("DRPSEQNO") + "." %>' Visible="false"></asp:Label>&nbsp;
                                                                            <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;" Text='<%# Bind("TEXT") %>' Visible="false"
                                                                                runat="server" CssClass="radio" ForeColor="DarkBlue" />&nbsp;
                                                                            <asp:CheckBox ID="CHK_FIELD" Style="height: auto; width: 250px; font-weight: bold;" Visible="false"
                                                                                runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                            <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>'
                                                                                Visible="false" ForeColor="Maroon" runat="server"></asp:Label>&nbsp;
                                                                        </div>
                                                                        <div style="padding-left: 240px;">
                                                                            <asp:Repeater runat="server" ID="repeat_CHK2" OnItemDataBound="repeat_CHK2_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <div class="col-md-12" style="display: inline-flex;">
                                                                                        <div style="width: 210px; margin-bottom: 5px;">
                                                                                            <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                                            <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;" Visible="false"
                                                                                runat="server"></asp:Label>
                                                                                            <asp:Label ID="lblVARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Visible="false"
                                                                                                Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                                                            <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                                        </div>
                                                                                        <asp:TextBox ID="TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                                                            Visible="false"></asp:TextBox>&nbsp;
                                                                                        <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblOPTIONSEQNO" runat="server" ForeColor="DarkViolet" Text='<%# Bind("DRPSEQNO") %>' Visible="false"></asp:Label>&nbsp
                                                                            <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;" Text='<%# Bind("TEXT") %>' Visible="false"
                                                                                runat="server" CssClass="radio" ForeColor="DarkBlue" />
                                                                                        <asp:CheckBox ID="CHK_FIELD" Style="height: auto; width: 250px; font-weight: bold;" Visible="false"
                                                                                            runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                                        <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>'
                                                                                            Visible="false" ForeColor="Maroon" runat="server"></asp:Label>
                                                                                    </div>
                                                                                    <div style="padding-left: 240px;">
                                                                                        <asp:Repeater runat="server" ID="repeat_CHK3" OnItemDataBound="repeat_CHK3_ItemDataBound">
                                                                                            <ItemTemplate>
                                                                                                <div class="col-md-12" style="display: inline-flex;">
                                                                                                    <div style="width: 210px; margin-bottom: 5px;">
                                                                                                        <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                                            <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;" Visible="false"
                                                                                runat="server"></asp:Label>
                                                                                                        <asp:Label ID="lblVARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Visible="false"
                                                                                                            Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                                                                        <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                                                    </div>
                                                                                                    <asp:TextBox ID="TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                                                                        Visible="false"></asp:TextBox>&nbsp;
                                                                                                    <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblOPTIONSEQNO" runat="server" ForeColor="DarkViolet" Text='<%# Bind("DRPSEQNO") %>' Visible="false"></asp:Label>&nbsp
                                                                            <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;" Text='<%# Bind("TEXT") %>' Visible="false"
                                                                                runat="server" CssClass="radio" ForeColor="DarkBlue" />
                                                                                                    <asp:CheckBox ID="CHK_FIELD" Style="height: auto; width: 250px; font-weight: bold;" Visible="false"
                                                                                                        runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                                                    <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>'
                                                                                                        Visible="false" ForeColor="Maroon" runat="server"></asp:Label>
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
                                                                    <div class="col-md-12" style="padding-left: 1%; display: inline-flex;">
                                                                        <asp:Label ID="lblSEQNO" runat="server" ForeColor="DarkViolet" Visible="false"></asp:Label>&nbsp
                                                                    <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 0px; font-weight: bold;"
                                                                        runat="server" CssClass="radio" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                    </div>
                                                                    <div style="padding-left: 25px;">
                                                                        <asp:Repeater runat="server" ID="repeat_RAD1" OnItemDataBound="repeat_RAD1_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <div class="col-md-12" style="padding-left: 1%; display: inline-flex;">
                                                                                    <div style="width: 210px; margin-bottom: 5px;">
                                                                                        <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                                            <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;" Visible="false"
                                                                                runat="server"></asp:Label>
                                                                                        <asp:Label ID="lblVARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Visible="false"
                                                                                            Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                                                        <br />
                                                                                        <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                                    </div>
                                                                                    <asp:TextBox ID="TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                                                        Visible="false"></asp:TextBox>&nbsp;
                                                                        <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblOPTIONSEQNO" runat="server" ForeColor="DarkViolet" Text='<%# Eval("DRPSEQNO") + "." %>' Visible="false"></asp:Label>&nbsp;
                                                                            <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;" Text='<%# Bind("TEXT") %>' Visible="false"
                                                                                runat="server" CssClass="radio" ForeColor="DarkBlue" />&nbsp;
                                                                            <asp:CheckBox ID="CHK_FIELD" Style="height: auto; font-weight: bold;" Visible="false"
                                                                                runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                                    <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>'
                                                                                        Visible="false" ForeColor="Maroon" runat="server"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblIndications2" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                                </div>
                                                                                <div style="padding-left: 240px;">
                                                                                    <asp:Repeater runat="server" ID="repeat_RAD2" OnItemDataBound="repeat_RAD2_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <div class="col-md-12" style="display: inline-flex;">
                                                                                                <div style="width: 210px; margin-bottom: 5px;">
                                                                                                    <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                                            <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;" Visible="false"
                                                                                runat="server"></asp:Label>
                                                                                                    <asp:Label ID="lblVARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Visible="false"
                                                                                                        Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                                                                    <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                                                </div>
                                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                                                                    Visible="false"></asp:TextBox>&nbsp;
                                                                                    <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblOPTIONSEQNO" runat="server" ForeColor="DarkViolet" Text='<%# Bind("DRPSEQNO") %>' Visible="false"></asp:Label>&nbsp
                                                                            <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;" Text='<%# Bind("TEXT") %>' Visible="false"
                                                                                runat="server" CssClass="radio" ForeColor="DarkBlue" />
                                                                                                <asp:CheckBox ID="CHK_FIELD" Style="height: auto; font-weight: bold;" Visible="false"
                                                                                                    runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                                                <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>'
                                                                                                    Visible="false" ForeColor="Maroon" runat="server"></asp:Label>
                                                                                                <asp:Label ID="lblIndications2" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                                            </div>
                                                                                            <div style="padding-left: 240px;">
                                                                                                <asp:Repeater runat="server" ID="repeat_RAD3" OnItemDataBound="repeat_RAD3_ItemDataBound">
                                                                                                    <ItemTemplate>
                                                                                                        <div class="col-md-12" style="display: inline-flex;">
                                                                                                            <div style="width: 210px; margin-bottom: 5px;">
                                                                                                                <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;
                                                                            <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT; color: Blue;" Visible="false"
                                                                                runat="server"></asp:Label>
                                                                                                                <asp:Label ID="lblVARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Visible="false"
                                                                                                                    Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
                                                                                                                <asp:Label ID="lblIndications" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
                                                                                                            </div>
                                                                                                            <asp:TextBox ID="TXT_FIELD" runat="server" Width="150px" Height="16px" ForeColor="DarkBlue" CssClass="form-control"
                                                                                                                Visible="false"></asp:TextBox>&nbsp;
                                                                                                <asp:Label ID="LBL_FIELD" runat="server" ForeColor="Red" Visible="false"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblOPTIONSEQNO" runat="server" ForeColor="DarkViolet" Text='<%# Bind("DRPSEQNO") %>' Visible="false"></asp:Label>&nbsp
                                                                            <asp:RadioButton ID="RAD_FIELD" Style="min-height: 5px; margin-bottom: 2px; font-weight: bold;" Text='<%# Bind("TEXT") %>' Visible="false"
                                                                                runat="server" CssClass="radio" ForeColor="DarkBlue" />
                                                                                                            <asp:CheckBox ID="CHK_FIELD" Style="height: auto; font-weight: bold;" Visible="false"
                                                                                                                runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' ForeColor="DarkBlue" />
                                                                                                            <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>'
                                                                                                                Visible="false" ForeColor="Maroon" runat="server"></asp:Label>
                                                                                                            <asp:Label ID="lblIndications2" ForeColor="Brown" Font-Bold="true" Text="" runat="server" Visible="false"></asp:Label>
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
                                            <tr id="TRKeys" runat="server">
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Font-Size="14px" Font-Bold="true" Font-Names="Arial"
                                                        Width="100%" CssClass="list-group-item" ForeColor="DarkBlue" Style="border-color: Black;"><label>Key: <br />
                                                <label style="color:DarkOrange;">[ABC]</label> = VariableName, <label style="color:Maroon;"> [XYZ]</label> = Control Type, [AN] = Alphanumerical
                                                <br />
                                                <label style="color:black;">Display Features:</label> [<i class="fa fa-bold"></i>] = Bold, [<i class="fa fa-eye-slash"></i>] = Mask Field, [<label style="color:maroon;font-family:cursive">R</label>] = Read only<br />
                                                <label style="color:black;">Data Significance:</label> [<label style="width: 20px; height: 10px; vertical-align: middle; border:1px solid #f2e8e8; margin-top: 5px !important;background-color:yellow;"></label>] = Required Information, [*] and [<label style="width: 20px; height: 10px; vertical-align: middle; border: 1px solid #e11111; margin-top: 5px !important;"></label>] = Mandatory Information, [⚠️] = Critical Data Point, [<i class="fa fa-user-md"></i>] = Medical Authority Response, [<i class="fa fa-clone"></i>] = Duplicates Check Information<br />
                                                <label style="color:black;">Data Linkages:</label>  [<i class="fa fa-link"></i>(P)] = Linked Field (Parent), [<i class="fa fa-link"></i>(C)] = Linked Field (Child), [<i class="fa fa-flask"></i>] = Lab Referance Range, [<i class="fa fa-desktop"></i>] = AutoCode<br />
                                                <label style="color:black;">Multiple Data Entry:</label> [<i class="fa fa-sort-numeric-asc"></i>] = Sequential Auto-Numbering, [<i class="fa fa-exchange-alt"></i>] = Non-Repetitive, [<i class="fa fa-list"></i>] = In List Data<br />
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="TRMODULECRIT" runat="server">
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Conditional Visibility Criterias" Font-Size="14px"
                                                        Font-Bold="true" Font-Names="Arial" Width="100%" CssClass="list-group-item" Style="border-color: Black; margin-top: 10px;"></asp:Label>
                                                    <asp:GridView ID="grd_Module_Crit" Width="100%" runat="server" AutoGenerateColumns="true"
                                                        CssClass="table table-striped" CaptionAlign="Left">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr id="TRVISITCRIT" runat="server" style="margin-bottom: 10%;">
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Width="100%" Text="Visit Visibility Criterias" Font-Size="14px"
                                                        Font-Bold="true" Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; margin-top: 10px;"></asp:Label>
                                                    <asp:GridView ID="grd_Visit_CRIT" Width="100%" runat="server" AutoGenerateColumns="true"
                                                        CssClass="table table-striped" CaptionAlign="Left">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr id="TRONSUBMIT_CRT" runat="server" style="margin-bottom: 10%;">
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text=" OnSubmit/OnLoad Criterias" Font-Size="14px"
                                                        Font-Bold="true" Font-Names="Arial" Width="100%" CssClass="list-group-item" Style="border-color: Black; margin-top: 10px;"></asp:Label>
                                                    <asp:GridView ID="grd_OnSubmit_CRIT" Width="100%" runat="server" AutoGenerateColumns="true"
                                                        CssClass="table table-striped" CaptionAlign="Left">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr id="TRONCHANGE" runat="server" style="margin-bottom: 10%;">
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="OnChange Criterias" Font-Size="14px"
                                                        Font-Bold="true" Font-Names="Arial" Width="100%" CssClass="list-group-item" Style="border-color: Black; margin-top: 10px;"></asp:Label>
                                                    <asp:GridView ID="grd_OnChange_CRIT" Width="100%" runat="server" AutoGenerateColumns="true"
                                                        CssClass="table table-striped" CaptionAlign="Left">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr id="TRLABDEFAULTS" runat="server" style="margin-bottom: 10%;">
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Lab Referance Range" Font-Size="14px"
                                                        Font-Bold="true" Font-Names="Arial" Width="100%" CssClass="list-group-item" Style="border-color: Black; margin-top: 10px;"></asp:Label>
                                                    <asp:GridView ID="grdLabData" Width="100%" runat="server" AutoGenerateColumns="true"
                                                        CssClass="table table-striped" CaptionAlign="Left">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style='page-break-after: always;'>&nbsp;</div>
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
