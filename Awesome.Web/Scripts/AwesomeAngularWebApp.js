var AwesomeAngularWebApp = angular.module('AwesomeAngularWebApp', ['ngRoute', 'ngCookies']);
AwesomeAngularWebApp.controller('BaseController', BaseController);
AwesomeAngularWebApp.controller('LoginController', LoginController);
AwesomeAngularWebApp.controller('RegisterController', RegisterController);
AwesomeAngularWebApp.controller('ValuesController', ValuesController);
AwesomeAngularWebApp.service('SessionService', SessionService);
AwesomeAngularWebApp.factory('LoginFactory', LoginFactory);
AwesomeAngularWebApp.factory('RegisterFactory', RegisterFactory);
AwesomeAngularWebApp.factory('GetValuesFactory', GetValuesFactory);

var ConfigFunction = function($routeProvider, $locationProvider) {
  $routeProvider
   .when('/login', {
    templateUrl: '/Home/login',
    //templateUrl: '/Views/login.html',
    controller: 'LoginController'
  })
  .when('/register', {
    templateUrl: '/Home/register',
    controller: 'RegisterController'
  })
  .when('/values', {
    templateUrl: '/Home/values',
    controller: 'ValuesController'
  });
    
  // enable html5Mode for pushstate ('#'-less URLs)
  //$locationProvider.html5Mode(true);
  //$locationProvider.hashPrefix('!');
};

ConfigFunction.$inject = ['$routeProvider'];
AwesomeAngularWebApp.config(ConfigFunction);
