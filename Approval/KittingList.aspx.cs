using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Approval
{
    public partial class KittingList : System.Web.UI.Page
    {
        string per, pat, use;
        DataProfile data = new DataProfile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["per"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            per = Session["per"].ToString();
            use = Session["id"].ToString();
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            string sql = "select a.*, b.fullname from it_note a left join IT_NOTE_USER b on a.user_kitting = b.ID " +
                         " where a.status in (0,1) and a.checked=1 and a.User_kitting = '" + use +"' and a.kittingstatus = '0' order by note_date desc";
            DataTable dt = data.GetDataTable(sql);
            grvNote.DataSource = dt;
            grvNote.DataBind();
        }
        int stt = 1;
        public string get_stt()
        {
            return Convert.ToString(stt++);
        }
        protected void grvNote_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvNote.PageIndex = e.NewPageIndex;
            int trang_thu = e.NewPageIndex;
            int so_dong = grvNote.PageSize;
            stt = trang_thu * so_dong + 1;
            LoadData();
        }
        protected void btnkitting_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvNote.Rows)
            {
                CheckBox chk = (row.FindControl("cbSelectAll") as CheckBox);
                if (chk.Checked)
                {
                    int id = int.Parse(grvNote.DataKeys[row.RowIndex].Value.ToString());
                    Response.Redirect("KittingDetail.aspx?id=" + id);
                    break;
                }
            }
        }

        protected void btncomplete_Click(object sender, EventArgs e)
        {

        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Default.aspx");
        }
    }
}