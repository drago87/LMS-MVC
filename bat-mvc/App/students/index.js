(function () {
"use strict"

function findStudent(arr, id) {
    return _.find(arr, function(a) {
        return a.StudentID == id;
    });
}

angular.module("app")
.component("studentIndex", {
    templateUrl: "/App/Students/index.html",
    bindings: { students: '<' },
    controllerAs: "model",
    controller: function (Student, Common, $rootScope) {
        var vm = this;

        $rootScope.$on("logout", function(){
            vm.Students= [];
        });

        this.add = function () {
            var newStudent = new Student(vm.Student);
            Common.AddEntity(newStudent, vm.Students);
        }
        this.remove = function (Student) {
            Student.$remove(function() {
                _.remove(vm.Students, function(s){
                    return s.StudentID == Student.StudentID;
                });
            });
        }
        this.startedit = function (Student) {
            vm.Student = angular.copy(Student);
            vm.editing = true;
        }
        this.update = function () {
            vm.Student.$update(function(Student) {
                var Student = findStudent(vm.Students, Student.StudentID);
                Student.ClassName = vm.Student.ClassName;
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
            vm.Student = new Student();
            vm.editing = false;
        }
    }
});

})();