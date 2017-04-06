angular.module('starter.services', ['ngResource'])

.factory('AcademicYear', function ($resource) {
    return $resource('http://scottrafaelnscccoursemap.azurewebsites.net/api/AcademicYears/:AcademicYearsId');
})

.factory('Program', function ($resource) {
    return $resource('http://scottrafaelnscccoursemap.azurewebsites.net/api/Programs/:ProgramsId');
});