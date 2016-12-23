(function () {
    'use strict';

    var module = angular.module('app',
        [ 'ui.router',
          'ngAnimate',
          'app.data',
          'app.common'
        ])

    module.controller('MainCtrl', function MainCtrl($scope) {
        $scope.date = '2016';
    });

})();