<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CTMS_CreateAgendaTopic.aspx.cs" Inherits="CTMS.CTMS_CreateAgendaTopic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 300px;">
                                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">
                                            Add Agenda Topic
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Enter Agenda Topic Name:</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:TextBox ID="txtTopicName" ValidationGroup="section" runat="server"
                                                                CssClass="form-control required"></asp:TextBox>
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
                                                            <asp:Button ID="btnsubmit" Text="Submit" runat="server" 
                                                                CssClass="btn btn-primary btn-sm cls-btnSave" onclick="btnsubmit_Click"
                                                                />
                                                            <asp:Button ID="btnupdate" Text="Update" Visible="false" runat="server"
                                                                CssClass="btn btn-primary btn-sm cls-btnSave" onclick="btnupdate_Click"  />
                                                            <asp:Button ID="btncancel" Text="Cancel" runat="server" 
                                                                CssClass="btn btn-primary btn-sm" onclick="btncancel_Click"
                                                                />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box box-primary">
                                    <div class="box-header with-border" style="float: left;">
                                        <h4 class="box-title" align="left">
                                            Records
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="rows">
                                                    <div style="width: 100%; height: 264px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdAgendaTopic" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" 
                                                                onrowcommand="grdAgendaTopic_RowCommand" >
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Topic ID" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="TopicIID" runat="server" Text='<%# Bind("TopicID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Agenda Topic" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="AgendaTopic" runat="server" Text='<%# Bind("AgendaTopic") %>'></asp:Label>
                                                                        </ItemTemplate>                                                                     
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("TopicID") %>'
                                                                                CommandName="EditVisit" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("TopicID") %>'
                                                                                CommandName="DeleteVisit" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                    </div>
                    </div>
                    </div>
                    
</asp:Content>
