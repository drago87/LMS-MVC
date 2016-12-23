(function () {
    "use strict"

angular.module("app")
  .component("studentIndex", {
    templateUrl: "/App/students/index.html",
    bindings: { students: '<' },
    controllerAs: "model",
    controller: function (Student, Common) {
        var vm = this;
        this.add = function () {
            var newStudent = new Student(vm.student);
            Common.AddEntity(newStudent, vm.students);
        }
        this.remove = function (student) {
            _.remove(vm.students, function(s){
                return s.Id == student.Id;
            });
        }
        this.startedit = function (student) {
            vm.student = angular.copy(student);
            vm.editing = true;
        }
        this.update = function () {
            vm.student.$update();
            vm.clear();
        }
        this.clear = function () {
            vm.student = new Student();
            vm.editing = false;
        }
    }
});

})();