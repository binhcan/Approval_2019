<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="KittingDetail.aspx.cs" Inherits="Approval.KittingDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HiddenField2" runat="server" />
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
        <h1>KITTING 
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <%--<asp:Button ID="btnDone" class="btn btn-danger btn-md" runat="server" Text="Back" OnClick="btnDone_Click" />--%>
        </ol>
    </section>
    <section class="content container-fluid">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Vote No:
                        <asp:Label ID="lblvoteno" runat="server" ForeColor="#FF3300"></asp:Label></h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:CheckBox ID="chSL" runat="server" Text="Syteline label" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Item</label>
                            <asp:TextBox ID="txtItem" runat="server" CssClass="form-control pull-right" AutoPostBack="true" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Quantity</label>
                            <asp:TextBox ID="txtqty" runat="server" CssClass="form-control pull-right"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="btautoQty" class="btn btn-danger btn-md" runat="server" Text="Auto Qty" Visible="false" OnClick="btautoQty_Click" OnClientClick="return confirm('Bạn có chắc muốn cập nhật số lượng tự động cho vote này?');" />
                            <asp:Button ID="btupdate" class="btn btn-primary btn-md" runat="server" Text="Save Quantity" OnClick="btupdate_Click" />
                            <asp:Button ID="btncancel" class="btn btn-danger btn-md" runat="server" Text="Cancel" OnClick="btncancel_Click" />
                            <asp:Button ID="btnFinish" class="btn btn-success btn-md" runat="server" Text="Finish Kitting" OnClick="btnFinish_Click" OnClientClick="return confirm('Bạn có chắc muốn kết thúc kitting cho vote này?');" />

                        </div>
                    </div>
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
                    <asp:BoundField DataField="semilot" HeaderText="Semi Lot" />
                    <asp:BoundField DataField="quantity" HeaderText="Quantity">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="note_id" HeaderText="note_id" Visible="False" />
                    <asp:BoundField DataField="quantity_act" HeaderText="Qty Act" />
                    <asp:BoundField DataField="remain" HeaderText="Qty remain" />
                    <asp:BoundField DataField="Kitting_date" HeaderText="Kitting time" />
                    <asp:BoundField DataField="quantity_daily" HeaderText="Kitting Daily" />

                </Columns>
                <PagerStyle BackColor="#99CCFF" HorizontalAlign="Center" CssClass="cssPager" />

                
            </asp:GridView>
        </div>
    </section>
</asp:Content>
