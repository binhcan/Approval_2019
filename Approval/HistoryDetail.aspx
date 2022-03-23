<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HistoryDetail.aspx.cs" Inherits="Approval.HistoryDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .cssPager td {
            padding-left: 10px;
            padding-right: 10px;
        }
    </style>
    <section class="content-header">
        <h1>Vote History
            <small></small>
        </h1>
        <ol class="breadcrumb">
        </ol>
    </section>
    <section class="content container-fluid">
        <div class="col-md-12">
            <div class="box box-primary">
                <%--                 <div class="box-header">
                    <h3 class="box-title">Check histor </h3>
                </div>--%>
                <div class="box-body">                    
                    <div class="col-md-6">
                        <%--<asp:Button ID="btnSearch" class="btn btn-primary btn-md" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        <asp:Button ID="bthistorydetail" class="btn btn-primary btn-md" runat="server" Text="View Detail" OnClick="bthistorydetail_Click" />--%>
                        <asp:Button ID="btnexport" class="btn btn-success btn-md" runat="server" Text="Export excel" OnClick="btnexport_Click" />
                        <asp:Button ID="btnEnd" class="btn btn-danger btn-md" runat="server" Text="Cancel" OnClick="btnEnd_Click" />
                    </div>
                </div>
                <div class="box-footer">
                    <div class="col-md-12">
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:GridView ID="grvhistory" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grvhistory_PageIndexChanging" Width="90%" BackColor="White"
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
                    
                    <asp:BoundField DataField="note_no" HeaderText="Vote" />
                    <asp:BoundField DataField="Item" HeaderText="Item" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="Semilot" HeaderText="Semi Lot" />
                    <asp:BoundField DataField="quantity" HeaderText="Quantity">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField> 
                    <asp:BoundField DataField="quantity_act" HeaderText="Qty Act">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>  
                    <asp:BoundField DataField="reson" HeaderText="Resson code" />
                    <asp:BoundField DataField="Description" HeaderText="Reason vote" />
                </Columns>
                <PagerStyle BackColor="#99CCFF" HorizontalAlign="Center" CssClass="cssPager" />

            </asp:GridView>
        </div>
                </div>
            </div>
        </div>
        
    </section>
</asp:Content>
