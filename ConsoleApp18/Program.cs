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


//ТАНЧИКИ

//using System;
//using System.Collections.Generic;
//using System.Threading;

//public enum Cell
//{
//    Empty,
//    Brick,   // Разрушаемая стена
//    Steel,   // Неразрушаемая стена
//    Base,    // База
//    T1,      // Танк игрока 1
//    T2,      // Танк игрока 2
//    Enemy1,  // Танк противника 1
//    Enemy2,  // Танк противника 2
//    Bullet   // Снаряд
//}

//public class Tank
//{
//    public int X { get; private set; }
//    public int Y { get; private set; }
//    public int Health { get; private set; }
//    public char Symbol { get; private set; }
//    public ConsoleKey MoveUp;
//    public ConsoleKey MoveDown;
//    public ConsoleKey MoveLeft;
//    public ConsoleKey MoveRight;
//    public ConsoleKey Fire;

//    public Tank(int x, int y, char symbol, ConsoleKey up = ConsoleKey.NoName, ConsoleKey down = ConsoleKey.NoName, ConsoleKey left = ConsoleKey.NoName, ConsoleKey right = ConsoleKey.NoName, ConsoleKey fire = ConsoleKey.NoName)
//    {
//        X = x;
//        Y = y;
//        Health = 3;
//        Symbol = symbol;
//        MoveUp = up;
//        MoveDown = down;
//        MoveLeft = left;
//        MoveRight = right;
//        Fire = fire;
//    }

//    public void Move(int dx, int dy)
//    {
//        X += dx;
//        Y += dy;
//    }

//    public void TakeDamage()
//    {
//        Health--;
//    }

//    public bool IsAlive()
//    {
//        return Health > 0;
//    }
//}

//public class Bullet
//{
//    public int X { get; private set; }
//    public int Y { get; private set; }
//    public int DX { get; private set; }
//    public int DY { get; private set; }

//    public Bullet(int x, int y, int dx, int dy)
//    {
//        X = x;
//        Y = y;
//        DX = dx;
//        DY = dy;
//    }

//    public void Move()
//    {
//        X += DX;
//        Y += DY;
//    }
//}

//public class GameField
//{
//    private Cell[,] field;
//    private Tank player1;
//    private Tank player2;
//    private Tank enemy1;
//    private Tank enemy2;
//    private List<Bullet> bullets = new List<Bullet>();
//    private Random random = new Random();

//    public GameField(Tank t1, Tank t2, Tank e1, Tank e2)
//    {
//        player1 = t1;
//        player2 = t2;
//        enemy1 = e1;
//        enemy2 = e2;
//        field = new Cell[13, 13];
//        InitField();
//    }

//    private void InitField()
//    {
//        // Инициализация поля, стен и базы
//        for (int i = 0; i < 13; i++)
//        {
//            for (int j = 0; j < 13; j++)
//            {
//                field[i, j] = Cell.Empty;
//            }
//        }

//        for (int i = 4; i < 9; i++)
//        {
//            field[11, i] = Cell.Brick; // Стены вокруг базы
//        }
//        field[12, 6] = Cell.Base; // База

//        PlaceTank(player1);
//        PlaceTank(player2);
//        PlaceTank(enemy1);
//        PlaceTank(enemy2);
//    }

//    public void UpdateField()
//    {
//        Console.Clear();
//        InitField();
//        PlaceBullets();
//        DisplayField();
//    }

//    private void DisplayField()
//    {
//        for (int i = 0; i < 13; i++)
//        {
//            for (int j = 0; j < 13; j++)
//            {
//                switch (field[i, j])
//                {
//                    case Cell.Empty: Console.Write(". "); break;
//                    case Cell.Brick: Console.Write("# "); break;
//                    case Cell.Steel: Console.Write("O "); break;
//                    case Cell.Base: Console.Write("B "); break;
//                    case Cell.T1: Console.Write("T1"); break;
//                    case Cell.T2: Console.Write("T2"); break;
//                    case Cell.Enemy1: Console.Write("E1"); break;
//                    case Cell.Enemy2: Console.Write("E2"); break;
//                    case Cell.Bullet: Console.Write("* "); break;
//                }
//            }
//            Console.WriteLine();
//        }
//    }

//    public void PlaceTank(Tank tank)
//    {
//        if (tank.IsAlive())
//        {
//            if (tank.Symbol == '1')
//                field[tank.X, tank.Y] = Cell.Enemy1;
//            else if (tank.Symbol == '2')
//                field[tank.X, tank.Y] = Cell.Enemy2;
//            else
//                field[tank.X, tank.Y] = tank.Symbol == '1' ? Cell.T1 : Cell.T2;
//        }
//    }

//    public void AddBullet(Bullet bullet)
//    {
//        bullets.Add(bullet);
//    }

//    public void PlaceBullets()
//    {
//        foreach (var bullet in bullets)
//        {
//            if (bullet.X >= 0 && bullet.X < 13 && bullet.Y >= 0 && bullet.Y < 13)
//            {
//                field[bullet.X, bullet.Y] = Cell.Bullet;
//            }
//        }
//    }

//    public bool CheckHit(Tank tank)
//    {
//        foreach (var bullet in bullets)
//        {
//            if (bullet.X == tank.X && bullet.Y == tank.Y)
//            {
//                tank.TakeDamage();
//                bullets.Remove(bullet);
//                return true;
//            }
//        }
//        return false;
//    }

//    public void MoveBullets()
//    {
//        foreach (var bullet in bullets)
//        {
//            bullet.Move();
//        }
//        bullets.RemoveAll(b => b.X < 0 || b.X >= 13 || b.Y < 0 || b.Y >= 13);
//    }

//    public void CheckCollision()
//    {
//        for (int i = bullets.Count - 1; i >= 0; i--)
//        {
//            Bullet bullet = bullets[i];
//            if (field[bullet.X, bullet.Y] == Cell.Brick)
//            {
//                field[bullet.X, bullet.Y] = Cell.Empty;
//                bullets.RemoveAt(i);
//            }
//            else if (field[bullet.X, bullet.Y] == Cell.Base)
//            {
//                Console.WriteLine("База разрушена! Игра окончена!");
//                Environment.Exit(0);
//            }
//        }
//    }

//    public void MoveEnemy(Tank enemy)
//    {
//        int direction = random.Next(4); // Случайное движение
//        switch (direction)
//        {
//            case 0: if (enemy.X > 0) enemy.Move(-1, 0); break; // Вверх
//            case 1: if (enemy.X < 12) enemy.Move(1, 0); break; // Вниз
//            case 2: if (enemy.Y > 0) enemy.Move(0, -1); break; // Влево
//            case 3: if (enemy.Y < 12) enemy.Move(0, 1); break; // Вправо
//        }

//        // Случайный шанс стрельбы
//        if (random.Next(10) < 2)
//        {
//            int dx = 0, dy = 0;
//            if (direction == 0) dx = -1;
//            if (direction == 1) dx = 1;
//            if (direction == 2) dy = -1;
//            if (direction == 3) dy = 1;
//            Bullet bullet = new Bullet(enemy.X, enemy.Y, dx, dy);
//            AddBullet(bullet);
//        }
//    }
//}

//public class Game
//{
//    private Tank player1;
//    private Tank player2;
//    private Tank enemy1;
//    private Tank enemy2;
//    private GameField field;

//    public Game()
//    {
//        player1 = new Tank(12, 5, '1', ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.D, ConsoleKey.Spacebar);
//        player2 = new Tank(0, 5, '2', ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Enter);
//        enemy1 = new Tank(5, 0, '1');
//        enemy2 = new Tank(5, 12, '2');
//        field = new GameField(player1, player2, enemy1, enemy2);
//    }

//    public void Start()
//    {
//        bool gameRunning = true;
//        while (gameRunning)
//        {
//            field.UpdateField();

//            if (Console.KeyAvailable)
//            {
//                var key = Console.ReadKey(true).Key;
//                MovePlayer(player1, key);
//                MovePlayer(player2, key);
//                FireBullet(player1, key);
//                FireBullet(player2, key);
//            }

//            field.MoveBullets();
//            field.CheckCollision();
//            field.CheckHit(player1);
//            field.CheckHit(player2);
//            field.CheckHit(enemy1);
//            field.CheckHit(enemy2);

//            if (!player1.IsAlive() || !player2.IsAlive() || !enemy1.IsAlive() || !enemy2.IsAlive())
//            {
//                gameRunning = false;
//                Console.WriteLine("Игра окончена!");
//            }

//            field.MoveEnemy(enemy1);
//            field.MoveEnemy(enemy2);

//            Thread.Sleep(500); // Задержка обновления
//        }
//    }

//    private void MovePlayer(Tank player, ConsoleKey key)
//    {
//        if (key == player.MoveUp && player.X > 0) player.Move(-1, 0);
//        if (key == player.MoveDown && player.X < 12) player.Move(1, 0);
//        if (key == player.MoveLeft && player.Y > 0) player.Move(0, -1);
//        if (key == player.MoveRight && player.Y < 12) player.Move(0, 1);
//    }

//    private void FireBullet(Tank player, ConsoleKey key)
//    {
//        if (key == player.Fire)
//        {
//            Bullet bullet = new Bullet(player.X, player.Y, -1, 0); // Стрельба вверх
//            field.AddBullet(bullet);
//        }
//    }
//}

//class Program
//{
//    static void Main(string[] args)
//    {
//        Game game = new Game();
//        game.Start();
//    }
//}

