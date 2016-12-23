(function () {
    "use strict"

angular.module("app")
  .component("logoutIndex", {
    template: "Du har loggats UT <br/>",
    // bindings: { students: '<' },
    // controllerAs: "model",
    controller: function (Identity) {
        Identity.Logout();
        // window.location.href = '/Login/SecurityInfo'
    }
});

})();