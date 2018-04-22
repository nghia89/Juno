(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', 'apiService']
    function productCategoryListController($scope, apiService) {
        $scope.productCategories = [];
        $scope.getproductCategories = getproductCategories;
        function getproductCategories() {
            apiService.get('/api/productcategory/getall', null, function (result) {
                $scope.productCategories = result.data;
            }, function () {
                console.log('load productcategory failed')
            });
        }
        $scope.getproductCategories();
    }
})(angular.module('junoshop.product_categories'));