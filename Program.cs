using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static readonly string filePath = "data.txt";
    private static readonly string logFilePath = "log.txt";
    private static readonly object fileLock = new object();
    private static int[] numbers;

    static async Task Main(string[] args)
    {
        // Создание файла с начальными данными
        CreateFile();

        // Чтение данных из файла
        ReadFile();

        // Запись новых данных в файл
        AppendToFile("60");

        // Чтение данных после добавления новых данных
        ReadFile();

        // Многопоточная обработка данных
        ProcessFileInThreads();

        // Взаимодействие с сетевым ресурсом
        await FetchDataFromApi();
    }

    static void CreateFile()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "10\n20\n30\n40\n50");
            Console.WriteLine("Файл, созданный с исходными данными.");
        }
    }

    static void ReadFile()
    {
        lock (fileLock)
        {
            var lines = File.ReadAllLines(filePath);
            numbers = lines
                .Select(line => {
                    int number;
                    bool success = int.TryParse(line, out number);
                    return success ? number : (int?)null;
                })
                .Where(n => n.HasValue)
                .Select(n => n.Value)
                .ToArray();

            Console.WriteLine("Данные, считываемые из файла:");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        }
    }

    static void AppendToFile(string newData)
    {
        lock (fileLock)
        {
            File.AppendAllText(filePath, newData + Environment.NewLine);
            Console.WriteLine("К файлу добавляются новые данные.");
        }
    }

    static void ProcessFileInThreads()
    {
        if (numbers.Length == 0)
        {
            Console.WriteLine("Нет действительных номеров для обработки.");
            return;
        }

        Thread averageThread = new Thread(() => CalculateAverage(numbers));
        Thread maxThread = new Thread(() => FindMaximum(numbers));
        Thread minThread = new Thread(() => FindMinimum(numbers));

        averageThread.Start();
        maxThread.Start();
        minThread.Start();

        averageThread.Join();
        maxThread.Join();
        minThread.Join();
    }

    static void CalculateAverage(int[] numbers)
    {
        double average = numbers.Length > 0 ? numbers.Average() : 0;
        Console.WriteLine($"Средний: {average}");
    }

    static void FindMaximum(int[] numbers)
    {
        int max = numbers.Length > 0 ? numbers.Max() : 0;
        Console.WriteLine($"Максимальный: {max}");
    }

    static void FindMinimum(int[] numbers)
    {
        int min = numbers.Length > 0 ? numbers.Min() : 0;
        Console.WriteLine($"Минимальный: {min}");
    }

    static async Task FetchDataFromApi()
    {
        using HttpClient client = new HttpClient();
        try
        {
            string url = "https://jsonplaceholder.typicode.com/posts/1";
            string result = await client.GetStringAsync(url);
            Console.WriteLine("Данные из API:");
            Console.WriteLine(result);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Ошибка запроса: {e.Message}");
        }
        static void Log(string message)
        {

            lock (fileLock)
            {

                File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
            }
        }
    }
}

