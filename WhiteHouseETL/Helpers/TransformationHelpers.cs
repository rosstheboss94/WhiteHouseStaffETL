using System.Text.RegularExpressions;

namespace WhiteHouseETL.Helpers;

public static class TransformationHelpers
{
    public static string SplitName(string text, string type = "FIRST")
    {
        string extractedText = "";
        string suffix;
        int commaCount = text.Count(c => c == ',');
        int comma = text.IndexOf(',');
        int spaceAfterFirstName = text.IndexOf(" ", comma);
        int commaAfterMiddleInitial = comma != -1 ? text.IndexOf(",", comma + 1) : -1;
        int length;

        if (type == "LAST" && text.Length > 0)
        {
            if (commaAfterMiddleInitial != -1)
            {
                length = text.Length - ( commaAfterMiddleInitial + 1 );
                suffix = text.Substring(commaAfterMiddleInitial + 1, length);

                length = comma;
                extractedText = text.Substring(0, length) + " " + suffix;
            }
            else
            {
                length = comma;
                extractedText = text.Substring(0, length);
            }
        }

        if (type == "FIRST" && text.Length > 0)
        {
            if (spaceAfterFirstName == -1 && commaAfterMiddleInitial != -1)
            {
                length = text.IndexOf(",", comma + 1) - ( comma + 1 );
                extractedText = text.Substring(comma + 1, length);
            }
            else if (spaceAfterFirstName == -1)
            {
                length = text.Length - (comma + 1);
                extractedText = text.Substring(comma + 1, length);
            }
            else
            {
                length = spaceAfterFirstName - ( comma + 1 );
                extractedText = text.Substring(comma + 1, length);
            }
        }

        if (type == "MIDDLE" && text.Length > 0)
        {
            if (spaceAfterFirstName != -1 && commaAfterMiddleInitial == -1) 
            {
                length = text.Length - ( spaceAfterFirstName + 1 );
                extractedText = text.Substring(spaceAfterFirstName + 1, length);
                if (!extractedText.Contains('.')) extractedText = extractedText.Substring(0, 1) + ".";
            }
            else if (spaceAfterFirstName != -1 && commaAfterMiddleInitial != -1)
            {
                length = commaAfterMiddleInitial - ( spaceAfterFirstName + 1 );
                extractedText = text.Substring(spaceAfterFirstName + 1, length);
                if (!extractedText.Contains('.')) extractedText = extractedText.Substring(0, 1) + ".";
            }

            if (extractedText.Count(c => c == '.') == 1 && extractedText.Length > 2)
            {
                int periodIndex = extractedText.IndexOf(".");

                if (periodIndex == extractedText.Length - 1) 
                {
                    extractedText = extractedText.Substring(0, 1) + ". " + extractedText.Substring(periodIndex - 1, 2);
                }
                else
                {
                    extractedText = extractedText.Substring(0, 2) + " " + extractedText.Substring(periodIndex + 2, 1) + ".";
                }
            }
        }

        return extractedText;
    }

    public static string[] SplitPositionTitle(string positionTitle)
    {
        int ofIndex;
        int forIndex;
        int toIndex;
        int commaIndex;
        int andIndex;
        string splitOn = "";

        positionTitle = positionTitle.Replace("&", "AND");
        positionTitle = positionTitle.Replace(" - ", " AND ");
        positionTitle = positionTitle.Replace("/", " AND ");
        positionTitle = positionTitle.Replace("-", " ");
        positionTitle = positionTitle.Replace(".", "");
        positionTitle = Regex.Replace(positionTitle, @"\([^)]*\)", string.Empty);

        ofIndex = positionTitle.IndexOf(" OF ");
        forIndex = positionTitle.IndexOf(" FOR ");
        toIndex = positionTitle.IndexOf(" TO ");
        commaIndex = positionTitle.IndexOf(", ");
        andIndex = positionTitle.IndexOf(" AND ");


        int selectedIndex =
            Math.Min(
                Math.Min(
                    Math.Min(
                         Math.Min(ofIndex >= 0 ? ofIndex : int.MaxValue, forIndex >= 0 ? forIndex : int.MaxValue), toIndex >= 0 ? toIndex : int.MaxValue),
                    commaIndex >= 0 ? commaIndex : int.MaxValue),
                andIndex >= 0 ? andIndex : int.MaxValue);

        if (selectedIndex == ofIndex) splitOn = " OF ";
        if (selectedIndex == forIndex) splitOn = " FOR ";
        if (selectedIndex == toIndex) splitOn = " TO ";
        if (selectedIndex == commaIndex) splitOn = ", ";
        if (selectedIndex == andIndex && ofIndex == -1 && forIndex == -1 && toIndex == -1 && commaIndex == -1) splitOn = " AND ";

        string[] roles = positionTitle.Split(splitOn);

        if (splitOn == " AND ") return roles;
        else return new string[] { roles[0] };
    }
}
