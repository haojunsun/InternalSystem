var sc = sc || {};
sc.app = angular.module('scApp', ['ngAnimate', 'ngTouch', 'scUtils'])
    //.config(['$routeProvider', function ($routeProvider) {
    //    $routeProvider.when('/', {
    //        templateUrl: 'Home/Main',
    //        controller: 'HomeController'
    //    }).when('/Home/Main', {
    //        templateUrl: 'Home/Main',
    //        controller: 'HomeController'
    //    }).when('/home/videolist', {
    //        templateUrl: 'Home/VideoList',
    //        controller: 'VideoListController'
    //    }).otherwise({
    //        redirectTo: '/'
    //    });
    //}])
    .controller('RootController', ['$scope', '$location',  function ($scope, $location) {
        
    }])
    .controller('HomeController', ['$scope', '$location', function ($scope, $location) {
        alert('1');
    }])
    .controller('VideoListController', ['$scope', function ($scope) {
        alert('2');
    }])
    .controller('DetailController', ['$scope', function ($scope) {
        alert('3');
    }])
    
;;