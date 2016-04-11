using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using vpro.functions;
using raovat.net.web;


namespace MVC_Kutun.Login
{
    public partial class Google : System.Web.UI.Page
    {
        dbRaovatNetDataContext db = new dbRaovatNetDataContext();
        protected string googleplus_client_id = "158143585526-tfi549gfv9prqdq4vt0fhsuk8hl30o7t.apps.googleusercontent.com";    // Replace this with your Client ID
        protected string googleplus_client_sceret = "aHmsptDlhfUmKQqMXe5uCNSK";                                                // Replace this with your Client Secret
        protected string googleplus_redirect_url = "http://chophuyen.vn/Login/Google.aspx";                                         // Replace this with your Redirect URL; Your Redirect URL from your developer.google application should match this URL.
        protected string Parameters;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Contents.Count > 0)
            {
                if (Session["loginWith"] != null)
                {
                    if (Session["loginWith"].ToString() == "google")
                    {
                        try
                        {
                            var url = Request.Url.Query;
                            if (url != "")
                            {
                                string queryString = url.ToString();
                                char[] delimiterChars = { '=' };
                                string[] words = queryString.Split(delimiterChars);
                                string code = words[1];

                                if (code != null)
                                {
                                    //Gán Token đuợc yêu cầu 
                                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
                                    webRequest.Method = "POST";
                                    Parameters = "code=" + code + "&client_id=" + googleplus_client_id + "&client_secret=" + googleplus_client_sceret + "&redirect_uri=" + googleplus_redirect_url + "&grant_type=authorization_code";
                                    byte[] byteArray = Encoding.UTF8.GetBytes(Parameters);
                                    webRequest.ContentType = "application/x-www-form-urlencoded";
                                    webRequest.ContentLength = byteArray.Length;
                                    Stream postStream = webRequest.GetRequestStream();
                                    // Thêm dữ liệu tới trang web yêu cầu
                                    postStream.Write(byteArray, 0, byteArray.Length);
                                    postStream.Close();

                                    //Đọc dữ liệu
                                    WebResponse response = webRequest.GetResponse();
                                    postStream = response.GetResponseStream();
                                    StreamReader reader = new StreamReader(postStream);
                                    string responseFromServer = reader.ReadToEnd();

                                    GooglePlusAccessToken serStatus = JsonConvert.DeserializeObject<GooglePlusAccessToken>(responseFromServer);

                                    if (serStatus != null)
                                    {
                                        string accessToken = string.Empty;
                                        accessToken = serStatus.access_token;

                                        if (!string.IsNullOrEmpty(accessToken))
                                        {
                                            // Gán dữ liệu nếu đăng nhập thành công
                                            getgoogleplususerdataSer(accessToken);
                                        }
                                        else
                                        { }
                                    }
                                    else
                                    { }
                                }
                                else
                                { }
                            }
                        }
                        catch (Exception ex)
                        {
                            //throw new Exception(ex.Message, ex);
                            Response.Redirect("/");
                        }
                    }
                }
            }
        }
        protected void Google_Click(object sender, EventArgs e)
        {
            var Googleurl = "https://accounts.google.com/o/oauth2/auth?response_type=code&redirect_uri=" + googleplus_redirect_url + "&scope=https://www.googleapis.com/auth/userinfo.email%20https://www.googleapis.com/auth/userinfo.profile&client_id=" + googleplus_client_id;
            Session["loginWith"] = "google";
            Response.Redirect(Googleurl);
        }
        public class GooglePlusAccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string id_token { get; set; }
            public string refresh_token { get; set; }
        }
        private void getgoogleplususerdataSer(string access_token)
        {
            try
            {
                string _email = "";
                string _fullname = "";
                //HttpClient client = new HttpClient();
                var urlProfile = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + access_token;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlProfile);

                //Đọc dữ liệu urlProfile đã nhận
                using (var response = req.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string x = reader.ReadToEnd();
                    string[] lines = Regex.Split(x.Replace("\": ", ":").Replace("\n \"", "").Replace("\"", "").Replace("{", ""), ",");
                    if (lines.Count() > 0)
                    {
                        for (int i = 0; i < lines.Count(); i++)
                        {
                            if (Regex.Split(lines[i], ":").Count() == 2)
                            {//Gán dữ liệu Email
                                if (Regex.Split(lines[i], ":")[0] == "email")
                                    _email = Regex.Split(lines[i], ":")[1];
                            }
                            if (Regex.Split(lines[i], ":").Count() == 2)
                            {//Gàn dữ liệu Tên
                                if (Regex.Split(lines[i], ":")[0] == "name")
                                    _fullname = Regex.Split(lines[3], ":")[1];
                            }
                        }
                    }
                }

                if (_email != "")
                {
                    HttpContext.Current.Session["user_email"] = _email;
                    HttpContext.Current.Session["user_username"] = _fullname;
                    if (!CheckExitsEmail(_email))
                    {
                        USER user = new USER();
                        user.EMAIL = _email;
                        user.NAME = _fullname;
                        user.LASTUPDATE = DateTime.Now;
                        user.ACTIVE = 1;

                        db.USERs.InsertOnSubmit(user);
                        db.SubmitChanges();
                        HttpContext.Current.Session["user_id"] = user.OID;
                    }
                    else
                    {
                        var item = db.USERs.Where(n => n.EMAIL == _email);
                        if (item != null && item.ToList().Count > 0)
                        {
                            HttpContext.Current.Session["user_id"] = item.ToList()[0].OID;
                        }
                    }
                    //Response.Redirect("/");
                }
                string strScript = "<script>";
                strScript += "window.onunload = refreshParent;function refreshParent() {window.opener.location.href='/';}";
                //strScript += "window.onunload = refreshParent;function refreshParent() {window.opener.location.reload();}";
                strScript += "window.parent.close();";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
            catch (Exception ex)
            {
                //catching the exception
            }
        }
        private bool CheckExitsEmail(string email)
        {
            try
            {
                var _user = db.GetTable<USER>().Where(u => u.EMAIL == email.Trim());

                if (_user.ToList().Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return true;
            }
        }
    }
}