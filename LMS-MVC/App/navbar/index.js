(function () {
    "use strict"

    angular.module('app').component('menuBar', {
        bindings: {
            menus: '<'
        },
        templateUrl: '/App/navbar/index.html',
        controllerAs: 'model',
        controller: function(Identity) {
            this.currentUser = Identity.currentUser;
            this.identity = Identity;
            Identity.getData();

            this.menus = [
                // {
                //     name: "Hem",
                //     state: "home"
                // },
                {
                    name:  "Studenter",
                    state: "students"
                },
                {
                    name:  "Klasser",
                    state: "classunits"
                }
            ];
        }
    });

})();