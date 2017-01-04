(function () {
    "use strict"

angular.module("app")
  .component("registerIndex", {
    templateUrl: "/App/register/index.html",
    // bindings: { students: '<' },
    // controllerAs: "model",
    controller: function ($scope, loginService, Identity) {

    //Scope Declaration
    $scope.responseData = "";

    $scope.userName = "";

    $scope.userRegistrationEmail = "kurt@ohlsson.lab";
    $scope.userRegistrationPassword = "Hejsan12345!";
    $scope.userRegistrationConfirmPassword = "Hejsan12345!";

    $scope.accessToken = "";
    $scope.refreshToken = "";
    //Ends Here

    //Function to register user
    $scope.registerUser = function () {

        $scope.responseData = "";

        //The User Registration Information
        var userRegistrationInfo = {
            Email: $scope.userRegistrationEmail,
            Password: $scope.userRegistrationPassword,
            ConfirmPassword: $scope.userRegistrationConfirmPassword
        };

        var promiseregister = loginService.register(userRegistrationInfo);

        promiseregister.then(function (resp) {
            $scope.responseData = "User is Successfully";
            $scope.userRegistrationEmail = "";
            $scope.userRegistrationPassword = "";
            $scope.userRegistrationConfirmPassword = "";
            window.location.href = '/'; //$state.go('home')
            
        }, function (err) {
            $scope.responseData = "Error " + err.status;
            // angular.ForEach
            console.log("Error: ", err.data.ModelState);
        });
    };

    $scope.setusername = function(userName) {
        Identity.setUserName(userName);
        $scope.currentUser = Identity.currentUser;
    };
  }

});

})();