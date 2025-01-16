using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json;

var salesFiles = FindFiles ("stores");

foreach (var file in salesFiles)
{
    Console.WriteLine (file);
}

IEnumerable <string> FindFiles (String folderName)
{
    List <string> salesFiles = new List <string> ();
    
    var foundFiles = Directory.EnumerateFiles (folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        // The file name will contain the full patch, so only check the end of it
        if (file.EndsWith ("sales.json"))
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

//GET THE CURRENT DIRECTORY WHERE THE PROGRAM IS RUNNING FROM
Console.WriteLine (Directory.GetCurrentDirectory());

//SPECTIAL DIRECTORIES
string docPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
Console.WriteLine (docPath);

//Special path characters
Console.WriteLine ($"stores{Path.DirectorySeparatorChar}201");

//JOIN PATHS
Console.WriteLine (Path.Combine("stores", "201"));

//DETERMINE FILENAME EXTENSIONS
Console.WriteLine (Path.GetExtension ("sales.json"));

//GET INFORMATION ABOUT A DIRECTORY OR FILE
string fileName = $"stores {Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";
FileInfo info = new FileInfo(fileName);

Console.WriteLine ($"Full Name: {info.FullName}{Environment.NewLine}Directory: {info.Directory}{Environment.NewLine}Extension: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}{Environment.NewLine}");


var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine (currentDirectory, "stores");
var salesFiles2 = FindFiles2(storesDirectory);
foreach (var file in salesFiles2)
{
    Console.WriteLine (file);
}

IEnumerable <string> FindFiles2 (string folderName)
{
    List <string> salesFiles2 = new List <string>();
    var foundFiles = Directory.EnumerateFiles (folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension (file);
        if (extension == ".json")
        {
            salesFiles2.Add(file);
        }
    }

    return salesFiles2;
}

//CREATE DIRECTORIES
Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "stores", "201", "newDir"));

//CHECK IF DIRECTORIES EXIST
bool doesDirectoryExist = Directory.Exists (Path.Combine(@"C:\Users\pocat\OneDrive\Documents\GitHub\.NET_Application_For_Beginner\mslearn-dotnet-files\stores\201\newDir"));
if (doesDirectoryExist == true)
{
    Console.WriteLine ("IT DOES!");
}

bool doesDirectoryExist2 = Directory.Exists (Path.Combine(@"C:\Users\pocat\OneDrive\Documents\GitHub\.NET_Application_For_Beginner\mslearn-dotnet-files\stores\201\newDir2"));
if (doesDirectoryExist2 == false)
{
    Console.WriteLine ("It's NOT there!");
}

//CREATE FILES
File.WriteAllText (Path.Combine (Directory.GetCurrentDirectory(), "greeting.txt"), "Hello BITCH!");

var salesTotalDir = Path.Combine (Directory.GetCurrentDirectory(), "salesTotalDir");
Directory.CreateDirectory (salesTotalDir);

//READ DATA FROM FILES
var salesJson = File.ReadAllText ($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");
Console.WriteLine (salesJson);

//PARSE DATA in FILES
var salesData = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

Console.WriteLine (salesData.Total);

//WRITE DATA TO FILES
var data = JsonConvert.DeserializeObject<SalesTotal> (salesJson);

//FILE.WRITE ALLTEXT DELETE ALL PREVIOUS DATA AND OVERWRITE NEW DATA IN ITS PLACE
File.WriteAllText ($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", data.Total.ToString());

var totalData = File.ReadAllText (Path.Combine(salesTotalDir, "totals.txt"));
Console.WriteLine (totalData);

var salesTotal = CalculateSalesTotal(salesFiles2);
//APPEND or ADD DATA TO FILES
File.AppendAllText ($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", $"{salesTotal}{Environment.NewLine}");


//METHOD THAT CALCULATES SALES TOTALS
double CalculateSalesTotal (IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    //READ FILES LOOP
    foreach (var file in salesFiles)
    {
        string salesJon = File.ReadAllText (file);

        // Parse the contents
        SalesData? data = JsonConvert.DeserializeObject<SalesData?> (salesJson);

        // Add the amount found in the Total field to te salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

record SalesData (double Total);
class SalesTotal
{
    public double Total { get; set; }
}