app.controller("mvcCRUDCtrl", function ($scope, crudAJService) {
    $scope.divBook = false;
    GetAllEmployee();
    //To Get all book records  
    function GetAllEmployee() {
        debugger;
        var getEmployeeData = crudAJService.getEmployees();
        getEmployeeData.then(function (employee) {
            $scope.employees = employee.data;
        }, function () {
            alert('Error in getting employee records');
        });
    }

    $scope.editEmployee = function (employee) {
        var getEmployeeData = crudAJService.getEmploye(employee.id);
            getEmployeeData.then(function (_employee) {
            $scope.employee = _employee.data;
            $scope.employeeId = employee.id;
            $scope.employeeFirstName = employee.FirstName;
            $scope.employeeLastName = employee.LastName;
            $scope.employeeGender = employee.Gender;
            $scope.employeePostalAddress = employee.PostalAddress;
            $scope.employeeContactAddress = employee.ContactAddress;
            $scope.employeeOccupation = employee.Occupation;
            $scope.employeeNextOfKin = employee.NextOfKin;
            $scope.employeeNextOfKinRelationship = employee.NextOfKinRelationship;
            $scope.employeeNextOfKinTelephoneNumber = employee.NextOfKinTelephoneNumber;
            $scope.employeeMonthlySavings = employee.MonthlySavings;
            $scope.employeeNumberOfSharesAppliedFor = employee.NumberOfSharesAppliedFor;
            $scope.employeeValuesOfShares = employee.ValuesOfShares;
            $scope.employeeIsActive = employee.IsActive;
            $scope.employeeRegistrationNumber = employee.RegistrationNumber;
            $scope.employeePhoneNumber = employee.PhoneNumber;
            $scope.employeeDepartment = employee.Department;
            $scope.employeeAccountNumber = employee.AccountNumber;
            $scope.employeePhoto = employee.Photo;
            $scope.employeeMonth= employee.Month;
            $scope.Action = "Update";
            $scope.divEmployee = true;
        }, function () {
            alert('Error in getting book records');
        });
    }
   

    $scope.AddUpdateEmployee = function () {
        var Employee = {
            FirstName:  $scope.employeeFirstName,
            LastName: $scope.employeeLastName,
            Gender:  $scope.employeeGender,
            PostalAddress: $scope.employeePostalAddress,
            ContactAddress:$scope.employeeContactAddress,
            Occupation:$scope.employeeOccupation,
            NextOfKin:$scope.employeeNextOfKin,
            NextOfKinRelationship:$scope.employeeNextOfKinRelationship,
            NextOfKinTelephoneNumber:$scope.employeeNextOfKinTelephoneNumber,
            MonthlySavings:$scope.employeeMonthlySavings,
            NumberOfSharesAppliedFor:$scope.employeeNumberOfSharesAppliedFor,
            IsActive: $scope.employeeIsActive,
            RegistrationNumber: $scope.employeeRegistrationNumber,
            Department: $scope.employeeDepartment,
            AccountNumber:$scope.employeeAccountNumber,
            PostalAddress:$scope.employeePostalAddress,
            Photo: $scope.employeePhoto,
            Month: $scope.employeeMonth
           
        };

        var getEmployeeAction = $scope.Action;

        if (getEmployeeAction == "Update") {
            Employee.id= $scope.employeeId;
            var getEmployeeData = crudAJService.updateBookupdateEmployee(employee);
                getEmployeeData.then(function (msg) {
                GetAllEmployee();
                alert(msg.data);
                $scope.divEmployee = false;
            }, function () {
                alert('Error in updating Employee record');
            });
        } else {
            var getEmployeeData = crudAJService.AddEmployee(employee);
            getEmployeeData.then(function (msg) {
                GetAllEmployee();
                alert(msg.data);
                $scope.divEmployee = false;
            }, function () {
                alert('Error in adding employee record');
            });
        }
    }

    $scope.AddEmployeeDiv = function () {
        ClearFields();
        $scope.Action = "Add";
        $scope.divEmployee = true;
    }

    $scope.deleteEmployee = function (employee) {
        var getBookData = crudAJService.DeleteEmployee(employee.Id);
        getEmployeeData.then(function (msg) {
            alert(msg.data);
            GetAllEmployee();
        }, function () {
            alert('Error in deleting Employee record');
        });
    }

    function ClearFields() {
        
        $scope.employeeFirstName="";
        $scope.employeeLastName = "";
        $scope.employeeGender = "";
        $scope.employeePostalAddress = "";
        $scope.employeeContactAddress = "";
        $scope.employeeOccupation = "";
        $scope.employeeNextOfKin = "";
        $scope.employeeNextOfKinRelationship = "";
        $scope.employeeNextOfKinTelephoneNumber = "";
        $scope.employeeMonthlySavings = "";
        $scope.employeeNumberOfSharesAppliedFor = "";
        $scope.employeeIsActive = "";
        $scope.employeeRegistrationNumber = "";
        $scope.employeeDepartment = "";
        $scope.employeeAccountNumber = "";
        $scope.employeePostalAddress = "";
        $scope.employeePhoto = "";
        $scope.employeeMonth = "";
           
    }
    $scope.Cancel = function () {
        $scope.divEmployee = false;
    };
});