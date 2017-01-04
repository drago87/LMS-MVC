(function () {
    "use strict"

    angular.module('app')
      .component('menuBar', {
        templateUrl: '/App/navbar/index.html',
        controllerAs: 'model',
        controller: function(Identity) {
            this.currentUser = Identity.currentUser;
            this.identity = Identity;
            this.identity.getData();
            // console.log('Identitet:', this.identity.userName);

            this.menus = [
                {
                    name:  "Studenter",
                    state: "students",
                    // guest: true
                },
                {
                    name:  "Klasser",
                    state: "classunits",
                },
                {
                    name:  "Schema",
                    state: "schema",
                },
            ];
        }
    });

})();