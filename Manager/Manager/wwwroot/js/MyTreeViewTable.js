$(document).ready(function () {
    var Parentperrmission = new Array();
    function loadPermission() {
        $.ajax({
            url: "/Permission/GetAllPermission",
            type: "GET",
            dataType: "json",
            async: false,
            success: function (d) {
                Parentperrmission = d;
            }
        })
    }
    loadPermission();



    $(document).on("click", ".collapsible", function (e) {
        e.preventDefault();
        var this1 = $(this); // Get Click item      
        var isLoaded = $(this1).attr('data-loaded'); // Check data already loaded or not
        if (isLoaded == "false") {
            $(this1).addClass("loadingP");   // Show loading panel
            $(this1).removeClass("collapses");
            var id = $(this).attr('pid');
            var childrentPermission = new Array();
            $.each(Parentperrmission, function (i, ele) {
                if (ele.parentId == id) {
                    childrentPermission.push(ele);
                }
            })
            // Now Load Data Here           
             $(this1).removeClass("loadingP");
            if (childrentPermission.length > 0) {
                $.each(childrentPermission, function (i, ele) {
                     var $ul = $("<tr></tr>");
                     $ul.append(
                         $("<td style='padding:30px 30px;border:none'></td>").append(
                             "<span class='collapsible collapses' data-loaded='false' pid='"+ele.permissionId+"'>&nbsp;</span>" +
                             "<span class='tbpermisionname' style='margin-right:20px'><a>" + ele.permissionName + "</a></span>" +
                             "<button style='margin-right:20px' href='#myModal' data-bs-toggle='modal' onclick=ShowFormUpdate('"+ele.permissionId+"') type='button' class='btn btn-default'>Chỉnh sửa</button>" +
                             "<a style='cursor:pointer' onclick=Delete('"+ele.permissionId+"') class= 'btn btn-light' > Xóa</a >"
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

