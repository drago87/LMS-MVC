(function () {
"use strict"

angular.module("app")
.component("subjectIndex", {
    templateUrl: "/App/subjects/index.html",
    bindings: { subjects: '<' },
    controllerAs: "model",
    controller: function (Subject, Common, $rootScope) {
        var vm = this;

        // $rootScope.$on("logout", function(){
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