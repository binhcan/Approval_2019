using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;

namespace Approval
{
    public partial class KittingDetail_M : System.Web.UI.Page
    {
        int id, reasoncode; string use, pat, wh;
        DataProfile data = new DataProfile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null) Response.Redirect("Default.aspx");
            try
            {
                use = Session["id"].ToString();
                pat = Session["pat"].ToString();
                wh = Session["wh"].ToString();
                id = int.Parse(Request.QueryString["id"].ToString());

                if (!IsPostBack)
                {

                    LoadData();
                    LoadDropkitting();
                }
            }
            catch
            {
                Response.Redirect("KittingList_M.aspx");
            }
        }
        private void LoadDropkitting()
        {
                DataTable tbl = data.GetDataTable("select id,fullname from it_note_user where permission = 6 and ORG_ID = '"+ wh +"' ");
                Dropkitting.DataSource = tbl;
                Dropkitting.DataTextField = "fullname";
                Dropkitting.DataValueField = "ID";
                Dropkitting.DataBind();

                Dropkitting.Items.Insert(0, "--select PIC--");          
            
        }
        public void LoadData()
        {
            string sql = "Select *, b.description,c.warehouse, d.fullname from it_note a " +
                " left join IT_RESON_APPROVA b on a.reasoncode = b.id" +
                " left join warehouse c on a.warehouse = c.ID_WH" +
                " left join IT_NOTE_USER d on d.id = a.User_kitting " +
                " where a.id=" + id;
            DataTable note = data.GetDataTable(sql);
            if (note.Rows.Count > 0)
            {
                txtDate.Text = DateTime.Parse(note.Rows[0]["note_date"].ToString()).ToString("dd/MM/yyyy hh:mm");

                txtreasonvote.Text = note.Rows[0]["description"].ToString();
                txtwarehouse.Text = note.Rows[0]["warehouse"].ToString();
                txtplanner.Text = note.Rows[0]["planner"].ToString();
                txtReson_no.Text = note.Rows[0]["reson_no"].ToString();
                txtPart.Text = note.Rows[0]["part"].ToString();
                txtJobname.Text = note.Rows[0]["jobname"].ToString();
                txtModel.Text = note.Rows[0]["model"].ToString();
                lblvoteno.Text = note.Rows[0]["note_no"].ToString();
                txtkitting.Text = note.Rows[0]["fullname"].ToString();

                //lay ten reson
                string sql1 = "Select * from it_reson_creat where code='" + txtReson_no.Text.Trim() + "'";
                DataTable note1 = data.GetDataTable(sql1);
                if (note1.Rows.Count > 0)
                {
                    lblReson.Text = note1.Rows[0]["Description"].ToString();
                }
                else
                {
                    lblReson.Text = "Sai lý do";
                }

                //drToPart.Text = note.Rows[0]["topart"].ToString();            
                //if (note.Rows[0]["semi"].ToString().Contains('1')) drToPart.Text = note.Rows[0]["topart"].ToString();
                //else { drToPart.Visible = false; lbPart.Visible = false; }
                //if (note.Rows[0]["semi"].ToString().Contains('1'))
                //{
                //    drToPart.Text = note.Rows[0]["topart"].ToString();
                //    if (note.Rows[0]["NgayNhan"].ToString() != "")
                //        txtNgayNhan.Text = DateTime.Parse(note.Rows[0]["NgayNhan"].ToString()).ToString("dd/MM/yyyy");
                //}
                //else { lbPart.Visible = false; lbNgayNhan.Visible = false; txtNgayNhan.Visible = false; IbtnSave0.Visible = false; }

                LoadGridView();
            }

        }
        public void LoadGridView()
        {
            string sql2 = "select *, [quantity] - [quantity_act] as remain from it_note_detail where displaydetail = 1 and note_id=" + id;
            DataTable note_detail = data.GetDataTable(sql2);
            if (note_detail.Rows.Count > 0)
            {
                grvDetail.DataSource = note_detail;
                grvDetail.DataBind();
            }
        }


        int stt = 1;
        public string get_stt()
        {
            return Convert.ToString(stt++);
        }
        protected void grvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvDetail.PageIndex = e.NewPageIndex;
            int trang_thu = e.NewPageIndex;
            int so_dong = grvDetail.PageSize;
            stt = trang_thu * so_dong + 1;
            LoadGridView();
        }
        protected void grvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Find the checkbox control in header and add an attribute
                ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                        ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToDouble(e.Row.Cells[7].Text) == Convert.ToDouble(e.Row.Cells[9].Text))
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    //e.Row.BackColor = System.Drawing.Color.OrangeRed;
                }
                else if (Convert.ToDouble(e.Row.Cells[7].Text) - Convert.ToDouble(e.Row.Cells[9].Text) > 0 && Convert.ToDouble(e.Row.Cells[9].Text) > 0)
                {
                    e.Row.BackColor = System.Drawing.Color.OrangeRed;
                    //e.Row.ForeColor = System.Drawing.Color.White;
                }
            }
        }
        protected void closemodal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#myModal", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#myModal').hide();", true);
            upModal.Update();
        }

        protected void btsetkitting_Click(object sender, EventArgs e)
        {
            if (Dropkitting.SelectedIndex == 0)
            {
                Response.Write("<script language='javascript'> alert('Bạn phải chọn người kitting') </script>");
            }
            else
            {
                string sql = "update IT_NOTE set user_kitting = '" + Dropkitting.SelectedValue + "', kittingstatus = '0' where id =" + id;
                data.ExcuteQuery(sql);
                Response.Redirect("KittingList_M.aspx");
            }

        }
        protected void btnreturn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("KittingList_M.aspx");
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string sql2 = "select * from it_note where id=" + id;
            DataTable dttmp = data.GetDataTable(sql2);
            string sqlvote = "update it_note set checked=0,status = 3, message = N'" + txtReason.Text + "' where id=" + id;
            data.ExcuteQuery(sqlvote);

            string vote_no = dttmp.Rows[0]["Note_no"].ToString();
            string info = vote_no + "-" + use + "- return vote -";
            GhiTextFile(info + DateTime.Now.ToString("ddMMyy"));

            string sql1 = "Select * from it_note_user where id in (select create_by from it_note where id=" + id + " )";
            DataTable mail = data.GetDataTable(sql1);
            //string sqlsend = "select * from it_note_user where id=" + use_id;
            //DataTable mailsend = data.GetDataTable(sqlsend);
            if (mail.Rows[0]["mail"].ToString() != "")
            {
                SendMail _send = new SendMail("webmaster@towada.com.vn", "Yar7u82&", mail.Rows[0]["mail"].ToString(), "Recheck vote " + dttmp.Rows[0]["note_no"].ToString(), "Recheck vote " + dttmp.Rows[0]["note_no"].ToString());
                _send.SendM();
            }
            Response.Redirect("KittingList_M.aspx");
        }
        public void GhiTextFile(string tmp)
        {
            string path = Server.MapPath("~/Log/tmp_" + use + ".txt");
            if (!File.Exists(path))
            {
                File.CreateText(path).Close();
            }
            using (StreamWriter sw1 = new StreamWriter(path, true))
            {
                sw1.WriteLine(tmp);
                sw1.Close();
            }

            //List<string> text = File.ReadAllLines(path).ToList();
            //text.Add(tmp);
            //File.WriteAllLines(path, text.ToArray());
        }
    }
}