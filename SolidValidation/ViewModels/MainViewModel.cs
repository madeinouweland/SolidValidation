using System;
using System.Collections.ObjectModel;
using System.Linq;
using SolidValidation.Models;
using SolidValidation.Validation;

namespace SolidValidation.ViewModels {
    public class MainViewModel : ObservableObject {
        private Employee employee;
        public ObservableCollection<Function> Functions { get; set; }
        public UIEmployee UIEmployee { get; private set; }
        public string Summary { get; private set; }

        public MainViewModel() {
            Functions = new ObservableCollection<Function>();
            Functions.Add(new Function { FunctionName = "TPS reporter" });
            Functions.Add(new Function { FunctionName = "Manager" });

            employee = new Employee {
                BirthDate = DateTime.Now,
                Username = "notloek",
                Email = "loek@toverstudio.nl",
                Name = "Loek",
                Salary = 4324M,
                Function = Functions.First(),
            };

            CreateValidation(employee);
            UpdateSummary();
        }

        public void SaveEmployee() {
            UIEmployee.Validate(() => {
                if (UIEmployee.HasErrors) {
                    return;
                }

                employee.Username = UIEmployee.Username.Value;
                employee.Name = UIEmployee.Name.Value;
                employee.BirthDate = UIEmployee.BirthDate.Value;
                employee.Email = UIEmployee.Email.Value;
                employee.Salary = UIEmployee.Salary.Value;
              //  employee.Function = UIEmployee.Function.Value; TODO: allow booleans and Lists

                UpdateSummary();
            });
        }

        private void UpdateSummary() {
            Summary = "Username: " + employee.Username;
            Summary += "\nName: " + employee.Name;
            Summary += "\nBirthdate: " + employee.BirthDate;
            Summary += "\nEmail: " + employee.Email;
            Summary += "\nSalary: " + employee.Salary;
            OnPropertyChanged(() => Summary);
        }

        private void CreateValidation(Employee employee) {
            UIEmployee = new UIEmployee();
            UIEmployee.Username = new EditProp<string>() { Caption = "Username" };
            UIEmployee.Name = new EditProp<string>() { Caption = "Name" };
            UIEmployee.BirthDate = new EditProp<DateTime?>() { Caption = "Birthdate", ValidatorConverter=new DateConverter() };
            UIEmployee.Email = new EditProp<string>() { Caption = "Email" };
            UIEmployee.Salary = new EditProp<decimal>() { Caption = "Salary", ValidatorConverter=new CurrencyConverter() };
        //    UIEmployee.Function = new EditProp<Function>() { Caption = "Function" };

            if (employee != null) {
                UIEmployee.Username.Value = employee.Username;
                UIEmployee.Name.Value = employee.Name;
                UIEmployee.BirthDate.Value = employee.BirthDate;
                UIEmployee.Email.Value = employee.Email;
                UIEmployee.Salary.Value = employee.Salary;
                //   UIEmployee.Function.Value = employee.Function; TODO: allow booleans and Lists
            }

            UIEmployee.Username.AddValidator(new RequiredValidator { ValidationFailedMessage = "Enter a username" });
            UIEmployee.Username.AddValidator(new UsernameUniqueValidator(employee) { ValidationFailedMessage = "Username already exists. Please type another one" });
            UIEmployee.Email.AddValidator(new EmailAddressValidator { ValidationFailedMessage = "Enter a valid email address" });
            UIEmployee.BirthDate.AddValidator(new BirthdayValidator { ValidationFailedMessage = "Are you really from the future?" });
        }
    }
}
