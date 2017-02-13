(function () {
    'use strict';

    angular.module('simpleXmlFeed.controllers')
        .controller('matchesController', ['$scope', 'signalrService', matchesController]);

    function matchesController($scope, signalrService) {

        function populateScope(matchesArray) {
            $scope.matches = matchesArray;
            $scope.numberOfMatches = matchesArray.length;
            $scope.updatedOn = new Date();
            $scope.$digest();
        }

        signalrService.startBroadcastMatchesWithOddsForToday(60, populateScope);
    }

}())