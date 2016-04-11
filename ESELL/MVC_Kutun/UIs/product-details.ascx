<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="product-details.ascx.cs"
    Inherits="MVC_Kutun.UIs.product_details" %>
<%@ Register Src="path.ascx" TagName="path" TagPrefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:HiddenField ID="Hdscore" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="Hdurl" runat="server" ClientIDMode="Static" />
<div id="main" class="cf">
<div class="path"> <a href="/">Trang chủ</a>
 <uc1:path ID="path1" runat="server" />
</div>
<!--Detail Products--> 
<!--Colorbox--> 
<script src="../vi-vn/Ajax/Ajaxlike.js" type="text/javascript"></script>
<link rel="stylesheet" href="../vi-vn/Styles/detail_product.css" type="text/css" />
<link href="../vi-vn/Styles/colorbox.css" media="all" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../vi-vn/Scripts/jquery.colorbox.js"></script> 
<script>//<![CDATA[
        $(document).ready(function () {
            $(".group_colorbox").colorbox({ rel: 'group_colorbox', width: "60%", height: "90%" });
        });	
    </script>
<div class="box">
 <div id="detail_products" itemscope itemtype="http://schema.org/Product">
  <div class="dtp_row hidden" style="border-bottom: 1px solid #cacaca; margin-bottom: 15px"> </div>
  <div class="pro_left">
   <div class="pro_l_in wmn">
    <link href="../vi-vn/Styles/cloud-zoom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../vi-vn/Scripts/cloud-zoom.1.0.2.js"></script>
    <div class="zoom-desc fl">
     <ul>
      <asp:Repeater ID="Rpimg_small" runat="server">
       <ItemTemplate>
        <li><a href='<%# GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMG_IMAGE1")) %>' class='cloud-zoom-gallery group_colorbox'
                                        title='<%#Eval("NEWS_IMG_DESC") %>' rel="useZoom: 'zoom1', smallImage: '<%# GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMG_IMAGE1")) %>'"> <img class="zoom-tiny-image" src="<%# GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMG_IMAGE1")) %>"
                                            alt="<%#Eval("NEWS_IMG_DESC") %>" /></a> </li>
       </ItemTemplate>
      </asp:Repeater>
     </ul>
     <div class="cf"> </div>
     <a id="prev_thumb" class="prev" href="#">&lt;</a> <a id="next_thumb" class="next"
                            href="#">&gt;</a> </div>
    <div class="zoom-section fl">
     <asp:Repeater ID="Rpimg_big" runat="server">
      <ItemTemplate>
       <div class="zoom-small-image"> <a href='<%# GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMG_IMAGE1")) %>' class='cloud-zoom'
                                        id='zoom1' rel="adjustX: 10, adjustY:-4" title="<%#Eval("NEWS_IMG_DESC") %>"> <img class="mainImage" src="<%# GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMG_IMAGE1")) %>"
                                            alt='<%#Eval("NEWS_IMG_DESC") %>' title="<%#Eval("NEWS_IMG_DESC") %>" itemprop="image"  /></a>
        <div class="zoom cf"> <a href='<%# GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMG_IMAGE1")) %>' class='group_colorbox'
                                            title='<%#Eval("NEWS_IMG_DESC") %>'>Click vào thumbnail để xem hình lớn</a></div>
       </div>
      </ItemTemplate>
     </asp:Repeater>
     <div class="clear bottom-img-zoom" style="margin-top:40px; display: block;"> </div>
    </div>
   </div>
  </div>
  <!--end: #dp-thumb-->
  <div id="dtp_info" class="fl">
   <h1 class="h1Title" itemprop="name">
    <asp:Label ID="Lbtitle_details" runat="server" Text=""></asp:Label>
   </h1>
   <div class="item-row1">
    <div class="item-price">
     <div class="item-brand"> <span>Thương hiệu:</span>
      <p>
       <asp:HyperLink ID="Hyperthuonghieu" runat="server"></asp:HyperLink>
      </p>
     </div>
     <p class="old-price-item" id="p-listpirce"> <span>Giá thị trường:</span> <span> <del>
      <asp:Literal ID="Lbprice_goc" runat="server" ></asp:Literal>
      </del> </span> </p>
     <p class="special-price-item" data-value="159000" id="p-specialprice"> <span>Giá khuyến mãi:</span> <span id="span-price">
      <asp:Label ID="Lbprice_promos" runat="server" Text="" ForeColor="Red"></asp:Label>
      </span>
      <asp:Label ID="lbVAT" runat="server"></asp:Label>
     </p>
     <p class="saleoff-price-item"> <span>Tiết kiệm:</span> <span>
      <asp:Label ID="Lbpricetietkiem" runat="server" Text=""></asp:Label>
      <asp:Label ID="Lbpricepointtietkiem" runat="server" Text="" ForeColor="red"></asp:Label>
      </span> </p>
    </div>
    <div class="item-other">
     <%--<div itemprop="aggregateRating" itemscope itemtype="http://schema.org/AggregateRating">
       <meta itemprop="ratingValue" content = "4.3">
       <meta itemprop="ratingCount" content = "34">
      </div>--%>
     <asp:Literal ID="lbAggregateRating" runat="server"></asp:Literal>
     <!--Rating Star-->
     <link rel="stylesheet" href="../vi-vn/Styles/jRating.jquery.css" type="text/css" />
     <script type="text/javascript" src="../vi-vn/Scripts/jquery.raty.js"></script> 
     <script type="text/javascript">
                    $(document).ready(function () {
                        var scro = document.getElementById("Hdscore").value;
                        $('#star').raty({
                            number: 5,
                            score: scro,

                        });
                        $('#star2').raty({
                            number: 5,
                            score: scro,

                        });
                    });

                </script>
     <div class="item-rating">
      <div class="rating">
       <div id="star"> </div>
      </div>
     </div>
     <p class="item-review-now btn-viet-nx2" id="tab_menu_detail"> <a id="write_review" href="#productReviews">Viết nhận xét</a> </p>
     <div class="item-benefit">
      <asp:Label ID="Lbdesc_details" runat="server" Text=""></asp:Label>
     </div>
    </div>
   </div>
   
   <!--Info Detail Products-->
   
   <div class="item-promotion">
    <div class="item-promotion-title">
     <h2>Thông tin &amp; Khuyến mãi</h2>
    </div>
    <div class="item-promotion-content">
     <div class="quatangkm" id="quatangkm" runat="server" visible="false" style="display:inline-block; max-width:200px; vertical-align:top;margin-right:20px">
      <div style="float:left;width:30%; margin-right:5%">
       <asp:Label ID="lbImgPromo" runat="server"></asp:Label>
      </div>
      <div style="float:left;width:65%">
       <p class="label">Quà tặng miễn phí</p>
       <p class="title">
        <asp:Label ID="lbNamepromo" runat="server"></asp:Label>
       </p>
       <p class="price">Trị giá:
        <asp:Label ID="lbpricepromo" runat="server"></asp:Label>
       </p>
      </div>
     </div>
     <div class="dknhanhang" style="display:inline-block;max-width:70% ; vertical-align:top">
      <asp:UpdatePanel ID="upnhanhang" runat="server">
       <ContentTemplate>
        <p class="pp1" style="">Nhận hàng tại
         <asp:DropDownList ID="Drarea2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Drarea2_SelectedIndexChanged"> </asp:DropDownList>
         <asp:Label ID="lbDesArea" runat="server" Text="từ 3 - 5 ngày , không kể Thứ 7 & CN"></asp:Label>
        </p>
       </ContentTemplate>
      </asp:UpdatePanel>
      <asp:Literal ID="Litdescbot" runat="server"></asp:Literal>
     </div>
    </div>
   </div>
   
   <!--Info Detail Products-->
   <div class="wmn">
    <div class="quantity-box">
     <p class="quantity-label">Số lượng:</p>
     <div class="quantity-col1">
      <div class="number-input">
       <div class="input-group bootstrap-touchspin">
        <asp:TextBox ID="qty" runat="server" Text="1" CssClass="form-control ipsl"></asp:TextBox>
        <span class="input-group-btn-vertical">
        <asp:LinkButton ID="btnUp" runat="server" CssClass="btn btn-default bootstrap-touchspin-up" type="button"><i class="fa fa-chevron-up"></i></asp:LinkButton>
        <asp:LinkButton ID="btnDown" runat="server" CssClass="btn btn-default bootstrap-touchspin-down" type="button"><i class="fa fa-chevron-down"></i></asp:LinkButton>
        </span>
        <ajaxToolkit:NumericUpDownExtender ID="NUD1" runat="server"
            TargetControlID="qty"  
            Width="100"
            RefValues="1;2;3;4;5;6;7;8;9"
            TargetButtonDownID="btnDown"
            TargetButtonUpID="btnUp"
            Tag="1" />
       </div>
      </div>
     </div>
     <div class="quantity-col2">
      <asp:LinkButton ID="Hyperaddtocart" runat="server" OnClick="Lbaddtocart_Click" CssClass="hidden">
       <button class="add-to-cart js-add-to-cart disabled is-css" type="button" > <span class="icon"> <i class="fa fa-shopping-cart"></i> </span> <span class="text">Mua Ngay</span> </button>
      </asp:LinkButton>
      <asp:LinkButton ID="btnAddtocart" runat="server" OnClick="btnAddtocart_Click">
       <button class="add-to-cart js-add-to-cart disabled is-css" type="button" > <span class="icon"> <i class="fa fa-shopping-cart"></i> </span> <span class="text">Thêm Vào Giỏ Hàng</span> </button>
      </asp:LinkButton>
     </div>
     <a href="/huong-dan-dat-hang.html" class="producthowToBuy" target="_blank"> <i class="icnhowtobuy"></i>Hướng dẫn<br>
     mua hàng</a> </div>
    <div class="box-like">
     <div id="fb-root"  class="wmn"> 
      <asp:Label ID="Lbface" runat="server" Text=""></asp:Label>
     </div>
     <div class="cf"></div>
     <div class="wmn themvaoyeuthich">
      <asp:HyperLink ID="Hyperlike" runat="server" CssClass="addlike" Style="cursor: pointer">
       <button class="btnlike" type="button"><i class="fa fa-heart"></i> <span class="text">Thêm Vào Yêu Thích</span></button>
      </asp:HyperLink>
     </div>
    </div>
   </div>
   <div class="info_dtp_product fl">
    <div class="dtp_row hidden" id="code_P">
     <div style="float: left; width: 50%"> <span>Mã sản phẩm:</span>
      <asp:Label ID="Lbcode" runat="server" Text="" ForeColor="blue"></asp:Label>
     </div>
     <div style="float: right; width: 50%; text-align: right"> <span>Lượt xem:</span>
      <asp:Label ID="Lbcount_see" runat="server" Text="" ForeColor="blue"></asp:Label>
     </div>
    </div>
    <div class="dtp_row hidden" id=" short_des_dtp">
     <div class="dtp_row" id="li_des">
      <ul>
      </ul>
     </div>
    </div>
    <div class="dtp_row price_detail">
     <div class="col_detail fl">
      <div class="dtp_price"> <b> </b> </div>
      <div id="div_promos" runat="server">
       <div class="f_price"> </div>
       <div class="prod_saving"> </div>
      </div>
     </div>
     <div class="col_detail fr">
      <div class="dtp_row">
       <div class="status_product available fr">
        <asp:Label ID="lbtinhtrang" runat="server" Text=""></asp:Label>
       </div>
       <!--<div class="status_product unavailable fr">Hết hàng</div>--> 
      </div>
     </div>
    </div>
    <div class="dtp_row buy_function"> <a href="/huong-dan-dat-hang.html" class="product__howToBuy" target="_blank"><i class="icn-howtobuy"> </i>Hướng dẫn mua hàng</a> </div>
   </div>
  </div>
 </div>
 <div class="cf"> </div>
</div>
<!--End Detail Products-->
<div class="box bg-none"> 
 <!--Sidebar Detail Products-->
 <div id="tab_menu_detail" class="sticky2" style="z-index:999999999999999">     
   <div id="checkdiv"></div>
   <ul>
    <li><a href="#productDetails" class="active">Thông số kỹ thuật</a></li>
    <li><a href="#productInfoTechnical">Chi tiết sản phẩm </a></li>
    <li><a href="#productReviews">Khách Hàng Nhận Xét</a></li>
   </ul>
   <div class="h2-tt"><asp:Literal ID="Lbtitle_details2" runat="server" Text=""></asp:Literal></div>
   <div class="fx-right">
   <div class="fx-price" style="color:red"> <asp:Literal ID="Lbprice_promos2" runat="server" Text="" ></asp:Literal></div>
   <div class="fx-cart">
   <i class="fa fa-shopping-cart"></i>
       <asp:DropDownList ID="ddlQty" runat="server" CssClass="slt">
           <asp:ListItem Value="1" Text="1"></asp:ListItem>
           <asp:ListItem Value="2" Text="2"></asp:ListItem>
           <asp:ListItem Value="3" Text="3"></asp:ListItem>
           <asp:ListItem Value="4" Text="4"></asp:ListItem>
           <asp:ListItem Value="5" Text="5"></asp:ListItem>
           <asp:ListItem Value="6" Text="6"></asp:ListItem>
           <asp:ListItem Value="7" Text="7"></asp:ListItem>
           <asp:ListItem Value="8" Text="8"></asp:ListItem>
           <asp:ListItem Value="9" Text="9"></asp:ListItem>
           <asp:ListItem Value="10" Text="10"></asp:ListItem>
       </asp:DropDownList>
       <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Lbaddtocart_Click"><span class="fx-addcard"> Đặt mua</span></asp:LinkButton>
   </div>
   </div>
  </div>
 <div id="l_slP" class="fl">   
  <!--Tab Detail Products-->
  <div id="tabs-container">
   <div class="tabs_detail_content" id="productDetails" data-anchor="productDetails">   
   <h3 class="tt_popP"> <span>Thông số kỹ thuật</span></h3> 
    <!--Info Brand-->
    <asp:Literal ID="liHtml_thongso" runat="server"></asp:Literal>
    <!--end Info Brand-->
    <div class="cf"> </div>
   </div>
   <div class="tabs_detail_content" id="productInfoTechnical" data-anchor="productInfoTechnical"> 
   <h3 class="tt_popP"> <span>Chi tiết sản phẩm</span></h3>
    <asp:Literal ID="liHtml_info" runat="server"></asp:Literal>
    <div class="cf"> </div>
   </div>
   <div class="box" id="productReviews" data-anchor="productReviews">
    <h3 class="tt_popP"> <span>Nhận xét về sản phẩm</span></h3>
    <div class="inner-box box-rating">
     <div class="product-customer-col-1">
      <h4>Đánh Giá Trung Bình</h4>
      <p class="total-review-point">
       <asp:Literal ID="lbcount_avg" runat="server"></asp:Literal>
      </p>
      <div class="item-rating" style="text-align: center">
       <div class="item-rating">
        <div class="rating">
         <div id="star2"> </div>
        </div>
       </div>
       <p class="comments-count"><a href="#list-comment">(
        <asp:Literal ID="lbcount" runat="server"></asp:Literal>
        nhận xét)</a></p>
      </div>
     </div>
     <div class="product-customer-col-2">
      <div class="inner-col2"> 
       <!-- RATING PROGRESS BAR -->
       <asp:Literal ID="lb5Sao" runat="server"></asp:Literal>
       <asp:Literal ID="lb4Sao" runat="server"></asp:Literal>
       <asp:Literal ID="lb3Sao" runat="server"></asp:Literal>
       <asp:Literal ID="lb2Sao" runat="server"></asp:Literal>
       <asp:Literal ID="lb1Sao" runat="server"></asp:Literal>
       <!-- END RATING PROGRESS BAR --> 
       
      </div>
     </div>
     <div class="product-customer-col-3">
      <h4>Chia sẻ nhận xét về sản phẩm</h4>
      <button type="button" class="buttoncm btn-viet-nx">Viết nhận xét của bạn</button>
     </div>
    </div>
   </div>
   <div class="box box-nhanxet">    
    <div class="inner-box box-rating">
     <div class="product-customer-col-6">
      <div class="row_danhgia"> 1. Đánh giá của bạn về sản phẩm này:
       <div class="box-start-2" style="margin-left:15px">
        <ul>
         <li>
          <asp:RadioButton ID="Rdfiverate" runat="server" class="contact_now_to_check" Checked="true"
                    GroupName="rate" />
          <span class="star_icon five_star"></span></li>
         <li>
          <asp:RadioButton ID="Rdfourrate" runat="server" class="contact_now_to_check" GroupName="rate" />
          <span class="star_icon four_star"></span></li>
         <li>
          <asp:RadioButton ID="Rdthreerate" runat="server" class="contact_now_to_check" GroupName="rate" />
          <span class="star_icon three_star"></span></li>
         <li>
          <asp:RadioButton ID="Rdtworate" runat="server" class="contact_now_to_check" GroupName="rate" />
          <span class="star_icon two_star"></span></li>
         <li>
          <asp:RadioButton ID="Rdonerate" runat="server" class="contact_now_to_check" GroupName="rate" />
          <span class="star_icon one_star"></span></li>
        </ul>
       </div>
      </div>
      <div class="row_danhgia"> 2. Tên của bạn:
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ErrorMessage="Nhập tên của bạn" ControlToValidate="txtname" Display="None"
                                    ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
      </div>
      <div class="row_danhgia">
       <input type="text" id="txtname" runat="server" class="input_danhgia" placeholder="Nhập tên của bạn">
      </div>
      <div class="row_danhgia"> 3. Email/phone của bạn:
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server" ErrorMessage="Nhập email/phone của bạn" ControlToValidate="txtemail"
                                    Display="None" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
      </div>
      <div class="row_danhgia">
       <input type="text" id="txtemail" runat="server" class="input_danhgia" placeholder="Nhập email/phone của bạn">
      </div>
      <div class="row_danhgia"> 4. Tiêu đề của nhận xét:
       <asp:RequiredFieldValidator ID="rfvHoVaTen" runat="server"
                                    ErrorMessage="Chưa nhập tiêu đề" ControlToValidate="txttitle" Display="None"
                                    ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
      </div>
      <div class="row_danhgia">
       <input type="text" id="txttitle" runat="server" class="input_danhgia" placeholder="Nhập tiêu đề">
      </div>
      <div class="row_danhgia"> 5. Viết nhận xét của bạn vào bên dưới:
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server" ErrorMessage="Nhận xét về sản phẩm" ControlToValidate="txtcontent"
                                    Display="None" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
      </div>
      <div class="row_danhgia">
       <textarea id="txtcontent" runat="server" class="input_danhgia" cols="20" placeholder="Nhập nhận xét của bạn." rows="2" style="min-height:100px"></textarea>
      </div>
      <div class="row_danhgia">
       <asp:Button ID="btnRate" runat="server" Text="Gửi đánh giá" class="buttoncm" OnClick="btnSendrate_Click" ValidationGroup="G40" />
      </div>
      <div class="row_danhgia">
       <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="true"
                ShowSummary="false" ValidationGroup="G40" />
       <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail"
                ErrorMessage="Email Định Dạng Chưa Đúng" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                ForeColor="Red" ValidationGroup="G40"></asp:RegularExpressionValidator>--%>
       <asp:Label ID="lblresult" runat="server" ForeColor="Red"></asp:Label>
      </div>
     </div>
     <div class="product-customer-col-5">
      <div class="inner-noidung-khuyen-mai">
       <asp:Label ID="lbKhiNhanxet" runat="server" Text=""></asp:Label>
      </div>
     </div>
    </div>
   </div>
   <div class="tabs_detail_content"> 
    <!--Comment--> 
       <script>
           function showQuickreply(id) {
               if (document.getElementById(id).style.display == 'none') {
                   document.getElementById(id).style.display = 'block';
               }
               else {
                   document.getElementById(id).style.display = 'none';
               }
               return false;
           }
           function hideQuickreply(id) {
               if (document.getElementById(id).style.display == 'block') {
                   document.getElementById(id).style.display = 'none';
               }
               return false;
           }
        </script> 
    <div class="wmn">
     <div class="comment_review">
      <h3 class="tt-bl" id="lbNhanxet" runat="server"><span><a name="list-comment">Nhận xét mới</a></span></h3>
      <div class="commentPost"> 
       <!--End noi dung binh luan-->
       <asp:Repeater ID="Rpcomment" runat="server" OnItemCommand="Rpcomment_ItemCommand" OnItemDataBound="Rpcomment_ItemDataBound">
        <ItemTemplate>
         <div class="itemFeedback" itemprop="review" itemscope itemtype="http://schema.org/Review"> 
          <!--Hinh dai dien nguoi binh luan-->
          <div class="postActor"> <img />
           <div class="nameActor">
            <p itemprop="author"><b><%#Eval("COMMENT_NAME")%></b> </p>
            <p class="postDateTime">(<%#getdate(Eval("COMMENT_PUBLISHDATE"))%>)</p>
            <p class="actorAdd"></p>
           </div>
          </div>
          <!--End hinh dai dien nguoi binh luan--> 
          <!--Noi dung binh luan-->
          <div class="postBody">
           <div class="postContainer">
            <div class="innerPostContainer">
             <p class="danhgia-bl" ><span class="star_icon <%# getRatingComment(Eval("COMMENT_FIELD2"))%>"></span><span itemprop="name"><%#Eval("COMMENT_FIELD1")%></span> </p>
             <%--<p class="pp1">Đã mua sản phẩm này từ 2 tháng trước</p>--%>
             <div class="contentP" itemprop="reviewBody"> <%#Eval("COMMENT_CONTENT")%></div>
                                
                <div class="link">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnThankreply" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Label ID="lbThank" runat="server" Text="Nhận xét này hữu ích với bạn?"></asp:Label>
                            <asp:LinkButton ID="btnThankreply" runat="server" CommandName="Thank" CommandArgument='<%#Eval("COMMENT_ID") %>' CssClass="btn btn-primary thank-review">
                                <i class="fa fa-thumbs-o-up"></i>
                                Cảm ơn
                            </asp:LinkButton>                    
                            <a href="javascript:void(0)" class="js-quick-reply" onclick="return showQuickreply('<%#Eval("COMMENT_ID") %>');">Gửi trả lời</a>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="quick-reply" id="<%#Eval("COMMENT_ID") %>" style="display:none;">
                    <asp:UpdatePanel ID="upReply" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnQuickreply" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:TextBox ID="txtQuickReply" runat="server" TextMode="MultiLine" CssClass="form-control review_comment" placeholder="Nhập nội dung trả lời tại đây."></asp:TextBox>
                        <span class="help-block text-left">                            
                            <asp:Literal ID="lbReplyError" runat="server"></asp:Literal>
                        </span>
                        <asp:Button ID="btnQuickreply" runat="server" CommandName="Reply" CommandArgument='<%#Eval("COMMENT_ID") %>' CssClass="btn btn-primary btn_add_comment" Text="Gửi trả lời của bạn" />
                        <button type="button" class="btn btn-default js-quick-reply-hide"  onclick="return hideQuickreply('<%#Eval("COMMENT_ID") %>');">Hủy bỏ</button>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="replies">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnQuickreply" />
                    </Triggers>
                    <ContentTemplate>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <div class="replies-item">
                                <p class="replies-image">
                                    <a href="javascript:void(0)"><img class="img-responsive" src="../vi-vn/Images/avatar.png" width="45" height="45"></a>
                                </p>
                                <p class="replies-text"><%#Eval("COMMENT_CONTENT")%></p>
                            </div>
                       </ItemTemplate>
                  </asp:Repeater>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
           </div>
          </div>
             <div itemprop="itemReviewed" itemscope itemtype="http://schema.org/Product">      <span itemprop="name" content=""></span> </div>
             <div itemprop="reviewRating" itemscope itemtype="http://schema.org/Rating">
              <meta itemprop="ratingValue" content="<%#Eval("COMMENT_FIELD2")%>">
             </div>
         </div>         
        </ItemTemplate>
       </asp:Repeater>
      </div>
     </div>
     <div class="cf"> </div>
     <div class="comment_part"> 
      <div class="fb-comments" data-href="<%=getUrlface() %>" data-numposts="5" data-colorscheme="light"
                            data-width="900"> </div>
     </div>
     <!--End Comment-->
     <div class="cf"> </div>
    </div>
    
   </div>
   <!--End Tab Detail Products--> 
   <!--The Same Products-->
   <div class="box">
    <div class="tt_popP"> <span>Sản phẩm tương tự</span></div>
    <div id="thesame_products" class="sl_products">
     <ul>
      <asp:Repeater ID="rp_sanphamcungloai" runat="server">
       <ItemTemplate>
        <li> 
         <!--Start Product-->
         <div class="product"> <a class="img_product" href="<%# GetLink(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>" title="<%#Eval("NEWS_TITLE") %>"> <img height="150" src="<%# GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMAGE3")) %>"
                                                alt="<%#Eval("NEWS_TITLE") %>"></a>
          <h3 class="product_name"> <a href="<%# GetLink(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>" title="<%#Eval("NEWS_TITLE") %>"> <%#Eval("NEWS_TITLE") %></a></h3>
          <div class="info_price"> <span class="f_price"><del> <%#GetPrice2(Eval("News_Price1"), Eval("News_Price2"))%></del></span> <span class="main_price"> <%#GetPrice1(Eval("News_Price1"), Eval("News_Price2"))%></span> </div>
          <%#Getgiam(Eval("News_Price1"), Eval("News_Price2"))%> <%#getBuy(Eval("NEWS_ID"), Eval("NEWS_FIELD3"))%> </div>
         <!--End Product--> 
        </li>
       </ItemTemplate>
      </asp:Repeater>
     </ul>
     <div class="cf"> </div>
     <a id="prev_thesameP" class="prev" href="#">&lt;</a> <a id="next_thesameP" class="next"
                        href="#">&gt;</a> </div>
   </div>
   <!--end The Same Products--> 
  </div>
  <!--end Sidebar Detail Products--> 
  
 </div>
 <!--Other Products--> 
 <script src="../vi-vn/Scripts/jquery.sticky.js" type="text/javascript"></script> 
 <script type="text/javascript">
      $(document).ready(function () {
          $(".sticky").sticky({ topSpacing: 40 });
										$(".sticky2").sticky({ topSpacing: 0 });
      });
        </script>
 <div id="other_products" class="fr">
  <div class="sticky" style="width: 245px">
   <div class="tt_popP"> <span>Có Thể Bạn Quan Tâm</span></div>
   <div class="box_ct">
    <ul>
     <asp:Repeater ID="Rpproquantam" runat="server">
      <ItemTemplate>
       <li> 
        <!--Start Product-->
        <div class="product">
         <h3 class="product_name"> <a href="<%# GetLink(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>" title="<%#Eval("NEWS_TITLE") %>"> <%#Eval("NEWS_TITLE") %></a></h3>
         <a class="img_product" href="<%# GetLink(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>" title="<%#Eval("NEWS_TITLE") %>"> <img   src="<%# GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMAGE3")) %>"
                                                alt="<%#Eval("NEWS_TITLE") %>"></a>
         <div class="info_price"> <span class="f_price"><del> <%#GetPrice2(Eval("News_Price1"), Eval("News_Price2"))%></del></span> <span class="main_price"> <%#GetPrice1(Eval("News_Price1"), Eval("News_Price2"))%></span> </div>
         <div class="btn_P"> <a class="view_detail" href="<%# GetLink(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>" title="<%#Eval("NEWS_TITLE") %>"> Chi tiết</a></div>
        </div>
        <!--End Product--> 
       </li>
      </ItemTemplate>
     </asp:Repeater>
    </ul>
   </div>
  </div>
 </div>
</div>
