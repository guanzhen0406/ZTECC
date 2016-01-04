$(function () {
    var template = $('.storeListContent').children().first().clone();
    var appendItem = function (list, clear, lastPage) {
        var container = $('.storeListContent');

        var result = [];
        $.each(list, function (i, item) {

            $(template).find('.levelEvaluate').removeClass().addClass('levelEvaluate icons');
            $(template).find('[data-source]').each(function () {
                var propertyName = $(this).attr('data-source');
                $(this).text(item[propertyName] || '').parent().attr('title', item[propertyName] || '');
            });

            $(template).find('.CityAreaName').text(item["CityAreaName"] + "-" + item["ProvinceCityName"]).attr(item["CityAreaName"] + "-" + item["ProvinceCityName"]);
            $(template).find('.levelEvaluate').addClass('icon_star' + (item["CountStar"] || 0));
            $(template).find('.storeIcon').removeClass().addClass('storeIcon icons icon_' + item["BrandTip"]);
            $(template).find('.attentionSeclect').attr('data-hotel', item["HotelUnifyNo"]).attr('data-selected', item["IsMyFavorite"]).text(item["IsMyFavorite"] == 1 ? "取消关注" : "关注");
            $(template).find('a.hotelDataDetail').attr('href', '/MainData/FranchiseeHotelDataDetail?nccode=' + item["NCCode"]);

            result.push('<li>' + $(template).html() + '</li>');
        });

        if (clear) container.html('');

        $('.StoreList').find('.notFound').remove();
        if (list && list.length) {
            container.append(result.join(''));
        } else {
            if (!lastPage) {
                $('.StoreList').append('<div class="notFound"><div class="tipImage"><img src="/Images/404.png"></div><div class="tipContent">您暂时没有关注符合筛选条件的门店！</div></div>');
            }
        }
    };

    //页面下滚加载更多
    (function () {
        window.registerScrollLoad = function (from) {

            $.unbindScrollLoadMore();

            var currentIndex = 1;
            var pageSize = 10, container = $('.storeListContent');
            var firstSize = 10;
            if (container.children().length < firstSize) return;
            $.bindScrollLoadMore(container, false, {
                url: '/Home/LoadHotelRegionMore',
                data: function () { return { pageIndex: currentIndex + 1, pageSize: pageSize, key: $('#key').val(), from: from || $('#from').val() } },
                success: function (res) {

                    if (!res) return;
                    appendItem(res.result, false, Math.ceil(res.total / pageSize) == currentIndex);

                    currentIndex = currentIndex + 1;

                    if (Math.ceil(res.total / pageSize) < currentIndex) {
                        $.unbindScrollLoadMore();
                    }
                }
            });

        };
        registerScrollLoad();
    })();

    //店长下拉展示
    (function () {
        $(function () {
            $('.selectBlockTitle').bind('click', function() {
                $('.selectBlockTitle + .allStoreLeader').toggle();
            });
        });
    })();

    //我的关注筛选条件下拉展示
    (function () {
        $(function () {
            $('.storeListTitle').bind('click', function () {
                $('.storeListTitle + .myAttention').toggle();
            });

            $('body').bind('click', function (e) {
                var target = $(e.target);
                if (!(target.hasClass('myAttention') || target.parents('.myAttention').length
                    || target.hasClass('storeListTitle') || target.parents('.storeListTitle').length)) {
                    $('.storeListTitle + .myAttention').hide();
                }
            });
        });
    })();

    //搜索条件筛选
    (function () {
        $(function () {

            var selected = function (element) {
                unselected($(element).parents('.selectBlock'));
                $(element).addClass('active');
                $(element).append('<i class="icon-remove"></i>');
            };

            var unselected = function (container) {
                container.find('li').removeClass('active');
                container.find('li i.icon-remove').remove();
            };

            $('[data-group] li').bind('click.select', function () {
                selected(this);
                filterHotelList(this);
            });

            $('[data-group] li').delegate('i', 'click.unselect', function (e) {
                var li = $(this).parent();
                unselected($(this).parents('.selectBlock'));
                filterHotelList(li);
                return false;
            });

            var filterHotelList = function (element) {
                var selected = $(element).parents('.selectList').find('li.active');
                var data = { key: $('#key').val(), from: $('#from').val() };
                $.each(selected, function (i, ele) {
                    var key = $(ele).parents('[data-group]').attr('data-group');
                    var value = $.trim($(ele).text());
                    data[key] = value;
                });

                $.ajax({
                    url: '/Home/FilterHotelRegion',
                    data: data,
                    success: function (res) {
                        appendItem(res.result, true);
                        $('#totalCount').text(res.total);
                        registerScrollLoad("Filter");
                    }

                });
            };

        });

    })();

    //操作我的关注（添加/删除）
    (function () {
        window.addMyFavorites = function (hotel) {

            $.ajax({
                url: '/Home/AddMyFavorites',
                type: 'POST',
                data: { hotel: hotel },
                success: function (res) {
                    if (!res) { Dialog.alert('关注失败！', { title: "错误" }); return; }
                    var container = $('#favoritesSuccess');
                    if (container.length) {
                        container.detach().appendTo('[data-role="body"]').show().find('#favoritesCount').text(res.count);
                    } else {
                        Dialog.alert("您已经关注" + res.count + "家门店。", {
                            title: "关注成功", okCallback: function () {
                                $('[data-hotel="' + hotel + '"]').attr('data-selected', 1).text('取消关注');
                            }
                        });
                    }
                    $('[data-hotel="' + hotel + '"]').attr('data-selected', 1).text('取消关注');
                },
                error: function () {
                    Dialog.alert('关注失败！', { title: "错误" });
                }
            });
        };

        window.deleteMyFavorites = function (hotel) {

            Dialog.confirm("确定取消关注该门店吗？", {
                title: "取消门店关注",
                okCallback: function () {
                    $.ajax({
                        url: '/Home/DeleteMyFavorites',
                        type: 'POST',
                        data: { hotel: hotel },
                        success: function () {
                            window.location.reload();
                            //$('[data-hotel="' + hotel + '"]').attr('data-selected', 0).text('关注');
                        },
                        error: function () {
                            Dialog.alert('取消关注失败！', { title: "错误" });
                        }
                    });
                }
            });
        }
    })();

    //我的关注
    (function () {
        $('.storeListContent').delegate('.attentionSeclect', 'click', function () {

            var hasFavorites = $(this).attr('data-selected') == 1,
                hotel = $(this).attr('data-hotel');

            if (!hasFavorites) addMyFavorites(hotel);
            else deleteMyFavorites(hotel);
        });

        $('#favoritesSuccess').delegate('.icon-remove', 'click', function () {
            $('#favoritesSuccess').hide();
        });
    })();




    (function () {
        //风险提示 文件删除 批量删除
        $('.fileList').delegate('.deleteFileListRisk', 'click', function () {
            var container = $(this).parent().parent();

            var uploadFileId = container.find('#uploadFileId').val();
            var filePath = container.find('#attachmentPath').val();
            if (filePath && uploadFileId) {
                KaopingFileDelete(filePath, uploadFileId, function (res) {
                    if (res && res.Success) {
                        container.find('#fileUpload').show();
                        container.find('#fileList').hide();
                        //存储路径清空
                        container.find('#attachmentPath').val('');
                        //uploadify-queue-item
                        container.find('.uploadify-queue-item').remove();
                    }
                });
            }
        });
    })();
});


//对外开店流程切换
(function ($) {
    $(function () {
        $('[data-role="processArea"]').delegate('li', 'click', function () {
            //  var target = $(this).attr('[data-target]');
            var target = $(this).attr('name');
            $(this).addClass('active').siblings().removeClass('active');
            $("#" + target).addClass('active').siblings().removeClass('active');

            iframeHeight($(parent.document).find('iframe')[0], 515, true);

        });
    });

})(jQuery);

/*
 * 文件删除操作
 */
(function () {
    window.KaopingFileDelete = function (filePath, uploadFileId, successCall) {
        $.ajax({
            url: '/File/DeleteFile',
            type: "POST",
            data: { filePath: filePath, uploadFileId: uploadFileId },
            success: function (res) {
                (successCall || function () { })(res);
            }
        });
    }

    //风险提示上传3文件后,提交
    window.AutoSubmitData = function () {
        var reportYear = $.trim($("input[name='reportYear']:checked").val());
        var quarterNo = $.trim($("input[name='quarterNo']:checked").val());
        var up1 = $.trim($("#file_up1").find("#attachmentPath").val());
        var up2 = $.trim($("#file_up2").find("#attachmentPath").val());
        var up3 = $.trim($("#file_up3").find("#attachmentPath").val());
        if (reportYear == '') {
            Dialog.alert("年份必须选择");
            return;
        }
        if (quarterNo == '') {
            Dialog.alert("季度必须选择");
            return;
        }
        if (up1 == '') {
            Dialog.alert("门店内控经理,主管表必须上传");
            return;
        }
        if (up2 == '') {
            Dialog.alert("加盟店应收账款风险表必须上传");
            return;
        }
        if (up3 == '') {
            Dialog.alert("加盟店财务风险表必须上传");
            return;
        }
        var perUploadid = $.trim($("#file_up1").find('#uploadFileId').val());
        var financeUploadid = $.trim($("#file_up2").find('#uploadFileId').val());
        var hotelUploadid = $.trim($("#file_up3").find('#uploadFileId').val());

        //保存上传的数据
        var url = "/Home/SaveRiskTipsImportData/";
        $.post(url, { reportYear: reportYear, quarterNo: quarterNo, perUploadid: perUploadid, financeUploadid: financeUploadid, hotelUploadid: hotelUploadid }, function (data) {
            if (data.IsSuccess) {
                Dialog.alert(data.ResultString);
                $(".searchButton").hide();
            } else {
                Dialog.alert(data.ResultString);
                return;
            }
        });
    }
})();

//风险提示
(function ($) {
    $(function () {
        //单文件上传
        if (isCurrentPage("/Home/RiskRemind")) {

            //文件上传成功回调
            (function () {
                window.loadFileList = function (data, containerID) {
                    var container = $('#' + containerID);
                    if (data.Success) {
                        if (container.length == 0) container = $('body');
                        container.find('#fileUpload').hide();
                        container.find('#uploadFile-queue').html('');
                        container.find("#fileList").find('a').attr("href", "/File/ExportFileToResponse?saveName=" + encodeURIComponent(data.SaveName) + "&downloadFileName=" + encodeURIComponent(data.FileName)).html(data.FileName);
                        container.find('input#attachmentPath').val(data.SaveName);
                        container.find('#fileList').show();
                    }
                }
            })();

            $('input[type="file"]').each(function () {
                var target = $('#' + $(this).attr('id')),
                    id = $(this).attr('id'),
                    onloadSuccess = window[target.attr('data-loadSuccess')] || function () { };

                var uploadFN = function () {
                    target.uploadify({
                        swf: '/Scripts/uploadify/uploadify.swf',
                        uploader: '/File/UploadKaoping',                                                               //上传action
                        queueSizeLimit: 1,                                                                      //每次只允许上传一个文件
                        formData: {
                            ASPSESSID: $('#ASPSESSID').val(), AUTHID: $('#AUTHID').val(),
                            'fileType': $("#fileType").val(), 'uploadFileId': $('#' + "file_" + id).find("#uploadFileId").val()
                        },
                        fileTypeExts: target.attr('data-filetype') || '*.xlsx;*.xls;',                                             //允许上传的文件类型[*.png...] '*.xlsx;*.xls',
                        fileTypeDesc: 'xls,xlsx格式', //类型说明
                        multi: false,//只允许单选
                        fileSizeLimit: '4MB',//4M限制
                        buttonText: target.attr('data-message') || '上传',                                   //按钮提示信息
                        onUploadSuccess: function (file, data, response) {
                            if (data) {
                                data = $.parseJSON(data);
                                var containId = "file_" + id;
                                var uploadfileId = $('#' + containId).find("#uploadFileId").val();
                                //var status = checkImportDataPreview(uploadfileId, containId);
                                //if (status == "ok") {
                                //    onloadSuccess(data, containId);
                                //} else {
                                //    Dialog.alert("您当前上传的excel格式有误，请检查后再上传!");
                                //    return;
                                //}
                                var url = "/Home/CheckImportDataPreview/";
                                $.post(url, { uploadfileId: uploadfileId, reportType: containId }, function (res) {
                                    if (res.IsSuccess) {
                                        onloadSuccess(data, containId);
                                    } else {
                                        Dialog.alert("您当前上传的excel格式有误，请检查后再上传!");
                                        return;
                                    }
                                });
                            }
                        },
                        onUploadStart: function (file) {
                        }
                    });
                };

                ////财务数据导入上传excel后回调
                //window.checkImportDataPreview = function (uploadfileId, reportType) {
                //    var url = "/Home/CheckImportDataPreview/";
                //    $.post(url, { uploadfileId: uploadfileId, reportType: reportType }, function (data) {
                //        if (data.IsSuccess) {
                //            return "ok";
                //        } else {
                //            return "no";
                //        }
                //    });
                //}

                if (isChrome) {
                    window.setTimeout(function () {
                        uploadFN();
                    }, 0);
                } else {
                    uploadFN();
                }
            });
        }
    });

})(jQuery);