using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;

namespace Approval
{
    public partial class LocationDetail : System.Web.UI.Page
    {
        DataProfile data = new DataProfile();
        int use;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null) Response.Redirect("Default.aspx");
            use = Convert.ToInt32(Session["id"].ToString());
            if (!IsPostBack)
            {
                load_trang();
                Load_warehouse();
                LoadData();
            }
        }
        private void Load_warehouse()
        {
            string sql = "select *  from Warehouse ";
            DataTable tbl = data.GetDataTable(sql);
            DropWH.DataSource = tbl;
            DropWH.DataTextField = "warehouse";
            DropWH.DataValueField = "ID_WH";
            DropWH.DataBind();

            DropWH.Items.Insert(0, "--Warehouse--");
        }
        public void load_trang()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtLocation.Text = "";            
            //btnEdit.Visible = false;           
        }
        public void refres()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtLocation.Text = "";
            txtsearch.Text = "";
            DropType.SelectedIndex = 0;
            DropWH.SelectedIndex = 0;
            HiddenField1.Value = "";
        }
        public void LoadData()
        {
            string sql = "";
            if (String.IsNullOrEmpty(txtsearch.Text))
            {
                sql = "select b.warehouse, a.* from locationdetail a left join Warehouse b on a.warehouse = b.ID_WH";
            }
            else
            {
                sql = "select b.warehouse, a.* from locationdetail a left join Warehouse b on a.warehouse = b.ID_WH where item = '"+ txtsearch.Text.Trim() +"' ";
            }
            
            DataTable tbl = data.GetDataTable(sql);
            grvLocation.DataSource = tbl;
            grvLocation.DataBind();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text)||string.IsNullOrEmpty(txtLocation.Text)|| DropWH.SelectedIndex == 0 || DropType.SelectedIndex ==0)
            {
                Response.Write("<script language='javascript'> alert('Bạn phải nhập đủ dữ liệu!!!') </script>");
            }
            else
            {
                if (HiddenField1.Value != "")
                {
                    int idlo = int.Parse(HiddenField1.Value.ToString());
                    string sql = "update locationdetail set item='" + txtCode.Text.Trim() + "',description='" + txtName.Text.Trim() + "',warehouse='" + DropWH.SelectedValue + "',location='" + txtLocation.Text.Trim() + "', type='" + DropType.SelectedValue + "',update_date = getdate(), update_by = '" + use + "' where id=" + idlo;
                    data.ExcuteQuery(sql);
                    refres();
                    LoadData();
                }
                else
                {
                    string sql2 = "insert into locationdetail(item,description,warehouse,location,type,Create_by,Create_date,status) "
                    + " values('" + txtCode.Text + "','" + txtName.Text + "','" + DropWH.SelectedValue + "','" + txtLocation.Text + "','" + DropType.SelectedValue + "','" + use + "',getdate(),1)";
                    data.ExcuteQuery(sql2);
                    refres();
                    LoadData();
                }
            }
            
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvLocation.Rows)
            {
                CheckBox chk = (row.FindControl("cbSelectAll") as CheckBox);
                if (chk.Checked)
                {
                    int id = int.Parse(grvLocation.DataKeys[row.RowIndex].Value.ToString());
                    HiddenField1.Value = id.ToString();
                    DataTable tam = data.GetDataTable("select * from locationdetail where id = " + id);
                    if (tam.Rows.Count > 0)
                    {
                        txtCode.Text = tam.Rows[0]["item"].ToString();
                        txtName.Text = tam.Rows[0]["description"].ToString();
                        txtLocation.Text = tam.Rows[0]["location"].ToString();
                        DropWH.SelectedValue = tam.Rows[0]["warehouse"].ToString();
                        DropType.SelectedValue = tam.Rows[0]["Type"].ToString();
                        txtsearch.Text = "";
                    }
                    break;
                }
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvLocation.Rows)
            {
                CheckBox chk = (row.FindControl("cbSelectAll") as CheckBox);
                if (chk.Checked)
                {
                    int id = int.Parse(grvLocation.DataKeys[row.RowIndex].Value.ToString());
                    data.ExcuteQuery("delete from locationdetail where id=" + id);
                    break;
                }

            }            
            refres();
            LoadData();
        }
        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("warehouse.aspx");
        }

        protected void grvLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvLocation.PageIndex = e.NewPageIndex;
            int trang_thu = e.NewPageIndex;
            int so_dong = grvLocation.PageSize;
            stt = trang_thu * so_dong + 1;
            LoadData();
        }

        protected void grvLocation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Find the checkbox control in header and add an attribute
                ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                        ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
            }
        }
        int stt = 1;
        public string get_stt()
        {
            return Convert.ToString(stt++);
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btrefresh_Click(object sender, EventArgs e)
        {
            txtsearch.Text = "";
            LoadData();
            refres();
        }

        protected void btexport_Click(object sender, EventArgs e)
        {
            string sql1 = "select * from Locationdetail";

            DataTable Hoso = data.GetDataTable(sql1);
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = Hoso;
            GridView1.DataBind();
            //GridDecorator.MergeRows(GridView1);
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename= Location_detail_List.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                // GridView_ListDefault.AllowPaging = false;
                //LoadData();

                GridView1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}