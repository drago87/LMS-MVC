var serverurl = "http://localhost:40000";

var getActions = function (headers) {
    return {
        'get':    { method: 'GET', headers: headers },
        'save':   { method: 'POST', headers: headers },
        'create': { method: 'POST', headers: headers },
        'query':  { method: 'GET', isArray: true, headers: headers },
        'remove': { method: 'DELETE', headers: headers },
        'delete': { method: 'DELETE', headers: headers },
        'update': { method: 'PUT', headers: headers }
    };
}

angular.module("app.data", ["ngResource"])
    .config(function ($httpProvider) {
         $httpProvider.defaults.useXDomain = true;
    })

    .factory("ClassUnit", function ($resource, $http) {
        var headers = { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') };
        var actions = getActions(headers);

        return $resource(serverurl + '/api/classunits/:ClassUnitID', { _id: '@ClassUnitID' }, actions);
    })

    .factory("Student", function ($resource) {
        var headers = { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') };
        var actions = getActions(headers);

        return $resource(serverurl + '/api/students/:id', { _id: '@id' }, actions);
        //return $resource(
        //    serverurl + "/api/students/:Id",
        //    { Id: "@Id" },
        //    { update: { method: "PUT" },
        //    } //);
    })
    .factory('Identity', function($state) {
        var svc = {
            userName: null,
            currentUser: "Not Logged In",
            authHeaders: {},
            getData: function() {
                this.setUserName(sessionStorage.getItem('userName'));
                var accesstoken = sessionStorage.getItem('accessToken');
                if (accesstoken) {
                    this.authHeaders.Authorization = 'Bearer ' + accesstoken;
                }
            },
            setData: function(data){
                sessionStorage.setItem('userName',     data.userName);
                sessionStorage.setItem('accessToken',  data.access_token);
                sessionStorage.setItem('refreshToken', data.refresh_token);
                this.setUserName(data.userName);
                $state.reload();
                $state.go('students');
            },
            setUserName: function(n) {
                this.userName = n;
                if (n) {
                    this.currentUser = n
                } else {
                    this.currentUser = "Not logged in";
                }
            },
            Logout: function() {
                sessionStorage.removeItem('userName');
                sessionStorage.removeItem('accessToken');
                sessionStorage.removeItem('refreshToken');
                this.setUserName(null);
                // $state.reload();
                // $state.go('login');
                // $state.go($state.current, {}, {reload: true});
            }
        }
        return svc;
    })
    .service('loginService', function ($http) {
        this.register = function (userInfo, Identity) {
           $http.defaults.useXDomain = true;
           console.log("userinfo:", userInfo);
            var resp = $http({
                url: serverurl + "/api/Account/Register",
                method: "POST",
                data: userInfo,
                // headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                // headers: {'Content-Type': 'application/json'}
            });
            return resp;
        };

        this.login = function (userlogin) {
           $http.defaults.useXDomain = true;
            var resp = $http({
                url: serverurl + "/TOKEN",
                method: "POST",
                data: $.param({
                    grant_type: 'password',
                    username: userlogin.username, password: userlogin.password
                }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                // headers: {'Content-Type': 'application/json'}
            });
            return resp;
        };
    })
    .service('empservice', function ($http) {
        this.get = function () {

            var accesstoken = sessionStorage.getItem('accessToken');

            var authHeaders = {};
            if (accesstoken) {
                authHeaders.Authorization = 'Bearer ' + accesstoken;
            }

            var response = $http({
                url: serverurl + "/api/EmployeeAPI",
                method: "GET",
                headers: authHeaders
            });
            return response;
        };
    })

// create: { method: "POST" },
// create: { method: "POST", isArray: true },
// save: { method:'POST', headers: [{'Content-Type': 'application/json'}] }

// $scope.wordImageTest.response = new WordImage();
// $scope.wordImageTest.response.$save();
// WordImage.save(data);