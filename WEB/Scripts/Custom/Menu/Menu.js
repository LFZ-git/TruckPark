function SMenu(id) {
    //var myData = localStorage.getItem('ActiveMenuId');
  // alert(id);
    //if (myData === undefined && id === myData)
    localStorage.setItem('ActiveMenuId', id);
    GetSubMenu(id);
}

function GetSubMenu(id) {
    //var myData = localStorage.getItem('ActiveMenuId');
    //if (myData != undefined && id == myData)
    //    return false;
    //localStorage.setItem('ActiveMenuId', id);
    $.ajax({
        url: '/Menu/GetMenu?parentId=' + id,
        method:'Get',
        success: function (response) {
            $("#childMenu").html(response);
        },
        error: function (jqXHR, exception) {
            var error = exception;
        },
    });
}