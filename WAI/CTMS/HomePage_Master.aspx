<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HomePage_Master.aspx.cs" Inherits="CTMS.HomePage_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>

        document.addEventListener('DOMContentLoaded', function () {
            var calendarEl = document.getElementById('calendar');


            var calendardata = eval($("#<%=hfCode.ClientID%>").val());

            var calendar = new FullCalendar.Calendar(calendarEl, {
                plugins: ['dayGrid', 'timeGrid', 'list', 'bootstrap'],
                timeZone: 'UTC',
                height: 350,
                themeSystem: 'bootstrap',
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
                },
                eventLimit: true, // allow "more" link when too many events
                events: calendardata
            });

            calendar.render();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="eventContent" title="Event Details">
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Repeater runat="server" ID="repeatTiles" OnItemDataBound="repeatTiles_ItemDataBound">
                                <ItemTemplate>
                                    <div class="txt_center col-md-3">
                                        <div id="divBox" runat="server" style="z-index: 0; height: 65px; margin-bottom: 2px;">
                                            <div class="inner SetTrigger txt_center">
                                                <asp:Label runat="server" ID="lblScore"></asp:Label>
                                                <asp:Label ID="lblVal" runat="server" Font-Bold="true" Font-Size="Larger"></asp:Label>&nbsp;
                                                <br />
                                                <asp:Label ID="lblName" runat="server" Text="Tile" Font-Size="Larger" Font-Bold="true">
                                                </asp:Label>
                                                <asp:HiddenField runat="server" ID="hfIndicID" Value="1" />
                                            </div>
                                            <a href="#" class="small-box-footer"></a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="box box-primary" style="height: 5%;">
                                    <div class="box-body">
                                        <div class="card card-primary">
                                            <div class="card-body p-0">
                                                <!-- THE CALENDAR -->
                                                <asp:HiddenField ID="hfCode" runat="server" />
                                                <div id="calendar" style="min-height: 300px;">
                                                </div>
                                            </div>
                                            <!-- /.card-body -->
                                        </div>
                                        <!-- /.card -->
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box box-success">
                                    <div class="box-header with-border" style="float: left;">
                                        <h4 class="box-title" align="left">
                                            To Do
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px; height: 170px;">
                                        </div>
                                    </div>
                                </div>
                                <div class="box box-danger">
                                    <div class="box-header with-border" style="float: left;">
                                        <h4 class="box-title" align="left">
                                            Urgent Attention Required
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px; height: 170px;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
