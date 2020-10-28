using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

namespace Approval
{
    public partial class View_TEV : System.Web.UI.Page
    {
        string id_, pat, use_id, per;
        DataProfile data = new DataProfile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null) Response.Redirect("Default.aspx");
            else use_id = Session["id"].ToString();
            //try
            //{
                id_ = Request.QueryString["id"].ToString();
                pat = Session["pat"].ToString();
                per = Session["per"].ToString();
                if (!IsPostBack)
                {
                    //if (per.Contains("1"))
                    //{
                    //    btnConfirm.Visible = false;                   
                    //}
                    //else
                    //{
                    //    btnApp.Visible = false;
                    //    btnNotApp.Visible = false;
                    //}
                    LoadData();
                }
            //}
            //catch
            //{
            //    Response.Redirect("Censorship_TEV.aspx");
            //}
        }
        public string GetFullName(string id_user)
        {
            string fullname = "";
            string sql = "select fullname from IT_note_user where ID =(" + id_user + ")";
            DataTable name = data.GetDataTable(sql);
            if (name.Rows.Count > 0)
                fullname = name.Rows[0][0].ToString();
            else fullname = "";
            return fullname;
        }
        public string Get_ResonApproval(string id)
        {
            string ResonApproval = "";
            string sqlr = "select description from it_reson_approva where id =(" + id + ")";
            DataTable name = data.GetDataTable(sqlr);
            if (name.Rows.Count > 0)
            {
                ResonApproval = name.Rows[0]["description"].ToString();
            }
            else ResonApproval = "";
            return ResonApproval;

        }
        public void LoadData()
        {
            string sql = "Select * from it_note where id=" + id_;
            DataTable note = data.GetDataTable(sql);
            if (note.Rows.Count > 0)
            {
                txtDate.Text = DateTime.Parse(note.Rows[0]["note_date"].ToString()).ToString("dd/MM/yyyy hh:mm");
                txtwarehouse.Text = note.Rows[0]["warehouse"].ToString();
                //drpReson.Text = note.Rows[0]["reson"].ToString();
                txtReson_no.Text = note.Rows[0]["reson_no"].ToString();
                //txtPart.Text = note.Rows[0]["part"].ToString();
                txtJobname.Text = note.Rows[0]["jobname"].ToString();
                txtModel.Text = note.Rows[0]["model"].ToString();
                txtNo.Text = note.Rows[0]["note_no"].ToString();
                //drToPart.Text = note.Rows[0]["topart"].ToString();
                lbCreate.Text = GetFullName(note.Rows[0]["create_by"].ToString());
                lblresonapproval.Text = Get_ResonApproval(note.Rows[0]["reasoncode"].ToString());
                if (note.Rows[0]["confirm_by"].ToString() != "")
                {
                    lbConfirm.Text = GetFullName(note.Rows[0]["confirm_by"].ToString());
                }
                else
                {
                    imgconfirm.Visible = false;
                }
                //if (note.Rows[0]["semi"].ToString().Contains('1')) drToPart.Text = note.Rows[0]["topart"].ToString();
                //else { drToPart.Visible = false; lbPart.Visible = false; }
                
                LoadGridView();
            }

        }
        public void LoadGridView()
        {
            string sql2 = "select id,item,description,quantity,inventory,note_id from it_note_detail  where note_id =" + id_;
            DataTable note_detail = data.GetDataTable(sql2);
            if (note_detail.Rows.Count > 0)
            {
                grvDetail.ShowFooter = true;
                grvDetail.DataSource = note_detail;
                grvDetail.DataBind();

            }
        }
        protected void grvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvDetail.PageIndex = e.NewPageIndex;
            int trang_thu = e.NewPageIndex;
            int so_dong = grvDetail.PageSize;
            stt = trang_thu * so_dong + 1;
            LoadData();
        }
        int stt = 1;
        public string get_stt()
        {
            return Convert.ToString(stt++);
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            //if (Session["per"].ToString() == "1")
            Response.Redirect("Censorship_TEV.aspx");
            //else
            //Response.Redirect("Warehouse_TEV.aspx");
        }
        float total = 0;
        protected void grvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblqy = (Label)e.Row.FindControl("lbtAmount");
                //float quantity = float.Parse(lblqy.Text);
                //total += quantity;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Label lbtotal = (Label)e.Row.FindControl("lbTotal");
                //lbtotal.Text = String.Format("{0:F6}",total);
            }
        }
        protected void btnApp_Click(object sender, EventArgs e)
        {
            
            //if (lbConfirm.Text == "")
            //{
            //    data.ExcuteQuery("update it_note set  confirm_by=" + use_id + ",confirm_date=sysdate,checked=1,Appro_Date=sysdate,Appro_By=" + use_id + " where id=" + id_);
            //}
            //else
            //{
            data.ExcuteQuery("update it_note set checked=1,Appro_Date= GETDATE(),Appro_By=" + use_id + " where id=" + id_);
            //}
            //string sql = "Select * from it_note_user where id in (select create_by from it_note where id=" + id_ + " )";
            //DataTable mail = data.GetDataTable(sql);
            //string sqlsend = "select * from it_note_user where id=" + use_id;
            //DataTable mailsend = data.GetDataTable(sqlsend);
            //string sqlorg = "select org from it_note where id=" + id_;
            //DataTable orgtmp = data.GetDataTable(sqlorg);
            //string sqluserlog = "select * from it_note_user where org_id like '%" + orgtmp.Rows[0][0].ToString() + "%'";
            //DataTable userlog = data.GetDataTable(sqluserlog);
            //if (mailsend.Rows[0]["passmail"].ToString() == "")
            //{
            //Khong can gui mail nguoc tro lai nguoi tao
            //SendMail _send = new SendMail("webmaster@towada.com.vn", "Yar7u82&", mail.Rows[0]["mail"].ToString(), "Vote " + txtNo.Text + " is approved !", "Vote " + txtNo.Text + " is approved !");
            //_send.SendM();

            //Gui mail toi LOG
            //for (int i = 0; i < userlog.Rows.Count; i++)
            //{
            //    SendMail guilog = new SendMail("webmaster@towada.com.vn", "Yar7u82&", userlog.Rows[i]["mail"].ToString(), "Processing vote " + txtNo.Text, "Processing vote " + txtNo.Text);
            //    guilog.SendM();
            //}
           
            Response.Redirect("Censorship_TEV.aspx");
        }
        protected void btnNotApp_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();

        }
        protected void closemodal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#myModal", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#myModal').hide();", true);
            upModal.Update();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DataTable tmpNote = data.GetDataTable("select * from it_note where id=" + id_);
            if (tmpNote.Rows.Count > 0)
            {
                if (tmpNote.Rows[0]["auto"].ToString().Contains("0"))
                {
                    data.ExcuteQuery("update it_note set checked=2, message=N'" + txtReason.Text + "' where id=" + id_);

                }
                else
                {
                    data.ExcuteQuery("update it_note set checked=2,message=N'" + txtReason.Text + "'  where note_no='" + tmpNote.Rows[0]["note_no"] + "'");
                    data.ExcuteQuery("delete from it_note_detail where note_id=" + id_);
                    data.ExcuteQuery("delete from it_note where id=" + id_);
                }

            }
            Response.Redirect("Censorship_TEV.aspx");

        }
    }
}