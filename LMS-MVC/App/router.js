(function () {
    "use strict"

    var sampleStudents =
    [ {  Id: 1, 
           FirstName: "Nils", 
           LastName: "Nilsson"
           // Files: { Id: 1, FileName: "History001.docx" }
      },
      {  Id: 2, 
         FirstName: "Lars", 
         LastName: "Larsson"
      } ];

    var sampleClassUnits =
    [ {  Id: 1, 
         Name: "Klass 1A", 
      },
      {  Id: 2, 
         Name: "Klass 1B", 
      } ];

    angular.module("app")
    .config(function($stateProvider) {
        var studentState = {
            name: 'students',
            url: '/students',
            component: 'studentIndex',
            resolve: {
                students: function(Student) {
                    //return sampleStudents;
                    return Student.query();
                }
            }
        }
        var classUnitState = {
            name: 'classunits',
            url: '/classunits',
            component: 'classUnitIndex',
            resolve: {
                classunits: function(ClassUnit) {
                    //return sampleClassUnits;
                    return ClassUnit.query();

                    //var accesstoken = sessionStorage.getItem('accessToken');
                    //var authHeaders = {};
                    //if (accesstoken) {
                    //    authHeaders.Authorization = 'Bearer ' + accesstoken;
                    //}

                    //var promise = $http({
                    //    url: "/api/classunits",
                    //    method: "GET",
                    //    headers: authHeaders
                    //});

                    //promise.then(function (res) {
                    //    console.log("classunits data: ", res.data);
                    //    return res.data;
                    //}, function (err) {
                    //    console.log("Error!!! " + err.status);
                    //});
                }
            }
        }
        var loginState = {
            name: 'login',
            url: '/login',
            component: 'loginIndex',
        }
        var logoutState = {
            name: 'logout',
            url: '/logout',
            component: 'logoutIndex',
        }
        var registerState = {
            name: 'register',
            url: '/register',
            component: 'registerIndex',
        }

    $stateProvider.state(loginState);
    $stateProvider.state(logoutState);
    $stateProvider.state(registerState);

    $stateProvider.state(studentState);
    $stateProvider.state(classUnitState);
    }); 

})();