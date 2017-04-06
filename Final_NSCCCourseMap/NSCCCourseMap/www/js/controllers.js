angular.module('starter.controllers', ['starter.services'])

.controller('AcademicYearsCtrl', function($scope, AcademicYear){
    $scope.academicYears = AcademicYear.query();
})

.controller('AcademicYearCtrl', function($scope, $stateParams ,AcademicYear){
    $scope.academicYear = AcademicYear.get({AcademicYearId: $stateParams.AcademicYearId});
})

.controller('ProgramsCtrl', function($scope, Program){
    $scope.programs = Program.query();
})

.controller('ProgramCtrl', function($scope, $stateParams ,Program){
    $scope.program = Program.get({ProgramId: $stateParams.ProgramId});
})

.controller('AccountCtrl', function($scope) {
  $scope.settings = {
    enableFriends: true
  };
});
