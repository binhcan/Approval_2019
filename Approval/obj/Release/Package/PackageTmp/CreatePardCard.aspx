<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreatePardCard.aspx.cs" Inherits="Approval.CreatePardCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">  
        body  
        {  
            font-family: Arial;  
            font-size: 10pt;  
        }  
        .table  
        {  
            border: 1px solid #ccc;  
            border-collapse: collapse;  
            height:161px;
            table-layout:fixed;
            margin-left:5px;
            margin-bottom:5px;
            width:50%;
        }  
        .table th  
        {  
            background-color: #F7F7F7;  
            color: #333;  
            font-weight: bold;  
            width:110px;
            padding: 5px;  
            border: 1px solid #ccc; 
            text-align:left;
            width:130px;
        }    
        .table td 
        {
            padding: 5px;  
            width:300px;
            border: 1px solid #ccc;
        }
        .td1{
            width:60px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="content-header">
        <h1>CREATE PART CARD<small></small></h1>
        <ol class="breadcrumb">
            <%--<asp:Button ID="btprint" class="btn btn-primary" runat="server" Text="Print" onclick='printDiv();' />--%>
            <input type="button" value="Print" id="btnPrint" class="btn btn-primary" onclick='printDiv();' />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnDone" class="btn btn-danger btn-md" runat="server" Text="Cancel" OnClick ="btnDone_Click" />
            <asp:Button ID="btnfinish" class="btn btn-success btn-md" runat="server" Text="Finish Pard" OnClick="btnfinish_Click" OnClientClick="return confirm('Bạn có chắc muốn kết thúc in partcard cho vote này?');"/>
        </ol>
    </section>
    <br />
<section class="content container-fluid">
    <div id="DivIdToPrint">
        <asp:DataList ID="DataList1" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" OnItemDataBound="DataList1_ItemDataBound" RepeatLayout="Table" CellPadding="15">           
            <ItemTemplate>
                    <table border="1" class="table">
                        <tr>
                            <th>VI TRI KHO</th>
                            <td colspan="2" class="th1"><asp:Label ID="lbllocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label></td>
                        </tr>
                        <tr>
                            <th > MA LINH KIEN</th>
                            <td colspan="2" class="th1"><asp:Label ID="lblItem" runat="server" Text='<%# Eval("Item") %>'></asp:Label></td>
                        </tr>
                        <tr>
                            <th> TEN LINH KIEN</th>
                            <td colspan="2"><asp:Label ID="lbldes" runat="server" Text='<%# Eval("Description") %>'></asp:Label></td>
                        </tr>
                        <tr>
                            <th>SO LUONG</th>
                            <td></td>
                            <td rowspan="3" style="text-align:right;" class="td1"><asp:Image ID="imgQrcode" runat="server" Width="60px" Height="60px" Visible="false" /></td>
                        </tr>
                        <tr>
                            <th>NGAY THANG</th>
                            <td><asp:Label ID="lbldate" runat="server" Text='<%# DateTime.Now.ToString("dd/MM/yyyy") %>'></asp:Label></td>
                        </tr>
                        <tr>
                            <th>VOTE NO</th>
                            <td><asp:Label ID="lblVoteNo" runat="server" Text='<%# Eval("Note_No") %>'></asp:Label></td>
                        </tr>
                        </table>                 
            </ItemTemplate>            
        </asp:DataList>
    </div>
</section>

<script type="text/javascript">
    function printDiv() 
    {
        var divToPrint = document.getElementById('DivIdToPrint');
        var a = window.open('', 'Print-Window');
        a.document.open();
        //new
        a.document.write('<html><head><style type="text/css">');
        a.document.write('table{border-collapse:collapse;width:100%;height:161px}');
        a.document.write('th{text-align:left;width:130px;font-weight:bold;padding:5px;font-size:11px;white-space: nowrap;}');
        a.document.write('td{padding:5px;width:300px;font-size:11px;}');
        a.document.write('.td1{width:60px;}');
        a.document.write('.th1{font-size:15px;font-weight:bold;}');
        a.document.write('.table{margin-right:5px;margin-bottom:2px;}');
        a.document.write('</style></head>');
        a.document.write('<body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
        //end new
        a.document.close();
        setTimeout(function () { a.close(); }, 10);
    }
</script>
</asp:Content>
