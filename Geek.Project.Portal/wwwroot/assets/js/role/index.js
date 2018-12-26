layui.use(['layer', 'form', 'formSelects', 'admin'], function () {
    var layer = layui.layer;
    var form = layui.form;
    var formSelects = layui.formSelects;
    var admin = layui.admin;

    formSelects.data('roleId', 'server', {
        url: '/system/sysrole/getroles',
        keyword: '',
        success: function (id, url, searchVal, result) {
            console.log(id);
            console.log(result);
            formSelects.value('roleId', [2, 4]);

        }
    });

    $('#btn1').click(function () {
        var h = admin.getPageHeight();
        alert(h);
        //admin.showLoading('#demo1');
    });
});

$(function () {

});
