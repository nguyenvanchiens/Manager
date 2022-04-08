$(document).ready(function () {
    var Menus = new Array();
    function loadMenus() {
        $.ajax({
            url: "/Menu/GetAllMenu",
            type: "GET",
            dataType: "json",
            async: false,
            success: function (d) {
                Menus = d;
            }
        })
    }
    loadMenus();



    $(document).on("click", ".collapsible", function (e) {
        e.preventDefault();
        var this1 = $(this); // Get Click item      
        var isLoaded = $(this1).attr('data-loaded'); // Check data already loaded or not
        if (isLoaded == "false") {
            $(this1).addClass("loadingP");   // Show loading panel
            $(this1).removeClass("collapses");
            var id = $(this).attr('pid');
            var childrentMenus = new Array();
            $.each(Menus, function (i, ele) {
                if (ele.parentId == id) {
                    childrentMenus.push(ele);
                }
            })
            // Now Load Data Here           
             $(this1).removeClass("loadingP");
            if (childrentMenus.length > 0) {
                $.each(childrentMenus, function (i, ele) {
                     var $ul = $("<tr></tr>");
                     $ul.append(
                         $("<td style='padding:30px 30px;border:none'></td>").append(
                             "<span class='collapsible collapses' data-loaded='false' pid='"+ele.menuId+"'>&nbsp;</span>" +
                             "<span class='tbpermisionname' style='margin-right:20px'><a>" + ele.title + "</a></span>" +
                             "<button style='margin-right:20px' href='#myModal' data-bs-toggle='modal' onclick=ShowFormUpdate('" + ele.menuId+"') type='button' class='btn btn-default'>Chỉnh sửa</button>" +
                             "<a style='cursor:pointer' onclick=Delete('" + ele.menuId+"') class= 'btn btn-light' > Xóa</a >"
                         )
                    )
                    $(this1).parent().append($ul);
                     $(this1).addClass('collapses');
                 });
                 $(this1).toggleClass('collapses expand');
                 $(this1).closest('td').children('tr').slideDown();
                
             }
             else {
                 // no sub menu
                 $(this1).toggleClass("collapses");
                 $(this1).css({ 'dispaly': 'inline-block !important', 'width': '15px' });
             }
             $(this1).attr('data-loaded', true);
        }
        else {
            // if already data loaded
            $(this1).toggleClass("collapses expand");
            $(this1).siblings('tr').slideToggle();
        }
    });
});

