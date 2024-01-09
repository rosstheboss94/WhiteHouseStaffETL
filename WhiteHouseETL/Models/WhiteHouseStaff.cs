using CsvHelper.Configuration;

namespace WhiteHouseETL.Models;

public class WhiteHouseStaff
{
    private static int conuter = 0;

    public int RowNumber { get; set; }
    public int Year { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public string Status { get; set; }
    public int Salary { get; set; }
    public string PayBasis { get; set; }
    public string PositionTitle { get; set; }

    public WhiteHouseStaff()
    {
        RowNumber = ++conuter;
    }
}

public class WhiteHouseStaffMap : ClassMap<WhiteHouseStaff> 
{
    public WhiteHouseStaffMap()
    {
        Map(m => m.Year).Name("year").Default(1900);
        Map(m => m.Name).Name("name").Default("");
        Map(m => m.Gender).Name("gender").Default("");
        Map(m => m.Status).Name("status").Default("");
        Map(m => m.Salary).Name("salary").Default(0);
        Map(m => m.PayBasis).Name("pay_basis").Default("");
        Map(m => m.PositionTitle).Name("position_title").Default("");
    }
}
