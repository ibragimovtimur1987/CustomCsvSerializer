using System.Diagnostics;
using System.Management;
using CustomCsvSerializer;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        GetEnvInfo();

        var testObject = F.Get();
        int iterations = 100000;

        TestSerializeDeserialize("Мой рефлекшен:", iterations, testObject, CsvSerializer.Serialize, CsvSerializer.Deserialize<F>);
        TestSerializeDeserialize("Cтандартный механизм (NewtonsoftJson):", iterations, testObject, JsonConvert.SerializeObject, JsonConvert.DeserializeObject<F>);
    }
    
    static void GetEnvInfo()
    {
        Console.WriteLine("Версия ОС: " + Environment.OSVersion);
        Console.WriteLine("Количество процессоров: " + Environment.ProcessorCount);
    
        var searcherCpu = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
        foreach (var o in searcherCpu.Get())
        {
            var obj = (ManagementObject)o;
            Console.WriteLine("Процессор: " + obj["Name"]);
        }
    
        var searcherMemory = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
        ulong totalMemory = 0;
        foreach (var o in searcherMemory.Get())
        {
            var obj = (ManagementObject)o;
            totalMemory += (ulong)obj["Capacity"];
        }

        Console.WriteLine("Объем оперативной памяти: " + totalMemory / (1024 * 1024) + " МБ");
    }

    static void TestSerializeDeserialize(string nameTest, int iterations1, F testObject, Func<F, string> funcSerialize, Func<string, F> funcDeserialize)
    {
        Console.WriteLine(nameTest);
    
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();

        for (int i = 0; i < iterations1; i++)
        {
            funcSerialize(testObject);
        }

        stopwatch.Stop();

        string data = funcSerialize(testObject);
        Console.WriteLine($"Время на сериализацию {data}: {stopwatch.ElapsedMilliseconds} мс");

        stopwatch.Restart();
        for (int i = 0; i < iterations1; i++)
        {
            funcDeserialize(data);
        }

        stopwatch.Stop();
        Console.WriteLine($"Время на десериализацию : {stopwatch.ElapsedMilliseconds} мс");
    }
}
