(function () {
    "use strict";

angular.module("app")
.component("classUnitIndex", {
    templateUrl: "/App/classunits/index.html",
    bindings: { classunits: '<' },
    controllerAs: "model",
    controller: function (ClassUnit, Common) {
        var vm = this;

        this.add = function () {
            var newClassUnit = new ClassUnit(vm.ClassUnit);
            Common.AddEntity(newClassUnit, vm.classunits);
        };
        this.remove = function (ClassUnit) {
            ClassUnit.$remove(function () {
                _.remove(vm.classunits, function (s) {
                    return s.ClassUnitID === ClassUnit.ClassUnitID;
                });
            });
        };
        this.startedit = function (ClassUnit) {
            vm.ClassUnit = angular.copy(ClassUnit);
            vm.editing = true;
        };
        this.update = function () {
            vm.ClassUnit.$update();
            vm.clear();
        };
        this.clear = function () {
            vm.ClassUnit = new ClassUnit();
            vm.editing = false;
        };
    }
});

})();