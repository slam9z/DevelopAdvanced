var CMUpload = {};
(function () {
    var _1 = [];
    var _2 = {};
    var _3 = $win().$;
    var _4 = false;
    var _5 = "";
    var _6 = new UploadBridge();
    var _7 = !!document.all || !!window.ActiveXObject || "ActiveXObject" in window;
    var _8 = !(window.ActiveXObject) && "ActiveXObject" in window;
    var _9 = navigator.userAgent.split(";")[0].split("(")[1] == "Macintosh";
    var _a = $H();
    var _b = {
        objs: [], setFileName: {}, curIndex: -1, uploadFailIds: [], timerID: null, STATUS_WAITTING: 0, STATUS_PREPARE_UPLOAD: 1, STATUS_UPLOADING: 2, STATUS_UPLOAD_SUCCESS: 3, STATUS_UPLOAD_FAIL: 4, STATUS_CANCEL: 5, STATUS_AUTO_TRS_UPLOAD: 6, STATUS_QUERY: 999, STATUS_FAIL_REASON: ["NORMAL", "RUNNING", gLang.compose.msg["msg_attachment_nomem"], gLang.compose.msg["msg_attachment_syserr"], gLang.compose.msg["msg_attachment_excced_size"], gLang.compose.msg["msg_attachment_excced_count"], gLang.compose.msg["msg_attachment_noexist"], gLang.compose.msg["msg_attachment_neterr"], gLang.compose.msg["msg_attachment_client_notfound"], gLang.compose.msg["msg_attachment_crcerr"]], isPeddingStatus: function (s) {
            return s == _b.STATUS_WAITTING || s == _b.STATUS_PREPARE_UPLOAD || s == _b.STATUS_QUERY || s == _b.STATUS_UPLOADING;
        }
    };
    CMUpload.submitDataByCoreMailPlugin = _c;
    CMUpload.DndSubmitData = _d;
    CMUpload.SubmitMultiData = _e;
    CMUpload.ListenDndEventbyId = _f;
    CMUpload.closeDnDDisplay = _10;
    CMUpload.showDnDDisplay = _11;
    CMUpload.CMCheckSecurityLevel = _12;
    CMUpload.CMCancelUpload = _13;
    CMUpload.CMRestoreState = _14;
    CMUpload.CMHasCOM = _15;
    CMUpload.CMHasDND = _16;
    CMUpload.setRisky = _17;
    CMUpload.setPrepareUploadCheckUrlParams = _18;
    CMUpload.supportClickFile = _19;
    CMUpload.getDescription = _1a;
    CMUpload.generateDesc = _1b;
    CMUpload.generateDescN = _1c;
    CMUpload.updateFailReasonForN = _1d;
    CMUpload.unGenerateDescForN = _1e;
    CMUpload.queryDisplayId = _1f;
    CMUpload.hasFileUploading = _20;
    CMUpload.delAttachment = _21;
    function _20() {
        var i;
        for (i = 0; i < _b.objs.length; i++) {
            if (_b.isPeddingStatus(_b.objs[i].status)) {
                return true;
            }
        }
        for (i = 0; i < _22.objs.length; i++) {
            if (_23(_22.objs[i].state)) {
                return true;
            }
        }
        return false;
    };
    function _21() {
        _b.objs = [];
        _b.setFileName = {};
        _b.curIndex = -1;
        jQ("#divAttachInfo").empty();
        jQ("#divAttach").empty();
    };
    function _19() {
        var _24 = window.navigator.userAgent;
        var _25 = false;
        var _26 = "";
        if (_24.toUpperCase().indexOf("FIREFOX") > -1) {
            _25 = true;
            _26 = _24.replace(/.+Firefox\//gi, "").replace(/\(.*\)/g, "");
        }
        return !_25 || (_26.match(/\d+.\d+/) >= 4);
    };
    function _17(_27) {
        _1 = _27;
    };
    function _18(_28) {
        _2 = _28;
    };
    function _15() {
        return _6.bdgSupportPlugin();
    };
    function _16() {
        return _6.bdgSupportHTML5Upload();
    };
    function _12(_29) {
        _29 = _1a(_29).trim();
        var _2a = [];
        var msg = gLang.compose.msg;
        for (var i = 0; msg["msg_mail_perm_pattern_" + (i + 1)]; i++) {
            var _2b = msg["msg_mail_perm_pattern_" + (i + 1)];
            _2a[i] = new RegExp("^(\\[(" + _2b + ")\\]|\\u3010(" + _2b + ")\\u3011)", "ig");
        }
        var _2c = _29;
        var _2d = 0;
        if (C.securityLevel > 1) {
            for (var i = 0, _2e = _2a.length; i < _2e; i++) {
                if (_2a[i].test(_29)) {
                    _2c = _29.replace(_2a[i], "");
                    _2d = i + 1;
                    break;
                }
            }
            if (_2d > C.securityLevel) {
                _2d = C.securityLevel;
            }
        }
        return { name: _2c, level: _2d };
    };
    function _2f(loc) {
        var _30 = loc.hostname;
        var _31 = loc.port;
        if (_30.indexOf(":") != -1 && _30.indexOf("[") == -1) {
            _30 = "[" + _30 + "]";
        }
        var _32 = _30;
        if (_31 != null && _31 != "") {
            _32 = _32 + ":" + _31;
        }
        return loc.protocol + "//" + _32;
    };
    function $(_33) {
        return document.getElementById(_33);
    };
    function _34(_35) {
        var id = _b.objs[_35].id;
        var _36 = Object.extend(document.createElement("span"), { id: "upSize_" + id });
        jQ(_36).addClass("attachSize");
        return _36;
    };
    function _37(_38) {
        var id = _b.objs[_38].id;
        var _39 = Object.extend(document.createElement("a"), {
            id: "cancel_" + id, className: "btl", innerHTML: gLang.compose.att["att_upload_cancel"], onclick: function () {
                CMUpload.CMCancelUpload(id);
            }
        });
        _39.style.display = "none";
        return _39;
    };
    function _3a(_3b) {
        var id = _b.objs[_3b].id;
        var _3c = Object.extend(document.createElement("a"), {
            id: "resume_" + id, className: "btl", innerHTML: gLang.compose.att["att_upload_resume"], onclick: function () {
                _3d(id, false);
                CMUpload.CMRestoreState(id);
            }
        });
        _3c.style.display = "none";
        return _3c;
    };
    function _3e(_3f, _40) {
        var id = _b.objs[_3f].id;
        var _41 = Object.extend(document.createElement("a"), {
            id: "delete_" + id, className: "btl", innerHTML: gLang.compose.att["att_upload_delete"], onclick: function () {
                _3d(id, true);
            }
        });
        if (!_40) {
            _41.style.display = "none";
        }
        return _41;
    };
    function _42(_43) {
        var id = _b.objs[_43].id;
        var _44 = Object.extend(document.createElement("a"), {
            id: "undelete_" + id, className: "btl", innerHTML: gLang.compose.att["att_upload_undelete"], onclick: function () {
                _3d(id, false);
            }
        });
        _44.style.display = "none";
        return _44;
    };
    function _45(id, _46, mid) {
        var mid = mid || "";
        var _47 = -1;
        for (var i = 0, _48 = _b.objs.length; i < _48; i++) {
            if (_b.objs[i].id === id) {
                _47 = i;
                break;
            }
        }
        var _49 = Object.extend(document.createElement("a"), {
            id: "download_" + id, className: "btl", innerHTML: gLang.compose.att["att_download"], onclick: function () {
                var _4a = -1;
                for (var i = 0, _48 = _b.objs.length; i < _48; i++) {
                    if (_b.objs[i].id === id) {
                        _4a = i;
                        break;
                    }
                }
                var _4b = _b.objs[_4a].attributeID;
                var _4c = _b.objs[_4a].enfUid;
                C.downloadAttach(_4b, mid, _4c);
                return false;
            }, href: "#"
        });
        if (!_46) {
            _49.style.display = "none";
        }
        return _49;
    };
    function _4d(_4e, _4f) {
        var id = _b.objs[_4e].id;
        var _50 = Object.extend(document.createElement("a"), {
            id: "edit_" + id, className: "btl", innerHTML: gLang.compose.att["att_edit"], onclick: function () {
                var _51 = _b.objs[_4e].attributeID;
                C.editAttach(id, true);
                return false;
            }, href: "#"
        });
        var _52 = _b.objs[_4e].name;
        if (C.getIsOpenAttachmentEdit(_52)) {
            _50.style.display = "";
        } else {
            _50.style.display = "none";
        }
        return _50;
    };
    function _53(_54, _55) {
        var id = _b.objs[_54].id;
        var _56 = { "Items": [{ "name": gLang.compose.msg["msg_mail_perm_1"], "topid": "0", "colid": "2", "value": "1", "fun": _57 }, { "name": gLang.compose.msg["msg_mail_perm_2"], "topid": "0", "colid": "3", "value": "2", "fun": _57 }, { "name": gLang.compose.msg["msg_mail_perm_3"], "topid": "0", "colid": "4", "value": "3", "fun": _57 }, { "name": gLang.compose.msg["msg_mail_perm_4"], "topid": "0", "colid": "5", "value": "4", "fun": _57 }, { "name": gLang.compose.msg["msg_mail_perm_5"], "topid": "0", "colid": "6", "value": "5", "fun": _57 }] };
        if (_55 > 0) {
            jQ($("colsel_security_" + id)).mlnColsel(_56, { title: gLang.compose.msg["msg_mail_perm_title0"], value: _55 + "", width: 90, bCheckType: true });
        } else {
            jQ($("colsel_security_" + id)).mlnColsel(_56, { title: gLang.compose.msg["msg_mail_perm_title0"], value: "-1", width: 90, bCheckType: true });
        }
        function _57(_58) {
            C.setAttachSecurityLevel(_b.objs[_54].attributeID, _58);
        };
    };
    function _3d(id, _59, _5a) {
        _5a = _5a || false;
        with (_b) {
            var _5b = -1;
            for (var i = 0, _5c = objs.length; i < _5c; i++) {
                if (objs[i].id === id) {
                    _5b = i;
                    break;
                }
            }
            var id = objs[_5b].id;
            objs[_5b].deleted = _59;
            AttachInfo.setDeleted(objs[_5b].attributeID + "", _59, _5a);
            updateSizeInfo();
            if (!_59) {
                if (AttachInfo.getValidSize() > C.getMailLimitSize()) {
                    UI.alert(gLang.compose.msg["msg_attachment_excced_size"]);
                    objs[_5b].deleted = !_59;
                    AttachInfo.setDeleted(objs[_5b].attributeID + "", !_59, false);
                    updateSizeInfo();
                    return;
                }
            }
            if ($("displayName_" + id)) {
                $("displayName_" + id).style.textDecoration = _59 ? "line-through" : "";
            }
            Element[_59 ? "hide" : "show"]($("delete_" + id));
            Element[_59 ? "show" : "hide"]($("undelete_" + id));
            if (objs[_5b].status != STATUS_UPLOAD_SUCCESS) {
                if (_59) {
                    _a[id] = "1";
                } else {
                    _a[id] = "0";
                }
                if (_5d()) {
                    jQ("#btnSend").removeClass("disabled");
                    jQ("#btnSend2").removeClass("disabled");
                    if ($("aTimeSet")) {
                        $("aTimeSet").style.display = "";
                    }
                    Element[_59 ? "hide" : "show"]($("download_" + id));
                    if ($("colsel_security_" + id)) {
                        Element[_59 ? "hide" : "show"]($("colsel_security_" + id));
                    }
                } else {
                    jQ("#btnSend").addClass("disabled");
                    jQ("#btnSend2").addClass("disabled");
                    if ($("aTimeSet")) {
                        $("aTimeSet").style.display = "none";
                    }
                }
            }
        }
    };
    function _5e(id, _5f, _60) {
        var _61 = $("upSize_" + id);
        if (_61) {
            var _62 = gLang.compose.att.att_upload_size;
            _62 = _62.replace("{0}", _5f).replace("{1}", _60);
            jQ(_61).show();
            jQ(_61).empty().html(_62);
        }
    };
    function _5d(_63) {
        var _64 = true;
        _a._each(function (_65) {
            if (_65.value == "0") {
                _64 = false;
            }
        });
        if (_63) {
            if (_22.objs.length > 0) {
                for (var i = 0; i < _22.objs.length; ++i) {
                    var obj = _22.objs[i];
                    if (obj.state && _23(obj.state)) {
                        _64 = false;
                        break;
                    }
                }
            }
        }
        return _64;
    };
    function _13(id) {
        with (_b) {
            var len = objs.length;
            for (var i = 0; i < len; ++i) {
                if (objs[i].id == id) {
                    _6.bdgCancelUpload(id);
                    var _66 = !_6.bdgIsDndMgr(id);
                    if (_66) {
                        objs[i].status = PluginManager.queryUploadItemStatus(i).state;
                    }
                    _67(i, !_6.bdgIsDndMgr(id));
                    break;
                }
            }
        }
    };
    function _14(id) {
        var _68 = $("composeIDForCMUpload").value;
        var _69 = AttachInfo.getUploadSuccessObjs();
        new CMXClient().simpleCall("mbox:compose", { id: _68, action: "continue", attrs: { id: _68, attachments: _69 } }, function () {
            with (_b) {
                var len = objs.length;
                var _6a = !_6.bdgIsDndMgr(id);
                for (var i = 0; i < len; ++i) {
                    if (objs[i].id == id) {
                        if (_6.bdgRestoreState(id)) {
                            var st = _6.bdgQueryStatus(id).split(",");
                            objs[i].status = parseInt(st[0], 10);
                            objs[i].percent = parseInt(st[1], 10);
                            if (st[4]) {
                                objs[i].uploaded = parseInt(st[4], 10);
                            }
                            _6a = !_6.bdgIsDndMgr(id);
                            _67(i, _6a);
                        } else {
                            UI.alert(gLang.compose.msg["msg_opsTooFrequent"]);
                        }
                        break;
                    }
                }
                if (timerID == null) {
                    _6b(_6a);
                }
            }
        });
    };
    function _6c(_6d) {
        var obj = _b.objs[_6d];
        switch (obj.status) {
            case PluginManager.UploadItemState_Type.Ready:
                obj.status = _b.STATUS_WAITTING;
                break;
            case PluginManager.UploadItemState_Type.CRC:
                obj.status = _b.STATUS_PREPARE_UPLOAD;
                break;
            case PluginManager.UploadItemState_Type.Query:
                obj.status = _b.STATUS_QUERY;
                break;
            case PluginManager.UploadItemState_Type.Uploading:
                obj.status = _b.STATUS_UPLOADING;
                break;
            case PluginManager.UploadItemState_Type.Finish:
                obj.status = _b.STATUS_UPLOAD_SUCCESS;
                break;
            case PluginManager.UploadItemState_Type.FinishWithoutCRC:
                obj.status = _b.STATUS_UPLOAD_SUCCESS;
                break;
            case PluginManager.UploadItemState_Type.Error:
                obj.status = _b.STATUS_UPLOAD_FAIL;
                break;
            case PluginManager.UploadItemState_Type.Stopping:
                obj.status = _b.STATUS_CANCEL;
                break;
        }
    };
    function _67(_6e, _6f) {
        _6f = _6f || false;
        var obj = _b.objs[_6e];
        if (_6f) {
            _6c(_6e);
        }
        var id = obj.id;
        if (obj.status == _b.STATUS_WAITTING) {
            if (!$("status_" + id)) {
                $("css_status_" + id).innerHTML = "<div class='comcapacityBar inlineb'>" + "<div id='status_" + id + "' class='comcapacityBarFont inlineb' style='width:0%'>" + "</div></div>";
            }
            $("status_" + id).style.width = (obj.percent || 0) + "%";
            $("status_" + id).innerHTML = (obj.percent || 0) + "%";
        } else {
            if (obj.status == _b.STATUS_PREPARE_UPLOAD) {
                $("css_status_" + id).className = "capacity inlineb";
                $("css_status_" + id).innerHTML = "&nbsp;" + gLang.compose.msg.msg_attach_upload_scanning + (obj.percent || 0) + "%";
                _70({ cancel_: "none", delete_: "none", undelete_: "none", resume_: "none", download_: "none" }, id);
                _70({ colsel_security_: "none" }, id, true);
            } else {
                if (obj.status == _b.STATUS_QUERY && _6f) {
                } else {
                    if (obj.status == _b.STATUS_UPLOADING) {
                        if (!$("status_" + id)) {
                            $("css_status_" + id).innerHTML = "<div class='comcapacityBar inlineb'>" + "<div id='status_" + id + "' class='comcapacityBarFont inlineb' style='width:0%'>" + "</div></div>";
                        }
                        $("status_" + id).style.width = obj.percent + "%";
                        $("status_" + id).innerHTML = obj.percent + "%";
                        _70({ cancel_: "", delete_: "none", undelete_: "none", resume_: "none", download_: "none" }, id);
                        _70({ colsel_security_: "none" }, id, true);
                        _5e(id, sizeAutoFormat(obj.size * obj.percent / 100), sizeAutoFormat(obj.size));
                    } else {
                        if (obj.status == _b.STATUS_UPLOAD_SUCCESS) {
                            _a[id] = "1";
                            _71(id);
                            if (obj.isCapture) {
                                var _72 = $("divAttachDisplay_" + id);
                                jQ(_72).remove();
                                var _73 = obj.identifier;
                                if (_73) {
                                    var _74 = window.location.search.substring(1).toQueryParams();
                                    var sid = _74["sid"];
                                    var _75 = $("composeIDForCMUpload").value;
                                    var src = "/coremail/s?func=mbox:getComposeData&sid=" + sid + "&composeId=" + _75 + "&attachId=" + obj.attributeID;
                                    C.parseHTMLForUpdateTheImgNode(_73, src);
                                }
                            }
                            _5e(id, sizeAutoFormat(obj.size), sizeAutoFormat(obj.size));
                            updateSizeInfo();
                        } else {
                            if (obj.status == _b.STATUS_UPLOAD_FAIL) {
                                var _76 = _6.bdgGetUploadFailReason(id);
                                var _77 = "";
                                if (_6f) {
                                    if (_4 && _76 == 8) {
                                        _b.objs.splice(_6e, 1);
                                        _78(parseInt(id));
                                        _79(id);
                                    } else {
                                        _77 = TrsUpload.STATUS_FAIL_REASON[_76];
                                        _7a(id, _6e, _77);
                                    }
                                } else {
                                    if (_76 == 3 || _76 == 4) {
                                        var _7b = _6.bdgGetServerReturnInfo(id);
                                        _77 = _7c(_7b);
                                    }
                                    if (!_77) {
                                        _77 = _b.STATUS_FAIL_REASON[_76];
                                    }
                                    _7a(id, _6e, _77);
                                    if (_4 && _76 == 4) {
                                        if (_6.bdgIsDndMgr(id)) {
                                            jQ("#upsupportDndTips").show();
                                        } else {
                                            UI.alert(gLang.compose.page["add_bigfile_no_activeX"]);
                                        }
                                    }
                                }
                            } else {
                                if ($("divAttachDisplay_" + id) || $("attachmentByActiveX_" + id)) {
                                    _70({ delete_: "", undelete_: "none", cancel_: "none", resume_: "", download_: "none" }, id);
                                    _70({ colsel_security_: "none" }, id, true);
                                }
                            }
                        }
                    }
                }
            }
        }
        _7d(_5d(_4));
    };
    function _7e() {
        if ($("installPluginTips")) {
            $("installPluginTips").style.display = "";
        }
    };
    function _7d(_7f) {
        if (_7f) {
            jQ("#btnSend").removeClass("disabled");
            jQ("#btnSend2").removeClass("disabled");
            if ($("aTimeSet")) {
                $("aTimeSet").style.display = "";
            }
        } else {
            jQ("#btnSend").addClass("disabled");
            jQ("#btnSend2").addClass("disabled");
            if ($("aTimeSet")) {
                $("aTimeSet").style.display = "none";
            }
        }
    };
    function _71(id, _80) {
        if (!$("status_" + id)) {
            $("css_status_" + id).innerHTML = "<div class='comcapacityBar inlineb'>" + "<div id='status_" + id + "' class='comcapacityBarFont inlineb' style='width:0%'>" + "</div></div>";
        }
        $("status_" + id).style.width = "100%";
        $("status_" + id).innerHTML = gLang.compose.msg["msg_attach_upload_successInfo"];
        _70({ cancel_: "none", delete_: "", undelete_: "none", resume_: "none", css_status_: "none", Upload_Success_: "" }, id);
        if (_80) {
            _70({ download_: "none" }, id);
        } else {
            _70({ download_: "" }, id);
        }
        _70({ colsel_security_: "" }, id, true);
    };
    function _7a(id, _81, _82) {
        _83(id, _82);
        _3d(id, true);
    };
    function _83(id, _84, _85) {
        if (!$("status_" + id)) {
            $("css_status_" + id).innerHTML = "<div class='comcapacityBar inlineb'>" + "<div id='status_" + id + "' class='comcapacityBarFont inlineb' style='width:0%'>" + "</div></div>";
        }
        $("css_status_" + id).className = "capacity warning inlineb";
        $("status_" + id).style.width = "100%";
        $("status_" + id).innerHTML = _84;
        _70({ cancel_: "none", delete_: _85 ? "none" : "", undelete_: "none", resume_: "none", download_: "none" }, id);
        if ($("colsel_security_" + id)) {
            _70({ colsel_security_: "none" }, id);
        }
        $("css_status_" + id).title = _84;
    };
    function _6b(_86) {
        _86 = _86 || false;
        with (_b) {
            var len = objs.length;
            if (curIndex == len || curIndex == -1 || !isPeddingStatus(objs[curIndex].status)) {
                for (curIndex = 0; curIndex < len; ++curIndex) {
                    if (isPeddingStatus(objs[curIndex].status)) {
                        break;
                    }
                }
                if (curIndex == len) {
                    timerID = null;
                    if (_4 && _b.uploadFailIds.length > 0) {
                        _87();
                    }
                    return;
                }
            }
            var id = objs[curIndex].id;
            if (objs[curIndex].status == STATUS_WAITTING) {
                _6.bdgStartUpload(id);
                objs[curIndex].status = STATUS_PREPARE_UPLOAD;
            } else {
                var st = _6.bdgQueryStatus(id).split(",");
                objs[curIndex].status = parseInt(st[0], 10);
                objs[curIndex].percent = parseInt(st[1], 10);
                objs[curIndex].uploaded = parseInt(st[4], 10);
                if (st.length > 2) {
                    objs[curIndex].attributeID = st[2];
                }
                if (st.length > 3) {
                    objs[curIndex].size = st[3];
                }
                if (!objs[curIndex].isloaded && objs[curIndex].attributeID != null && objs[curIndex].attributeID != "") {
                    var _88 = false;
                    if (objs[curIndex].isCapture) {
                        _88 = true;
                    }
                    var _89 = { id: parseInt(objs[curIndex].attributeID, 10), type: "upload", deleted: false, inlined: _88, size: objs[curIndex].size != null ? parseInt(objs[curIndex].size, 10) : 0, fileName: objs[curIndex].name };
                    if (objs[curIndex].size != null) {
                        _5e(id, "0", sizeAutoFormat(objs[curIndex].size));
                    }
                    if (!objs[curIndex].isCapture) {
                        AttachInfo.addAttachInfo(objs[curIndex].attributeID, _1a(objs[curIndex].name), "upload", false, null, objs[curIndex].size, null);
                        AttachInfo.setSecurityLevel(objs[curIndex].attributeID + "", "0", _1a(objs[curIndex].name));
                        C.setAttachSecurityLevel(id, CMUpload.CMCheckSecurityLevel(objs[curIndex].name).level);
                    }
                    objs[curIndex].isloaded = true;
                    objs[curIndex].deleted = false;
                }
            }
            _67(curIndex, _86);
            timerID = setTimeout(function () {
                _6b(_86);
            }, 100);
        }
    };
    function _8a(id, _8b, _8c) {
        var _8d;
        if (_8c && _8c == PluginManager.Submit_Type.ScreenCapture) {
            _8d = true;
        }
        _8b = _8b || false;
        var _8e = "";
        if (_8d) {
            _8e = self.frames["htmleditor"].fAddImgBeforeCaptureUploaded();
        }
        var _8f = _1a(_6.bdgGetUploadFileName(id));
        if (_90(_8f)) {
            _8f = fileNameSoLong(_8f, 30);
            UI.alert(gLang.compose.msg["msg_risky_local_attach"].replace("${filename}", _8f));
            return false;
        }
        var _91;
        if (_8f != null && _8f != "") {
            if (_b.setFileName[_8f] != null) {
                var _92 = _b.setFileName[_8f];
                var obj = _b.objs[_92];
                _91 = gLang.compose.msg["msg_attach_exist_confirm"].replace("{attach_name}", _8f);
                if (obj && (obj.status == _b.STATUS_UPLOAD_SUCCESS || obj.status == _b.STATUS_UPLOAD_FAIL)) {
                    UI.confirm({
                        message: _91, yes: function () {
                            var _93 = obj.attributeID;
                            var _94 = obj.id;
                            new CMXClient().simpleCall("upload:deleteTasks", { composeId: $("composeIDForCMUpload").value, item: [_8f] }, function () {
                                _3d(_94, true, true);
                                var _95 = $("divAttachDisplay_" + _94);
                                if (_95) {
                                    _95.parentNode.removeChild(_95);
                                }
                                C.handleDisplayOfAttach();
                                C.autoCompleteSubject(_8f);
                                _b.setFileName[_8f] = null;
                                _9b(id, _8f, _8b, _8c);
                            });
                        }, no: function () {
                            var obj = {};
                            var _96 = _b.objs.length;
                            _b.objs[_96] = obj;
                        }
                    });
                } else {
                    UI.alert(gLang.compose.msg["msg_attach_exist"]);
                }
            } else {
                if (_4 && _22.setFileName[_8f] != null) {
                    var _92 = _22.setFileName[_8f];
                    var obj = _22.objs[_92];
                    _91 = gLang.compose.msg["msg_attach_exist_confirm"].replace("{attach_name}", _8f);
                    if (obj && !_23(obj.state)) {
                        UI.confirm({
                            message: _91, yes: function () {
                                var _97 = obj.attributeID;
                                var _98 = obj.id;
                                _14b(_92, true, true);
                                var _99 = $("divAttachDisplay_" + _98);
                                if (_99) {
                                    _99.parentNode.removeChild(_99);
                                }
                                _9b(id, _8f, _8b, _8c, _8e);
                            }, no: function () {
                                var obj = {};
                                var _9a = _22.objs.length;
                                _22.objs[_9a] = obj;
                            }
                        });
                    } else {
                        UI.alert(gLang.compose.msg["msg_attach_exist"]);
                    }
                } else {
                    C.autoCompleteSubject(_8f);
                    _9b(id, _8f, _8b, _8c, _8e);
                }
            }
        }
    };
    var _9c = 0;
    function _1c(obj, _9d) {
        if (_9d) {
            var _9e = _b.objs.length;
            var id = "n:" + _9c;
            obj.id = id;
            _b.objs[_9e] = obj;
            _b.setFileName[obj.name] = _9e;
            _b.objs[_9e].status = _22.STATUS_WAITTING;
            var _9f = jQ("#divAttach")[0];
            var _a0 = document.createElement("DIV");
            jQ(_a0).addClass("attachRow");
            jQ(_a0).attr("id", "divAttachDisplay_" + id);
            var _a1 = document.createElement("SPAN");
            jQ(_a1).addClass("ico icoNormalAttach");
            jQ(_a0).append(_a1);
            var _a2 = CMUpload.CMCheckSecurityLevel(_1a(obj.name));
            var _a3 = _1a(_a2.name);
            var _a4 = document.createElement("SPAN");
            jQ(_a4).attr("id", "displayName_" + id);
            jQ(_a4).html(_a3 + "&nbsp;").addClass("inlineb");
            jQ(_a0).append(_a4);
            var _a5 = "<span id='css_status_" + id + "' class='capacity inlineb'><div class='comcapacityBar inlineb'>" + "<div id='status_" + id + "' class='comcapacityBarFont inlineb' style='width:100%'>" + gLang.compose.msg.msg_attachment_uploading + "</div></div></span>";
            jQ(_a0).append(_a5);
            var _a6 = document.createElement("SPAN");
            jQ(_a6).attr("id", "Upload_Success_" + id).css("display", "none");
            jQ(_a6).addClass("ico icoUploadSuccess");
            jQ(_a0).append(_a6);
            jQ(_9f).append(_a0);
            jQ(_a0).append(_a7(_9e));
            _9c++;
            _a[id] = "0";
        } else {
            var _9e = _b.setFileName[obj.name];
            _b.objs[_9e].attributeID = obj.attributeID;
            _b.objs[_9e].status = _22.STATUS_UPLOAD_SUCCESS;
            _b.objs[_9e].size = obj.size;
            var obj = _b.objs[_9e];
            var id = obj.id;
            var _a0 = $("divAttachDisplay_" + id);
            jQ($("cancel_" + id)).remove();
            jQ($("css_status_" + id)).remove();
            jQ($("Upload_Success_" + id)).show();
            jQ(_a0).append(_34(_9e));
            _5e(id, sizeAutoFormat(obj.size), sizeAutoFormat(obj.size));
            jQ(_a0).append(_4d(_9e, true));
            jQ(_a0).append(_3e(_9e, true));
            jQ(_a0).append(_42(_9e));
            jQ(_a0).append(_45(id, true));
            if (C.securityLevel > 1) {
                var _a8 = document.createElement("DIV");
                jQ(_a8).attr("id", "colsel_security_" + id).addClass("inlineb");
                jQ(_a0).append(_a8);
                var _a2 = CMUpload.CMCheckSecurityLevel(_1a(obj.name));
                _53(_9e, _a2.level);
                jQ(_a8).css({ "vertical-align": "middle", "margin-top": "-3px" });
            }
            _a[id] = "1";
        }
    };
    function _a7(_a9) {
        var obj = _b.objs[_a9];
        return Object.extend(document.createElement("a"), {
            id: "cancel_" + obj.id, className: "btl", innerHTML: gLang.compose.att["att_upload_cancel_by_dnd"], onclick: function () {
                C.cancelUploadForN(obj.formId, obj.name, obj.inline, obj.identifier);
                jQ($("divAttachDisplay_" + obj.id)).remove();
                C.handleDisplayOfAttach();
            }
        });
    };
    function _1d(msg, _aa, _ab) {
        _ab = _ab + "";
        jQ.each(_b.objs, function () {
            if (this.formId == _aa) {
                this.attributeID = _ab;
                this.status = _22.STATUS_UPLOAD_FAIL;
                _a[this.id] = "0";
                if (!$("status_" + this.id)) {
                    $("css_status_" + this.id).innerHTML = "<div class='comcapacityBar inlineb'>" + "<div id='status_" + this.id + "' class='comcapacityBarFont inlineb' style='width:0%'>" + "</div></div>";
                }
                $("css_status_" + this.id).className = "capacity warning inlineb";
                $("status_" + this.id).style.width = "100%";
                $("status_" + this.id).innerHTML = msg;
                $("css_status_" + this.id).title = msg;
                var _ac = $("divAttachDisplay_" + this.id);
                var _ad = _b.setFileName[this.name];
                jQ($("cancel_" + this.id)).remove();
                jQ(_ac).append(_3e(_ad, true));
                jQ(_ac).append(_42(_ad));
                _3d(this.id, true);
                return false;
            }
        });
    };
    function _1e(_ae, _af) {
        _af = _af + "";
        jQ.each(_b.objs, function () {
            if (this.formId == _ae) {
                this.attributeID = _af;
                this.status = _22.STATUS_UPLOAD_SUCCESS;
                _a[this.id] = "1";
                var _b0 = $("divAttachDisplay_" + this.id);
                jQ(_b0).remove();
                return false;
            }
        });
    };
    function _1f(_b1, _b2) {
        _b2 = _b2 || false;
        var _b3 = "";
        jQ.each(_b.objs, function (i, n) {
            if (this.attributeID && this.attributeID == _b1) {
                _b3 = this.id;
                if (_b2) {
                    _b.objs.splice(i, 1);
                }
                return false;
            }
        });
        if (_4 && _b3 == "" && _22.objs.length > 0) {
            for (var i = 0; i < _22.objs.length; ++i) {
                var obj = _22.objs[i];
                if (("BigFile" + obj.id) == _b1) {
                    _b3 = obj.id;
                    if (_b2) {
                        _22.objs.splice(i, 1);
                    }
                    return _b3;
                }
            }
        }
        return _b3;
    };
    function _1b(obj, _b4, _b5) {
        _b5 = _b5 || false;
        var _b6 = _b.objs.length;
        var id = obj.id;
        _b.objs[_b6] = obj;
        _b.setFileName[obj.name] = _b6;
        var _b7 = jQ("#divAttach")[0];
        var _b8 = document.createElement("DIV");
        jQ(_b8).addClass("attachRow");
        jQ(_b8).attr("id", "divAttachDisplay_" + id);
        var _b9 = document.createElement("SPAN");
        jQ(_b9).addClass("ico");
        if (_b4 == PluginManager.Submit_Type.Normal || _b4 == PluginManager.Submit_Type.DropFiles) {
            jQ(_b9).addClass("icoNormalAttach");
        } else {
            if (_b4 == PluginManager.Submit_Type.SoundRecord) {
                jQ(_b9).addClass("icoSoundAttach");
            } else {
                if (_b4 == PluginManager.Submit_Type.VideoRecord) {
                    jQ(_b9).addClass("icoVedioAttach");
                } else {
                    if (_b4 == "trs") {
                        jQ(_b9).addClass("icoTrsAttach");
                    } else {
                        if (_b4 == "netfolder") {
                            jQ(_b9).addClass("icoNFAttach");
                        } else {
                            if (_b4 == "upload" || _b4 == "internal") {
                                jQ(_b9).addClass("icoNormalAttach");
                            } else {
                                jQ(_b9).addClass("icoNormalAttach");
                            }
                        }
                    }
                }
            }
        }
        jQ(_b8).append(_b9);
        var _ba = CMUpload.CMCheckSecurityLevel(_1a(obj.name));
        var _bb = _1a(_ba.name);
        var _bc = document.createElement("SPAN");
        jQ(_bc).attr("id", "displayName_" + id);
        jQ(_bc).html(_bb.htmlencode() + "&nbsp;").addClass("inlineb");
        jQ(_b8).append(_bc);
        if (!_b5) {
            var _bd = "<div id='css_status_" + id + "' class='capacity inlineb'>" + "&nbsp;" + gLang.compose.msg.msg_attach_upload_scanning + "0%";
            +"</div>";
            jQ(_b8).append(_bd);
        }
        var _be = document.createElement("SPAN");
        jQ(_be).attr("id", "Upload_Success_" + id);
        if (!_b5) {
            jQ(_be).css("display", "none");
        }
        jQ(_be).addClass("ico");
        jQ(_be).addClass("icoUploadSuccess");
        jQ(_b8).append(_be);
        jQ(_b7).append(_b8);
        jQ(_b8).append(_34(_b6));
        if (_b5) {
            _5e(id, sizeAutoFormat(obj.size), sizeAutoFormat(obj.size));
        }
        jQ(_b8).append(_4d(_b6, _b5));
        if (!_b5) {
            jQ(_b8).append(_37(_b6));
        }
        jQ(_b8).append(_3e(_b6, _b5));
        jQ(_b8).append(_42(_b6));
        if (_b5 && obj.deleted || (AttachInfo.getValidSize() > C.getMailLimitSize())) {
            _3d(id, true);
        }
        if (!_b5) {
            jQ(_b8).append(_3a(_b6));
        }
        if (_b4 != "trs") {
            if (_b4 == "netfolder") {
                jQ(_b8).append(_45(id, _b5, obj.mid));
            } else {
                jQ(_b8).append(_45(id, _b5));
            }
        }
        if (C.securityLevel > 1) {
            var _bf = document.createElement("DIV");
            jQ(_bf).attr("id", "colsel_security_" + id).addClass("inlineb");
            jQ(_b8).append(_bf);
            _53(_b6, _ba.level);
            jQ(_bf).css({ "vertical-align": "middle", "margin-top": "-3px" });
        }
    };
    function _9b(id, _c0, _c1, _c2, _c3) {
        _c1 = _c1 || false;
        _c2 = _c2 || null;
        var _c4 = (_c2 == PluginManager.Submit_Type.ScreenCapture);
        _c3 = _c3 || "";
        _c0 = _1a(_c0);
        var obj = { name: _c0, id: id, status: _b.STATUS_WAITTING, percent: 0, isloaded: false, isCapture: _c4, identifier: _c3 };
        _1b(obj, _c2, false);
        C.handleDisplayOfAttach();
        _a[id] = "0";
        jQ("#btnSend").addClass("disabled");
        jQ("#btnSend2").addClass("disabled");
        if ($("aTimeSet")) {
            $("aTimeSet").style.display = "none";
        }
        if (_b.timerID == null) {
            _6b(_c1);
        }
    };
    function _c5(_c6) {
        var ids = _c6.split(",");
        if (_1 && _1.length > 0 && _c7(_6, ids)) {
            return;
        }
        for (var i = 0; i < ids.length; ++i) {
            _8a(ids[i]);
        }
        if (_b.timerID == null) {
            _6b();
        }
    };
    function _c7(mgr, ids) {
        for (var i = 0; i < ids.length; ++i) {
            var _c8 = "";
            if (!ids[i]) {
                continue;
            }
            if (mgr.bdgGetUploadFileName) {
                _c8 = mgr.bdgGetUploadFileName(ids[i]);
            } else {
                _c8 = mgr.getUploadFileName(ids[i]);
            }
            _c8 = _c8.substr(_c8.lastIndexOf("\\") + 1);
            if (_90(_c8)) {
                _c8 = fileNameSoLong(_c8, 30);
                var msg = gLang.compose.msg["msg_risky_attach_select"];
                msg = msg.replace("${filename}", _c8);
                msg = msg.replace("${suffix}", _1.join(" , "));
                UI.alert(msg);
                return true;
            }
        }
        return false;
    };
    function _90(_c9) {
        return _1 && _c9 && _c9.lastIndexOf(".") != -1 && _1.member(_c9.substr(_c9.lastIndexOf(".")).toLowerCase());
    };
    function _10(_ca) {
        _ca = _ca || "dndContainer";
        if (_ca && $(_ca)) {
            Element.hide($(_ca));
        }
    };
    function _11(_cb) {
        _cb = _cb || "dndContainer";
        if (_cb && $(_cb)) {
            Element.show($(_cb));
        }
    };
    function _f(_cc) {
        if (!_cc) {
            return;
        }
        var _cd = $(_cc);
        var _ce = false;
        var _cf = setTimeout(function () {
            CMUpload.closeDnDDisplay(_cc);
            _ce = false;
        }, 500);
        if (_cd) {
            _dd();
            if (_6.bdgSupportHTML5Upload() && !_7 || _8) {
                var _d0 = jQuery("#htmleditor").contents();
                var _d1 = _d0.find("body")[0];
                var _d2 = _d0.find("#HtmlEditor")[0].contentWindow.document.body;
                Event.observe(top, "dragenter", Event.stop, false);
                Event.observe(window, "dragenter", Event.stop, false);
                Event.observe(_d1, "dragenter", Event.stop, false);
                Event.observe(_d2, "dragenter", Event.stop, false);
                var _d3 = function (_d4) {
                    _d4 = _d4 || window.event;
                    if ($win && $win().event) {
                        _d4 = $win().event;
                    }
                    if (_6.bdgCheckDnDFile(_d4)) {
                        if (_cf) {
                            clearTimeout(_cf);
                            _cf = setTimeout(function () {
                                CMUpload.closeDnDDisplay(_cc);
                                _ce = false;
                            }, 500);
                        }
                        _ce = true;
                        Element.show(_cd);
                    } else {
                        Event.stop(_d4);
                        if (_7) {
                            _d4.dataTransfer.dropEffect = "none";
                        } else {
                            _d4.dataTransfer.effectAllowed = "copy";
                            _d4.dataTransfer.dropEffect = "copy";
                        }
                    }
                    if (_ce) {
                        Event.stop(_d4);
                    }
                };
                Event.observe(top, "dragover", _d3, false);
                Event.observe(window, "dragover", _d3, false);
                Event.observe(_d1, "dragover", _d3, false);
                Event.observe(_d2, "dragover", _d3, false);
                var _d5 = function (e) {
                    CMUpload.closeDnDDisplay(_cc);
                    if (_ce) {
                        Event.stop(e);
                    }
                };
                Event.observe(top, "drop", _d5, false);
                Event.observe(window, "drop", _d5, false);
                Event.observe(_d1, "drop", _d5, false);
                Event.observe(_d2, "drop", _d5, false);
                Event.observe(_cd, "dragenter", Event.stop, false);
                Event.observe(_cd, "dragover", function (e) {
                    if (_cf) {
                        clearTimeout(_cf);
                        _cf = setTimeout(function () {
                            CMUpload.closeDnDDisplay(_cc);
                            _ce = false;
                        }, 500);
                    }
                    _ce = true;
                    Element.show(_cd);
                    if (_ce) {
                        Event.stop(e);
                    }
                }, false);
                Event.observe(_cd, "drop", CMUpload.DndSubmitData, false);
            } else {
                if (PluginManager.isLoaded()) {
                    if (PluginManager.supportDropFiles()) {
                        PluginManager.setDropFiles(true);
                        $("cmplugin").attachEvent("onDropIn", function (ids) {
                            CMUpload.showDnDDisplay(_cc);
                        });
                        $("cmplugin").attachEvent("onDropOut", function (ids) {
                            CMUpload.closeDnDDisplay(_cc);
                        });
                        $("cmplugin").attachEvent("onDropFiles", function (ids) {
                            CMUpload.closeDnDDisplay(_cc);
                            CMUpload.submitDataByCoreMailPlugin(PluginManager.Submit_Type.DropFiles, { ids: ids });
                        });
                    } else {
                        $("html5_dragdrop_area").innerHTML = gLang.compose.msg["upload_attachment_unSupport_drap_other"];
                    }
                } else {
                    PluginInfo.asyncLoad(function (_d6) {
                        var g = gLang.compose.multimidea.dropfiles_notfound;
                        $("html5_dragdrop_area").innerHTML = g[0] + "<b>" + _d6.createDownloadLinkForCMPlugin(g[1], "color:#0000FF;text-decoration:underline") + "</b>";
                    });
                }
            }
        }
    };
    function _d(_d7) {
        CMUpload.closeDnDDisplay("dndContainer");
        _d7 = _d7 || window.event;
        if ($win && $win().event) {
            _d7 = $win().event;
        }
        var ids = _6.bdgAddMutiDnDFile(_d7);
        _d8(ids);
    };
    function _d8(ids) {
        var _d9 = $("composeIDForCMUpload").value;
        var _da = AttachInfo.getUploadSuccessObjs();
        if (_d9 == null || _d9 == "") {
            new CMXClient().simpleCall("mbox:compose", null, function (_db) {
                _d9 = _db;
                $("composeIDForCMUpload").value = _d9;
                if (_6.bdgSupportDnDUpload()) {
                    _6.bdgInit(_d9);
                    _c5(ids);
                }
            });
        } else {
            new CMXClient().simpleCall("mbox:compose", { id: _d9, action: "continue", attrs: { id: _d9, attachments: _da } }, function () {
                if (_6.bdgSupportDnDUpload()) {
                    _6.bdgInit(_d9);
                    _c5(ids);
                }
            });
        }
    };
    function _e() {
        var _dc = document.getElementById("autoDel");
        if (_dc && _dc.checked) {
            return false;
        }
        jQ("div.infoPanel").closest("tr").hide();
        _dd();
        if (PluginManager.isLoaded()) {
            CMUpload.submitDataByCoreMailPlugin(PluginManager.Submit_Type.Normal);
        } else {
            if (_6.bdgSupportHTML5Upload()) {
                var _de = document.getElementById("divUploadAttachFile");
                if (!_de) {
                    var div = document.createElement("div");
                    div.innerHTML = "<input id=\"divUploadAttachFile\" type=\"file\" multiple style='position:relative;right:99999px;'/>";
                    _de = div.removeChild(div.firstChild);
                    document.body.appendChild(_de);
                } else {
                    var _df = jQ("#divUploadAttachFile");
                    _df.replaceWith(_df = _df.val("").clone(true));
                    _de = _df[0];
                    _de.onchange = null;
                }
                _de.onchange = function () {
                    var ids = _6.bdgAddMutiFile(this.files);
                    _d8(ids);
                };
                setTimeout(function () {
                    _de.click();
                }, 0);
            }
        }
    };
    function _dd() {
        var _e0 = $("supportAutoNormal2Trs").value;
        if ((typeof _e0 == "string" && _e0 == "true") || typeof _e0 == "boolean" && _e0) {
            _4 = true;
        }
    };
    function _c(_e1, _e2) {
        var ids = [];
        var _e3 = false;
        var _e4 = false;
        if (_e1 == PluginManager.Submit_Type.ScreenCapture) {
            _e3 = true;
        } else {
            if (_e1 == PluginManager.Submit_Type.DropFiles) {
                _e4 = true;
                ids = _e2.ids;
            }
        }
        var _e5 = $("composeIDForCMUpload").value;
        var _e6 = AttachInfo.getUploadSuccessObjs();
        if (_e5 == null || _e5 == "") {
            new CMXClient().simpleCall("mbox:compose", null, function (_e7) {
                $("composeIDForCMUpload").value = _e5 = _e7;
                _e8();
            });
        } else {
            new CMXClient().simpleCall("mbox:compose", { id: _e5, action: "continue", attrs: { id: _e5, attachments: _e6 } }, function () {
                _e8();
            });
        }
        function _e8() {
            if (jQ.browser.mozilla && !_7) {
                PluginManager.reloadPluginDiv(jQ, _e9);
            } else {
                _e9();
            }
            function _e9() {
                if (_ea.bdgSupportPlugin(true)) {
                    PluginManager.initUpload(PluginManager.Upload_Type.Upload_Compose, { composeId: _e5, isCapture: _e3 }, null, { ids: ids }, _e4);
                    ids = PluginManager.submitMultiUpload(_e1, { ids: ids });
                    for (var i = 0; i < ids.length; i++) {
                        _8a(ids[i], true, _e1);
                    }
                }
            };
        };
    };
    var _eb = false;
    var _ec = [];
    var _ed = [];
    var _ea = new UploadBridge();
    var _ee = true;
    var _22 = {
        objs: [], setFileName: {}, curIndex: -1, timerID: null, STATUS_WAITTING: 0, STATUS_PREPARE_UPLOAD: 1, STATUS_UPLOADING: 2, STATUS_UPLOAD_SUCCESS: 3, STATUS_UPLOAD_FAIL: 4, STATUS_CANCEL: 5, STATUS_MOVING_TO_NF: 6, STATUS_MOVE_TO_NF_SUCCESS: 7, STATUS_FAIL_REASON: ["NORMAL", "RUNNING", gLang.compose.msg["msg_attachment_nomem"], gLang.compose.msg["msg_attachment_syserr"], gLang.compose.msg["msg_attachment_excced_size"], gLang.compose.msg["msg_attachment_excced_count"], gLang.compose.msg["msg_attachment_noexist"], gLang.compose.msg["msg_attachment_neterr"], gLang.compose.msg["msg_attachment_client_notfound"], gLang.compose.msg["msg_attachment_crcerr"], gLang.compose.msg["fa_request_expired"]], isPeddingStatus: function (s) {
            return s == _b.STATUS_WAITTING || s == _b.STATUS_PREPARE_UPLOAD || s == _b.STATUS_UPLOADING;
        }, isProcessing: function (s) {
            return _22.isPeddingStatus(s) || (s == _22.STATUS_UPLOAD_SUCCESS && _ee) || s == _22.STATUS_MOVING_TO_NF;
        }
    };
    CMUpload.CMHasHTML5UploadByBigFile = _ef;
    CMUpload.CMSubmidDataIdsByBigFile = _f0;
    CMUpload.CMRestoreStateByBigFile = _f1;
    CMUpload.CMCancelUploadByBigFile = _f2;
    CMUpload.CMRestoreUploadingByBigFile = _f3;
    CMUpload.CMUpdateFileStatusWhenDialogShow = _f4;
    CMUpload.CMHasFileUpload = _f5;
    CMUpload.CMReleaseTrsActiveX = _f6;
    CMUpload.CMHasFileUploading = _f7;
    function _ef() {
        return _ea.bdgSupportHTML5Upload();
    };
    function _f0(_f8, _f9, fid) {
        var ids = _ea.bdgAddMutiFile(_f9);
        if (_ea.bdgSupportDnDUpload()) {
            _ea.bdgInit2(fid);
            _fa(ids, _f8);
        }
    };
    function _f3(_fb, id, crc, _fc, fid) {
        if (!_ec) {
            _ec = [];
        }
        if (_ea.bdgSupportCM_COM(true)) {
            _ea.bdgInit2(fid, id);
            var id = _ea.bdgAddNetfolderToContinue(_fb, id, crc);
            _fd(id, _fc);
            if (_22.timerID == null) {
                _fe();
            }
        }
    };
    function _f5() {
        return _eb || (_ec && _ec.length > 0);
    };
    function _fa(_ff, _100) {
        var ids = _ff.split(",");
        if (_1 && _1.length > 0 && _c7(_ea, ids)) {
            return;
        }
        for (var i = 0; i < ids.length; ++i) {
            _fd(ids[i], _100);
        }
        if (_22.timerID == null) {
            _fe();
        }
    };
    function _fd(id, _101) {
        var _102 = _ea.bdgGetUploadFileName(id);
        if (_102 != null && _102 != "") {
            if (_22.setFileName[_102] != null) {
                var _103 = _22.setFileName[_102];
                var obj = _22.objs[_103];
                var _104, _105;
                if (obj.status == _22.STATUS_UPLOAD_SUCCESS) {
                    _104 = $win().$("ft_startdiv_" + _103);
                    if (!_104) {
                        _106(id, _102, _101);
                    } else {
                        obj = {};
                        _105 = _22.objs.length;
                        _22.objs[_105] = obj;
                    }
                } else {
                    _104 = $win().$("ft_startdiv_" + _103);
                    if (!_104) {
                        _106(id, _102, _101);
                    } else {
                        obj = {};
                        _105 = _22.objs.length;
                        _22.objs[_105] = obj;
                        UI.alert(gLang.compose.msg["msg_attach_exist"]);
                    }
                }
            } else {
                _106(id, _102, _101);
            }
        }
    };
    function _106(id, _107, _108) {
        var obj = { name: _107, id: id, status: _22.STATUS_WAITTING, percent: 0, isloaded: false, uploaded: 0 };
        var _109 = _22.objs.length;
        _22.objs[_109] = obj;
        _22.setFileName[_107] = _109;
        var El = $doc();
        var _10a = El.createElement("div");
        _10a.setAttribute("id", "ft_startdiv_" + id);
        _10a.className = "ft_start";
        var _10b = El.createElement("div");
        _10b.setAttribute("id", "ft_icodiv_" + id);
        _10b.className = "ft_ico";
        _10b.style.display = "none";
        _10b.innerHTML = "<div class=\"ico_upload_success\"></div>";
        var _10c = El.createElement("div");
        _10c.className = "ft_main";
        var _10d = El.createElement("div");
        _10d.className = "ft_top";
        var _10e = El.createElement("div");
        _10e.className = "ft_name";
        var _10f = (_107.lastIndexOf("\\") != -1) ? _107.lastIndexOf("\\") + 1 : 0;
        _10e.innerHTML = _107.substring(_10f).escapeHTML();
        var _110 = El.createElement("div");
        _110.setAttribute("id", "operatordiv_" + id);
        _110.className = "ft_tOperator";
        _110.appendChild(_111(id));
        _110.appendChild(_112(id));
        _10d.appendChild(_10e);
        _10d.appendChild(_110);
        var _113 = El.createElement("div");
        _113.className = "ft_content";
        var _114 = El.createElement("div");
        _114.className = "ft_tProcess";
        _114.setAttribute("id", "ft_tProcessdiv_" + id);
        _114.style.display = "none";
        var _115 = El.createElement("div");
        _115.setAttribute("id", "ft_txProcessPausediv_" + id);
        _115.className = "ft_txProcessPause";
        var _116 = El.createElement("div");
        _116.setAttribute("id", "uploadProcessBardiv_" + id);
        _116.className = "uploadProcessBar";
        _116.style.width = "0";
        _115.appendChild(_116);
        var _117 = El.createElement("div");
        _117.className = "ft_processCount";
        _117.setAttribute("id", "ft_processCountdiv_" + id);
        _113.appendChild(_115);
        _113.appendChild(_117);
        _113.appendChild(_114);
        var _118 = El.createElement("div");
        _118.setAttribute("id", "ft_processDetaildiv_" + id);
        _118.innerHTML = "";
        _118.className = "ft_bottom";
        _10c.appendChild(_10d);
        _10c.appendChild(_113);
        _10c.appendChild(_118);
        _10a.appendChild(_10b);
        _10a.appendChild(_10c);
        _108.appendChild(_10a);
        if (_22.timerID == null) {
            _fe();
        }
    };
    function _111(id) {
        var node = Object.extend($doc().createElement("a"), {
            id: "cancel_bigFile_" + id, className: "btl", innerHTML: gLang.compose.att["att_upload_cancel"], onclick: function () {
                CMUpload.CMCancelUploadByBigFile(id);
            }
        });
        node.style.display = "none";
        return node;
    };
    function _112(id) {
        var node = Object.extend($doc().createElement("a"), {
            id: "resume_bigFile_" + id, className: "btl", innerHTML: gLang.compose.att["att_upload_resume"], onclick: function () {
                CMUpload.CMRestoreStateByBigFile(id);
            }
        });
        node.style.display = "none";
        return node;
    };
    function _f2(id) {
        for (var i = 0; i < _22.objs.length; ++i) {
            if (_22.objs[i].id == id) {
                var obj = _22.objs[i];
                var _119 = $win().$("uploadProcessBardiv_" + id);
                _119.className = "uploadProcessBarNotActive";
                _ea.bdgCancelUpload(id);
                _11a(i);
                obj.beginUploadTime = null;
                obj.beginUploadSize = null;
                obj.cancelling = true;
                setTimeout(function () {
                    obj.cancelling = false;
                }, 2000);
                break;
            }
        }
    };
    var _11b = false;
    function _f1(id) {
        for (var i = 0; i < _22.objs.length; ++i) {
            var obj = _22.objs[i];
            if (obj.id == id) {
                if (obj.cancelling || _11b) {
                    UI.alert(gLang.compose.msg["msg_opsTooFrequent"]);
                    return;
                }
                _11b = true;
                var _11c = $win().$("uploadProcessBardiv_" + id);
                _11c.className = "uploadProcessBar";
                _ea.bdgRestoreState(id);
                var st = _ea.bdgQueryStatus(id).split(",");
                obj.status = parseInt(st[0], 10);
                obj.percent = parseInt(st[1], 10);
                obj.uploaded = parseInt(st[4], 10);
                _11a(i);
                _11b = false;
                break;
            }
        }
        if (_22.timerID == null) {
            _fe();
        }
    };
    function _11d(id, _11e, _11f) {
        _11f = _11f || false;
        with (_22) {
            var _120 = new CMXClient();
            _120.resultListener = function (_121) {
                objs[curIndex].status = STATUS_UPLOAD_FAIL;
                timerID = setTimeout(_fe(_11f), 100);
                if (_121.code == "FA_OVERFLOW") {
                    _ea.bdgCancelUpload(id);
                    $win().$("ft_txProcessPausediv_" + id).style.display = "none";
                    $win().$("ft_processCountdiv_" + id).style.display = "none";
                    $win().$("ft_tProcessdiv_" + id).style.display = "";
                    var _122 = _121.overflowReason;
                    var _123 = gLang.compose.msg[_122] || _122 || _121.code;
                    if (_122 == "pref_netfolder_max_file_count") {
                        _123 = gLang.compose.msg["msg_attachment_excced_count"];
                    } else {
                        if (_122 == "pref_netfolder_quota" || _122 == "pref_netfolder_max_file_size") {
                            _123 = gLang.compose.msg["msg_attachment_excced_size"];
                        }
                    }
                    $win().$("ft_tProcessdiv_" + id).innerHTML = _123;
                    $win().$("cancel_bigFile_" + id).style.display = "none";
                    $win().$("resume_bigFile_" + id).style.display = "none";
                    return true;
                }
                return false;
            };
            var _124 = objs[curIndex].attributeID || "";
            var _125 = 9;
            if (_ea.bdgSupportDnDUpload()) {
                _124 = "";
                _125 = _ea.bdgGetComposeId();
                if (_125.indexOf("c:nf:") == 0) {
                    _125 = _125.substr("c:nf:".length);
                }
            }
            _120.cgi = { uid: _2.uid };
            _120.simpleCall("upload:prepareCheckUploadSizeLimit", { attachId: _124, fid: parseInt(_125), size: parseInt(_11e), uid: _2.uid }, function (_126) {
                objs[curIndex].size = _11e;
                if (!_11f) {
                    if ($win().$("ft_startdiv_" + id)) {
                        _11a(curIndex);
                    } else {
                        if (objs[curIndex].status == STATUS_UPLOAD_SUCCESS) {
                            _ed[_ed.length] = { id: id, index: curIndex };
                        }
                    }
                } else {
                    if ($("divAttachDisplay_" + id)) {
                        _127(curIndex);
                    } else {
                        if (objs[curIndex].status == STATUS_UPLOAD_SUCCESS) {
                            _ed[_ed.length] = { id: id, index: curIndex };
                        }
                    }
                }
                timerID = setTimeout(_fe(_11f), 100);
            });
            objs[curIndex].checkSize = true;
        }
    };
    function _fe() {
        if (!_ea) {
            return;
        }
        with (_22) {
            var len = objs.length;
            if (curIndex == len || curIndex == -1 || !isProcessing(objs[curIndex].status)) {
                for (curIndex = 0; curIndex < len; ++curIndex) {
                    if (isProcessing(objs[curIndex].status)) {
                        break;
                    }
                }
                if (curIndex == len) {
                    timerID = null;
                    return;
                }
            }
            var id = objs[curIndex].id;
            if (objs[curIndex].status == STATUS_WAITTING) {
                if (_ea.bdgSupportDnDUpload()) {
                    var _128 = _ea.bdgGetFileSize(id);
                    if (!objs[curIndex].checkSize && _128 > 0) {
                        _11d(id, _128);
                        return;
                    } else {
                        if (_128 == 0) {
                            objs[curIndex].status = STATUS_UPLOAD_FAIL;
                            timerID = setTimeout(_fe, 100);
                            _11a(curIndex);
                            return;
                        } else {
                            if (objs[curIndex].checkSize && _128) {
                                objs[curIndex].size = _128;
                            }
                        }
                    }
                }
                _ea.bdgStartUpload(id, _2);
                objs[curIndex].status = STATUS_PREPARE_UPLOAD;
            } else {
                var st = _ea.bdgQueryStatus(id).split(",");
                objs[curIndex].status = parseInt(st[0], 10);
                objs[curIndex].percent = parseInt(st[1], 10);
                objs[curIndex].uploaded = parseInt(st[4], 10);
                if (st.length > 2) {
                    objs[curIndex].attributeID = st[2];
                }
                if (!objs[curIndex].isloaded && objs[curIndex].attributeID != null && objs[curIndex].attributeID != "") {
                    objs[curIndex].isloaded = true;
                }
                if (st.length > 3) {
                    if (!objs[curIndex].checkSize && parseInt(st[3]) > 0) {
                        if (!_ea.bdgSupportDnDUpload()) {
                            _11d(id, st[3]);
                        }
                        return;
                    } else {
                        objs[curIndex].size = st[3];
                    }
                }
            }
            if ($win().$("ft_startdiv_" + id)) {
                _11a(curIndex);
            } else {
                if (objs[curIndex].status == STATUS_UPLOAD_SUCCESS) {
                    _ed[_ed.length] = { id: id, index: curIndex };
                }
            }
            timerID = setTimeout(_fe, 100);
        }
    };
    function _23(_129) {
        return _129 == PluginManager.UploadItemState_Type.Ready || _129 == PluginManager.UploadItemState_Type.CRC || _129 == PluginManager.UploadItemState_Type.Uploading;
    };
    function _12a(id) {
        if (!PluginManager.isLoaded()) {
            return;
        }
        for (var i = 0; i < _22.objs.length; ++i) {
            if (_22.objs[i].id == id) {
                var obj = PluginManager.queryUploadItemStatus(id, _22.objs[i]);
                _22.objs[i].percent = obj.percent;
                _22.objs[i].attributeID = obj.attachmentId;
                if (!_22.objs[i].isloaded && _22.objs[i].attributeID != null && _22.objs[i].attributeID != "") {
                    _22.objs[i].isloaded = true;
                }
                if (_23(_22.objs[i].state)) {
                    _127(i);
                    setTimeout(function () {
                        return _12a(id);
                    }, 100);
                } else {
                    _127(i);
                    return;
                }
                break;
            }
        }
    };
    function _f4() {
        with (_22) {
            for (var i = 0; i < _ed.length; i++) {
                var id = _ed[i].id;
                var _12b = _ed[i].index;
                $win().$("ft_icodiv_" + id).style.display = "";
                $win().$("ft_txProcessPausediv_" + id).style.display = "none";
                $win().$("ft_processCountdiv_" + id).style.display = "none";
                $win().$("ft_tProcessdiv_" + id).style.display = "";
                $win().$("ft_tProcessdiv_" + id).innerHTML = gLang.compose.msg["uploadFinish"];
                $win().$("cancel_bigFile_" + id).style.display = "none";
                $win().$("resume_bigFile_" + id).style.display = "none";
                $win().$("ft_processDetaildiv_" + id).style.display = "none";
                _ec[_ec.length] = { mid: objs[_12b].attributeID, name: _1a(objs[_12b].name), desc: _1a(objs[_12b].name), nfSize: objs[_12b].size, size: _12c(objs[_12b].size) };
                _ea.bdgMoveToNetFolder(id);
            }
            _ed = [];
        }
    };
    function _12c(size, n) {
        var p = 1;
        if (n && n > 0) {
            for (var i = 0; i < n; i++) {
                p = 10 * p;
            }
        }
        if (size < 1024) {
            size = size * p;
            size = Math.round(size);
            size = size / p;
            return size + "B";
        } else {
            if (size < 1024 * 1024) {
                size = size / 1024;
                size = size * p;
                size = Math.round(size);
                size = size / p;
                return size + "K";
            } else {
                size = size / (1024 * 1024);
                size = size * p;
                size = Math.round(size);
                size = size / p;
                return size + "M";
            }
        }
    };
    function _12d(time) {
        var hour = Math.floor(time / (60 * 60));
        var _12e = Math.floor((time - hour * 60 * 60) / 60);
        var _12f = Math.round(time - hour * 60 * 60 - _12e * 60);
        var text = "";
        if (hour > 9) {
            text += hour + ":";
        } else {
            text += "0" + hour + ":";
        }
        if (_12e > 9) {
            text += _12e + ":";
        } else {
            text += "0" + _12e + ":";
        }
        if (_12f > 9) {
            text += _12f;
        } else {
            text += "0" + _12f;
        }
        return text;
    };
    function _11a(_130) {
        if (!_ea) {
            return;
        }
        with (_22) {
            var obj = objs[_130];
            var id = obj.id;
            if (obj.moveToNetFolder || !$win().$("ft_startdiv_" + id)) {
                return;
            }
            if (obj.status == STATUS_WAITTING) {
                $win().$("ft_txProcessPausediv_" + id).style.display = "";
                $win().$("ft_processCountdiv_" + id).style.display = "";
                $win().$("ft_tProcessdiv_" + id).style.display = "none";
                $win().$("uploadProcessBardiv_" + id).style.width = (obj.percent || 0) + "%";
                $win().$("ft_processCountdiv_" + id).innerHTML = "";
            } else {
                if (obj.status == STATUS_PREPARE_UPLOAD) {
                    $win().$("ft_txProcessPausediv_" + id).style.display = "";
                    $win().$("ft_processCountdiv_" + id).style.display = "";
                    $win().$("ft_tProcessdiv_" + id).style.display = "none";
                    $win().$("ft_processCountdiv_" + id).innerHTML = gLang.compose.msg["uploadPrepare"] + obj.percent + "%";
                    $win().$("cancel_bigFile_" + id).style.display = "";
                    $win().$("resume_bigFile_" + id).style.display = "none";
                } else {
                    if (obj.status == STATUS_UPLOADING) {
                        $win().$("ft_tProcessdiv_" + id).style.display = "none";
                        $win().$("ft_txProcessPausediv_" + id).style.display = "";
                        $win().$("ft_processCountdiv_" + id).style.display = "";
                        $win().$("ft_processDetaildiv_" + id).style.display = "";
                        $win().$("uploadProcessBardiv_" + id).style.width = obj.percent + "%";
                        var _131 = new Date().getTime();
                        var _132 = obj.uploaded || obj.size * obj.percent / 100;
                        if (!obj.beginUploadSize) {
                            obj.beginUploadSize = _132;
                        }
                        if (!obj.beginUploadTime) {
                            obj.beginUploadTime = _131;
                        }
                        if (!obj.lastSpeed) {
                            obj.lastSpeed = 0;
                        }
                        if (!obj.leftTime) {
                            obj.leftTime = 0;
                        }
                        if (_131 - obj.beginUploadTime > 1000 && _132 - obj.beginUploadSize > 0) {
                            var _133 = _131 - obj.beginUploadTime;
                            obj.lastSpeed = (_132 - obj.beginUploadSize) * 1000 / _133;
                            if (obj.lastSpeed > 0) {
                                obj.leftTime = (obj.size - _132) / obj.lastSpeed;
                            }
                            obj.beginUploadTime = _131;
                            obj.beginUploadSize = _132;
                        }
                        obj.leftTime = obj.leftTime * 10;
                        obj.leftTime = Math.round(obj.leftTime);
                        obj.leftTime = obj.leftTime / 10;
                        $win().$("ft_processCountdiv_" + id).innerHTML = obj.percent + "%";
                        $win().$("ft_processDetaildiv_" + id).innerHTML = gLang.compose.msg["uploadDetail"].replace("*upload*", _12c(_132, 2)).replace("*speed*", _12c(obj.lastSpeed, 2)).replace("*lefttime*", _12d(obj.leftTime));
                        $win().$("cancel_bigFile_" + id).style.display = "";
                        $win().$("resume_bigFile_" + id).style.display = "none";
                        _eb = true;
                    } else {
                        if (obj.status == STATUS_UPLOAD_SUCCESS) {
                            $win().$("ft_icodiv_" + id).style.display = "";
                            $win().$("ft_txProcessPausediv_" + id).style.display = "none";
                            $win().$("ft_processCountdiv_" + id).style.display = "none";
                            $win().$("ft_processDetaildiv_" + id).style.display = "none";
                            $win().$("ft_tProcessdiv_" + id).style.display = "";
                            $win().$("cancel_bigFile_" + id).style.display = "none";
                            $win().$("resume_bigFile_" + id).style.display = "none";
                            obj.moveToNetFolder = true;
                            if (_2.moveToEnf) {
                                _ea.bdgMoveToEtpNetFolder(id, objs[curIndex].name, _2, _134);
                            } else {
                                _ea.bdgMoveToNetFolder(id, objs[curIndex].name, _134);
                            }
                            if (_ee) {
                                $win().$("ft_tProcessdiv_" + id).innerHTML = gLang.compose.msg["server_handling"];
                            } else {
                                $win().$("ft_tProcessdiv_" + id).innerHTML = gLang.compose.msg["uploadFinish"];
                                _135();
                            }
                            _eb = true;
                        } else {
                            if (obj.status == STATUS_MOVING_TO_NF) {
                            } else {
                                if (obj.status == STATUS_MOVE_TO_NF_SUCCESS) {
                                    _135();
                                } else {
                                    if (obj.status == STATUS_UPLOAD_FAIL) {
                                        var _136 = _ea.bdgGetUploadFailReason(id);
                                        var _137 = "";
                                        if (_136 == 3 || _136 == 4) {
                                            var _138 = _ea.bdgGetServerReturnInfo(id);
                                            _137 = _7c(_138);
                                        }
                                        if (!_137) {
                                            _137 = STATUS_FAIL_REASON[_136];
                                        }
                                        $win().$("resume_bigFile_" + id).style.display = "";
                                        if (!_137 && _ea.bdgGetFileSize(id) == 0) {
                                            _137 = gLang.compose.msg["msg_attachment_file_upload_empty"];
                                            $win().$("resume_bigFile_" + id).style.display = "none";
                                        }
                                        $win().$("ft_txProcessPausediv_" + id).style.display = "none";
                                        $win().$("ft_processCountdiv_" + id).style.display = "none";
                                        $win().$("ft_processDetaildiv_" + id).style.display = "none";
                                        $win().$("ft_tProcessdiv_" + id).style.display = "";
                                        $win().$("ft_tProcessdiv_" + id).innerHTML = _137;
                                        $win().$("cancel_bigFile_" + id).style.display = "none";
                                    } else {
                                        $win().$("cancel_bigFile_" + id).style.display = "none";
                                        $win().$("ft_processDetaildiv_" + id).style.display = "none";
                                        $win().$("resume_bigFile_" + id).style.display = "";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    };
    function _135() {
        var objs = _22.objs;
        var _139 = _22.curIndex;
        _ec[_ec.length] = { mid: objs[_139].attributeID, name: _1a(objs[_139].name), desc: _1a(objs[_139].name), nfSize: objs[_139].size, size: _12c(objs[_139].size) };
    };
    function _134(id, name, _13a) {
        $win().$("ft_tProcessdiv_" + id).innerHTML = gLang.nf.att["finish_rename"] + "<input type='hidden' id='renameId_bigFile_" + id + "' value='" + _13a + "'/>" + "<input type='text' class='rename_bigFile' id='rename_bigFile_" + id + "' value='" + name.htmlencode() + "'/>";
    };
    function _f6() {
        var temp = _ea;
        with (_22) {
            if (timerID) {
                clearTimeout(timerID);
            }
            var len = objs.length;
            for (curIndex = 0; curIndex < len; ++curIndex) {
                if (isPeddingStatus(objs[curIndex].status)) {
                    var id = objs[curIndex].id;
                    temp.bdgCancelUpload(id);
                }
            }
            temp = null;
            _ed = [];
            _ec = [];
            _eb = false;
            timerID = null;
            objs = [];
            setFileName = {};
            curIndex = -1;
        }
    };
    function _f7() {
        var len = _22.objs.length;
        for (var _13b = 0; _13b < len; ++_13b) {
            if (_22.isPeddingStatus(_22.objs[_13b].status)) {
                return true;
            }
        }
        return false;
    };
    function _7c(_13c) {
        var _13d = "";
        if (_13c) {
            var _13e = _13c.substring(_13c.indexOf("\n") + 1);
            if (_13e) {
                var _13f = "var responseObj=" + _13e;
                eval(_13f);
                if (responseObj) {
                    var _140 = responseObj.overflowReason || responseObj.code;
                    if (_140) {
                        _13d = gLang.compose.msg[_140.toLowerCase()];
                    }
                }
            }
        }
        return _13d;
    };
    function _1a(_141) {
        var _142 = -1;
        if (_9) {
            _142 = _141.lastIndexOf("/");
        } else {
            _142 = _141.lastIndexOf("\\");
        }
        if (_142 == -1) {
            return _141;
        } else {
            return _141.substring(_142 + 1);
        }
    };
    function _143(id) {
        _70({ cancel_: "", delete_: "none", undelete_: "none", download_: "none", resume_: "none" }, id);
    };
    function _144(id) {
        _145(id);
        _146(id);
        _147(id);
        _148(id);
    };
    function _145(id) {
        $("cancel_" + id).onclick = function () {
            for (var i = 0; i < _22.objs.length; i++) {
                if (_22.objs[i].id == id) {
                    PluginManager.cancelUpload(id);
                    _12a(id);
                    break;
                }
            }
        };
    };
    function _146(id) {
        $("resume_" + id).onclick = function () {
            for (var i = 0; i < _22.objs.length; i++) {
                if (_22.objs[i].id == id) {
                    if (_22.objs[i].state == PluginManager.UploadItemState_Type.Stopping) {
                        UI.alert(gLang.compose.msg["msg_opsTooFrequent"]);
                    } else {
                        PluginManager.startUpload(id);
                    }
                    setTimeout(function () {
                        return _12a(id);
                    }, 200);
                    break;
                }
            }
        };
    };
    function _147(id) {
        $("delete_" + id).onclick = function () {
            var _149 = _14a(id);
            if (_149 != _22.objs.length) {
                _14b(_149, true);
            }
        };
    };
    function _148(id) {
        $("undelete_" + id).onclick = function () {
            var _14c = _14a(id);
            if (_14c != _22.objs.length) {
                _14b(_14c, false);
            }
        };
    };
    function _14a(id) {
        for (var i = 0; i < _22.objs.length; ++i) {
            if (_22.objs[i].id == id) {
                return i;
            }
        }
        return _22.objs.length;
    };
    function _14b(_14d, _14e, _14f) {
        _14f = _14f || false;
        with (_22) {
            var id = objs[_14d].id;
            objs[_14d].deleted = _14e;
            var sId = "BigFile" + id;
            AttachInfo.setDeleted(sId, _14e, _14f);
            if ($("displayName_" + id)) {
                $("displayName_" + id).style.textDecoration = _14e ? "line-through" : "";
            }
            Element[_14e ? "hide" : "show"]($("delete_" + id));
            Element[_14e ? "show" : "hide"]($("undelete_" + id));
        }
    };
    function _79(id) {
        jQ("#divAttachDisplay_" + id + ">.ico:first").removeClass("icoNormalAttach").addClass("icoTrsAttach");
    };
    function _150() {
        var _151 = jQ("#autoTrsTips");
        _5 = _5 || _151.find(".info").html() || "";
        var _152 = "<span style='color: #0077b9'>", _153 = 0;
        for (var idx = 0, size = _22.objs.length; idx < size; idx++) {
            if (_153 < 2 && _22.objs[idx].state != PluginManager.UploadItemState_Type.Error) {
                _152 += _1a(_22.objs[idx].name);
                _153++;
                if (_153 < 2) {
                    _152 += ", ";
                }
            }
        }
        _152 += "</span>";
        _151.find(".info").html(_5.replace("$file$", _152).replace("$number$", size));
        _151.show();
    };
    function _78(id) {
        _b.uploadFailIds.push(id);
    };
    function _87() {
        var _154 = { ids: _b.uploadFailIds };
        PluginManager.initUpload(PluginManager.Upload_Type.Upload_NetFolder, { fid: 9 }, null, _154, true);
        PluginManager.setUploadItemStates(_b.uploadFailIds);
        for (var i = 0; i < _b.uploadFailIds.length; i++) {
            var id = _b.uploadFailIds[i];
            _155(parseInt(id), $("divAttachDisplay_" + id));
            _144(id);
            _143(id);
        }
        _150();
        _b.uploadFailIds = [];
    };
    function _155(id, _156) {
        var _157 = _1a(_ea.bdgGetUploadFileName(id));
        if (_b.setFileName[_157] >= 0) {
            _b.setFileName[_157] = null;
        }
        _158(id, _157);
    };
    function _158(id, _159) {
        var obj = { name: _159, id: id, status: _22.STATUS_WAITTING, percent: 0, isloaded: false, uploaded: 0 };
        var _15a = _22.objs.length;
        _22.objs[_15a] = obj;
        _22.setFileName[_159] = _15a;
        PluginManager.startUpload(id);
        _12a(id);
    };
    function _127(_15b) {
        if (!PluginManager.isLoaded()) {
            return;
        }
        with (_22) {
            var obj = objs[_15b];
            var id = obj.id;
            if (!$("divAttachDisplay_" + id)) {
                return;
            }
            var _15c = obj.state;
            if (_15c == PluginManager.UploadItemState_Type.Ready) {
                if (!$("status_" + id)) {
                    $("css_status_" + id).innerHTML = "<div class='comcapacityBar inlineb'>" + "<div id='status_" + id + "' class='comcapacityBarFont inlineb' style='width:0%'>" + "</div></div>";
                }
                $("status_" + id).style.width = (obj.percent || 0) + "%";
                $("status_" + id).innerHTML = (obj.percent || 0) + "%";
            } else {
                if (_15c == PluginManager.UploadItemState_Type.CRC) {
                    $("css_status_" + id).className = "capacity inlineb";
                    $("css_status_" + id).innerHTML = "&nbsp;" + gLang.compose.msg.msg_attach_upload_validation + (obj.percent || 0) + "%";
                    _70({ cancel_: "", delete_: "none", undelete_: "none", resume_: "none", download_: "none" }, id);
                    _70({ colsel_security_: "none" }, id, true);
                } else {
                    if (_15c == PluginManager.UploadItemState_Type.Uploading) {
                        if (!$("status_" + id)) {
                            $("css_status_" + id).innerHTML = "<div class='comcapacityBar inlineb'>" + "<div id='status_" + id + "' class='comcapacityBarFont inlineb' style='width:0%'>" + "</div></div>";
                        }
                        $("status_" + id).style.width = obj.percent + "%";
                        $("status_" + id).innerHTML = obj.percent + "%";
                        _70({ cancel_: "", delete_: "none", undelete_: "none", resume_: "none", download_: "none" }, id);
                        _70({ colsel_security_: "none" }, id, true);
                        _5e(id, sizeAutoFormat(obj.size * obj.percent / 100), sizeAutoFormat(obj.size));
                    } else {
                        if (_15c == PluginManager.UploadItemState_Type.Finish || _15c == PluginManager.UploadItemState_Type.FinishWithoutCRC) {
                            _71(id, true);
                            AttachInfo.addAttachInfo("BigFile" + id, _1a(obj.name), obj.type || "trs", obj.deleted || false, obj.mid || obj.attributeID, obj.nfSize || obj.size || null, obj.enfUid || null);
                            _5e(id, sizeAutoFormat(obj.size), sizeAutoFormat(obj.size));
                            updateSizeInfo();
                        } else {
                            if (_15c == PluginManager.UploadItemState_Type.Stopping || (_15c == PluginManager.UploadItemState_Type.Error && obj.lastErrorNum == PluginManager.UploadItemError_Type.Interrupt_Error)) {
                                _70({ cancel_: "none", resume_: "" }, id);
                            } else {
                                if (_15c == PluginManager.UploadItemState_Type.Error) {
                                    var _15d = TrsUpload.STATUS_FAIL_REASON[obj.lastErrorNum];
                                    if (!_15d) {
                                        _15d = obj.lastErrorStr;
                                    }
                                    _83(id, _15d, _4);
                                    jQ("div.infoPanel").closest("tr").hide();
                                    if ($("displayName_" + id)) {
                                        $("displayName_" + id).style.textDecoration = "line-through";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (!_23(_15c) && _a[id]) {
                _a[id] = "1";
            }
            _7d(_5d(true));
        }
    };
    function _70(_15e, id, _15f) {
        for (var p in _15e) {
            if (typeof (_15e[p]) == "function") {
            } else {
                if (_15f) {
                    if ($(p + "" + id)) {
                        $(p + "" + id).style.display = _15e[p];
                    }
                } else {
                    $(p + "" + id).style.display = _15e[p];
                }
            }
        }
    };
})();
AttachInfo = function () {
    var _160;
    function _161(_162) {
        _160 = _162 || jQ("#divAttachInfo")[0];
    };
    function _163(_164) {
        _164 = _164 || false;
        var _165 = 0;
        jQ("div[name*=attachment]", _160).each(function () {
            var _166 = jQ("input[name*=type]", this).attr("value");
            var _167 = jQ("input[name*=deleted]", this).attr("value");
            if ((!_164 && _166 == "trs") || _167 == "true") {
                return true;
            }
            _165 += parseInt(jQ("input[name*=size]", this).attr("value"));
        });
        return _165;
    };
    function _168(_169, _16a) {
        _16a = _16a || false;
        var _16b = false;
        if (_16a) {
            _16b = "";
        }
        jQ("input[name*=displayName]", _160).each(function () {
            if (jQ(this).attr("value") == _169) {
                var _16c = jQ(this).siblings("input[name=attachment]").attr("value");
                var _16d = jQ(this).siblings("input[name=attachment_" + _16c + "_replaced]").attr("value");
                if (_16d && _16d == "true") {
                    return true;
                } else {
                    if (_16a) {
                        _16b = _16c;
                    } else {
                        _16b = true;
                    }
                    return false;
                }
            }
        });
        return _16b;
    };
    function _16e(sId, _16f, _170, _171, sMid, _172, _173) {
        jQ("#attachment_" + sId, _160).remove();
        var eDiv = document.createElement("DIV");
        jQ(eDiv).attr({ id: "attachment_" + sId, name: "attachment_" + sId });
        jQ(eDiv).append(_174("attachment", sId));
        jQ(eDiv).append(_174("attachment_" + sId + "_type", _170));
        jQ(eDiv).append(_174("attachment_" + sId + "_displayName", _16f));
        jQ(eDiv).append(_174("attachment_" + sId + "_deleted", _171 + ""));
        if (sMid) {
            jQ(eDiv).append(_174("attachment_" + sId + "__mid", sMid + ""));
        }
        if (_172) {
            jQ(eDiv).append(_174("attachment_" + sId + "_size", _172 + ""));
        }
        if (_173) {
            jQ(eDiv).append(_174("attachment_" + sId + "_enfUid", _173 + ""));
        }
        jQ(_160).append(eDiv);
    };
    function _175(sId, _176, _177) {
        _176 = _176 || "0";
        _177 = _177 || "";
        var _178 = "input[name=attachment][value=" + _179(sId) + "]";
        var _17a = "input[name=attachment_" + _179(sId) + "_security_level]";
        if (_177 && _177 != "") {
            _176 = CMUpload.CMCheckSecurityLevel(_177).level + "";
        }
        if (jQ(_17a, _160).length == 0) {
            jQ(_178, _160).after(_174("attachment_" + sId + "_security_level", _176 + ""));
        } else {
            jQ(_17a, _160).attr("value", _176 + "");
        }
    };
    function _17b(sId, _17c, _17d, _17e) {
        if (!sId) {
            return;
        }
        _17c = _17c || false;
        _17d = _17d || false;
        _17e = _17e || false;
        if (_17e) {
            var _17f = "#attachment_" + _179(sId);
            jQ(_17f, _160).remove();
        }
        var _180 = "input[name=attachment][value=" + _179(sId) + "]";
        var _181 = "input[name=attachment_" + _179(sId) + "_deleted]";
        if (jQ(_181, _160).length == 0) {
            jQ(_180, _160).after(_174("attachment_" + sId + "_deleted", _17c + ""));
        } else {
            jQ(_181, _160).attr("value", _17c + "");
        }
        if (_17c && _17d) {
            jQ(_180, _160).after(_174("attachment_" + sId + "_replaced", _17d + ""));
        }
    };
    function _182() {
        var _183 = [];
        jQ("div[name*=attachment]", _160).each(function () {
            var _184 = {};
            jQ(this).children("input").each(function () {
                _184[jQ(this).attr("name")] = jQ(this).attr("value");
            });
            _183.push(_184);
        });
        return _183;
    };
    function _185() {
        var _186 = [];
        jQ("div[name*=attachment]", _160).each(function () {
            var _187 = {};
            jQ(this).children("input").each(function () {
                var name = jQ(this).attr("name");
                var _188 = jQ(this).attr("value");
                if (name == "attachment") {
                    if (_188.match(/^[0-9]*$/)) {
                        _188 = parseInt(_188);
                        _187["id"] = _188;
                    } else {
                        return false;
                    }
                } else {
                    if (name.match(/type/)) {
                        _187["type"] = _188;
                    } else {
                        if (name.match(/deleted/)) {
                            _187["deleted"] = (_188 == "true");
                        } else {
                            if (name.match(/size/)) {
                                _187["size"] = parseInt(_188);
                            } else {
                                if (name.match(/displayName/)) {
                                    _187["fileName"] = _188;
                                }
                            }
                        }
                    }
                }
            });
            if (_187.hasOwnProperty("id")) {
                _186.push(_187);
            }
        });
        return _186;
    };
    function _174(_189, _18a) {
        var _18b = document.createElement("INPUT");
        jQ(_18b).attr({ type: "hidden", name: _189, value: _18a });
        return _18b;
    };
    function _179(s) {
        s += "";
        var i = s.indexOf(":");
        if (i > 0) {
            return s.substring(0, i) + "\\" + s.substring(i, s.length);
        } else {
            if (i == 0) {
                return "\\" + s;
            } else {
                return s;
            }
        }
    };
    return { initAttachInfo: _161, addAttachInfo: _16e, setSecurityLevel: _175, setDeleted: _17b, getValidSize: _163, checkAttrNameExit: _168, getAttachInfo: _182, getUploadSuccessObjs: _185 };
} ();
