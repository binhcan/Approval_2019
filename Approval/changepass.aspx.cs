using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Approval
{
    public partial class changepass : System.Web.UI.Page
    {
        DataProfile data = new DataProfile();
        int Dept_ID, use, per;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null || Session["pat"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            use = Convert.ToInt32(Session["id"].ToString());
            per = Convert.ToInt32(Session["per"].ToString());

            if (!IsPostBack)
            {
                txtusename.Text = Session["use"].ToString();
                txtusename.Enabled = false;
            }
        }

        protected void Changepassword_Click(object sender, EventArgs e)
        {
            string old = txttpassold.Text;
            string newpas = txttpassnew.Text;
            string newpas2 = txttpassnew2.Text;
            if (String.IsNullOrEmpty(txttpassold.Text) || String.IsNullOrEmpty(txttpassnew.Text))
            {
                Response.Write("<script language='javascript'> alert('Password or confirm password is null!') </script>");
            }
            else
            {
                if (newpas.Equals(newpas2) && old == Session["pas"].ToString())
                {

                    string sql = "update IT_Note_User set Password = '" + txttpassnew.Text.Trim() + "' where ID = '" + use + "'";
                    data.ExcuteQuery(sql);
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    Response.Write("<script language='javascript'> alert('Thông tin mật khẩu không đúng!') </script>");
                    txttpassold.Text = "";
                    txttpassnew.Text = "";
                    txttpassnew2.Text = "";
                }
            }
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Default.aspx");
        }
    }
}