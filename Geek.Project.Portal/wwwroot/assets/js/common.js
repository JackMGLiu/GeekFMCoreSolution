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
    }, window == top ? 1500 : 300);

});

// 移除主题
function removeTheme() {
    var app = 'layuicss-' + getThemeDir().replace(/\.|\//g, '');
    layui.jquery('link[id^="' + app + '"]').remove();
}

// 获取主题css的路径
function getThemeDir() {
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

//自定义
/*
 * 描 述：操作类	
 */
var geek = {};
geek = {
    log: function () {
        console.log('=====>' + new Date().getTime() + '<=====');
        var len = arguments.length;
        for (var i = 0; i < len; i++) {
            console.log(arguments[i]);
        }
    },
    // 创建一个GUID
    newGuid: function () {
        var guid = "";
        for (var i = 1; i <= 32; i++) {
            var n = Math.floor(Math.random() * 16.0).toString(16);
            guid += n;
            if ((i === 8) || (i === 12) || (i === 16) || (i === 20)) guid += "-";
        }
        return guid;
    },
    isNullOrEmpty: function (obj) {
        //var obj = this;
        var flag = false;
        if (obj === null || obj === undefined || typeof (obj) === 'undefined' || obj === '') {
            flag = true;
        } else if (typeof (obj) === 'string') {
            obj = obj.trim();
            if (obj === '') {//为空  
                flag = true;
            } else {//不为空  
                obj = obj.toUpperCase();
                if (obj === 'NULL' || obj === 'UNDEFINED' || obj === '{}') {
                    flag = true;
                }
            }
        }
        else {
            flag = false;
        }
        return flag;
    }
};

/*获取和设置表单数据*/
$.fn.getFormData = function (keyValue) {// 获取表单数据
    var resdata = {};
    $(this).find('input,select,textarea').each(function (r) {
        var id = $(this).attr('id');
        if (!!id) {
            var type = $(this).attr('type');
            switch (type) {
                case "radio":
                    if ($("#" + id).is(":checked")) {
                        var _name = $("#" + id).attr('name');
                        resdata[_name] = $("#" + id).val();
                    }
                    break;
                case "checkbox":
                    if ($("#" + id).is(":checked")) {
                        resdata[id] = 1;
                    } else {
                        resdata[id] = 0;
                    }
                    break;
                default:
                    var value = $("#" + id).val();
                    resdata[id] = $.trim(value);
                    break;
            }
            resdata[id] += '';
            if (resdata[id] === '') {
                resdata[id] = '&nbsp;';
            }
            if (resdata[id] === '&nbsp;' && !keyValue) {
                resdata[id] = '';
            }
        }
    });
    return resdata;
};