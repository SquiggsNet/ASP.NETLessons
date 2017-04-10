angular.module('starter.services', ['ngResource'])

.factory('AcademicYear', function ($resource) {
    return $resource('http://scottrafaelnscccoursemap.azurewebsites.net/api/AcademicYears/:AcademicYearsId');
})

.factory('Semester', function ($resource) {
    return $resource('http://scottrafaelnscccoursemap.azurewebsites.net/api/Semesters/:SemestersId');
})

.factory('Program', function ($resource) {
    return $resource('http://scottrafaelnscccoursemap.azurewebsites.net/api/Programs/:ProgramsId');
})

.factory('Concentration', function ($resource) {
    return $resource('http://scottrafaelnscccoursemap.azurewebsites.net/api/Concentrations/:ConcentrationsId');
})

.factory('Course', function ($resource) {
    return $resource('http://scottrafaelnscccoursemap.azurewebsites.net/api/Courses/:CoursesId');
});