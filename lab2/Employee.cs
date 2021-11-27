using System;
using System.Collections.Generic;
using System.IO;


namespace c_sharp_lab_2
{
    internal class Program
    {
        //функция создания объекта и добавления животного в очередь с проверкой полей
        public static Queue<Animal> AddAnimal(Queue<Animal> animalsQueue)
        {
            Animal myAnimal = new Animal();
            Console.WriteLine("Введите название животного:");
            myAnimal.AnimalName = Console.ReadLine();

            Console.WriteLine("Введите природную зону:");
            myAnimal.NaturalArea = Console.ReadLine();
            

            Console.WriteLine("Введите затраты на корм в день:");
            if (int.TryParse(Console.ReadLine(), out var checkedNumber))
            {
                myAnimal.FeedCosts = checkedNumber;
            }
            else
            {
                Console.WriteLine("Затраты на корм в день введены неверно, значение установленно по умолчанию: " +
                                  $"{Animal.DEFAULT_FEED_COSTS}");
                myAnimal.FeedCosts = Animal.DEFAULT_FEED_COSTS;
            }

            Console.WriteLine("Введите количество животных определенной природной зоны, находящихся в зоопарке:");
            if (int.TryParse(Console.ReadLine(), out checkedNumber))
            {
                myAnimal.CountAnimal = checkedNumber;
            }
            else
            {
                Console.WriteLine("Количество животных определенной природной зоны, находящихся в зоопарке" +
                                  $" введены неверно, значение установленно по умолчанию: {Animal.DEFAULT_COUNT_ANIMAL}");
                myAnimal.CountAnimal = Animal.DEFAULT_COUNT_ANIMAL;
            }

            Console.WriteLine("Введите сколько денег тратится на содержание определенного животного в месяц:");
            if (int.TryParse(Console.ReadLine(), out checkedNumber))
            {
                myAnimal.MonthFee = checkedNumber;
            }
            else
            {
                Console.WriteLine("Кол-во денег введено неверно, значение задано по умолчанию: " +
                                  Animal.DEFAULT_MONTH_FEE);
                myAnimal.MonthFee = Animal.DEFAULT_MONTH_FEE;
            }

            animalsQueue.Enqueue(myAnimal);
            Console.WriteLine("Животное добавлено в список");
            return animalsQueue;
        }

        public static void Main(string[] args)
        {
            string filename = "animals.bin"; // файл сохраняется в lab2/bin/Debug/net5.0

            Queue<Animal> animalsQueue = new Queue<Animal>();
            Console.WriteLine($"Введите название файла(название файла по умолчанию {filename}):");
            filename=Console.ReadLine();
            int animalsCounter = 0;

            if (File.Exists(filename)) // чтение объектов класса из бинарного файла
            {
                using (BinaryReader binaryReader = new BinaryReader(File.Open(filename, FileMode.OpenOrCreate)))
                {
                    while (binaryReader.PeekChar() != -1)
                    {
                        Animal myAnimal = new Animal();
                        myAnimal.AnimalName = binaryReader.ReadString();
                        myAnimal.NaturalArea = binaryReader.ReadString();
                        myAnimal.FeedCosts = binaryReader.ReadInt32();
                        myAnimal.CountAnimal = binaryReader.ReadInt32();
                        myAnimal.MonthFee = binaryReader.ReadInt32();
                        animalsQueue.Enqueue(myAnimal);
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
                        AddAnimal(animalsQueue);
                        break;
                    }

                    case "2":
                    {
                        animalsCounter = 1;
                        if (animalsQueue.Count != 0)
                        {
                            Console.WriteLine(Animal.BOARD + 
                                              "\n|Номер\t|Название\t|Природная зона\t\t|Затраты на корм (1 день)\t" +
                                              "|Кол-во жив-х опр. пр. зоны\t|Денег на содержание животного в месяц |\n" +
                                              Animal.BOARD);
                            foreach (Animal animal in animalsQueue)
                            {
                                Console.WriteLine($"|{animalsCounter++}\t|{animal}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Список животных пуст");
                        }
                        break;
                    }

                    case "3":
                        if (animalsQueue.Count != 0)
                        {
                            animalsQueue.Dequeue();
                            Console.WriteLine("Животное успешно удалёно из начала списка");
                        }
                        else
                        {
                            Console.WriteLine("Очередь пуста");
                        }

                        break;
                    case "4":
                    {
                        animalsCounter = 0;
                        int maxFee = 0; // создаем переменную чтобы сравнивать с ней затраты на содержание каждого животного
                        if (animalsQueue.Count != 0)
                        {
                            Console.WriteLine("Животные на содержание которых уходит больше всего денег:");
                            Console.WriteLine(Animal.BOARD + 
                                              "\n|Номер\t|Название\t|Природная зона\t\t|Затраты на корм (1 день)\t" +
                                              "|Кол-во жив-х опр. пр. зоны\t|Денег на содержание животного в месяц |\n" +
                                              Animal.BOARD);
                            foreach (Animal animal in animalsQueue)
                            {
                                int checkedFee = animal.MonthFee;
                                if (checkedFee > maxFee) // поиск эивотного на содержание которого уходит больше всего денег
                                {
                                    maxFee = checkedFee;
                                }
                            }
                    
                            foreach (Animal animal in animalsQueue) // вывод информации о самом пожилом сотруднике
                            {
                                animalsCounter++;
                                if (animal.MonthFee == maxFee) 
                                {
                                    Console.WriteLine(animalsCounter + "\t" + animal);
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
                        animalsCounter = 0;
                        if (animalsQueue.Count != 0)
                        {
                            Console.WriteLine("Введите, максимальное количество животных:");
                            if (!(int.TryParse(Console.ReadLine(), out var searchingCount) && searchingCount > 0))
                            {
                                searchingCount = 4;
                                Console.WriteLine("Ввод выполнен неверно, задано кол-во по умолчанию: " + searchingCount);
                            }
                    
                            Console.WriteLine("Введите природную зону для поиска: ");
                            string searchingArea = Console.ReadLine();
                    
                            Console.WriteLine(Animal.BOARD + 
                                              "\n|Номер\t|Название\t|Природная зона\t\t|Затраты на корм (1 день)\t" +
                                              "|Кол-во жив-х опр. пр. зоны\t|Денег на содержание животного в месяц |\n" +
                                              Animal.BOARD);
                    
                            bool isFindSomething = false; //нашли ли что-то 
                            foreach (Animal animal in animalsQueue) // вывод информации в зависимости от количества жтвотных и природной зоны
                            {
                                animalsCounter++;
                                if (animal.NaturalArea.Equals(searchingArea) &&
                                    animal.CountAnimal <= searchingCount) //сравнение строк и вычисление возраста и дальнейшее его сравнение с искомым
                                {
                                    Console.WriteLine(animalsCounter + "\t" + animal);
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
                        Animal searchingAnimal = new Animal();
                        Console.WriteLine("Введите название животного для поиска:");
                        searchingAnimal.AnimalName = Console.ReadLine();//тут создается новое животное, так проще проверить
                        animalsCounter = 0;
                        Console.WriteLine("Поиск животного с названием "+searchingAnimal.AnimalName);
                        if (animalsQueue.Count != 0)
                        {

                            Console.WriteLine(Animal.BOARD + 
                                              "\n|Номер\t|Название\t|Природная зона\t\t|Затраты на корм (1 день)\t" +
                                              "|Кол-во жив-х опр. пр. зоны\t|Денег на содержание животного в месяц |\n" +
                                              Animal.BOARD);

                            bool isFindSomething = false; //нашли ли что-то 
                            foreach (Animal animal in animalsQueue)
                            {
                                animalsCounter++;
                                if (animal.AnimalName.Equals(searchingAnimal.AnimalName))
                                {
                                    Console.WriteLine(animalsCounter + "\t" + animal);
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
                foreach (Animal animal in animalsQueue)
                {
                    binaryWriter.Write(animal.AnimalName);
                    binaryWriter.Write(animal.NaturalArea);
                    binaryWriter.Write(animal.FeedCosts);
                    binaryWriter.Write(animal.CountAnimal);
                    binaryWriter.Write(animal.MonthFee);
                }

                Console.WriteLine("Данные успешно записаны в файл");
            }
        }
    }
}
