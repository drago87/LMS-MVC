(function () {
"use strict"

function findClassUnit(arr, id) {
    return _.find(arr, function(a) {
        return a.ClassUnitID == id;
    });
}

angular.module("app")
.component("classUnitIndex", {
    templateUrl: "/App/classunits/index.html",
    bindings: { classunits: '<' },
    controllerAs: "model",
    controller: function (ClassUnit, Common, $rootScope) {
        var vm = this;

        $rootScope.$on("logout", function(){
            vm.classunits= [];
        });

        this.add = function () {
            var newclassUnit = new ClassUnit(vm.ClassUnit);
            Common.AddEntity(newclassUnit, vm.classunits);
        }
        this.remove = function (classUnit) {
            classUnit.$remove(function() {
                _.remove(vm.classunits, function(s){
                    return s.ClassUnitID == classUnit.ClassUnitID;
                });
            });
        }
        this.startedit = function (classUnit) {
            vm.ClassUnit = angular.copy(classUnit);
            vm.editing = true;
        }
        this.update = function () {
            vm.ClassUnit.$update(function(classunit) {
                var classunit = findClassUnit(vm.classunits, classunit.ClassUnitID);
                classunit.ClassName = vm.ClassUnit.ClassName;
                vm.clear();
            });
        }
        this.submit = function() {
            if (vm.editing) {
                vm.update();
            } else {
                vm.add();
            }
        }
        this.clear = function () {
            vm.ClassUnit = new ClassUnit();
            vm.editing = false;
        }
    }
});

})();