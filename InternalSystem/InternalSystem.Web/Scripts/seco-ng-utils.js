var sc = sc || {};

//support libraries
(function ($) {
    $.fn.scWinSize = function (options) {
        var settings = $.extend({
            //target width /height
            aspectRatio: 12 / 7
        }, options);

        var windowW = $(window).width();
        var windowH = $(window).height();
        var windowAspect = windowW / windowH;

        return this.each(function () {
            $(this).removeAttr('style');
            if (windowAspect < settings.aspectRatio) {
                $(this).css({
                    "height": "100%",
                    "width": parseInt(windowH * settings.aspectRatio) + "px",
                    "margin-left": parseInt((windowH * settings.aspectRatio - windowW) / -2) + "px"
                });
            }
            else {
                $(this).css({
                    "height": parseInt(windowW / settings.aspectRatio) + "px",
                    "width": "100%",
                    "margin-top": parseInt((windowW / settings.aspectRatio - windowH) / -2) + "px"
                });
            }
        });
    };
})(jQuery);

//scUtils module
(function (window, angular) {
    'use strict';
    if (!sc.modUtils) {
        sc.modUtils = angular.module('scUtils', []);
    }

    //module initialize code
    sc.modUtils.run(['$location', '$rootScope', '$anchorScroll', function ($location, $rootScope, $anchorScroll) {
        $rootScope.$on('$routeChangeSuccess', function (event, newUrl, oldUrl) {
            $rootScope.curPath = $location.path();

            if (typeof ga != 'undefined') {
                ga('push', 'pageview', $rootScope.curPath);
            }

            setTimeout(function () {
                if ($location.hash()) {
                    $anchorScroll();
                }
            }, 300);
        });

        //和autoscroll配合使用
        $rootScope.scrollTop = function () {
            return !$location.hash();
        };

        $rootScope.goto = function (url) {
            $location.path(url);
        };
    }]);

    //resize window according to portion
    sc.modUtils.directive('scResize', function () {
        var aspectRatio = 12 / 7; //deafult aspect
        return {
            restrict: 'AC',
            link: function (scope, element, attrs) {
                if (attrs.scResize) {
                    aspectRatio = parseFloat(attrs.scResize);
                }
                element.scWinSize({
                    aspectRatio: aspectRatio
                });
                $(window).resize(function () {
                    element.scWinSize({
                        aspectRatio: aspectRatio
                    });
                });
            }
        };
    });

    //anime delay
    sc.modUtils.directive('scOnDelay', ['$timeout', '$animate', function ($timeout, $animate) {
        return {
            link: function (scope, element, attrs) {
                $timeout(function () {
                    $animate.addClass(element, 'on');
                }, attrs.scOnDelay);
            }
        };
    }]);

    //click to add
    sc.modUtils.directive('scOnAdd', ['$animate', function ($animate) {
        return {
            link: function (scope, element, attrs) {
                element.click(function () {
                    if (attrs.scOnAdd) {
                        $(attrs.scOnAdd).each(function () {
                            $animate.addClass($(this), 'on');
                        });
                    }
                    else {
                        $animate.addClass(element, 'on');
                    }
                });
            }
        }
    }]);

    //click to remove
    sc.modUtils.directive('scOnRemove', ['$animate', function ($animate) {
        return {
            link: function (scope, element, attrs) {
                element.click(function () {
                    if (attrs.scOnRemove) {
                        $(attrs.scOnRemove).each(function () {
                            $animate.removeClass($(this), 'on');
                        });
                    }
                    else {
                        $animate.removeClass(element, 'on');
                    }
                });
            }
        }
    }]);

    //click to toggle
    sc.modUtils.directive('scOnToggle', ['$animate', function ($animate) {
        return {
            link: function (scope, element, attrs) {
                element.click(function () {
                    if (attrs.scOnToggle) {
                        if ($(attrs.scOnToggle).hasClass('on')) {
                            $(attrs.scOnToggle).each(function () {
                                $animate.removeClass($(this), 'on');
                            });
                        }
                        else {
                            $(attrs.scOnToggle).each(function () {
                                $animate.addClass($(this), 'on');
                            });
                        }
                    }
                    else {
                        if (element.hasClass('on')) {
                            $animate.removeClass(element, 'on');
                        }
                        else {
                            $animate.addClass(element, 'on');
                        }
                    }
                });
            }
        }
    }]);

    //switch on class between lists
    sc.modUtils.directive('scOnSwitch', function () {
        return {
            link: function (scope, element, attrs) {
                element.children().click(function () {
                    if (attrs.scOnSwitch) {
                        var index = $(this).index();
                        $(attrs.scOnSwitch).each(function () {
                            $(this).children().eq(index).addClass('on').siblings().removeClass('on');
                        });
                    }
                    else {
                        $(this).addClass('on').siblings().removeClass('on');
                    }
                });
            }
        }
    });

    //menu display detection
    sc.modUtils.directive('scOnComp', ['$location', '$animate', function ($location, $animate) {
        return {
            link: function (scope, element, attrs) {
                function check(curScene) {
                    var scenes = attrs.scOnComp.split(' ');
                    var isHide = false;
                    if (scenes[0] === '!') {
                        isHide = true;
                    }
                    for (var i = 0; i < scenes.length; i++) {
                        if (curScene == scenes[i]) {
                            return isHide ? false : true;
                        }
                    }
                    if (isHide) {
                        return true;
                    }
                }
                function toggleState(newVal) {
                    if (check(newVal)) {
                        $animate.addClass(element, 'on');
                    }
                    else {
                        $animate.removeClass(element, 'on');
                    }
                }
                if (attrs.scCompShowByUrl) {
                    scope.$watch(function () {
                        return $location.path();
                    }, toggleState);
                }
                else if (attrs.scCompShowByVal) {
                    scope.$watch(attrs.scCompShowByVal, toggleState);
                }
                else {
                    scope.$watch('view', toggleState);
                }
            }
        };
    }]);

    //next item selection
    sc.modUtils.directive('scOnNext', function () {
        return {
            link: function (scope, element, attrs) {
                element.click(function () {
                    var $items = $(attrs.scOnNext).children('.on');
                    if (!$items.last().is(':last-child')) {
                        $items.last().next().addClass('on');
                        $items.first().removeClass('on');
                    }
                });

            }
        };
    });

    //prev item selection
    sc.modUtils.directive('scOnPrev', function () {
        return {
            link: function (scope, element, attrs) {
                element.click(function () {
                    var $items = $(attrs.scOnPrev).children('.on');
                    if (!$items.first().is(':first-child')) {
                        $items.first().prev().addClass('on');
                        $items.last().removeClass('on');
                    }
                });
            }
        };
    });

    //add on class when current location.path equals href target
    sc.modUtils.directive('scOnNavlink', ['$location', '$animate', function ($location, $animate) {
        return {
            link: function (scope, element, attrs) {
                if (!/^#\/.+$/.test(attrs.href)) {
                    return;
                }
                var matched = false;
                var path = $location.path();
                if (attrs.scOnNavlink) {
                    angular.forEach(attrs.scOnNavlink.split(' '), function (val) {
                        if (path.indexOf(val) == 0) {
                            matched = true;
                        }
                    });
                }
                else if (path.indexOf(attrs.href.substr(1)) == 0) {
                    matched = true;
                }
                if (matched) {
                    $animate.addClass(element, 'on');
                }
                else {
                    $animate.removeClass(element, 'on');
                }
            }
        };
    }]);

    //general form validation plugin
    sc.modUtils.directive('scFormValidation', function () {
        return {
            link: function (scope, element, attrs) {
                var defaultMessage = '表单验证未通过。';
                if (attrs.scFormValidation) {
                    defaultMessage = attrs.scFormValidation;
                }
                if (attrs.name) {
                    scope[attrs.name].checkForm = function () {
                        var result = element.find(':input.ng-invalid').eq(0).data('msg');
                        if (!result && element.find(':input.ng-invalid').length) {
                            result = defaultMessage;
                        }
                        return result;
                    };
                }
            }
        };
    });
    
    /*
     * 导航当前路径绑定on插件，支持NG动画效果
     */
    sc.modUtils.directive('scOnPath', ['$location', '$animate', function ($location, $animate) {
        return {
            link: function (scope, element, attrs) {
                scope.$on('$routeChangeSuccess', function (event, newUrl, oldUrl) {
                    var path = $location.path();
                    if (!path || !attrs.scOnPath) {
                        return;
                    }
                    if (attrs.exact && attrs.scOnPath === path) {
                        $animate.addClass(element, 'on');
                    }
                    else if (!attrs.exact && path.indexOf(attrs.scOnPath) > -1) {
                        $animate.addClass(element, 'on');
                    }
                    else {
                        $animate.removeClass(element, 'on');
                    }
                });
            }
        };
    }]);

    /*
     * 主要针对手机，横屏时给当前元素加on，纵屏后取消。
     */
    sc.modUtils.directive('scOnHorizontal', ['$animate', function ($animate) {
        return {
            link: function (scope, element, attrs) {
                window.onorientationchange = function () {
                    if (window.orientation === 90 || window.orientation === -90) {
                        $animate.addClass(element, 'on');
                    }
                    else {
                        $animate.removeClass(element, 'on');
                    }
                };
            }
        };
    }]);

    /*
     * 加入背景音乐，且音乐播放时，当前元素加on，结束播放或者暂停后，on取消。
     */
    sc.modUtils.directive('scOnMusic', function () {
        return {
            link: function (scope, element, attrs) {
                var player = $('<audio></audio>').appendTo(element)[0];

                player.onplay = function () {
                    scope.$apply(function () {
                        element.addClass('on');
                    });
                };

                player.onpause = function () {
                    scope.$apply(function () {
                        element.removeClass('on');
                    });
                };

                player.src = attrs.scOnMusic;
                player.autoplay = true;
                player.loop = true;

                element.click(function () {
                    if (player.paused) {
                        player.play();
                    }
                    else {
                        player.pause();
                    }
                });
            }
        };
    });

})(window, window.angular);

