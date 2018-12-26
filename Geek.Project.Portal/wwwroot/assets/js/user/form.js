var form;
layui.use(['layer', 'form', 'formSelects', 'admin'], function () {
    var layer = layui.layer;
    form = layui.form;
    var formSelects = layui.formSelects;
    var admin = layui.admin;

    // 回显user数据
    var user = admin.getTempData('t_user');

    var url = geek.isNullOrEmpty(user) ? '/System/SysUser/Form' : '/System/SysUser/Form?key=' + user.Id;
    console.log(url);

    if (user) {
        $('input[name="UserName"]').attr('readonly', 'readonly').removeAttr('onblur');
        form.val('userForm', user);
        //var rds = new Array();
        //for (var i = 0; i < user.roles.length; i++) {
        //    rds.push(user.roles[i].roleId);
        //}
        //formSelects.value('roleId', rds);
    }
    admin.iframeAuto();  // 让当前iframe弹层高度适应

    //验证
    form.verify({
        UserName: function (value) {
            $.ajax({
                type: 'get',
                async: false, // 使用同步的方法
                url: '/System/SysUser/CheckName',
                data: { //要提交到服务端验证的用户名
                    userName: value
                },
                dataType: 'json',
                success: function (res) {
                    if (res) {
                        return '该用户名已经存在';
                    }
                }
            });
        }
    });

    // 表单提交事件
    form.on('submit(btnSubmit)', function (data) {
        layer.load(2);
        $.post(url, data.field, function (data) {
            layer.closeAll('loading');
            if (data.status === '1') {
                top.notice.success({
                    title: '系统信息',
                    //timeout: 2500,
                    //position: 'topRight',
                    //transitionIn: 'bounceInLeft',
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

function unameIsExist(obj) {
    $.ajax({
        type: 'get',
        async: false, // 使用同步的方法
        url: '/System/SysUser/CheckName',
        data: { //要提交到服务端验证的用户名
            userName: obj.value
        },
        dataType: 'json',
        success: function (res) {
            if (res) {
                layer.msg('该用户名已经存在', {
                    icon: 5,
                    time: 1500,
                    shift: 6
                }, function () {
                    $(obj).focus().select();
                });
                return;
            }
        }
    });
}


function blurV() {
    var DANGER = 'layui-form-danger',
        stop = null,
        verify = form.verify;
    //device = layui.device()
    var othis = $(this), ver = othis.attr('lay-verify'), tips = '';
    var value = othis.val(), isFn = typeof verify[ver] === 'function';
    othis.removeClass(DANGER);
    //console.log(ver);
    //console.log(value);
    $.ajax({
        type: 'get',
        async: false, // 使用同步的方法
        url: '/sysuser/checkval',
        data: { //要提交到服务端验证的用户名
            userName: value
        },
        dataType: 'json',
        success: function (res) {
            if (res) {
                //return '该用户名已经存在！';
                othis.addClass(DANGER);
                layer.msg('用户名已经存在' || verify[ver][1], {
                    icon: 5
                    , shift: 6
                });
                return stop = true;
            }
        }
    });
    //if (verify[ver] && (isFn ? tips = verify[ver](value, this) : !verify[ver][0].test(value))) {
    //    layer.msg(tips || verify[ver][1], {
    //        icon: 5
    //        , shift: 6
    //    });
    //    ////非移动设备自动定位焦点
    //    //if (!device.android && !device.ios) {
    //    //    this.focus();
    //    //}
    //    othis.addClass(DANGER);
    //    return stop = true;
    //}
}