layui.use(['layer', 'form', 'formSelects', 'admin'], function () {
    var layer = layui.layer;
    var form = layui.form;
    var formSelects = layui.formSelects;
    var admin = layui.admin;

    // 回显user数据
    var user = admin.getTempData('t_user');

    admin.iframeAuto();  // 让当前iframe弹层高度适应

    // 表单提交事件
    form.on('submit(btnSubmit)', function (data) {
        layer.load(2);
        $.post('/sys/userform', data.field, function (data) {
            layer.closeAll('loading');
            if (data.status === '1') {
                top.notice.success({
                    title: '系统信息',
                    timeout: 2500,
                    position: 'topRight',
                    transitionIn: 'bounceInLeft',
                    message: data.msg,
                    class: 'noticeTop'
                });
                admin.putTempData('formOk', true);  // 操作成功刷新表格
                // 关闭当前iframe弹出层
                admin.closeThisDialog();
            } else {
                top.notice.error({
                    title: '系统信息',
                    timeout: 2500,
                    position: 'topRight',
                    transitionIn: 'bounceInLeft',
                    message: data.msg,
                    class: 'noticeTop'
                });
            }
        }, 'json');
        return false;
    });
});