layui.use(['layer', 'element', 'admin', 'index'], function () {
    var layer = layui.layer;
    var admin = layui.admin;
    var index = layui.index;

    index.loadSetting();  // 加载本地缓存的设置属性

    // 默认加载主页
    index.loadView({
        menuPath: '/main/console',
        menuName: '<i class="layui-icon layui-icon-home"></i>'
    });

});