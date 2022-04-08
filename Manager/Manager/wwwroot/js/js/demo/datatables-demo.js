////// Call the dataTables jQuery plugin

$(document).ready(function () {
    //$('#dataTable thead tr').clone(true).appendTo('#dataTable thead');
    //$('#dataTable thead tr:eq(1) th').each(function (i) {
    //    var title = $(this).text();
    //    $(this).html('<input type="text" placeholder="Search ' + title + '" />');

    //    $('input', this).on('keyup change', function () {
    //        if (table.column(i).search() !== this.value) {
    //            table
    //                .column(i)
    //                .search(this.value)
    //                .draw();
    //        }
    //    });
    //});
    //--------------Sort search column for datatable
    $('.dataTables thead tr')
        .clone(true)
        .find('th')
        .off('click')
        .end()
        .appendTo('#dataTable thead');

    $('#dataTable thead tr:eq(1) th').each(function (i) {
        var title = $(this).text();
        $(this).html('<input type="text" class="col-md-11 form-controls" placeholder="Search..." />');

        $('input', this).on('keyup change', function () {
            if (table.column(i).search() !== this.value) {
                table
                    .column(i)
                    .search(this.value)
                    .draw();
            }
        });
    });
    
    var table = $('#dataTable').DataTable({
        language: {
            searchPlaceholder: "Search..."
        },
        "orderCellsTop": true,
        "responsive": true
    });
    //-------------------------------------------------------------
    //$('#dataTable').DataTable({
    //    language: {
    //        searchPlaceholder: "Search..."
    //    },
    //    "orderCellsTop": true,
    //    "responsive": true
    //});
   
});
//******************************************//
// Ẩn cloumns
//$(document).ready(function () {
//    $('#dataTable').DataTable({
//        "columnDefs": [
//            {
//                "targets": [2],
//                "visible": false,
//                "searchable": false
//            },
//            {
//                "targets": [3],
//                "visible": false
//            }
//        ]
//    });
//});
//******************************************//
// Thêm số thứ tự ở dòng đầu
//$(document).ready(function () {
//    var t = $('#dataTable').DataTable({
//        "columnDefs": [{
//            "searchable": false,
//            "orderable": false,
//            "targets": 0
//        }],
//        "order": [[1, 'asc']]
//    });

//    t.on('order.dt search.dt', function () {
//        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
//            cell.innerHTML = i + 1;
//        });
//    }).draw();
//});


//******************************************//
/*
Chèn các biểu tượng vào các table 
 */
//$(document).ready(function () {
//    $('#dataTable').DataTable({
//        ajax: '../ajax/data/objects_salary.txt',
//        columns: [
//            {
//                data: 'name'
//            },
//            {
//                data: 'position',
//                render: function (data, type) {
//                    if (type === 'display') {
//                        let link = "http://datatables.net";

//                        if (data[0] < 'H') {
//                            link = "http://cloudtables.com";
//                        }
//                        else if (data[0] < 'S') {
//                            link = "http://editor.datatables.net";
//                        }

//                        return '<a href="' + link + '">' + data + '</a>';
//                    }

//                    return data;
//                }
//            },
//            {
//                className: 'f32', // used by world-flags-sprite library
//                data: 'office',
//                render: function (data, type) {
//                    if (type === 'display') {
//                        let country = '';

//                        switch (data) {
//                            case 'Argentina':
//                                country = 'ar';
//                                break;
//                            case 'Edinburgh':
//                                country = '_Scotland';
//                                break;
//                            case 'London':
//                                country = '_England';
//                                break;
//                            case 'New York':
//                            case 'San Francisco':
//                                country = 'us';
//                                break;
//                            case 'Sydney':
//                                country = 'au';
//                                break;
//                            case 'Tokyo':
//                                country = 'jp';
//                                break;
//                        }

//                        return '<span class="flag ' + country + '"></span> ' + data;
//                    }

//                    return data;
//                }
//            },
//            {
//                data: 'extn',
//                render: function (data, type, row, meta) {
//                    return type === 'display' ?
//                        '<progress value="' + data + '" max="9999"></progress>' :
//                        data;
//                }
//            },
//            {
//                data: "start_date"
//            },
//            {
//                data: "salary",
//                render: function (data, type) {
//                    var number = $.fn.dataTable.render.number(',', '.', 2, '$').display(data);

//                    if (type === 'display') {
//                        let color = 'green';
//                        if (data < 250000) {
//                            color = 'red';
//                        }
//                        else if (data < 500000) {
//                            color = 'orange';
//                        }

//                        return '<span style="color:' + color + '">' + number + '</span>';
//                    }

//                    return number;

//                }
//            }
//        ]
//    });
//});
//-------------------------------------------*/
// Điều chỉnh lại pageing của table
//$(document).ready(function () {
//    $('#dataTable').DataTable({
//        "pagingType": "full_numbers"
//    });
//});

//*******************************************
// Tạo dọc trang scoll
//$(document).ready(function () {
//    $('#dataTable').DataTable({
//        "scrollY": "200px",
//        "scrollCollapse": true,
//        "paging": false
//    });
//});

//-------------------------------------------
// Cuộn ngang trang
//$(document).ready(function () {
//    $('#dataTable').DataTable({
//        "scrollX": true
//    });
//});

//------------------------------------------
// Cuộn ngang và dọc
//$(document).ready(function () {
//    $('#dataTable').DataTable({
//        "scrollY": 200,
//        "scrollX": true
//    });
//});

//------------------------------------------
// Nhận dạng số phẩy động và chèn vào
//$(document).ready(function () {
//    $('#example').DataTable({
//        "language": {
//            "decimal": ",",
//            "thousands": "."
//        }
//    });
//});

//----------------------------------------------
// Thay đổi các info trong datatable
//$(document).ready(function () {
//    $('#dataTable').DataTable({
//        "language": {
//            "lengthMenu": "Display _MENU_ records per page",
//            "zeroRecords": "Nothing found - sorry",
//            "info": "Showing page _PAGE_ of _PAGES_",
//            "infoEmpty": "No records available",
//            "infoFiltered": "(filtered from _MAX_ total records)"
//        }
//    });
//});


// Customize lại search 
//$(document).ready(function () {
//    // Setup - add a text input to each footer cell
//    $('#example tfoot th').each(function () {
//        var title = $(this).text();
//        $(this).html('<input type="text" placeholder="Search ' + title + '" />');
//    });

//    // DataTable
//    var table = $('#example').DataTable({
//        initComplete: function () {
//            // Apply the search
//            this.api().columns().every(function () {
//                var that = this;

//                $('input', this.footer()).on('keyup change clear', function () {
//                    if (that.search() !== this.value) {
//                        that
//                            .search(this.value)
//                            .draw();
//                    }
//                });
//            });
//        }
//    });

//});


//-----------------------------------------
// Cách đánh giấu các cột có màu active đi
//$(document).ready(function () {
//    var table = $('#example').DataTable();

//    $('#example tbody')
//        .on('mouseenter', 'td', function () {
//            var colIdx = table.cell(this).index().column;

//            $(table.cells().nodes()).removeClass('highlight');
//            $(table.column(colIdx).nodes()).addClass('highlight');
//        });
//});

//----------------------------------------------------
// Cách bắt event khi click vào td
//$(document).ready(function () {
//    var table = $('#dataTable').DataTable();

//    $('#dataTable tbody').on('click', 'tr', function () {
//        var data = table.row(this).data();
//        alert('You clicked on ' + data[1] + '\'s row');
//    });
//});