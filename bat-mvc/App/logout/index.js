(function () {
    "use strict"

angular.module("app")
  .component("logoutIndex", {
    template: "Du har loggats UT <br/>",
    // bindings: { students: '<' },
    // controllerAs: "model",
    controller: function (Identity, $state) {
        Identity.Logout();
        $state.go('home')
        // window.location.href = '/Login/SecurityInfo'
    }
});

})();