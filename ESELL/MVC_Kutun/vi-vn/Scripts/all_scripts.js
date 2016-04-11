//Scripts click Toggle
$(document).ready(function() {
var notH = 1,
    $pop = $('.contentEGP').hover(function(){notH^=1;});

$(document).on('mouseup keyup', function( e ){
  if(notH||e.which==27) $pop.stop().hide();
});




/////// CALL POPUP 
$('.filter_name').click(function(){
	jQuery(this).find('.contentEGP').slideDown("fast").end().siblings().find('.contentEGP').hide('fast');							 
	event.stopPropagation();  
});

//Nivo Slide Responsive
$('#slider').nivoSlider();	
});        


$( function(){
		$('#popular_products ul').carouFredSel({					
					prev: '#prev_popP',
					next: '#next_popP',					
					auto: { pauseDuration: 3000, duration: 1500 },	
					scroll : {
						items	: 4,
			            pauseOnHover: true
        			},
					speed: 2000,
					duration: 4000,
					items: {			
						visible: {
							min: 1,
							max: 4
						}
					}
				});
		
		$('#thesame_products ul').carouFredSel({					
					prev: '#prev_thesameP',
					next: '#next_thesameP',					
					auto: { pauseDuration: 3000, duration: 1500 },	
					scroll : {
						items	: 1,
			            pauseOnHover: true
        			},
					speed: 2000,
					duration: 4000,
					items: {			
						visible: {
							min: 1,
							max: 5
						}
					}
				});
		
		$('.zoom-desc ul').carouFredSel({					
					prev: '#prev_thumb',
					next: '#next_thumb',					
					auto: { pauseDuration: 20000, duration: 1000 },
					direction: 'up',
					scroll : {
						items	: 1,
			            pauseOnHover: true
        			},
					speed: 2000,
					duration: 8000,	
					items: {						
						visible: {
							min: 1,
							max: 5
						}
					}
				});
		
		$('#bestselling_products ul').carouFredSel({					
					prev: '#prev_bestsellP',
					next: '#next_bestsellP',					
					auto: { pauseDuration: 4000, duration: 1500 },	
					scroll : {
						items	: 1,
			            pauseOnHover: true
        			},
					speed: 2000,
					duration: 4000,
					items: {			
						visible: {
							min: 1,
							max: 5
						}
					}
				});	
		
		$('#slide_viewed_P ul').carouFredSel({					
					prev: '#prev_viewedP',
					next: '#next_viewedP',
					auto: { pauseDuration: 5000, duration: 1500 },	
					scroll : {
						items	: 1,
			            pauseOnHover: true
        			},
					speed: 2000,
					duration: 4000,
					items: {			
						visible: {
							min: 1,
							max: 8
						}
					}
				});			
				
				$('#sl_trademark ul').carouFredSel({					
					prev: '#prev_trademark',
					next: '#next_trademark',
					auto: { pauseDuration: 5000, duration: 1500 },	
					scroll : {
						items	: 1,
			            pauseOnHover: true
        			},
					speed: 2000,
					duration: 4000,
					items: {			
						visible: {
							min: 1,
							max: 8
						}
					}
				});			
		
});	
///home_menu la class add o trang chu
$(".home_menu").closest(".show_menu").find(".tab_cate").hide();
$(".home_menu").closest(".show_menu").find(".tab_cate").hide();
//
$(".show_menu").closest(".top_main").find(".info_header").hide();

	
	$( ".nav_menu .t_menu" ).mouseover(function() {
	$('.m_li').removeClass('active');
	$(this).closest('.m_li').addClass('active');
$('.m_li').find('.list_categories').css('display','none');
			$('.m_li.active').find('.list_categories').css('display','block');
	});
	//mouseleave
	$( ".nav_menu .t_menu" ).mouseleave(function() {
		
		$('.m_li').removeClass('active');
		$(this).closest('.m_li').addClass('active');
		$('.m_li').find('.list_categories').css('display','none');
			$('.m_li.active').find('.list_categories').css('display','block');
			});

	$(document).ready(function() {
				$(".box-nhanxet").addClass("hidden");
	  $(".btn-viet-nx").click(function(){
				
				if($(".box-nhanxet").hasClass("hidden")){
				
				$(".box-nhanxet").show().removeClass("hidden");
				
				$(".btn-viet-nx").html("Đóng");
				
				}
				else
				{
					$(".box-nhanxet").hide().addClass("hidden");
					$(".btn-viet-nx").html("Viết nhận xét của bạn");
				}
				
				});	
				
		});	
	$(document).ready(function () {
	    $(".box-nhanxet").addClass("hidden");
	    $(".btn-viet-nx2").click(function () {
	        $(".box-nhanxet").show().removeClass("hidden");
	        $(".btn-viet-nx").html("Đóng");

	    });

	});
		
//popup
		
$(function() {  
 
$(".link-popup").on('click', function(e){
	
	var namepp=$(this).attr('href');
 $(document).find(namepp).show().removeClass("hidden");
	});
	$(".close-pp").on('click', function(e){$(this).closest(".popup-over").addClass("hidden").hide();});
	
});

$( ".popup-login-link" ).on('click', function (e) {
		e.preventDefault();
		$( "#popup-login" ).fadeIn();
});

$( ".popup-reg-link" ).on('click', function (e) {
		e.preventDefault();
		$( "#popup-reg" ).fadeIn();
});


$( ".popup-cart-link" ).on('click', function (e) {
		e.preventDefault();
		$( "#popup-cart" ).fadeIn();
});

//popup


$(document).on("scroll", onScroll);
$('#tab_menu_detail a[href^="#"]').on('click', function (e) {
    e.preventDefault();
    $(document).off("scroll");

    $('#tab_menu_detail a').each(function () {
        $(this).removeClass('active');
    })
    $(this).addClass('active');

    var target = this.hash,
    menu = target;
    $target = $(target);
    $('html, body').stop().animate({
        'scrollTop': $target.offset().top - 50
    }, 600, 'swing', function () {
        //window.location.hash = target;
        $(document).on("scroll", onScroll);
    });
});

function onScroll(event) {
    var scrollPos = $(document).scrollTop();

    $('#tab_menu_detail a').each(function () {
        var currLink = $(this);
        var refElement = $(currLink.attr("href"));
        if (refElement.position().top <= scrollPos && refElement.position().top + refElement.height() > scrollPos) {
            $('#tab_menu_detail a').removeClass("active");
            currLink.addClass("active");
        }
        else {
            currLink.removeClass("active");
        }
    });
}	