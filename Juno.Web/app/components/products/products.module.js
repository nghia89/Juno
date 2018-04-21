/// <reference path="~/Assets/Admin/libs/angular/angular.js" />
/// <reference path="~/Assets/Admin/libs/angular/angular.js" />

(function () {
    angular.module('junoshop.products', ['junoshop.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('products', {
            url: "/products",
            templateUrl: "/app/components/products/productListView.html",
            controller: "productListController"
            }).state('products_add', {
            url: "/products_add",
            templateUrl: "/app/components/products/productAddView.html",
            controller: "productAddController"
        });
    }
})();



