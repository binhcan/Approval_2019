using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Approval
{
    public partial class Default : System.Web.UI.Page
    {
        DataProfile data = new DataProfile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtusername.Focus();
                Session.RemoveAll();

            }
        }

        protected void bttLogin_Click(object sender, EventArgs e)
        {
            string sql = "select * from [IT_NOTE_USER] where Username='" + txtusername.Text.ToLower() + "' and Password='" + txtpassword.Text + "'";
            DataTable login = data.GetDataTable(sql);
            //string pas = txtpass.Text;
            if (login.Rows.Count > 0)
            {


                Session["use"] = txtusername.Text.ToLower();
                Session["pas"] = login.Rows[0]["Password"].ToString();
                Session["id"] = login.Rows[0]["id"].ToString();
                Session["per"] = login.Rows[0]["PERMISSION"].ToString();
                Session["pat"] = login.Rows[0]["PARTS"].ToString();
                Session["wh"] = login.Rows[0]["ORG_ID"].ToString();
                //Response.Redirect("Manage_Detail.aspx");

                //int per = Convert.ToInt32(login.Rows[0]["PERMISSION"].ToString());
                //string Dept = login.Rows[0]["Dept"].ToString();
                string per = login.Rows[0]["PERMISSION"].ToString();
                if (per.Contains("23"))
                {
                    Response.Redirect("warehouse.aspx");
                }
                else if (per.Contains("1") || per.Contains("4"))
                {
                    Response.Redirect("Censorship_TEV.aspx");
                }
                else if (per.Contains("6"))
                {
                    Response.Redirect("KittingList.aspx");
                }
                else if (per.Contains("5"))
                {
                    Response.Redirect("KittingList_M.aspx");
                }
                else if (per.Contains("2"))
                {
                    Response.Redirect("Manage_Detail.aspx");
                }
            }
            else
            {
                Response.Write("<script language='javascript'> alert('Login Fail !') </script>");
                txtusername.Focus();
            }
            txtusername.Text = "";
            txtpassword.Text = "";
        }
    }
}