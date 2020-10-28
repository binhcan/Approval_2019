<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LocationMaster.aspx.cs" Inherits="Approval.LocationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <script type="text/javascript">
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var grid = objRef.parentNode.parentNode.parentNode;
            var row = objRef.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {

                if (inputs[i].type == "checkbox") {
                    var rowElement = grid.rows[i];
                    rowElement.style.backgroundColor = "white";

                    if (objRef.checked && inputs[i] != objRef && inputs[i].checked) {
                        //grid.style.backgroundColor = "white";
                        inputs[i].checked = false;
                    }
                }
            }
            if (objRef.checked) {
                //If checked change color to Aqua               
                row.style.backgroundColor = "aqua";
            }
            else
                row.style.backgroundColor = "white";


        }
    </script>

    <style type="text/css">
        .note_checked {
            background-color: Olive;
        }

        .style1 {
            width: 100%;
        }
    </style>
     <style>
        .cssPager td {
            padding-left: 10px;
            padding-right:  10px;
        }
    </style>
    <script type="text/javascript">
        function apply_confim() {
            var result = confirm("Are you sure you want approval ?");
            if (result) {
                return result
            }
            else {
                window.location.reload();
                return false;
            }
        }
    </script>
    <%--    <section class="content-header">
        <h1>Location master
            <small></small>
        </h1>
        <ol class="breadcrumb">
        </ol>
    </section>--%>
    <section class="content container-fluid">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Location master Information:</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Approval reason</label>
                            <asp:DropDownList ID="drreason" runat="server" class="form-control pull-right" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Department</label>
                            <asp:DropDownList ID="drDept" runat="server" class="form-control pull-right">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Warehouse</label>
                            <asp:DropDownList ID="drwarehouse" runat="server" class="form-control pull-right" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Planner</label>
                            <asp:DropDownList ID="drplaner" runat="server" class="form-control pull-right" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Planner Type</label>
                            <asp:DropDownList ID="drpplannerNo" runat="server" class="form-control pull-right" >
                                <asp:ListItem Text="---Select Type---" Value="" />
                                <asp:ListItem Text="1" Value="1" />
                                <asp:ListItem Text="2" Value="2" />
                                <asp:ListItem Text="3" Value="3" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Location:</label>
                            <asp:TextBox ID="txtlocation" CssClass="form-control pull-right" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="btnView" class="btn btn-primary btn-md" runat="server" Text="View List" OnClick="btnView_Click" />
                            <asp:Button ID="btrefresh" class="btn btn-danger btn-md" runat="server" Text="Refresh" onclick="btrefresh_Click"/>
                            <asp:Button ID="btexport" class="btn btn-primary btn-md" runat="server" Text="Export" OnClick="btexport_Click" />
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <div class="col-xs-4">
                        
                        <asp:Button ID="btnCreate" class="btn btn-success btn-md" runat="server" Text="Save" onclick="btnCreate_Click"/>
                        <asp:Button ID="btedit" class="btn btn-primary btn-md" runat="server" Text="Edit" onclick="btedit_Click"/>
                        <asp:Button ID="btnDelete" class="btn btn-danger btn-md" runat="server" Text="Delete" onclick="btnDelete_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section class="content container-fluid">


        <div class="col-xs-12">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:GridView ID="grvLocation" runat="server" AllowPaging="True"
                AutoGenerateColumns="False" PageSize="20" Width="95%" DataKeyNames="id_lo"
                OnPageIndexChanging="grvLocation_PageIndexChanging"
                OnRowDataBound="grvLocation_RowDataBound" BackColor="White"
                CssClass="Grid"
                AlternatingRowStyle-CssClass="alt"
                PagerStyle-CssClass="pgr">

                <Columns>
                    <asp:TemplateField>
                        <AlternatingItemTemplate>
                            <asp:CheckBox ID="cbSelectAll" runat="server" />
                        </AlternatingItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cbSelectAll" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelectAll" runat="server" onclick="Check_Click(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%#get_stt() %>
                        </ItemTemplate>
                        <ControlStyle Width="20px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID_lo" HeaderText="id Lo" Visible="False" />
                    <asp:BoundField DataField="Dept" HeaderText="Dept">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="warehouse" HeaderText="Warehouse">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Planner" HeaderText="Planner">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PlannerNo" HeaderText="Planner Type">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Location" HeaderText="Location">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Create_by" HeaderText="Create by">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Create_date" HeaderText="Create Date"
                        DataFormatString="{0:dd/MM/yyyy hh:mm tt}">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Update_by" HeaderText="Update by">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Update_date" HeaderText="Update Date"
                        DataFormatString="{0:dd/MM/yyyy hh:mm tt}">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Status" HeaderText="Status">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                </Columns>

                <PagerStyle BackColor="#99CCFF" HorizontalAlign="Center" CssClass="cssPager" />


            </asp:GridView>
        </div>
    </section>
</asp:Content>
