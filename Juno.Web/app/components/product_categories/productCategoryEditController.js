/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);
    productCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService']

    function productCategoryEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.productCategory = {
            CreateDate: new Date(),
            Status: true
        }

        $scope.UpdateProductCategory = UpdateProductCategory;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function loadProductCategoryDetail() {
            apiService.get('Api/productCategory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.productCategory=result.data;
            }, function (error) {
                notificationService.displayError(Error);
            });
        }
        function UpdateProductCategory() {
            apiService.put('Api/productCategory/update', $scope.productCategory,
                function (result) {
                notificationService.displaySuccess(result.data.Name + ' ' + 'Đã được cập nhật.');
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công');
            });
        }
        function loadparentcategory() {
            apiService.get('Api/productCategory/getallparent', null, function (result) {
                $scope.parentcategories = result.data;
            }, function () {
                console.log('canot get list parent  ')
            });
        }
        loadparentcategory();
        loadProductCategoryDetail();
    }

})(angular.module('junoshop.product_categories'));