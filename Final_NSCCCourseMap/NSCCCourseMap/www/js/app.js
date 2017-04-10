// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.services' is found in services.js
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'starter.controllers', 'starter.services'])

.run(function($ionicPlatform) {
  $ionicPlatform.ready(function() {
    // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
    // for form inputs)
    if (window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
      cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
      cordova.plugins.Keyboard.disableScroll(true);

    }
    if (window.StatusBar) {
      // org.apache.cordova.statusbar required
      StatusBar.styleDefault();
    }
  });
})

.config(function($stateProvider, $urlRouterProvider) {

  // Ionic uses AngularUI Router which uses the concept of states
  // Learn more here: https://github.com/angular-ui/ui-router
  // Set up the various states which the app can be in.
  // Each state's controller can be found in controllers.js
  $stateProvider

  // setup an abstract state for the tabs directive
    .state('tab', {
    url: '/tab',
    abstract: true,
    templateUrl: 'templates/tabs.html'
  })

  // Each tab has its own nav history stack:
  .state('tab.AcademicYears', {
      url: '/AcademicYears',
      views: {
        'tab-AcademicYears': {
          templateUrl: 'templates/AcademicYears.html',
          controller: 'AcademicYearsCtrl'
        }
      }
    })
    .state('tab.AcademicYear', {
      url: '/AcademicYears/:AcademicYearsId',
      views: {
        'tab-AcademicYears': {
          templateUrl: 'templates/AcademicYear.html',
          controller: 'AcademicYearCtrl'
        }
      }
    })

    .state('tab.Semester', {
      url: '/Semesters/:SemestersId',
      views: {
        'tab-AcademicYears': {
          templateUrl: 'templates/Semester.html',
          controller: 'SemesterCtrl'
        }
      }
    })
    .state('tab.AcaCourse', {
      url: '/ACourses/:CoursesId',
      views: {
        'tab-AcademicYears': {
          templateUrl: 'templates/Course.html',
          controller: 'CourseCtrl'
        }
      }
    })

  .state('tab.Programs', {
      url: '/Programs',
      views: {
        'tab-Programs': {
          templateUrl: 'templates/Programs.html',
          controller: 'ProgramsCtrl'
        }
      }
    })
    .state('tab.Program', {
      url: '/Programs/:ProgramsId',
      views: {
        'tab-Programs': {
          templateUrl: 'templates/Program.html',
          controller: 'ProgramCtrl'
        }
      }
    })

.state('tab.Concentration', {
      url: '/Concentrations/:ConcentrationsId',
      views: {
        'tab-Programs': {
          templateUrl: 'templates/Concentration.html',
          controller: 'ConcentrationCtrl'
        }
      }
    })
    .state('tab.ProgCourse', {
      url: '/PCourses/:CoursesId',
      views: {
        'tab-Programs': {
          templateUrl: 'templates/Course.html',
          controller: 'CourseCtrl'
        }
      }
    })

    .state('tab.Courses', {
      url: '/Courses',
      views: {
        'tab-Courses': {
          templateUrl: 'templates/Courses.html',
          controller: 'CoursesCtrl'
        }
      }
    })
    .state('tab.CoursesCourse', {
      url: '/Courses/:CoursesId',
      views: {
        'tab-Courses': {
          templateUrl: 'templates/Course.html',
          controller: 'CourseCtrl'
        }
      }
    })

  // if none of the above states are matched, use this as the fallback
  $urlRouterProvider.otherwise('/tab/AcademicYears');

});
