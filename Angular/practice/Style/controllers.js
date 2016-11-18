myApp = angular.module('myApp', []);

myApp.controller('DeathrayMenuController',
function ($scope) {
    $scope.isDisabled = false;
    $scope.stun = function () {
        // stun the target, then disable menu to allow regeneration
        $scope.isDisabled = 'true';
    }
}
);

myApp.controller('HeaderController',
    function ($scope) {
        $scope.isError = false;
        $scope.isWarn = false;
        $scope.Message = 'Message';

        $scope.showError = function () {
            $scope.isError = true;
            $scope.isWarn = false;
            $scope.Message = 'This is Error';
        }

        $scope.showWarn = function () {
            $scope.isError = false;
            $scope.isWarn = true;
            $scope.Message = 'This is Warn';
        }

    }

    );

myApp.controller('RestaurantTableController',

function ($scope) {
    $scope.directory =
        [{ name: 'The Handsome Heifer', cuisine: 'BBQ' },
    { name: 'GreenGreen Greens', cuisine: ' Salads' },
    { name: ' House of Fine Fish', cuisine: ' Seafood' }];
    $scope.selectRestaurant = function (row) {
        $scope.selectedRow = row;
    };
});