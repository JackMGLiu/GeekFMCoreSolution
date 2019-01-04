var notice;
layui.use(['layer', 'form', 'table', 'util', 'admin', 'laydate', 'notice'], function () {
    var layer = layui.layer;
    var form = layui.form;
    var table = layui.table;
    var util = layui.util;
    var admin = layui.admin;
    var laydate = layui.laydate;
    notice = layui.notice;

    // 渲染表格
    var ins1 = table.render({
        id: 'logTable',
        elem: '#logTable',
        url: '/System/SysLog/PageLogData',
        height: 'full-120',
        limit: 15,
        limits: [10, 15, 20, 30, 40, 50, 100, 200],
        page: { //支持传入 laypage 组件的所有参数（某些参数除外，如：jump/elem） - 详见文档
            layout: ['count', 'prev', 'page', 'next', 'limit', 'refresh', 'skip'], //自定义分页布局
            //,curr: 5 //设定初始在第 5 页
            groups: 6, //只显示 1 个连续页码
            first: false, //不显示首页
            last: false //不显示尾页
        },
        request: {
            pageName: 'PageIndex',
            limitName: 'PageSize'
        },
        cols: [[
            { type: 'checkbox' },
            { type: 'numbers', width: 80, title: '序号' },
            { field: 'Message', width: 180, title: '日志消息' },
            { field: 'Level', width: 100, title: '日志级别' },
            { field: 'Class', width: 160, title: '操作类' },
            { field: 'Url', width: 160, title: '操作地址' },
            { field: 'User', minWidth: 100, title: '操作人' },
            {
                sort: true, templet: function (d) {
                    return util.toDateString(d.TimeStamp);
                }, title: '记录时间'
            },
            { align: 'left', toolbar: '#tableBar', title: '操作', fixed: "right", minWidth: 125 }
        ]]
    });

    // 渲染laydate 日期范围
    laydate.render({
        elem: '#TimeStamp',
        range: '-'
    });

    //// 工具条点击事件
    //table.on('tool(logTable)', function (obj) {
    //    var data = obj.data;
    //    var layEvent = obj.event;

    //    if (layEvent === 'edit') { // 修改
    //        showEditModel(data);
    //    } else if (obj.event === 'del') { //删除
    //        delObj(obj);
    //    } else if (layEvent === 'reset') { // 重置密码
    //        //resetPsw(obj.data.userId);
    //    }
    //});

    // 搜索按钮点击事件
    $('#btnSearch').click(function () {
        var formData = $('#searchFrom').getFormData();
        console.log('formData', formData);
        table.reload('logTable', {
            page: {
                curr: 1
            },
            where: formData
        });
    });

    ////新增
    //$('#btn_add').click(function () {
    //    showEditModel();
    //});

    //$('#btn_delAll').click(function () {
    //    delObjs();
    //});

    //// 显示表单弹窗
    //function showEditModel(data) {
    //    admin.putTempData('t_user', data);
    //    admin.putTempData('formOk', false);

    //    var index = top.layui.admin.open({
    //        type: 2,
    //        title: data ? '编辑用户' : '新增用户',
    //        content: '/System/SysUser/Form',
    //        area: ['55%', '70%'],
    //        success: function (layero, index) {
    //            setTimeout(function () {
    //                top.layui.layer.tips('点击此处返回数据列表', '.layui-layer-setwin .layui-layer-close', {
    //                    tips: 3
    //                });
    //            }, 500);
    //        },
    //        end: function () {
    //            admin.getTempData('formOk') && table.reload('userTable');  // 成功刷新表格
    //        }
    //    });
    //}

    ////删除
    //function delObj(obj) {
    //    top.layer.confirm('确定要删除这条数据吗？', function (index) {
    //        top.layer.close(index);
    //        layer.load(2);
    //        $.post('/System/SysUser/DeleteModel', {
    //            userId: obj.data.Id
    //        }, function (data) {
    //            layer.closeAll('loading');
    //            if (data.status === '1') {
    //                top.notice.success({
    //                    title: '系统信息',
    //                    message: data.msg,
    //                    class: 'noticeTop'
    //                });
    //                table.reload('userTable');
    //            } else {
    //                top.notice.error({
    //                    title: '系统信息',
    //                    message: data.msg,
    //                    class: 'noticeTop'
    //                });
    //            }
    //        }, 'json');
    //    });
    //}

    ////删除
    //function delObjs() {
    //    var checkStatus = table.checkStatus('userTable');
    //    var itemCount = checkStatus.data.length;
    //    if (itemCount > 0) {
    //        var data = checkStatus.data;
    //        var ids = new Array();
    //        for (var i in data) {
    //            ids.push(data[i].Id);
    //        }
    //        var res = ids.join();
    //        top.layer.confirm('确定要删除这【' + itemCount + '】条数据吗？', function (index) {
    //            top.layer.close(index);
    //            layer.load(2);
    //            $.post('/System/SysUser/DeleteModels', {
    //                userIds: res
    //            }, function (data) {
    //                layer.closeAll('loading');
    //                if (data.status === '1') {
    //                    top.notice.success({
    //                        title: '系统信息',
    //                        message: data.msg,
    //                        class: 'noticeTop'
    //                    });
    //                    table.reload('userTable');
    //                } else {
    //                    top.notice.error({
    //                        title: '系统信息',
    //                        message: data.msg,
    //                        class: 'noticeTop'
    //                    });
    //                }
    //            }, 'json');
    //        });
    //    }
    //    else {
    //        top.notice.warning({
    //            title: '系统信息',
    //            message: '请选择要删除的项',
    //            class: 'noticeTop'
    //        });
    //        return;
    //    }
    //    //console.log(checkStatus.data); //获取选中行的数据
    //    //console.log(checkStatus.data.length); //获取选中行数量，可作为是否有选中行的条件
    //}
});
