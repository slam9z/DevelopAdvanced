var myApp = angular.module('hello',[]);
myApp.controller('HelloController',
    function ($scope) {
        $scope.greeting = { text: 'Hello' };
    });


//function HelloController($scope) {
//    $scope.greeting = {text: 'Hello' };
//}