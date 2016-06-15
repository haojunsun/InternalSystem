var sc = sc || {};
(function (window, angular) {
    'use strict';
    if (!sc.modUtils) {
        sc.modUtils = angular.module('scUtils', []);
    }

    /*
     * touch gesture directive for self defined ng functions
     */
    angular.forEach([
        'panswipeleft:scSwipeLeft',
        'panswiperight:scSwipeRight',
        'panswipeup:scSwipeUp',
        'panswipedown:scSwipeDown'
    ], function (val) {
        if (typeof Hammer == 'undefined') return;
        var hammerEv = val.split(':')[0];
        var angularEv = val.split(':')[1];
        var direction = Hammer.DIRECTION_LEFT;
        if (hammerEv.indexOf('right') > -1) {
            direction = Hammer.DIRECTION_RIGHT;
        }
        else if (hammerEv.indexOf('up') > -1) {
            direction = Hammer.DIRECTION_UP;
        }
        else if (hammerEv.indexOf('down') > -1) {
            direction = Hammer.DIRECTION_DOWN;
        }
        sc.modUtils.directive(angularEv, ['$parse', function ($parse) {
            return {
                link: function (scope, element, attrs) {
                    var fn = $parse(attrs[angularEv]);
                    var hm = new Hammer.Manager(element[0]);
                    hm.add(new Hammer.Swipe({ event: hammerEv, velocity: 0.1, direction: direction }));
                    hm.on(hammerEv, function (e) {
                        scope.$apply(function () {
                            fn(scope, { $event: e });
                        });
                    });
                }
            };
        }]);
    });

    /*
     * original hammer gestures
     */
    angular.forEach([
        'pan:hmPan',
        'pinch:hmPinch',
        'rotate:hmRotate',
        'swipe:hmSwipe',
        'swipeup:hmSwipeUp',
        'swipedown:hmSwipeDown',
        'swipeleft:hmSwipeLeft',
        'swiperight:hmSwipeRight'
    ], function (val) {
        if (typeof Hammer == 'undefined') return;
        var hammerEv = val.split(':')[0];
        var angularEv = val.split(':')[1];
        sc.modUtils.directive(angularEv, ['$parse', function ($parse) {
            return {
                link: function (scope, element, attrs) {
                    var fn = $parse(attrs[angularEv]);
                    var hm = new Hammer(element[0]);
                    if (hammerEv.indexOf('swipeup') > -1) {
                        hm.get('swipe').set({ direction: Hammer.DIRECTION_UP });
                    }
                    if (hammerEv.indexOf('swipedown') > -1) {
                        hm.get('swipe').set({ direction: Hammer.DIRECTION_DOWN });
                    }
                    hm.on(hammerEv, function (e) {
                        scope.$apply(function () {
                            fn(scope, { $event: e });
                        });
                    });
                }
            };
        }]);
    });

    /*
     * touch gesture directive for 'on' series plugins
     * switch on class between lists by drag(pan) swipe touch gesture
     */
    sc.modUtils.directive('scOnSwipe', [
        '$animate', function ($animate) {
            return {
                link: function (scope, element, attrs) {
                    var hm = new Hammer.Manager(element[0]);
                    var target = attrs.scOnSwipe ? $(attrs.scOnSwipe) : element;

                    hm.add(new Hammer.Swipe({ event: 'panswipeleft', velocity: 0.1, direction: Hammer.DIRECTION_LEFT }));
                    hm.add(new Hammer.Swipe({ event: 'panswiperight', velocity: 0.1, direction: Hammer.DIRECTION_RIGHT }));

                    hm.on('panswipeleft', function () {
                        var items = target.children('.on');
                        if (!items.is(':last-child')) {
                            element.removeClass('prev').addClass('next');
                            target.removeClass('prev').addClass('next');
                            items.each(function () {
                                $animate.removeClass($(this), 'on');
                                $animate.addClass($(this).next(), 'on');
                            });
                        }
                    });
                    hm.on('panswiperight', function () {
                        var items = target.children('.on');
                        if (!items.is(':first-child')) {
                            element.removeClass('next').addClass('prev');
                            target.removeClass('next').addClass('prev');
                            items.each(function () {
                                $animate.removeClass($(this), 'on');
                                $animate.addClass($(this).prev(), 'on');
                            });
                        }
                    });
                }
            };
        }
    ]);

    /*
     * press the element for a short time before triggering
     * ngTouch is recommended
     * hammer.js is required
     */
    sc.modUtils.directive('scPress', ['$parse', function ($parse) {
        return {
            link: function (scope, element, attrs) {
                var fn = $parse(attrs.scPress);
                var hammer = new Hammer(element[0]);
                hammer.get('press').set({
                    time: 300
                });
                hammer.on('press', function (e) {
                    scope.$apply(function () {
                        fn(scope, { $event: e });
                    });
                });
            }
        };
    }]);

    /*
     * drag scroll effect by using IScroll
     */
    sc.modUtils.directive('scScroll', ['$parse', function ($parse) {
        var settings = {
            //scrollX: true,
            //scrollY: false,
            //snap: true,
            click: true
        };
        return {
            link: function (scope, element, attrs) {
                if (attrs.scScroll) {
                    //parse string to object as scroll settings
                    var extra = $parse(attrs.scScroll)();
                    angular.extend(settings, extra);
                }
                var scroll = new IScroll(element[0], settings);
            }
        };
    }]);

    /*
     * owl carousel jquery plugin 轮播图播放插件封装
     */
    sc.modUtils.directive('scCarousel', ['$parse', function ($parse) {
        return {
            link: function (scope, element, attrs) {
                var settings = {};
                if (attrs.scCarousel) {
                    angular.extend(settings, $parse(attrs.scCarousel));
                }
                element.owlCarousel(settings);
            }
        };
    }]).directive('scCarouselDelay', ['$parse', function ($parse) {
        return {
            link: function (scope, element, attrs) {
                setTimeout(function () {
                    var settings = {};
                    if (attrs.scCarousel) {
                        angular.extend(settings, $parse(attrs.scCarousel));
                    }
                    element.owlCarousel(settings);
                    $('.carousel').addClass('on');
                }, 800);

            }
        };
    }])
    ;

    /*
     * 滚轮切换场景，需要jquery mousewheel插件
     */
    sc.modUtils.directive('scRouteSwitchWheel', ['$location', function ($location) {
        var isLoading = false;
        return {
            link: function (scope, element, attrs) {
                var scenes = attrs.scRouteSwitchWheel.split(',');
                element.mousewheel(function (e) {
                    if (isLoading) {
                        return;
                    }
                    isLoading = true;
                    var index = scenes.indexOf($location.path());
                    if (e.deltaY > 0) {
                        index = index - 1 < 0 ? 0 : index - 1;
                        element.parent().removeClass('next').addClass('prev');
                    }
                    else {
                        index = scenes.length <= index ? scenes.length - 1 : index + 1;
                        element.parent().removeClass('prev').addClass('next');
                    }
                    var newScene = scenes[index];
                    scope.$apply(function () {
                        $location.path(newScene);
                    });
                    setTimeout(function () {
                        isLoading = false;
                    }, 1000);
                });
            }
        };
    }]);

    /*
     * 手势触摸上下切换场景，需要Hammer插件
     */
    sc.modUtils.directive('pjRouteSwitchTouch', ['$location', function ($location) {
        var isLoading = false;
        return {
            link: function (scope, element, attrs) {
                var scenes = attrs.pjRouteSwitchTouch.split(',');

                var hm = new Hammer(element[0]);
                hm.get('swipe').set({ direction: Hammer.DIRECTION_VERTICAL });

                hm.on('swipeup swipedown', function (e) {
                    if (isLoading) {
                        return;
                    }
                    isLoading = true;
                    var index = scenes.indexOf($location.path());
                    if (e.type === 'swipedown') {
                        index = index - 1 < 0 ? 0 : index - 1;
                        element.parent().removeClass('next').addClass('prev');
                    }
                    else {
                        index = scenes.length <= index ? scenes.length - 1 : index + 1;
                        element.parent().removeClass('prev').addClass('next');
                    }
                    var newScene = scenes[index];
                    scope.$apply(function () {
                        $location.path(newScene);
                    });
                    setTimeout(function () {
                        isLoading = false;
                    }, 1000);
                });
            }
        };
    }]);

    /*
     * 鼠标滚轮向下滚动触发事件
     */
    sc.modUtils.directive('scWheelDown', ['$parse', function ($parse) {
        return {
            link: function (scope, element, attrs) {
                var fn = $parse(attrs.scWheelDown);
                element.mousewheel(function (e) {
                    if (e.deltaY < 0) {
                        fn(scope);
                    }
                });
            }
        };
    }]);

    /*
     * 鼠标滚轮向上滚动触发事件
     */
    sc.modUtils.directive('scWheelUp', ['$parse', function ($parse) {
        return {
            link: function (scope, element, attrs) {
                var fn = $parse(attrs.scWheelUp);
                element.mousewheel(function (e) {
                    if (e.deltaY > 0) {
                        fn(scope);
                    }
                });
            }
        };
    }]);

    /*
     * 预加载插件，当所有assets加载完毕后，在当前标签上加on
     */
    sc.modUtils.directive('scOnPreload', ['$parse', '$animate', function ($parse, $animate) {
        return {
            link: function (scope, element, attrs) {
                var assets = $parse(attrs.scOnPreload)(window); //需要加载的素材,context 为 window
                scope.preloadCount = 0; //加载进度（项目加载数）
                scope.preloadProgress = 0; //加载进度（百分比数值）

                function handleFileComplete(e) {
                    setTimeout(function () {
                        $animate.addClass(element, 'on');
                        scope.preloaded = true;
                        scope.$broadcast('preloaded');
                    }, 1000);
                }

                function handleFileLoad(e) {
                    scope.$apply(function () {
                        scope.preloadCount++;
                        scope.preloadProgress = ((scope.preloadCount / assets.length) * 100).toFixed(0).toString();
                    });
                }

                var preload = new createjs.LoadQueue();
                preload.addEventListener("fileload", handleFileLoad);
                preload.addEventListener("complete", handleFileComplete);

                setTimeout(function () {
                    for (var i = 0; i < assets.length; i++) {
                        preload.loadFile(assets[i]);
                    }
                }, 500);
            }
        };
    }]);

})(window, window.angular);;