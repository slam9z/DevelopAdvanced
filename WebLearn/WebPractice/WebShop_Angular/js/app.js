'use strict';

var webShop = angular.module('webShop', ['ngRoute', 'webShopControllers']);


webShop.service('CruUserService', function () {
    this.cruUser = new UserEntity();
    this.isLogin = true;
});

webShop.controller('rootCtrl', ['CruUserService', function ($scope, CruUserService) {
    $scope.isLogin = false;
    // $scope.$apply();
}]);



webShop.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/', {  //必须
            templateUrl: 'partials/shoplist.html',
            controller: 'shopListCtrl'
        }).
         when('/shop/:id', {
             templateUrl: 'partials/shopdetail.html',
             controller: 'shopDetailCtrl'
         }).
        when('/login', {
            templateUrl: 'partials/login.html',
            controller: 'loginCtrl'
        }).
        when('/signout', {
            templateUrl: 'partials/signout.html',
            controller: 'loginCtrl'
        }).
         when('/reg', {
             templateUrl: 'partials/reg.html',
             controller: 'regCtrl'
         }).
         when('/usercenter', {
             templateUrl: 'partials/usercenter.html',
             controller: 'userCenterCtrl'
         }).
      otherwise({
          redirectTo: '/'
      });
  }]);