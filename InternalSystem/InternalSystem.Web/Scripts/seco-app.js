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
        $scope.pageIndex = 1;
        $scope.pageSize = 20;

        $('.classtype').click(function () {
            $scope.classtype = $(this).text();
            console.log($scope.classtype);
            $scope.getVedioList();
        })
        $('.type').click(function () {
            $scope.type = $(this).text();
            console.log($scope.type);
            $scope.getVedioList();
        })
        $('.nation').click(function () {
            $scope.nation = $(this).text();
            console.log($scope.nation);
            $scope.getVedioList();
        })
        $('.area').click(function () {
            $scope.area = $(this).text();
            console.log($scope.area);
            $scope.getVedioList();
        })
        $scope.changeSearchkey = function () {
            $scope.searchkey = $("#searchkey").val();
            console.log($scope.searchkey);
            $scope.getVedioList();
        }

        $scope.getVedioList = function () {
            console.log(sc.baseUrl + 'Import/Search');
            $http.post(sc.baseUrl + 'Import/Search', { "firstlevel": $scope.classtype, "dataformat": $scope.type, "nation": $scope.nation, "municipalities": $scope.area, "title": $scope.searchkey, "pageSize": $scope.pageSize, "pageIndex": $scope.pageIndex }).success(function (data) {
                console.log(data);
            }).error(function (data) {
                console.log("查询失败");
            });
        }

    }])
    .controller('DetailController', ['$scope', function ($scope) {
        console.log('DetailController');
    }])
;;