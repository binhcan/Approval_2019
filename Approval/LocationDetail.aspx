<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LocationDetail.aspx.cs" Inherits="Approval.LocationDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <script type="text/javascript">
        function delete_confim() {
            var result = confirm("Are you sure you want delete ?");
            if (result) {
                return result
            }
            else {
                window.location.reload();
                return false;
            }
        }
    </script>
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
            padding-right: 10px;
        }
    </style>
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
                    <h3 class="box-title">Location Detail Information:</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-6" id="boxbody1" runat="server">
                        <div class="form-group">
                            <label>Warehouse:</label>
                            <asp:DropDownList ID="DropWH" runat="server" class="form-control pull-right"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Item:</label>
                            <asp:TextBox ID="txtCode" class="form-control pull-right" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Description</label>
                            <asp:TextBox ID="txtName" class="form-control pull-right" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6" id="boxbody2" runat="server">
                        <div class="form-group">
                            <label>Location:</label>
                            <asp:TextBox ID="txtLocation" class="form-control pull-right" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Type:</label>
                            <asp:DropDownList ID="DropType" runat="server" class="form-control pull-right">
                                <asp:ListItem Text="---Select Type---" Value="" />
                                <asp:ListItem Text="Location-Inspection" Value="Location-Inspection" />
                                <asp:ListItem Text="Location-RM" Value="Location-RM" />
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>----------------------</label>
                            <br />
                            <asp:Button ID="btnAdd" class="btn btn-primary btn-md" runat="server" Text="Save" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnEdit" class="btn btn-success btn-md" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                            <asp:Button ID="btnDelete" class="btn btn-danger btn-md" runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick="javascript:return delete_confim()" />
                            <asp:Button ID="btCancel" class="btn btn-success btn-md" runat="server" Text="Cancel" OnClick="btCancel_Click" />
                        </div>

                    </div>
                </div>
                <div class="box-footer">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Search by Item:</label>
                            <asp:TextBox ID="txtsearch" class="form-control pull-right" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="btnsearch" class="btn btn-success btn-md" runat="server" Text="Search" OnClick="btnsearch_Click" />
                            <asp:Button ID="btrefresh" class="btn btn-danger btn-md" runat="server" Text="Refresh" OnClick="btrefresh_Click" />
                            <asp:Button ID="btexport" class="btn btn-primary btn-md" runat="server" Text="Export" OnClick="btexport_Click" />
                        </div>
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
                AutoGenerateColumns="False" PageSize="20" Width="95%" DataKeyNames="id"
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
                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                    <asp:BoundField DataField="warehouse" HeaderText="WH">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Item" HeaderText="Item">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Description" HeaderText="Description">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Location" HeaderText="Location">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Type" HeaderText="Location Type">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <%--<asp:BoundField DataField="Create_by" HeaderText="Create by">
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
                    </asp:BoundField>--%>
                </Columns>

                <PagerStyle BackColor="#99CCFF" HorizontalAlign="Center" CssClass="cssPager"/>


            </asp:GridView>
        </div>
    </section>
</asp:Content>
