/*
First系统管理表格数据绑定
*/
$(function () {

    //1--菜单
    (function () {
        //4.1一级菜单列表表格控件绑定
        if (isCurrentPage("/Sys_Menu/FirstLevelList")) {
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

        //4.2二级菜单列表表格控件绑定
        if (isCurrentPage("/Sys_Menu/ChildMenuList")) {
            window.registerDatagridOfChildMenuList = function () {
                registerDatGrid('childMenuListDataTbl', {
                    url: "/Sys_Menu/GetMenuListByParentIdAndPage",
                    queryParams: {
                        parentId: $.trim($("#ParentID").val())
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
            registerDatagridOfChildMenuList();
        }


        //4.3三级菜单列表表格控件绑定
        if (isCurrentPage("/Sys_Menu/ThirdMenuList")) {
            window.registerDatagridOfThirdMenuList = function () {
                registerDatGrid('thirdMenuListDataTbl', {
                    url: "/Sys_Menu/GetMenuListByParentIdAndPage",
                    queryParams: {
                        parentId: $.trim($("#ParentID").val())
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
            registerDatagridOfThirdMenuList();
        }

        //4.4功能列表表格控件绑定,四级菜单
        if (isCurrentPage("/Sys_Menu/FunctionList")) {
            window.registerDatagridOfFunctionList = function () {
                registerDatGrid('functionListDataTbl', {
                    url: "/Sys_Menu/GetMenuListByParentIdAndPage",
                    queryParams: {
                        parentId: $.trim($("#ParentID").val())
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
            registerDatagridOfFunctionList();
        }

    })();

    //2--角色(菜单组)
    (function () {
        //5.1角色(菜单组)列表表格控件绑定
        if (isCurrentPage("/Sys_Role/GroupList")) {
            window.registerDatagridOfGroupList = function () {
                registerDatGrid('groupListDataTbl', {
                    url: "/Sys_Role/GetGroupListByPage",
                    queryParams: {
                    },
                    onBeforeLoad: function (param) {
                        //默认排序
                        if (!param.sort) param.sort = "ID";
                        if (!param.order) param.order = "desc";
                    },
                    onLoadSuccess: function () {
                        iframeHeight($(parent.document).find('#rightContent')[0], 460);
                    }
                });
            };
            registerDatagridOfGroupList();
        }

        //5.2设置用户已有用户列表表格控件绑定
        if (isCurrentPage("/Sys_Role/SetUser2GroupAlreadyList")) {
            window.registerDatagridOfSetUser2GroupAlreadyList = function () {
                registerDatGrid('setUser2GroupAlreadyListDataTbl', {
                    url: "/Sys_Role/GetUserListOfGroupByPage",
                    queryParams: {
                        key: $.trim($('#KeyWord').val()),//关键字
                        groupId: $.trim($("#groupId").val())//角色id
                    },
                    onBeforeLoad: function (param) {
                        //默认排序
                        if (!param.sort) param.sort = "ID";
                        if (!param.order) param.order = "desc";
                    },
                    onLoadSuccess: function (row) {
                        iframeHeight($(parent.document).find('#rightContent')[0], 460);
                        var rowData = row.rows;
                        $.each(rowData, function (index, val) {//遍历JSON
                            $("#setUser2GroupAlreadyListDataTbl").datagrid("selectRow", index);//默认选中当前所有数据
                        });
                    }
                });
            };
            registerDatagridOfSetUser2GroupAlreadyList();

            $(function () {
                //搜索
                $('#searchList').bind('click', function () {
                    registerDatagridOfSetUser2GroupAlreadyList();
                });
            });
        }

        //5.3设置用户--可选择用户列表表格控件绑定
        if (isCurrentPage("/Sys_Role/AddOtherUserPermission")) {
            window.registerDatagridOfAddOtherUserPermission = function () {
                registerDatGrid('addOtherUserPermissionListDataTbl', {
                    url: "/Sys_Role/GetHasNoUserListOfGroupByPage",
                    queryParams: {
                        key: $.trim($('#KeyWord').val()),//关键字
                        groupId: $("#groupId").val()//角色id
                    },
                    onBeforeLoad: function (param) {
                        //默认排序
                        if (!param.sort) param.sort = "ID";
                        if (!param.order) param.order = "desc";
                    },
                    onLoadSuccess: function () {
                        iframeHeight($(parent.document).find('#rightContent')[0], 460);
                    },
                    checkOnSelect: true,//选择checkbox则选择行
                    selectOnCheck: true//选择行则选择checkbox
                });
            };
            registerDatagridOfAddOtherUserPermission();

            $(function () {
                //搜索
                $('#searchList').bind('click', function () {
                    registerDatagridOfAddOtherUserPermission();
                });
            });
        }

    })();

    //3--数据组
    (function () {
        //5.1数据组列表表格控件绑定
        if (isCurrentPage("/Sys_Group/GroupList")) {
            window.registerDatagridOfDataGroupList = function () {
                registerDatGrid('groupListDataTbl', {
                    url: "/Sys_Group/GetGroupListByPage",
                    queryParams: {
                    },
                    onBeforeLoad: function (param) {
                        //默认排序
                        if (!param.sort) param.sort = "GroupType";
                        if (!param.order) param.order = "asc";
                    },
                    onLoadSuccess: function () {
                        iframeHeight($(parent.document).find('#rightContent')[0], 460);
                    }
                });
            };
            registerDatagridOfDataGroupList();
        }

        //5.2设置用户--已有用户列表表格控件绑定
        if (isCurrentPage("/Sys_Group/SetUser2GroupAlreadyList")) {
            window.registerDatagridOfSetUser2GroupAlreadyList = function () {
                registerDatGrid('setUser2GroupAlreadyListDataTbl', {
                    url: "/Sys_Group/GetUserListOfGroupByPage",
                    queryParams: {
                        key: $.trim($('#KeyWord').val()),//关键字
                        groupId: $.trim($("#groupId").val())//角色id
                    },
                    onBeforeLoad: function (param) {
                        //默认排序
                        if (!param.sort) param.sort = "ID";
                        if (!param.order) param.order = "desc";
                    },
                    onLoadSuccess: function (row) {
                        iframeHeight($(parent.document).find('#rightContent')[0], 460);
                        var rowData = row.rows;
                        $.each(rowData, function (index, val) {//遍历JSON
                            $("#setUser2GroupAlreadyListDataTbl").datagrid("selectRow", index);//默认选中当前所有数据
                        });
                    },
                    checkOnSelect: true,//选择checkbox则选择行
                    selectOnCheck: true//选择行则选择checkbox
                });
            };
            registerDatagridOfSetUser2GroupAlreadyList();

            $(function () {
                //搜索
                $('#searchList').bind('click', function () {
                    registerDatagridOfSetUser2GroupAlreadyList();
                });
            });
        }

        //5.3设置用户--添加用户列表表格控件绑定
        if (isCurrentPage("/Sys_Group/AddOtherUserPermission")) {
            window.registerDatagridOfAddOtherUserPermission = function () {
                registerDatGrid('addOtherUserPermissionListDataTbl', {
                    url: "/Sys_Group/GetHasNoUserListOfGroupByPage",
                    queryParams: {
                        key: $.trim($('#KeyWord').val()),//关键字
                        groupId: $.trim($("#groupId").val())//角色id
                    },
                    onBeforeLoad: function (param) {
                        //默认排序
                        if (!param.sort) param.sort = "ID";
                        if (!param.order) param.order = "desc";
                    },
                    onLoadSuccess: function () {
                        iframeHeight($(parent.document).find('#rightContent')[0], 460);
                    },
                    checkOnSelect: true,//选择checkbox则选择行
                    selectOnCheck: true//选择行则选择checkbox
                });
            };
            registerDatagridOfAddOtherUserPermission();

            $(function () {
                //搜索
                $('#searchList').bind('click', function () {
                    registerDatagridOfAddOtherUserPermission();
                });
            });
        }

        //5.4设置酒店--已有酒店列表表格控件绑定
        if (isCurrentPage("/Sys_Group/SetHotel2GroupAlreadyList")) {
            window.registerDatagridOfSetHotel2GroupAlreadyList = function () {
                registerDatGrid('setHotel2GroupAlreadyListDataTbl', {
                    url: "/Sys_Group/GetGroupHotelPermissionListByGroupId",
                    queryParams: {
                        key: $.trim($('#KeyWord').val()),//关键字
                        groupId: $("#groupId").val()//角色id
                    },
                    onBeforeLoad: function (param) {
                        //默认排序
                        if (!param.sort) param.sort = "DevelopNo";
                        if (!param.order) param.order = "desc";
                    },
                    onLoadSuccess: function (row) {
                        iframeHeight($(parent.document).find('#rightContent')[0], 460);
                        var rowData = row.rows;
                        $.each(rowData, function (index, val) {//遍历JSON
                            $("#setHotel2GroupAlreadyListDataTbl").datagrid("selectRow", index);//默认选中当前所有数据
                        });
                    },
                    checkOnSelect: true,//选择checkbox则选择行
                    selectOnCheck: true//选择行则选择checkbox
                });
            };
            registerDatagridOfSetHotel2GroupAlreadyList();

            $(function () {
                //搜索
                $('#searchList').bind('click', function () {
                    registerDatagridOfSetHotel2GroupAlreadyList();
                });
            });
        }

        //5.5设置酒店--添加酒店列表表格控件绑定
        if (isCurrentPage("/Sys_Group/AddOtherHotelPermission")) {
            window.registerDatagridOfAddOtherHotelPermission = function () {
                registerDatGrid('addOtherHotelPermissionDataTbl', {
                    url: "/Sys_Group/GetHasNoHotelPerByPageAndGroupId",
                    queryParams: {
                        key: $.trim($('#KeyWord').val()),//关键字
                        groupId: $.trim($("#groupId").val())//角色id
                    },
                    onBeforeLoad: function (param) {
                        //默认排序
                        if (!param.sort) param.sort = "DevelopNo";
                        if (!param.order) param.order = "desc";
                    },
                    onLoadSuccess: function () {
                        iframeHeight($(parent.document).find('#rightContent')[0], 460);
                    },
                    checkOnSelect: true,//选择checkbox则选择行
                    selectOnCheck: true//选择行则选择checkbox
                });
            };
            registerDatagridOfAddOtherHotelPermission();

            $(function () {
                //搜索
                $('#searchList').bind('click', function () {
                    registerDatagridOfAddOtherHotelPermission();
                });
            });
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
    if (isCurrentPage("/Sys_Menu/FirstLevelList")) {
        //操作按钮
        window.OperateFormatter = function (value, row, index) {
            return '<input type="submit" class="SysM_detailButton button"  value="删除" onclick="DeleteMenu(' + row["ID"] + ')" />&nbsp&nbsp&nbsp<input data-wrapper-id="bgWrapperOne" data-detail-link="/Sys_Menu/ChildMenuList/' + row["ID"] + '" type="submit" class="SysM_detailButton button" value="二级菜单">';
        };
        //菜单名称[添加链接到详情编辑]
        window.NameFormatter = function (value, row, index) {
            if (value) {
                return '<a href="#" data-wrapper-id="bgWrapperTwo" data-detail-link="/Sys_Menu/EditMenu/' + row["ID"] + '" class="normal">' + value + '</a>';
            }
            return "";
        };
    }

    //1.2二级菜单列表
    if (isCurrentPage("/Sys_Menu/ChildMenuList")) {
        //操作按钮
        window.OperateFormatter = function (value, row, index) {
            return '<input type="submit" class="SysM_detailButton button"  value="删除" onclick="DeleteMenu(' + row["ID"] + ')" />&nbsp&nbsp&nbsp<input data-wrapper-id="bgWrapperTwo" data-detail-link="/Sys_Menu/ThirdMenuList/' + row["ID"] + '" type="submit" class="SysM_detailButton button" value="三级菜单">';
        };
        //菜单名称[添加链接到详情编辑]
        window.NameFormatter = function (value, row, index) {
            if (value) {
                return '<a href="#" data-wrapper-id="bgWrapperTwo" data-detail-link="/Sys_Menu/EditMenu/' + row["ID"] + '" class="normal">' + value + '</a>';
            }
            return "";
        };
    }

    //1.3三级菜单列表
    if (isCurrentPage("/Sys_Menu/ThirdMenuList")) {
        //操作按钮
        window.OperateFormatter = function (value, row, index) {
            return '<input type="submit" class="SysM_detailButton button"  value="删除" onclick="DeleteMenu(' + row["ID"] + ')" />&nbsp&nbsp&nbsp<input data-wrapper-id="bgWrapperThree" data-detail-link="/Sys_Menu/FunctionList/' + row["ID"] + '" type="submit" class="SysM_detailButton button" value="功能列表">';
        };
        //菜单名称[添加链接到详情编辑]
        window.NameFormatter = function (value, row, index) {
            if (value) {
                return '<a href="#" data-wrapper-id="bgWrapperThree" data-detail-link="/Sys_Menu/EditThirdMenu/' + row["ID"] + '" class="normal">' + value + '</a>';
            }
            return "";
        };
    }

    //1.4功能列表,相当于4级
    if (isCurrentPage("/Sys_Menu/FunctionList")) {
        //操作按钮
        window.OperateFormatter = function (value, row, index) {
            return '<input type="submit" class="SysM_detailButton button"  value="删除" onclick="DeleteMenu(' + row["ID"] + ')" />';
        };
        //功能名称[添加链接到详情编辑]
        window.NameFormatter = function (value, row, index) {
            if (value) {
                return '<a href="#" data-wrapper-id="bgWrapperFour" data-detail-link="/Sys_Menu/EditFunction/' + row["ID"] + '" class="normal">' + value + '</a>';
            }
            return "";
        };
    }

    //2角色列表
    if (isCurrentPage("/Sys_Role/GroupList")) {
        //操作按钮
        window.OperateFormatter = function (value, row, index) {
            return '<input type="submit" class="SysM_detailButton button"  value="删除" onclick="DeleteMenuGroup(' + row["ID"] + ')" />&nbsp&nbsp&nbsp<input data-wrapper-id="bgWrapperOne" data-detail-link="/Sys_Role/SetMenu2Group?id=' + row["ID"] + '" type="submit" class="SysM_detailButton button" value="设置菜单">&nbsp&nbsp&nbsp<input data-wrapper-id="bgWrapperOne" data-detail-link="/Sys_Role/SetUser2GroupAlreadyList?id=' + row["ID"] + '" type="submit" class="SysM_detailButton button" value="设置用户">';
        };
        //菜单名称[添加链接到详情编辑]
        window.NameFormatter = function (value, row, index) {
            if (value) {
                return '<a href="#" data-wrapper-id="bgWrapperOne" data-detail-link="/Sys_Role/EditGroup/' + row["ID"] + '" class="normal">' + value + '</a>';
            }
            return "";
        };
    }

    //3数据组列表
    if (isCurrentPage("/Sys_Group/GroupList")) {
        //操作按钮
        window.OperateFormatter = function (value, row, index) {
            var url = '<input type="button" class="SysM_detailButton button"  value="删除" onclick="DeleteDataGroup(' + row["ID"] + ')" />&nbsp&nbsp&nbsp<input data-wrapper-id="bgWrapperOne" data-detail-link="/Sys_Group/SetUser2GroupAlreadyList?id=' + row["ID"] + '" type="button" class="SysM_detailButton button" value="设置用户">';
            if (row["GroupType"] == 3) {
                url = url + '&nbsp&nbsp&nbsp<input data-wrapper-id="bgWrapperOne" data-detail-link="/Sys_Group/SetHotel2GroupAlreadyList?id=' + row["ID"] + '" type="button" class="SysM_detailButton button" value="设置酒店">';
            }
            return url;
        };
        //菜单名称[添加链接到详情编辑]
        window.NameFormatter = function (value, row, index) {
            if (value) {
                return '<a href="#" data-wrapper-id="bgWrapperOne" data-detail-link="/Sys_Group/EditGroup/' + row["ID"] + '" class="normal">' + value + '</a>';
            }
            return "";
        };
        //数据组类型初始化
        window.GroupTypeFormatter = function (value, row, index) {
            switch (value) {
                case 0:
                    return '品牌';
                case 1:
                    return '城区';
                case 2:
                    return '区域';
                case 3:
                    return '酒店组';
                case 4:
                    return '全量酒店';
                default: return "";
            }
        };
    }

})();


/*
Third系统管理数据提交
*/
(function () {

    window.AutoSubmitData = function () {
        //1添加菜单提交
        if (isCurrentPage("/Sys_Menu/AddMenu") || isCurrentPage("/Sys_Menu/AddFunction")) {
            if (CommonValideFormCheck() == true) {
                AddMenuFormSub();
            }
        }
        //编辑
        if (isCurrentPage("/Sys_Menu/EditMenu") || isCurrentPage("/Sys_Menu/EditFunction")) {
            if (CommonValideFormCheck() == true) {
                EditMenuFormSub();
            }
        }

        //1.2三级菜单提交
        if (isCurrentPage("/Sys_Menu/AddThirdMenu")) {
            if (CommonValideFormCheck() == true) {
                AddThirdMenuFormSub();
            }
        }
        //三级菜单编辑
        if (isCurrentPage("/Sys_Menu/EditThirdMenu")) {
            if (CommonValideFormCheck() == true) {
                EditThirdMenuFormSub();
            }
        }

        //2添加角色(菜单组)提交
        if (isCurrentPage("/Sys_Role/AddGroup")) {
            if (CommonValideFormCheck() == true) {
                AddMenuGroupFormSub();
            }
        }
        //编辑
        if (isCurrentPage("/Sys_Role/EditGroup")) {
            if (CommonValideFormCheck() == true) {
                EditMenuGroupFormSub();
            }
        }

        //3设置菜单
        if (isCurrentPage("/Sys_Role/SetMenu2Group")) {
            if (SetMenu2GroupFormCheck() == true) {
                SetMenu2GroupFormSub();
            }
        }

        //4设置用户--角色
        if (isCurrentPage("/Sys_Role/SetUser2GroupAlreadyList")) {
            if (SetUser2GroupAlreadyListFormCheck() == true) {
                SetUser2GroupAlreadyListFormSub("menuGroup");
            }
        }


        //5添加用户--角色
        if (isCurrentPage("/Sys_Role/AddOtherUserPermission")) {
            if (AddOtherUserPermissionFormCheck() == true) {
                AddOtherUserPermissionFormSub("menuGroup");
            }
        }

        //6添加数据组提交
        if (isCurrentPage("/Sys_Group/AddGroup")) {
            if (CommonValideFormCheck() == true) {
                AddDataGroupFormSub();
            }
        }
        //编辑
        if (isCurrentPage("/Sys_Group/EditGroup")) {
            if (CommonValideFormCheck() == true) {
                EditDataGroupFormSub();
            }
        }

        //7.1设置用户--数据组
        if (isCurrentPage("/Sys_Group/SetUser2GroupAlreadyList")) {
            if (SetUser2GroupAlreadyListFormCheck() == true) {
                SetUser2GroupAlreadyListFormSub("dataGroup");
            }
        }

        //7.2添加用户--数据组
        if (isCurrentPage("/Sys_Group/AddOtherUserPermission")) {
            if (AddOtherUserPermissionFormCheck() == true) {
                AddOtherUserPermissionFormSub("dataGroup");
            }
        }

        //8.1设置酒店--数据组
        if (isCurrentPage("/Sys_Group/SetHotel2GroupAlreadyList")) {
            if (SetHotel2GroupAlreadyListFormCheck() == true) {
                SetHotel2GroupAlreadyListFormSub();
            }
        }

        //8.2添加酒店--数据组
        if (isCurrentPage("/Sys_Group/AddOtherHotelPermission")) {
            if (AddOtherHotelPermissionFormCheck() == true) {
                AddOtherHotelPermissionFormSub();
            }
        }

        //9设置公共菜单菜单
        if (isCurrentPage("/Sys_Menu/SetCommonMenuForAll")) {
            if (SetCommonMenuForAllFormCheck() == true) {
                SetCommonMenuForAllFormSub();
            }
        }
    }


    //8添加酒店--数据组
    window.AddOtherHotelPermissionFormCheck = function () {
        var checkedItems = $('#addOtherHotelPermissionDataTbl').datagrid('getChecked');
        if (checkedItems.length <= 0) {
            Dialog.alert("请至少选择一个酒店");
            return false;
        }
        return true;
    };
    //提交
    window.AddOtherHotelPermissionFormSub = function (type) {
        var checkedItems = $('#addOtherHotelPermissionDataTbl').datagrid('getChecked');
        var s = '';
        $.each(checkedItems, function (index, item) {
            if (s != '') s += ',';
            s += item.HotelUnifyNo;
        });
        $.post("/Sys_Group/AddOtherHotelPermission", { groupId: $("#groupId").val(), hotelUnifyNos: s }, function (data) {
            SuccessMsg(data);
        });

    };

    //9设置酒店--数据组
    window.SetHotel2GroupAlreadyListFormCheck = function () {
        return true;
    };
    //提交
    window.SetHotel2GroupAlreadyListFormSub = function (type) {
        var checkedItems = $('#setHotel2GroupAlreadyListDataTbl').datagrid('getChecked');
        var s = '';
        $.each(checkedItems, function (index, item) {
            if (s != '') s += ',';
            s += item.HotelUnifyNo;
        });
        $.post("/Sys_Group/SetHotel2GroupAlreadyList", { groupId: $("#groupId").val(), hotelUnifyNos: s }, function (data) {
            SuccessMsg(data);
        });
    };

    //5添加用户--酒店组,角色公用
    window.AddOtherUserPermissionFormCheck = function () {
        var checkedItems = $('#addOtherUserPermissionListDataTbl').datagrid('getChecked');
        if (checkedItems.length <= 0) {
            Dialog.alert("请至少选择一个用户");
            return false;
        }
        return true;
    };
    //提交
    window.AddOtherUserPermissionFormSub = function (type) {
        var checkedItems = $('#addOtherUserPermissionListDataTbl').datagrid('getChecked');
        var s = '';
        var url = '';
        $.each(checkedItems, function (index, item) {
            if (s != '') s += ',';
            s += item.ADAccount;
        });
        if (type == "menuGroup") {
            url = "/Sys_Role/AddOtherUserPermission";
        }
        if (type == "dataGroup") {
            url = "/Sys_Group/AddOtherUserPermission";
        }
        if (url != '') {
            $.post(url, { groupId: $("#groupId").val(), adaccounts: s }, function (data) {
                SuccessMsg(data);
            });
        }
    };

    //4设置用户--酒店组,角色公用
    window.SetUser2GroupAlreadyListFormCheck = function () {
        return true;
    };
    //提交
    window.SetUser2GroupAlreadyListFormSub = function (type) {
        var checkedItems = $('#setUser2GroupAlreadyListDataTbl').datagrid('getChecked');
        var allItems = $('#setUser2GroupAlreadyListDataTbl').datagrid('getRows');
        var s = '';
        var all = '';
        var url = '';
        $.each(checkedItems, function (index, item) {
            if (s != '') s += ',';
            s += item.ADAccount;
        });
        $.each(allItems, function (index, item) {
            if (all != '') all += ',';
            all += item.ADAccount;
        });
        if (type == "menuGroup") {
            url = "/Sys_Role/SetUser2GroupAlreadyList";
        }
        if (type == "dataGroup") {
            url = "/Sys_Group/SetUser2GroupAlreadyList";
        }
        if (url != '') {
            $.post(url, { groupId: $("#groupId").val(), adaccounts: s, curentPageAdaccounts: all }, function (data) {
                SuccessMsg(data);
            });
        }
    };

    //3设置菜单
    window.SetMenu2GroupFormCheck = function () {
        return true;
    };
    //提交
    window.SetMenu2GroupFormSub = function () {
        var nodes = $('#menuTree').tree('getChecked');
        var s = '';
        for (var i = 0; i < nodes.length; i++) {
            if (s != '') s += ',';
            s += nodes[i].id;
        }
        $.post("/Sys_Role/SetMenu2Group", { groupId: $("#groupId").val(), menus: s }, function (data) {
            SuccessMsg(data);
        });
    };

    //9设置通用菜单
    window.SetCommonMenuForAllFormCheck = function () {
        //去除个数限制,2015-07-31
        //var nodes = $('#menuTree').tree('getChecked');
        //if (nodes.length <= 0) {
        //    Dialog.alert("请至少选择一个菜单");
        //    return false;
        //}
        return true;
    };
    //提交
    window.SetCommonMenuForAllFormSub = function () {
        var nodes = $('#menuTree').tree('getChecked');
        var s = '';
        for (var i = 0; i < nodes.length; i++) {
            if (s != '') s += ',';
            s += nodes[i].id;
        }
        $.post("/Sys_Menu/SetCommonMenuForAll", { groupId: $("#groupId").val(), menus: s }, function (data) {
            SuccessMsg(data);
        });
    };

    //删除菜单
    window.DeleteMenu = function (value) {
        Dialog.confirm("确认删除吗?", {
            okCallback: function () {
                $.get("/Sys_Menu/DeleteMenu/" + value, {}, function (data) {
                    //SuccessMsg(data);
                    Dialog.alert(data.IsSuccess);
                });
            }
        });
    }

    //删除角色(菜单组)
    window.DeleteMenuGroup = function (value) {
        Dialog.confirm("确认删除吗?", {
            okCallback: function () {
                $.get("/Sys_Role/DeleteGroup/" + value, {}, function (data) {
                    SuccessMsg(data);
                });
            }
        });
    }

    //删除数据组
    window.DeleteDataGroup = function (value) {
        Dialog.confirm("确认删除吗?", {
            okCallback: function () {
                $.get("/Sys_Group/DeleteGroup/" + value, {}, function (data) {
                    SuccessMsg(data);
                });
            }
        });
    }

})();


/*
Forth表单校验
*/
(function () {
    //1添加菜单,添加功能表单校验
    $(function () {
        if (isCurrentPage("/Sys_Menu/AddMenu") || isCurrentPage("/Sys_Menu/AddFunction") || isCurrentPage("/Sys_Menu/AddThirdMenu")) {
            $("#detailForm").validate({
                rules: {
                    Name: {
                        "required": true,
                        "maxlength": 50,
                        "remote":
                        {
                            url: "/Sys_Menu/CheckMenuNameUnique", //后台处理程序
                            type: "post", //数据发送方式
                            dataType: "json", //接受数据格式   
                            data: {
                                //要传递的数据
                                id: function () {
                                    return '';
                                },
                                name: function () {
                                    return $.trim($("#Name").val());
                                },
                                parentId: function () {
                                    return $.trim($("#ParentID").val());
                                }
                            }
                        }
                    },
                    Sort: { "required": true, "digits": true }
                },
                messages: {
                    Name: {
                        required: "<span style='color:red;'>*</span>必填",
                        maxlength: "<span style='color:red;'>*</span>最长50字符",
                        remote: "<span style='color:red;'>*</span>名称已存在"
                    },
                    Sort: {
                        required: "<span style='color:red;'>*</span>必填",
                        digits: "<span style='color:red;'>*</span>请输入整数"
                    }
                }
            });
        }
    });

    //2编辑菜单,编辑功能表单校验
    $(function () {
        if (isCurrentPage("/Sys_Menu/EditMenu") || isCurrentPage("/Sys_Menu/EditFunction") || isCurrentPage("/Sys_Menu/EditThirdMenu")) {
            $("#detailForm").validate({
                rules: {
                    Name: {
                        "required": true,
                        "maxlength": 50,
                        "remote":
                        {
                            url: "/Sys_Menu/CheckMenuNameUnique", //后台处理程序
                            type: "post", //数据发送方式
                            dataType: "json", //接受数据格式   
                            data: {
                                //要传递的数据
                                id: function () {
                                    return $.trim($("#ID").val());
                                },
                                name: function () {
                                    return $.trim($("#Name").val());
                                },
                                parentId: function () {
                                    return $.trim($("#ParentID").val());
                                }
                            }
                        }
                    },
                    Sort: { "required": true, "digits": true }
                },
                messages: {
                    Name: {
                        required: "<span style='color:red;'>*</span>必填",
                        maxlength: "<span style='color:red;'>*</span>最长50字符",
                        remote: "<span style='color:red;'>*</span>名称已存在"
                    },
                    Sort: {
                        required: "<span style='color:red;'>*</span>必填",
                        digits: "<span style='color:red;'>*</span>请输入整数"
                    }
                }

            });
        }
    });

    //3添加角色表单校验
    $(function () {
        if (isCurrentPage("/Sys_Role/AddGroup")) {
            $("#detailForm").validate({
                rules: {
                    Name: {
                        "required": true,
                        "maxlength": 50,
                        "remote":
                        {
                            url: "/Sys_Role/CheckRoleNameUnique", //后台处理程序
                            type: "post", //数据发送方式
                            dataType: "json", //接受数据格式   
                            data: {
                                //要传递的数据
                                id: function () {
                                    return '';
                                },
                                name: function () {
                                    return $.trim($("#Name").val());
                                }
                            }
                        }
                    }
                },
                messages: {
                    Name: {
                        required: "<span style='color:red;'>*</span>必填",
                        maxlength: "<span style='color:red;'>*</span>最长50字符",
                        remote: "<span style='color:red;'>*</span>名称已存在"
                    }
                }
            });
        }
    });


    //4编辑角色表单校验
    $(function () {
        if (isCurrentPage("/Sys_Role/EditGroup")) {
            $("#detailForm").validate({
                rules: {
                    Name: {
                        "required": true,
                        "maxlength": 50,
                        "remote":
                        {
                            url: "/Sys_Role/CheckRoleNameUnique", //后台处理程序
                            type: "post", //数据发送方式
                            dataType: "json", //接受数据格式   
                            data: {
                                //要传递的数据
                                id: function () {
                                    return $.trim($("#ID").val());
                                },
                                name: function () {
                                    return $.trim($("#Name").val());
                                }
                            }
                        }
                    }
                },
                messages: {
                    Name: {
                        required: "<span style='color:red;'>*</span>必填",
                        maxlength: "<span style='color:red;'>*</span>最长50字符",
                        remote: "<span style='color:red;'>*</span>名称已存在"
                    }
                }
            });
        }
    });

    //5添加数据组表单校验
    $(function () {
        if (isCurrentPage("/Sys_Group/AddGroup")) {
            $("#detailForm").validate({
                rules: {
                    Name: {
                        "required": true,
                        "maxlength": 50,
                        "remote":
                        {
                            url: "/Sys_Group/CheckGroupNameUnique", //后台处理程序
                            type: "post", //数据发送方式
                            dataType: "json", //接受数据格式   
                            data: {
                                //要传递的数据
                                id: function () {
                                    return '';
                                },
                                name: function () {
                                    return $.trim($("#Name").val());
                                }
                            }
                        }
                    },
                    GroupType: {
                        "required": true,
                        "digits": true
                    }
                },
                messages: {
                    Name: {
                        required: "<span style='color:red;'>*</span>必填",
                        maxlength: "<span style='color:red;'>*</span>最长50字符",
                        remote: "<span style='color:red;'>*</span>名称已存在"
                    },
                    GroupType: {
                        required: "<span style='color:red;'>*</span>必填",
                        digits: "<span style='color:red;'>*</span>组类型为数字"
                    }
                }
            });
        }
    });


    //6编辑数据组表单校验
    $(function () {
        if (isCurrentPage("/Sys_Group/EditGroup")) {
            $("#detailForm").validate({
                rules: {
                    Name: {
                        "required": true,
                        "maxlength": 50,
                        "remote":
                        {
                            url: "/Sys_Group/CheckGroupNameUnique", //后台处理程序
                            type: "post", //数据发送方式
                            dataType: "json", //接受数据格式   
                            data: {
                                //要传递的数据
                                id: function () {
                                    return $.trim($("#ID").val());
                                },
                                name: function () {
                                    return $.trim($("#Name").val());
                                }
                            }
                        }
                    },
                    GroupType: {
                        "required": true,
                        "digits": true
                    }
                },
                messages: {
                    Name: {
                        required: "<span style='color:red;'>*</span>必填",
                        maxlength: "<span style='color:red;'>*</span>最长50字符",
                        remote: "<span style='color:red;'>*</span>名称已存在"
                    },
                    GroupType: {
                        required: "<span style='color:red;'>*</span>必填",
                        digits: "<span style='color:red;'>*</span>组类型为数字"
                    }
                }
            });
        }
    });

    //0通用表单校验
    window.CommonValideFormCheck = function () {
        $("#detailForm").valid();
        if ($("#detailForm").valid()) {
            return true;
        }
        return false;
    };

    //1添加菜单,添加功能--提交
    window.AddMenuFormSub = function () {
        var parentId = $.trim($("#ParentID").val());
        var name = $.trim($("#Name").val());
        var controllerName = $.trim($("#ControllerName").val());
        var actionName = $.trim($("#ActionName").val());
        var sort = $.trim($("#Sort").val());
        var isFirstSecondMenu = $("#IsFirstSecondMenu:checked").length > 0;
        var ico = $.trim($("#ICO").val());
        var iframeId = $(".header>.icon-remove").attr('data-close');
        $.post("/Sys_Menu/AddMenu", { name: name, controllerName: controllerName, actionName: actionName, sort: sort, isFirstSecondMenu: isFirstSecondMenu, parentId: parentId, ico: ico }, function (data) {
            //SuccessMsg(data, iframeId);
        });
    };

    //2编辑菜单,编辑功能--提交
    window.EditMenuFormSub = function () {
        var id = $.trim($("#ID").val());
        var parentId = $.trim($("#ParentID").val());
        var name = $.trim($("#Name").val());
        var controllerName = $.trim($("#ControllerName").val());
        var actionName = $.trim($("#ActionName").val());
        var sort = $.trim($("#Sort").val());
        var isFirstSecondMenu = $("#IsFirstSecondMenu:checked").length > 0;
        var ico = $.trim($("#ICO").val());
        var iframeId = $(".header>.icon-remove").attr('data-close');
        $.post("/Sys_Menu/EditMenu", { id: id, name: name, controllerName: controllerName, actionName: actionName, sort: sort, isFirstSecondMenu: isFirstSecondMenu, parentId: parentId, ico: ico }, function (data) {
            SuccessMsg(data, iframeId);
        });
    };

    //3添加角色(菜单组)--提交
    window.AddMenuGroupFormSub = function () {
        var name = $.trim($("#Name").val());
        var description = $.trim($("#Description").val());
        var iframeId = $(".header>.icon-remove").attr('data-close');
        $.post("/Sys_Role/AddGroup", { name: name, description: description }, function (data) {
            SuccessMsg(data, iframeId);
        });
    };
    //4编辑角色(菜单组)--提交
    window.EditMenuGroupFormSub = function () {
        var id = $.trim($("#ID").val());
        var name = $.trim($("#Name").val());
        var description = $.trim($("#Description").val());
        var iframeId = $(".header>.icon-remove").attr('data-close');
        $.post("/Sys_Role/EditGroup", { id: id, name: name, description: description }, function (data) {
            SuccessMsg(data, iframeId);
        });
    };

    //5添加数据组--提交
    window.AddDataGroupFormSub = function () {
        var name = $.trim($("#Name").val());
        var description = $.trim($("#Description").val());
        var groupType = $.trim($("#GroupType").val());
        var iframeId = $(".header>.icon-remove").attr('data-close');
        $.post("/Sys_Group/AddGroup", { name: name, description: description, groupType: groupType }, function (data) {
            SuccessMsg(data, iframeId);
        });
    };
    //6编辑数据组--提交
    window.EditDataGroupFormSub = function () {
        var id = $.trim($("#ID").val());
        var name = $.trim($("#Name").val());
        var description = $.trim($("#Description").val());
        var groupType = $.trim($("#GroupType").val());
        var iframeId = $(".header>.icon-remove").attr('data-close');
        $.post("/Sys_Group/EditGroup", { id: id, name: name, description: description, groupType: groupType }, function (data) {
            SuccessMsg(data, iframeId);
        });
    };

    //7添加三级菜单,添加功能--提交
    window.AddThirdMenuFormSub = function () {
        var parentId = $.trim($("#ParentID").val());
        var name = $.trim($("#Name").val());
        var controllerName = $.trim($("#ControllerName").val());
        var actionName = $.trim($("#ActionName").val());
        var sort = $.trim($("#Sort").val());
        var isFirstSecondMenu = $("#IsFirstSecondMenu:checked").length > 0;
        var ico = $.trim($("#ICO").val());
        var iframeId = $(".header>.icon-remove").attr('data-close');
        $.post("/Sys_Menu/AddThirdMenu", { name: name, controllerName: controllerName, actionName: actionName, sort: sort, isFirstSecondMenu: isFirstSecondMenu, parentId: parentId, ico: ico }, function (data) {
            SuccessMsg(data, iframeId);
        });
    };

    //8编辑三级菜单,编辑功能--提交
    window.EditThirdMenuFormSub = function () {
        var id = $.trim($("#ID").val());
        var parentId = $.trim($("#ParentID").val());
        var name = $.trim($("#Name").val());
        var controllerName = $.trim($("#ControllerName").val());
        var actionName = $.trim($("#ActionName").val());
        var sort = $.trim($("#Sort").val());
        var isFirstSecondMenu = $("#IsFirstSecondMenu:checked").length > 0;
        var ico = $.trim($("#ICO").val());
        var iframeId = $(".header>.icon-remove").attr('data-close');
        $.post("/Sys_Menu/EditThirdMenu", { id: id, name: name, controllerName: controllerName, actionName: actionName, sort: sort, isFirstSecondMenu: isFirstSecondMenu, parentId: parentId, ico: ico }, function (data) {
            SuccessMsg(data, iframeId);
        });
    };
})();