var TrsUpload = function () {
    var _1 = navigator.userAgent.split(";")[0].split("(")[1] == "Macintosh";
    var _2 = 200;
    var _3 = { CALLBACK_FUNC: 0, TIMER: 1 };
    var _4 = ["NORMAL", "Input Param Error", gLang.compose.msg["msg_attachment_nomem"], "File Access Deny", gLang.compose.msg["msg_attachment_crcerr"], gLang.compose.msg["msg_attachment_neterr"], gLang.compose.msg["msg_attachment_noexist"], gLang.compose.msg["msg_attachment_excced_count"], gLang.compose.msg["msg_attachment_excced_size"], gLang.compose.msg["msg_attachment_syserr"]];
    var _5 = [];
    var _6 = [];
    var _7 = false;
    var _8 = false;
    var _9 = {
        beginUploadTime: null, beginUploadSize: null, leftTime: null, lastSpeed: null, finishAll: false, createNew: function (_a) {
            return Object.extend(_a, _9);
        }
    };
    return { UPDATE_TYPE: _3, STATUS_FAIL_REASON: _4, initTrsUpload: _b, resetTrsUpload: _c, addUploadFiles: _d, getUploadSuccessFiles: _e, updateFileState: _f, updateFilesState: _10, updateFileObjById: _11, updateFileStateCallBack: _12, cancelUpload: _13, resumeUpload: _14, restoreUpload: _15, updateUploadingParam: _16, queryStatesOfFiles: _17 };
    function _b(_18, _19, _1a, _1b, _1c) {
        var _1d = null;
        _8 = _1c || false;
        if (_19 == _3.CALLBACK_FUNC) {
            _1d = _12;
        } else {
            if (_19 == _3.TIMER) {
                _7 = true;
                _10();
            }
        }
        return PluginManager.initUpload(_18, _1a, _1d, _1b);
    };
    function _c() {
        _7 = false;
        _6 = [];
        for (var i = 0; i < _5.length; i++) {
            if (_5[i].state != PluginManager.UploadItemState_Type.Finish) {
                _13(_5[i].id);
            }
        }
        _5 = [];
    };
    function _d(_1e, _1f) {
        var ids = PluginManager.browserFolder(_1e);
        for (var i = 0; i < ids.length; ++i) {
            _20(parseInt(ids[i]), _1f);
        }
        PluginManager.startUploadSyn(ids);
        return ids;
    };
    function _e() {
        var _21 = [];
        for (var i = 0; i < _5.length; i++) {
            var _22 = [PluginManager.UploadItemState_Type.Finish, PluginManager.UploadItemState_Type.FinishWithoutCRC];
            for (var j = 0; j < _22.length; j++) {
                if (_22[j] == _5[i].state) {
                    _21[_21.length] = _5[i];
                    _5[i].name = _23(_5[i].name);
                    _5[i].mid = _5[i].attachmentId;
                    break;
                }
            }
        }
        return _21;
    };
    function _12(_24, _25, id) {
        _f(id);
    };
    function _11(id) {
        for (var i = 0; i < _5.length; i++) {
            if (_5[i].id == id) {
                var obj = PluginManager.queryUploadItemStatus(id, _5[i]);
                if (obj.state == PluginManager.UploadItemState_Type.Uploading) {
                    _16(_5[i]);
                }
                return obj;
            }
        }
        return null;
    };
    function _10() {
        for (var i = 0; i < _5.length; i++) {
            _f(_5[i].id);
        }
        if (_7) {
            setTimeout(_10, _2);
        }
    };
    function _f(id) {
        if (!PluginManager.isLoaded()) {
            return;
        }
        var obj = _11(id);
        if (!obj || !$win().$("ft_startdiv_" + id) || obj.finishAll) {
            return;
        }
        if (obj.state == PluginManager.UploadItemState_Type.Ready) {
            $win().$("ft_txProcessPausediv_" + id).style.display = "";
            $win().$("ft_processCountdiv_" + id).style.display = "";
            $win().$("ft_tProcessdiv_" + id).style.display = "none";
            $win().$("uploadProcessBardiv_" + id).style.width = (obj.percent || 0) + "%";
            $win().$("ft_processCountdiv_" + id).innerHTML = "";
        } else {
            if (obj.state == PluginManager.UploadItemState_Type.CRC) {
                $win().$("ft_txProcessPausediv_" + id).style.display = "";
                $win().$("ft_processCountdiv_" + id).style.display = "";
                $win().$("ft_tProcessdiv_" + id).style.display = "none";
                $win().$("ft_processCountdiv_" + id).innerHTML = gLang.compose.msg["uploadPrepare"] + obj.percent + "%";
                $win().$("cancel_bigFile_" + id).style.display = "";
                $win().$("resume_bigFile_" + id).style.display = "none";
            } else {
                if (obj.state == PluginManager.UploadItemState_Type.Uploading) {
                    $win().$("ft_tProcessdiv_" + id).style.display = "none";
                    $win().$("ft_txProcessPausediv_" + id).style.display = "";
                    $win().$("ft_processCountdiv_" + id).style.display = "";
                    $win().$("ft_processDetaildiv_" + id).style.display = "";
                    $win().$("uploadProcessBardiv_" + id).style.width = obj.percent + "%";
                    var _26 = parseInt(obj.size) * obj.percent / 100;
                    $win().$("ft_processCountdiv_" + id).innerHTML = obj.percent + "%";
                    $win().$("ft_processDetaildiv_" + id).innerHTML = gLang.compose.msg["uploadDetail"].replace("*upload*", _27(_26, 2)).replace("*speed*", _27(obj.lastSpeed, 2)).replace("*lefttime*", _28(obj.leftTime));
                    $win().$("cancel_bigFile_" + id).style.display = "";
                    $win().$("resume_bigFile_" + id).style.display = "none";
                } else {
                    if (obj.state == PluginManager.UploadItemState_Type.Finish || obj.state == PluginManager.UploadItemState_Type.FinishWithoutCRC) {
                        $win().$("ft_icodiv_" + id).style.display = "";
                        $win().$("ft_txProcessPausediv_" + id).style.display = "none";
                        $win().$("ft_processCountdiv_" + id).style.display = "none";
                        $win().$("ft_processDetaildiv_" + id).style.display = "none";
                        $win().$("ft_tProcessdiv_" + id).style.display = "";
                        $win().$("cancel_bigFile_" + id).style.display = "none";
                        $win().$("resume_bigFile_" + id).style.display = "none";
                        $win().$("ft_tProcessdiv_" + id).innerHTML = gLang.compose.msg["uploadFinish"];
                        if (_8) {
                            $win().$("ft_tProcessdiv_" + id).innerHTML = gLang.nf.att["finish_rename"] + "<input type='hidden' id='renameId_bigFile_" + id + "' value='" + obj.attachmentId + "'/>" + "<input type='text' class='rename_bigFile' id='rename_bigFile_" + id + "' value='" + _23(obj.name).htmlencode() + "'/>";
                        }
                        obj.finishAll = true;
                    } else {
                        if (obj.state == PluginManager.UploadItemState_Type.Stopping || (obj.state == PluginManager.UploadItemState_Type.Error && obj.lastErrorNum == PluginManager.UploadItemError_Type.Interrupt_Error)) {
                            $win().$("cancel_bigFile_" + id).style.display = "none";
                            $win().$("ft_processDetaildiv_" + id).style.display = "none";
                            $win().$("resume_bigFile_" + id).style.display = "";
                        } else {
                            if (obj.state == PluginManager.UploadItemState_Type.Error) {
                                var _29 = TrsUpload.STATUS_FAIL_REASON[obj.lastErrorNum];
                                if (!_29) {
                                    _29 = obj.lastErrorStr;
                                }
                                $win().$("ft_txProcessPausediv_" + id).style.display = "none";
                                $win().$("ft_processCountdiv_" + id).style.display = "none";
                                $win().$("ft_processDetaildiv_" + id).style.display = "none";
                                $win().$("ft_tProcessdiv_" + id).style.display = "";
                                $win().$("ft_tProcessdiv_" + id).innerHTML = _29;
                                $win().$("cancel_bigFile_" + id).style.display = "none";
                            }
                        }
                    }
                }
            }
        }
    };
    function _20(id, _2a) {
        var _2b = _9.createNew(PluginManager.queryUploadItemStatus(id));
        var _2c = _2b.name;
        if (_2c != null && _2c != "") {
            if (_6[_2c]) {
                UI.alert(gLang.compose.msg["msg_attach_exist"]);
            } else {
                var _2d = _5.length;
                _5[_5.length] = _2b;
                _6[_2c] = _2d;
                var El = $doc();
                var _2e = El.createElement("div");
                _2e.setAttribute("id", "ft_startdiv_" + id);
                _2e.className = "ft_start";
                var _2f = El.createElement("div");
                _2f.setAttribute("id", "ft_icodiv_" + id);
                _2f.className = "ft_ico";
                _2f.style.display = "none";
                _2f.innerHTML = "<div class=\"ico_upload_success\"></div>";
                var _30 = El.createElement("div");
                _30.className = "ft_main";
                var _31 = El.createElement("div");
                _31.className = "ft_top";
                var _32 = El.createElement("div");
                _32.className = "ft_name";
                _32.innerHTML = _23(_2c).escapeHTML();
                _31.appendChild(_32);
                var _33 = El.createElement("div");
                _33.className = "ft_content";
                var _34 = El.createElement("div");
                _34.className = "ft_tProcess";
                _34.setAttribute("id", "ft_tProcessdiv_" + id);
                _34.style.display = "none";
                var _35 = El.createElement("div");
                _35.setAttribute("id", "ft_txProcessPausediv_" + id);
                _35.className = "ft_txProcessPause";
                var _36 = El.createElement("div");
                _36.setAttribute("id", "uploadProcessBardiv_" + id);
                _36.className = "uploadProcessBar";
                _36.style.width = "0";
                _35.appendChild(_36);
                var _37 = El.createElement("div");
                _37.setAttribute("id", "operatordiv_" + id);
                _37.className = "ft_tOperator trs_operator trs_line_height";
                _37.appendChild(_38(id));
                _37.appendChild(_39(id));
                var _3a = El.createElement("div");
                _3a.className = "ft_processCount trs_line_height trs_processCount";
                _3a.setAttribute("id", "ft_processCountdiv_" + id);
                _33.appendChild(_35);
                _33.appendChild(_37);
                _33.appendChild(_3a);
                _33.appendChild(_34);
                var _3b = El.createElement("div");
                _3b.setAttribute("id", "ft_processDetaildiv_" + id);
                _3b.innerHTML = "";
                _3b.className = "ft_bottom trs_bottom";
                _30.appendChild(_31);
                _30.appendChild(_33);
                _30.appendChild(_3b);
                _2e.appendChild(_2f);
                _2e.appendChild(_30);
                _2a.appendChild(_2e);
            }
        } else {
            alert("Illegal file name!");
        }
    };
    function _38(id) {
        var _3c = Object.extend($doc().createElement("a"), {
            id: "cancel_bigFile_" + id, className: "btl", innerHTML: gLang.compose.att["att_upload_cancel"], onclick: function () {
                TrsUpload.cancelUpload(id);
            }
        });
        _3c.style.display = "none";
        return _3c;
    };
    function _39(id) {
        var _3d = Object.extend($doc().createElement("a"), {
            id: "resume_bigFile_" + id, className: "btl", innerHTML: gLang.compose.att["att_upload_resume"], onclick: function () {
                TrsUpload.resumeUpload(id);
            }
        });
        _3d.style.display = "none";
        return _3d;
    };
    function _13(id) {
        for (var i = 0; i < _5.length; i++) {
            if (_5[i].id == id) {
                PluginManager.cancelUpload(id);
                var _3e = $win().$("uploadProcessBardiv_" + id);
                if (_3e) {
                    _3e.className = "uploadProcessBarNotActive";
                    _f(i);
                }
                _5[i].beginUploadTime = null;
                _5[i].beginUploadSize = null;
                break;
            }
        }
    };
    function _14(id) {
        for (var i = 0; i < _5.length; i++) {
            if (_5[i].id == id) {
                if (_5[i].state == PluginManager.UploadItemState_Type.Stopping) {
                    UI.alert(gLang.compose.msg["msg_opsTooFrequent"]);
                } else {
                    var _3f = $win().$("uploadProcessBardiv_" + id);
                    _3f.className = "uploadProcessBar";
                    PluginManager.startUpload(id);
                    _f(i);
                }
                break;
            }
        }
    };
    function _15(_40, _41, _42, _43) {
        var ids = PluginManager.resumeUpload(_40, _41, _42);
        for (var i = 0; i < ids.length; ++i) {
            _20(parseInt(ids[i]), _43);
            PluginManager.startUpload(parseInt(ids[i]));
        }
    };
    function _16(obj) {
        var _44 = new Date().getTime();
        var _45 = parseInt(obj.size) * obj.percent / 100;
        obj.size = parseInt(obj.size) || 0;
        obj.percent = obj.percent || 0;
        obj.beginUploadSize = obj.beginUploadSize || _45;
        obj.beginUploadTime = obj.beginUploadTime || _44;
        obj.lastSpeed = obj.lastSpeed || 0;
        obj.leftTime = obj.leftTime || 0;
        if (obj.state == PluginManager.UploadItemState_Type.Uploading) {
            var _46 = _44 - obj.beginUploadTime;
            obj.lastSpeed = (_45 - obj.beginUploadSize) * 1000 / _46;
            if (obj.lastSpeed > 0) {
                obj.leftTime = (obj.size - _45) / obj.lastSpeed;
            }
        }
        obj.leftTime = obj.leftTime * 10;
        obj.leftTime = Math.round(obj.leftTime);
        obj.leftTime = obj.leftTime / 10;
        return obj;
    };
    function _17(_47) {
        for (var i = 0; i < _47.length; i++) {
            for (var j = 0; j < _5.length; j++) {
                if (_5[j].state == _47[i]) {
                    return true;
                }
            }
        }
        return false;
    };
    function _23(_48) {
        var _49 = -1;
        if (_1) {
            _49 = _48.lastIndexOf("/");
        } else {
            _49 = _48.lastIndexOf("\\");
        }
        if (_49 == -1) {
            return _48;
        } else {
            return _48.substring(_49 + 1);
        }
    };
    function _27(_4a, n) {
        var p = 1;
        if (n && n > 0) {
            p = 10 * n;
        }
        if (_4a < 1024) {
            _4a = _4a * p;
            _4a = Math.round(_4a);
            _4a = _4a / p;
            return _4a + "B";
        } else {
            if (_4a < 1024 * 1024) {
                _4a = _4a / 1024;
                _4a = _4a * p;
                _4a = Math.round(_4a);
                _4a = _4a / p;
                return _4a + "K";
            } else {
                _4a = _4a / (1024 * 1024);
                _4a = _4a * p;
                _4a = Math.round(_4a);
                _4a = _4a / p;
                return _4a + "M";
            }
        }
    };
    function _28(_4b) {
        var _4c = Math.floor(_4b / (60 * 60));
        var _4d = Math.floor((_4b - _4c * 60 * 60) / 60);
        var _4e = Math.round(_4b - _4c * 60 * 60 - _4d * 60);
        var _4f = "";
        if (_4c > 9) {
            _4f += _4c + ":";
        } else {
            _4f += "0" + _4c + ":";
        }
        if (_4d > 9) {
            _4f += _4d + ":";
        } else {
            _4f += "0" + _4d + ":";
        }
        if (_4e > 9) {
            _4f += _4e;
        } else {
            _4f += "0" + _4e;
        }
        return _4f;
    };
} ();

