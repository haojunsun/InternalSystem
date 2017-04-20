var sc = sc || {};
sc.app = angular.module('scApp', [])
    .controller('RootController', ['$scope', '$location', function ($scope, $location) {

    }])
    .controller('HomeController', ['$scope', '$location', '$http', function ($scope, $location, $http) {
        $scope.pageIndex = 1;//页码
        $scope.pageSize = 5;//条数每页
        $scope.mainVideoList = [];

        $scope.getMainVedioList = function () {
            $scope.videoList = [];
            $http.post(sc.baseUrl + 'Import/NewSearch', { "key": '', "firstLevelOfArtClassification": '', "secondLevelOfEthnicGroup": '', "type": '', "pageSize": 5, "pageIndex": 1 }).success(function (data) {
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
    .controller('TextListController', ['$scope', '$http', '$location', function ($scope, $http, $location) {
        $scope.pageIndex = 1;//页码
        $scope.pageSize = 6;//条数每页
        $scope.datacount = 0;//总条数
        $scope.totalpage = 0;//总页数
        $scope.textList = [];

        $scope.resourceType = location.search.indexOf('=') > -1 ? location.search.split('=')[1] : "";

        $scope.type = '文字';

        $scope.getTextList = function () {
            $scope.textList = [];
            $http.post(sc.baseUrl + 'Import/NewSearch', { "key": "", "firstLevelOfArtClassification": "", "secondLevelOfEthnicGroup": "", "type": "文字", "pageSize": $scope.pageSize, "pageIndex": $scope.pageIndex }).success(function (data) {
                console.log(data.Data.Items);
                $scope.datacount = data.Data.TotalCount;
                $scope.totalpage = data.Data.TotalPaged;
                $scope.textList = data.Data.Items;
                //重新加载页码
                $.pagination('pages', $scope.pageIndex, $scope.pageSize, data.Data.TotalCount, "", { keyword: 'hello world' });

            }).error(function (data) {
                console.log("查询失败");
            });
        }

        $scope.getTextList();

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
            $scope.getTextList();
        }

        //进入详情页
        openDetail = function (whid, ele) {
            window.open('TextDetail?video=' + whid);
        }
    }])
        .controller('ImgListController', ['$scope', '$http', '$location', function ($scope, $http, $location) {
            $scope.searchkey = "";//搜索关键字
            $scope.pageIndex = 1;//页码
            $scope.pageSize = 9;//条数每页
            $scope.datacount = 0;//总条数
            $scope.totalpage = 0;//总页数
            $scope.imgList = [];


            $scope.getImgList = function () {
                $scope.videoList = [];
                $http.post(sc.baseUrl + 'Import/NewSearch', { "key": "", "firstLevelOfArtClassification": "", "secondLevelOfEthnicGroup": "", "type": "图片", "pageSize": $scope.pageSize, "pageIndex": $scope.pageIndex }).success(function (data) {
                    console.log(data.Data.Items);
                    $scope.datacount = data.Data.TotalCount;
                    $scope.totalpage = data.Data.TotalPaged;
                    $scope.imgList = data.Data.Items;
                    //重新加载页码
                    $.pagination('pages', $scope.pageIndex, $scope.pageSize, data.Data.TotalCount, "", { keyword: 'hello world' });

                }).error(function (data) {
                    console.log("查询失败");
                });
            }

            $scope.getImgList();

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
                $scope.getImgList();
            }

            //进入详情页
            openDetail = function (whid, ele) {
                window.open('ImgDetail?video=' + whid);
            }
        }])
     .controller('VideoListController', ['$scope', '$http', '$location', function ($scope, $http, $location) {
         $scope.searchkey = "";//搜索关键字
         $scope.pageIndex = 1;//页码
         $scope.pageSize = 6;//条数每页
         $scope.datacount = 0;//总条数
         $scope.totalpage = 0;//总页数
         $scope.videoList = [];


         $scope.getVedioList = function () {
             $scope.videoList = [];
             $http.post(sc.baseUrl + 'Import/NewSearch', { "key": "", "firstLevelOfArtClassification": "", "secondLevelOfEthnicGroup": "", "type": "视频", "pageSize": $scope.pageSize, "pageIndex": $scope.pageIndex }).success(function (data) {
                 console.log("data:", data.Data.Items);
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
             window.open('VideoDetail?video=' + whid);
         }
     }])
     .controller('MusicListController', ['$scope', '$http', '$location', function ($scope, $http, $location) {
         $scope.searchkey = "";//搜索关键字
         $scope.pageIndex = 1;//页码
         $scope.pageSize = 8;//条数每页
         $scope.datacount = 0;//总条数
         $scope.totalpage = 0;//总页数
         $scope.MusicList = [];


         $scope.getMusicList = function () {
             $scope.videoList = [];
             $http.post(sc.baseUrl + 'Import/NewSearch', { "key": "", "firstLevelOfArtClassification": "", "secondLevelOfEthnicGroup": "", "type": "音频", "pageSize": $scope.pageSize, "pageIndex": $scope.pageIndex }).success(function (data) {
                 console.log(data.Data.Items);
                 $scope.datacount = data.Data.TotalCount;
                 $scope.totalpage = data.Data.TotalPaged;
                 $scope.MusicList = data.Data.Items;
                 //重新加载页码
                 $.pagination('pages', $scope.pageIndex, $scope.pageSize, data.Data.TotalCount, "", { keyword: 'hello world' });

             }).error(function (data) {
                 console.log("查询失败");
             });
         }

         $scope.getMusicList();

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
             $scope.getMusicList();
         }

         //进入详情页
         openDetail = function (whid, ele) {
             window.open('MusicDetail?video=' + whid);
         }
     }])
      .controller('SearchController', ['$scope', '$http', '$location', function ($scope, $http, $location) {
          $scope.classtype = "";//分类
          $scope.type = "";//类型
          $scope.nation = "";//民族
          $scope.area = "";//地区
          $scope.searchkey = "";//搜索关键字
          $scope.pageIndex1 = 1;//页码
          $scope.pageIndex2 = 1;//页码
          $scope.pageIndex3 = 1;//页码
          $scope.pageIndex4 = 1;//页码
          $scope.pageSize1 = 8;//条数每页
          $scope.pageSize2 = 6;//条数每页
          $scope.pageSize3 = 6;//条数每页
          $scope.pageSize4 = 6;//条数每页
          $scope.datacount1 = 0;//总条数
          $scope.datacount2 = 0;//总条数
          $scope.datacount3 = 0;//总条数
          $scope.datacount4 = 0;//总条数
          $scope.totalpage1 = 0;//总页数
          $scope.totalpage2 = 0;//总页数
          $scope.totalpage3 = 0;//总页数
          $scope.totalpage4 = 0;//总页数
          $scope.textList = [];
          $scope.imgList = [];
          $scope.videoList = [];
          $scope.musicList = [];

          $scope.resourceType = location.search.indexOf('=') > -1 ? location.search.split('=')[1] : "";


          $scope.type = '文字';

          //艺术门类
          $('.classtype').click(function () {
              $scope.classtype = $(this).text();
              if ($scope.classtype == "全部") {
                  $scope.classtype = "";
                  $('.clearList').find('div').eq(0).hide();
              }
              console.log($scope.classtype);
              $scope.pageIndex1 = 1;
              $scope.pageIndex2 = 1;
              $scope.pageIndex3 = 1;
              $scope.pageIndex4 = 1;

              $scope.gettextList();
              $scope.getmusicList();
              $scope.getvideoList();
              $scope.getimgList();
          })
          //民族
          $('.nation').click(function () {
              $scope.nation = $(this).text();
              if ($scope.nation == "全部") {
                  $scope.nation = "";
                  $('.clearList').find('div').eq(2).hide();
              }
              console.log($scope.nation);
              $scope.pageIndex1 = 1;
              $scope.pageIndex2 = 1;
              $scope.pageIndex3 = 1;
              $scope.pageIndex4 = 1;

              $scope.gettextList();
              $scope.getmusicList();
              $scope.getvideoList();
              $scope.getimgList();
          })

          $scope.changeSearchkey = function () {
              $scope.searchkey = $("#searchkey").val();
              console.log("key:", $scope.searchkey);

              $scope.pageIndex1 = 1;
              $scope.pageIndex2 = 1;
              $scope.pageIndex3 = 1;
              $scope.pageIndex4 = 1;

              $scope.gettextList();
              $scope.getmusicList();
              $scope.getvideoList();
              $scope.getimgList();
          }

          $scope.gettextList = function () {
              $scope.textList = [];
              $http.post(sc.baseUrl + 'Import/NewSearch', { "key": $scope.searchkey, "firstLevelOfArtClassification": $scope.classtype, "secondLevelOfEthnicGroup": $scope.nation, "type": "文字", "pageSize": $scope.pageSize3, "pageIndex": $scope.pageIndex3 }).success(function (data) {
                  console.log(data.Data.Items);
                  $scope.datacount3 = data.Data.TotalCount;
                  $scope.totalpage3 = data.Data.TotalPaged;
                  $scope.textList = data.Data.Items;
                  //重新加载页码
                  $.pagination('pages3', $scope.pageIndex3, $scope.pageSize3, data.Data.TotalCount, "", { keyword: 'hello world' });

              }).error(function (data) {
                  console.log("查询失败");
              });
          }
          $scope.getmusicList = function () {
              $scope.musicList = [];
              $http.post(sc.baseUrl + 'Import/NewSearch', { "key": $scope.searchkey, "firstLevelOfArtClassification": $scope.classtype, "secondLevelOfEthnicGroup": $scope.nation, "type": "音频", "pageSize": $scope.pageSize4, "pageIndex": $scope.pageIndex4 }).success(function (data) {
                  console.log(data.Data.Items);
                  $scope.datacount4 = data.Data.TotalCount;
                  $scope.totalpage4 = data.Data.TotalPaged;
                  $scope.musicList = data.Data.Items;
                  //重新加载页码
                  $.pagination('pages4', $scope.pageIndex4, $scope.pageSize4, data.Data.TotalCount, "", { keyword: 'hello world' });

              }).error(function (data) {
                  console.log("查询失败");
              });
          }
          $scope.getvideoList = function () {
              $scope.videoList = [];
              $http.post(sc.baseUrl + 'Import/NewSearch', { "key": $scope.searchkey, "firstLevelOfArtClassification": $scope.classtype, "secondLevelOfEthnicGroup": $scope.nation, "type": "视频", "pageSize": $scope.pageSize2, "pageIndex": $scope.pageIndex2 }).success(function (data) {
                  console.log(data.Data.Items);
                  $scope.datacount2 = data.Data.TotalCount;
                  $scope.totalpage2 = data.Data.TotalPaged;
                  $scope.videoList = data.Data.Items;
                  //重新加载页码
                  $.pagination('pages2', $scope.pageIndex2, $scope.pageSize2, data.Data.TotalCount, "", { keyword: 'hello world' });

              }).error(function (data) {
                  console.log("查询失败");
              });
          }

          $scope.getimgList = function () {
              $scope.imgList = [];
              $http.post(sc.baseUrl + 'Import/NewSearch', { "key": $scope.searchkey, "firstLevelOfArtClassification": $scope.classtype, "secondLevelOfEthnicGroup": $scope.nation, "type": "图片", "pageSize": $scope.pageSize1, "pageIndex": $scope.pageIndex1 }).success(function (data) {
                  console.log(data.Data.Items);
                  $scope.datacount1 = data.Data.TotalCount;
                  $scope.totalpage1 = data.Data.TotalPaged;
                  $scope.imgList = data.Data.Items;
                  //重新加载页码
                  $.pagination('pages1', $scope.pageIndex1, $scope.pageSize1, data.Data.TotalCount, "", { keyword: 'hello world' });

              }).error(function (data) {
                  console.log("查询失败");
              });
          }

          //$scope.getList();

          //翻页
          changePage = function (ele) {
              var parid = $(ele).parent().parent().parent().attr('id');
              var nextpage = $(ele).text();
              var tempPageIndex = 1;
              if (nextpage == '第一页') {
                  $tempPageIndex = 1;
              } else if (nextpage == '下一页') {
                  if (parid == 'pages1') {
                      tempPageIndex = parseInt($scope.pageIndex1) + 1;
                  } else if (parid == 'pages2') {
                      tempPageIndex = parseInt($scope.pageIndex2) + 1;
                  } else if (parid == 'pages3') {
                      tempPageIndex = parseInt($scope.pageIndex3) + 1;
                  } else if (parid == 'pages4') {
                      tempPageIndex = parseInt($scope.pageIndex4) + 1;
                  }
              } else if (nextpage == '最后一页') {
                  if (parid == 'pages1') {
                      tempPageIndex = $scope.totalpage1;
                  } else if (parid == 'pages2') {
                      tempPageIndex = $scope.totalpage2;
                  } else if (parid == 'pages3') {
                      tempPageIndex = $scope.totalpage3;
                  } else if (parid == 'pages4') {
                      tempPageIndex = $scope.totalpage4;
                  }
              } else if (nextpage == '上一页') {
                  if (parid == 'pages1') {
                      tempPageIndex = $scope.pageIndex1 - 1;
                  } else if (parid == 'pages2') {
                      tempPageIndex = $scope.pageIndex2 - 1;
                  } else if (parid == 'pages3') {
                      tempPageIndex = $scope.pageIndex3 - 1;
                  } else if (parid == 'pages4') {
                      tempPageIndex = $scope.pageIndex4 - 1;
                  }
              } else {
                  tempPageIndex = nextpage;
              }

              if (parid == 'pages1') {
                  $scope.pageIndex1 = tempPageIndex;
                  $scope.gettextList();
              } else if (parid == 'pages2') {
                  $scope.pageIndex2 = tempPageIndex;
                  $scope.getvideoList();
              } else if (parid == 'pages3') {
                  $scope.pageIndex3 = tempPageIndex;
                  $scope.getimgList();
              } else if (parid == 'pages4') {
                  $scope.pageIndex4 = tempPageIndex;
                  $scope.getmusicList();
              }
          }

          //进入详情页
          openDetail1 = function (whid, ele) {
              window.open('ImgDetail?video=' + whid);
          }

          openDetail2 = function (whid, ele) {
              window.open('VideoDetail?video=' + whid);
          }

          openDetail3 = function (whid, ele) {
              window.open('TextDetail?video=' + whid);
          }

          openDetail4 = function (whid, ele) {
              window.open('MusicDetail?video=' + whid);
          }
      }])
    .controller('TextDetailController', ['$scope', '$http', function ($scope, $http) {
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
        //进入详情页
        openDetail = function (whid, ele) {
            window.open('MusicDetail?video=' + whid);
        }
    }])
 .controller('VideoDetailController', ['$scope', '$http', function ($scope, $http) {
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
     //进入详情页
     openDetail = function (whid, ele) {
         window.open('MusicDetail?video=' + whid);
     }
 }])
 .controller('ImgDetailController', ['$scope', '$http', function ($scope, $http) {
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
     //进入详情页
     openDetail = function (whid, ele) {
         window.open('MusicDetail?video=' + whid);
     }
 }])
 .controller('MusicDetailController', ['$scope', '$http', function ($scope, $http) {
     var whid = window.location.search.indexOf('=') > -1 ? window.location.search.split('=')[1] : "";
     $scope.videoinfo = {};
     $scope.musicList = [];

     $scope.getmusicList = function () {
         $scope.musicList = [];
         $http.post(sc.baseUrl + 'Import/NewSearch', { "key": "", "firstLevelOfArtClassification": "", "secondLevelOfEthnicGroup":"", "type": "音频", "pageSize": 6, "pageIndex": 1 }).success(function (data) {
             $scope.musicList = data.Data.Items;
         }).error(function (data) {
             console.log("查询失败");
         });
     }
     $scope.getmusicList();

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

             $('#musicaudio').attr("src", localhostPaht + $scope.videoinfo.FileName.substring(1));
             //$('#videosource').attr("src", localhostPaht + $scope.videoinfo.FileName.substring(1));
             //setTimeout(function () {
             //    projekktor('#videoplayer'); // instantiation
             //}, 100)
         }).error(function (data) {
             console.log("查询失败");
         });
     }

     $scope.getVideoInfo();
     //进入详情页
     openDetail = function (whid, ele) {
         window.open('MusicDetail?video=' + whid);
     }
 }])
;;