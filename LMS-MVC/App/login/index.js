(function () {
    "use strict";

angular.module("app")
  .component("loginIndex", {
    templateUrl: "/App/login/index.html",
    // bindings: { students: '<' },
    // controllerAs: "model",
    controller: function ($scope, loginService, Identity) {

    //Scope Declaration
    $scope.responseData = "";
    $scope.userName = "";
    $scope.userLoginEmail = "";
    $scope.userLoginPassword = "";

    //Function to Login. This will generate Token 
    $scope.login = function () {

        //This is the information to pass for token based authentication
        var userLogin = {
            grant_type: 'password',
            username: $scope.userLoginEmail,
            password: $scope.userLoginPassword
        };

        var promiselogin = loginService.login(userLogin);

        promiselogin.then(function (resp) {
            Identity.setData(resp.data);
        }, function (err) {
            $scope.responseData = "Error " + err.status;
            Identity.userName = null;
            console.log("Error: ", err.status);
        });

    };
    }

});

})();