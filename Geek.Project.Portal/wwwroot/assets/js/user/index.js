layui.use(['layer', 'form', 'table', 'util', 'admin'], function () {
    var layer = layui.layer;
    var form = layui.form;
    var table = layui.table;
    var util = layui.util;
    var admin = layui.admin;

    // 渲染表格
    var ins1 = table.render({
        elem: '#userTable',
        url: '/sys/users',
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
            { field: 'UserName', width: 120, title: '账号' },
            { field: 'RealName', width: 120, title: '姓名' },
            { field: 'Age', sort: true, width: 70, title: '年龄' },
            { field: 'Email', minWidth: 120, title: '电子邮箱' },
            {
                field: 'RoleName', width: 100, title: '角色', templet: function (d) {
                    return d.Role.roleName;
                }
            },
            {
                sort: true, templet: function (d) {
                    return util.toDateString(d.CreateTime);
                }, title: '创建时间'
            },
            { field: 'state', sort: true, templet: '#tbaleState', title: '状态' },
            { align: 'left', toolbar: '#tableBar', title: '操作', fixed: "right", minWidth: 120 }
        ]]
    });
});