using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Approval
{
    public partial class Create_TEV : System.Web.UI.Page
    {
        DataProfile data = new DataProfile();
        string pat, use, loc;

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
                Load_Page();
            }
        }
        public void Load_Page()
        {
            btnContinue.Visible = true;
            btnEnd.Visible = true;
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtPart.Text = pat;
            txtJobname.Text = "";
            txtModel.Text = "";
            Load_drseason();
            Load_warehouse();
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
        private void Load_warehouse()
        {            
            string sql = "select *  from Warehouse ";
            DataTable tbl = data.GetDataTable(sql);
            drwarehouse.DataSource = tbl;
            drwarehouse.DataTextField = "warehouse";
            drwarehouse.DataValueField = "ID_WH";
            drwarehouse.DataBind();

            drwarehouse.Items.Insert(0, "--Warehouse--");
            
        }
        private void Load_Planner()
        {
            
            string sql = "select DISTINCT Planner from LocationMaster where PlannerNo = '" + Convert.ToInt32(HiddenField1.Value) + "' and warehouse = '"+ drwarehouse.SelectedValue +"' and (Dept = '"+pat+"' or Dept = 'ALL') and reasoncode ='"+ drreason.SelectedValue +"'  ";
            DataTable tbl = data.GetDataTable(sql);
            drplaner.DataSource = tbl;
            drplaner.DataTextField = "Planner";
            drplaner.DataValueField = "Planner";
            drplaner.DataBind();

            drplaner.Items.Insert(0, "--Planner--");

        }
        protected void btnContinue_Click(object sender, EventArgs e)
        {
            if (use == null) Response.Redirect("Default.aspx");
            String sql; string no_tmp = CreateNo();
            int partcardstatus = 0;
            //lay location cho vote
            if (drreason.SelectedIndex == 5 || drreason.SelectedIndex == 6 || drreason.SelectedIndex == 8 || Convert.ToInt32(drreason.SelectedValue) == 9)
            {
                string sql1 = "select location from locationMaster where Dept = '" + txtPart.Text.Trim() + "' and warehouse = '" + drwarehouse.SelectedValue + "' and planner = '" + drplaner.SelectedValue + "' and ReasonCode = '" + drreason.SelectedValue + "' ";
                DataTable tbl1 = data.GetDataTable(sql1);
                loc = tbl1.Rows[0]["Location"].ToString();
            }
            else if (Convert.ToInt32(drreason.SelectedValue) == 1 || Convert.ToInt32(drreason.SelectedValue) == 2 || Convert.ToInt32(drreason.SelectedValue) == 3 )
            {
                loc = "Location-RM";
            }
            else if (Convert.ToInt32(drreason.SelectedValue) == 4)
            {
                loc = "No_location";
            }
            else if (Convert.ToInt32(drreason.SelectedValue) == 7)
            {
                loc = "location_by_user";
            }
            //check partcardstatus
            if ((Convert.ToInt32(drreason.SelectedValue) == 2 || Convert.ToInt32(drreason.SelectedValue) == 3 || Convert.ToInt32(drreason.SelectedValue) == 6) && drwarehouse.SelectedValue == "5")
            {
                partcardstatus = 1;
            }
            else partcardstatus = 0;


            sql = "insert into it_note(reson,part,jobname,model,note_date,note_no,checked,status,create_by,warehouse,semi,reson_no,auto,Reasoncode,Planner,Locationtmp,KittingStatus,partcardstatus) "
                           + "values ('" + txtReson_no.Text + "','" + txtPart.Text + "','" + txtJobname.Text.Trim() + "','" + txtModel.Text.Trim() + "',GETDATE(),'" + no_tmp + "',0,1," + use + ",'"+ drwarehouse.SelectedValue +"' ,0, '" + txtReson_no.Text + "' ,0,'"+ drreason.SelectedValue +"', '"+ drplaner.SelectedValue +"', '"+ loc + "',0,'" + partcardstatus + "') ";
                data.ExcuteQuery(sql);
                DataTable get_id = data.GetDataTable("select top 1 id from it_note where create_by = '" + use + "' order by (id) desc");
                if (get_id.Rows.Count > 0)
                {
                    int detail_id = Convert.ToInt32(get_id.Rows[0]["id"].ToString());
                    Response.Redirect("Create_detail.aspx?id=" + detail_id);
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
                       
                
        }

        protected void btnEnd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
        public string CreateNo()
        {
            string sql = "SELECT count(ID) from IT_NOTE where auto=0 and part='" + pat + "' and convert(varchar,note_date,103)= CONVERT(varchar,getdate(),103)";
            //string sql = "select count(t.id) from it_note t where t.auto=0 and t.part='"+pat+"' and to_date(t.note_date,'dd/MM/yyyy')=to_date(sysdate,'dd/MM/yyyy')";
            DataTable count = data.GetDataTable(sql);
            return pat + DateTime.Now.ToString("ddMMyy") + (int.Parse(count.Rows[0][0].ToString()) + 1).ToString();
        }

        protected void drreason_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select PlannerNo from IT_RESON_APPROVA where id = '" + drreason.SelectedValue + "' ";
            DataTable tbl = data.GetDataTable(sql);
            if (tbl.Rows.Count > 0)
            {
                HiddenField1.Value = tbl.Rows[0]["PlannerNo"].ToString();
            }

            Load_warehouse();
            Load_Planner();
        }

        protected void drwarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Planner();
        }

        protected void drplaner_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtReson_no_TextChanged(object sender, EventArgs e)
        {
            DataTable model = data.GetDataTable("select * from it_reson_creat where code='" + txtReson_no.Text.Trim() + "'");
            //lblReson.Text = "";
            //DataTable model = data.GetDataTable("select * from mtl_system_items a where a.org_id!=8");
            if (model.Rows.Count > 0)
            {
                lblReson.Text = model.Rows[0]["Description"].ToString();
            }
            else
            {
                txtReson_no.Text = "";
                lblReson.Text = "Nhập sai Reson Code";
            }

        }
    }
}