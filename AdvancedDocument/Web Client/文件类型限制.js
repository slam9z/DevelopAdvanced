function LoadFileExensions(filetypes) {
    var extensions = filetypes.split(";");
    var validFileExtensions = new Array();

    for (var i = 0; i < extensions.length; i++) {
        var extension = String(extensions[i]);
        var dot_index = extension.lastIndexOf(".");

        if (dot_index >= 0) {
            if (dot_index >= 0) {
                extension = extension.substr(dot_index + 1).toLowerCase();
            } else {
                extension = extension.toLowerCase();
            }

            // If one of the extensions is * then we allow all files
            if (extension == "*") {
                validFileExtensions = new Array();
                break;
            }

           validFileExtensions.push(extension);
        }
    }

    return  validFileExtensions;
}


function CheckFileType(validFileExtensions,fileName) {
    // If no extensions are defined then a *.* was passed and the check is unnecessary
    if (validFileExtensions.length == 0) {
        return true;
    }

    var last_dot_index = fileName.lastIndexOf(".");
    var extension = "";
    if (last_dot_index >= 0) {
        extension = fileName.substr(last_dot_index + 1).toLowerCase();
    }

    var is_valid_filetype = false;
    for (var i = 0; i < validFileExtensions.length; i++) {
        if (String(validFileExtensions[i]) == extension) {
            is_valid_filetype = true;
            break;
        }
    }

    return is_valid_filetype;
}
