// function for  validate file extension
var validExt = ".png, .pdf, .jpeg, .jpg, .zip,.gif,.tiff";
function fileExtValidate(fdata) {
    var filePath = fdata.value;
    var getFileExt = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
    var pos = validExt.indexOf(getFileExt);
    if (pos < 0) {
        //alert("Allowed extensions are .png, .jpg, .jpeg ,.pdf,.gif,.tiff and .zip. Please choose the correct file.");
        return false;
    } else {
        return true;
    }
}

//function for validate file size 
var maxSize = '5120'; var ZipmaxSize = '5120'; var message = "File is exceeding the allowed limit of 5 MB. Please choose another file";
function fileSizeValidate(fdata) {
    var filePath = fdata.value;
    var getFileExt = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
    console.log(getFileExt);
    if (getFileExt === "zip") { maxSize = ZipmaxSize; message = "Zip file is exceeding the allowed limit of 5 MB. Please choose another file"; }
    if (getFileExt === "mp4") { maxSize = ZipmaxSize; message = "Mp4 file is exceeding the allowed limit of 5 MB. Please choose another file"; }
    if (fdata.files && fdata.files[0]) {
        var fsize = fdata.files[0].size / 1024;
        if (fsize > maxSize) {
            //alert(message);
            return false;
        } else {
            return true;
        }
    }
}

var validExtmp = ".mp4, .pdf";
function fileExtValidatemp4(fdata) {
    var filePath = fdata.value;
    var getFileExt = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
    var pos = validExtmp.indexOf(getFileExt);
    if (pos < 0) {
        //alert("Allowed extensions are .mp4,.pdf. Please choose the correct file.");
        return false;
    } else {
        return true;
    }
}

var validExtmppdf = ".pdf";
function fileExtValidatpdf(fdata) {
    var filePath = fdata.value;
    var getFileExt = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
    var pos = validExtmppdf.indexOf(getFileExt);
    if (pos < 0) {
        //alert("Allowed extensions are .mp4,.pdf. Please choose the correct file.");
        return false;
    } else {
        return true;
    }
}


var validExtmponlyimg = ".png, .jpeg, .jpg ,.gif,.tiff";
function fileExtValidateOnlyimg(fdata) {
    var filePath = fdata.value;
    var getFileExt = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
    var pos = validExtmponlyimg.indexOf(getFileExt);
    if (pos < 0) {
        //alert("Allowed extensions are .png, .jpeg, .jpg ,.gif,.tiff. Please choose the correct file.");
        return false;
    } else {
        return true;
    }
}

var validExtpdfimg = ".png, .jpeg, .jpg ,.gif,.tiff,.pdf";
function fileExtValidatepdfimg(fdata) {
    var filePath = fdata.value;
    var getFileExt = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
    var pos = validExtpdfimg.indexOf(getFileExt);
    if (pos < 0) {
        //alert("Allowed extensions are .png, .jpeg, .jpg ,.gif,.tiff,.pdf. Please choose the correct file.");
        return false;
    } else {
        return true;
    }
}

var validExtpdfxlsx = ".pdf, .doc , .xlsx , .docx , .xls , .txt";
function fileExtValidatepdfxlsx(fdata) {
    var filePath = fdata.value;
    var getFileExt = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
    var pos = validExtpdfxlsx.indexOf(getFileExt);
    if (pos < 0) {
        //alert("Allowed extensions are .png, .jpeg, .jpg ,.gif,.tiff,.pdf. Please choose the correct file.");
        return false;
    } else {
        return true;
    }
}