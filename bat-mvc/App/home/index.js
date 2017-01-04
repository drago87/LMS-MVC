(function () {
"use strict"

angular.module("app")
.component("homeIndex", {
    template: "<h1>Hem</h1>",
    // templateUrl: "/App/home/index.html",
    controllerAs: "model",
    controller: function () {
        var vm = this;
    }
});

})();