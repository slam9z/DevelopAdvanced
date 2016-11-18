var app = angular.module('app', []);
app.directive('ngbkFocus', function () {
    return {

        link: function (scope, element, attrs, contoller) {
            element[0].focus();
        }
    }

});


app.controller('SomeController',
function ($scope) {
    $scope.message = { text: 'no click' };

    $scope.clickUnfocused = function () {
        $scope.message.text = 'unfocused';
    }
    $scope.clickFocused = function () {
        $scope.message.text = 'focused';
    }


}
);