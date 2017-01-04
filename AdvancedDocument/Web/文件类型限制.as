           private function LoadFileExensions(filetypes:String):void {
                var extensions:Array = filetypes.split(";");
                this.valid_file_extensions = new Array();

                for (var i:Number=0; i < extensions.length; i++) {
                    var extension:String = String(extensions[i]);
                    var dot_index:Number = extension.lastIndexOf(".");

                    if (dot_index >= 0) {
                    if (dot_index >= 0) {
                        extension = extension.substr(dot_index + 1).toLowerCase();
                    } else {
                        extension = extension.toLowerCase();
                    }

                    // If one of the extensions is * then we allow all files
                    if (extension == "*") {
                        this.valid_file_extensions = new Array();
                        break;
                    }

                    this.valid_file_extensions.push(extension);
                }
            }

			
			
				private function CheckFileType(file_item:FileItem):Boolean {
                // If no extensions are defined then a *.* was passed and the check is unnecessary
                if (this.valid_file_extensions.length == 0) {
                    return true;
                }

                var fileRef:FileReference = file_item.file_reference;
                var last_dot_index:Number = fileRef.name.lastIndexOf(".");
                var extension:String = "";
                if (last_dot_index >= 0) {
                    extension = fileRef.name.substr(last_dot_index + 1).toLowerCase();
                }

                var is_valid_filetype:Boolean = false;
                for (var i:Number=0; i < this.valid_file_extensions.length; i++) {
                    if (String(this.valid_file_extensions[i]) == extension) {
                        is_valid_filetype = true;
                        break;
                    }
                }

                return is_valid_filetype;
            }
