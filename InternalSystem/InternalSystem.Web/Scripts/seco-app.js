var sc = sc || {};
sc.app = angular.module('scApp', ['ngAnimate', 'ngTouch', 'scUtils'])

    .controller('RootController', ['$scope', '$location', function ($scope, $location) {

    }])
    .controller('HomeController', ['$scope', '$location', function ($scope, $location) {
        console.log('HomeController');
    }])
    .controller('VideoListController', ['$scope', '$http', function ($scope, $http) {
        console.log('VideoListController');

        $scope.classtype = "";//分类
        $scope.type = "";//类型
        $scope.nation = "";//民族
        $scope.area = "";//地区
        $scope.searchkey = "";//搜索关键字
        $scope.pageIndex = 1;//页码
        $scope.pageSize = 3;//条数每页
        $scope.datacount = 0;//总条数
        $scope.totalpage = 0;//总页数
        $scope.videoList = [];

        $('.classtype').click(function () {
            $scope.classtype = $(this).text();
            if ($scope.classtype == "全部")
                $scope.classtype = "";
            console.log($scope.classtype);
            $scope.pageIndex = 1;
            $scope.getVedioList();
        })
        $('.type').click(function () {
            $scope.type = $(this).text();
            if ($scope.type == "全部")
                $scope.type = "";
            console.log($scope.type);
            $scope.pageIndex = 1;
            $scope.getVedioList();
        })
        $('.nation').click(function () {
            $scope.nation = $(this).text();
            if ($scope.nation == "全部")
                $scope.nation = "";
            console.log($scope.nation);
            $scope.pageIndex = 1;
            $scope.getVedioList();
        })
        $('.area').click(function () {
            $scope.area = $(this).text();
            if ($scope.area == "全国")
                $scope.area = "";
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
                console.log(data);
                $scope.datacount = data.Data.TotalCount;
                $scope.totalpage = data.Data.TotalPaged;
                $scope.videoList = data.Data.Items;
                $.pagination('pages', $scope.pageIndex, $scope.pageSize, data.Data.TotalCount, "", { keyword: 'hello world' });
            }).error(function (data) {
                console.log("查询失败");
            });
        }

        $scope.getVedioList();

        changePage = function (ele) {
            var nextpage = $(ele).text();
            if (nextpage == '第一页') {
                $scope.pageIndex = 1;
            } else if (nextpage == '下一页') {
                $scope.pageIndex = $scope.pageIndex + 1;
            } else if (nextpage == '最后一页') {
                $scope.pageIndex = $scope.totalpage;
            } else if (nextpage == '上一页') {
                $scope.pageIndex = $scope.pageIndex - 1;
            } else {
                $scope.pageIndex = nextpage;
            }
            $scope.getVedioList();
        }

        $scope.openDetail = function (whid) {
            console.log(whid);
        }

    }])
    .controller('DetailController', ['$scope', function ($scope) {
        console.log('DetailController');
    }])
;;