﻿var sc = sc || {};
sc.app = angular.module('scApp', [])

    .controller('RootController', ['$scope', '$location', function ($scope, $location) {

    }])
    .controller('HomeController', ['$scope', '$location', '$http', function ($scope, $location, $http) {
        $scope.pageIndex = 1;//页码
        $scope.pageSize = 5;//条数每页
        $scope.mainVideoList = [];

        $scope.getMainVedioList = function () {
            $scope.videoList = [];
            $http.post(sc.baseUrl + 'Import/Search', { "firstlevel": "", "dataformat": "视频", "nation": "", "municipalities": "", "title": "", "pageSize": $scope.pageSize, "pageIndex": $scope.pageIndex }).success(function (data) {
                $scope.mainVideoList = data.Data.Items;
            }).error(function (data) {
                console.log("查询失败");
            });
        }

        //进入详情页
        openDetail = function (whid, ele) {
            console.log(whid);
            window.location.href = '/home/detail?video=' + whid;
        }

        $scope.getMainVedioList();
    }])
    .controller('VideoListController', ['$scope', '$http', '$location', function ($scope, $http, $location) {
        $scope.classtype = "";//分类
        $scope.type = "";//类型
        $scope.nation = "";//民族
        $scope.area = "";//地区
        $scope.searchkey = "";//搜索关键字
        $scope.pageIndex = 1;//页码
        $scope.pageSize = 6;//条数每页
        $scope.datacount = 0;//总条数
        $scope.totalpage = 0;//总页数
        $scope.videoList = [];

        $('.classtype').click(function () {
            $scope.classtype = $(this).text();
            if ($scope.classtype == "全部") {
                $scope.classtype = "";
                $('.clearList').find('div').eq(0).hide();
            }
            console.log($scope.classtype);
            $scope.pageIndex = 1;
            $scope.getVedioList();
        })
        $('.type').click(function () {
            $scope.type = $(this).text();
            if ($scope.type == "全部") {
                $scope.type = "";
                $('.clearList').find('div').eq(1).hide();
            }
            console.log($scope.type);
            $scope.pageIndex = 1;
            $scope.getVedioList();
        })
        $('.nation').click(function () {
            $scope.nation = $(this).text();
            if ($scope.nation == "全部") {
                $scope.nation = "";
                $('.clearList').find('div').eq(2).hide();
            }
            console.log($scope.nation);
            $scope.pageIndex = 1;
            $scope.getVedioList();
        })
        $('.area').click(function () {
            $scope.area = $(this).text();
            if ($scope.area == "全国") {
                $scope.area = "";
                $('.clearList').find('div').eq(3).hide();
            }
            console.log($scope.area);
            $scope.pageIndex = 1;
            $scope.getVedioList();
        })
        $scope.changeSearchkey = function () {
            $scope.searchkey = $("#searchkey").val();
            console.log($scope.searchkey);
            $scope.pageIndex = 1;
            $scope.getVedioList();
        }

        $scope.getVedioList = function () {
            $scope.videoList = [];
            $http.post(sc.baseUrl + 'Import/Search', { "firstlevel": $scope.classtype, "dataformat": $scope.type, "nation": $scope.nation, "municipalities": $scope.area, "title": $scope.searchkey, "pageSize": $scope.pageSize, "pageIndex": $scope.pageIndex }).success(function (data) {
                //console.log(data);
                $scope.datacount = data.Data.TotalCount;
                $scope.totalpage = data.Data.TotalPaged;
                $scope.videoList = data.Data.Items;
                //重新加载页码
                $.pagination('pages', $scope.pageIndex, $scope.pageSize, data.Data.TotalCount, "", { keyword: 'hello world' });

            }).error(function (data) {
                console.log("查询失败");
            });
        }

        $scope.getVedioList();

        //翻页
        changePage = function (ele) {
            var nextpage = $(ele).text();
            if (nextpage == '第一页') {
                $scope.pageIndex = 1;
            } else if (nextpage == '下一页') {
                $scope.pageIndex = parseInt($scope.pageIndex) + 1;
            } else if (nextpage == '最后一页') {
                $scope.pageIndex = $scope.totalpage;
            } else if (nextpage == '上一页') {
                $scope.pageIndex = $scope.pageIndex - 1;
            } else {
                $scope.pageIndex = nextpage;
            }
            $scope.getVedioList();
        }

        //进入详情页
        openDetail = function (whid, ele) {
            console.log(whid);
            window.location.href = 'detail?video=' + whid;
        }


        //筛选条件
        //删除一个
        $(".selectedShow em").live("click", function () {
            $(this).parents(".selectedShow").hide();
            var textTypeIndex = $(this).parents(".selectedShow").index();
            index = textTypeIndex;
            $(".listIndex").eq(index).find("a").removeClass("selected");

            if ($(".listIndex .selected").length < 2) {
                $(".eliminateCriteria").hide();
            }
            if (index == 0)
                $scope.classtype = "";//分类
            else if (index == 1)
                $scope.type = "";//类型
            else if (index == 2)
                $scope.nation = "";//民族
            else if (index == 3)
                $scope.area = "";//地区

            $(".listIndex").eq(index).find("a").eq(0).addClass("selected");

        });
        //清空
        $(".eliminateCriteria").live("click", function () {
            $(".selectedShow").hide();
            $(this).hide();
            $(".listIndex a ").removeClass("selected");
            $scope.classtype = "";//分类
            $scope.type = "";//类型
            $scope.nation = "";//民族
            $scope.area = "";//地区
            $scope.getVedioList();
            $(".listIndex").eq(0).find("a").eq(0).addClass("selected");
            $(".listIndex").eq(1).find("a").eq(0).addClass("selected");
            $(".listIndex").eq(2).find("a").eq(0).addClass("selected");
            $(".listIndex").eq(3).find("a").eq(0).addClass("selected");
        });
    }])
    .controller('DetailController', ['$scope', '$http', function ($scope, $http) {
        var whid = window.location.search.indexOf('=') > -1 ? window.location.search.split('=')[1] : "";
        $scope.videoinfo = {};

        //获取单条数据
        $scope.getVideoInfo = function () {
            if (!whid)
                return;
            $http.post(sc.baseUrl + 'Import/Find', { "id": whid }).success(function (data) {
                console.log(data);
                $scope.videoinfo = data;
                //console.log($scope.videoinfo.FileName);

                var curWwwPath = window.document.location.href;
                var pathName = window.document.location.pathname;
                var pos = curWwwPath.indexOf(pathName);
                var localhostPaht = curWwwPath.substring(0, pos);

                $('#videosource').attr("src", localhostPaht + $scope.videoinfo.FileName.substring(1));
                setTimeout(function () {
                    projekktor('#videoplayer'); // instantiation
                }, 100)
            }).error(function (data) {
                console.log("查询失败");
            });
        }

        $scope.getVideoInfo();
    }])
;;