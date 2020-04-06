angular.module("motoStoreAdmin").directive("uploadImg", function() {
    return {
        restrict: "AE",
        scope: {
            uploadedFile: "=ngModel",
            onImgDelete: "&",
            name: "@"
        },
        templateUrl: "/SPA/admin/directives/uploadImg.html",
        link: function(scope, element) {
            var $inputFile = element.find("input[type=file]");
            var inputChangeHandler = ($input) => {
                var readerSingleImage = new FileReader();
                var uploadedImg = $input[0].files[0];
                var uploadedImages = $input[0].files;
                var uploadedImagesArray = $input[0].files;
                scope.uploadedFile = {};
                scope.uploadedFile.srcList = new Array();

                readerSingleImage.onload = function(e) {
                    scope.isRemoveButtonVisible = true;
                    scope.uploadedFile.name = uploadedImg.name;
                    scope.uploadedFile.src = e.target.result;
                    scope.uploadedFile.file = uploadedImg;
                    scope.uploadedFile.files = uploadedImagesArray;
                    scope.$apply();
                };
                var functionReadMultipleFiles = function(e) {
                    scope.uploadedFile.srcList.push(e.target.result);
                    scope.$apply();
                };

                angular.forEach(uploadedImages,
                    function(item) {
                        var readerMultiImages = new FileReader();
                        readerMultiImages.onload = functionReadMultipleFiles;
                        readerMultiImages.readAsDataURL(item);
                    });

                readerSingleImage.readAsDataURL(uploadedImg);

                //uploadedImages.forEach(function(item, index, array) {
                //    readerMultiImages.readAsDataURL(item);
                //});
            };

            scope.selectFile = () => {
                $inputFile.click();
            };

            scope.removeFile = () => {
                angular.forEach(element.find("input"),
                    function(input) {
                        input.value = null;
                    });
                scope.isRemoveButtonVisible = false;
                scope.uploadedFile = {};
                scope.onImgDelete();
            };

            scope.getUploadImgLabel = () => {
                return scope.name;
                //return scope.uploadedFile.name || "Upload An Image";
            };

            scope.isRemoveButtonVisible = scope.uploadedFile.src;

            $inputFile.bind("change", inputChangeHandler.bind(this, $inputFile));
        }
    };
})

    