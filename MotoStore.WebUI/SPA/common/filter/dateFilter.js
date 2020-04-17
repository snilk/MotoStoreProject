angular.module('bookStoreCommon').filter('dateFilter', function () {
    return function (date) {
        var thenum = date.match(/\d/g);
        var theNumber = thenum.join("");
        var newDate = new Date(+theNumber);
        var formattedDate = newDate.toLocaleDateString("en-US");
        return formattedDate;
    }
});