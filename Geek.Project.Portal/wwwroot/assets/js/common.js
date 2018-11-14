// 以下代码是配置layui扩展模块的目录，以及加载主题
layui.config({
    base: getProjectUrl() + 'module/'
}).extend({
    formSelects: 'formSelects/formSelects-v4',
    treetable: 'treetable-lay/treetable',
    dropdown: 'dropdown/dropdown',
    notice: 'notice/notice',
    step: 'step-lay/step'
}).use(['layer'], function () {
    var $ = layui.jquery;
    var layer = layui.layer;

    // 加载缓存的主题
    var theme = layui.data('geekweb').theme;
    if (theme) {
        layui.link(getThemeDir() + theme + '.css');
    }

    // 移除loading动画
    setTimeout(function () {
        $('.page-loading').addClass('layui-hide');
    }, window === top ? 1500 : 300);

});

// 移除主题
function removeTheme() {
    var app = 'layuicss-' + getThemeDir().replace(/\.|\//g, '');
    layui.jquery('link[id^="' + app + '"]').remove();
}

// 获取主题css的路径
function getThemeDir() {
    //console.log(getProjectUrl() + 'assets/css/theme/');
    return getProjectUrl() + 'assets/css/theme/';
}

// 获取当前项目的绝对路径
function getProjectUrl() {
    var layuiDir = layui.cache.dir;
    if (!layuiDir) {
        var js = document.scripts, last = js.length - 1, src;
        for (var i = last; i > 0; i--) {
            if (js[i].readyState === 'interactive') {
                src = js[i].src;
                break;
            }
        }
        var jsPath = src || js[last].src;
        layuiDir = jsPath.substring(0, jsPath.lastIndexOf('/') + 1);
    }
    return layuiDir.substring(0, layuiDir.indexOf('assets'));
}