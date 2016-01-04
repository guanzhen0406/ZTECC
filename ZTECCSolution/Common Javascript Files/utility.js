var isIE7 = navigator.appVersion.indexOf("MSIE 7.") != -1;
var isIE = navigator.appVersion.indexOf("MSIE") != -1;
var isChrome = navigator.userAgent.indexOf("Chrome") !== -1;
var isSafari = navigator.userAgent.indexOf("Safari") !== -1;

var loginUrl = "/Sys_User/Login";//默认页面,登录页面,Session过期默认跳转url,上线根据实际情况更改

var playerVersion = swfobject.getFlashPlayerVersion();
var canUseUplodify = (window.File && window.FileList && window.Blob && (window.FileReader || window.FormData)) || (playerVersion.major >= 9);



/*
 * 文件上传控件，兼容Flash和HTML5(苹果safari不支持Flash)
 */
(function ($) {
    $.fn.uploadify = function (method) {
        if (!canUseUplodify) {
            $(this).replaceWith('<input type="file" />');
            return;
        }
        var flashInstalled = (playerVersion.major >= 9);
        //不同名参数匹配
        //$.fn.uploadifive向$.fn.uploadifyFlash靠拢
        if (!flashInstalled) {
            method["fileType"] = method["fileTypeExts"];
            method["uploadScript"] = method["uploader"];
            method["onUploadComplete"] = method["onUploadSuccess"];
            method["onUploadFile"] = method["onUploadStart"];
            method["queueSizeLimit"] = 999;

            if (method["fileTypeExts"]) {
                var typeList = method["fileTypeExts"].split(';');
                var fileType = [];
                $.each(typeList, function (i, type) {
                    switch (type) {
                        case "*.xls":
                            fileType.push('application/vnd.ms-excel');
                            break;
                        case "*.xlsx":
                            fileType.push('application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
                            break;
                        case "*.doc":
                            fileType.push("application/msword");
                            break;
                        case "*.docx":
                            fileType.push("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                            break;
                        case "*.zip":
                            fileType.push("application/x-zip-compressed");
                            break;
                        case "*.rar":
                            //fileType.push("application/octet-stream");
                            break;
                        case "*.jpg": case "*.jpeg": case "*.img": case "*.gif": case "*.msg": case "*.png":
                            fileType.push('image/*');
                            break;
                        case "*.txt":
                            fileType.push("text/plain");
                            break;
                        case "*.htm": case "*.html":
                            fileType.push("text/html");
                            break;
                        default:
                            fileType.push(type.replace("*", ''));
                            break;
                    }
                });
                method["fileType"] = fileType.join(',');
            }
        }
        if (!flashInstalled) {
            return $.fn.uploadifive.apply(this, [method]);
        } else {
            return $.fn.uploadifyFlash.apply(this, [method]);
        }
    }

    $(function () {
        $('body').delegate('input[type="file"]', 'click', function () {
            if (!canUseUplodify) {
                Dialog.alert("为了当前系统功能的正常使用,请先安装Flash！");
                return false;
            }
        });
    });
})(jQuery);



/* 
 * IE不支持的扩展
 */
(function () {
    if (!Array.prototype.map) {
        Array.prototype.map = function (callback, thisArg) {
            var T, A, k;
            if (this == null) {
                throw new TypeError(' this is null or not defined');
            }

            // 1. Let O be the result of calling ToObject passing the |this| 
            //    value as the argument.
            var O = Object(this);

            // 2. Let lenValue be the result of calling the Get internal 
            //    method of O with the argument "length".
            // 3. Let len be ToUint32(lenValue).
            var len = O.length >>> 0;

            // 4. If IsCallable(callback) is false, throw a TypeError exception.
            // See: http://es5.github.com/#x9.11
            if (typeof callback !== 'function') {
                throw new TypeError(callback + ' is not a function');
            }

            // 5. If thisArg was supplied, let T be thisArg; else let T be undefined.
            if (arguments.length > 1) {
                T = thisArg;
            }

            // 6. Let A be a new array created as if by the expression new Array(len) 
            //    where Array is the standard built-in constructor with that name and 
            //    len is the value of len.
            A = new Array(len);

            // 7. Let k be 0
            k = 0;

            // 8. Repeat, while k < len
            while (k < len) {

                var kValue, mappedValue;

                // a. Let Pk be ToString(k).
                //   This is implicit for LHS operands of the in operator
                // b. Let kPresent be the result of calling the HasProperty internal 
                //    method of O with argument Pk.
                //   This step can be combined with c
                // c. If kPresent is true, then
                if (k in O) {

                    // i. Let kValue be the result of calling the Get internal 
                    //    method of O with argument Pk.
                    kValue = O[k];

                    // ii. Let mappedValue be the result of calling the Call internal 
                    //     method of callback with T as the this value and argument 
                    //     list containing kValue, k, and O.
                    mappedValue = callback.call(T, kValue, k, O);

                    // iii. Call the DefineOwnProperty internal method of A with arguments
                    // Pk, Property Descriptor
                    // { Value: mappedValue,
                    //   Writable: true,
                    //   Enumerable: true,
                    //   Configurable: true },
                    // and false.

                    // In browsers that support Object.defineProperty, use the following:
                    // Object.defineProperty(A, k, {
                    //   value: mappedValue,
                    //   writable: true,
                    //   enumerable: true,
                    //   configurable: true
                    // });

                    // For best browser support, use the following:
                    A[k] = mappedValue;
                }
                // d. Increase k by 1.
                k++;
            }

            // 9. return A
            return A;
        };
    }
})();



/*
通用方法加载--不需要页面加载就执行
*/
(function () {

    //判定当前是否在某个URL上--模糊匹配
    window.isCurrentPage = function (href) {
        var currentPage = location.href;
        return new RegExp(href).test(currentPage);
    }


    //全局错误捕捉
    window.onerror = function () { return true; };


    //ajax全局设定
    $.ajaxSetup({
        cache: false, //关闭AJAX相应的缓存
        complete: function (res) {
            if (res && /LoginBox/.test(res.responseText)) {
                //Dialog.alert('登录超时，请重新登录！', {
                //    okCallback: function () {
                //        window.top.location.reload();
                //    }
                //});
            }
            if (res && res.status == 500) {
                Dialog.alert('服务器错误!');
            }
        }
    });
    $(document).ajaxStart(function () {
        $('#ajaxLoading').show();
    }).ajaxStop(function (res) {
        $('#ajaxLoading').hide();
    });


    //文本框里按回车键搜索
    window.enterPress = function (e, func) {
        e = e || window.event;
        if (e.keyCode == 13) {
            func();
        }
    }


    //iframe自适应高度
    window.iframeHeight = function (iframe, initHeight) {
        if (iframe != null) {
            var id = iframe.id;
            var ifm = document.getElementById(id);
            if (!ifm) ifm = parent.document.getElementById(id);
            if (ifm == null) ifm = iframe;
            ifm.height = initHeight;
            var height = ifm.contentWindow.document.body.offsetHeight;
            iframe.height = Math.max(height, initHeight);
        }
    };
    window.iframeHeightAndParent = function (iframe, initHeight) {
        iframeHeight(iframe, initHeight);
        if (iframe != null) {
            var parentFrame = iframe.contentWindow.parent.document.getElementById("rightContent");
            if (parentFrame == null) {
                parentFrame = iframe.contentWindow.parent.parent.document.getElementById("rightContent");
            }
            if (parentFrame != null) {
                iframeHeight(parentFrame, initHeight);
            }
        }
    };


    //关闭非弹出窗
    window.closeWindow = function () {
        open(location, '_self').close();
        return false;
    }

    //滚动加载更多
    $.bindScrollLoadMore = function (container, hasParent, ajaxOption, callback) {
        if (typeof ajaxOption == "function") { callback = ajaxOption; ajaxOption = null; }
        var wnd = window;
        if (hasParent) wnd = parent;
        var isLoading = false,
            threshold = 100;
        $(wnd).scroll(function () {
            if (!container.length) return;
            if ($(document).height() - $(wnd).scrollTop() - $(wnd).height() > threshold) return;
            if (ajaxOption != null && isLoading) return;
            if (ajaxOption) {
                isLoading = true;
                $.ajax($.extend({}, ajaxOption, {
                    data: (function () {
                        if (typeof ajaxOption.data == "function") {
                            return ajaxOption.data();
                        }
                        return ajaxOption.data;
                    })(),
                    success: function (res) {
                        ajaxOption.success.call(this, res);
                        isLoading = false;
                    }
                }));
            }
            if (typeof callback == "function") callback.call();
        });
    };
    $.unbindScrollLoadMore = function () {
        var wnd = window;
        if (parent && parent.length) {
            wnd = parent;
        }
        $(wnd).unbind('scroll');
    };


    //弹出框Dialog
    var confirmTemplate = '<div class="bgWrapper" id="{{id}}"><div class="dialog"><div class="title">{{title}}</div><div class="content">{{content}}</div><div class="confirm"><div class="ok fl" tabindex="-1">{{okText}}</div><div class="cancel fl" >{{closeText}}</div><div class="clx"></div></div></div></div>';
    var alertTemplate = '<div class="bgWrapper" id="{{id}}"><div class="dialog"><div class="title">{{title}}</div><div class="content">{{content}}</div><div class="alert"><div class="ok button" tabindex="-1">{{okText}}</div></div></div></div>';
    var container = 'body';
    var dialog = function (template, opt) {
        var id = opt.id || new Date().getTime();
        opt.id = id;
        container = $(window.top.document).find(container);
        var html = template;
        template
            .match(/{{\s*[\w\.]+\s*}}/g)
            .map(function (x) {
                var value = opt[x.match(/[\w\.]+/)[0]] || "";
                html = html.replace(x, value);
            });
        container.append(html);
        var dialogView = container.find('#' + id);
        dialogView.show();
        $(".ok").focus();//ok按钮的tabindex属性设置为-1，然后将焦点移至这个按钮，避免弹出框弹出后按回车或空格再一次提交数据
        dialogView.find('.ok').bind('click.ok', function () {
            dialogView.remove();
            (opt.okCallback || function () { }).call();
            return true;
        });
        dialogView.find('.cancel').bind('click.cancel', function () {
            dialogView.remove();
            (opt.cancelCallback || function () { }).call();
        });
    };
    window.Dialog = {
        //弹出提示框
        alert: function (content, options) {
            var opt = $.extend({
                content: content,
                title: '信息',
                okText: '确定',
                okCallback: function () { }
            }, options);
            dialog(alertTemplate, opt);
        },
        //弹出确认框
        confirm: function (content, options) {
            var opt = $.extend({
                content: content,
                title: '信息',
                okText: '确定',
                closeText: '返回',
                okCallback: function () { },
                cancelCallback: function () { }
            }, options);
            dialog(confirmTemplate, opt);
        }
    };


    //tab切换
    $.fn.tab = function (options) {
        var opt = $.extend({}, {
            contentContainer: null,
            activeClass: 'current',
            usingAnimation: false
        }, options);
        var tabTrigger = $(this),                               //切换头部
            clickTrigger = $(this).children(),                  //点击切换
            contentContainer = opt.contentContainer;            //切换区域
        if (clickTrigger.length && opt.contentContainer) {
            $(tabTrigger).on('click.tab', " > *", function () {
                showHide($(this), opt.activeClass);
                switchContent(this, $(this).index());
            });
        }
        var switchContent = function (selector, index) {
            if (contentContainer && contentContainer.children().length) {
                var target = contentContainer.children().filter($($(selector).attr('data-target')));
                if (!target.length) {
                    target = contentContainer.children().eq(index);
                }
                showHide(target);
            }
        };
        var showHide = function (target, activeClass) {
            if (activeClass) {
                target.addClass(activeClass).siblings().removeClass(activeClass);
            } else {
                if (!isIE7 && opt.usingAnimation) {
                    target.fadeIn().siblings().fadeOut();
                } else {
                    target.show().siblings().hide();
                }
                if (opt.afterShow) { opt.afterShow(target); }
            }
        }
    };


    //验证数组里面是否包含某个值
    Array.prototype.contains = function (item) {
        return RegExp("\\b" + item + "\\b").test(this);
    };


    //登录
    window.innerLogin = function () {
        $('.errorMsg').text('');
        var adAccount = $.trim($("#AdAccount").val());
        if (adAccount === "") {
            $('#username_error').text($("#AdAccount").attr("placeholder"));
            $("#AdAccount").focus();
            return false;
        }
        var adPassword = $("#Password").val();
        if (adPassword === "") {
            $('#userpassword_error').text($("#Password").attr("placeholder"));
            $("#Password").focus();
            return false;
        }
        var imgCode = $.trim($("#imgCode").val());
        var isCheckVerifyCode = $.trim($("#IsCheckVerifyCode").val());// 是否检验验证码,True or False
        if (isCheckVerifyCode === "True" && imgCode === "") {
            $('#code_error').text($("#imgCode").attr("placeholder"));
            $("#imgCode").focus();
            return false;
        }
        return true;
    }
    //登录页错误信息
    var errorMessage = $.trim($("#ErrorMessage").val());
    if (errorMessage !== "") {
        $('#innerloginerror').text(errorMessage);
    }
    //获取验证码
    window.getImageCode = function () {
        $(".imgCode").attr("src", "/Sys_User/GetVeriCodeImg?random=" + Math.random());
        $("#imgCode").val("");
    }


})();


/*
文件上传--全局方法注册,静态绑定
*/
(function () {

    //通用文件删除
    window.commonFileDelete = function (filePath, uploadFileId, successCall) {
        $.ajax({
            url: '/Com_UploadFile/DeleteFile',
            type: "POST",
            data: { filePath: filePath, uploadFileId: uploadFileId },
            success: function (res) {
                (successCall || function () { })(res);
            }
        });
    }

    //文件上传成功回调
    window.uploadSuccessCallback = function (data, containerId) {
        var container = $('#' + containerId);
        if (data.IsSuccess) {
            if (container.length == 0) container = $('body');
            container.find('#fileUpload').hide();
            container.find('#uploadFile-queue').html('');
            container.find("#fileList").find('a').attr("href", "/Com_UploadFile/ExportFileToResponse?saveName=" + encodeURIComponent(data.SaveName) + "&downloadFileName=" + encodeURIComponent(data.FileName)).html(data.FileName);
            container.find('#attachmentPath').val(data.SaveName);
            container.find('#fileList').show();
        }
    }
})();


/*
通用方法加载--需要页面加载执行
*/
$(function () {

    //页面加载,隔行变色
    $(".detail_group table").each(function () {
        $(this).find('tbody tr:odd').addClass("gray");
    });


    //防止在只读控件按Backspace后退键
    $(document).keydown(function (e) {
        var doPrevent;
        var d = e.srcElement || e.target;
        if (e.keyCode == 8) {
            if (d.tagName.toUpperCase() == 'INPUT' || d.tagName.toUpperCase() == 'TEXTAREA') {
                doPrevent = d.readOnly || d.disabled;
            }
            else
                doPrevent = true;
        }
        else
            doPrevent = false;
        if (doPrevent)
            e.preventDefault();
        if (e.keyCode == 13 && d.tagName.toUpperCase() == 'INPUT') {
            return false;
        }
    });


    //表格控件注册
    (function () {
        window.registerDatGrid = function (tableID, options) {
            options = options || {};
            var container = $(tableID ? "#" + tableID : ".dataTableList > table"); //获取对应样式下的table容器
            if (container.length) {
                container.datagrid({
                    method: options.method == undefined ? "GET" : !!options.method, //请求类型
                    queryParams: options.queryParams || {}, //参数
                    pagination: options.pagination == undefined ? true : !!options.pagination, //是否分页
                    pageList: options.pageList == undefined ? [10, 20] : !!options.pageList,//分页页码
                    pageSize: options.pageSize == undefined ? 10 : !!options.pageSize, //每页默认条数
                    pageNumber: options.pageNumber == undefined ? 1 : !!options.pageNumber,//页码
                    rownumbers: options.rownumbers == undefined ? true : !!options.rownumbers, //行号
                    fitColumns: options.fitColumns == undefined ? false : !!options.fitColumns,//自适应列宽
                    fit: options.fit == undefined ? false : !!options.fit,//自适应
                    singleSelect: options.singleSelect == undefined ? true : !!options.singleSelect, //单选
                    checkOnSelect: options.checkOnSelect == undefined ? false : !!options.checkOnSelect,//选择checkbox则选择行
                    selectOnCheck: options.selectOnCheck == undefined ? false : !!options.selectOnCheck,//选择行则选择checkbox
                    columns: options.columns, //列
                    data: options.data,
                    rowStyler: options.rowStyler || function () { }, //样式
                    onClickRow: options.onClickRow || function (index, row) { }, //单击
                    onBeforeLoad: options.onBeforeLoad || function () { }, //加载前
                    onDblClickRow: options.onDblClickRow || function () { }, //双击
                    onLoadSuccess: options.onLoadSuccess || function () { } //加载成功
                });
                var pager = container.datagrid('getPager');
                $(pager).pagination({
                    beforePageText: '第',
                    afterPageText: '页&nbsp;共&nbsp;{pages}&nbsp;页',
                    displayMsg: '当前显示{from} - {to}&nbsp;条记录&nbsp;共&nbsp;{total}&nbsp;条记录'
                });
            }
        };
    })();


    //日期控件注册--My97DatePicker
    (function () {
        $('body').delegate('input.datePicker', 'click', function () {
            WdatePicker({
                el: $(this).attr('id'),
                dateFmt: $(this).attr('date-format'),
                maxDate: $(this).attr('date-max'),
                minDate: $(this).attr('date-min'),
                onpicked: window[$(this).attr('onpicked')] || function () { }//自定义事件
            });
        });
    })();


    //时间日期扩展
    (function () {
        //时间格式化
        Date.prototype.format = function (format) {
            if (!format) {
                format = "yyyy-MM-dd hh:mm:ss";
            }
            var o = {
                "M+": this.getMonth() + 1, // month
                "d+": this.getDate(), // day
                "h+": this.getHours(), // hour
                "m+": this.getMinutes(), // minute
                "s+": this.getSeconds(), // second
                "q+": Math.floor((this.getMonth() + 3) / 3), // quarter
                "S": this.getMilliseconds()// millisecond
            };
            if (/(y+)/.test(format)) {
                format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
            }
            for (var k in o) {
                if (new RegExp("(" + k + ")").test(format)) {
                    format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
                }
            }
            return format;
        };
        window.fomatDateTime = function (str) {
            return (new Date(parseInt(str.substring(str.indexOf('(') + 1, str.indexOf(')'))))).format("yyyy-MM-dd hh:mm:ss");
        }
        window.fomatDate = function (str) {
            return (new Date(parseInt(str.substring(str.indexOf('(') + 1, str.indexOf(')'))))).format("yyyy-MM-dd");
        }
        window.formatDateYearMonth = function (str) {
            return (new Date(parseInt(str.substring(str.indexOf('(') + 1, str.indexOf(')'))))).format("yyyy-MM");
        }

        Date.prototype.addDay = function (num) {
            this.setDate(this.getDate() + num);
            return this;
        }
        Date.prototype.addMonth = function (num) {
            var tempDate = this.getDate();
            this.setMonth(this.getMonth() + num);
            if (tempDate != this.getDate()) this.setDate(0);
            return this;
        }
        Date.prototype.addYear = function (num) {
            var tempDate = this.getDate();
            this.setYear(this.getYear() + num);
            if (tempDate != this.getDate()) this.setDate(0);
            return this;
        }

        //时间初始化
        window.DateTimeFormatter = function (value, row, index) {
            if (value == undefined) {
                return "";
            }
            return fomatDateTime(value);
        };
        //日期初始化
        window.DateTimeDateFormatter = function (value, row, index) {
            if (value == undefined) {
                return "";
            }
            return fomatDate(value);
        };
    })();


    //获取父页面
    (function () {
        window.getParentWindow = function () {
            var opener = window.opener;
            if (!opener) {
                //for IE window.opener=undefined
                opener = window.dialogArguments;
            }
            if (!opener) {
                opener = window.parent;
            }
            return opener;
        };
    })();


    //弹出页面
    (function () {
        var wrapper = $('[data-role="dialog-page"]');
        if (!wrapper.length && parent) wrapper = $(parent.document).find('[data-role="dialog-page"]');
        var iframe = wrapper.find('iframe');
        var fromClick = false;
        $(iframe).each(function () {
            $(this).load(function () {
                if (fromClick) {
                    $(this).parents('[data-role="dialog-page"]').show();
                    window.scrollTo(0, 0);
                }
            });
        });
        $('body').delegate('[data-detail-link]', 'click', function () {
            fromClick = true;
            iframe = wrapper.filter('#' + $(this).attr('data-wrapper-id')).find('iframe');
            if (iframe.length)
                iframe.attr('src', $(this).attr('data-detail-link'));
        });

        window.openDetailLink = function (src, wrapperID) {
            fromClick = true;
            iframe = wrapper.filter('#' + wrapperID).find('iframe');
            if (iframe.length)
                iframe.attr('src', src);
        };
    })();


    //菜单切换--Layout一二三级菜单高亮选中
    (function () {
        //一级菜单(顶部菜单)
        $('[data-role="menu"]').delegate('>li', 'click', function () {
            $(this).addClass('active').siblings().removeClass('active');
        });
        //二级菜单(顶部菜单)
        $('[data-role="subMenu"] li').bind('click', function () {
            $(this).addClass('active').siblings().removeClass('active');
        });
        //三级菜单(左侧菜单)切换
        $('.left-menu').delegate('.left-menu-item', 'click', function () {
            $('.left-menu-item').removeClass('active');
            $(this).addClass('active');
        });
    })();


    //下拉控件注册
    (function () {
        $('[data-role="select"]').hover(function () {
            var beforeHover = window[$(this).attr('on-hover')] || function () { };
            beforeHover();
            var dropDownList = $(this).find('.dropDownList');
            dropDownList.show();
            var val = $(this).find('[data-role="selectedValue"]').val();
            dropDownList.find('.option[data-value="' + val + '"]').addClass('active').siblings().removeClass('active');
        }, function () {
            $(this).find('.dropDownList').hide();
        });

        $('.dropDownList .option').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
        });

        $('.dropDownList').delegate(' > .option', 'click', function () {
            var text = $.trim($(this).text()),
                value = $.trim($(this).attr('data-value')),
                container = $(this).parents('[data-role="select"]'),
                dropDownList = $(this).parents('.dropDownList');

            container.find('[data-role="selectedText"]').text(text);
            container.find('[data-role="selectedValue"]').val(value).trigger('change');
            dropDownList.hide();
        });
    })();


    //全选多选框
    (function () {
        $('body').delegate('input[type="checkbox"][data-group][data-role="all"]', 'click', function () {
            var groupName = $(this).attr('data-group');
            var checkList = $('input[type="checkbox"][data-group="' + groupName + '"][data-role="item"]');
            var checked = $(this).prop('checked');
            checkList.prop('checked', checked);
        });
        $('body').delegate('input[type="checkbox"][data-group][data-role="item"]', 'click', function () {
            var groupName = $(this).attr('data-group');
            var checkbox = $('input[type="checkbox"][data-group="' + groupName + '"][data-role="all"]');
            var checked = $('input[type="checkbox"][data-group="' + groupName + '"][data-role="item"]:checked').length == $('input[type="checkbox"][data-group="' + groupName + '"][data-role="item"]').length;
            checkbox.prop('checked', checked);
        });
    })();


    //关闭弹出页
    (function () {
        //关闭弹出框
        window.closeTip = function (id) {
            if (!id)
                $(parent.document).find('[data-role="dialog-page"]').hide();
            $(parent.document).find('[data-role="dialog-page"]#' + id).hide();
        }
        //点击X -> 关闭弹出页
        $('.header').delegate('.icon-remove', 'click', function () {
            closeTip($(this).attr('data-close'));
        });
    })();


    //回到顶部
    (function () {
        $('body').delegate('.opePanel div,.toTop', 'click', function () {
            var target = $(this);
            target.addClass('active');
            window.setTimeout(function () {
                target.removeClass('active');
            }, 200);
        });
        $('body').delegate('.toTop', 'click', function () {
            window.scrollTo(0, 0);
        });
    })();


    //记住用户名
    (function () {
        $('input[type="radio"].radio').each(function () {
            $(this).data('isChecked', $(this).is(':checked'));
            $(this).bind('click.radio', function () {
                var checked = $(this).data('isChecked');
                if (!checked) $(this).attr('checked', 'checked');
                else $(this).removeAttr('checked');
                $(this).data('isChecked', !checked);
            });
        });
    })();


    //Safari去除placeholder的line-height
    (function () {
        if (isSafari) {
            $('input[placeholder]').css('line-height', 'normal');
        }
    })();

    //Session过期重新登录,若跳转地址不为空不为登录页,则登录后跳转过期的地址
    (function () {
        var topUrl = window.top.location.href;
        if (!(new RegExp(loginUrl).test(topUrl))) {
            if (isCurrentPage(loginUrl) && $('#ReturnUrl').val() != "") {
                window.top.location.reload();
            }
        }
    })();


    //表单提交成功后页面跳转
    (function () {

        //返回刷新父页面(含条件),相当于重新查询
        window.Return2ParentPageReload = function () {
            var parentWindow = getParentWindow();
            parentWindow.location.reload();
        }

        //刷新当前页面
        window.RefreshCurrentPage = function () {
            window.location.reload();
        }

        //后台返回结果不成功,有两种情况,1返回提示信息为空,则Session过期跳转登录页面,否则提示信息
        window.HandlerUnSuccessful = function (info) {
            if (info == undefined || info == '') {
                Dialog.alert("登录超时，请重新登录！", {
                    okCallback: function () {
                        window.top.window.location.href = loginUrl;
                    }
                });
            } else {
                Dialog.alert(info);
                return;
            }
        }

        //提交成功返回数据调用--仅关闭指定弹窗
        window.SuccessMsgCloseWindowById = function (data, iframeId) {
            if (data.IsSuccess) {
                Dialog.alert(data.ResultString, {
                    okCallback: function () {
                        closeTip(iframeId);
                    }
                });
            } else {
                HandlerUnSuccessful(data.ResultString);
            }
        }

        //提交成功返回数据调用--仅关闭所有弹窗
        window.SuccessMsgCloseWindowAll = function (data) {
            if (data.IsSuccess) {
                Dialog.alert(data.ResultString, {
                    okCallback: function () {
                        closeTip($(this).attr('data-close'));
                    }
                });
            } else {
                HandlerUnSuccessful(data.ResultString);
            }
        }

        //提交成功返回数据调用--弹出框用
        window.SuccessMsg = function (data) {
            if (data.IsSuccess) {
                Dialog.alert(data.ResultString, { okCallback: Return2ParentPageReload });
            } else {
                HandlerUnSuccessful(data.ResultString);
            }
        }

        //提交成功返回数据调用--刷新当前页
        window.SuccessMsgRefresh = function (data) {
            if (data.IsSuccess) {
                Dialog.alert(data.ResultString, { okCallback: RefreshCurrentPage });
            } else {
                HandlerUnSuccessful(data.ResultString);
            }
        }

        //关闭弹出页,刷新window.open的来源页面
        window.SuccessMsgCloseAndRefreshOrigin = function (data) {
            if (data.IsSuccess) {
                Dialog.alert(data.ResultString, {
                    okCallback: function () {
                        var parentWindow = getParentWindow();
                        window.close();
                        parentWindow.location.reload();
                    }
                });
            } else {
                HandlerUnSuccessful(data.ResultString);
            }
        }

    })();


    //通用文件上传示例--动态绑定--已经整合单文件，多文件
    (function () {

        //文件上传通用绑定方法
        $('input[type="file"]').each(function () {
            var id = $(this).attr('id');
            var target = $('#' + id);
            var uploadFun = function () {
                target.uploadify({
                    swf: '/Scripts/uploadify/uploadify.swf',//上传选择的图表,需要安装flash播放器
                    uploader: '/Com_UploadFile/UploadFileAction',//上传action
                    queueSizeLimit: 1,//每次只允许上传一个文件
                    formData: {
                        ASPSESSID: $('#ASPSESSID').val(), AUTHID: $('#AUTHID').val(),
                        'fileType': $("#fileType").val(), 'uploadFileId': $('#' + "file_" + id).find("#uploadFileId").val()
                    },
                    fileTypeExts: target.attr('data-filetype') || '*.xlsx;*.xls;*.rar;*.zip;*.doc;*.docx;*.jpg;*.jpeg;*.img;*.gif;*.msg;*.png;*.pdf;*.eml',//允许上传的文件类型[*.png...] '*.xlsx;*.xls',
                    fileTypeDesc: '非exe,bat等执行文件',//类型说明
                    multi: false,//只允许单选
                    fileSizeLimit: '10MB',//10M限制
                    buttonText: target.attr('data-message') || '附件上传',//按钮提示信息
                    onUploadSuccess: function (file, data, response) {
                        if (data) {
                            data = $.parseJSON(data);
                            var containerId = "file_" + id;
                            //默认回调函数,通用
                            uploadSuccessCallback(data, containerId);
                        }
                    },
                    onUploadStart: function (file) {

                    }
                });
            };
            if (isChrome) {
                window.setTimeout(function () {
                    uploadFun();
                }, 0);
            } else {
                uploadFun();
            }
        });

        //删除指定文件
        $('.fileList').delegate('.deleteFileListCommon', 'click', function () {
            var container = $(this).parent().parent();
            var uploadFileId = container.find('#uploadFileId').val();
            var filePath = container.find('#attachmentPath').val();
            if (filePath && uploadFileId) {
                commonFileDelete(filePath, uploadFileId, function (res) {
                    if (res && res.IsSuccess) {
                        container.find('#fileUpload').show();
                        container.find('#fileList').hide();
                        //存储路径清空
                        container.find('#attachmentPath').val('');
                        container.find('.uploadify-queue-item').remove();
                    }
                });
            }
        });

    })();


    //通用表单校验
    (function () {

        //非空验证
        window.EmptyValidate = function (container, excludeIDs) {
            var result = true;
            var validateList = container.find('span.redBorder');
            var displayError = function (span, input, isError) {
                if (isError) {
                    $(span).show();
                    $(input).addClass('errorBorder');
                } else {
                    $(span).hide();
                    $(input).removeClass('errorBorder');
                }
            };
            var clearError = function () {
                validateList.hide();
                container.find('span.typeError').hide();
                container.find('input[type="text"]').removeClass('errorBorder');
            };
            clearError(container);
            $.each(validateList, function (index, span) {
                var item = $(span).parent().find('input[type="text"],select,textarea');
                if (item.is(':visible')) {
                    var id = $(item).attr('id');
                    excludeIDs = excludeIDs || [];
                    if (!excludeIDs.contains(id)) {
                        var value = $.trim($(item).val());
                        if (item.hasClass('comboboxUI')) {
                            value = $('#' + id).combobox('getValue');
                        }
                        if (item.hasClass('combogridUI')) {
                            value = $('#' + id).combogrid('getValue');
                        }
                        result = result && !!value;
                        displayError(span, item, !value);
                    }
                }
            });
            return result;
        };

        //银行卡号格式验证
        window.checkBankAccount = function (key) {
            return isNaN(key) === false;
        }

        //验证年龄，面积等(必须为正整数)
        window.checkPositiveInteger = function (val) {
            var re = /^[1-9]\d*$/;
            return re.test(val);
        };

        //验证年龄或者面积
        window.checkAgeOrAreaType = function (eleId, errorEleId) {
            eleId = eleId || "Age";
            errorEleId = errorEleId || "AgeTypeError";
            var ele = $('#' + eleId);
            var errorEle = $('#' + errorEleId);
            var eleVal = $.trim(ele.val());
            if (eleVal) {
                if (!checkPositiveInteger(ele.val())) {
                    errorEle.show();
                    ele.addClass('errorBorder');
                    return false;
                } else {
                    errorEle.hide();
                    ele.removeClass('errorBorder');
                    return true;
                }
            }
            return true;
        }

        //验证银行卡号格式
        window.checkBankAccountType = function (eleId, errorEleId) {
            eleId = eleId || "AccountNumber";
            errorEleId = errorEleId || "AccountNumberTypeError";
            var ele = $('#' + eleId);
            var errorEle = $('#' + errorEleId);
            var eleVal = $.trim(ele.val());
            if (eleVal) {
                if (!checkBankAccount(ele.val())) {
                    errorEle.text("账号格式不正确").show();
                    ele.addClass('errorBorder');
                    return false;
                } else {
                    errorEle.text("").hide();
                    ele.removeClass('errorBorder');
                    return true;
                }
            }
            return true;
        }

        //验证电话号码
        window.checkPhone = function (phoneValue) {
            var re = /^0\d{2,3}-?\d{7,8}$/;
            return re.test(phoneValue);
        };

        //验证电话号码格式
        window.checkPhoneType = function (eleId, errorEleId) {
            eleId = eleId || "Phone";
            errorEleId = errorEleId || "PhoneTypeError";
            var ele = $('#' + eleId);
            var errorEle = $('#' + errorEleId);
            var eleVal = $.trim(ele.val());
            if (eleVal) {
                if (checkPhone(eleVal) || checkMobilePhone(eleVal)) {
                    errorEle.hide();
                    ele.removeClass('errorBorder');
                    return true;
                } else {
                    errorEle.show();
                    ele.addClass('errorBorder');
                    return false;
                }
            }
            return true;
        }

        //从身份证中获取生日
        window.getBirthdayFromIDCard = function (value) {
            if (validateFirIdCard(value)) {
                var sId = '';
                if (value.length == 15)
                    sId = idCardUpdate(value);
                else
                    sId = value;
                sId = sId.replace(/x$/i, "a");
                var sBirthday = sId.substr(6, 4) + "-" + Number(sId.substr(10, 2))
                    + "-" + Number(sId.substr(12, 2));
                var d = new Date(sBirthday.replace(/-/g, "/"));
                var month = d.getMonth() + 1;
                if (month < 10) {
                    month = "0" + month;
                }
                var day = d.getDate();
                if (day < 10) {
                    day = "0" + day;
                }
                return d.getFullYear() + "-" + month + "-" + day;
            } else {
                return "";
            }
        }

        //从身份证中获取性别
        window.getGenderFromIDCard = function (value) {
            if (validateFirIdCard(value)) {
                if (parseInt(value.charAt(16)) % 2) return "男";
                return "女";
            } else {
                return "";
            }
        }

        //验证身份证
        window.validateFirIdCard = function (value) {
            var iSum = 0;
            var sId;
            var aCity = {
                11: "北京",
                12: "天津",
                13: "河北",
                14: "山西",
                15: "内蒙",
                21: "辽宁",
                22: "吉林",
                23: "黑龙",
                31: "上海",
                32: "江苏",
                33: "浙江",
                34: "安徽",
                35: "福建",
                36: "江西",
                37: "山东",
                41: "河南",
                42: "湖北",
                43: "湖南",
                44: "广东",
                45: "广西",
                46: "海南",
                50: "重庆",
                51: "四川",
                52: "贵州",
                53: "云南",
                54: "西藏",
                61: "陕西",
                62: "甘肃",
                63: "青海",
                64: "宁夏",
                65: "新疆",
                71: "台湾",
                81: "香港",
                82: "澳门",
                91: "国外"
            };
            //如果输入的为15位数字,则先转换为18位身份证号
            if (value.length == 15)
                sId = idCardUpdate(value);
            else
                sId = value;
            if (!/^\d{17}(\d|x)$/i.test(sId)) {
                return false;
            }
            sId = sId.replace(/x$/i, "a");
            //非法地区
            if (aCity[parseInt(sId.substr(0, 2))] == null) {
                return false;
            }
            var sBirthday = sId.substr(6, 4) + "-" + Number(sId.substr(10, 2))
                    + "-" + Number(sId.substr(12, 2));
            var d = new Date(sBirthday.replace(/-/g, "/"));
            //非法生日
            if (sBirthday != (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d
                    .getDate())) {
                return false;
            }
            for (var i = 17; i >= 0; i--) {
                iSum += (Math.pow(2, i) % 11) * parseInt(sId.charAt(17 - i), 11);
            }
            if (iSum % 11 != 1) {
                return false;
            }
            return true;
        }

        //验证身份证格式
        window.checkIDCardType = function (eleId, errorEleId) {
            eleId = eleId || "IDCard";
            errorEleId = errorEleId || "IDCardTypeError";
            var ele = $('#' + eleId);
            var errorEle = $('#' + errorEleId);
            var eleVal = $.trim(ele.val());
            if (eleVal) {
                if (!validateFirIdCard(ele.val())) {
                    errorEle.show();
                    ele.addClass('errorBorder');
                    return false;
                } else {
                    errorEle.hide();
                    ele.removeClass('errorBorder');
                    return true;
                }
            }
            return true;
        }

        //输入框最大输入长度限制
        window.checkTextAreaMaxLength = function (textBox, e, length) {
            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;
            var maxLength = parseInt(mLen);
            var checkResult = checkSpecialKeys(e);
            if (!checkResult) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                    {
                        e.returnValue = false;
                        return false;
                    }
                    else//Firefox
                        e.preventDefault();
                }
            }
            return checkResult;
        };

        //校验特殊字符
        window.checkSpecialKeys = function (e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 35 && e.keyCode != 36 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }

        //验证手机号码
        window.checkMobilePhone = function (moilePhoneValue) {
            var re = /^1\d{10}$/;
            return re.test(moilePhoneValue);
        };

        //验证手机号码格式
        window.checkMobileType = function (eleId, errorEleId) {
            eleId = eleId || "Mobile";
            errorEleId = errorEleId || "MobileTypeError";
            var ele = $('#' + eleId);
            var errorEle = $('#' + errorEleId);
            var eleVal = $.trim(ele.val());
            if (eleVal) {
                if (!checkMobilePhone(eleVal)) {
                    errorEle.show();
                    ele.addClass('errorBorder');
                    return false;
                } else {
                    errorEle.hide();
                    ele.removeClass('errorBorder');
                    return true;
                }
            }
            return true;
        }

        //邮箱格式验证
        window.checkEmail = function (key) {
            return /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/.test(key);
        }

        //验证邮箱格式
        window.checkEmailType = function (eleId, errorEleId) {
            eleId = eleId || "Email";
            errorEleId = errorEleId || "EmailTypeError";
            var ele = $('#' + eleId);
            var errorEle = $('#' + errorEleId);
            var eleVal = $.trim(ele.val());
            if (eleVal) {
                if (!checkEmail(ele.val())) {
                    errorEle.show();
                    ele.addClass('errorBorder');
                    return false;
                } else {
                    errorEle.hide();
                    ele.removeClass('errorBorder');
                    return true;
                }
            }
            return true;
        }

        //只允许输入数字
        window.focusNumberInput = function () {
            var keycode = event.keyCode;
            if (keycode > 57 || keycode < 47) {
                if (keycode == 45) {
                    return true;
                }
                return false;
            }
            return true;
        };

        //禁止中文
        window.forbiddenChinese = function (element) {
            var value = $(element).val();
            if (value) {
                $(element).val(value.replace(/[^\w\.\-\/]/ig, ''));
            }
            return true;
        }

    })();

});