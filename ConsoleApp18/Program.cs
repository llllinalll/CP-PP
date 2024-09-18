/////////////Системное программирование и прикладное


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
//Разработка системы учета посещаемости студентов

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text.RegularExpressions;
//using System.Xml;
//using Newtonsoft.Json;

//public class AttendanceRecord
//{
//    public DateTime Date { get; set; }
//    public bool IsPresent { get; set; }

//    public AttendanceRecord(DateTime date, bool isPresent)
//    {
//        Date = date;
//        IsPresent = isPresent;
//    }
//}

//public class Subject
//{
//    public string Name { get; set; }
//    public List<double> Grades { get; set; } = new List<double>();

//    public Subject(string name)
//    {
//        Name = name;
//    }

//    public double GetAverageGrade()
//    {
//        return Grades.Count > 0 ? Grades.Average() : 0;
//    }
//}

//public class Student
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public string Email { get; set; }
//    public List<AttendanceRecord> Attendance { get; set; } = new List<AttendanceRecord>();
//    public List<Subject> Subjects { get; set; } = new List<Subject>();
//}

//public class User
//{
//    public string Username { get; set; }
//    public string Password { get; set; }
//    public bool IsTeacher { get; set; } // true для преподавателя, false для ученика
//}

//public class AttendanceManager
//{
//    private List<Student> students = new List<Student>();
//    private List<User> users = new List<User>();
//    private const string StudentsFilePath = "students.json";
//    private const string UsersFilePath = "users.json";
//    private int nextId = 1;

//    public AttendanceManager()
//    {
//        LoadStudents();
//        LoadUsers();
//    }

//    private void LoadStudents()
//    {
//        if (File.Exists(StudentsFilePath))
//        {
//            var json = File.ReadAllText(StudentsFilePath);
//            students = JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
//            nextId = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1; // Установка следующего ID
//        }
//    }

//    private void SaveStudents()
//    {
//        var json = JsonConvert.SerializeObject(students, Newtonsoft.Json.Formatting.Indented);
//        File.WriteAllText(StudentsFilePath, json);
//    }

//    private void LoadUsers()
//    {
//        if (File.Exists(UsersFilePath))
//        {
//            var json = File.ReadAllText(UsersFilePath);
//            users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
//        }
//    }

//    public void SaveUsers()
//    {
//        var json = JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
//        File.WriteAllText(UsersFilePath, json);
//    }

//    public void AddStudent(Student student)
//    {
//        student.Id = nextId++;
//        students.Add(student);
//        SaveStudents();
//    }

//    public void RemoveStudent(int id)
//    {
//        var student = students.FirstOrDefault(s => s.Id == id);
//        if (student != null)
//        {
//            students.Remove(student);
//            SaveStudents();
//            Console.WriteLine("Студент успешно удален.");
//        }
//        else
//        {
//            Console.WriteLine("Ошибка: Студент с данным ID не найден.");
//        }
//    }

//    public void EditStudent(int id, Student updatedStudent)
//    {
//        var student = students.FirstOrDefault(s => s.Id == id);
//        if (student != null)
//        {
//            student.Name = updatedStudent.Name;
//            student.Email = updatedStudent.Email;
//            SaveStudents();
//            Console.WriteLine("Данные студента успешно обновлены.");
//        }
//        else
//        {
//            Console.WriteLine("Ошибка: Студент с данным ID не найден.");
//        }
//    }

//    public void ListStudents()
//    {
//        if (students.Count == 0)
//        {
//            Console.WriteLine("Нет студентов для отображения.");
//            return;
//        }

//        foreach (var student in students)
//        {
//            Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Email: {student.Email}");
//        }
//    }

//    public void AddAttendance(int studentId, AttendanceRecord record)
//    {
//        var student = students.FirstOrDefault(s => s.Id == studentId);
//        if (student != null)
//        {
//            student.Attendance.Add(record);
//            SaveStudents();
//            Console.WriteLine("Запись о посещении успешно добавлена.");
//        }
//        else
//        {
//            Console.WriteLine("Ошибка: Студент с данным ID не найден.");
//        }
//    }

//    public void ListAttendance(int studentId)
//    {
//        var student = students.FirstOrDefault(s => s.Id == studentId);
//        if (student != null)
//        {
//            Console.WriteLine($"Посещения для студента {student.Name}:");
//            foreach (var record in student.Attendance)
//            {
//                Console.WriteLine($"Дата: {record.Date.ToShortDateString()}, Присутствовал: {(record.IsPresent ? "П" : "О")}");
//            }
//        }
//        else
//        {
//            Console.WriteLine("Ошибка: Студент с данным ID не найден.");
//        }
//    }

//    public void ListAllAttendance()
//    {
//        if (students.Count == 0)
//        {
//            Console.WriteLine("Нет студентов для отображения.");
//            return;
//        }

//        foreach (var student in students)
//        {
//            Console.WriteLine($"Посещения для студента {student.Name}:");
//            foreach (var record in student.Attendance)
//            {
//                Console.WriteLine($"ID: {student.Id}, Дата: {record.Date.ToShortDateString()}, Присутствовал: {(record.IsPresent ? "П" : "О")}");
//            }
//            Console.WriteLine(); // Добавляем пустую строку для удобства чтения
//        }
//    }

//    public void AddGrade(int studentId, string subjectName, double grade)
//    {
//        var student = students.FirstOrDefault(s => s.Id == studentId);
//        if (student != null)
//        {
//            var subject = student.Subjects.FirstOrDefault(s => s.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase));
//            if (subject == null)
//            {
//                subject = new Subject(subjectName);
//                student.Subjects.Add(subject);
//            }
//            subject.Grades.Add(grade);
//            SaveStudents();
//            Console.WriteLine($"Оценка {grade} по предмету {subjectName} добавлена для студента {student.Name}.");
//        }
//        else
//        {
//            Console.WriteLine("Ошибка: Студент с данным ID не найден.");
//        }
//    }

//    public void ListAllGrades(int studentId)
//    {
//        var student = students.FirstOrDefault(s => s.Id == studentId);
//        if (student != null)
//        {
//            Console.WriteLine($"Оценки для студента {student.Name}:");
//            foreach (var subject in student.Subjects)
//            {
//                double average = subject.GetAverageGrade();
//                Console.WriteLine($"Предмет: {subject.Name}, Оценки: {string.Join(", ", subject.Grades)}, Средний балл: {average:F2}");
//            }
//        }
//        else
//        {
//            Console.WriteLine("Ошибка: Студент с данным ID не найден.");
//        }
//    }

//    public bool IsValidEmail(string email)
//    {
//        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
//        return Regex.IsMatch(email, pattern);
//    }

//    public bool AuthenticateUser(string username, string password, out User user)
//    {
//        user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
//        return user != null;
//    }

//    public void AddUser(User user)
//    {
//        users.Add(user);
//        SaveUsers();
//    }
//}

//class Program
//{
//    static void Main(string[] args)
//    {
//        AttendanceManager manager = new AttendanceManager();
//        bool running = true;

//        while (true)
//        {
//            Console.WriteLine("Вход: Введите логин и пароль.");
//            Console.Write("Логин: ");
//            string username = Console.ReadLine();
//            Console.Write("Пароль: ");
//            string password = Console.ReadLine();

//            if (manager.AuthenticateUser(username, password, out User user))
//            {
//                Console.WriteLine($"Добро пожаловать, {username}!");

//                if (user.IsTeacher)
//                {
//                    // Меню для преподавателя
//                    while (running)
//                    {
//                        Console.WriteLine("Меню преподавателя:");
//                        Console.WriteLine("1. Добавить студента");
//                        Console.WriteLine("2. Удалить студента");
//                        Console.WriteLine("3. Редактировать студента");
//                        Console.WriteLine("4. Показать всех студентов");
//                        Console.WriteLine("5. Добавить запись о посещении");
//                        Console.WriteLine("6. Показать записи о посещаемости");
//                        Console.WriteLine("7. Показать всю посещаемость");
//                        Console.WriteLine("8. Добавить оценку");
//                        Console.WriteLine("9. Показать все предметы и оценки для студента");
//                        Console.WriteLine("10. Выход");

//                        switch (Console.ReadLine())
//                        {
//                            case "1":
//                                Console.Write("Введите имя студента: ");
//                                string name = Console.ReadLine();

//                                string email;
//                                do
//                                {
//                                    Console.Write("Введите email студента: ");
//                                    email = Console.ReadLine();
//                                    if (!manager.IsValidEmail(email))
//                                    {
//                                        Console.WriteLine("Некорректный email. Попробуйте снова.");
//                                    }
//                                } while (!manager.IsValidEmail(email));

//                                manager.AddStudent(new Student { Name = name, Email = email });
//                                break;
//                            case "2":
//                                Console.Write("Введите ID студента для удаления: ");
//                                if (int.TryParse(Console.ReadLine(), out int removeId))
//                                {
//                                    manager.RemoveStudent(removeId);
//                                }
//                                else
//                                {
//                                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
//                                }
//                                break;
//                            case "3":
//                                Console.Write("Введите ID студента для редактирования: ");
//                                if (int.TryParse(Console.ReadLine(), out int editId))
//                                {
//                                    Console.Write("Введите новое имя студента: ");
//                                    string newName = Console.ReadLine();

//                                    string newEmail;
//                                    do
//                                    {
//                                        Console.Write("Введите новый email студента: ");
//                                        newEmail = Console.ReadLine();
//                                        if (!manager.IsValidEmail(newEmail))
//                                        {
//                                            Console.WriteLine("Некорректный email. Попробуйте снова.");
//                                        }
//                                    } while (!manager.IsValidEmail(newEmail));

//                                    manager.EditStudent(editId, new Student { Name = newName, Email = newEmail });
//                                }
//                                else
//                                {
//                                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
//                                }
//                                break;
//                            case "4":
//                                manager.ListStudents();
//                                break;
//                            case "5":
//                                Console.Write("Введите ID студента для добавления посещения: ");
//                                if (int.TryParse(Console.ReadLine(), out int attendanceId))
//                                {
//                                    Console.Write("Введите дату посещения (yyyy-MM-dd): ");
//                                    if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
//                                    {
//                                        Console.Write("Присутствовал? (П/О): ");
//                                        string presenceInput = Console.ReadLine().ToUpper();
//                                        bool isPresent = presenceInput == "П";
//                                        manager.AddAttendance(attendanceId, new AttendanceRecord(date, isPresent));
//                                    }
//                                    else
//                                    {
//                                        Console.WriteLine("Некорректный формат даты.");
//                                    }
//                                }
//                                else
//                                {
//                                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
//                                }
//                                break;
//                            case "6":
//                                Console.Write("Введите ID студента для показа посещаемости: ");
//                                if (int.TryParse(Console.ReadLine(), out int attendanceShowId))
//                                {
//                                    manager.ListAttendance(attendanceShowId);
//                                }
//                                else
//                                {
//                                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
//                                }
//                                break;
//                            case "7":
//                                manager.ListAllAttendance();
//                                break;
//                            case "8":
//                                Console.Write("Введите ID студента для добавления оценки: ");
//                                if (int.TryParse(Console.ReadLine(), out int gradeId))
//                                {
//                                    Console.Write("Введите название предмета: ");
//                                    string subjectName = Console.ReadLine();
//                                    Console.Write("Введите оценку: ");
//                                    if (double.TryParse(Console.ReadLine(), out double grade))
//                                    {
//                                        manager.AddGrade(gradeId, subjectName, grade);
//                                    }
//                                    else
//                                    {
//                                        Console.WriteLine("Некорректный ввод оценки.");
//                                    }
//                                }
//                                else
//                                {
//                                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
//                                }
//                                break;
//                            case "9":
//                                Console.Write("Введите ID студента для показа всех предметов и оценок: ");
//                                if (int.TryParse(Console.ReadLine(), out int gradesShowId))
//                                {
//                                    manager.ListAllGrades(gradesShowId);
//                                }
//                                else
//                                {
//                                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
//                                }
//                                break;
//                            case "10":
//                                running = false;
//                                break;
//                            default:
//                                Console.WriteLine("Неверный выбор, попробуйте еще раз.");
//                                break;
//                        }
//                    }
//                }
//                else
//                {
//                    // Меню для ученика
//                    Console.Write("Введите ваш ID: ");
//                    if (int.TryParse(Console.ReadLine(), out int studentId))
//                    {
//                        manager.ListAllGrades(studentId);
//                    }
//                    else
//                    {
//                        Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
//                    }
//                }
//            }
//            else
//            {
//                Console.WriteLine("Ошибка: Неверный логин или пароль.");
//            }
//        }
//    }
//}


