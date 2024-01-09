using System.Data.SqlClient;
using System.Data;
using WhiteHouseETL.Helpers;
using WhiteHouseETL.Models;

namespace WhiteHouseETL.Tasks;

public static class TaskEmployee
{
    public static (List<Employee>, List<ValidationResult>) Transform(List<WhiteHouseStaff> records, List<ValidationResult> validationResults)
    {
        List<Employee> employees = new List<Employee>();

        foreach (var record in records)
        {
            string firstName = TransformationHelpers.SplitName(record.Name, "FIRST").Trim();
            string middleInitial = TransformationHelpers.SplitName(record.Name, "MIDDLE").Trim();
            string lastName = TransformationHelpers.SplitName(record.Name, "LAST").Trim();
            string gender = record.Gender.Trim();

            ValidationResult validationResult = ValidationHelpers.EmployeeTableValidation(firstName, middleInitial, lastName, gender);

            Employee employee = new Employee() { RowNumber = record.RowNumber, FirstName = firstName, MiddleInitial = middleInitial, LastName = lastName, Gender = gender};

            if (validationResult.Passed)
            {
                employees.Add(employee);
            }
            else
            {
                validationResult.Employee = employee;
                validationResult.Record = record;
                validationResults.Add(validationResult);
            }
        }

        return (employees, validationResults);
    }

    public static void Load(SqlConnection connection, List<Employee> employees)
    {
        var ado = new ADOHelpers();

        using (SqlCommand command = new SqlCommand("InsertEmployees", connection))
        {
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = command.Parameters.AddWithValue("@Employees", ado.CreateEmployeeTable(employees));
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.TypeName = "EmployeeTableType";

            command.ExecuteNonQuery();
        }   
    }
}
