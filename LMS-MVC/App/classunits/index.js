(function () {
"use strict"

angular.module("app")
.component("classUnitIndex", {
    templateUrl: "/App/classunits/index.html",
    bindings: { classunits: '<' },
    controllerAs: "model",
    controller: function (ClassUnit, Common) {
        var vm = this;
        this.add = function () {
            var newclassUnit = new classUnit(vm.classUnit);
            Common.AddEntity(newclassUnit, vm.classunits);
        }
        this.remove = function (classUnit) {
            _.remove(vm.classunits, function(s){
                return s.Id == classUnit.Id;
            });
        }
        this.startedit = function (classUnit) {
            vm.classUnit = angular.copy(classUnit);
            vm.editing = true;
        }
        this.update = function () {
            vm.classUnit.$update();
            vm.clear();
        }
        this.clear = function () {
            vm.classUnit = new classUnit();
            vm.editing = false;
        }
    }
});

})();