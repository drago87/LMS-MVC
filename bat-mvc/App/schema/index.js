(function () {
"use strict"

// function findSchema(arr, id) {
//     return _.find(arr, function(a) {
//         return a.SchemaID == id;
//     });
// }

angular.module("app")
.component("schemaIndex", {
    templateUrl: "/App/schema/index.html",
    bindings: { subjects: '<', lessons: '<' },
    controllerAs: "model",
    controller: function (Subject, Lesson, Common, $rootScope) {
        var vm = this;

        vm.getSubjectNameForId = function(id) {
            var ut = "";
            angular.forEach(vm.subjects, function(value, key) {
                if (value.SubjectID == id) {
                    // this.push(key + ': ' + value.SubjectName);
                    // this.log = value.SubjectName
                    // console.log(value.SubjectName);
                    ut = value.subjectname;
                    // return (value.SubjectName);
                }
            });
            return ut;
        }
        // vm.getSubjectNameForId(4);

        // console.log("subjects.", vm.lessons);

        // $rootScope.$on("logout", function(){
        //     vm.lessons = [];
        //     vm.subjects = [];
        // });

        // this.add = function () {
        //     var newSchema = new Schema(vm.Schema);
        //     Common.AddEntity(newSchema, vm.Schemas);
        // }
        // this.remove = function (Schema) {
        //     Schema.$remove(function() {
        //         _.remove(vm.Schemas, function(s){
        //             return s.SchemaID == Schema.SchemaID;
        //         });
        //     });
        // }
        // this.startedit = function (Schema) {
        //     vm.Schema = angular.copy(Schema);
        //     vm.editing = true;
        // }
        // this.update = function () {
        //     vm.Schema.$update(function(Schema) {
        //         var Schema = findSchema(vm.Schemas, Schema.SchemaID);
        //         Schema.ClassName = vm.Schema.ClassName;
        //         vm.clear();
        //     });
        // }
        // this.submit = function() {
        //     if (vm.editing) {
        //         vm.update();
        //     } else {
        //         vm.add();
        //     }
        // }
        // this.clear = function () {
        //     vm.lesson = new Lesson();
        //     vm.editing = false;
        // }
    }
});

})();