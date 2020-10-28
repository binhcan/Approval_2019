<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="KittingList.aspx.cs" Inherits="Approval.KittingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
     <style>
        .cssPager td {
            padding-left: 10px;
            padding-right:  10px;
        }
    </style>
    <section class="content-header">
        <h1>List Vote waiting for kitting
            <small></small>
        </h1>
        <ol class="breadcrumb">
        </ol>
    </section>
    <section class="content container-fluid">
        <div class="col-xs-4">
            <br />
            <asp:Button ID="btnkitting" class="btn btn-primary btn-md" runat="server" Text="Kitting" OnClick="btnkitting_Click" />
            <%--<asp:Button ID="btncomplete" class="btn btn-success btn-md" runat="server" Text="Finished" onclick ="btncomplete_Click" />--%>
            <asp:Button ID="btncancel" class="btn btn-danger btn-md" runat="server" Text="Cancel" OnClick="btncancel_Click" />

        </div>
        <div class="col-md-12">
            <asp:GridView ID="grvNote" runat="server" AllowPaging="True"
                AutoGenerateColumns="False" PageSize="20" Width="80%" DataKeyNames="id"
                OnPageIndexChanging="grvNote_PageIndexChanging"
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
                </Columns>

                <PagerStyle BackColor="#99CCFF" HorizontalAlign="Center" CssClass="cssPager" />

            </asp:GridView>
        </div>
    </section>
</asp:Content>
