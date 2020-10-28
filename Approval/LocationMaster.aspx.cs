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
    public partial class LocationMaster : System.Web.UI.Page
    {
        DataProfile data = new DataProfile();
        string pat, use;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            use = Session["id"].ToString();
            pat = Session["pat"].ToString();

            if (!IsPostBack)
            {
                Load_drseason();
                Load_drDept();
                Load_warehouse();
                Load_Planner();
                LoadData();
                txtlocation.Text = "";
                //PageLoad();
            }
        }        
        public void refres()
        {
            txtlocation.Text = "";
            Load_drseason();
            Load_drDept();
            Load_warehouse();
            Load_Planner();
            drpplannerNo.SelectedIndex = 0;
            LoadData();            
            HiddenField1.Value = "";
        }
        private void PageLoad()
        {

            Load_warehouse();
            Load_Planner();
            LoadData();
        }
        private void LoadData()
        {
            string sql = "";
            if (drDept.SelectedIndex !=0 || drreason.SelectedIndex!=0 || drwarehouse.SelectedIndex !=0 || drplaner.SelectedIndex !=0 || drpplannerNo.SelectedIndex !=0)
            {
                sql = " select b.warehouse,a.* from locationMaster a left join warehouse b on a.warehouse = b.ID_WH " +
                    " where reasoncode = '" + drreason.SelectedValue + "' and Dept = '" + drDept.SelectedValue + "' and a.warehouse = '" + drwarehouse.SelectedValue + "' and Planner = '" + drplaner.SelectedValue + "'and PlannerNo = '" + drpplannerNo.SelectedValue + "' ";
            }
            else
            {
                sql = " select b.warehouse,a.* from locationMaster a left join warehouse b on a.warehouse = b.ID_WH";
            }
            
            DataTable tbl = data.GetDataTable(sql);
            grvLocation.DataSource = tbl;
            grvLocation.DataBind();
        }
        private void Load_drseason()
        {
            string sql = "select * from IT_RESON_APPROVA";
            DataTable tbl = data.GetDataTable(sql);
            drreason.DataSource = tbl;
            drreason.DataTextField = "description";
            drreason.DataValueField = "ID";
            drreason.DataBind();

            drreason.Items.Insert(0, "--Reason vote--");

        }
        private void Load_drDept()
        {
            string sql = "select * from Dept";
            DataTable tbl = data.GetDataTable(sql);
            drDept.DataSource = tbl;
            drDept.DataTextField = "Dept";
            drDept.DataValueField = "Dept";
            drDept.DataBind();

            drDept.Items.Insert(0, "--Dept--");
        }
        private void Load_warehouse()
        {
            
                string sql = "select * from warehouse ";
                DataTable tbl = data.GetDataTable(sql);
                drwarehouse.DataSource = tbl;
                drwarehouse.DataTextField = "warehouse";
                drwarehouse.DataValueField = "ID_WH";
                drwarehouse.DataBind();

                drwarehouse.Items.Insert(0, "--Warehouse--");
           
        }
        private void Load_Planner()
        {
           
                string sql = "select * from Planner ";
                DataTable tbl = data.GetDataTable(sql);
                drplaner.DataSource = tbl;
                drplaner.DataTextField = "Planner";
                drplaner.DataValueField = "Planner";
                drplaner.DataBind();

                drplaner.Items.Insert(0, "--Planner--");
           
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

        }
        int stt = 1;
        public string get_stt()
        {
            return Convert.ToString(stt++);
        }

        protected void btrefresh_Click(object sender, EventArgs e)
        {
            refres();
            LoadData();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (drDept.SelectedIndex == 0 || drreason.SelectedIndex == 0 || drwarehouse.SelectedIndex == 0 || drplaner.SelectedIndex == 0 || drpplannerNo.SelectedIndex == 0 || String.IsNullOrEmpty(txtlocation.Text))
            {
                Response.Write("<script language='javascript'> alert('Bạn phải nhập đủ dữ liệu!!!') </script>");
            }
            else
            {
                if (HiddenField1.Value != "")
                {
                    int idlo = int.Parse(HiddenField1.Value.ToString());
                    string sql = "update locationMaster set Dept='" + drDept.SelectedValue + "',warehouse='" + drwarehouse.SelectedValue + "',planner='" + drplaner.SelectedValue + "',location='" + txtlocation.Text.Trim() + "', Reasoncode='" + drreason.SelectedValue + "',PlannerNo = '"+ drpplannerNo.SelectedValue +"',update_date = getdate(), update_by = '" + use + "' where id_lo =" + idlo;
                    data.ExcuteQuery(sql);
                    refres();
                    LoadData();
                }
                else
                {
                    string sql2 = "insert into locationMaster(Dept,warehouse,planner,location,Reasoncode,PlannerNo,Create_by,Create_date,status) "
                    + " values('" + drDept.SelectedValue + "','" + drwarehouse.SelectedValue + "','" + drplaner.SelectedValue + "','" + txtlocation.Text.Trim() + "','" + drreason.SelectedValue + "','" + drpplannerNo.SelectedValue + "','" + use + "',getdate(),1)";
                    data.ExcuteQuery(sql2);
                    refres();
                    LoadData();
                }
            }
        }

        protected void btedit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvLocation.Rows)
            {
                CheckBox chk = (row.FindControl("cbSelectAll") as CheckBox);
                if (chk.Checked)
                {
                    int id = int.Parse(grvLocation.DataKeys[row.RowIndex].Value.ToString());
                    HiddenField1.Value = id.ToString();
                    DataTable tam = data.GetDataTable("select * from locationMaster where id_lo = " + id);
                    if (tam.Rows.Count > 0)
                    {
                        drDept.SelectedValue = tam.Rows[0]["Dept"].ToString();
                        drreason.SelectedValue = tam.Rows[0]["Reasoncode"].ToString();
                        drplaner.SelectedValue = tam.Rows[0]["Planner"].ToString();
                        drwarehouse.SelectedValue = tam.Rows[0]["warehouse"].ToString();
                        drpplannerNo.SelectedValue = tam.Rows[0]["PlannerNo"].ToString();
                        txtlocation.Text = tam.Rows[0]["location"].ToString(); ;
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
                    data.ExcuteQuery("delete from locationMaster where id_lo =" + id);
                    break;
                }

            }
            refres();
            LoadData();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btexport_Click(object sender, EventArgs e)
        {
            string sql1 = "select * from LocationMaster";

            DataTable Hoso = data.GetDataTable(sql1);
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = Hoso;
            GridView1.DataBind();
            //GridDecorator.MergeRows(GridView1);
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename= Location_master_List.xls");
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