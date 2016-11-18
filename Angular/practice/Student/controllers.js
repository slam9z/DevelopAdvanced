

var students = [
    { name: '12312', id: '1' },
    { name: 'wqe', id: '12' }
];

var myApp = angular.module('myApp', []);

myApp.controller('StudentListController',
function ($scope) {
    $scope.students = students;
    $scope.insertTom = function () {
        $scope.students.splice(1, 0, { name: 'tom', id: '12' });
    }



}
);