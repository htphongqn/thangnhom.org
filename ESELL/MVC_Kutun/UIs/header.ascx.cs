using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using Controller;
using System.Data;
using MVC_Kutun.Components;
using System.Text;
using System.Web.UI.HtmlControls;

namespace MVC_Kutun.UIs
{
    public partial class header : System.Web.UI.UserControl
    {
        #region Declare
        Propertity per = new Propertity();
        Function fun = new Function();
        Cart_result cart = new Cart_result();
        Checkcookie ck = new Checkcookie();
        Home index = new Home();
        setCookieSignin setck = new setCookieSignin();
        Account acc = new Account();
        List_product list_pro = new List_product();
        Product_Details pro_detail = new Product_Details();
        Config cf = new Config();
        Register_email rg = new Register_email();
        clsFormat fm = new clsFormat();
        SendMail send = new SendMail();
        string _cat_seo_url = "";
        int _count = 1;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _cat_seo_url = Request.QueryString["purl"];
            if (Utils.CIntDef(Session["User_ID"]) == 0)
                setSession();
            if (!string.IsNullOrEmpty(Utils.CStrDef(Session["User_ID"])))
            {
                lbthanhvien.Text = Utils.CStrDef(Session["User_Name"]);
                div_login.Visible = false;
                div_logout.Visible = true;
                //div_register.Visible = false;
            }
            else
            {
                //div_register.Visible = true;
                div_login.Visible = true;
                div_logout.Visible = false;
            }
            if (!IsPostBack)
            {
                Guid _guid = (Guid)Session["news_guid"];
                Load_Cart(_guid);
                //Lbtotal.Text = fm.FormatMoney(cart.Total_Amount(_guid));
                load_logo();
                Load_prolike();
                Load_Menu1();
                loadYear();
                loadMonth();
                loadDay();
            }
        }
        public string Total_Amount()
        {
            Guid _guid = (Guid)Session["news_guid"];
            return fm.FormatMoney(cart.Total_Amount(_guid));
        }
        private void setSession()
        {
            acc.setSessionCokie(ck.getCookieSignin());
        }
        #region Logout

        protected void lbtnDangXuat_Click(object sender, EventArgs e)
        {
            try
            {
                //sau khi đăng xuất, xóa hết sản phẩm trong giỏ hàng của người đó
                LogOut();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void LogOut()
        {
            try
            {
                setck.Removecookie(Utils.CStrDef(Session["User_ID"]));
                Session.Abandon();
                Response.Redirect("/");

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion
        #region loaddata
        protected void Load_Menu1()
        {
            try
            {
                //Rpmenutop.DataSource = per.Loadmenu(1, 20, 1).Take(1);
                //Rpmenutop.DataBind();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }

        }
        protected IQueryable Load_Menu2(object cat_parent_id)
        {
            try
            {
                return per.Menu2(cat_parent_id);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }

        }
        private void Load_prolike()
        {
            //Rppro_like.DataSource = index.Loadpro_cookie(1, ck.Listcookie_like());
            //Rppro_like.DataBind();
        }

        public int countprolike()
        {
            return index.Loadpro_cookie(1, ck.Listcookie_like()).Count;
        }
        public int totalCart()
        {
            if (Session["news_guid"] == null)
                return 0;
            Guid _guid = (Guid)Session["news_guid"];
            return cart.Total_quantity(_guid);
        }
        #endregion
        #region function
        public string Getprice(object News_Price1)
        {
            return fun.Getprice(News_Price1);
        }
        public decimal Getprice_addtocart(object News_Price1)
        {
            return fun.Getprice_addtocart(News_Price1);
        }
        public string GetPrice1(object News_Price1, object News_Price2)
        {
            try
            {
                return fun.Getprice1(News_Price1, News_Price2);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string GetPrice2(object News_Price1, object News_Price2)
        {
            try
            {
                return fun.Getprice2(News_Price1, News_Price2);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string Getgiam(object News_Price1, object News_Price2)
        {
            return fun.Getgiamgia(News_Price1, News_Price2);
        }
        public string GetLinkCat(object Cat_Url, object Cat_Seo_Url)
        {
            try
            {

                return fun.Getlink_Cat(Cat_Url, Cat_Seo_Url);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GetLink(object News_Url, object News_Seo_Url)
        {
            try
            {
                return fun.Getlink_News(News_Url, News_Seo_Url);
            }
            catch (Exception ex)
            {
                vpro.functions.clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string GetImageT(object News_Id, object News_Image1)
        {

            try
            {
                return fun.GetImageT_News(News_Id, News_Image1);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        #endregion
        private void loadYear()
        {
            for (int i = 1900; i <= DateTime.Now.Year; i++)
            {
                ListItem l = new ListItem();
                l.Value = i.ToString();
                l.Text = i.ToString();
                Dryear.Items.Add(l);
            }
            ListItem s = new ListItem("Năm", "0");
            s.Selected = true;
            Dryear.Items.Insert(0, s);
        }
        private void loadMonth()
        {
            for (int i = 1; i <= 12; i++)
            {
                ListItem l = new ListItem();
                l.Value = i.ToString();
                l.Text = i.ToString();
                Drmoth.Items.Add(l);
            }
            ListItem s = new ListItem("Tháng", "0");
            s.Selected = true;
            Drmoth.Items.Insert(0, s);
        }
        private void loadDay()
        {
            int _month = Utils.CIntDef(Drmoth.SelectedValue);
            int[] leday = new int[] { 1, 3, 5, 7, 8, 10, 12 };
            int limitday = 30;
            if (leday.Contains(_month))
                limitday = 31;
            else if (_month == 2)
                limitday = 28;
            for (int i = 1; i <= 31; i++)
            {
                ListItem l = new ListItem();
                l.Value = i.ToString();
                l.Text = i.ToString();
                Drday.Items.Add(l);
            }
            ListItem s = new ListItem("Ngày", "0");
            s.Selected = true;
            Drday.Items.Insert(0, s);
        }
        private string getImgLogo()
        {
            var list = per.Load_logo_or_sologan("1", 1);
            if (list.Count > 0)
            {
                if (!String.IsNullOrEmpty(list[0].BANNER_FILE))
                    return PathFiles.GetPathBanner(list[0].BANNER_ID) + "/" + list[0].BANNER_FILE;
            }
            return string.Empty;
        }
        private void addEmail(string email, int _active)
        {
            rg.Add_email(email, _active);
        }
        private string htmlEmailRegister(string _username, string _email, string _code)
        {

            StringBuilder _res = new StringBuilder();
            string _imglogo = getImgLogo();
            _res.Append("<body style='padding: 0;margin: 0;font: normal 13px/1.5 Arial, Helvetica, sans-serif;color: #333333'>");
            _res.Append("<div style='background: #f4f3f3; width: 100%'>");
            _res.Append("<div class='wrap head' style='background: #f4f3f3; width: 100%;padding: 10px 0'> <a href=''><img src='http://thangnhom.org" + _imglogo + "' alt='' width='150' /></a> <span class='hotline' style='float: right;color: #FF0000;font-size: 18px;background: url(http://thangnhom.org/vi-vn/Images/phone_ico.png) 0 center no-repeat;padding-left: 28px;margin-top: 10px;'><b>0944 74 68 79 </b></span> </div>");
            _res.Append("</div>");
            _res.Append("<div class='wrap' style='width: 600px;margin: 0 auto'>");
            _res.Append("<p><strong>Chào " + _username + ",</strong></p>");
            _res.Append("<p>Cảm ơn Bạn đã đăng ký <font color='red'>thành viên</font> trang <a href='http://www.thangnhom.org'>www.thangnhom.org</a></p>");
            _res.Append("<p>Bạn vui lòng xác nhận thành viên của thangnhom.org bằng cách click vào đường");
            _res.Append("link này: <a href='http://www.thangnhom.org/xac-thuc-email.html?code=" + _code + "'>http://www.thangnhom.org/xac-thuc-email.html?code=" + _code + "</a></p>");
            _res.Append("<p>thangnhom.org dành tặng bạn  trải nghiệm mua sắm ĐẦU TIÊN với ưu đãi CỰC HOT. Kiểm tra email của Bạn ngay  nhé! </p>");
            _res.Append("<p>" + _username + " nhất định sẽ  có những trải nghiệm mua sắm online đầy thú vị trên <a href='http://www.thangnhom.org'>www.thangnhom.org</a>. </p>");
            _res.Append("</div>");
            _res.Append("<div style='background: #414141; text-align: center; padding: 5px 0; color: #FFFFFF'>");
            _res.Append("<div class='wrap'>");
            _res.Append("<p><strong>Chúng tôi luôn sẵn sàng hỗ trợ bạn qua email <a href='mailto:info@thangnhom.org' style='color: #FFFFFF'>info@thangnhom.org</a> và hotline: 0944 74 68 79</strong></p>");
            _res.Append("<p style='font-size:11px'><strong>CÔNG TY TNHH MTV THƯƠNG MẠI HÀ NHƯ</strong><br>");
            _res.Append("<strong>Trụ sở</strong>: 185/21D Ni Sư Huỳnh Liên, P. 10, Q. Tân Bình, TP. Hồ Chí Minh</p>");
            _res.Append("<p style='font-size:11px'>Bạn nhận được thư này là do địa chỉ email đã được đăng ký thành viên thangnhom.org.</p>");
            _res.Append("</div>");
            _res.Append("</div>");
            _res.Append("</body>");
            return _res.ToString();
        }
        protected void Lbregis_Click(object sender, EventArgs e)
        {
            string _sbody = string.Empty;
            string _email = txtemailreg.Value;
            string _fullname = txtName.Value;
            string _pass = fm.MaHoaMatKhau(txtPassword1.Value);
            string _sCodeActive = fm.TaoChuoiTuDong(15);
            string _sphone = ""; //txtphone.Value;
            string _add = "";// txtadd.Value;
            int _sex = Utils.CIntDef(Rdsex.SelectedValue);
            string _idcity = null;// Drcity.SelectedValue;
            string _iddistrict = null;// Drdistric.SelectedValue;
            string _date = Dryear.SelectedValue + "/" + Drmoth.SelectedValue + "/" + Drday.SelectedIndex;
            DateTime _birthday = Utils.CDateDef(_date, DateTime.Now);
            //if (this.txtcode.Value == this.Session["CaptchaImageText"].ToString())
            //{
                if (acc.Register(_fullname, _email, _pass, _sCodeActive, _sphone, _add, _sex, _idcity, _iddistrict, _birthday))
                {
                    int _active = 0;
                    if (Checkemail.Checked)
                        _active = 1;
                    addEmail(_email, _active);
                    _sbody = htmlEmailRegister(_fullname, _email, _sCodeActive);
                    send.SendMail_Active_Account(txtemailreg.Value, "", "", _sbody);

                    //    Response.Redirect("~/vi-vn/deal/thong-bao.html");
                    string strScript = "<script>";
                    strScript += "alert(' Đăng ký thành công!');";
                    strScript += "window.location='/';";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
                }
                else
                {

                    Labelerrors.Text = "Email này đã có người sử dụng!";
                }
            //}
            //else
            //{

            //    Labelerrors.Text = "Mã xác nhận không đúng!";
            //}
        }
        protected void LbcheckOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("/kiem-tra-don-hang.html?code=" + orderCode.Value + "&email=" + email.Value);
        }
        protected void load_logo()
        {
            var _logoSlogan = per.Load_logo_or_sologan("1", 1);
            if (_logoSlogan.ToList().Count > 0)
            {
                Rplogo.DataSource = _logoSlogan;
                Rplogo.DataBind();
            }
        }
        public string Getbanner(object Banner_type, object banner_field, object Banner_ID, object Banner_Image)
        {
            string title = list_pro.Loadtitle(_cat_seo_url);
            if (title.Length == 0)
                title = pro_detail.Loadtitle(_cat_seo_url);
            var _configs = cf.Config_meta();

            if (_configs != null && _configs.ToList().Count > 0)
            {
                if (title.Length == 0)
                    title = _configs.ToList()[0].CONFIG_TITLE;
            }

            string s = "";
            if (Utils.CIntDef(Session["home"]) == 0)
                s += "<h1 style='position: fixed;left: 999999px;'>" + title;
            if (Utils.CIntDef(Session["home"]) == 0)
                s += "</h1>";
            s += fun.Getbanner(Banner_type, banner_field, Banner_ID, Banner_Image, title);
            
            return s;
        }

        public void Load_Cart(Guid _guid)
        {
            //if (string.IsNullOrEmpty(Utils.CStrDef(Session["news_guid"])))
            //{
            //    string strScript = "<script>";
            //    strScript += "alert('Thông báo: Giỏ hàng của bạn trống! Quay trở lại mua hàng... ');";
            //    strScript += "window.location='/';";
            //    strScript += "</script>";
            //    Page.RegisterClientScriptBlock("strScript", strScript);
            //}
            //else
            //{

                var _basket = cart.Load_cart(_guid);

                if (_basket.ToList().Count > 0)
                {

                    Rpgiohang.DataSource = _basket;
                    Rpgiohang.DataBind();
                    for (int i = 0; i < Rpgiohang.Items.Count; i++)
                    {
                        DropDownList dr = Rpgiohang.Items[i].FindControl("Drquan") as DropDownList;
                        dr.SelectedValue = _basket[i].Basket_quantity.ToString();
                    }
                }
                //else
                //{
                //    string strScript = "<script>";
                //    strScript += "alert('Thông báo: Giỏ hàng của bạn trống! Quay trở lại mua hàng... ');";
                //    strScript += "window.location='/';";
                //    strScript += "</script>";
                //    Page.RegisterClientScriptBlock("strScript", strScript);
                //}
            //}
        }

        public string getNamethuognhieu(object id)
        {
            int _id = Utils.CIntDef(id);
            return per.getLinkHangsx(_id);
        }
        //public string GetLink(object News_Url, object News_Seo_Url)
        //{
        //    try
        //    {
        //        return fun.Getlink_News(News_Url, News_Seo_Url);
        //    }
        //    catch (Exception ex)
        //    {
        //        vpro.functions.clsVproErrorHandler.HandlerError(ex);
        //        return null;
        //    }
        //}
        //public string GetPrice2(object News_Price1, object News_Price2)
        //{
        //    try
        //    {
        //        return fun.Getprice2(News_Price1, News_Price2);
        //    }
        //    catch (Exception ex)
        //    {
        //        clsVproErrorHandler.HandlerError(ex);
        //        return null;
        //    }
        //}
        //public string Getgiam(object News_Price1, object News_Price2)
        //{
        //    return fun.GetgiamgiaCart(News_Price1, News_Price2);
        //}
        //public string GetImageT(object News_Id, object News_Image1)
        //{

        //    try
        //    {
        //        return fun.GetImageT_News(News_Id, News_Image1);
        //    }
        //    catch (Exception ex)
        //    {
        //        clsVproErrorHandler.HandlerError(ex);
        //        return null;
        //    }
        //}

        public string GetProductName(object Title, int intLength)
        {
            if (Utils.CStrDef(Title).Length > intLength)
                return Utils.CStrDef(Title).Substring(0, intLength) + "...";
            return Utils.CStrDef(Title);
        }
        public string Getnamecolor_size(object cat_id)
        {
            return cart.Getnamecolor_size(cat_id);
        }

        protected void Rpgiohang_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                Guid _guid = (Guid)Session["news_guid"];

                int _sID = int.Parse(e.CommandArgument.ToString());
                cart.Delete_cart(_guid, _sID);
                Response.Redirect("/gio-hang.html");
            }
        }

        protected void Lbcapnhap_Click(object sender, EventArgs e)
        {
            Guid _guid = (Guid)Session["news_guid"];
            for (int i = 0; i < Rpgiohang.Items.Count; i++)
            {
                TextBox txt = Rpgiohang.Items[i].FindControl("Txtquantity") as TextBox;
                HtmlInputHidden s = Rpgiohang.Items[i].FindControl("newsid") as HtmlInputHidden;
                int _newid = int.Parse(s.Value);
                if (int.Parse(txt.Text) <= 0 || int.Parse(txt.Text) > 50)
                {
                    string strScript = "<script>";
                    strScript += "alert('Số lượng sản phẩm phải lớn hơn 0 và nhỏ hơn 100 ');";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
                }
                else
                {
                    cart.Update_cart(_guid, _newid, Utils.CIntDef(txt.Text));
                }
            }
            Load_Cart(_guid);
            //Lbtotal.Text = fm.FormatMoney(cart.Total_Amount(_guid));
        }
        protected void drSoLuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid _guid = (Guid)Session["news_guid"];

            for (int i = 0; i < Rpgiohang.Items.Count; i++)
            {
                //TextBox txt = Rpgiohang.Rows[i].FindControl("txtSoLuong") as TextBox;
                DropDownList dr = Rpgiohang.Items[i].FindControl("Drquan") as DropDownList;
                HtmlInputHidden s = Rpgiohang.Items[i].FindControl("newsid") as HtmlInputHidden;
                int _sID = Utils.CIntDef(s.Value);
                if (_sID != 0)
                    cart.Update_cart(_guid, _sID, Utils.CIntDef(dr.SelectedValue));

            }
            Load_Cart(_guid);
            //Lbtotal.Text = fm.FormatMoney(cart.Total_Amount(_guid));
        }
        public int getorder()
        {
            return _count++;
        }
        public string setStyle(object price2)
        {
            decimal _price = Utils.CDecDef(price2);
            if (_price == 0)
                return "style='margin-top:25px'";
            return "";
        }
    }
}