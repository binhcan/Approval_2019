<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Manage_Detail.aspx.cs" Inherits="Approval.Manage_Detail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <%--Script hien thi ly do khong duyet--%>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script src="js/jquery-ui.js" type="text/javascript"></script>
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).on("click", "[id*=lnkView]", function() {            
            $("#message").html($(".Message", $(this).closest("tr")).html());
            $("#dialog").dialog({
                title: "View Details",
                buttons: {
                    Ok: function() {
                        $(this).dialog('close');
                    }
                },
                modal: true
            });
            return false;
        });
    </script>
    
    <script type="text/javascript">
        function SelectAll(id) {
            //get reference of GridView control
            var grid = document.getElementById("<%= grvNote_detail.ClientID %>");
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

    <section class="content-header">
        <h1>Manager Detail
            <small></small>
        </h1>
        <ol class="breadcrumb">

            <asp:Button ID="Downloadreport" class="btn btn-success btn-md" runat="server" Text="Download Report" OnClick="Downloadreport_Click" />

        </ol>
    </section>
    <section class="content container-fluid">
        <div class="col-xs-4">
            <br />
            <asp:Button ID="btnEdit" class="btn btn-primary btn-md" runat="server" Text="Modify" OnClick="btnEdit_Click" />
            <asp:Button ID="btnCreate" class="btn btn-success btn-md" runat="server" Text="Create" onclick="btnCreate_Click"/>
            <asp:Button ID="btnDelete" class="btn btn-danger btn-md" runat="server" Text="Delete" onclick ="btnDelete_Click" onclientclick="javascript:return delete_confim()"/>
        </div>

        <div class="col-xs-12">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
            <asp:GridView ID="grvNote_detail" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" PageSize="20" Width="90%" DataKeyNames="id"                   
                    onpageindexchanging="grvNote_detail_PageIndexChanging1" 
                    onrowdatabound="grvNote_detail_RowDataBound1" BackColor="White"  >
                <Columns>
                       <asp:TemplateField>
                            <%--<AlternatingItemTemplate>
                                <asp:CheckBox ID="cbSelectAll" runat="server" />
                            </AlternatingItemTemplate>
                            <HeaderTemplate>
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
                    <asp:BoundField DataField="jobname" HeaderText="JobName" />
                    <asp:BoundField DataField="model" HeaderText="Model" >
                       <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                    <asp:BoundField DataField="reson" HeaderText="Reson" Visible="False" />
                    <asp:BoundField DataField="part" HeaderText="Part" Visible="False" />
                    <asp:BoundField DataField="note_date" HeaderText="Create Date" 
                           DataFormatString="{0:dd/MM/yyyy hh:mm tt}" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                    <asp:BoundField DataField="note_no" HeaderText="No Vote" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="confirm_by" HeaderText="Confirmed By" />
                    <asp:BoundField DataField="checked" HeaderText="Status" />
                     <asp:BoundField DataField="kittingstatus" HeaderText="kittingstatus" Visible="False" />
                  <asp:TemplateField ItemStyle-Width="40" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Img/View.png" />
                        <ajax:popupcontrolextender id="PopupControlExtender1" runat="server" popupcontrolid="Panel1"
                            targetcontrolid="Image1" dynamiccontextkey='<%# Eval("id") %>' dynamiccontrolid="Panel1"
                            dynamicservicemethod="GetDynamicContent" position="Bottom">
                         </ajax:popupcontrolextender>
                    </ItemTemplate>

                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:TemplateField>
                </Columns>
            
                <PagerStyle BackColor="#99CCFF" HorizontalAlign="Center" />
            
                <HeaderStyle BackColor="#99CCFF" />
            
            </asp:GridView>
        </div>
        <asp:Panel ID="Panel1" runat="server">
             </asp:Panel> 
    </section>

</asp:Content>
