using System.Diagnostics;
using CustomCsvSerializer;
using Newtonsoft.Json;


// Подготовка данных
F testObject = F.Get();
int iterations = 100000;

// Сериализация с использованием собственного класса
Stopwatch stopwatch = new Stopwatch();

// Сериализация
stopwatch.Start();
for (int i = 0; i < iterations; i++)
{
    CsvSerializer.Serialize(testObject);
}

stopwatch.Stop();
Console.WriteLine($"Время на сериализацию (CSV): {stopwatch.ElapsedMilliseconds} мс");

// Десериализация
string csv = CsvSerializer.Serialize(testObject);
stopwatch.Restart();
for (int i = 0; i < iterations; i++)
{
    CsvSerializer.Deserialize<F>(csv);
}

stopwatch.Stop();
Console.WriteLine($"Время на десериализацию (CSV): {stopwatch.ElapsedMilliseconds} мс");

// Сравнение с Newtonsoft.Json
string json = String.Empty;

// Сериализация JSON
stopwatch.Restart();
for (int i = 0; i < iterations; i++)
{
    json = JsonConvert.SerializeObject(testObject);
}

stopwatch.Stop();
Console.WriteLine($"Время на сериализацию (JSON): {stopwatch.ElapsedMilliseconds} мс");

// Десериализация JSON
stopwatch.Restart();
for (int i = 0; i < iterations; i++)
{
    var obj = JsonConvert.DeserializeObject<F>(json);
}

stopwatch.Stop();
Console.WriteLine($"Время на десериализацию (JSON): {stopwatch.ElapsedMilliseconds} мс");