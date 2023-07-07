using Newtonsoft.Json;
using TestCases.Models;
//using InternalTypes.Types;

//var typeSet = new CppTypeSet();

var testCaseCount = 10000;

var random = new Random();
var testCases = new List<TestCase>();
for (int i = 0; i < testCaseCount; i++)
{
    var expected = random.Next();
    var firstInput = random.Next() % expected;
    testCases.Add(new TestCase
    {
        Id = i,
        Expected = expected.ToString(),
        Input = new()
        {
            firstInput.ToString(),
            (expected - firstInput).ToString(),
        },
    });
}
using var fileStream = new StreamWriter($"{Guid.NewGuid()}.json");

fileStream.WriteLine(JsonConvert.SerializeObject(testCases));

//Console.WriteLine(typeSet.ValueOfTypeAsString(InternalType._Char, "a"));
//Console.WriteLine(typeSet.ValueOfTypeAsString(InternalType._String, "sdasdwa"));
//Console.WriteLine(typeSet.ValueOfTypeAsString(InternalType._Int32, "1"));
//Console.WriteLine(typeSet.ValueOfTypeAsString(InternalType._Int64, "1234"));
//Console.WriteLine(typeSet.ValueOfTypeAsString(InternalType._Float, "1241"));
//Console.WriteLine(typeSet.ValueOfTypeAsString(InternalType._Array | InternalType._String, "3, 3, 4,5, 1, 43"));


//Console.WriteLine(typeSet.TypeAsString(InternalType._Char));
//Console.WriteLine(typeSet.TypeAsString(InternalType._String));
//Console.WriteLine(typeSet.TypeAsString(InternalType._Int32));
//Console.WriteLine(typeSet.TypeAsString(InternalType._Int64));
//Console.WriteLine(typeSet.TypeAsString(InternalType._Float));
//Console.WriteLine(typeSet.TypeAsString(InternalType._Array | InternalType._String));