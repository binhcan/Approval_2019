<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Warehouse.aspx.cs" Inherits="Approval.Warehouse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .note_checked {
            background-color: Olive;
        }
    </style>
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
    <script type="text/javascript">
        function SelectAll(id) {
            //get reference of GridView control
            var grid = document.getElementById("<%= grvNote.ClientID %>");
            //variable to contain the cell of the grid
            var cell;

            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length; i++) {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
    </script>
    <style>
        .cssPager td {
            padding-left: 10px;
            padding-right:  10px;
        }
    </style>
    <section class="content-header">
        <h1>WAREHOUSE
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <asp:Button ID="bthisory" class="btn btn-danger btn-md" runat="server" Text="History" OnClick="bthisory_Click" />
        </ol>
    </section>
    <section class="content container-fluid">
        <div class="box box-primary">
            <div class="box-body">
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Dept</label>
                        <asp:DropDownList ID="drdept" runat="server" class="form-control pull-right">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Warehouse</label>
                        <asp:DropDownList ID="drwarehouse" runat="server" class="form-control pull-right">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <br />
                        <asp:Button ID="btview" class="btn btn-primary btn-md" runat="server" Text="View List Vote" OnClick="btview_Click" />
                        <asp:Button ID="btnrefresh" class="btn btn-danger btn-md" runat="server" Text="Refresh" OnClick="btnrefresh_Click" />
                        <asp:Button ID="btnDetail" class="btn btn-success btn-md" runat="server" Text="View Vote Detail" OnClick="btnDetail_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12">
            <asp:GridView ID="grvNote" runat="server" AllowPaging="True"
                AutoGenerateColumns="False" PageSize="20" Width="80%" DataKeyNames="id"
                OnPageIndexChanging="grvNote_PageIndexChanging" OnRowDataBound="grvNote_RowDataBound"
                BackColor="White"
                CssClass="Grid"
                AlternatingRowStyle-CssClass="alt"
                PagerStyle-CssClass="pgr">
                <Columns>
                    <asp:TemplateField>
                        <%-- <AlternatingItemTemplate>
                                <asp:CheckBox ID="cbSelectAll" runat="server" />
                            </AlternatingItemTemplate>--%>
                        <%-- <HeaderTemplate>
                                <asp:CheckBox ID="cbSelectAll" runat="server" />
                            </HeaderTemplate>--%>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelectAll" runat="server" onclick="Check_Click(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%#get_stt() %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="id" HeaderText="id" Visible="False" />
                    <asp:BoundField DataField="jobname" HeaderText="JobName">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="model" HeaderText="Model">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="reson" HeaderText="Reson" Visible="False" />
                    <asp:BoundField DataField="part" HeaderText="Part" Visible="False" />
                    <asp:BoundField DataField="note_date" HeaderText="Create Date"
                        DataFormatString="{0:dd/MM/yyyy hh:mm}">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="note_no" HeaderText="Vote">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fullname" HeaderText="Kitting by" />
                    <asp:BoundField DataField="Kittingstatus" HeaderText="Kitting status" />
                </Columns>

                <PagerStyle BackColor="#99CCFF" HorizontalAlign="Center" CssClass="cssPager" />


            </asp:GridView>
        </div>

    </section>
</asp:Content>
