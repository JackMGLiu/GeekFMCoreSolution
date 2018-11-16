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
        elem: '#userTable',
        url: '/sys/users',
        height: 'full-160',
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
            { field: 'UserName', width: 120, title: '账号' },
            { field: 'RealName', width: 120, title: '姓名' },
            { field: 'Age', sort: true, width: 70, title: '年龄' },
            { field: 'Email', minWidth: 125, title: '电子邮箱' },
            {
                field: 'RoleName', width: 100, title: '角色', templet: function (d) {
                    if (geek.isNullOrEmpty(d.Role)) {
                        return '<span style="color:red; font-weight:bolder;">暂无</span>';
                    } else {
                        return d.Role.roleName;
                    }

                }
            },
            {
                sort: true, templet: function (d) {
                    return util.toDateString(d.CreateTime);
                }, title: '创建时间'
            },
            { field: 'state', sort: true, width: 95, templet: '#tbaleState', title: '状态' },
            { align: 'left', toolbar: '#tableBar', title: '操作', fixed: "right", minWidth: 135 }
        ]]
    });

    // 渲染laydate
    laydate.render({
        elem: '#CreateTime'
    });


    // 搜索按钮点击事件
    $('#btnSearch').click(function () {
        var formData = $('#searchFrom').getFormData();
        table.reload('userTable', {
            page: {
                curr: 1
            },
            where: formData
        });
    });

    //新增
    $('#btn_add').click(function () {
        showEditModel();
    });

    // 显示表单弹窗
    function showEditModel(data) {
        admin.putTempData('t_user', data);
        admin.putTempData('formOk', false);

        var index = top.layui.admin.open({
            type: 2,
            title: data ? '编辑用户' : '新增用户',
            content: '/sys/userform',
            area: ['55%', '70%'],
            success: function (layero, index) {
                setTimeout(function () {
                    top.layui.layer.tips('点击此处返回数据列表', '.layui-layer-setwin .layui-layer-close', {
                        tips: 3
                    });
                }, 500);
            },
            end: function () {
                admin.getTempData('formOk') && table.reload('userTable');  // 成功刷新表格
            }
        });
    }
});