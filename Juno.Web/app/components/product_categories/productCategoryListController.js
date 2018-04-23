(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService']

    function productCategoryListController($scope, apiService) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getproductCategories = getproductCategories;
        function getproductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize:20
                }
            }
            apiService.get('/Api/ProductCategory/getall', config, function (result) {
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                //$scope.productCategories = result.data;
            }, function () {
                console.log('load productcategory failed')
            });
        }
        $scope.getproductCategories();
    }
})(angular.module('junoshop.product_categories'));