(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService']

    function productCategoryListController($scope, apiService, notificationService) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getproductCategories = getproductCategories;
        $scope.KeyWord = '';
        $scope.search = search;
        function search() {
            getproductCategories();
        }
        function getproductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    KeyWord:$scope.KeyWord,
                    page: page,
                    pageSize:20
                }
            }
            apiService.get('/Api/ProductCategory/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('không có bản ghi nào được tìm thấy.');
                } else {
                    notificationService.displaySuccess('Đã tìm thấy' +' '+ result.data.TotalCount+' '+ 'bản ghi')
                }
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