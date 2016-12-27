(function () {
    "use strict";

angular.module("app")
.component("studentIndex", {
    templateUrl: "/App/Students/index.html",
    bindings: { Students: '<' },
    controllerAs: "model",
    controller: function (Student, Common) {
        var vm = this;

        this.add = function () {
            var newStudent = new Student(vm.Student);
            Common.AddEntity(newStudent, vm.Students);
        };
        this.remove = function (Student) {
            Student.$remove(function () {
                _.remove(vm.Students, function (s) {
                    return s.StudentID === Student.StudentID;
                });
            });
        };
        this.startedit = function (Student) {
            vm.Student = angular.copy(Student);
            vm.editing = true;
        };
        this.update = function () {
            vm.Student.$update();
            vm.clear();
        };
        this.clear = function () {
            vm.Student = new Student();
            vm.editing = false;
        };
    }
});

})();