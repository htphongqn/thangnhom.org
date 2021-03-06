﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using vpro.functions;
using Controller;
using MVC_Kutun.Components;
using System.Configuration;

namespace MVC_Kutun.vi_vn
{
    public partial class Payment_step3 : System.Web.UI.Page
    {
        #region Declare
        Payment_cart pay = new Payment_cart();
        Cart_result rscart = new Cart_result();
        clsFormat fm = new clsFormat();
        SendMail sm = new SendMail();
        Config cf = new Config();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            var _configs = cf.Config_meta();

            if (_configs.ToList().Count > 0)
            {
                if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                    ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";
            }

            HtmlHead header = base.Header;
            HtmlMeta headerDes = new HtmlMeta();
            HtmlMeta headerKey = new HtmlMeta();
            headerDes.Name = "Description";
            headerKey.Name = "Keywords";

            header.Title = "Thanh toán";
            Guid _guid = (Guid)Session["news_guid"];
            if (!pay.Check_Cart(_guid))
            {
                Response.Redirect("/", false);
            }
            loadBanking();
            loadCast();
        }
        private void loadBanking()
        {
            Litbanking.Text = cf.Show_File_HTML("contact-banking.htm", "/Data/contact/");
        }
        private void loadCast()
        {
            Litcast.Text = cf.Show_File_HTML("contact-cast.htm", "/Data/contact/");
        }
        private void Finish(int _hinhthucpay)
        {
            try
            {

                Guid _guid = (Guid)Session["News_guid"];

                //Thông tin lưu vào bảng đặt hàng
                decimal _ship = 0;
                if (Session["price"] != null)
                {
                    _ship = Utils.CIntDef(Session["price"]);
                }
                string _sName = Utils.CStrDef(Request["name"]);
                string _sEmail = Utils.CStrDef(Request["email"]);
                string _sAddress = Utils.CStrDef(Request["add"]);
                int _iPaymentID = Utils.CIntDef(Request["typepay"]);
                string _sDesc = Utils.CStrDef(Request["note"]);
                DateTime _dateDate = DateTime.Now;
                string _sEmailCC = string.Empty;
                string _sPhone = Utils.CStrDef(Request["phone"]);
                string _hinhtuc = _hinhthucpay == 1 ? "Thanh toán bằng tiền mặt" : "Thanh toán tại ngân hàng";
                string _webname = ConfigurationManager.AppSettings["EmailDisplayName"];
                string _url = ConfigurationManager.AppSettings["URLWebsite"];
                var _ccMail = cf.Getemail(3);
                if (_ccMail.ToList().Count > 0)
                {
                    _sEmailCC = _ccMail.ToList()[0].EMAIL_TO;
                }
                //var listShop = null;
                //foreach (var item in listShop)
                //{
                    
                //}
                if (pay.Payment_cart_rs(_guid, rscart.Total_Amount(_guid), _sEmail, _sName, _sPhone, _sAddress, _iPaymentID, _hinhtuc, _sDesc, _webname, _url, _ship))
                {
                    sm.SendEmailSMTP("Thông báo: Bạn đã đặt hàng thành công", _sEmail,"" ,_sEmailCC, pay._Mailbody, true, false);
                    pay.Delete_basket(_guid);
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Thông báo: Xác nhận thành công. Cảm ơn bạn đã mua hàng tại cửa hàng của chúng tôi!');</script>");
                }
                else Response.Write("<script LANGUAGE='JavaScript' >alert('Lỗi!');document.location='" + ResolveClientUrl("/") + "';</script>");


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        protected void LbPayment2_Click(object sender, EventArgs e)
        {
            Finish(2);
            Response.Redirect("/finish.html?orderid=" + Payment_cart._idorder);
        }

        protected void LbPayment_Click1(object sender, EventArgs e)
        {
            Finish(1);
            Response.Redirect("/finish.html?orderid=" + Payment_cart._idorder);
        }
    }
}