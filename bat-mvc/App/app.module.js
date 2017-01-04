(function () {
    'use strict';

    var module = angular.module('app',
        [ 'ui.router',
          'ngAnimate',
          'app.data',
          'app.common'
        ])

    module.controller('MainCtrl', function MainCtrl($scope, $http, Identity) {
        $scope.date = '2016';

        $scope.init = function (isAuthenticated) {
            $scope.isAuthenticated = isAuthenticated
            //console.log(isAuthenticated);
            if (isAuthenticated == "ja") {
                //console.log("inloggad");
                Identity.setData({ userName: "Nils", access_token: 1, refresh_token: 1 });
            }
       }

        //$http.get("/api/sample")
        // .then(function (response) {
        //     console.log("respons: ", response);
        //     //$scope.myWelcome = response.data;
        // });
    });

})();