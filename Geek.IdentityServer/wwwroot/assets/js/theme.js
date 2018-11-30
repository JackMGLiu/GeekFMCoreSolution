layui.use(['layer', 'form', 'admin'], function () {
    var layer = layui.layer;
    var form = layui.form;
    var admin = layui.admin;

    // 切换主题
    function doChangeTheme(theme) {
        $('.btnTheme').removeClass('active');
        if (theme) {
            $('.btnTheme[theme=' + theme + ']').addClass('active');
            layui.data('geekweb', { key: 'theme', value: theme });
        } else {
            $('.btnTheme').eq(0).addClass('active');
            layui.data('geekweb', { key: 'theme', remove: true });
        }
        try {
            top.layui.admin.changeTheme(theme ? getThemeDir() + theme + '.css' : theme);
        } catch (e) {
            console.warn(e);
        }
    }

    doChangeTheme(layui.data('geekweb').theme);

    // 切换主题按钮
    $('.btnTheme').click(function () {
        var theme = $(this).attr('theme');
        doChangeTheme(theme);
    });

    // 关闭/开启页脚
    var openFooter = layui.data('geekweb').openFooter;
    $('#setFooter').prop('checked', openFooter === undefined ? true : openFooter);
    form.render('checkbox');
    form.on('switch(setFooter)', function (data) {
        var checked = data.elem.checked;
        layui.data('geekweb', { key: 'openFooter', value: checked });
        if (checked) {
            top.layui.jquery('.layui-layout-admin .layui-footer').css('display', 'block');
            top.layui.jquery('.layui-layout-admin .layui-body').css('bottom', '44px');
        } else {
            top.layui.jquery('.layui-layout-admin .layui-footer').css('display', 'none');
            top.layui.jquery('.layui-layout-admin .layui-body').css('bottom', '0');
        }
    });

    // 关闭/开启Tab记忆功能
    var cacheTab = layui.data('geekweb').cacheTab;
    $('#setTab').prop('checked', cacheTab === undefined ? true : cacheTab);
    form.render('checkbox');
    form.on('switch(setTab)', function (data) {
        var checked = data.elem.checked;
        layui.data('geekweb', { key: 'cacheTab', value: checked });
        top.layui.index.cacheTab = checked;
        if (checked) {
            var tabList = [];
            top.layui.jquery('.layui-body .layui-tab-content .layui-tab-item iframe').each(function (index) {
                var menuPath = $(this).attr('src');
                var $title = top.layui.jquery('.layui-body .layui-tab-title li').eq(index);
                var menuName = $title.html();
                menuName = menuName.replace('<i class="layui-icon layui-unselect layui-tab-close">ဆ</i>', '');
                tabList.push({
                    menuPath: menuPath,
                    menuName: menuName
                });
            });
            admin.putTempData('indexTabs', tabList);
            admin.putTempData('tabPosition', top.layui.jquery('.layui-body .layui-tab-content .layui-tab-item.layui-show iframe').attr('src'));
        } else {
            admin.putTempData('indexTabs', []);
            admin.putTempData('tabPosition', undefined);
        }
    });

    // 切换Tab自动刷新
    var tabAutoRefresh = layui.data('geekweb').tabAutoRefresh;
    $('#setRefresh').prop('checked', tabAutoRefresh === undefined ? false : tabAutoRefresh);
    form.render('checkbox');
    form.on('switch(setRefresh)', function (data) {
        var checked = data.elem.checked;
        layui.data('geekweb', { key: 'tabAutoRefresh', value: checked });
        if (checked) {
            top.layui.jquery('.layui-body .layui-tab[lay-filter="admin-pagetabs"]').attr('lay-autoRefresh', 'true');
        } else {
            top.layui.jquery('.layui-body .layui-tab[lay-filter="admin-pagetabs"]').removeAttr('lay-autoRefresh');
        }
    });
});