<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Create_detail.aspx.cs" Inherits="Approval.Create_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        function SelectAll(id) {
            //get reference of GridView control
            var grid = document.getElementById("<%= grvDetail.ClientID %>");
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
        <h4>Create vote detail
            <small></small>
        </h4>
        <ol class="breadcrumb">
            <asp:Button ID="btnOK" runat="server" class="btn btn-primary btn-md" Text="Done" OnClick="btnOK_Click" />
        </ol>
    </section>
    <section class="content container-fluid">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header">
                    <%--<h3 class="box-title">Information:</h3>--%>
                </div>
                <div class="box-body">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Location:</label>
                            <asp:TextBox ID="txtlocation" class="form-control pull-right" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Item:</label>
                            <asp:TextBox ID="txtCode" class="form-control pull-right" runat="server" AutoPostBack="true" OnTextChanged="txtCode_TextChanged"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Description</label>
                            <asp:TextBox ID="txtName" class="form-control pull-right" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <br />
                            <asp:Button ID="btnAdd" class="btn btn-primary btn-md" runat="server" Text="Add" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Quantity:</label>
                            <asp:TextBox ID="txtQuan" class="form-control pull-right" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Semi Lot:</label>
                            <asp:TextBox ID="txtSemilot" class="form-control pull-right" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Import by excel file</label>
                            <div class="col-md-9">
                                <asp:FileUpload ID="fileupload" runat="server" class="btn btn-default btn-md" />
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btnImport" class="btn btn-primary btn-md" runat="server" Text="Import" OnClick="btnImport_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnEdit" class="btn btn-primary btn-md" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnDelete" class="btn btn-success btn-md" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                    <asp:Button ID="btnRefresh" class="btn btn-danger btn-md" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
                </div>
            </div>
        </div>

        <div class="col-md-12">
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:GridView ID="grvDetail" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvDetail_RowDataBound"
                DataKeyNames="id" AllowPaging="True" OnPageIndexChanging="grvDetail_PageIndexChanging" Width="90%" BackColor="White"
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
                            <asp:CheckBox ID="cbSelectAll" runat="server" />
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
                    <asp:BoundField DataField="location" HeaderText="Location" />
                    <asp:BoundField DataField="Item" HeaderText="Item" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="quantity" HeaderText="Quantity">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="note_id" HeaderText="note_id" Visible="False" />
                    <asp:BoundField HeaderText="Semi Lot" DataField="semilot" />
                    <asp:BoundField HeaderText="Qty Act" DataField="quantity_act" />
                    <asp:BoundField HeaderText="Qty remain" DataField="remain" />
                </Columns>
                <PagerStyle BackColor="#99CCFF" HorizontalAlign="Center" CssClass="cssPager" />

            </asp:GridView>
        </div>
    </section>
</asp:Content>
