var fund = angular.module('fund', []);
fund.controller('StartUpController',
function ($scope) {
    $scope.funding = { startingEstimate: 0 };
    computeNeeded = function () {
        $scope.funding.need = $scope.funding.startingEstimate * 10;
    }
    $scope.$watch('funding.startingEstimate', computeNeeded);

    $scope.requestFunding = function () {
        window.alert("submit");
    }
    $scope.reset = function () {
        $scope.funding.startingEstimate = 0;
    };


}
);