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
using System.IO;
using System.Configuration;
using vpro.functions;
using Controller;

namespace MVC_Kutun.Login
{
    public partial class Facebook2 : System.Web.UI.Page
    {
        private Account acc = new Account();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the Facebook code from the querystring
            string _email = "";
            string _fullname = "";
            if (Request.QueryString["code"] != "")
            {
                var obj = GetFacebookUserData(Request.QueryString["code"]);
                _email = obj.ToList()[0].email;
                _fullname = obj.ToList()[0].first_name + obj.ToList()[0].last_name;
                if (_email != "")
                {                   
                    HttpContext.Current.Session["user_email"] = _email;
                    HttpContext.Current.Session["user_username"] = _fullname;
                    if (!acc.Check_email(_email))
                    {
                        acc.Register(_fullname, _email, "0123456789", "", "", "", 1, "", "", DateTime.Now);
                    }
                    else
                    {
                        acc.Load_All_Cuss(_email);
                    }
                }

            }
            Response.Redirect("~/Login/Close.aspx");
        }

        protected List<FacebookUserInfo> GetFacebookUserData(string code)
        {
            // Exchange the code for an access token
            Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&redirect_uri=http://thangnhom.org/Login/FaceBook2.aspx&code=" + code);
            HttpWebRequest at = (HttpWebRequest)HttpWebRequest.Create(targetUri);

            System.IO.StreamReader str = new System.IO.StreamReader(at.GetResponse().GetResponseStream());
            string token = str.ReadToEnd().ToString().Replace("access_token=", "");

            // Split the access token and expiration from the single string
            string[] combined = token.Split('&');
            string accessToken = combined[0];

            // Exchange the code for an extended access token
            Uri eatTargetUri = new Uri("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&fb_exchange_token=" + accessToken);
            HttpWebRequest eat = (HttpWebRequest)HttpWebRequest.Create(eatTargetUri);

            StreamReader eatStr = new StreamReader(eat.GetResponse().GetResponseStream());
            string eatToken = eatStr.ReadToEnd().ToString().Replace("access_token=", "");

            // Split the access token and expiration from the single string
            string[] eatWords = eatToken.Split('&');
            string extendedAccessToken = eatWords[0];

            // Request the Facebook user information
            Uri targetUserUri = new Uri("https://graph.facebook.com/me?fields=first_name,last_name,gender,link,locale,email&access_token=" + accessToken);
            HttpWebRequest user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

            // Read the returned JSON object response
            StreamReader userInfo = new StreamReader(user.GetResponse().GetResponseStream());
            string jsonResponse = string.Empty;
            jsonResponse = userInfo.ReadToEnd();

            // Deserialize and convert the JSON object to the Facebook.User object type
            JavaScriptSerializer sr = new JavaScriptSerializer();
            string jsondata = jsonResponse;
            FacebookUserInfo converted = sr.Deserialize<FacebookUserInfo>(jsondata);

            // Write the user data to a List
            List<FacebookUserInfo> currentUser = new List<FacebookUserInfo>();
            currentUser.Add(converted);

            // Return the current Facebook user
            return currentUser;
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
}