'use strict';

function ShopEntity(id, name, imageId, price) {
    this.id = id;
    this.name = name;
    this.imageId = imageId;
    this.price = price;
}

function UserEntity(username, password) {
    this.UserName = username;
    this.Password = password;
    this.RepeatPassword = '';
    this.Email = '';
    //0男性
    this.Sex = 0;
}

function OrderEntity() {
    this.id;
    this.shopId;
    this.shopName;
    this.address = '';
    this.number = 0;
    this.phone = '';
    this.time;
}





var ShopList = new Array();
//无add方法
ShopList.push(new ShopEntity('3bce4931-6c75-41ab-afe0-2ec108a30860', '梦见 Drifting Dream', '3bce4931-6c75-41ab-afe0-2ec108a30862', 70));
ShopList.push(new ShopEntity('3bce4931-6c75-41ab-afe0-2ec108a30860', '梦见 Drifting Dream', '3bce4931-6c75-41ab-afe0-2ec108a30862', 70));
ShopList.push(new ShopEntity('3bce4931-6c75-41ab-afe0-2ec108a30860', '梦见 Drifting Dream', '3bce4931-6c75-41ab-afe0-2ec108a30862', 70));
ShopList.push(new ShopEntity('3bce4931-6c75-41ab-afe0-2ec108a30860', '梦见 Drifting Dream', '3bce4931-6c75-41ab-afe0-2ec108a30862', 70));
ShopList.push(new ShopEntity('3bce4931-6c75-41ab-afe0-2ec108a30860', '梦见 Drifting Dream', '3bce4931-6c75-41ab-afe0-2ec108a30862', 70));
ShopList.push(new ShopEntity('3bce4931-6c75-41ab-afe0-2ec108a30860', '梦见 Drifting Dream', '3bce4931-6c75-41ab-afe0-2ec108a30862', 70));


var webShopControllers = angular.module('webShopControllers', []);



//CruUserService一定要注入吗？  多了一个,号
webShopControllers.controller('loginCtrl', ['$scope', '$location', '$http'
    , function ($scope,  $location, $http) {
    $scope.user = new UserEntity('', '');
 
    $scope.login = function () {
        console.log($scope.user.UserName);
        $http.post('http://localhost:3000/api/Account/Login', $scope.user).success(function (data) {
            if (data.Code != 0) {
                return $scope.err = data.Code;
            }

            $location.path("/");
            //  CruUserService.cruUser = $scope.user;
            $scope.$parent.isLogin = true;
        });

    };

    $scope.Signout = function () {
        $scope.$parent.isLogin = false;
        //  CruUserService.cruUser = null;
        $location.path("/");
    };

}]
);  


webShopControllers.controller('regCtrl', ['$scope', '$location', '$http', function ($scope, $location,$http) {
    $scope.user = new UserEntity('', '');
    $scope.register = function () {
        console.log($scope.user.username);
        $http.post('http://localhost:3000/api/Account/Register', $scope.user).success(function (data) {
            if (data.Code!=1) {
                return $scope.err = data.Code;
            }
            $location.path("/");
        });
    };
}]
);

webShopControllers.controller('shopDetailCtrl', ['$scope', '$routeParams', function ($scope, $routeParams) {
    $scope.shop = {};
    for (var index in ShopList) {
        //竟然只是下标
        if (ShopList[index].id == $routeParams.id) {
            $scope.shop = ShopList[index];
        }
    }

}]
);

webShopControllers.controller('shopListCtrl', ['$scope', function ($scope) {
    $scope.shopList = ShopList;
}
]);


webShopControllers.controller('userCenterCtrl', ['$scope', function ($scope) {
    $scope.orders = [];
    var order = new OrderEntity();
    order.shopName = "123123";
    order.number = 12;
    $scope.orders.push(order);

    var order = new OrderEntity();
    order.shopName = "123123";
    order.number = 12;
    $scope.orders.push(order);
}
]);


