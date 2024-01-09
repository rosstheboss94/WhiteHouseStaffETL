using System.Data.SqlClient;
using System.Data;
using WhiteHouseETL.Helpers;
using WhiteHouseETL.Models;

namespace WhiteHouseETL.Tasks;

public class TaskSalary
{
    public static (List<Salary>, List<ValidationResult>) Transform(List<WhiteHouseStaff> records, List<ValidationResult> validationResults)
    {
        List<Salary> salaries = new List<Salary>();

        foreach (var record in records)
        {    
            ValidationResult validationResult = ValidationHelpers.SalaryTableValidation(record.Salary, record.Year);
            Salary salary = new Salary() { RowNumber = record.RowNumber, EmployeeSalary = record.Salary, Year = record.Year };
            if (validationResult.Passed)
            {
                
                salaries.Add(salary);
            }
            else
            {
                validationResult.Salary = salary;
                validationResult.Record = record;
                validationResults.Add(validationResult);
            }                       
        }

        return (salaries, validationResults);
    }

    public static void Load(SqlConnection connection, List<Salary> salaries)
    {
        var ado = new ADOHelpers();

        using (SqlCommand command = new SqlCommand("InsertSalaries", connection))
        {
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = command.Parameters.AddWithValue("@Salaries", ado.CreateSalaryTable(salaries));
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.TypeName = "SalaryTableType";

            command.ExecuteNonQuery();
        }  
    }
}
