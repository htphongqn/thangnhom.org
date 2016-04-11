using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OpenIdRelyingPartyWebForms;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.ProviderAuthenticationPolicy;
using System.Net;
using System.Web.Script.Serialization;
using vpro.functions;
using Controller;

namespace MVC_Kutun.Login
{
    public partial class Facebook : System.Web.UI.Page
    {
        private Account acc = new Account();
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Url.AbsoluteUri;

            //Nếu đường dẫn trả ở Client về có chứa Token chứng thực thì thực hiện kiểm tra
            if (url.Contains("access_token"))
            {
                //Xác thực token
                string accessToken = Request.QueryString["access_token"];
                string requestUrl = "https://graph.facebook.com/me?access_token=" + accessToken;
                WebClient client = new WebClient();
                string userInformation = client.DownloadString(requestUrl);

                //Sau khi xác thực lưu thông tin vào Profile
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var profile = jss.Deserialize<FacebookUserInfo>(userInformation);


                if (profile != null)
                {
                    //Luu thong tin dang nhap cua user vao day
                    HttpContext.Current.Session["user_email"] = profile.email;
                    HttpContext.Current.Session["user_username"] = profile.name;
                    if (!acc.Check_email(profile.email))
                    {
                        acc.Register(profile.name, profile.email, "0123456789", "", "", "", 1, "", "", DateTime.Now);
                    }
                    else
                    {
                        acc.Load_All_Cuss(profile.email);
                    }
                }


                if (HttpContext.Current.Session["Status_LinkFace"] == "1")
                {
                    HttpContext.Current.Session["Status_LinkFace"] = "0";
                    Response.Redirect("http://www.facebook.com/thangnhom.org");
                }

                //Dong trang dang nhap
                Response.Redirect("/Login/Close.aspx");
            }

            else
                if (url.Contains("error"))
                    Response.Write("Có lỗi trong quá trình đăng nhập. <a href=\"javascript:window.close();\">Đóng cửa sổ</a>");

        }
       
    }
    public class FacebookUserInfo
    {
        public FacebookUserInfo()
        {
            //
            // TODO: Thông tin User facebook
            //
        }

        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string link { get; set; }
        public string username { get; set; }
        public string gender { get; set; }
        public string timezone { get; set; }
        public string locale { get; set; }
        public string verified { get; set; }
        public string updated_time { get; set; }
        public string birthday { get; set; }
        //location {}
        //work {}
        //education{}

    }
}