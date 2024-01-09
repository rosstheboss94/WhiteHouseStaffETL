using CsvHelper;
using System.Data.SqlClient;
using System.Globalization;
using WhiteHouseETL.Helpers;
using WhiteHouseETL.Models;
using WhiteHouseETL.Tasks;


string filePath = @"C:\Users\torra\Desktop\Projects\WhiteHouseETL\WhiteHouseETL\BS&A whstafferdata.csv";
string db = "Data Source=TJ;Database=WhiteHouseETL;Integrated Security=True;";

using (var streamReader = new StreamReader(filePath))
{
    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
    {
        List<Employee> employees = new List<Employee>();
        List<Position> positions = new List<Position>();
        List<Salary> salaries = new List<Salary>();
        List<ValidationResult> validationResults = new List<ValidationResult>();

        csvReader.Context.RegisterClassMap<WhiteHouseStaffMap>();
        var records = csvReader.GetRecords<WhiteHouseStaff>().ToList();

        (records, validationResults) = ValidationHelpers.WhiteHouseStaffFileValidation(records);

        (employees, validationResults) = TaskEmployee.Transform(records, validationResults);
        (positions, validationResults) = TaskPosition.Transform(records, validationResults);
        (salaries, validationResults) = TaskSalary.Transform(records, validationResults);

        using (SqlConnection connection = new SqlConnection(db))
        {
            connection.Open();

            TaskEmployee.Load(connection, employees);
            TaskPosition.Load(connection, positions);
            TaskSalary.Load(connection, salaries);

            connection.Close();
        }

        PrintInvalid(validationResults);
    }
}

void PrintInvalid(List<ValidationResult> invalidRecords)
{
    string content = "";
    string empContent = "";
    string posContent = "";
    string salContent = "";

    foreach (var record in invalidRecords)
    {
        string yearStr = record.Record.Year == 1900 ? "" : record.Record.Year.ToString();
        string salaryStr = record.Record.Salary == 0 ? "" : record.Record.Salary.ToString();

        content = content + "Record: " + $"\n\tYear - {yearStr} \n\tName - {record.Record.Name} \n\tGender - {record.Record.Gender} \n\tStatus - {record.Record.Status} \n\tSalary - {salaryStr} \n\tPay Basis - {record.Record.PayBasis} \n\tPosition Title - {record.Record.PositionTitle}" + "\n";
        empContent = empContent + "Record: " + $"\n\tYear - {yearStr} \n\tName - {record.Record.Name} \n\tGender - {record.Record.Gender} \n\tStatus - {record.Record.Status} \n\tSalary - {salaryStr} \n\tPay Basis - {record.Record.PayBasis} \n\tPosition Title - {record.Record.PositionTitle}" + "\n";
        posContent = posContent + "Record: " + $"\n\tYear - {yearStr} \n\tName - {record.Record.Name} \n\tGender - {record.Record.Gender} \n\tStatus - {record.Record.Status} \n\tSalary - {salaryStr} \n\tPay Basis - {record.Record.PayBasis} \n\tPosition Title - {record.Record.PositionTitle}" + "\n";
        salContent = salContent + "Record: " + $"\n\tYear - {yearStr} \n\tName - {record.Record.Name} \n\tGender - {record.Record.Gender} \n\tStatus - {record.Record.Status} \n\tSalary - {salaryStr} \n\tPay Basis - {record.Record.PayBasis} \n\tPosition Title - {record.Record.PositionTitle}" + "\n";

        if (record.Results["Validation Type"] == "File")
        {
            content = content + "Outcome: " + record.Passed + "\n";
            content = content + "Results: \n";

            foreach (KeyValuePair<string, string> kvp in record.Results)
            {
                content = content + "\t" + kvp.Key + ": " + kvp.Value + "\n";
            }

            content = content + "\n";
        }

        if (record.Results["Validation Type"] == "Employee")
        {
            empContent = empContent + "Employee: " + $"\n\tFirst Name - {record.Employee.FirstName} \n\tMiddle Initial - {record.Employee.MiddleInitial} \n\tLast Name - {record.Employee.LastName} \n\tGender - {record.Employee.Gender} \n";

            empContent = empContent + "Outcome: " + record.Passed + "\n";
            empContent = empContent + "Results: \n";

            foreach (KeyValuePair<string, string> kvp in record.Results)
            {
                empContent = empContent + "\t" + kvp.Key + ": " + kvp.Value + "\n";
            }

            empContent = empContent + "\n";
        }

        if (record.Results["Validation Type"] == "Position")
        {
            posContent = posContent + "Position: " + $"\n\tPosition Title - {record.Position.PositionTitle} \n\tPay Basis - {record.Position.PayBasis} \n\tStatus - {record.Position.Status} \n";

            posContent = posContent + "Outcome: " + record.Passed + "\n";
            posContent = posContent + "Results: \n";

            foreach (KeyValuePair<string, string> kvp in record.Results)
            {
                posContent = posContent + "\t" + kvp.Key + ": " + kvp.Value + "\n";
            }

            posContent = posContent + "\n";
        } 

        if (record.Results["Validation Type"] == "Salary") 
        {
            string salaryYearStr = record.Salary.Year == 1900 ? "" : record.Salary.Year.ToString();
            string salarySalaryStr = record.Salary.EmployeeSalary == 0 ? "" : record.Salary.EmployeeSalary.ToString();
            salContent = salContent + "Salary: " + $"\n\tSalary - {salarySalaryStr} \n\tYear - {salaryYearStr} \n";

            salContent = salContent + "Outcome: " + record.Passed + "\n";
            salContent = salContent + "Results: \n";

            foreach (KeyValuePair<string, string> kvp in record.Results)
            {
                salContent = salContent + "\t" + kvp.Key + ": " + kvp.Value + "\n";
            }

            salContent = salContent + "\n";
        } 
    }

    File.WriteAllText(@"C:\Users\torra\Desktop\Projects\WhiteHouseETL\WhiteHouseETL\Validation Report\FileValidationResult.txt", content);
    File.WriteAllText(@"C:\Users\torra\Desktop\Projects\WhiteHouseETL\WhiteHouseETL\Validation Report\EmployeeValidationResult.txt", empContent);
    File.WriteAllText(@"C:\Users\torra\Desktop\Projects\WhiteHouseETL\WhiteHouseETL\Validation Report\PositionValidationResult.txt", posContent);
    File.WriteAllText(@"C:\Users\torra\Desktop\Projects\WhiteHouseETL\WhiteHouseETL\Validation Report\SalaryValidationResult.txt", salContent);

    Console.WriteLine(invalidRecords.Count());
    Console.WriteLine("finished");
}
