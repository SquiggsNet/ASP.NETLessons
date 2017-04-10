angular.module('starter.controllers', ['starter.services'])

.controller('AcademicYearsCtrl', function($scope, AcademicYear){
    $scope.academicYears = AcademicYear.query();
})

.controller('AcademicYearCtrl', function($scope, $stateParams ,AcademicYear){
    $scope.academicYear = AcademicYear.get({AcademicYearsId: $stateParams.AcademicYearsId});
})

.controller('SemesterCtrl', function($scope, $stateParams ,Semester){
    $scope.semester = Semester.get({SemestersId: $stateParams.SemestersId});
})

.controller('CourseCtrl', function($scope, $stateParams ,Course){
    $scope.course = Course.get({CoursesId: $stateParams.CoursesId});
})

.controller('ProgramsCtrl', function($scope, Program){
    $scope.programs = Program.query();
})

.controller('ProgramCtrl', function($scope, $stateParams ,Program){
    $scope.program = Program.get({ProgramsId: $stateParams.ProgramsId});
})

.controller('ConcentrationCtrl', function($scope, $stateParams ,Concentration){
    $scope.concentration = Concentration.get({ConcentrationsId: $stateParams.ConcentrationsId});
});