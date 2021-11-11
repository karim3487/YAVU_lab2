using System;
namespace c_sharp_lab_2
{
    public struct Employee
    {
        private string _animalName;
        private string _naturalArea;
        private int _feedCosts;
        private int _countAnimal;
        private int _monthFee;

        public const string DEFAULT_ANIMAL_NAME = "Лев";
        public const string DEFAULT_NATURAL_AREA = "Саванна";
        public const int DEFAULT_FEED_COSTS = 4000;
        public const int DEFAULT_COUNT_ANIMAL = 3;
        public const int DEFAULT_MONTH_FEE = DEFAULT_FEED_COSTS * 30;
        public const string BOARD = "-----------------------------------------------------------------------------" +
                                    "---------------------------------------------------------------------------";

        /* название животного,
         * природная зона,
         * затраты на корм за один день,
         * количество животных определенной природной зоны, находящихся в зоопарке,
         * сколько денег тратится на содержание определенного животного в месяц.
         */ 
        public Employee(string animalName, string naturalArea, int feedCosts, int countAnimal, int monthFee)
        {
            _animalName = animalName;
            _naturalArea = naturalArea;
            _feedCosts = feedCosts;
            _countAnimal = countAnimal;
            _monthFee = monthFee;
        }

        //проверка фамилии
        public string AnimalName
        {
            get => _animalName;
            set
            {
                _animalName = value;
                string testString = value;
                bool isRightString = true;
                if (testString != "")
                {
                    char convertLetter=Convert.ToChar(testString[0]);
                    if (convertLetter is >= 'А' and <= 'Я' or >= 'A' and <= 'Z') //первая буква должна быть заглавная
                    {
                        for (int i = 1; i < testString.Length; i++)
                        {
                            convertLetter=Convert.ToChar(testString[i]);
                            if (!((convertLetter >= 'a' && convertLetter <= 'z') || (convertLetter >= 'а' &&
                                convertLetter <= 'я'))) //остальные строчные
                            {
                                _animalName = DEFAULT_ANIMAL_NAME;
                                isRightString = false;
                                break;
                            }
                        }
                        if (isRightString)
                        {
                            _animalName = value;
                        }
                    }
                    else
                    {
                        _animalName = DEFAULT_ANIMAL_NAME;
                    }
                }
                else
                {
                    _animalName = DEFAULT_ANIMAL_NAME;
                }
            }
        }

        public string NaturalArea
        {
            get => _naturalArea;
            set
            {
                string testArea = value;
                if(testArea != "")
                    _naturalArea = value;
                else
                {
                    _naturalArea = DEFAULT_NATURAL_AREA;
                }
            }
        }

        public override string ToString()
        {
            return $"{_animalName}\t\t|{_naturalArea}\t\t|{_feedCosts}\t\t\t\t|{_countAnimal}\t\t\t\t|" +
                   $"{_monthFee}\t\t\t\t       |\n" + BOARD;
        }

        public int FeedCosts
        {
            get => _feedCosts;
            set => _feedCosts = value > 0 ? value : DEFAULT_FEED_COSTS;
        }
        public int CountAnimal
        {
            get => _countAnimal;
            set
            {
                if (value is > 1 and < 15)
                {
                    _countAnimal = value;
                }
                else
                {
                    _countAnimal = DEFAULT_COUNT_ANIMAL;
                }
            }
        }
        public int MonthFee
        {
            get => _monthFee;
            set
            {
                if (value > FeedCosts)
                {
                    _monthFee = value;
                }
                else
                {
                    _monthFee = DEFAULT_MONTH_FEE;
                }
            }
        }
    }
}
