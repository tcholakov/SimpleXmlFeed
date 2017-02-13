(function () {
    'use strict';
    angular.module('simpleXmlFeed.services', []);
    angular.module('simpleXmlFeed.controllers', ['simpleXmlFeed.services']);

    angular.module('simpleXmlFeed', ['ngRoute', 'simpleXmlFeed.controllers']).
        config(['$routeProvider', config])
        .constant('baseServiceUrl', 'http://localhost:62342');

    function config($routeProvider) {
        var PARTIALS_PREFIX = 'views/partials/';
        var CONTROLLER_AS_VIEW_MODEL = 'vm';


        $routeProvider
            .when('/', {
                templateUrl: PARTIALS_PREFIX + 'matches/matches.html',
                controller: 'matchesController',
                controllerAs: CONTROLLER_AS_VIEW_MODEL
            })
            .otherwise({ redirectTo: '/' });
    }

}());