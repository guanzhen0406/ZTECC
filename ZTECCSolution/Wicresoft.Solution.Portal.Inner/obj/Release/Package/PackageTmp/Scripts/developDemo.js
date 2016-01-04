$(function () {

    //日期控件示例--自定义事件
    if (isCurrentPage("/Com_DevelopDemo/DatePickerDemo")) {
        //非当前月,导入按钮隐藏,日期控件选择的自定义事件
        window.AdjustInportBtn = function (dp) {
            //选择完日期后自定义事件,获取选取的日期
            var date = dp.cal.getDateStr();
            var now = new Date().format("yyyy-MM");
            if (now != date) {
                Dialog.alert("not current month");
            } else {
                Dialog.alert("current month");
            }
        }
    }

    //下拉控件示例--范围切换
    if (isCurrentPage("/Com_DevelopDemo/DropdownlistDemo")) {
        $('#reportRangeSelect').change(function () {
            var selectedVal = $(this).val();
            $('[data-for]').hide();
            //季度
            if (selectedVal == 0) {
                $('[data-for="quarterSelect"]').show();
            }
            //半年
            if (selectedVal == 1) {
                $('[data-for="halfYearSelect"]').show();
            }
            //非良性搜索下拉框半年度和年度展示
            if (selectedVal == 1 || selectedVal == 2) {
                $('[data-for="isGoodHotelSelect"]').show();
            }
        });
    }

    //弹出提示框示例
    if (isCurrentPage("/Com_DevelopDemo/DialogAlertDemo")) {
        window.AutoSubmitData = function () {
            Dialog.alert("0. alert success", {
                //重写回调函数
                okCallback: function () {
                    Dialog.alert("1. we meet again");
                }
            });
        }
    }

    //弹出确认框示例
    if (isCurrentPage("/Com_DevelopDemo/DialogConfirmDemo")) {
        window.AutoSubmitData = function () {
            Dialog.confirm("0. please select ok or cancel?", {
                //重写回调函数
                okCallback: function () {
                    Dialog.alert("1. ok ");
                },
                cancelCallback: function () {
                    Dialog.alert("1. cancel ");
                }
            });
        }
    }

    //数据列表示例,数据格式化示例
    if (isCurrentPage("/Com_DevelopDemo/GetModelListDemo") || isCurrentPage("/Com_DevelopDemo/InitialDataFormatterDemo")) {
        window.registerDatagridOfFirstLevelList = function () {
            registerDatGrid('firstLevelListDataTbl', {
                url: "/Com_DevelopDemo/GetMenuListByParentIdAndPage",
                queryParams: {
                    parentId: -1
                },
                pageList: [5, 10], //分页页码
                pageSize: 10, //每页默认条数
                onBeforeLoad: function (param) {
                    //默认排序
                    if (!param.sort) param.sort = "Sort";
                    if (!param.order) param.order = "asc";
                },
                onLoadSuccess: function () {
                    iframeHeight($(parent.document).find('#rightContent')[0], 460);
                }
            });
        };
        registerDatagridOfFirstLevelList();
    }


});


/*
初始化,数据,链接
*/
(function () {
    //数据格式化示例
    if (isCurrentPage("/Com_DevelopDemo/InitialDataFormatterDemo")) {
        //操作按钮
        window.OperateFormatter = function (value, row, index) {
            return '<input type="button" class="SysM_detailButton button"  value="删除" onclick="DeleteMenu(' + row["ID"] + ')" />&nbsp&nbsp&nbsp<input data-wrapper-id="bgWrapperOne" data-detail-link="/Sys_Menu/ChildMenuList/' + row["ID"] + '" type="button" class="SysM_detailButton button" value="二级菜单">';
        };
        //菜单名称[添加链接到详情编辑]
        window.NameFormatter = function (value, row, index) {
            if (value) {
                return '<a href="#" data-wrapper-id="bgWrapperTwo" data-detail-link="/Sys_Menu/EditMenu/' + row["ID"] + '" class="normal">' + value + '</a>';
            }
            return "";
        };
        //菜单是否一二级菜单,动态菜单
        window.IsFirstOrSecondMenuFormatter = function (value, row, index) {
            if (value == false) {
                return "否";
            }
            return "是";
        };
    }

})();

/*
文件上传
*/
(function () {
    $(function () {
        //文件上传--回调重写
        if (isCurrentPage("/Com_DevelopDemo/OneFileUploadDemo")) {

            //若需要定制上传成功后的操作,可在此重写上传的绑定,重写成功之后的回调函数

        }
    });
})();
