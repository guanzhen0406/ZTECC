/*
First系统管理表格数据绑定
*/
$(function () {

    //1--菜单
    (function () {
        //4.1一级菜单列表表格控件绑定
        if (isCurrentPage("/AssetManage/ProjectBudgetList")) {
            window.registerDatagridOfFirstLevelList = function () {
                registerDatGrid('firstLevelListDataTbl', {
                    url: "/Sys_Menu/GetMenuListByParentIdAndPage",
                    queryParams: {
                        parentId: -1
                    },
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

    })();

});


/*
Second初始化链接
*/
(function () {

    //菜单是否一二级菜单,动态菜单
    window.IsFirstOrSecondMenuFormatter = function (value, row, index) {
        if (value == false) {
            return "否";
        }
        return "是";
    };

    //1菜单
    //1.1一级菜单列表
    if (isCurrentPage("/AssetManage/ProjectBudgetList")) {
        //操作按钮
        window.OperateFormatter = function (value, row, index) {
            return '<input type="submit" class="SysM_detailButton button"  value="删除" onclick="DeleteMenu(' + row["ID"] + ')" />&nbsp&nbsp&nbsp<input data-wrapper-id="bgWrapperOne" data-detail-link="/Sys_Menu/ChildMenuList/' + row["ID"] + '" type="submit" class="SysM_detailButton button" value="编辑">';
        };
        //菜单名称[添加链接到详情编辑]
        window.NameFormatter = function (value, row, index) {
            if (value) {
                return '<a href="#" data-wrapper-id="bgWrapperTwo" data-detail-link="/Sys_Menu/EditMenu/' + row["ID"] + '" class="normal">' + value + '</a>';
            }
            return "";
        };
    }

})();
