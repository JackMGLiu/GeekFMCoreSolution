layui.use(['layer', 'form', 'table', 'util', 'admin'], function () {
    var layer = layui.layer;
    var form = layui.form;
    var table = layui.table;
    var util = layui.util;
    var admin = layui.admin;

    // 渲染表格
    var ins1 = table.render({
        elem: '#userTable',
        url: '/json/user.json',
        page: true,
        cols: [[
            { type: 'checkbox' },
            { field: 'username', sort: true, title: '账号' },
            { field: 'nickName', sort: true, title: '用户名' },
            { field: 'phone', sort: true, title: '手机号' },
            { field: 'sex', sort: true, title: '性别' },
            {
                sort: true, templet: function (d) {
                    return util.toDateString(d.createTime);
                }, title: '创建时间'
            },
            { field: 'state', sort: true, templet: '#tbaleState', title: '状态' },
            { align: 'center', toolbar: '#tableBar', title: '操作' }
        ]]
    });
});