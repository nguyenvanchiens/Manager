var arr3;
$.ajax({
    type: 'Get',
    url: '/Menu/GetParentNode',
    contentType: 'application/json; charset=utf-8',
    async: false,
    success: function (data) {
        arr3 = data;
    }
})
var arrParent =
{
    title: "Parent",
    parentId: "0",
};
arr3.unshift(arrParent);
var options2 = {
    title: "Parent",
    data: arr3,
    maxHeight: 3000,
    clickHandler: function (element) {
        $(".dropdowntree-name").text(
            $(element).find("a").first().text()
        );
        $("#firstDropDownTree2").attr("parentid", $(element).find("a").first().attr("id"))
    },
    closedArrow: '<i class="fa fa-caret-right" aria-hidden="true"></i>',
    openedArrow: '<i class="fa fa-caret-down" aria-hidden="true"></i>',
    multiSelect: true,
};
console.log(options2);
$("#firstDropDownTree2").DropDownTree(options2);