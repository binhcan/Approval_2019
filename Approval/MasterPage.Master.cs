using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Approval
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        DataProfile data = new DataProfile();
        int use, per;
        string dep;
        protected void Page_Load(object sender, EventArgs e)
        {
            Loadpagedefault();
            //Loadpage();
        }
        private void Loadpagedefault()
        {
           
            if (Session["id"] != null)
            {
                use = Convert.ToInt32(Session["id"].ToString());
                per = Convert.ToInt32(Session["per"].ToString());
                dep = Session["pat"].ToString();

                //Loadpage();
                lbtlogin.Visible = false;
                lblogout.Visible = true;

                if ((per == 1 || per == 14 || per == 4) && dep == "LOG")
                {
                    admin.Visible = true;
                    Dept.Visible = true;
                }
                if (per == 23 && dep == "LOG")
                {
                    Dept.Visible = true;
                }                
                
            }

        }
        //private void Loadpage()
        //{
        //    admin.Visible = false;
        //    Dept.Visible = false;
        //}
        //private void Loadpage1()
        //{
        //    admin.Visible = true;
        //    Dept.Visible = true;
        //}
        //private void Loadpage2()
        //{
        //    admin.Visible = false;
        //    Dept.Visible = true;
        //}
        protected void lbtlogin_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Default.aspx");
        }

        protected void Linkdownload_Click(object sender, EventArgs e)
        {
            string fileName = "File\\Input.xlsx";
            //This method helps to download File from Server.
            DownLoadFileFromServer(fileName);
        }

        protected void lblogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("default.aspx");
        }

        public static string ServerMapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }
        public static HttpResponse GetHttpResponse()
        {
            return HttpContext.Current.Response;
        }

        protected void lbtchangepass_Click(object sender, EventArgs e)
        {
            Response.Redirect("changepass.aspx");
        }

        public static void DownLoadFileFromServer(string fileName)
        {
            //This is used to get Project Location.
            string filePath = fileName;
            //This is used to get the current response.
            HttpResponse res = GetHttpResponse();
            res.Clear();
            res.AppendHeader("content-disposition", "attachment; filename=" + filePath);
            res.ContentType = "application/octet-stream";
            res.WriteFile(filePath);
            res.Flush();
            res.End();
        }
    }
}