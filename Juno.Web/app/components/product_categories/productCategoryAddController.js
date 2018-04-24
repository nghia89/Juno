/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);
    productCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state']

    function productCategoryAddController(apiService, $scope, notificationService, $state) {
        $scope.productCategory = {
            CreateDate: new Date(),
            Status: true
        }
        $scope.AddProductCategory = AddProductCategory;
        function AddProductCategory() {
            apiService.post('Api/productCategory/create', $scope.productCategory, function (result ) {
                notificationService.displaySuccess(result.data.Name+' ' +'Đã được thêm mới.');
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công');
            });
        }
        function loadparentcategory() {
            apiService.get('Api/productCategory/getallparent', null, function (result) {
                $scope.parentcategories =result.data;
            }, function () {
                console.log('canot get list parent  ')
            });
        }
        loadparentcategory();
    }

})(angular.module('junoshop.product_categories'));