var appContextAwareTT = angular.module("appContextAwareTT", ["ngRoute", "ngTable", 'ngAnimate', "kendo.directives", "ngResource", "checklist-model", "ui.bootstrap"]);

appContextAwareTT.config(['$routeProvider', '$locationProvider',
  function ($routeProvider, $locationProvider) {
      
      $routeProvider
         .when('/', {
             templateUrl: '/app/partials/Dashboard.html',
        })
        .when('/category/:action/:categoryId', {
            templateUrl:function(params){return '/app/partials/category/edit.tpl.html'}
        })
          .when('/comment/view/:activityId/:commentId', {
              templateUrl: function (params) { return '/app/partials/activity/view-comment.html' }
          })
        .when('/activity/list/', {
            templateUrl: function (params) { return '/app/partials/activity/activity.html' },
        })
          .when('/activity/:action/:activityId', {
              templateUrl: function (params) { return '/app/partials/activity/activity-edit.html' }
           })
        .when('/student/preferences/view/', {
              templateUrl: function (params) { return '/app/partials/student/SelectedPreferences.html' },
        })
         .when('/student/prefered/timetable/view/', {
             templateUrl: function (params) { return '/app/partials/student/prefered-timetable.html' },
           })
         .when('/student/preferences/', {
              templateUrl: function (params) { return '/app/partials/student/preferences.html' },
         })
         .when('/student/:action/:studentId', {
              templateUrl: function (params) { return '/app/partials/student/create.html' },
         })
           .when('/message/:action/:messageId', {
              templateUrl: function (params) { return '/app/partials/message/edit.tpl.html' }
          })
         .when('/activity/timetable/', {
              templateUrl: function (params) { return '/app/partials/activity/todaystimetable.html' },
         })
         .when('/activity/timetable/ongoing/', {
             templateUrl: function (params) { return '/app/partials/OnGoingLecturers.html' },
         })

         .otherwise({ redirectTo: '/nf.html' })
  }]);



appContextAwareTT.run(['$route', angular.noop]);
