using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Text;


namespace Zadanie_1
{
    internal class Zadanie_1_A
    {
        static void Main(string[] args)
        {//Инициализация всех данный по умолчанию и их вывод
            int[][] rvanArray = CreateDefaultRvanArray();
            int[,] array2D = CreateDefault2DArray();
            string[] ArrayOfDefaultStrings = {"Привет, проверяющий, какой кофе пьёшь?", "Я. Очень. Устал. Писать. Код!", "Время 4 часа ночи! Сори, что скину так поздно!"};
            string curentstring = CreateDefaultString();
            bool continueRunning = true;
            FillTextAnswer("---------------Ознакомление с Правилами:)---------------");
            Console.WriteLine("Создадим двумерный массив по умолчанию:");
            View2DArray(array2D);
            Console.WriteLine("Создадим рваный массив по умолчанию:");
            ViewRvanArray(rvanArray);
            Console.WriteLine("Создадим несколько строк по умолчанию:");
            ViewList(ArrayOfDefaultStrings);
            Console.WriteLine("Создадим текущюю строку по умолчанию");
            Console.WriteLine("Привет, проверяющий, какой кофе пьёшь?");
            FillTextException("Учтите Максимальный размер матрицы 30x30, для удобства пользования");//Так как больше будет не читабельно
            Console.WriteLine("Для продолжения нажмите любую клавишу");
            Console.ReadKey();
            Console.Clear();
            while (continueRunning)//Создание основного цикла работы
            {
                continueRunning = NextActionMainMenu(ref rvanArray, ref array2D, ref curentstring, ref ArrayOfDefaultStrings);
            }
            Console.WriteLine("Конец работы программы");
        }

        #region Вспомогательные фунции
        static void FillTextException(string message)//Красный шрифт-для ошибок
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void FillTextAnswer(string message)//Зелёный шрифт-для успешно сделанных операций
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static bool CheckOfEmpty2DArray(int[,] array)//Проверка на пустой массив
        {
            return array == null || array.GetLength(0) == 0 || array.GetLength(1) == 0;//Тестируем
        }

        static bool CheckOfEmptyRvanArray(int[][] array)//Проверка на пустой массив
        {
            return array == null || array.Length == 0;//Тестируем
        }

        static int[,] CreateDefault2DArray()//Данные по умолчанию
        {
            return new int[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };
        }

        static int[][] CreateDefaultRvanArray()//Данные по умолчанию
        {
            return
            [
                    [1, 2],
                    [3, 4, 5],
                    [6, 7, 8, 9]
            ];
        }

        static string CreateDefaultString()//Данные по умолчанию
        {
            return "Привет, проверяющий, какой кофе пьёшь?";
        }

        static void ViewList(int[] list)//Функция Показа одномерного списка
        {
            foreach (int elementOfArray in list)//Перебор элементов списка
            {
                Console.Write(elementOfArray + " ");
            }
            Console.WriteLine();//Для вывода следующего текста с новой строки
        }

        static void ViewList(string[] list)//Функция Показа одномерного списка
        {
            int i = 1;
            foreach (string elementOfArray in list)//Перебор элементов списка
            {
                Console.WriteLine($"{i++}){elementOfArray}");
            }
        }

        static int CheckIntBigger0(string text)//функция Проверки числа на целое и больше 0
        {
            int intNumber;//Входные данные(число больше 0)
            do
            {
                try
                {
                    Console.WriteLine(text);//Выводим сообщение о данных, которые надо получить
                    intNumber = int.Parse(Console.ReadLine());//Преобразуем в int
                    if (intNumber < 1)//если число не положительное
                    {
                        throw new ArgumentException();
                    }
                }
                catch (FormatException)//Если введено не целое число
                {
                    FillTextException("Ошибка: введите целое число больше 0.");
                    intNumber = -1;
                }
                catch (ArgumentException)//Если введено не целое число
                {
                    FillTextException("Ошибка(Введено не положительное число): введите целое число больше 0.");
                    intNumber = -1;
                }
                catch (OverflowException)//Если введено число слишком большое или слишком маленькое, выходит за пределы int
                {
                    FillTextException($"Ошибка: Введённое число слишком большое или слишком маленькое." +
                        $"\nВведите число в пределах [{int.MinValue}, {int.MaxValue}]");
                    intNumber = -1;
                }
            } while (intNumber < 1);//Цикл работает пока не получит целое число больше 0
            return intNumber;//Возвращаем значение
        }

        static int CheckIntAny(string text)//Функция Проверки числа на целое
        {
            int intNumber = 0;//Входные данные(число)
            bool isValidInput;//Для проверки корректности ввода целого числа
            do
            {
                try
                {
                    Console.WriteLine(text);//Выводим сообщение о данных, которые надо получить
                    intNumber = int.Parse(Console.ReadLine());//Преобразуем в int
                    isValidInput = true;//Если получилось, то программа присвоит true и вследствие, выйдет из цикла
                }
                catch (FormatException)//Если введено не целое число
                {
                    FillTextException("Ошибка: введите целое число.");
                    isValidInput = false;//Мы попали в блок с ошибкой, тогда формат число не корректен 
                }
                catch (OverflowException)//Если введено число слишком большое или слишком маленькое, выходит за пределы инта
                {
                    FillTextException($"Ошибка: Введённое число слишком большое или слишком маленькое." +
                        $"\nВведите число в пределах [{int.MinValue}, {int.MaxValue}]");
                    isValidInput = false;//Мы попали в блок с ошибкой, тогда формат число не корректен 
                }
            } while (!isValidInput);//Если число целое

            return intNumber;//Возвращаем целое число
        }

        static int[] ListКRandom(int lengthList, int minValueOfElement = -1000, int maxValueOfElement = 1000)//Функция Создания списка(строки) случайно
        {
            int[] list = new int[lengthList];
            Random random = new Random();//Подключаем модуль Random, для заполнения случайными данными
            for (int i = 0; i < lengthList; i++)
            {
                list[i] = random.Next(minValueOfElement, maxValueOfElement + 1);//заполняем элементами
            }
            return list;//Возвращаем этот список
        }

        #endregion
        #region Двумерные
        static int[] ListClava2D(int lengthList)//Функция Создания массива вручную
        {
            int[] list = new int[lengthList];//создаём массив 
            Console.WriteLine("Учтите что все элементы массива целые");
            if (lengthList > 30)
            {
                FillTextException("Ошибка: Введена слишком большая длина массива");
            }
            for (int i = 0; i < lengthList; i++)
            {
                list[i] = CheckIntAny($"Введите {i + 1} элемент строки");//заполняем массив элементами, с клавиатуры 
            }
            ViewList(list);//Выводим массив
            return list;//возвращаем массив для дальнейшей работы
        }

        static void View2DArray(int[,] array2D)//Функция вывода двумерного массива
        {
            for (int i = 0; i < array2D.GetLength(0); i++)
            {
                for (int j = 0; j < array2D.GetLength(1); j++)
                {
                    Console.Write(array2D[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static int[,] Create2DAraayRandom(int strings, int columns, int minValueOfElement, int maxValueOfElement)//Создание двумерного массива случайно
        {
            Random random = new Random();
            int[,] array2D = new int[strings, columns];
            int i, j;
            for (i = 0; i < strings; i++)
            {
                for (j = 0; j < columns; j++)
                {
                    array2D[i, j] = random.Next(minValueOfElement, maxValueOfElement);
                }
            }
            return array2D;
        }
        
        static int[,] Create2DAraayClava(int strings, int columns)//Создание двумерного массива вручную
        {
            int[,] array2D = new int[strings, columns];
            int i, j;
            for (i = 0; i < strings; i++)
            {
                for (j = 0; j < columns; j++)
                {
                    array2D[i, j] = CheckIntAny($"Введите элемент таблицы({i + 1}, {j + 1})");
                }
            }
            return array2D;
        }
        
        static int[,] DeleteStrings2D(int[,] array2D, int k1, int k2)//Удаляем элементы массива
        {
            int strings = array2D.GetLength(0);// Количество строк и столбцов в исходном массиве
            int columns = array2D.GetLength(1);

            int newStrings = strings - (k2 - k1 + 1);// Количество строк в новом массиве
            int[,] newArray2D = new int[newStrings, columns];

            int newString = 0; // Индекс для новой строки
            for (int i = 0; i < strings; i++)
            {
                if (i < k1 || i > k2) // Пропускаем строки из диапазона [k1, k2]
                {
                    for (int j = 0; j < columns; j++)
                    {
                        newArray2D[newString, j] = array2D[i, j];
                    }
                    newString++;
                }
            }

            FillTextAnswer($"Строки с {k1 + 1} по {k2 + 1} успешно удалены.");
            return newArray2D;
        }
        
        static int[,] AddString2DArray(int[,] array2D, int stringIndex, int[] newString)//Добавляем строку в двумерный массив
        {
            if (stringIndex < 0 || stringIndex > array2D.GetLength(0))// Проверяем, что индекс строки корректен
            {
                FillTextException("Ошибка: Указан некорректный номер строки.");
                return array2D;
            }
            int strings = array2D.GetLength(0);// Создаём новый массив с количеством строк на 1 больше
            int columns = array2D.GetLength(1);
            int[,] newArray = new int[strings + 1, columns];// Копируем строки в новый массив с добавлением новой строки
            int newStringIndex = 0;            
            for (int i = 0; i <= strings; i++)
            {
                if (i == stringIndex) // Вставляем новую строку
                {
                    for (int j = 0; j < columns; j++)
                    {
                        newArray[newStringIndex, j] = newString[j];
                    }
                }
                else // Копируем строки из старого массива
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (i < stringIndex)
                        {
                            newArray[newStringIndex, j] = array2D[i, j];
                        }
                        else
                        {
                            newArray[newStringIndex, j] = array2D[i - 1, j];
                        }
                    }
                }
                newStringIndex++;
            }
            return newArray;
        }

        #endregion
        #region Рваные
        static int[] ListClavaRvan(int numberOfString)//Функция Создания рваного массива вручную
        {
            bool corectColumns;
            int lengthList;
            do
            {
                corectColumns = true;
                lengthList = CheckIntBigger0($"Введите длину {numberOfString + 1}-ый строки");//Получаем длину массива
                if (lengthList > 30)
                {
                    FillTextException("Ошибка: Кол-во столбцов слишком большое, введите не больше 30");
                    corectColumns = false;
                }
            } while (!corectColumns);

            int[] list = new int[lengthList];//создаём массив 
            Console.WriteLine("Учтите что все элементы строки целые");
            for (int i = 0; i < lengthList; i++)
            {
                list[i] = CheckIntAny($"Введите {i + 1} элемент строки");//заполняем массив элементами, с клавиатуры 
            }
            ViewList(list);//Выводим массив
            return list;//возвращаем массив для дальнейшей работы
        }

        static void ViewRvanArray(int[][] arrayRvan)//Функция вывода рваного массива
        {
            for (int i = 0; i < arrayRvan.GetLength(0); i++)
            {
                ViewList(arrayRvan[i]);
            }
        }

        static int[][] CreateRvanAraayRandom(int strings, int maxColumns, int minValueOfElement, int maxValueOfElement)//Создаём рваный массив случайно
        {
            Random random = new Random();
            int[][] arrayRvan = new int[strings][];
            int i;

            for (i = 0; i < strings; i++)
            {
                arrayRvan[i] = ListКRandom(random.Next(1, maxColumns + 1), minValueOfElement, maxValueOfElement);
            }
            return arrayRvan;
        }
        
        static int[][] CreateRvanAraayClava(int strings)//Создаём рваный массив вручную
        {
            int[][] arrayRvan = new int[strings][];
            int i;

            for (i = 0; i < strings; i++)
            {
                arrayRvan[i] = ListClavaRvan(i);
            }
            return arrayRvan;
        }
        
        static int[][] DeleteStringsRvan(int[][] arrayRvan, int k1, int k2)
        {
            int strings = arrayRvan.GetLength(0);// Количество строк в исходном массиве
            int newStrings = strings - (k2 - k1 + 1);// Количество строк в новом массиве
            int[][] newArrayRvan = new int[newStrings][];

            int newString = 0; // Индекс для строк нового
            for (int i = 0; i < strings; i++)
            {
                if (i < k1 || i > k2) // Пропускаем строки из диапазона [k1, k2]
                {
                    newArrayRvan[newString] = arrayRvan[i];
                    newString++;
                }
            }
            FillTextAnswer($"Строки с {k1 + 1} по {k2 + 1} успешно удалены");
            return newArrayRvan;
        }
        
        static int[][] AddStringRvanArray(int[][] arrayRvan, int stringIndex, int[] newString)
        {
            if (stringIndex < 0 || stringIndex > arrayRvan.GetLength(0))// Проверяем, что индекс строки корректен
            {
                FillTextException("Ошибка: Указан некорректный номер строки.");
                return arrayRvan;
            }
            int strings = arrayRvan.GetLength(0);
            int[][] newArray = new int[strings + 1][];// Создаём новый массив с количеством строк на 1 больше
            int newStringIndex = 0;
            for (int i = 0; i <= strings; i++)// Копируем строки в новый массив с добавлением новой строки
            {
                if (i == stringIndex)// Вставляем новую строку
                {
                    newArray[newStringIndex] = newString;
                }
                else // Копируем строки из старого массива
                {
                    if (i < stringIndex)
                    {
                        newArray[newStringIndex] = arrayRvan[i];
                    }
                    else
                    {
                        newArray[newStringIndex] = arrayRvan[i - 1];
                    }
                }
                newStringIndex++;
            }
            return newArray;
        }

        #endregion
        #region Строки
        static string ReverceString(string str)//Переворот строки
        {
            string reversedString = new string(str.Reverse().ToArray());
            return reversedString;
        }

        static string CapitalSizeFirstLetter(string word)//Преобразование первых элементов предложения в заглавные
        {

            return char.ToUpper(word[0]) + word.Substring(1);
        }

        static bool CheckCorectLine(string curentString)//Проверка корректности строки
        {
            string psternOsniv = @"[^а-яА-Яa-zA-Z0-9\s.,!?;:]";
            string patternPrep = @"[.,!?;:]{2,}";
            string patternProb = @"[.,!?;: ]{3,}";
            if (Regex.IsMatch(curentString, psternOsniv))//Условие допустимых символов
            {
                Console.Clear();
                FillTextException("В строке могут присутствовать только такие знаки препинания как: (.,!?;:)");
                Console.WriteLine("Повторите ввод:");
                return false;
            }
            if (Regex.IsMatch(curentString, patternPrep))//Условие нескольких знаков препинания подряд
            {
                Console.Clear();
                FillTextException("В строке не могут подряд идти несколько знаков препинания или пробелов в любой комбинации.");
                Console.WriteLine("Повторите ввод:");
                return false;
            }
            if (Regex.IsMatch(curentString, patternProb))//Условие нескольких знаков препинания через пробел
            {
                Console.Clear();
                FillTextException("В строке не могут знаки препинания отделятся от текста пробелами с двух сторон.");
                Console.WriteLine("Повторите ввод:");
                return false;
            }
            if (curentString.Length > 100)//Условие длины строки
            {
                Console.Clear();
                FillTextException("Строка не может, быть длиннее 100 символов");
                Console.WriteLine("Повторите ввод:");
                return false;
            }
            if (!char.IsLetterOrDigit(curentString.TrimStart().FirstOrDefault()))//Проверка первого элемента, без лишних пробелов
            {
                Console.Clear();
                FillTextException("Строка не может начинаться с знаков препинания, пробелов или быть пустой");
                Console.WriteLine("Повторите ввод:");
                return false;
            }
            if (!".!?".Contains(curentString.TrimEnd().LastOrDefault()))//Проверка последнего элемента, без лишних пробелов
            {
                Console.Clear();
                FillTextException("Строка должна заканчиваться одним из следующих символов('!','?','.')");
                Console.WriteLine("Повторите ввод:");
                return false;
            }
            Console.Clear();
            FillTextAnswer($"Строка '{curentString}' успешно введена");
           
            return true;
        }

        static void AddNewDefaultStrings(ref string[] ArrayOfDefaultStrings)//Добавление нового элемента в список по умолчанию
        {
            string curentString;
            Console.WriteLine("Введите строку, которую хотите добавить в набор стандартных строк:");
            do
            {
                curentString = Console.ReadLine();
            } while (!CheckCorectLine(curentString));
            Array.Resize(ref ArrayOfDefaultStrings, ArrayOfDefaultStrings.Length + 1);
            ArrayOfDefaultStrings[ArrayOfDefaultStrings.Length - 1] = curentString;
        }
        static string TransformationOfstring(string curentString)//Преобразование строки
        {
            bool ChekFirstWord = true;
            StringBuilder resultString = new StringBuilder();
            string[] sentences = Regex.Split(curentString, @"(?<=[.!?])");//Делим на предложения
            foreach (string sentence in sentences) 
            {
                bool CheckFirstSentences = true;
                char lastPunctuation = sentence.LastOrDefault();
                string editString = sentence.TrimEnd(lastPunctuation).TrimStart();
                var allStringElements = Regex.Matches(editString, @"\w+|[^\w\s]");//Делим на слова и знаки препинания
                string[] wordsAndSymbols = new string[allStringElements.Count];
                int i = 0;
                foreach(object elements in allStringElements)//Добавляем в список из коллекции
                {
                    wordsAndSymbols[i++] = elements.ToString();
                }
                int countOfPunctuation=0;
                foreach (object elements in wordsAndSymbols)//Смотрим сколько знаков препинания
                {
                    if (Regex.IsMatch(elements.ToString(), @"[,:;]"))
                    {
                        countOfPunctuation++;
                    }  
                }
                string[] words = new string[allStringElements.Count - countOfPunctuation];
                int j = 0;
                foreach (object elements in wordsAndSymbols)//Добавляем в список только слова
                {
                    if (!Regex.IsMatch(elements.ToString(), @"[,:;]"))
                    {
                        words[j++] = elements.ToString();
                    }
                }
                int k = 0;
                foreach (string word in words)//Переворачиваем все слова
                {
                    words[k++] = ReverceString(word);
                }
                Array.Sort(words);
                Array.Reverse(words);

                int wordIndex = 0;
                for (int n = 0; n < wordsAndSymbols.Length; n++)//Собираем предложение обратно с учетом знаков препинания
                {
                    if (Regex.IsMatch(wordsAndSymbols[n], @"^\w+$"))
                    {
                        wordsAndSymbols[n] = words[wordIndex];
                        wordIndex++;
                    }
                }
                string stringItog= "";
                foreach (string elem in wordsAndSymbols)//собираем в строку предложения
                {
                    if (Regex.IsMatch(elem, @"[,:;]"))
                    {
                        stringItog = stringItog + elem;
                    }
                    else
                    {
                        if (CheckFirstSentences)
                        {
                            if (ChekFirstWord)
                            {
                                stringItog = stringItog + CapitalSizeFirstLetter(elem);
                                CheckFirstSentences = false;
                                ChekFirstWord = false;
                                //Изменить регистр
                            }
                            else
                            {
                                stringItog = stringItog + " " + CapitalSizeFirstLetter(elem);
                                CheckFirstSentences = false;
                            }
                        }
                        else
                        {
                            stringItog = stringItog + " " + elem;
                        }
                        
                    }
                }
                stringItog = stringItog + lastPunctuation;//Добавляем конечный знак
                resultString.Append(stringItog);
            }
            return resultString.ToString();//Собираем все предложения
        }

        #endregion
        #region Меню
        static bool NextActionMainMenu(ref int[][] rvanArray, ref int[,] array2D, ref string curentString, ref string[] ArrayOfDefaultStrings)//Функция через которую происходит навигация в текстовом меню
        {
            Console.WriteLine("-----------Работа с главным меню-----------");
            Console.WriteLine("Введите номер функции, которую хотите применить");
            Console.WriteLine("1-Работа с двумерным массивом\n2-Работа с рваным массивом\n3-Работа со строками \nКонец - Закончить работу программы");

            string action = Console.ReadLine();//Приём числа(Навигация в меню)
            Console.Clear();
            switch (action)//Выбор исполняемой функции
            {
                case "1":
                    bool continue2DArray = true;
                    while (continue2DArray)
                    {
                        continue2DArray = NextAction2DArrayMenu(ref array2D);
                    };
                    break;
                case "2":
                    bool continueRvanArray = true;
                    while (continueRvanArray)
                    {
                        continueRvanArray = NextActionRvanArrayMenu(ref rvanArray);
                    };
                    break;
                case "3":
                    bool continueString = true;
                    while (continueString)
                    {
                        continueString = NextActionStringMenu(ref curentString, ref ArrayOfDefaultStrings);
                    };
                    break;
                case "Конец":
                    return false;//Конец работы программы
                default:
                    Console.Clear();
                    FillTextException("Введена неправильная функция, попробуйте ещё раз");//Если введены другие значения
                    break;
            }
            return true;
        }
        
        static bool NextActionStringMenu(ref string currentString, ref string[] ArrayOfDefaultStrings)
        {
            Console.WriteLine("-----------Работа с Строками-----------\n0-Ввести строку\n1-Преобразовать введённую строку\n2-Преобразовать строку из массива по умолчанию\n3-Показать строки по умолчанию" +
                "\n4-Добавить строку в массив по умолчанию\nНажмите '-' для выхода в главное меню");
            Console.WriteLine("Введите номер функции, которую хотите применить к Строке");
            string action = Console.ReadLine();//Приём числа(Навигация в меню)
            switch (action)//Выбор исполняемой функции
            {
                case "0":
                    Console.WriteLine("Введите строку, с которой хотите работать:");
                    do
                    {
                        currentString = Console.ReadLine();
                    } while (!CheckCorectLine(currentString));
                    break;
                case "1":
                    currentString = TransformationOfstring(currentString);
                    Console.Clear();
                    FillTextAnswer("Строка успешно преобразована:");
                    Console.WriteLine(currentString);
                    break;
                case "2":
                    Console.Clear();
                    int NubmerOfString;
                    do
                    {
                        Console.WriteLine("Выберите строку с которой будем работать");
                        ViewList(ArrayOfDefaultStrings);
                        NubmerOfString = CheckIntBigger0("Введите номер строки");
                        if (NubmerOfString > ArrayOfDefaultStrings.Length)
                        {
                            FillTextException($"Введите число не больше {ArrayOfDefaultStrings.Length}");
                        }
                    } while (NubmerOfString > ArrayOfDefaultStrings.Length);
                    currentString = TransformationOfstring(ArrayOfDefaultStrings[NubmerOfString - 1]);
                    FillTextAnswer("Строка успешно преобразована:");
                    Console.WriteLine(currentString);
                    break;
                case "3":
                    Console.Clear();
                    ViewList(ArrayOfDefaultStrings);
                    break;
                case "4":
                    Console.Clear();
                    AddNewDefaultStrings(ref ArrayOfDefaultStrings);
                    break;
                case "-":
                    Console.Clear();
                    return false;//Конец работы программы
                default:
                    Console.Clear();
                    FillTextException("Введена неправильная функция, попробуйте ещё раз");//Если введены другие значения
                    break;
            }
            return true;
        }
        
        static bool NextAction2DArrayMenu(ref int[,] array2D)//Функция через которую происходит навигация в текстовом меню
        {
            Console.WriteLine("-----------Работа с двумерными массивами-----------\n0-Показать двумерный массив\n1-Создать массив вручную\n2-Создать массив рандомно" +
                "\n3-Удалить строки в матрице\n4-Добавить строку с заданным номером c помощью клавиатуры\n5-Добавить строку с заданным номером случайно\nНажмите '-' для выхода в главное меню");
            Console.WriteLine("Введите номер функции, которую хотите применить к двумерному массиву");
            string action = Console.ReadLine();//Приём числа(Навигация в меню)
            int strings, columns;
            bool corectString = true;
            bool corectColumns = true;
            int addStringIndex;
            switch (action)//Выбор исполняемой функции
            {
                case "0":
                    Console.Clear();
                    FillTextAnswer("Вы успешно вывели матрицу:");
                    if (CheckOfEmpty2DArray(array2D))
                    {
                        FillTextException("Массив пуст, попробуйте создать массив");
                        break;
                    }
                    View2DArray(array2D);
                    break;
                case "1":
                    corectString = true;
                    do
                    {
                        corectString = true;
                        strings = CheckIntBigger0("Введите кол-во строк в матрице");
                        if (strings > 30)
                        {
                            FillTextException("Ошибка: Кол-во строк слишком большое, введите не больше 30");
                            corectString = false;
                        }
                    } while (!corectString);
                    corectColumns = true;
                    do
                    {
                        corectColumns = true;
                        columns = CheckIntBigger0("Введите кол-во столбцов в матрице");
                        if (columns > 30)
                        {
                            FillTextException("Ошибка: Кол-во столбцов слишком большое, введите не больше 30");
                            corectColumns = false;
                        }
                    } while (!corectColumns);

                    array2D = Create2DAraayClava(strings, columns);
                    Console.Clear();
                    FillTextAnswer("Массив успешно создан");
                    break;
                case "2":
                    corectString = true;
                    do
                    {
                        corectString = true;
                        strings = CheckIntBigger0("Введите кол-во строк в матрице");
                        if (strings > 30)
                        {
                            FillTextException("Ошибка: Кол-во строк слишком большое, введите не больше 30");
                            corectString = false;
                        }
                    } while (!corectString);
                    corectColumns = true;
                    do
                    {
                        corectColumns = true;
                        columns = CheckIntBigger0("Введите кол-во столбцов в матрице");
                        if (columns > 30)
                        {
                            FillTextException("Ошибка: Кол-во столбцов слишком большое, введите не больше 30");
                            corectColumns = false;
                        }
                    } while (!corectColumns);
                    int minValueOfElement, maxValueOfElement;
                    bool chekingOfcorrect;
                    do
                    {
                        chekingOfcorrect = true;
                        minValueOfElement = CheckIntAny("Введите минимальное значение элементов матрицы");
                        maxValueOfElement = CheckIntAny("Введите максимальное значение элементов матрицы");
                        if (minValueOfElement > maxValueOfElement)
                        {
                            chekingOfcorrect = false;
                            Console.Clear();
                            FillTextException("Ошибка: длина минимальное значение не может быть больше чем максимальное");
                            Console.WriteLine("Попробуйте ещё раз");
                        }
                    } while (!chekingOfcorrect);
                    array2D = Create2DAraayRandom(strings, columns, minValueOfElement, maxValueOfElement);
                    Console.Clear();
                    FillTextAnswer("Массив успешно создан");
                    break;
                case "3":
                    Console.Clear();
                    View2DArray(array2D);
                    chekingOfcorrect = true;
                    int k1, k2;
                    if (CheckOfEmpty2DArray(array2D))
                    {
                        FillTextException("Ошибка: Массив пуст. Удаление строк невозможно.");
                        break;
                    }
                    do
                    {
                        chekingOfcorrect = true;
                        k1 = CheckIntBigger0("Введите номер первой строки для удаления:") - 1;

                        k2 = CheckIntBigger0("Введите номер последней строки для удаления:") - 1;
                        if (k1 >= array2D.GetLength(0) || k2 >= array2D.GetLength(0) || k1 > k2)
                        {
                            chekingOfcorrect = false;
                            FillTextException("Введены не корректны данные попробуйте ещё раз");
                        }
                    } while (!chekingOfcorrect);
                    array2D = DeleteStrings2D(array2D, k1, k2);
                    break;
                case "4":
                    Console.Clear();
                    if (CheckOfEmpty2DArray(array2D))
                    {
                        FillTextException("Для добавления строки, сначала создайте массив");
                        break;
                    }
                    if (CheckOfEmpty2DArray(array2D))
                    {
                        addStringIndex = 0;
                    }
                    else
                    {
                        addStringIndex = CheckIntBigger0("Введите номер строки для добавления:") - 1;
                    }
                    if (addStringIndex > array2D.GetLength(0))
                    {
                        FillTextException($"Введённые номер строки не корректен, так как нельзя добавить несколько строк стразу");
                        Console.WriteLine($"Далее мы вставим введённую вами строку на последнее {array2D.GetLength(0) + 1}-ое место");
                        addStringIndex = array2D.GetLength(0);
                    }
                    if (array2D.GetLength(0) + 1 > 30)
                    {
                        FillTextException("Ошибка: Нельзя добавить больше 30 строк в матрицу");
                        break;
                    }
                    int[] newStringToClava2D = ListClava2D(array2D.GetLength(1));
                    Console.Clear();
                    array2D = AddString2DArray(array2D, addStringIndex, newStringToClava2D);
                    Console.Clear();
                    FillTextAnswer("Новая строка успешно добавлена.");
                    break;
                case "5":
                    Console.Clear();
                    if (CheckOfEmpty2DArray(array2D))
                    {
                        FillTextException("Для добавления строки, сначала создайте массив");
                        break;
                    }
                    if (CheckOfEmpty2DArray(array2D))
                    {
                        addStringIndex = 0;
                    }
                    else
                    {
                        addStringIndex = CheckIntBigger0("Введите номер строки для добавления:") - 1;
                    }
                    if (addStringIndex > array2D.GetLength(0))
                    {
                        FillTextException($"Введённые номер строки не корректен, так как нельзя добавить несколько строк сразу");
                        Console.WriteLine($"Далее мы вставим введённую вами строку на последнее {array2D.GetLength(0) + 1}-ое место");
                        addStringIndex = array2D.GetLength(0);
                    }
                    if (array2D.GetLength(0) + 1 > 30)
                    {
                        FillTextException("Ошибка: Нельзя добавить больше 30 строк в матрицу");
                        break;
                    }
                    int[] newStringToRandom2D = ListКRandom(array2D.GetLength(1));
                    array2D = AddString2DArray(array2D, addStringIndex, newStringToRandom2D);
                    FillTextAnswer("Новая строка успешно добавлена.");
                    break;
                case "-":
                    Console.Clear();
                    return false;//Конец работы меню
                default:
                    Console.Clear();
                    FillTextException("Введена неправильная функция, попробуйте ещё раз");//Если введены другие значения
                    break;
            }
            return true;
        }
        
        static bool NextActionRvanArrayMenu(ref int[][] arrayRvan)//Функция через которую происходит навигация в текстовом меню
        {
            Console.WriteLine("-----------Работа с рваными массивами-----------\n0-Показать рваный массив\n1-Создать массив вручную\n2-Создать массив рандомно" +
                "\n3-Удалить строки в матрице\n4-Добавить строку с заданным номером\n5-Добавить строку с заданным номером случайно\nНажмите '-' для выхода в главное меню");
            Console.WriteLine("Введите номер функции, которую хотите применить к рваному массиву");
            string action = Console.ReadLine();//Приём числа(Навигация в меню)
            int strings, maxColumns;
            bool corectString = true;
            bool corectColumns = true;
            int addStringIndex;
            switch (action)//Выбор исполняемой функции
            {
                case "0":
                    Console.Clear();
                    FillTextAnswer("Вы успешно вывели матрицу:");
                    if (CheckOfEmptyRvanArray(arrayRvan))
                    {
                        FillTextException("Массив пуст, попробуйте создать массив");
                        break;
                    }
                    ViewRvanArray(arrayRvan);
                    break;
                case "1":
                    corectString = true;
                    do
                    {
                        corectString = true;
                        strings = CheckIntBigger0("Введите кол-во строк в матрице");
                        if (strings > 30)
                        {
                            FillTextException("Ошибка: Кол-во строк слишком большое, введите не больше 30");
                            corectString = false;
                        }
                    } while (!corectString);
                    corectColumns = true;
                    arrayRvan = CreateRvanAraayClava(strings);
                    Console.Clear();
                    FillTextAnswer("Массив успешно создан");
                    break;
                case "2":
                    corectString = true;
                    do
                    {
                        corectString = true;
                        strings = CheckIntBigger0("Введите кол-во строк в матрице");
                        if (strings > 30)
                        {
                            FillTextException("Ошибка: Кол-во строк слишком большое, введите меньше 30");
                            corectString = false;
                        }
                    } while (!corectString);
                    corectColumns = true;
                    do
                    {
                        corectColumns = true;
                        maxColumns = CheckIntBigger0("Введите максимальное кол-во столбцов в матрице");
                        if (maxColumns > 30)
                        {
                            FillTextException("Ошибка: Кол-во столбцов слишком большое, введите меньше 30");
                            corectColumns = false;
                        }
                    } while (!corectColumns);
                    int minValueOfElement, maxValueOfElement;
                    bool chekingOfcorrect;
                    do
                    {
                        chekingOfcorrect = true;
                        minValueOfElement = CheckIntAny("Введите минимальное значение элементов матрицы(включительно)");
                        maxValueOfElement = CheckIntAny("Введите максимальное значение элементов матрицы(включительно)");
                        if (minValueOfElement > maxValueOfElement)
                        {
                            chekingOfcorrect = false;
                            Console.Clear();
                            FillTextException("Ошибка: минимальное значение не может быть больше чем максимальное");
                            Console.WriteLine("Попробуйте ещё раз");
                        }
                    } while (!chekingOfcorrect);
                    arrayRvan = CreateRvanAraayRandom(strings, maxColumns, minValueOfElement, maxValueOfElement);
                    Console.Clear();
                    FillTextAnswer("Массив успешно создан");
                    break;
                case "3":
                    Console.Clear();
                    ViewRvanArray(arrayRvan);
                    chekingOfcorrect = true;
                    int k1, k2;
                    if (CheckOfEmptyRvanArray(arrayRvan))
                    {
                        FillTextException("Ошибка: Массив пуст. Удаление строк невозможно.");
                        break;
                    }
                    do
                    {
                        chekingOfcorrect = true;
                        k1 = CheckIntBigger0("Введите номер первой строки для удаления:") - 1;
                        k2 = CheckIntBigger0("Введите номер последней строки для удаления:") - 1;
                        if (k1 >= arrayRvan.GetLength(0) || k2 >= arrayRvan.GetLength(0) || k1 > k2)
                        {
                            chekingOfcorrect = false;
                            FillTextException("Введены не корректны данные попробуйте ещё раз");
                        }
                    } while (!chekingOfcorrect);
                    arrayRvan = DeleteStringsRvan(arrayRvan, k1, k2);
                    break;
                case "4":
                    Console.Clear();
                    if (CheckOfEmptyRvanArray(arrayRvan))
                    {
                        FillTextException("Для добавления строки, сначала создайте массив");
                        break;
                    }
                    if (CheckOfEmptyRvanArray(arrayRvan))
                    {
                        addStringIndex = 0;
                    }
                    else
                    {
                        addStringIndex = CheckIntBigger0("Введите номер строки для добавления:") - 1;
                    }
                    if (addStringIndex > arrayRvan.GetLength(0))
                    {
                        FillTextException($"Введённые номер строки не корректен, так как нельзя добавить несколько строк сразу");
                        Console.WriteLine($"Далее мы вставим введённую вами строку на последнее {arrayRvan.GetLength(0) + 1}-ое место");
                        addStringIndex = arrayRvan.GetLength(0);
                    }
                    if (arrayRvan.GetLength(0) + 1 > 30)
                    {
                        FillTextException("Ошибка: Нельзя добавить больше 30 строк в матрицу");
                        break;
                    }
                    int[] newStringToClavaRvan = ListClavaRvan(addStringIndex);
                    arrayRvan = AddStringRvanArray(arrayRvan, addStringIndex, newStringToClavaRvan);
                    Console.Clear();
                    FillTextAnswer("Новая строка успешно добавлена.");
                    break;
                case "5":
                    Random random = new Random();
                    Console.Clear();
                    if (CheckOfEmptyRvanArray(arrayRvan))
                    {
                        FillTextException("Для добавления строки, сначала создайте массив");
                        break;
                    }
                    if (CheckOfEmptyRvanArray(arrayRvan))
                    {
                        addStringIndex = 0;
                    }
                    else
                    {
                        addStringIndex = CheckIntBigger0("Введите номер строки для добавления:") - 1;
                    }
                    if (addStringIndex > arrayRvan.GetLength(0))
                    {
                        FillTextException($"Введённые номер строки не корректен, так как нельзя добавить несколько строк стразу");
                        Console.WriteLine($"Далее мы вставим введённую вами строку на последнее {arrayRvan.GetLength(0) + 1}-ое место");
                        addStringIndex = arrayRvan.GetLength(0);
                    }
                    if (arrayRvan.GetLength(0) + 1 > 30)
                    {
                        FillTextException("Ошибка: Нельзя добавить больше 30 строк в матрицу");
                        break;
                    }
                    int[] newStringToRandomRvan = ListКRandom(random.Next(1, 31));
                    arrayRvan = AddStringRvanArray(arrayRvan, addStringIndex, newStringToRandomRvan);
                    FillTextAnswer("Новая строка успешно добавлена.");
                    break;
                case "-":
                    Console.Clear();
                    return false;//Конец работы меню
                default:
                    Console.Clear();
                    FillTextException("Введена неправильная функция, попробуйте ещё раз");//Если введены другие значения
                    break;
            }
            return true;
        }
        #endregion 
    }
}
