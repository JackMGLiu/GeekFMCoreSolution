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

});