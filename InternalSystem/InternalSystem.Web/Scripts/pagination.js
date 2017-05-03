$.extend({
    pagination: function (render, curPage, pageSize, totalRecord, callback, extra) {
        var totalPage = Math.ceil(totalRecord / pageSize);
        var renderObj = $('#' + render);
        renderObj.html('');
        renderObj.addClass('pagination');

        var ulObj = $('<ul></ul>');
        ulObj.appendTo(renderObj);
        var pageArg = { current: curPage, pageSize: pageSize, totalRecord: totalRecord };
        //第一页
        $.createLi(curPage, 1, '第一页', totalPage, pageArg, extra, callback, "").appendTo(ulObj);
        if (parseInt(curPage) - 1 >= 1)
            $.createLi(curPage, curPage - 1, '上一页', totalPage, pageArg, extra, callback, "").appendTo(ulObj);
        i = 1;
        //前省略
        if (curPage > 4) {
            i = parseInt(curPage) - 3;
            $('<li>...</li>').appendTo(ulObj);
        }
        for (; (i <= totalPage) && (i <= parseInt(curPage) + 3) ; i++) {
            $.createLi(curPage, i, i, totalPage, pageArg, extra, callback, "").appendTo(ulObj);
        }
        //后省略
        if (i == parseInt(curPage) + 4) {
            $('<li>...</li>').appendTo(ulObj);
        }
        //下一页
        if (parseInt(curPage) + 1 <= totalPage) {
            $.createLi(curPage, parseInt(curPage) + 1, '下一页', totalPage, pageArg, extra, callback, "").appendTo(ulObj);
        }
        //最后一页
        if (totalRecord == 0)
            $.createLi(curPage, totalPage, '最后一页', totalPage, pageArg, extra, callback, "current").appendTo(ulObj);
        else
            $.createLi(curPage, totalPage, '最后一页', totalPage, pageArg, extra, callback, "").appendTo(ulObj);

        $('<input type="text" class="pagema" style="float:left;width:45px;border:1px solid #2E6AB1;height:22px;margin-left:8px;" />').appendTo(ulObj);
        $('<input type="button" class="pagebtn" value="跳转" onclick="jumpPage($(this));" style="background-color:white;color:#2E6AB1;float:left;width:35px;border:1px solid #2E6AB1;height:22px;margin-left:2px;" />').appendTo(ulObj);
        $('<p style="margin-left:8px;"></p>').text('共' + totalPage + '页，' + totalRecord + '条记录').appendTo(ulObj);
    },
    createLi: function (current, now, content, totalPage, pageArg, extra, callback, classname) {
        var liObj = classname ? $('<li class="' + classname + '">最后一页</li>') : $('<li class="' + classname + '"></li>');
        if (current == now) {
            liObj.html(content).addClass('current');

        }
        else {
            if (!classname) {
                $('<a onclick="changePage($(this));"></a>').html(content).appendTo(liObj);
                liObj.addClass('ordinary');
                liObj.bind('click', { current: now, pagination: pageArg, extra: extra }, callback);
            }
        }
        return liObj;
    }
})




