<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="MVC_Kutun.UIs.header" %>
<%@ Register Src="header-search.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="forgetpass.ascx" TagName="forgetpass" TagPrefix="uc2" %>
<script language="javascript" type="text/javascript">
    function openWindow(filename, winname, width, height) {
        var features, top, left;
        left = (window.screen.width - width) / 2;
        top = (window.screen.height - height) / 2;
        features = "width=" + width + ",height=" + height + ",top=" + top + ",left=" + left;
        void (window.open(filename, winname, features));
    }

    //Su dung cho facebook
    var appID = "1694538324159186";
    var redirectURL = "http://thangnhom.org/Login/FaceBook.aspx";  //url se tra lai sau khi dang nhap thanh cong, luu y url nay phai trung khop khi cau hinh app tren facebook
    var stateValue = "0";  //ko can thiet
    var scope = "email,user_birthday"; //day la nhung thong tin mo rong, nhung thong tin basic van dc lay kem theo http://developers.facebook.com/docs/authentication/permissions/
    var responseType = "code"; //token or code, default=code
    var display = "popup";  //popup,page,touch 
    //var linkFaceBook = "https://www.facebook.com/v2.4/dialog/oauth/?client_id=" + appID + "&redirect_uri=http://localhost:13897/account/user.aspx&response_type=code&state=1&scope=email"
    var linkFaceBook = "https://www.facebook.com/v2.4/dialog/oauth/?client_id=" + appID + "&redirect_uri=http://thangnhom.org/Login/FaceBook2.aspx&response_type=code&state=1&scope=email"
    //var linkFaceBook = "https://www.facebook.com/v2.4/dialog/oauth?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&redirect_uri=" + redirectURL + "&response_type=code" + "&state=1" + "&scope=email" + "&display=" + display;
    </script>

<div class="wrap">
 <asp:Repeater ID="Rplogo" runat="server">
  <ItemTemplate> <%# Getbanner(Eval("BANNER_TYPE"),Eval("BANNER_FIELD1"), Eval("BANNER_ID"), Eval("BANNER_FILE"))%> </ItemTemplate>
 </asp:Repeater>
 <div id="header_r" class="fr">
  <div>
   <div class="search_form fl">
    <uc1:header ID="header1" runat="server" />
   </div>
   <div class="order-tracking-shortcut"> <a title="Kiểm tra trạng thái Đơn hàng của bạn" onclick="ktdh()" class="link_ktradh">Kiểm tra Đơn hàng</a>
    <div class="contentEGP dropdown orderTracking hidden" id="drop_ktradh">
     <div class="closebtn">x</div>
     <div class="dropdown_title"> Kiểm tra đơn hàng</div>
     <div class="input_form">
      <label for="order_number"> Vui lòng nhập mã đơn hàng:</label>
      <input type="text" id="orderCode" value="" name="orderNr" class="form__input__text" runat="server">
     </div>
     <div class="input_form">
      <label for="order_number"> Vui lòng nhập địa chỉ email:</label>
      <input type="text" id="email" value="" name="email" class="form__input__text" runat="server">
     </div>
     <p class="form_text"> Nếu bạn có mắc thắc nào khác, <a class="link_acount" target="_blank" href="/lien-he.html"> Nhấn vào đây</a> </p>
     <div class="input_form">
      <asp:LinkButton ID="LbcheckOrder" runat="server" class="button" OnClick="LbcheckOrder_Click">Kiểm tra</asp:LinkButton>
     </div>
    </div>
   </div>
   <div class="header-user">
    <div class="user-name js-header-user-name">
     <div class="user-name-link user-ajax-link">
      <ul class="txtuser">
       <li id="div_login" runat="server" class="user-name-short"> <span class="tt_linktop link_login">
       <a class="link-popup popup-login-link" href="#popup-login">Đăng nhập</a></span> /<a  class="link-popup popup-reg-link" href="#popup-reg"  > Đăng ký</a> 
        
        <!--popup-login-->
        <div class="popup-over" id="popup-login">
         <div class="inner-popup">
          <div class="cust-box-1" style=" width:350px;"> <i class="close-pp fa fa-remove"></i>
           <div class="inner-box-cust-1">
            <div class="panel-popup">
             <p class="tt-popup"><i class="fa  fa-lock"></i> Đăng nhập</p>
             <%--<p><small>Bạn chưa có tài khoản? <a href="#popup-reg">Đăng ký</a></small></p>--%>
            </div>
            <div class="content-popup">
             <div class="row-pp">
              <input type="text"  class="txtpp" id="txtemail" maxlength="50" placeholder="Nhập Email"/>
             </div>
             <div class="row-pp">
              <input type="password"  class="txtpp" id="txtpass" placeholder="Nhập mật khẩu"/>
             </div>
                <div id="loading-errors"></div>
                <div class="errors" style="color:red;"></div>
             <div class="row-pp"><small> Quên mật khẩu? Nhấn vào <a class="link_acount close-pp" onclick="showquyenmk();" href="javascript:void(0)">đây</a></small></div>
                
             <div class="row-pp-2"> <a class="btn-pp" href="javascript:void(0)" onclick="ajaxlogin();">Đăng nhập</a> </div>
             <div class="row-pp-2">
              <a onclick="openWindow(linkFaceBook,'Login',660,480)" href="javascript:;" class="btn-pp  btn-facebook"><i class="fa fa-facebook"></i>Đăng nhập bằng  Facebook</a>
             </div>
            </div>
           </div>
          </div>
         </div>
        </div>
        
        <!--popup-login--> 
        <!--popup-reg-->
        
        <div class="popup-over" id="popup-reg" >
         <div class="inner-popup">
          <div class="cust-box-1" style=" width:800px;"> <i class="close-pp fa fa-remove"></i>
           <div class="inner-box-cust-1">
            <div class="panel-popup">
             <p class="tt-popup"> <i class="fa  fa-user-plus"></i> Đăng ký tài khoản</p>
             <%--<p><small>Bạn đã có tài khoản? <a href="#popup-login">Đăng nhập</a></small></p>--%>
            </div>
            <div class="content-popup">
             <div class="col7">
              <div class="row-pp">
               <div class=" col3 lblpp">Email:</div>
               <div class="col9">
                   <input class="txtpp" type="text" name="txtEmail" id="txtemailreg" runat="server" />
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Vui lòng nhập địa chỉ email"
                                ControlToValidate="txtemailreg" Display="None" ForeColor="Red" ValidationGroup="G8">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemailreg"
                            ErrorMessage="Địa chỉ email nhập không đúng định dạng" SetFocusOnError="True" Display="None"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red"
                            ValidationGroup="G8" CssClass="errorsval"></asp:RegularExpressionValidator>
               </div>
              </div>
              <div class="row-pp">
               <div class=" col3 lblpp">Mật khẩu:</div>
               <div class="col9">
                   <input class="txtpp" type="password" name="txtPassword1" id="txtPassword1"
                            runat="server" />
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Vui lòng nhập mật khẩu"
                                ControlToValidate="txtPassword1" Display="None" ForeColor="Red" ValidationGroup="G8">*</asp:RequiredFieldValidator>
               </div>
              </div>
              <div class="row-pp">
               <div class=" col3 lblpp">Họ tên:</div>
               <div class="col9">
                   <input class="txtpp" type="text" name="txtName" id="txtName" runat="server" />
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập mật khẩu"
                                ControlToValidate="txtName" Display="None" ForeColor="Red" ValidationGroup="G8">*</asp:RequiredFieldValidator>
               </div>
              </div>
              <div class="row-pp">
                  <div class=" col3 lblpp">Giới tính:</div>
               <div class="col9">                   
                  <asp:RadioButtonList ID="Rdsex" runat="server" RepeatColumns="3" CellPadding="10" CellSpacing="10">
                            <asp:ListItem Value="0" Text="Nam" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Nữ"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Khác"></asp:ListItem>
                        </asp:RadioButtonList> 
                   </div>
               <%--<div class=" col3">
                <input type="radio" class="chk" />
                Nam
                <input type="radio"  class="chk" />
                Nữ 
                   </div>  --%>            
              </div>
              <div class="row-pp">
               <div class=" col3 lblpp">Ngày Sinh: <small class="cf clear clearfix" style="font-weight:100; font-weight: 100; font-size:0.8em"><i class="red">*</i> Không bắt buộc</small></div>
               <div class="col3">
                   <asp:DropDownList ID="Drday" runat="server" class="txtpp">
                        </asp:DropDownList>
               </div>
               <div class="col3">
                        <asp:DropDownList ID="Drmoth" runat="server" class="txtpp">
                        </asp:DropDownList>
               </div>
               <div class="col3">
                        <asp:DropDownList ID="Dryear" runat="server" class="txtpp">
                        </asp:DropDownList>
               </div>
              </div>
              <div class="row-pp">
               <div class=" col3"> &nbsp;</div>
               <div class="col9">
                <p style="margin-bottom:10px">
                <asp:CheckBox ID="Checkemail" runat="server" Checked="true" CssClass="chk" />
                 Nhận các thông tin và chương trình khuyến mãi của thangnhom.org qua email.</p>
                <div class="row-pp-2">
                    <asp:Label ID="Labelerrors" runat="server"
                        Text="" ForeColor="Red"></asp:Label>
                    <asp:Button ID="Lbregis" runat="server" CssClass="btn-pp" ValidationGroup="G8" OnClick="Lbregis_Click" Text="Đăng ký"></asp:Button>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="G8" />
                </div>
                <%--<div class="row-pp-2"> Tôi đồng ý với các <a>điều khoản sử dụng của thangnhom.org</a>.</div>--%>
               </div>
              </div>
             </div>
             <!--col7-->
             
             <div class="col5" >
              <p  class="row-pp-2">Đăng nhập vào thangnhom.org bằng tài khoản mạng xã hội</p>
              <div class="row-pp-2">
               <a onclick="openWindow(linkFaceBook,'Login',660,480)" href="javascript:;" class="btn-pp  btn-facebook"><i class="fa fa-facebook"></i>Đăng nhập bằng  Facebook</a>
              </div>
             </div>
            </div>
           </div>
          </div>
         </div>
        </div>
        <!--popup-reg--> 
       </li>
       <li><span id="div_logout" runat="server">
        <div class="filter_name"> <span class="tt_linktop"><b style="color: #fcd207">Chào,
         <asp:Label ID="lbthanhvien" runat="server" Text=""></asp:Label>
         </b><img src="../vi-vn/Images/white_arr_down.png" alt="" /></span>
         <div class="contentEGP popup_login" id="Div1"> <a class="dropdown_item" target="_parent" href="/quan-ly-tai-khoan.html">Quản lý tài
          khoản</a> <a class="dropdown_item" target="_parent" href="/lich-su-mua-hang.html">Đơn
          hàng của tôi</a> <a class="dropdown_item" target="_parent" href="/doi-mat-khau.html"> Đổi mật khẩu</a>
          <asp:LinkButton ID="lbtnDangXuat" runat="server" CausesValidation="False" OnClick="lbtnDangXuat_Click"
                                CssClass="dropdown_item">Đăng xuất</asp:LinkButton>
         </div>
        </div>
        </span></li>
       <li class="user-name-account"> Tài khoản &amp; Đơn hàng </li>
      </ul>
      <span class="caret"></span> </div>
    </div>
    <div class="cart_head  user-cart" data-container="body" data-placement="auto bottom"> <a class="link-popup user-cart-link popup-cart-link" href="#popup-cart" > <i class="fa fa-shopping-cart"></i> <span class="user-cart-text">Giỏ hàng</span> <span class="user-cart-quantity hide"><%=totalCart() %></span> </a> </div>
    
    <!--cart-->
    <div class="popup-over" id="popup-cart" >
     <div class="inner-popup">
      <div class="cust-box-1" style=" width:800px;"> <i class="close-pp fa fa-remove"></i>
       <div class="inner-box-cust-1">
        <div class="panel-popup">
         <p class="tt-popup"> <i class="fa fa-shopping-basket"></i> Giỏ hàng của bạn</p>
        </div>
        <div class="content-popup">
         
              <asp:Repeater ID="Rpgiohang" runat="server" OnItemCommand="Rpgiohang_ItemCommand">
                  <HeaderTemplate>
                      <div class="cart-content">
                      <table class="tblcart">
                       <tr>
                        <th>SẢN PHẨM</th>
                        <th>ĐƠN GIÁ</th>
                        <th>SỐ LƯỢNG</th>
                        <th>THÀNH TIỀN</th>
                       </tr>
                  </HeaderTemplate>
            <ItemTemplate>
           <tr>
            <td><div class="col3 imgcartpp">
                <input type="hidden" value='<%# Eval("news_id") %>' id="newsid" runat="server" />
                    <a href='<%# GetLink(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>' target='_parent'
                        class='img_cart fl'>
                        <img src='<%# GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMAGE3")) %>' alt='' height='100' /></a>
                </div>
             <div class="col9">
              <div class="tt-sp-cart"><a href='<%# GetLink(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>'>
                                <%# Eval("NEWS_TITLE") %></a></div>
              <%--<div class="removesp"> <i class="fa fa-remove" ></i><a class="removesp_link"> Bỏ sản phẩm</a> </div>--%>
             </div></td>
            <td>
                <span class="new" id="soldprice_1757" <%#setStyle(Eval("NEWS_PRICE2")) %>>
                            <%# String.Format("{0:0,0 VNĐ}", Eval("Basket_Price")).Replace(",",".")%></span>
                        <%--<del class="old">
                            <%#GetPrice2(Eval("NEWS_PRICE1"),Eval("NEWS_PRICE2"))%></del>--%>
                        <%--<%#Getgiam(Eval("NEWS_PRICE1"),Eval("NEWS_PRICE2"))%>--%>
            </td>
            <td>                
                 <asp:DropDownList ID="Drquan" runat="server" style="width:50px;" AutoPostBack="True" OnSelectedIndexChanged="drSoLuong_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Drquan" />
                    </Triggers>
                    <ContentTemplate>
                        <%# String.Format("{0:0,0 VNĐ}",Convert.ToInt32(Eval("Basket_Price"))*Convert.ToInt32(Eval("Basket_Quantity"))).Replace(",",".") %>
                    </ContentTemplate>
                </asp:UpdatePanel></td>
           </tr>
              </ItemTemplate>
                  <FooterTemplate>
                      </table>
                         </div>
                            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Drquan" />
                                </Triggers>
                                <ContentTemplate>--%>
                                    <div class="cart-footer">
                                        <div class="backpp" > <a class="close-pp"  href="/trang-chu.html"><i class="fa fa-reply"></i> Tiếp tục mua hàng</a></div>
                                        <div class="cart-price-total" style="display:none"> Thành tiền: <span class="pTotalPrice">
                                            <%--<asp:Label ID="Lbtotal" runat="server" Text=""></asp:Label>--%>
                                            <%# Total_Amount() %>
                                                                                   </span> </div>
                                        <p>
                                            <a href="/thanh-toan-buoc-1.html" class="btn-pp  btn-cart-pp" style=" width:250px">Tiến hành thanh toán</a>
                                        </p>
                                        </div>
                                <%--</ContentTemplate>
                            </asp:UpdatePanel>  --%> 
                  </FooterTemplate>
        </asp:Repeater>
                
        </div>
       </div>
      </div>
     </div>
    </div>
    
    <!--cart--> 
   </div>
   <div class="fr hidden"> <a href="javascript:void(0)" onclick="regisEmail();" title="Đăng ký ngay nhận nhiều ưu đãi"> <span class="register_button"></span> </a> </div>
  </div>
 </div>
</div>
<script>
 $(".link_login").click(function(){
    $(".popup_login").fadeIn(); 
});

$(".closebtn").click(function(){
    $(".popup_login").fadeOut(); 
});


 $(".link_ktradh").click(function(){
    $("#drop_ktradh").fadeIn(); 
});

$(".closebtn").click(function(){
    $("#drop_ktradh").fadeOut(); 
});
    function regisEmail() {
        $('html, body').animate({ scrollTop: $("#get_email").offset().top }, 1000);
    }
</script>
<%--<ul class="link_right fr hidden">
 <li id="div_register" runat="server"><span class="top_ico account_ico1"></span> <a href="/dang-ky.html" class="regis_link ">Đăng ký</a></li>
 <li>
  <div class="filter_name" id="dLikeProduct"> <span class="top_ico liked_ico"></span><span class="tt_linktop">Sản phẩm yêu thích</span>
   <div class="contentEGP dropdown liked_P">
    <div id="likecount"> Có <b style="color: #FF0000"> <%=countprolike()%></b> sản phẩm được yêu thích</div>
    <div class="list_liked_P">
     <ul>
      <asp:Repeater ID="Rppro_like" runat="server">
       <ItemTemplate>
        <li> 
         <!--Start Product-->
         <div class="liked_item_P">
          <div class="img_general"> <a href="<%# GetLink(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>"> <img src="<%# GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMAGE3")) %>"> </a> &nbsp;</div>
          <div class="r_product_info">
           <h4> <a href="<%# GetLink(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>"> <%#Eval("NEWS_TITLE") %></a></h4>
           <div class="info_price"> <span class="f_price"><del> <%#GetPrice2(Eval("News_Price1"), Eval("News_Price2"))%></del></span> <span class="main_price"> <%#GetPrice1(Eval("News_Price1"), Eval("News_Price2"))%></span> </div>
          </div>
         </div>
         <!--End Product--> 
        </li>
       </ItemTemplate>
      </asp:Repeater>
     </ul>
    </div>
   </div>
  </div>
 </li>
 <li> 
  <!--Toggle Click-->
  <div class="filter_name"> <span class="top_ico check_order_ico"></span><span class="tt_linktop">Kiểm tra đơn hàng</span> </div>
  <!--End Toggle Click--> 
 </li>
 <li>
  <div class="filter_name">
   <asp:Repeater ID="Rpmenutop" runat="server">
    <ItemTemplate> <span class="top_ico support_ico"></span><span class="tt_linktop"> <%#Eval("CAT_NAME") %></span>
     <div class="contentEGP dropdown dropdown_items">
     <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Load_Menu2(Eval("Cat_ID")) %>'>
      <ItemTemplate> <a target="_top" class="dropdown_item" href="http<%#GetLinkCat(Eval("Cat_Url"),Eval("Cat_Seo_Url")) %>"> <%#Eval("CAT_NAME") %></a>
       </div>
      </ItemTemplate>
     </asp:Repeater>
    </ItemTemplate>
   </asp:Repeater>
  </div>
 </li>
</ul>--%>
<uc2:forgetpass ID="forgetpass1" runat="server" />
<script  src="../vi-vn/Ajax/login.js" type="text/javascript"></script> 