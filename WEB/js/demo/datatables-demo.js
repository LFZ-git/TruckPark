// Call the dataTables jQuery plugin
//$(document).ready(function() {
//  $('#dataTable').DataTable();
//});
$(document).ready(function() {
  $("#example").DataTable({
    aaSorting: [],
    responsive: true,

    columnDefs: [
      {
        responsivePriority: 1,
        targets: 0
      },
      {
        responsivePriority: 2,
        targets: -1
      }
    ]
  });

  $(".dataTables_filter input")
    .attr("placeholder", "Search here...")
    .css({
      width: "width: 60%;",
      display: "inline-block"
    });

  $('[data-toggle="tooltip"]').tooltip();
});

$(document).ready(function () {
    $("#example_a").DataTable({
        aaSorting: [],
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50]],
        responsive: true,

        columnDefs: [
            {
                responsivePriority: 1,
                targets: 0
            },
            {
                responsivePriority: 2,
                targets: -1
            }
        ]
    });

    $(".dataTables_filter input")
        .attr("placeholder", "Search here...")
        .css({
            width: "width: 60%;",
            display: "inline-block"
        });

    $('[data-toggle="tooltip"]').tooltip();
});



$(document).ready(function () {
    $("#example_v").DataTable({
        aaSorting: [],
        responsive: true,

        columnDefs: [
            {
                responsivePriority: 1,
                targets: 0
            },
            {
                responsivePriority: 2,
                targets: -1
            }
        ]
    });

    $(".dataTables_filter input")
        .attr("placeholder", "Search here...")
        .css({
            width: "width: 60%;",
            display: "inline-block"
        });

    $('[data-toggle="tooltip"]').tooltip();
});






//$(document).ready(function () {
//    $('#example_b').DataTable({
//        initComplete: function () {
//            this.api().columns().every(function () {
//                var column = this;
//                var select = $('<select><option value=""></option></select>')
//                    .appendTo($(column.footer()).empty())
//                    .on('change', function () {
//                        var val = $.fn.dataTable.util.escapeRegex(
//                            $(this).val()
//                        );

//                        column
//                            .search(val ? '^' + val + '$' : '', true, false)
//                            .draw();
//                    });

//                column.data().unique().sort().each(function (d, j) {
//                    select.append('<option value="' + d + '">' + d + '</option>')
//                });
//            });
//        }
//    });
//});



