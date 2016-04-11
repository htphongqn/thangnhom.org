﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using MVC_Kutun.Components;
using System.IO;
using Controller;
using System.Data;

namespace MVC_Kutun.UIs
{
    public partial class product_details : System.Web.UI.UserControl
    {
        #region Declare
        Home index = new Home();
        Product_Details pro_detail = new Product_Details();
        Addto_cart cart = new Addto_cart();
        Function fun = new Function();
        Attfile att = new Attfile();
        Comment cm = new Comment();
        List_product list_pro = new List_product();
        SendMail sm = new Components.SendMail();
        Config cf = new Config();
        Getcookie ck = new Getcookie();
        Checkcookie checkck = new Checkcookie();
        setCookieLike setck = new setCookieLike();
        setCookiePartner setckpartner = new setCookiePartner();
        public clsFormat fm = new clsFormat();
        string _sNewsSeoUrl = string.Empty,_code=string.Empty;
        int count1 = 1;
        int count2 = 1;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            _sNewsSeoUrl = Utils.CStrDef(Request.QueryString["purl"]);
            _code = Utils.CStrDef(Request.QueryString["code"]);

            Hdurl.Value = _sNewsSeoUrl;
            Hyperlike.Attributes.Add("newsurl", _sNewsSeoUrl);
            
            if (!IsPostBack)
            {
                addPartner();
                loadComment();
                Addcookie();
                loadRaing();
                Addsee();
                //Show_File_HTML_sup("contact-sup.htm");
                Load_Similar_Product(_sNewsSeoUrl);
                Load_random_quantam(_sNewsSeoUrl);
                Loadimgalbum(0,1,ref Rpimg_big);
                Loadimgalbum(0, 20, ref Rpimg_small);
                loadArea();
                Loaddis(-1);
                Loaddetails();
                loadKhinhanxet();
            }
        }
        private void loadKhinhanxet()
        {
            lbKhiNhanxet.Text = index.Getdesc_Showfilehtml_index(4);
        }
        private void loadComment()
        {
            var list = cm.Load_comment(_sNewsSeoUrl);
            Rpcomment.DataSource = list;
            Rpcomment.DataBind();
            if (list.Count > 0)
                lbNhanxet.Visible = true;
            else
                lbNhanxet.Visible = false;
        }
        public IQueryable Load_Reply(object commentID)
        {
            var list = cm.Load_Reply(commentID);
            return list;
        }
        #region addPartner
        private void addPartner()
        {
            string _codeexits = checkck.getCookiePartner();
            if (_code != _codeexits&&!String.IsNullOrEmpty(_code))
                setckpartner.Addcookie(_code);
        }
        
        #endregion
        #region getRating
        private void loadRaing()
        {
            //List<string> l = checkck.Listcookie_Rating();
            //int[] b = new int[l.Count];
            //foreach (string item in l)
            //{
            //    string[] a = item.Split(',');
            //    if (a.Length > 1)
            //    {
            //        if (a[0].Contains(_sNewsSeoUrl))
            //        {
            //            Hdscore.Value = a[1];
            //            return;
            //        }
            //    }

            //}
            var list = cm.Load_comment(_sNewsSeoUrl);
            int count = list.Count;
            double avg = 0;
            if (count > 0)
            {
                int sum = list.Sum(n => Utils.CIntDef(n.COMMENT_FIELD2));
                avg = sum / count;
                Hdscore.Value = avg.ToString();
            }
            lbAggregateRating.Text = "";
            lbAggregateRating.Text += "<div itemprop='aggregateRating' itemscope itemtype='http://schema.org/AggregateRating'>";
            lbAggregateRating.Text += "   <meta itemprop='ratingValue' content = '" + avg + "'>";
            lbAggregateRating.Text += "   <meta itemprop='ratingCount' content = '" + count + "'>";
            lbAggregateRating.Text += "</div>";


            lbcount_avg.Text = avgRaing() + "/5";
            lbcount.Text = count.ToString();

            var list5 = cm.Load_comment(_sNewsSeoUrl, "5");
            int count5 = list5.Count;
            double avg5 = 0;
            if (count5 > 0)
            {
                avg5 = (count5 * 100) / count;
            }
            lb5Sao.Text = "<div class='item'> <span class='rating-num'>5 sao</span>";
            lb5Sao.Text += "<div class='progress'>";
            lb5Sao.Text += "<div class='progress-bar progress-bar-success' style='width: " + avg5.ToString("#,0") + "%;'> <span class='sr-only'>" + avg5.ToString("0,0") + "% Complete</span> </div>";
            lb5Sao.Text += "</div>";
            lb5Sao.Text += "<span class='rating-num-total'>" + count5.ToString() + "</span> </div>";


            var list4 = cm.Load_comment(_sNewsSeoUrl, "4");
            int count4 = list4.Count;
            double avg4 = 0;
            if (count4 > 0)
            {
                avg4 = (count4 * 100) / count;
            }
            lb4Sao.Text = "<div class='item'> <span class='rating-num'>4 sao</span>";
            lb4Sao.Text += "<div class='progress'>";
            lb4Sao.Text += "<div class='progress-bar progress-bar-success' style='width: " + avg4.ToString("#,0") + "%;'> <span class='sr-only'>" + avg4.ToString("0,0") + "% Complete</span> </div>";
            lb4Sao.Text += "</div>";
            lb4Sao.Text += "<span class='rating-num-total'>" + count4.ToString() + "</span> </div>";

            var list3 = cm.Load_comment(_sNewsSeoUrl, "3");
            int count3 = list3.Count;
            double avg3 = 0;
            if (count3 > 0)
            {
                avg3 = (count3 * 100) / count;
            }
            lb3Sao.Text = "<div class='item'> <span class='rating-num'>3 sao</span>";
            lb3Sao.Text += "<div class='progress'>";
            lb3Sao.Text += "<div class='progress-bar progress-bar-success' style='width: " + avg3.ToString("#,0") + "%;'> <span class='sr-only'>" + avg3.ToString("0,0") + "% Complete</span> </div>";
            lb3Sao.Text += "</div>";
            lb3Sao.Text += "<span class='rating-num-total'>" + count3.ToString() + "</span> </div>";

            var list2 = cm.Load_comment(_sNewsSeoUrl, "2");
            int count2 = list2.Count;
            double avg2 = 0;
            if (count2 > 0)
            {
                avg2 = (count2 * 100) / count;
            }
            lb2Sao.Text = "<div class='item'> <span class='rating-num'>2 sao</span>";
            lb2Sao.Text += "<div class='progress'>";
            lb2Sao.Text += "<div class='progress-bar progress-bar-success' style='width: " + avg2.ToString("#,0") + "%;'> <span class='sr-only'>" + avg2.ToString("0,0") + "% Complete</span> </div>";
            lb2Sao.Text += "</div>";
            lb2Sao.Text += "<span class='rating-num-total'>" + count2.ToString() + "</span> </div>";

            var list1 = cm.Load_comment(_sNewsSeoUrl, "1");
            int count1 = list1.Count;
            double avg1 = 0;
            if (count1 > 0)
            {
                avg1 = (count1 * 100) / count;
            }
            lb1Sao.Text = "<div class='item'> <span class='rating-num'>1 sao</span>";
            lb1Sao.Text += "<div class='progress'>";
            lb1Sao.Text += "<div class='progress-bar progress-bar-success' style='width: " + avg1.ToString("#,0") + "%;'> <span class='sr-only'>" + avg1.ToString("0,0") + "% Complete</span> </div>";
            lb1Sao.Text += "</div>";
            lb1Sao.Text += "<span class='rating-num-total'>" + count1.ToString() + "</span> </div>";
        }
        public int countRaing()
        {
            var list = cm.Load_comment(_sNewsSeoUrl);
            int count = list.Count;
            return count;
        }
        public string avgRaing()
        {
            var list = cm.Load_comment(_sNewsSeoUrl);
            int count = list.Count;
            double avg = 0;
            if (count > 0)
            {
                int sum = list.Sum(n => Utils.CIntDef(n.COMMENT_FIELD2));
                avg = sum / count;                
            }
            return avg.ToString("#,0.#");
        }
        #endregion
        #region area
        private void loadArea()
        {
            Drarea2.DataValueField = "PROP_ID";
            Drarea2.DataTextField = "PROP_NAME";
            Drarea2.DataSource = pro_detail.loadArea();
            Drarea2.DataBind();
            ListItem l = new ListItem("---Chọn---", "0");
            l.Selected = true;
            Drarea2.Items.Insert(0, l);
        }
        private void Loaddis(int id)
        {
            //var list = pro_detail.loadAreaDis(id);
            //if (list.Count > 0)
            //{
            //    Drdistric.DataValueField = "PROP_ID";
            //    Drdistric.DataTextField = "PROP_NAME";
            //    Drdistric.DataSource = list;
            //    Drdistric.DataBind();
            //    ListItem l = new ListItem("---Chọn---", "0");
            //    l.Selected = true;
            //    Drdistric.Items.Insert(0, l);
            //}
            //else
            //{
            //    DataTable dt = new DataTable("Newtable");

            //    dt.Columns.Add(new DataColumn("PROP_ID"));
            //    dt.Columns.Add(new DataColumn("PROP_NAME"));

            //    DataRow row = dt.NewRow();
            //    row["PROP_ID"] = 0;
            //    row["PROP_NAME"] = "---Chọn---";
            //    dt.Rows.Add(row);

            //    Drdistric.DataTextField = "PROP_NAME";
            //    Drdistric.DataValueField = "PROP_ID";
            //    Drdistric.DataSource = dt;
            //    Drdistric.DataBind();

            //}
        }
        #endregion
        #region Addcookie
        private void Addcookie()
        {
            if (!checkck.Listcookie_see().Contains(_sNewsSeoUrl))
                ck.Addcookie(_sNewsSeoUrl);
        }
        #endregion
        #region Addsee
        private void Addsee()
        {
            pro_detail.Addsee(_sNewsSeoUrl);
        }
        #endregion
        #region Loaddata

        public void Load_img_news(int take, int skip, ref Repeater rp)
        {
            rp.DataSource = pro_detail.Load_Product_Detail(_sNewsSeoUrl);
            rp.DataBind();
        }
        public void Loadimgalbum(int skip,int limit,ref Repeater rp)
        {
            try
            {
                int _newsID = Utils.CIntDef(Session["news_id"]);
                var list = pro_detail.Load_albumimg(_newsID,skip,limit).ToList();
                if (list.Count > 0)
                {
                    rp.DataSource = list;
                    rp.DataBind();
                    
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Loaddetails()
        {
            try
            {
                Show_File_HTML();
                var list = pro_detail.Load_Product_Detail(_sNewsSeoUrl);
                if (list.Count > 0)
                {
                    if (Utils.CDecDef(list[0].NEWS_PRICE2) != 0)
                        div_promos.Visible = true;
                    else
                        div_promos.Visible = false;
                    Lbtitle_details.Text = Lbtitle_details2.Text = list[0].NEWS_TITLE;
                    Lbdesc_details.Text = setBr(list[0].NEWS_DESC);
                    Lbcode.Text = list[0].NEWS_CODE;
                    //Lbdesc_bot.Text = setBr(list[0].NEWS_FIELD4);
                    Litdescbot.Text = setBrLi(list[0].NEWS_FIELD4);
                    Lbcount_see.Text = list[0].NEWS_COUNT.ToString();
                    //Lbcount_buy.Text = list[0].UNIT_ID3.ToString();
                    //Hyperaddtocart.NavigateUrl = "../vi-vn/Addtocart.aspx?id=" + list[0].NEWS_ID;
                    //HyperaddTocart2.NavigateUrl = "../vi-vn/Addtocart.aspx?id=" + list[0].NEWS_ID;
                    //Lbbaohanh.Text = list[0].NEWS_FIELD2;
                    Hyperthuonghieu.NavigateUrl = pro_detail.getSeourl(Utils.CIntDef(list[0].UNIT_ID2));
                    //Litthuonghieu_info.Text = list_pro.getHmtlSeo(list[0].UNIT_ID2);
                    //Lbxuatxu.Text = pro_detail.getnamecate(list[0].UNIT_ID1);
                    Hyperthuonghieu.Text = pro_detail.getnamecate(list[0].UNIT_ID2);

                    //if (list[0].SHOP_ID > 0)
                    //{
                    //    hplShop.Text = pro_detail.getnameshop(list[0].SHOP_ID);
                    //    hplShop.NavigateUrl = pro_detail.getShopSeourl(Utils.CIntDef(list[0].SHOP_ID));
                    //}
                    //else
                    //{
                    //    hplShop.Text = "thangnhom.org";
                    //}

                    if (Utils.CIntDef(list[0].NEWS_FIELD3) == 0)
                    {
                        //Hyperaddtocart.Text = "<span>Hết hàng</span>";
                        Hyperaddtocart.Enabled = false;
                        //Hyperaddtocart.CssClass = "addtocart_btn fl";

                    }
                    else
                    {
                        //Hyperaddtocart.Text = "<span>Mua ngay</span>";
                        Hyperaddtocart.Enabled = true;

                    }
                    lbtinhtrang.Text = Utils.CIntDef(list[0].NEWS_FIELD3) == 1 ? "Còn hàng" : "Hết hàng";
                    Lbprice_goc.Text = GetPrice2(list[0].NEWS_PRICE1, list[0].NEWS_PRICE2);
                    Lbprice_promos.Text = Lbprice_promos2.Text = GetPrice1(list[0].NEWS_PRICE1, list[0].NEWS_PRICE2);
                    lbVAT.Text = Utils.CIntDef(list[0].UNIT_ID1) == 1 ? "(Đã có VAT)" : "(Chưa có VAT)";
                    //lbPricetietkiem.Text = getTietkiem(list[0].NEWS_PRICE1, list[0].NEWS_PRICE2);
                    Lbpricetietkiem.Text = getTietkiem(list[0].NEWS_PRICE1, list[0].NEWS_PRICE2);
                    Lbpricepointtietkiem.Text = " (" + getPointietkiem(list[0].NEWS_PRICE1, list[0].NEWS_PRICE2) + ")";
                    Lbface.Text = "<div class='fb-like' data-href='" + Request.RawUrl + "'data-layout='button_count' data-action='like' data-show-faces='true' data-share='true' style='float: left'> </div>";
                    if (checkck.Listcookie_like().Contains(_sNewsSeoUrl))
                    {
                        Hyperlike.CssClass = "active";
                        //Hyperlike.Text = "Đã thêm vào yêu thích";
                        Hyperlike.Text = "<button class='add-to-wishlist  is-css' type='button'><span class='icon'> <i class='fa fa-heart'></i> </span> <span class='text'>Đã thêm vào yêu thích</span></button>";
                        Hyperlike.Enabled = false;
                    }
                    //Lbtitle.Text = pro_detail.Load_Product_Detail(_sNewsSeoUrl)[0].NEWS_TITLE;
                    if (list[0].NEWS_IMAGE4 != "")
                    {
                        quatangkm.Visible = true;
                        lbImgPromo.Text = "<a href='" + list[0].NEWS_FIELD5 + "' title='" + list[0].NEWS_IMAGE4 + "'><img src='" + fun.GetImageT_News(list[0].NEWS_ID, list[0].NEWS_IMAGE2) + "' alt='" + list[0].NEWS_IMAGE4 + "' style='max-width:100%; max-height: 60px ' /></a>";

                        lbNamepromo.Text = "<a href='" + list[0].NEWS_FIELD5 + "' title='" + list[0].NEWS_IMAGE4 + "'>" + list[0].NEWS_IMAGE4 + "</a>";
                        lbpricepromo.Text = String.Format("{0:0,0 ₫}", Utils.CDblDef(list[0].NEWS_IMAGE1));
                    }
                }

            }
            catch (Exception ex)
            {

                clsVproErrorHandler.HandlerError(ex);
            }
        }
        //private void Show_File_HTML_sup(string HtmlFile)
        //{
        //    string pathFile;
        //    string strHTMLContent;
        //    pathFile = Server.MapPath("../Data/contact/" + HtmlFile);

        //    if ((File.Exists(pathFile)))
        //    {
        //        StreamReader objNewsReader;
        //        objNewsReader = new StreamReader(pathFile);
        //        strHTMLContent = objNewsReader.ReadToEnd();
        //        objNewsReader.Close();
        //        Lithotro.Text = strHTMLContent;
        //    }


        //}
        private void Show_File_HTML()
        {
            try
            {
                int _newsID = Utils.CIntDef(Session["news_id"]);
                liHtml_info.Text = pro_detail.Show_File_HTML(_newsID,1);
                liHtml_thongso.Text = pro_detail.Show_File_HTML(_newsID, 2);
                //Literal_bailienquan.Text = pro_detail.Show_File_HTML(_newsID, 3);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        public void Load_Similar_Product(string News_Seo_Url)
        {
            try
            {
                rp_sanphamcungloai.DataSource = pro_detail.Load_Similar_Product(_sNewsSeoUrl, 10);
                rp_sanphamcungloai.DataBind();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void Load_random_quantam(string News_Seo_Url)
        {
            Rpproquantam.DataSource = pro_detail.loadPro_Random(News_Seo_Url, 5);
            Rpproquantam.DataBind();
        }

        #endregion
        #region function
        public string getBuy(object news_id, object sta)
        {
            return fun.getBuy(news_id, sta);
        }
        public string getNewsurl()
        { 
            return _sNewsSeoUrl;
        }
        public string getUrlface()
        {
            return System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + Request.RawUrl;
        }
        public string getCount1()
        {
            return "pimg" + count1++;
        }
        public string getCount2()
        {
            return "pimg" + count2++;
        }
        private string setBr(object desc)
        { 
            string _des=Utils.CStrDef(desc);
            if (!String.IsNullOrEmpty(_des))
            {
                if (_des.Contains("\r\n")) _des = _des.Replace("\r\n", "<br/>");
            }
            return _des;
        }
        private string setBrLi(object desc)
        {
            string _des = Utils.CStrDef(desc);
            string _res = string.Empty;
            if (!String.IsNullOrEmpty(_des))
            {
                if (_des.Contains("\r\n")) _des = _des.Replace("\r\n", "-");
                string[] a = _des.Split('-');
                for(int i=0;i<a.Length;i++)
                {
                    _res += "<li>" + a[i] + "</li>";
                }
            }
            return _res;
            

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
        public string getTietkiem(object News_Price1, object News_Price2)
        {
            return fun.getTietkiem(News_Price1, News_Price2);
        }
        public string getPointietkiem(object News_Price1, object News_Price2)
        {
            return fun.getPointietkiem(News_Price1, News_Price2);
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
        public string getdate(object date)
        {
            return fun.getDate(date);
        }

        public string getnamecate(object newsid)
        {
            try
            {
                return pro_detail.getnamecate(newsid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        protected void Lbaddtocart_Click(object sender, EventArgs e)
        {
            try
            {
                var list = pro_detail.Load_Product_Detail(_sNewsSeoUrl);
                if (list.Count > 0)
                {
                    int id = list[0].NEWS_ID;
                    int _quantity = Utils.CIntDef(ddlQty.Text);
                    Guid _guid = (Guid)Session["news_guid"];
                    if (cart.Add_To_Cart(id, _guid, _quantity))
                    {
                        string url = "/gio-hang.html";// Request.UrlReferrer.AbsolutePath;
                        string strScript = "<script>";
                        //strScript += "alert('Đã thêm sản phẩm vào giỏ hàng!');";
                        strScript += "window.location='" + (!String.IsNullOrEmpty(url) ? url : "/") + "';";
                        strScript += "</script>";
                        Page.RegisterClientScriptBlock("strScript", strScript);
                    }
                    else
                    {
                        string url = Request.UrlReferrer.AbsolutePath;
                        string strScript = "<script>";
                        strScript += "alert('Sản phẩm này đã hết hàng.Bạn hãy liên hệ chúng tôi để mua sản phẩm này!');";
                        strScript += "window.location='" + (!String.IsNullOrEmpty(url) ? url : "/") + "';";
                        strScript += "</script>";
                        Page.RegisterClientScriptBlock("strScript", strScript);
                    }
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                
            }
        }

        protected void Lbaddlike_Click(object sender, EventArgs e)
        {
            string url = Request.RawUrl;
            if (!String.IsNullOrEmpty(url))
            {
                string[] a = url.Split('?');
                if (!checkck.Listcookie_like().Contains(_sNewsSeoUrl))
                {
                    setck.Addcookie(_sNewsSeoUrl);
                    string strScript = "<script>";
                    strScript += "alert('Đã thêm vào yêu thích!');";
                    strScript += "window.location='"+a[0]+"';";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
                }
                else
                {
                    string strScript = "<script>";
                    strScript += "alert(' Bạn đã thích sản phẩm này!');";
                    strScript += "window.location='" + a[0] + "';";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
                }
            }
            
        }

        protected void Drarea2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Utils.CIntDef(Drarea2.SelectedValue);
            //Loaddis(id);
            string s = pro_detail.getAreaDesc(id);
            if (s != "")
                lbDesArea.Text = s;
        }

        protected void Drdistric_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int _iddist = Utils.CIntDef(Drdistric.SelectedValue);
            //if (_iddist != 0)
            //    Lbgiaohang.Text = pro_detail.getGiaohang(_iddist);
            //else
            //    Lbgiaohang.Visible = false;
        }
        protected void btnAddtocart_Click(object sender, EventArgs e)
        {
            try
            {
                var list = pro_detail.Load_Product_Detail(_sNewsSeoUrl);
                if (list.Count > 0)
                {
                    int id = list[0].NEWS_ID;
                    int _quantity = Utils.CIntDef(qty.Text);
                    Guid _guid = (Guid)Session["news_guid"];
                    if (cart.Add_To_Cart(id, _guid, _quantity))
                    {
                        string url = "/gio-hang.html";// Request.UrlReferrer.AbsolutePath;
                        string strScript = "<script>";
                        //strScript += "alert('Đã thêm sản phẩm vào giỏ hàng!');";
                        strScript += "window.location='" + (!String.IsNullOrEmpty(url) ? url : "/") + "';";
                        strScript += "</script>";
                        Page.RegisterClientScriptBlock("strScript", strScript);
                    }
                    else
                    {
                        string url = Request.UrlReferrer.AbsolutePath;
                        string strScript = "<script>";
                        strScript += "alert('Sản phẩm này đã hết hàng.Bạn hãy liên hệ chúng tôi để mua sản phẩm này!');";
                        strScript += "window.location='" + (!String.IsNullOrEmpty(url) ? url : "/") + "';";
                        strScript += "</script>";
                        Page.RegisterClientScriptBlock("strScript", strScript);
                    }
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);

            }
        }
        public string getRatingComment(object rate)
        {
            int _point = Utils.CIntDef(rate);
            switch (_point)
            {
                case 1: return "one_star";
                case 2: return "two_star";
                case 3: return "three_star";
                case 4: return "four_star";
                case 5: return "five_star";
            }
            return "";
        }
        private int getRate()
        {
            int _point = 0;
            if (Rdfiverate.Checked)
                _point = 5;
            if (Rdfourrate.Checked)
                _point = 4;
            if (Rdthreerate.Checked)
                _point = 3;
            if (Rdtworate.Checked)
                _point = 2;
            if (Rdonerate.Checked)
                _point = 1;
            return _point;
        }
        protected void btnSendrate_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Utils.CStrDef(Session["User_ID"])))
            //{
            //    string strScript = "<script>";
            //    strScript += "alert(' Bạn cần đăng nhập để nhận xét về sản phẩm!');";
            //    strScript += "</script>";
            //    Page.RegisterClientScriptBlock("strScript", strScript);
            //    return;
            //}
            if (Utils.CStrDef(txtcontent.Value).Length < 50)
            {
                string strScript = "<script>";
                strScript += "alert(' Nội dung phải lớn hơn 50 ký tự!');";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
                return;
            }
            int _newsID = Utils.CIntDef(Session["news_id"]);
            string _name = txtname.Value;
            string _email = txtemail.Value;
            string _title = txttitle.Value;
            string _content = txtcontent.Value;
            int _rating = getRate();
            //if (this.txtcapcha.Value != this.Session["CaptchaImageText"].ToString())
            //{
            //    lblresult.Text = "Mã bảo vệ không đúng.";
            //    lblresult.Focus();
            //    return;
            //}
            if (cm.AddQuestion(_name, _email, _title, _content, _newsID, _rating))
            {
                //string _mailBody = string.Empty;
                //string _sEmailCC = string.Empty;
                //_mailBody += "<br/><br/><strong>Name</strong>: " + _name;
                //_mailBody += "<br/><br/><strong>Email</strong>: " + _email;
                //_mailBody += "<br/><br/><strong>Title</strong>: " + _title;
                //_mailBody += "<br/><br/><strong>Content</strong>: " + _content + "<br/><br/>";
                //string _sMailBody = string.Empty;
                //_sMailBody += "Cám ơn: " + _name + " bạn đã đánh giá sản phẩm. Đây là email được gửi từ website " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                //_sEmailCC = cf.Getemail(2).Count > 0 ? cf.Getemail(2)[0].EMAIL_TO : "";
                //sm.SendEmailSMTP("Notice: Gửi đánh giá thành công", _email, _sEmailCC, "", _sMailBody, true, false);
                string strScript = "<script>";
                strScript += "alert('Cám ơn bạn đã đánh giá cho sản phẩm, Nội dung đánh giá sẽ hiển thị sau khi duyệt!');";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
            resetInput();
        }
        private void resetInput()
        {
            txtname.Value = txtemail.Value = txttitle.Value = txtcontent.Value = "";
        }

        protected void Rpcomment_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Reply":
                    Literal lbReplyError = e.Item.FindControl("lbReplyError") as Literal;
                    TextBox txtQuickReply = e.Item.FindControl("txtQuickReply") as TextBox;
                    if (txtQuickReply.Text == "")
                        lbReplyError.Text = "Nội dung trả lời không được rỗng";
                    else
                    {
                        int _newsID = Utils.CIntDef(Session["news_id"]);
                        int commentId = Utils.CIntDef(e.CommandArgument);
                        cm.Addcomment(txtQuickReply.Text, _newsID, commentId);
                        txtQuickReply.Text = "";
                        lbReplyError.Text = "";
                        Repeater Repeater1 = e.Item.FindControl("Repeater1") as Repeater;
                        Repeater1.DataSource = Load_Reply(e.CommandArgument);
                        Repeater1.DataBind();
                        //Response.Redirect(Request.RawUrl + "#list-comment");
                    }
                    break;
                case "Thank":
                    LinkButton btnThankreply = e.Item.FindControl("btnThankreply") as LinkButton;
                    Label lbThank = e.Item.FindControl("lbThank") as Label;
                    btnThankreply.Visible = false;
                    lbThank.Text = "Bạn đã cảm ơn nhận xét này";
                    lbThank.ForeColor = System.Drawing.Color.Green;
                    break;
                default:
                    break;
            }
        }

        protected void Rpcomment_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnQuickreply = e.Item.FindControl("btnQuickreply") as Button;
                Repeater Repeater1 = e.Item.FindControl("Repeater1") as Repeater;
                if (Repeater1 != null && btnQuickreply != null)
                {
                    Repeater1.DataSource = Load_Reply(btnQuickreply.CommandArgument);
                    Repeater1.DataBind();
                }
            }
        }
    }
}