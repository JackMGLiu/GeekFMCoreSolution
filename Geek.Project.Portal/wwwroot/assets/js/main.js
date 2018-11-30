var notice;
layui.use(['layer', 'element', 'admin', 'index', 'notice'], function () {
    var layer = layui.layer;
    var admin = layui.admin;
    var index = layui.index;
    notice = layui.notice;

    index.loadSetting();  // 加载本地缓存的设置属性

    // 默认加载主页
    index.loadView({
        menuPath: '/Main/Console',
        menuName: '<i class="layui-icon layui-icon-home"></i>'
    });

    $('#btnLogout').click(function () {
        top.layer.confirm('确定要退出本系统吗？', function (index) {
            top.layer.close(index);
            layer.load(2);
            $.post('/Main/Logout', {}, function (data) {
            }, 'json');
            return false;
        });

    });

});