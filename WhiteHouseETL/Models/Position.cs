using CsvHelper.Configuration;

namespace WhiteHouseETL.Models;

public class Position
{
    public int RowNumber { get; set; }
    public string PositionTitle { get; set; }
    public string PayBasis { get; set; }
    public string Status { get; set; }
}

