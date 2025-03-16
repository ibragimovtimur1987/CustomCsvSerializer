namespace CustomCsvSerializer;

public class CsvSerializer
{
    public static string Serialize<T>(T obj)
    {
        var properties = typeof(T).GetFields();
        var values = new List<string>();

        foreach (var property in properties)
        {
            if (property.FieldType == typeof(int))
            {
                var value = property.GetValue(obj)?.ToString();
                values.Add(value);
            }
        }

        return string.Join(",", values);
    }

    public static T Deserialize<T>(string csv) where T : new()
    {
        var properties = typeof(T).GetFields();
        var values = csv.Split(',');

        var obj = new T();

        for (int i = 0; i < properties.Length; i++)
        {
            if (i < values.Length && properties[i].FieldType == typeof(int))
            {
                properties[i].SetValue(obj, int.Parse(values[i]));
            }
        }

        return obj;
    }
}