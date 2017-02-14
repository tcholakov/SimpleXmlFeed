(function () {
    'use strict';

    angular.module('simpleXmlFeed.services')
        .factory('signalrService', [signalrService]);

    function signalrService() {

        function startBroadcastMatchesWithOddsForToday(intervalInSeconds, callback) {
            $.connection.hub.logging = true;
            var matches = $.connection.matchesHub;

            matches.client.getMatches = callback

            $.connection.hub.start().done(function () {
                matches.server.matchesWitOddsForToday();
                setInterval(matches.server.matchesWitOddsForToday, intervalInSeconds * 1000);
            });
        }

        return {
            startBroadcastMatchesWithOddsForToday: startBroadcastMatchesWithOddsForToday
        }
    }
}())