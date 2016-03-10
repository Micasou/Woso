(function(app){
    var ListController = function ($scope) {
        $scope.message = "Hello";
    };
    app.controller("ListController", ListController);
}(angular.module("Explorer")));