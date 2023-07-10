function Edit(id) {
    $.ajax({
        url: "/FAQ/Edit?id="+id,
        success: function (data) {

        }
    });
}

function Delete(id) {
    $('#ConfirmDailog').modal({
        show: 'fatrue'
    });

}