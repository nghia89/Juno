/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);
    productCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService']

    function productCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.productCategory = {
            CreateDate: new Date(),
            Status: true
        }
        $scope.AddProductCategory = AddProductCategory;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }
        function AddProductCategory() {
            apiService.post('api/productcategory/create', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name+' ' +'Đã được thêm mới.');
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công');
            });
        }
        function loadparentcategory() {
            apiService.get('api/productCategory/getallparent', null, function (result) {
                $scope.parentcategories =result.data;
            }, function () {
                console.log('canot get list parent  ')
            });
        }
        loadparentcategory();
    }

})(angular.module('junoshop.product_categories'));