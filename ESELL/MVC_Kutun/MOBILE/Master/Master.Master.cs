using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MVC_Kutun.Components;
using vpro.functions;
using Controller;

namespace MVC_Kutun.MOBILE.Master
{
    public partial class Master : System.Web.UI.MasterPage
    {
        setCookieDevice setckdv = new setCookieDevice();
        Get_session getsession = new Get_session();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) form1.Action = Request.RawUrl;
            if (Check_type())
            {
                div_slider.Visible = false;
            }
            addControlSlide();
        }
        private bool Check_type()
        {
            int _type = Utils.CIntDef(Request["type"]);
            return _type != 0 ? true : false;
        }
        private void addControlSlide()
        {
            UserControl ucpro=null;
            string _catSeoUrl = Utils.CStrDef(Request.QueryString["purl"]);
            if (getsession.check_CatorNews(_catSeoUrl))
                ucpro = Page.LoadControl("/MOBILE/UIs/ProQuantam.ascx") as UserControl;
            else
            {
                int _type = Utils.CIntDef(Request["type"]);
                if(_type==0)
                    ucpro = Page.LoadControl("/MOBILE/UIs/ProBuy.ascx") as UserControl;
            }
            if(ucpro!=null)
                Plprobuy.Controls.Add(ucpro);
        }
        
        protected void Lbdevice_Click(object sender, EventArgs e)
        {
            Session["devicefirts"] = 1;
            setckdv.Removecookie("itemmobile");
            string _url = Utils.CStrDef(Request["purl"]);
            if (!String.IsNullOrEmpty(_url))
                Response.Redirect("/" + _url + ".html");
            else
                Response.Redirect("/");
        }
    }
}