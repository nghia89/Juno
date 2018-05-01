/// <reference path="/Assets/Admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productAddController', productAddController);
    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService']

    function productAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.product = {
            CreateDate: new Date(),
            Status: true
        }
        $scope.AddProduct = AddProduct;
        $scope.GetSeoTitle = GetSeoTitle;

        $scope.ckeditorOptions={
            languague: 'vi',
            height: '200px'
        }
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }
        function AddProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('api/product/create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' ' + 'Đã được thêm mới.');
                $state.go('product');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công');
            });
        }
        function loadProductCategory() {
            apiService.get('api/productCategory/getallparent', null, function (result) {
                $scope.productCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.DefaultImage = fileUrl;
                })
            }
            finder.popup();
        }

        $scope.moreImages = [];
        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })

            }
            finder.popup();
        }
        loadProductCategory();
    }

})(angular.module('junoshop.products'));