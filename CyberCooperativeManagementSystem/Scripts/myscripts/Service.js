/// <reference path="../angular.min.js" />
app.service("crudAJService", function ($http) {

    //get All employees
    this.getEmployees = function () {
        return $http.get("../MembersList/GetAllEmployee");
    };

    // get employee By Id 
    this.getEmploye = function (id) {
        var response = $http({
            method: "post",
            url: "../MembersList/GetEmployeeById",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;
    }

    // Update Employee 
    this.updateEmployee = function (employee) {
        var response = $http({
            method: "post",
            url: "../MembersList/UpdateEmployee",
            data: JSON.stringify(employee),
            dataType: "json"
        });
        return response;
    }

    // Add Book
    this.AddEmployee = function (employee) {
        var response = $http({
            method: "post",
            url: "../MembersList/AddEmployee",
            data: JSON.stringify(employee),
            dataType: "json"
        });
        return response;
    }

    //Delete Book
    this.DeleteEmployee = function (id) {
        var response = $http({
            method: "post",
            url: "../MembersList/DeleteEmployee",
            params: {
                bookId: JSON.stringify(id)
            }
        });
        return response;
    }
});