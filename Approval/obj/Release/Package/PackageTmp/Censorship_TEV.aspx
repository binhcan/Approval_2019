<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Censorship_TEV.aspx.cs" Inherits="Approval.Censorship_TEV" %>

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

    <style type="text/css">
        .note_checked {
            background-color: Olive;
        }

        .style1 {
            width: 100%;
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

    <section class="content-header">
        <h1>List Vote waiting for confirm or approval
            <small></small>
        </h1>
        <ol class="breadcrumb">
        </ol>
    </section>
    <section class="content container-fluid">
        <div class="col-xs-4">
            <br />
            <asp:button id="btnView" class="btn btn-primary btn-md" runat="server" text="View" onclick="btnView_Click" />
            <%--            <asp:Button ID="btnCreate" class="btn btn-success btn-md" runat="server" Text="Create" onclick="btnCreate_Click"/>
            <asp:Button ID="btnDelete" class="btn btn-danger btn-md" runat="server" Text="Delete" onclick ="btnDelete_Click"/>--%>
        </div>

        <div class="col-xs-12">
            <asp:scriptmanager id="ScriptManager1" runat="server">
                        </asp:scriptmanager>
            <asp:gridview id="grvNote" runat="server" allowpaging="True"
                autogeneratecolumns="False" pagesize="20" width="95%" datakeynames="id"
                onpageindexchanging="grvNote_PageIndexChanging"
                onrowdatabound="grvNote_RowDataBound" backcolor="White">
                    
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
                             <ControlStyle Width="20px" />
                             <ItemStyle HorizontalAlign="Center" />
                         </asp:TemplateField>
                    <asp:BoundField DataField="id" HeaderText="id" Visible="False" />
                    <asp:BoundField DataField="jobname" HeaderText="JobName" >
                       <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                    <asp:BoundField DataField="model" HeaderText="Model" >
                       <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                    <asp:BoundField DataField="reson" HeaderText="Reson" Visible="False" />
                    <asp:BoundField DataField="part" HeaderText="Part" Visible="False" />
                    <asp:BoundField DataField="note_date" HeaderText="Create Date" 
                           DataFormatString="{0:dd/MM/yyyy hh:mm tt}" >
                       <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                       <asp:BoundField DataField="note_no" HeaderText="No Vote" >
                       <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                    <asp:BoundField DataField="create_by" HeaderText="Created By" >
                       <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
<asp:BoundField DataField="confirm_by" HeaderText="Confirm By">
                       <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                    <asp:BoundField DataField="checked" HeaderText="Status" >
                       <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                </Columns>
            
                <PagerStyle BackColor="#99CCFF" HorizontalAlign="Center" />
            
                <HeaderStyle BackColor="#99CCFF" />
            
            </asp:gridview>
        </div>
    </section>
</asp:Content>
