$(document).ready(function () {
    loadPermission();
    $(document).on("click", ".collapsible", function (e) {
        e.preventDefault();
        var this1 = $(this); // Get Click item 
        var data = {
            pid: $(this).attr('pid'),
            idEp: $(this).attr('idEp')
        };

        var isLoaded = $(this1).attr('data-loaded'); // Check data already loaded or not
        if (isLoaded == "false") {
            $(this1).addClass("loadingP");   // Show loading panel
            $(this1).removeClass("collapse");
            // Now Load Data Here 
            $.ajax({
                url: "/Permission/GetSubMenuCheck",
                type: "GET",
                data: data,
                dataType: "json",
                async: false,
                success: function (d) {
                    $(this1).removeClass("loadingP");
                    if (d.data.length > 0) {
                        var $ul = $("<ul style='margin-bottom: 10px;'></ul>");
                        $.each(d.data, function (i, ele) {
                            var flg = 0;
                            if (d.checkedvalue != null) {
                                $.each(d.checkedvalue, function (i, value) {
                                    if (value == ele.permissionId) {
                                        $ul.append(
                                            $("<li style='margin: 10px 0;'></li>").append(
                                                "<span style='position: relative; top: -2px' ideP='" + d.ideP + "' class='collapsible collapses' data-loaded='false' pid='" + ele.permissionId + "'>&nbsp;</span>" +
                                                "<input style='margin-right:10px' type='checkbox' checked class='parentCheck' value='" + ele.permissionId + "' />"+
                                                "<span style='margin-right:10px'><a>" + ele.permissionName + "</a></span>"                    
                                            )
                                        )
                                        flg = 1;
                                    }
                                })
                            }
                            if (flg == 0) {
                                $ul.append(
                                    $("<li style='margin: 10px 0;'></li>").append(
                                        "<span style='position: relative; top: -2px' ideP='" + d.ideP + "' class='collapsible collapses' data-loaded='false' pid='" + ele.permissionId + "'>&nbsp;</span>" +
                                        "<input style='margin-right:10px' type='checkbox' class='parentCheck' value='" + ele.permissionId + "' />"+
                                        "<span style='margin-right:10px'><a>" + ele.permissionName + "</a></span>"
                                        
                                    )
                                )
                            }
                        });

                        $(this1).parent().append($ul);
                        $(this1).addClass('collapses');
                        $(this1).toggleClass('collapses expand');
                        $(this1).closest('li').children('ul').slideDown();
                    }
                    else {
                        // no sub menu
                        $(this1).addClass('collapses');
                        $(this1).css({ 'dispaly': 'inline-block !important', 'width': '15px' });
                    }

                    $(this1).attr('data-loaded', true);
                },
                error: function () {
                    alert("Error!");
                }
            });
        }
        else {
            // if already data loaded
            $(this1).toggleClass("collapses expand");
            $(this1).siblings('ul').slideToggle();
        }

    });
});
function loadPermission() {
    $.ajax({
        url: "/Permission/GetAllPermission",
        type: "GET",
        dataType: "json",
        async: false,
        success: function (d) {
            console.log(d);
        }
    })
}