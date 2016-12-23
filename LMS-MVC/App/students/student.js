angular.module('app').component('student', {
  bindings: { student: '<' },
  template: '<h3>Student</h3>' +
  
            '<div>Förnamn: {{$ctrl.student.FirstName}}</div>' +
            '<div>Efternamn: {{$ctrl.student.LastName}}</div>' +
            '<div>Id: {{$ctrl.person.id}}</div>' +
            '<div>StudentGroup: {{$ctrl.student.studyGroup}}</div>' +
            '<div>Email: {{$ctrl.student.email}}</div>' +
            
            '<button ui-sref="student">Stäng</button>'
});