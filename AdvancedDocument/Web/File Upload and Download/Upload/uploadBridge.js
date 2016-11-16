function DnDUploadManager(_1) {
    _1 = _1 || window;
    var _2 = {};
    var _3;
    var _4;
    var _5;
    var _6 = 0;
    var _7 = 0, _8 = 1, _9 = 2, _a = 3, _b = 4, _c = 5, _d = 6, _e = 7;
    if (_1.File) {
        _5 = true;
    } else {
        try {
            new FileReader();
            _5 = true;
        }
        catch (e) {
        }
    }
    this.setUrlParams = function (_f) {
        _2 = _f || {};
    };
    this.initUpload = function (_10) {
        _3 = _10;
    };
    this.initUpload2 = function (fid, id) {
        fid = fid ? fid : 0;
        _3 = "c:nf:" + fid;
    };
    this.getComposeId = function () {
        return _3;
    };
    this.isSupportHTML5 = function () {
        return _5;
    };
    this.deleteUpload = function (id) {
        var _11 = this.getUploadFileObj(id);
        if (!_11) {
            return;
        }
        try {
            _11.XHR.abort();
        }
        catch (ex) {
        }
        _11.status = _c;
        var _12 = _11.name.substring(_11.name.lastIndexOf("\\") + 1);
        new CMXClient().simpleCall("upload:deleteTasks", { composeId: _3, item: _12 });
    };
    this.moveToNetFolder = function (id, _13, _14) {
        var sid;
        if (CMXClient.getSID) {
            sid = CMXClient.getSID();
        } else {
            if (gSID) {
                sid = gSID;
            }
        }
        var _15 = this.getUploadFileObj(id);
        var _16 = _15.name.substring(_15.name.lastIndexOf("\\") + 1);
        _15.status = _d;
        new CMXClient().simpleCall("upload:moveToNetFolder", { sid: sid, composeId: _3, item: _16, attachmentId: _15.id }, function (_17) {
            _15.fileId = _17.fileId;
            _15.status = _e;
            if (_14) {
                _14(id, _13, _17.fileId);
            }
        });
    };
    this.moveToEtpNetFolder = function (id, _18, _19) {
        var sid;
        if (CMXClient.getSID) {
            sid = CMXClient.getSID();
        } else {
            if (gSID) {
                sid = gSID;
            }
        }
        var _1a = this.getUploadFileObj(id);
        var _1b = _1a.name.substring(_1a.name.lastIndexOf("\\") + 1);
        _1a.status = _d;
        var _1c = new CMXClient();
        _1c.cgi = _2;
        _1c.simpleCall("upload:moveToNetDisk", { sid: sid, composeId: _3, item: _1b, attachmentId: _1a.id }, function (_1d) {
            _1a.fileId = _1d.fileId;
            _1a.status = _e;
            if (_19) {
                _19(id, _18, _1d.fileId);
            }
        });
    };
    this.cancelUpload = function (id) {
        var _1e = this.getUploadFileObj(id);
        if (!_1e) {
            return;
        }
        _1e.offset = _1e.bytesLoaded || 0;
        try {
            _1e.XHR.abort();
        }
        catch (e) {
        }
        _1e.status = _c;
        _1e.cancel = true;
    };
    this.restoreState = function (id) {
        var _1f = this.getUploadFileObj(id);
        if (!_1f) {
            return;
        }
        _1f.status = _7;
    };
    this.addNetFolderToContinue = function (_20, id, crc) {
        return id;
    };
    this.queryStatus = function (id) {
        var _21 = this.getUploadFileObj(id);
        var _22 = _21.bytesLoaded || _21.offset || 0;
        var _23 = this.getFileSize(id);
        var _24 = Math.floor(_22 / (_23 || 1) * 100);
        var _25 = _9;
        if (_7 == _21.status || _21.status) {
            _25 = _21.status;
        }
        var _26 = _21.id || "";
        return _25 + "," + _24 + "," + _26 + "," + _23 + "," + _22;
    };
    this.getFileSize = function (id) {
        var _27 = this.getUploadFileObj(id);
        var _28 = 0;
        if (_27.blob) {
            _28 = _27.blob.length;
        } else {
            if (_27.size) {
                _28 = _27.size;
            }
        }
        return _28;
    };
    this.getUploadFailReason = function (id) {
        var _29 = this.getUploadFileObj(id);
        if (!_29 || _29.status != _b) {
            return;
        }
        return _29.failId || 3;
    };
    this.getServerReturnInfo = function (id) {
    };
    this.checkDnDFile = function (_2a) {
        _2a = _2a || _1.event;
        var _2b = false;
        if (_5) {
            var n = _2a.dataTransfer.types;
            var G = n.length;
            var q = "";
            for (var i = 0; i < G; i++) {
                q = n[i];
                if (q == "Files") {
                    _2b = true;
                    break;
                }
            }
        }
        return _2b;
    };
    this.addMultiDnDFile = function (_2c) {
        _2c = _2c || _1.event;
        var _2d = [];
        try {
            if (_5) {
                _2d = _2c.dataTransfer.files;
            }
            Event.stop(_2c);
            return this.addMultiFile(_2d);
        }
        catch (er) {
        }
    };
    this.addMultiFile = function (_2e) {
        _4 = (_4 || []).concat($A(_2e));
        var ids = [];
        for (var i = 0; i < _2e.length; i++) {
            ids.push("g:" + (_6 + i));
        }
        _6 = _4.length;
        return ids.join(",");
    };
    this.getUploadFileName = function (id) {
        if (id.indexOf("g:") == 0) {
            var pos = id.substring(2);
            var _2f = pos - (_6 - _4.length);
            if (_4.length > _2f) {
                return _4[_2f].name;
            }
        }
    };
    this.getUploadFileObj = function (id) {
        if (id.indexOf("g:") == 0) {
            var pos = id.substring(2);
            var _30 = pos - (_6 - _4.length);
            if (_4.length > _30) {
                return _4[_30];
            }
        }
    };
    this.startUpload = function (id) {
        var sid;
        if (CMXClient.getSID) {
            sid = CMXClient.getSID();
        } else {
            if (gSID) {
                sid = gSID;
            }
        }
        var _31 = this.getUploadFileObj(id);
        _31.offset = _31.offset || 0;
        var _32 = { sid: sid, inlined: false, size: this.getFileSize(id), fileName: this.getUploadFileName(id), attachId: 0, composeId: _3 };
        var _33 = new CMXClient();
        _33.resultListener = function (_34) {
            _31.status = _b;
            _31.id = 0;
            _31.failObj = _34;
            if (_34.code == "FA_OVERFLOW") {
                _31.failId = 4;
            } else {
                if (_34.code == "FA_COMPOSE_NOT_FOUND") {
                    _31.failId = 6;
                } else {
                    _31.failId = 3;
                }
            }
            return _31.failId != 3;
        };
        _33.cgi = _2;
        _33.simpleCall("upload:prepare", _32, function (_35) {
            _31.status = _8;
            _31.id = _35.attachmentId;
            var xhr;
            if (_5) {
                xhr = new XMLHttpRequest();
            } else {
                return;
            }
            xhr.upload.onprogress = function (_36) {
                _36 = _36 || _1.event;
                if (_36.lengthComputable) {
                    _31.bytesLoaded = _36.loaded + _31.offset;
                }
                _31.status = _9;
            };
            xhr.onreadystatechange = function (_37) {
                if (xhr.readyState == 4) {
                    xhr.onreadystatechange = xhr.upload.onprogress = null;
                    if (xhr.status == 200) {
                        var _38;
                        var _39 = xhr.responseXML;
                        try {
                            if (!_39) {
                                _39 = new _1.ActivexObject("MSXML2.DOMDocument");
                                _39.loadXML(xhr.responseText);
                            } else {
                                _39 = xhr.responseXML;
                            }
                            _38 = _39.getElementsByTagName("int")[2].childNodes[0].nodeValue;
                        }
                        catch (e) {
                            _33.simpleCall("upload:prepare", _32, function (_3a) {
                                _38 = _3a.actualSize;
                                _3b(parseInt(_38));
                            });
                            return;
                        }
                        _3b(parseInt(_38));
                    } else {
                        _31.status = _b;
                        _31.error = "upload failed: HTTP " + xhr.status + " " + xhr.statusText;
                    }
                }
                function _3b(_3c) {
                    if ((_31.bytesLoaded != undefined ? _3c == _31.bytesLoaded : false) || (_31.size != undefined ? _3c == _31.size : false)) {
                        _31.status = _a;
                        _31.bytesLoaded = _31.size;
                    }
                };
            };
            _31.XHR = xhr;
            var url = CMXClient.getURL("upload:directData", { attachmentId: _35.attachmentId, sid: sid, composeId: _3, offset: _31.offset });
            xhr.open("POST", url, true);
            xhr.setRequestHeader("content-type", "application/octet-stream");
            if (_31.blob) {
                xhr.send(_31.blob.slice(_31.offset, _31.blob.length - _31.offset));
            } else {
                var _3d = _31;
                if (_31.slice && typeof _31.slice == "function") {
                    _3d = _31.slice(_31.offset, _31.size);
                } else {
                    if (_31.webkitSlice && typeof _31.webkitSlice == "function") {
                        _3d = _31.webkitSlice(_31.offset, _31.size);
                    } else {
                        if (_31.mozSlice && typeof _31.mozSlice == "function") {
                            _3d = _31.mozSlice(_31.offset, _31.size);
                        } else {
                            alert("Upload File Blob init Error ,please update your Broswer!");
                            return;
                        }
                    }
                }
                xhr.send(_3d);
            }
        });
    };
};
function UploadBridge(win) {
    var _3e;
    var _3f;
    try {
        _3f = new DnDUploadManager(win);
    }
    catch (e) {
    }
    this.bdgSupportHTML5Upload = function isSupportHTML5Upload() {
        return _3f && _3f.isSupportHTML5();
    };
    this.bdgSupportPlugin = function isSupportPlugin(_40) {
        var _41 = PluginManager.isLoaded();
        if (_41 && _40) {
            _3f = null;
        }
        return _41;
    };
    this.bdgSupportCM_COM = function isSupportCM_COM(_42) {
        var _43 = _3e != null;
        if (_43 && _42) {
            _3f = null;
        }
        return _43;
    };
    this.bdgSupportDnDUpload = function isSupportDndUpload() {
        return _3f != null;
    };
    this.bdgGetFileSize = function (id) {
        if (_44(id)) {
            return _3f.getFileSize(id);
        }
        return null;
    };
    this.bdgInit = function initCompose(_45, _46) {
        if (_3f != null) {
            _3f.initUpload(_45);
        }
    };
    this.bdgInit2 = function initBigFile(fid, id) {
        if (_3f != null) {
            _3f.initUpload2(fid, id);
        }
    };
    this.bdgGetComposeId = function () {
        if (this.bdgSupportDnDUpload()) {
            return _3f.getComposeId();
        }
    };
    this.bdgAddNetfolderToContinue = function addNetFolderToContinue(_47, id, crc) {
        if (_44(id)) {
            return _3f.addNetFolderToContinue(_47, id, crc);
        } else {
            return _3e.addNetFolderToContinue(_47, id, crc);
        }
    };
    this.bdgMoveToNetFolder = function (id, _48, _49) {
        if (_44(id)) {
            return _3f.moveToNetFolder(id, _48, _49);
        }
    };
    this.bdgMoveToEtpNetFolder = function (id, _4a, _4b, _4c) {
        if (_44(id)) {
            _3f.setUrlParams(_4b);
            return _3f.moveToEtpNetFolder(id, _4a, _4c);
        }
    };
    this.bdgCancelUpload = function cancelUpload(id) {
        if (_44(id)) {
            _3f.cancelUpload(id);
        } else {
            PluginManager.cancelUpload(id);
        }
    };
    this.bdgRestoreState = function restoreState(id) {
        if (_44(id)) {
            _3f.restoreState(id);
            return true;
        } else {
            if (PluginManager.queryUploadItemStatus(id).state == PluginManager.UploadItemState_Type.Stopping || PluginManager.queryUploadItemStatus(id).state == PluginManager.UploadItemState_Type.Query) {
                return false;
            }
            PluginManager.startUpload(id);
            return true;
        }
    };
    this.bdgQueryStatus = function queryStatus(id) {
        if (_44(id)) {
            return _3f.queryStatus(id);
        } else {
            var obj = PluginManager.queryUploadItemStatus(id);
            return obj.state + "," + obj.percent + "," + obj.attachmentId + "," + obj.size;
        }
    };
    this.bdgGetUploadFailReason = function getUploadFailReason(id) {
        if (_44(id)) {
            return _3f.getUploadFailReason(id);
        } else {
            return PluginManager.queryUploadItemStatus(id).lastErrorNum;
        }
    };
    this.bdgGetServerReturnInfo = function getServerReturnInfo(id) {
        if (_44(id)) {
            return _3f.getServerReturnInfo(id);
        } else {
            return PluginManager.queryUploadItemStatus(id).lastErrorStr;
        }
    };
    this.bdgAddMutiDnDFile = function addMultiDnDFile(_4d) {
        if (this.bdgSupportDnDUpload()) {
            return _3f.addMultiDnDFile(_4d);
        }
    };
    this.bdgAddMutiFile = function addMultiFile(_4e) {
        if (this.bdgSupportDnDUpload()) {
            return _3f.addMultiFile(_4e);
        }
    };
    this.bdgCheckDnDFile = function checkDnDFile(_4f) {
        if (this.bdgSupportDnDUpload()) {
            return _3f.checkDnDFile(_4f);
        }
    };
    this.bdgGetUploadFileName = function getUploadFileName(id) {
        if (_44(id)) {
            return _3f.getUploadFileName(id);
        } else {
            return PluginManager.queryUploadItemStatus(parseInt(id)).name;
        }
    };
    this.bdgStartUpload = function startUpload(id, _50) {
        if (_44(id)) {
            _3f.setUrlParams(_50);
            _3f.startUpload(id);
        } else {
            PluginManager.startUpload(parseInt(id));
        }
    };
    this.bdgIsDndMgr = function (id) {
        return _44(id);
    };
    function _44(id) {
        var _51 = false;
        id = id + "";
        if (id.indexOf("g:") == 0) {
            _51 = true;
        }
        return _51;
    };
};

