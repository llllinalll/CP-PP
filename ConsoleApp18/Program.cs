/////////////Системное программирование


//using System;
//using System.IO;
//using System.Threading;
//using System.Threading.Tasks;

//class Program
//{
//    static string filePath = "log.txt";
//    static readonly object fileLock = new object(); // Объект для синхронизации доступа к файлу

//    static void Main()
//    {
//        while (true)
//        {
//            Console.WriteLine("1. Создать файл и записать данные");
//            Console.WriteLine("2. Прочитать данные из файла");
//            Console.WriteLine("3. Записать данные параллельно через потоки");
//            Console.WriteLine("4. Удалить файл");
//            Console.WriteLine("5. Выход");
//            Console.Write("Введите номер команды: ");

//            if (int.TryParse(Console.ReadLine(), out int choice))
//            {
//                switch (choice)
//                {
//                    case 1:
//                        CreateFileAndWriteData();
//                        break;
//                    case 2:
//                        ReadFile();
//                        break;
//                    case 3:
//                        WriteDataInParallel();
//                        break;
//                    case 4:
//                        DeleteFile();
//                        break;
//                    case 5:
//                        Console.WriteLine("Выход из программы.");
//                        return;
//                    default:
//                        Console.WriteLine("Неверный выбор.");
//                        break;
//                }
//            }
//            else
//            {
//                Console.WriteLine("Неверный ввод.");
//            }
//        }
//    }

//    // Создание файла и запись данных
//    static void CreateFileAndWriteData()
//    {
//        try
//        {
//            using (StreamWriter writer = new StreamWriter(filePath))
//            {
//                for (int i = 0; i < 10; i++)
//                {
//                    writer.WriteLine($"Строка {i + 1}: {DateTime.Now}");
//                }
//            }
//            Console.WriteLine("Файл создан и данные в него записаны.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка при создании или записи файла: {ex.Message}");
//        }
//    }

//    // Чтение данных из файла
//    static void ReadFile()
//    {
//        try
//        {
//            if (File.Exists(filePath))
//            {
//                using (StreamReader reader = new StreamReader(filePath))
//                {
//                    Console.WriteLine("Содержимое файла:");
//                    Console.WriteLine(reader.ReadToEnd());
//                }
//            }
//            else
//            {
//                Console.WriteLine("Файл не существует.");
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
//        }
//    }

//    // Запись данных параллельно через потоки
//    static void WriteDataInParallel()
//    {
//        try
//        {
//            Task[] tasks = new Task[2];

//            for (int i = 0; i < 2; i++)
//            {
//                int threadNumber = i + 1;
//                tasks[i] = Task.Run(() =>
//                {
//                    for (int j = 0; j < 5; j++)
//                    {
//                        lock (fileLock) // Блокировка для синхронизации доступа к файлу
//                        {
//                            using (StreamWriter writer = new StreamWriter(filePath, append: true))
//                            {
//                                writer.WriteLine($"Поток {threadNumber}: Время {DateTime.Now}");
//                            }
//                        }
//                        Thread.Sleep(100); // Задержка для видимости параллелизма
//                    }
//                });
//            }

//            Task.WaitAll(tasks);
//            Console.WriteLine("Данные записаны параллельно через потоки.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка при записи данных : {ex.Message}");
//        }
//    }

//    // Удаление файла
//    static void DeleteFile()
//    {
//        try
//        {
//            if (File.Exists(filePath))
//            {
//                File.Delete(filePath);
//                Console.WriteLine("Файл удален.");
//            }
//            else
//            {
//                Console.WriteLine("Файл не существует.");
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка при удалении файла: {ex.Message}");
//        }
//    }
//}






///////////////////////////Прикладное программирование

using System;

class PuzzleGame
{
    private int[,] board;
    private int emptyRow;
    private int emptyCol;
    private int size;
    private int moveCount;
    private static readonly int[] initialBoard =
    {
        1, 2, 3, 4,
        5, 6, 7, 8,
        9, 10, 11, 12,
        13, 14, 15, 0
    };

    public PuzzleGame(int size = 4, int seed = 123)
    {
        this.size = size;
        board = new int[size, size];
        InitializeBoard(seed);
        moveCount = 0;
    }

    // Инициализация игрового поля с фиксированным начальным состоянием
    private void InitializeBoard(int seed)
    {
        Random rand = new Random(seed);
        int[] boardArray = (int[])initialBoard.Clone();
        ShuffleArray(boardArray, rand);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                board[i, j] = boardArray[i * size + j];
                if (board[i, j] == 0)
                {
                    emptyRow = i;
                    emptyCol = j;
                }
            }
        }
    }

    // Перемешивание массива
    private void ShuffleArray(int[] array, Random rand)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    // Вывод игрового поля
    public void PrintBoard()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (board[i, j] == 0)
                    Console.Write("_ ");
                else
                    Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    // Перемещение плитки
    public void Move(char direction)
    {
        int newRow = emptyRow;
        int newCol = emptyCol;

        switch (direction)
        {
            case 'W': // вверх
                newRow--;
                break;
            case 'A': // влево
                newCol--;
                break;
            case 'S': // вниз
                newRow++;
                break;
            case 'D': // вправо
                newCol++;
                break;
            default:
                Console.WriteLine("Неверная команда.");
                return;
        }

        if (IsValidPosition(newRow, newCol))
        {
            board[emptyRow, emptyCol] = board[newRow, newCol];
            board[newRow, newCol] = 0;
            emptyRow = newRow;
            emptyCol = newCol;
            moveCount++;
        }
        else
        {
            Console.WriteLine("Неверный ход.");
        }
    }

    // Проверка на корректность позиции
    private bool IsValidPosition(int row, int col)
    {
        return row >= 0 && row < size && col >= 0 && col < size;
    }

    // Проверка победного состояния
    public bool CheckWin()
    {
        int num = 1;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i == size - 1 && j == size - 1)
                    return board[i, j] == 0;
                if (board[i, j] != num++)
                    return false;
            }
        }
        return true;
    }

    public void Run()
    {
        Console.WriteLine("Добро пожаловать в игру 'Пятнашки'!");
        while (true)
        {
            PrintBoard();
            Console.WriteLine("Введите команду для перемещения плитки (W - вверх, A - влево, S - вниз, D - вправо, Q - выход):");
            char command = Console.ReadKey().KeyChar;
            Console.WriteLine();

            // Преобразуем команду в верхний регистр для унификации
            command = char.ToUpper(command);

            if (command == 'Q')
            {
                Console.WriteLine("Выход из игры.");
                break;
            }

            Move(command);

            if (CheckWin())
            {
                PrintBoard();
                Console.WriteLine($"Поздравляем! Вы решили головоломку за {moveCount} ходов.");
                break;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        PuzzleGame game = new PuzzleGame(seed: 123); // Укажите здесь фиксированный seed для постоянного начального состояния
        game.Run();
    }
}

//Лабиринт
//using System;
//using System.Collections.Generic;
//using System.Linq;

//class Program
//{
//    private static List<string> names = new List<string>
//    {
//        "Anna", "Jonathan", "Canada", "Andrew", "Banana", "Alan"
//    };
//    private static string currentSearch = string.Empty; // Переменная для хранения текущего ввода пользователя 

//    static void Main()
//    {
//        // Инициализация экрана 
//        Console.Clear();
//        DisplayList(names);

//        // Основной цикл для обработки ввода пользователя 
//        while (true)
//        {
//            // Обновление экрана с текущим поисковым запросом 
//            UpdateScreen();

//            // Показ приглашения и получение ввода пользователя 
//            Console.WriteLine("\nВведите строку для поиска (или 'exit' для завершения):");
//            Console.Write($"Поиск: '{currentSearch}'"); // Показываем текущий запрос 

//            ConsoleKeyInfo keyInfo;
//            while (true)
//            {
//                keyInfo = Console.ReadKey(intercept: true); // Читаем клавишу без отображения в консоли 

//                if (keyInfo.Key == ConsoleKey.Enter) // При нажатии Enter выходим из внутреннего цикла 
//                {
//                    Console.WriteLine(); // Для перевода строки после ввода 
//                    break;
//                }
//                else if (keyInfo.Key == ConsoleKey.Backspace) // При нажатии Backspace удаляем последний символ 
//                {
//                    if (currentSearch.Length > 0)
//                    {
//                        // Удаляем последний символ из текущего запроса 
//                        currentSearch = currentSearch.Remove(currentSearch.Length - 1);
//                        // Обновляем экран 
//                        UpdateScreen();
//                        // Убираем последний символ из консоли 
//                        Console.Write("\b \b"); // Удаляем последний символ в консоли 
//                    }
//                }
//                else if (keyInfo.Key == ConsoleKey.Escape) // При нажатии Escape очищаем текущий запрос 
//                {
//                    currentSearch = string.Empty;
//                    UpdateScreen();
//                }
//                else if (char.IsLetterOrDigit(keyInfo.KeyChar) || char.IsWhiteSpace(keyInfo.KeyChar)) // Для ввода символов 
//                {
//                    currentSearch += keyInfo.KeyChar;
//                    Console.Write(keyInfo.KeyChar); // Отображаем символ в консоли 
//                }
//            }
//        }
//    }

//    // Метод для обновления экрана 
//    static void UpdateScreen()
//    {
//        Console.Clear();
//        DisplayList(names);
//        DisplayResults(SearchStrings(names, currentSearch), currentSearch);
//    }

//    // Метод для отображения списка строк 
//    static void DisplayList(List<string> list)
//    {
//        Console.WriteLine("Список строк:");
//        foreach (var name in list)
//        {
//            Console.WriteLine(name);
//        }
//    }

//    // Метод для отображения результатов поиска 
//    static void DisplayResults(List<string> results, string search)
//    {
//        Console.WriteLine($"\nРезультаты поиска по '{search}':");
//        if (results.Count > 0)
//        {
//            foreach (var result in results)
//            {
//                Console.WriteLine(result);
//            }
//        }
//        else
//        {
//            Console.WriteLine("Совпадений не найдено.");
//        }
//    }

//    // Функция для поиска строк 
//    static List<string> SearchStrings(List<string> list, string search)
//    {
//        return list.Where(s => s.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
//    }
//}




using System;

class Program
{
    static void Main()
    {
        // Определяем лабиринт (1 - стена, пробел - путь, '.' - сокровище)
        char[,] maze = {
            { '1', '1', '1', '1', '1', '1', '1', '1' },
            { '1', ' ', '$', '1', ' ', ' ', ' ', '1' },
            { '1', ' ', '1', '1', ' ', '1', '1', '1' },
            { '1', '$', ' ', ' ', ' ', ' ', ' ', '1' },
            { '1', ' ', '1', '1', '1', '1', '$', '1' },
            { '1', '$', ' ', ' ', '$', '1', ' ', ' ' },
            { '1', ' ', '1', '1', ' ', '1', ' ', '1' },
            { '1', '1', '1', '1', '1', '1', '1', '1' }
        };

        // Начальная позиция 'O'
        int posX = 1;
        int posY = 1;
        maze[posY, posX] = 'O';

        // Целевая позиция (выход из лабиринта)
        int exitX = maze.GetLength(1) - 1; // Предположим, что выход находится на последнем столбце
        int exitY = maze.GetLength(0) - 3; // и предпоследней строке

        int treasuresCollected = 0;

        while (true)
        {
            // Выводим лабиринт и счет сокровищ
            PrintMaze(maze);
            Console.WriteLine($"Собранные сокровища: {treasuresCollected}");

            // Проверка на достижение выхода
            if (posX == exitX && posY == exitY)
            {
                Console.WriteLine("Поздравляю! Вы нашли выход");
                Console.WriteLine($"Общее количество собранных сокровищ: {treasuresCollected}");
                break;
            }

            // Ввод пользователя
            Console.Write("Введите направление (w/a/s/ d - вверх/влево/ вниз/вправо, q - выход).: ");
            char direction = Console.ReadKey().KeyChar;
            Console.WriteLine();

            // Выход из программы
            if (direction == 'q') break;

            // Перемещение
            Move(ref posY, ref posX, direction, maze, ref treasuresCollected);
        }
    }

    static void PrintMaze(char[,] maze)
    {
        Console.Clear();
        for (int y = 0; y < maze.GetLength(0); y++)
        {
            for (int x = 0; x < maze.GetLength(1); x++)
            {
                Console.Write(maze[y, x]);
            }
            Console.WriteLine();
        }
    }

    static void Move(ref int posY, ref int posX, char direction, char[,] maze, ref int treasuresCollected)
    {
        // Очистка текущей позиции
        maze[posY, posX] = ' ';

        // Определение новой позиции
        int newY = posY;
        int newX = posX;

        switch (direction)
        {
            case 'w': // вверх
                newY--;
                break;
            case 's': // вниз
                newY++;
                break;
            case 'a': // влево
                newX--;
                break;
            case 'd': // вправо
                newX++;
                break;
            default:
                Console.WriteLine("Неверное направление. Используйте \"w\", \"a\", \"s\", \"d\" или \"q\".");
                // Вернуться без обновления позиции
                maze[posY, posX] = 'O'; // Восстановить предыдущую позицию
                return;
        }

        // Проверка границ и стен
        if (newY >= 0 && newY < maze.GetLength(0) && newX >= 0 && newX < maze.GetLength(1))
        {
            if (maze[newY, newX] == ' ')
            {
                posY = newY;
                posX = newX;
            }
            else if (maze[newY, newX] == '$')
            {
                posY = newY;
                posX = newX;
                treasuresCollected++;
            }
        }

        // Установка нового положения
        maze[posY, posX] = 'O';
    }
}


