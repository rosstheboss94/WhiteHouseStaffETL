using System.Data;
using System.Data.SqlClient;
using WhiteHouseETL.Models;

namespace WhiteHouseETL.Helpers;

public class ADOHelpers
{
    public ADOHelpers()
    {

    }

    public DataTable CreateEmployeeTable(List<Employee> employees)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Row Number", typeof(int));
        dt.Columns.Add("First Name", typeof(string));
        dt.Columns.Add("Middle Inital", typeof(string));
        dt.Columns.Add("Last Name", typeof(string));
        dt.Columns.Add("Gender", typeof(string));

        foreach (var employee in employees)
        {
            dt.Rows.Add(employee.RowNumber, employee.FirstName, employee.MiddleInitial, employee.LastName, employee.Gender);
        }

        return dt;
    }

    public DataTable CreatePositionTable(List<Position> positions)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Row Number", typeof(int));
        dt.Columns.Add("Position Title", typeof(string));
        dt.Columns.Add("Pay Basis", typeof(string));
        dt.Columns.Add("Status", typeof(string));

        foreach (var position in positions)
        {
            dt.Rows.Add(position.RowNumber, position.PositionTitle, position.PayBasis, position.Status);
        }

        return dt;
    }

    public DataTable CreateSalaryTable(List<Salary> salaries)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Row Number", typeof(int));
        dt.Columns.Add("Salary", typeof(string));
        dt.Columns.Add("Year", typeof(string));

        foreach (var salary in salaries)
        {
            dt.Rows.Add(salary.RowNumber, salary.EmployeeSalary, salary.Year);
        }

        return dt;
    }
}
