appContextAwareTT.controller('DashBoardCtrl', ['$scope', '$routeParams', 'ngTableParams', '$http', '$interval', '$location', function ($scope, $routeParams, ngTableParams, $http, $interval, $location) {

    var promise = $http.get('/webapi/GlobalApi/GetAllYears', {});
    promise.then(
     function (payload) {
         $scope.years = payload.data;
     });

    var promise = $http.get('/webapi/GlobalApi/GetAllCourses', {});
    promise.then(
     function (payload) {
         $scope.courses = payload.data;
     });

    $scope.getOnGoingTimetableSchedules = function () {

        var promise = $http.get('/webapi/GlobalApi/GetAllOnGoingTimetableSchedules', {});
        promise.then(
        function (payload) {
            $scope.OnGoingTimetableSchedules = payload.data;
        });
    }

    $scope.getOnGoingTimetableSchedules();
    $interval(function () {
        $scope.getOnGoingTimetableSchedules();
    }, 30000);

  
}]);


appContextAwareTT.controller('StudentEditController', ['$scope', '$routeParams', 'ngTableParams', '$http', '$filter', '$location', '$log', '$timeout',
    function ($scope, $routeParams, ngTableParams, $http, $filter, $location, $log,$timeout) {
        var studentId = $routeParams.studentId;
        var action = $routeParams.action;
        if (action == 'create') {
            studentId = 0;
        }
        $scope.createStudent = function (student) {
            $scope.showMessageSave = false;        
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/GlobalApi/saveStudent', { StudentId: studentId, StudentNumber: student.StudentNumber, RegistrationNumber: student.RegistrationNumber });
                promise.then(function (payload) {
                    $scope.student.studentId = payload.data;
                    studentId = payload.data;
                    $scope.showMessageSave = true
                    setTimeout(
                    function () {
                   $scope.$apply(function () {
                       $scope.showMessageSave = false;
                   });
               }, 1500);
                }, function (errorPayload) {
                    $log.error('failed to save student', errorPayload);
                    $scope.showMessageSaveFailed = true;
                });
            }
        }
    }
]);



appContextAwareTT.controller('PreferencesEditController', ['$scope', '$routeParams', 'ngTableParams', '$http', '$filter', '$location', '$log', '$timeout','$interval',
    function ($scope, $routeParams, ngTableParams, $http, $filter, $location, $log, $timeout, $interval) {
        var action = $routeParams.action;
        $scope.years = [];
        $scope.submissionStatus;
        $scope.courses = [];
        $scope.subjects = [];
        $scope.groups = [];
        $scope.studentSelectedYears = [];
        $scope.studentSelectedCourses = [];
        $scope.studentSelectedGroups = [];
        $scope.studentSelectedSubjects = [];


        var yearpromise = $http.get('/webapi/GlobalApi/getAllYears', {});
        yearpromise.then(
            function (payload) {
                $scope.years = payload.data;                
            }
          );

        var promise = $http.get('/webapi/GlobalApi/getAllCourses', {});
        promise.then(
             function (payload) {
                 $scope.courses = payload.data;
             });


        $scope.getGroupsForSelectedCoursesAndYear = function () {
            var subjectPromise = $http.post('/webapi/GlobalApi/GetGroupsForSpecifiedYearsAndCourses', { Years: $scope.studentSelectedYears, Courses: $scope.studentSelectedCourses });
            subjectPromise.then(
                function (payload) {
                    $scope.groups = payload.data;
                }
                );
        };

        $scope.getSubjectsForSelectedCoursesYearAndGroups = function () {
            var subjectPromise = $http.post('/webapi/GlobalApi/GetSubjectsForSpecifiedCoursesYearsAndGroups', { Groups: $scope.studentSelectedGroups, Courses: $scope.studentSelectedCourses, Years:$scope.studentSelectedYears });
            subjectPromise.then(
                function (payload) {
                    $scope.subjects = payload.data;
                }
                );
        };

        $interval(function () {
            $scope.getGroupsForSelectedCoursesAndYear();
            $scope.getSubjectsForSelectedCoursesYearAndGroups();
        }, 3000);

        $scope.savePreference = function () {
            var preferencePromise = $http.post('/webapi/GlobalApi/SaveStudentPreferences', { SelectedYears: $scope.studentSelectedYears, SelectedCourses: $scope.studentSelectedCourses, SelectedSubjects: $scope.studentSelectedSubjects, SelectedGroups: $scope.studentSelectedGroups });
            preferencePromise.then(
                function (payload) {
                    $scope.submissionStatus = payload.data;
                    if ($scope.submissionStatus == true) {
                        $scope.showMessageSave = true
                        setTimeout(
                        function () {
                            $scope.$apply(function () {
                                $scope.showMessageSave = false;
                            });
                        }, 1500);
                    }
                }
                );
        }
        
      
    }
]);

appContextAwareTT.controller('PreferencesViewController', ['$scope', '$routeParams', 'ngTableParams', '$http', '$filter', '$location', '$log', '$timeout', '$interval',
    function ($scope, $routeParams, ngTableParams, $http, $filter, $location, $log, $timeout, $interval) {
        var action = $routeParams.action;

        $scope.studentSelectedYears = [];
        $scope.studentSelectedCourses = [];
        $scope.studentSelectedGroups = [];
        $scope.studentSelectedSubjects = [];

        $scope.GetStudentPreferences = function () {
            var promise = $http.get('/webapi/GlobalApi/GetStudentPreferences', {});
            promise.then(
                 function (payload) {
                     $scope.studentPreferences = payload.data;
                     $scope.studentSelectedYears = payload.data.SelectedYears;
                     $scope.studentSelectedCourses = payload.data.SelectedCourses;
                     $scope.studentSelectedSubjects = payload.data.SelectedSubjects;
                     $scope.studentSelectedGroups = payload.data.SelectedGroups;
                 });
        }

        $scope.GetStudentPreferences();
    }
]);


appContextAwareTT.controller('ActivityController', ['$scope', '$routeParams', 'ngTableParams', '$http', '$filter', '$location', '$log','$interval',
function ($scope, $routeParams, ngTableParams, $http, $filter, $location, $log,$interval) {
        $http.get('/webapi/GlobalApi/GetAllActivities').success(function (data, status) {
            $scope.data = data;
            $scope.tableParams = new ngTableParams({
                page: 1,
                count: 10,
                sorting: { CreatedOn: 'desc' }
            }, {
                total: $scope.data.length,
                getData: function ($defer, params) {
                    var orderData = params.sorting() ?
                                        $filter('orderBy')($scope.data, params.orderBy()) :
                                        $scope.data;
                    $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }

            });
        });

        $scope.getOnGoingTimetableSchedules = function () {

            var promise = $http.get('/webapi/GlobalApi/GetAllOnGoingActivities', {});
            promise.then(
            function (payload) {
                $scope.onGoingSchedules = payload.data;
            });
        }

        $scope.getOnGoingTimetableSchedules();
        $interval(function () {
            $scope.getOnGoingTimetableSchedules();
        }, 30000);
    }
]);

appContextAwareTT.controller('PreferedActivityController', ['$scope', '$routeParams', 'ngTableParams', '$http', '$filter', '$location', '$log',
    function ($scope, $routeParams, ngTableParams, $http, $filter, $location, $log) {
        $http.get('/webapi/GlobalApi/GetPreferedActivities').success(function (data, status) {
            $scope.data = data;
            $scope.tableParams = new ngTableParams({
                page: 1,
                count: 10,
                sorting: { CreatedOn: 'desc' }
            }, {
                total: $scope.data.length,
                getData: function ($defer, params) {
                    var orderData = params.sorting() ?
                                        $filter('orderBy')($scope.data, params.orderBy()) :
                                        $scope.data;
                    $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }

            });
        });
    }
]);


appContextAwareTT.controller('TodaysActivityController', ['$scope', '$routeParams', 'ngTableParams', '$http', '$filter', '$location', '$log',
    function ($scope, $routeParams, ngTableParams, $http, $filter, $location, $log) {
        $http.get('/webapi/GlobalApi/GetAllTodaysActivities').success(function (data, status) {
            $scope.data = data;
            $scope.tableParams = new ngTableParams({
                page: 1,
                count: 10,
                sorting: { CreatedOn: 'desc' }
            }, {
                total: $scope.data.length,
                getData: function ($defer, params) {
                    var orderData = params.sorting() ?
                                        $filter('orderBy')($scope.data, params.orderBy()) :
                                        $scope.data;
                    $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }

            });
        });
    }
]);

appContextAwareTT.controller('TomorrowsActivityController', ['$scope', '$routeParams', 'ngTableParams', '$http', '$filter', '$location', '$log',
    function ($scope, $routeParams, ngTableParams, $http, $filter, $location, $log) {
        $http.get('/webapi/GlobalApi/GetAllTomorrowsActivities').success(function (data, status) {
            $scope.data = data;
            $scope.tableParams = new ngTableParams({
                page: 1,
                count: 10,
                sorting: { CreatedOn: 'desc' }
            }, {
                total: $scope.data.length,
                getData: function ($defer, params) {
                    var orderData = params.sorting() ?
                                        $filter('orderBy')($scope.data, params.orderBy()) :
                                        $scope.data;
                    $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }

            });
        });
    }
]);

appContextAwareTT.controller('TestsActivityController', ['$scope', '$routeParams', 'ngTableParams', '$http', '$filter', '$location', '$log',
    function ($scope, $routeParams, ngTableParams, $http, $filter, $location, $log) {
        $http.get('/webapi/GlobalApi/GetAllTestActivities').success(function (data, status) {
            $scope.data = data;
            $scope.tableParams = new ngTableParams({
                page: 1,
                count: 10,
                sorting: { CreatedOn: 'desc' }
            }, {
                total: $scope.data.length,
                getData: function ($defer, params) {
                    var orderData = params.sorting() ?
                                        $filter('orderBy')($scope.data, params.orderBy()) :
                                        $scope.data;
                    $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }

            });
        });
    }
]);


appContextAwareTT.controller('ExamsActivityController', ['$scope', '$routeParams', 'ngTableParams', '$http', '$filter', '$location', '$log',
    function ($scope, $routeParams, ngTableParams, $http, $filter, $location, $log) {
        $http.get('/webapi/GlobalApi/GetAllExamsActivities').success(function (data, status) {
            $scope.data = data;
            $scope.tableParams = new ngTableParams({
                page: 1,
                count: 10,
                sorting: { CreatedOn: 'desc' }
            }, {
                total: $scope.data.length,
                getData: function ($defer, params) {
                    var orderData = params.sorting() ?
                                        $filter('orderBy')($scope.data, params.orderBy()) :
                                        $scope.data;
                    $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }

            });
        });
    }
]);

appContextAwareTT.controller('ActivityEditController', ['$scope', '$routeParams', 'ngTableParams', '$http', '$filter', '$location', '$log', '$interval',
function ($scope, $routeParams, ngTableParams, $http, $filter, $location, $log, $interval) {
    var activityId = $routeParams.activityId;
    var action = $routeParams.action;
     $scope.showComments = false;

    if (activityId > 0) {
        var promise = $http.get('/webapi/GlobalApi/getActivity?activityId=' + activityId, {});
        promise.then(
          function (payload) {
              var activity = payload.data;
               $scope.activity = {
                  ActivityId: activity.ActivityId,
                  StartTime: activity.StartTime,
                  EndTime: activity.EndTime,
                  Comments: activity.Comments,
                  RoomId: activity.RoomId,
                  GroupId: activity.GroupId,
                  DayId: activity.DayId,
                  SubjectId:activity.SubjectId,
                  LecturerId :activity.LecturerId,
                  Room: activity.Room,                
                  Subject: activity.Subject,
                  Group:activity.Group,               
              };
              $scope.activityComments = activity.Comments;
          }
        );
    }

    $scope.DisplayComments = function () {
        $scope.showComments = true;        
    }

    $scope.DisplayComments();

    $scope.updateActivity = function (activity) {
        $scope.showActivitySave = false;
       
        var promise = $http.post('/webapi/GlobalApi/saveActivity',
        {
            ActivityId: activityId, StartTime: activity.StartTime, EndTime: activity.EndTime, RoomId: activity.RoomId,
            DayId: activity.DayId, Comments: {}, GroupId: activity.GroupId, SubjectId: activity.SubjectId, LecturerId: activity.LecturerId
        });

        promise.then(
            function (payload) {
                $scope.activity.activityId = payload.data;
                if (action == 'create') {
                    $scope.activity.Comments = [];
                }
                activityId = payload.data;
                $scope.showActivitySave = true;
                setTimeout(function () {
                    $scope.$apply(function () {
                        $scope.showActivitySave = false;
                    });
                }, 1500);
            });

    }

    $scope.addComment = function (comment) {
        $scope.showCommentActivitySave = false;

        var promise = $http.post('/webapi/GlobalApi/saveComment', { ActivityId: activityId, Description: comment.Description, CreatedBy: comment.CreatedBy });
        promise.then(
        function (payload) {
            $scope.comment.commentId = payload.data;
            $scope.commentId = payload.data;
            $scope.activityComments.push({ ActivityId: activityId, Description: comment.Description, CreatedBy: comment.CreatedBy });
            comment.Description = "";

            $scope.showCommentActivitySave = true;
            setTimeout(function () {
                $scope.$apply(function () {
                    $scope.showCommentActivitySave = false;
                });
            }, 1500);
        });
    }

    $scope.deleteComment = function (comment) {
        $scope.showCommentActivityDelete = false;
        $scope.showCommentActivityDeleteFailed = false;
        if ($scope.form.$valid) {
            var promise = $http.get('/webapi/GlobalApi/deleteComment/?ActivityId=' + activityId + "&commentId=" + $scope.commentId, {});
            promise.then(
                function (payload) {
                    $scope.showCommentActivityDelete = true;
                    setTimeout(function () {
                        $scope.$apply(function () {
                            $scope.showCommentActivityDelete = false;
                        });
                    }, 1500);
                },
                function (errorPayload) {
                    $log.error('faliled to delete comment with id' + commentId, errorPayload);
                    $scope.showCommentActivityDeleteFailed = true;
                });
        }
    }

    $scope.getActivityComment = function () {
        $scope.showCommentEditingArea = true;
        var promise = $http.get('/webapi/GlobalApi/getComment?ActivityId=' + activityId + "&commentId=" + activityCommentId, {});
        promise.then(
            function (payload) {
                var comment = payload.data;
                $scope.comment = {
                    ActivityId: comment.ActivityId,
                    Description: comment.Description,
                    AgentId: comment.AgentId,
                    AgentFirstName: comment.Agent.FirstName,
                    AgentLastName: comment.Agent.LastName,
                    CreatedOn: comment.CreatedOn
                };
            }
          );
    }


    $http.get('/webapi/GlobalApi/GetAllDays').success(function (data, status) {
        $scope.days = data;
    });

    $http.get('/webapi/GlobalApi/GetAllGroups').success(function (data, status) {
        $scope.groups = data;
    });

    $http.get('/webapi/GlobalApi/GetAllRooms').success(function (data, status) {
        $scope.rooms = data;
    });

    $http.get('/webapi/GlobalApi/GetAllLecturers').success(function (data, status) {
        $scope.lecturers = data;
    });

    $http.get('/webapi/GlobalApi/GetAllSubjects').success(function (data, status) {
        $scope.subjects = data;
    });

}
]);


appContextAwareTT.controller('CommentViewController', ['$scope', '$routeParams', 'ngTableParams', '$http', '$filter', '$location', '$log', '$interval',
function ($scope, $routeParams, ngTableParams, $http, $filter, $location, $log, $interval) {
    var activityId = $routeParams.activityId;
    var commentId = $routeParams.commentId;

    $scope.getActivityComment = function () {     
        var promise = $http.get('/webapi/GlobalApi/getComment?ActivityId=' + commentId + "&commentId=" + activityId, {});
        promise.then(
            function (payload) {
                var comment = payload.data;
                $scope.comment = {
                    ActivityId: comment.ActivityId,
                    Description: comment.Description,
                    AspNetUser: comment.AspNetUser,                   
                    CreatedOn: comment.CreatedOn
                };
            }
          );
    }

    $scope.getActivityComment ();

}
]);












