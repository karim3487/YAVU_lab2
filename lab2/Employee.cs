using System;
using System.Collections.Generic;
using System.IO;


namespace c_sharp_lab_2
{
    internal class Program
    {
        //функция создания объекта и добавления животного в очередь с проверкой полей
        public static Queue<Employee> AddEmployee(Queue<Employee> employeesQueue)
        {
            Employee myEmployee = new Employee();
            Console.WriteLine("Введите название животного:");
            myEmployee.AnimalName = Console.ReadLine();

            Console.WriteLine("Введите природную зону:");
            myEmployee.NaturalArea = Console.ReadLine();
            

            Console.WriteLine("Введите затраты на корм в день:");
            if (int.TryParse(Console.ReadLine(), out var checkedNumber))
            {
                myEmployee.FeedCosts = checkedNumber;
            }
            else
            {
                Console.WriteLine("Затраты на корм в день введены неверно, значение установленно по умолчанию: " +
                                  $"{Employee.DEFAULT_FEED_COSTS}");
                myEmployee.FeedCosts = Employee.DEFAULT_FEED_COSTS;
            }

            Console.WriteLine("Введите количество животных определенной природной зоны, находящихся в зоопарке:");
            if (int.TryParse(Console.ReadLine(), out checkedNumber))
            {
                myEmployee.CountAnimal = checkedNumber;
            }
            else
            {
                Console.WriteLine("Количество животных определенной природной зоны, находящихся в зоопарке" +
                                  $" введены неверно, значение установленно по умолчанию: {Employee.DEFAULT_COUNT_ANIMAL}");
                myEmployee.CountAnimal = Employee.DEFAULT_COUNT_ANIMAL;
            }

            Console.WriteLine("Введите сколько денег тратится на содержание определенного животного в месяц:");
            if (int.TryParse(Console.ReadLine(), out checkedNumber))
            {
                myEmployee.MonthFee = checkedNumber;
            }
            else
            {
                Console.WriteLine("Кол-во денег введено неверно, значение задано по умолчанию: " +
                                  Employee.DEFAULT_MONTH_FEE);
                myEmployee.MonthFee = Employee.DEFAULT_MONTH_FEE;
            }

            employeesQueue.Enqueue(myEmployee);
            Console.WriteLine("Животное добавлено в список");
            return employeesQueue;
        }

        public static void Main(string[] args)
        {
            string filename = "animals.bin"; // файл сохраняется в lab2/bin/Debug/net5.0

            Queue<Employee> employeesQueue = new Queue<Employee>();
            Console.WriteLine($"Введите название файла(название файла по умолчанию {filename}):");
            filename=Console.ReadLine();
            int employeesCounter = 0;

            if (File.Exists(filename)) // чтение объектов класса из бинарного файла
            {
                using (BinaryReader binaryReader = new BinaryReader(File.Open(filename, FileMode.OpenOrCreate)))
                {
                    while (binaryReader.PeekChar() != -1)
                    {
                        Employee myEmployee = new Employee();
                        myEmployee.AnimalName = binaryReader.ReadString();
                        myEmployee.NaturalArea = binaryReader.ReadString();
                        myEmployee.FeedCosts = binaryReader.ReadInt32();
                        myEmployee.CountAnimal = binaryReader.ReadInt32();
                        myEmployee.MonthFee = binaryReader.ReadInt32();
                        employeesQueue.Enqueue(myEmployee);
                    }
                }
            }

            string selector = null;

            do
            {
                selector = null;
                Console.WriteLine("М Е Н Ю");
                Console.WriteLine("1> Добавить животное в конец очереди");
                Console.WriteLine("2> Вывести всех животных");
                Console.WriteLine("3> Удаление животного из начала очереди");
                Console.WriteLine("4> Найти животного(-ых), на содержание которого(-ых) уходит больше всего денег");
                Console.WriteLine(
                    "5> Вывести список животных до определенного количества и с определенной природной зоной");
                Console.WriteLine("6> Выполнить поиск по названию животного из полей");
                Console.WriteLine("q> Выход");
                selector = Console.ReadLine();
                switch (selector)
                {
                    case "1":
                    {
                        AddEmployee(employeesQueue);
                        break;
                    }

                    case "2":
                    {
                        employeesCounter = 1;
                        if (employeesQueue.Count != 0)
                        {
                            Console.WriteLine(Employee.BOARD + 
                                              "\n|Номер\t|Название\t|Природная зона\t\t|Затраты на корм (1 день)\t" +
                                              "|Кол-во жив-х опр. пр. зоны\t|Денег на содержание животного в месяц |\n" +
                                              Employee.BOARD);
                            foreach (Employee employees in employeesQueue)
                            {
                                Console.WriteLine($"|{employeesCounter++}\t|{employees}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Список животных пуст");
                        }
                        break;
                    }

                    case "3":
                        if (employeesQueue.Count != 0)
                        {
                            employeesQueue.Dequeue();
                            Console.WriteLine("Животное успешно удалёно из начала списка");
                        }
                        else
                        {
                            Console.WriteLine("Очередь пуста");
                        }

                        break;
                    case "4":
                    {
                        employeesCounter = 0;
                        int maxFee = 0; // создаем переменную чтобы сравнивать с ней затраты на содержание каждого животного
                        if (employeesQueue.Count != 0)
                        {
                            Console.WriteLine("Животные на содержание которых уходит больше всего денег:");
                            Console.WriteLine(Employee.BOARD + 
                                              "\n|Номер\t|Название\t|Природная зона\t\t|Затраты на корм (1 день)\t" +
                                              "|Кол-во жив-х опр. пр. зоны\t|Денег на содержание животного в месяц |\n" +
                                              Employee.BOARD);
                            foreach (Employee employees in employeesQueue)
                            {
                                int checkedFee = employees.MonthFee;
                                if (checkedFee > maxFee) // поиск эивотного на содержание которого уходит больше всего денег
                                {
                                    maxFee = checkedFee;
                                }
                            }
                    
                            foreach (Employee employees in employeesQueue) // вывод информации о самом пожилом сотруднике
                            {
                                employeesCounter++;
                                if (employees.MonthFee == maxFee) 
                                {
                                    Console.WriteLine(employeesCounter + "\t" + employees);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Список Животных пуст");
                        }
                    
                        break;
                    }
                    case "5":
                    {
                        employeesCounter = 0;
                        if (employeesQueue.Count != 0)
                        {
                            Console.WriteLine("Введите, максимальное количество животных:");
                            if (!(int.TryParse(Console.ReadLine(), out var searchingCount) && searchingCount > 0))
                            {
                                searchingCount = 4;
                                Console.WriteLine("Ввод выполнен неверно, задано кол-во по умолчанию: " + searchingCount);
                            }
                    
                            Console.WriteLine("Введите природную зону для поиска: ");
                            string searchingArea = Console.ReadLine();
                    
                            Console.WriteLine(Employee.BOARD + 
                                              "\n|Номер\t|Название\t|Природная зона\t\t|Затраты на корм (1 день)\t" +
                                              "|Кол-во жив-х опр. пр. зоны\t|Денег на содержание животного в месяц |\n" +
                                              Employee.BOARD);
                    
                            bool isFindSomething = false; //нашли ли что-то 
                            foreach (Employee employees in employeesQueue) // вывод информации в зависимости от количества жтвотных и природной зоны
                            {
                                employeesCounter++;
                                if (employees.NaturalArea.Equals(searchingArea) &&
                                    employees.CountAnimal <= searchingCount) //сравнение строк и вычисление возраста и дальнейшее его сравнение с искомым
                                {
                                    Console.WriteLine(employeesCounter + "\t" + employees);
                                    isFindSomething = true;
                                }
                            }
                    
                            if (!isFindSomething)
                            {
                                Console.WriteLine("По Вашему запросу ничего не найдено");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Список животных пуст");
                        }
                    
                        break;
                    }

                    case "6":
                    {
                        Employee searchingEmployee = new Employee();
                        Console.WriteLine("Введите название животного для поиска:");
                        searchingEmployee.AnimalName = Console.ReadLine();//тут создается новое животное, так проще проверить
                        employeesCounter = 0;
                        Console.WriteLine("Поиск животного с названием "+searchingEmployee.AnimalName);
                        if (employeesQueue.Count != 0)
                        {

                            Console.WriteLine(Employee.BOARD + 
                                              "\n|Номер\t|Название\t|Природная зона\t\t|Затраты на корм (1 день)\t" +
                                              "|Кол-во жив-х опр. пр. зоны\t|Денег на содержание животного в месяц |\n" +
                                              Employee.BOARD);

                            bool isFindSomething = false; //нашли ли что-то 
                            foreach (Employee employees in employeesQueue)
                            {
                                employeesCounter++;
                                if (employees.AnimalName.Equals(searchingEmployee.AnimalName))
                                {
                                    Console.WriteLine(employeesCounter + "\t" + employees);
                                    isFindSomething = true;
                                }
                            }

                            if (!isFindSomething)
                            {
                                Console.WriteLine("По Вашему запросу ничего не найдено");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Список животных пуст");
                        }

                        break;
                    }
                    case "q":
                        Console.WriteLine("Завершение работы программы...");
                        break;
                    default:
                        Console.WriteLine("Пункт меню выбран неверно, повторите попытку");
                        break;
                }
            } while (selector != "q");
            

            //запись в динарный файл
            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                foreach (Employee employees in employeesQueue)
                {
                    binaryWriter.Write(employees.AnimalName);
                    binaryWriter.Write(employees.NaturalArea);
                    binaryWriter.Write(employees.FeedCosts);
                    binaryWriter.Write(employees.CountAnimal);
                    binaryWriter.Write(employees.MonthFee);
                }

                Console.WriteLine("Данные успешно записаны в файл");
            }
        }
    }
}
