/**
 * Created by joker on 2016/6/15.
 */

dnsTool.controller('indexCtrl', function ($scope, $http) {
    $scope.data = {};
    $scope.action = {};
    $scope.data.currentDomainList = '';
    $scope.data.addDomainList = '';
    $scope.data.successMessage = "";
    $scope.data.failMessage = "";

    $scope.action.addDomain = function () {
        const urls = $scope.data.addDomainList.split(/\n/);
        const domains = [];
        angular.forEach(urls, function (url) {
            if (/\S/.test(url)) {
                const domain = extractDomain(url);
                domains.push(domain);
            }
        });
        let url = "/whitelist/post";
        $http.post(url, domains).success(function (rep) {
            if (rep && (rep === true || rep === "true")) {
                $scope.data.successMessage = "domains added successfully."
            } else {
                $scope.data.failMessage = "domains added failed."
            }
        });
    };
    $scope.action.getCurrentDomains = function () {
        const url = "/whitelist/getall";
        $http.get(url).success(function (rep) {
            if (rep && (typeof rep) === "object") {
                $scope.data.currentDomainList = Enumerable.from(rep).aggregate('', function (current, s) {
                    return current + s + "\n";
                });
                $scope.data.successMessage = "get domains successfully."
                console.log($scope.data.currentDomainList);
            }
        });
    };
    (function () {
        $scope.action.getCurrentDomains();
    })();
});