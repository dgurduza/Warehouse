using System;
using System.IO;

namespace ConsoleApp5
{
    class Program
    {
        /// <summary>
        /// Получение данных об складе из файлов.
        /// </summary>
        /// <param name="warehouse">Склад</param>
        /// <returns>Склад с новыми значениями</returns>
        public static Warehouse CheckFileForWarehouse(ref Warehouse warehouse)
        {
            int tempNum;
            string check;
            do
            {
                Console.WriteLine("Введите полный путь файла.");
                check = Console.ReadLine();
                // check = "D:\\Warehouse.txt";
            } while (!File.Exists(check));
            string result = File.ReadAllText(check);
            string[] info = result.Split(new char[] { ';' });
            for (int i = 0; i < info.Length; i++)
            {
                if (warehouse.NumOfContainers == 0)
                {
                    if (int.TryParse(info[i], out tempNum))
                    {
                        warehouse.NumOfContainers = tempNum;
                    }
                }
                else
                {
                    if (int.TryParse(info[i], out tempNum))
                    {
                        warehouse.Payment = tempNum;
                    }
                }
            }
            warehouse = new Warehouse(warehouse.NumOfContainers, warehouse.Payment);
            // Присваивание полученных из файла значений.
            return warehouse;
        }
        /// <summary>
        /// Получение целочисленных значений из файла.
        /// </summary>
        /// <param name="j">Номер числа в массиве</param>
        /// <returns>Целое число</returns>
        public static int CheckFileForIntNum(int j)
        {
            int tempNum;
            string check;
            do
            {
                Console.WriteLine("Введите полный путь файла.");
                check = Console.ReadLine();
                //check = "D:\\Box.txt";
            } while (!File.Exists(check));
            string result = File.ReadAllText(check);
            string[] info = result.Split(new char[] { ';' });
            if (!int.TryParse(info[j], out tempNum))
            {
                tempNum = 0;
            }
            return tempNum;
        }
        /// <summary>
        /// Получение вещественных значений из файла.
        /// </summary>
        /// <param name="l">Номер числа в массиве</param>
        /// <returns>Вещественное число</returns>
        public static double CheckFileForDoubleNum(int l)
        {
            double doubNum;
            string check;
            do
            {
                Console.WriteLine("Введите полный путь файла.");
                check = Console.ReadLine();
                //check = "D:\\Box.txt";
            } while (!File.Exists(check));
            // Проверка существования файла.
            string result = File.ReadAllText(check);
            string[] info = result.Split(new char[] { ';' });
            if (!double.TryParse(info[l], out doubNum))
            {
                doubNum = 0;
            }
            return doubNum;
        }
        /// <summary>
        /// Получение данных об контейнере из файла.
        /// </summary>
        /// <param name="i">Номер контейнера</param>
        /// <returns>Размер контейнера</returns>
        public static int CheckFileForContainer(int i)
        {
            int tempNum1;
            string check;
            do
            {
                Console.WriteLine("Введите полный путь файла.");
                check = Console.ReadLine();
                //check = "D:\\Container.txt";
            } while (!File.Exists(check));
            string result = File.ReadAllText(check);
            // Получение данных из файла.
            string[] info = result.Split(new char[] { ';' });
            if (!int.TryParse(info[i], out tempNum1))
            {
                tempNum1 = 0;
            }
            return tempNum1;
        }
        /// <summary>
        /// Проверка на ввод целых чисел.
        /// </summary>
        /// <returns>Целое число</returns>
        public static int CheckingOfIntNums()
        {
            int number;
            while (!int.TryParse(Console.ReadLine(), out number) || number < 1)
            {
                Console.WriteLine("Некорректный ввод. Введите число снова");
            }
            return number;
        }
        /// <summary>
        /// Проверка на ввод из консоли вещественных чисел.
        /// </summary>
        /// <returns>Вещественное число</returns>
        public static double CheckingOfDoubleNums()
        {
            double number;
            while (!double.TryParse(Console.ReadLine(), out number) || number < 1)
            {
                Console.WriteLine("Некорректный ввод. Введите число снова");
            }
            return number;
        }
        /// <summary>
        /// Заполнение склада контейнерами. 
        /// Все данные заполняются с консоли.
        /// </summary>
        /// <param name="warehouse">Склад</param>
        /// <param name="sizeOfWh">Размер склада</param>
        /// <param name="paymentForContent">Плата за аренду</param>
        /// <returns></returns>
        public static Warehouse GetWarehouse(out Warehouse warehouse, int sizeOfWh, int paymentForContent)
        {
            warehouse = new Warehouse(sizeOfWh, paymentForContent);
            for (int i = 0; i < sizeOfWh; i++)
            {
                warehouse.NewContainers[i] = GetNewContainer();
                double temp = warehouse.NewContainers[i].GetSumAfterDamage();
                if (paymentForContent > temp)
                {
                    Console.WriteLine(Environment.NewLine + $"Контейнер №{i + 1} не рентабелен, поэтому он не будет добавлен на склад. Плата за хранение больше " +
                        $"стоимости содержимого контейнера ({paymentForContent} > {temp:F2})");
                    warehouse.NewContainers[i] = null;
                }
                else
                {
                    Console.WriteLine($"Контейнер №{i + 1} добавлен на склад. Цена за аренду меньше либо равна стоимости содержимого контейнера({paymentForContent} < {temp:F2})."
                        + Environment.NewLine);
                }
            }
            warehouse.EmptyPlace = CheckWarehouse(warehouse);
            return warehouse;
        }
        /// <summary>
        /// Получение контейнера заполненого ящиками.
        /// </summary>
        /// <returns>Контейнер</returns>
        public static Container GetNewContainer()
        {
            Console.WriteLine("                                            Создание контейнера на складе");
            double sumOfWeight = 0;
            double sumOfPrice = 0;
            Console.WriteLine("Введи количество ящиков:");
            int numOfBoxes = CheckingOfIntNums();
            Random rand = new Random();
            int maxWeight = rand.Next(50, 1001);
            Console.WriteLine(Environment.NewLine + $"Максимально допустимая масса: {maxWeight}");
            Container container = new Container(numOfBoxes);
            for (int i = 0; i < numOfBoxes; i++)
            {
                container.NewBoxes[i] = GetNewBox();
                sumOfWeight += container.NewBoxes[i].AddWeightOfBox();
                if (maxWeight < sumOfWeight)
                // Проверка соответствия массы ящиков массе контейнера.
                {
                    Console.WriteLine($"Mасса превышает допустимую ({sumOfWeight:F2} > {maxWeight}). Ящик № {i + 1} не будет добавлен в контейнер." + Environment.NewLine);
                    sumOfWeight -= container.NewBoxes[i].AddWeightOfBox();
                    container.NewBoxes[i] = null;
                }
                else
                {
                    sumOfPrice += container.NewBoxes[i].AddPriceOfBox();
                    Console.WriteLine($"Ящик №{i + 1} добавлен в контейнер. Максимально допустимая масса больше либо равна суммарной массе ящиков ({maxWeight} > {sumOfWeight:F2})" + Environment.NewLine);
                }
                if (maxWeight == sumOfWeight)
                {
                    Console.WriteLine("Добавление больше невозможно так как масса добавленных ящиков равна максимально допустимой");
                    break;
                }
            }
            container.SumOfPrices = sumOfPrice;
            return container;
        }
        /// <summary>
        /// Получение нового ящика заполнением из консоли.
        /// </summary>
        /// <returns>Ящик</returns>
        public static Box GetNewBox()
        {
            Console.WriteLine(Environment.NewLine + "                                           Добавления ящика в контейнер:");
            Console.WriteLine("1)Введи массу ящика");
            double weight = CheckingOfDoubleNums();
            Console.WriteLine("2)Введи размер платы за один киллограмм");
            int payment = CheckingOfIntNums();
            Box newBox = new Box(weight, payment);
            // Получение ящика соответствующего введенным значениям.
            return newBox;
        }
        /// <summary>
        /// Проверка склада на пустые ячейки.
        /// </summary>
        /// <param name="warehouse">Склад</param>
        /// <returns>Количество пустых ячеек на складе</returns>
        public static int CheckWarehouse(Warehouse warehouse)
        {
            int count = 0;
            for (int i = 0; i < warehouse.NumOfContainers; i++)
            {
                if (warehouse.NewContainers[i] == null)
                {
                    count++;
                }
            }
            Console.WriteLine(Environment.NewLine + $"Количество свободных мест на складе:{count}");
            warehouse.EmptyPlace = count;
            return count;
        }
        /// <summary>
        /// Добавление контейнера на склад.
        /// </summary>
        /// <param name="warehouse">Склад</param>
        /// <returns>Измененный склад</returns>
        public static Warehouse AddingANewContainer(Warehouse warehouse)
        {
            int quantity = CheckWarehouse(warehouse);
            // Получение данных об пустых ячейках на складе.
            double temp;
            int placeOfNull = 0;
            if (quantity == 0)
            {
                Console.WriteLine("Создадим новый контейнер на месте первого контейнера");
                do
                {
                    warehouse.NewContainers[0] = GetNewContainer();
                    temp = warehouse.NewContainers[0].GetSumAfterDamage();
                    if (warehouse.Payment >= temp)
                    {
                        Console.WriteLine($"Контейнер не рентабелен ({warehouse.Payment} > {temp:F2}). Создайте его еще раз");
                    }
                } while (warehouse.Payment > temp);
            }
            else
            {
                Console.WriteLine("Создадим новый контейнер в пустой ячейке.");
                for (int i = 0; i < warehouse.NumOfContainers; i++)
                {
                    if (warehouse.NewContainers[i] == null)
                    {
                        placeOfNull = i;
                    }
                }
                do
                {
                    warehouse.NewContainers[placeOfNull] = GetNewContainer();
                    temp = warehouse.NewContainers[placeOfNull].GetSumAfterDamage();
                    if (warehouse.Payment >= temp)
                    {
                        Console.WriteLine($"Контейнер не рентабелен ({warehouse.Payment} > {temp:F2}). Создайте его еще раз");
                    }
                } while (warehouse.Payment > temp);
            }
            return warehouse;
        }
        /// <summary>
        /// Метод по удалению содержимого склада.
        /// </summary>
        /// <param name="warehouse">Склад</param>
        /// <returns>Измененный склад</returns>
        public static Warehouse DeletingAContainer(Warehouse warehouse)
        {
            Console.WriteLine("Введите номер контейнера, который хотите удалить");
            int num;
            while (!int.TryParse(Console.ReadLine(), out num) || num < 1 || num > warehouse.NumOfContainers)
            // Проверка существования контейнера, номер которого ввел пользователь.
            {
                Console.WriteLine("Некорректный ввод. Введите номер снова");
            }
            if (warehouse.NewContainers[num - 1] == null)
            {
                throw new Exception("Данная ячейка на складе пуста. Удаление невозможно");
            }
            else
            {
                warehouse.NewContainers[num - 1] = null;
            }
            return warehouse;
        }
        /// <summary>
        /// Вывод правил работы с программой в консоль.
        /// </summary>
        public static void Rules()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("                                                      Привет!" + Environment.NewLine +
                    "Давай создадим овощной склад. Он будет единственным в программе и изменить его во время работы программы невозможно."
                    + Environment.NewLine + "На складе будут храниться контейнеры. Их количество ты определяешь сам. Но не все контейнеры запишутся на склад."
                    + Environment.NewLine + "Если суммарная стоимость содержимого контейнера меньше взимаемой арендной платы за хранение(Цена аренды вводится с консоли), то контейнер не рентабелен."
                    + Environment.NewLine + "И он не будет добавлен на склад. Содержимым контейнера является ящик. Если его масса больше допустимой массы контейнера"
                    + Environment.NewLine + "(которая будет выбрана среди случайных чисел),или сумма масс ящиков больше чем допустимая масса контейнера, то последний ящик не будет добавлен."
                    + Environment.NewLine + "Если ты выберешь ввод из консоли, то тебе придется вводить все параметры с консоли давай расскажу какие значения " +
                    "вещественные,а какие целочисленные." + Environment.NewLine + "Создать склад размера 0 невозможно."
                    + Environment.NewLine + "                                             Целочисленные переменные:"
                    + Environment.NewLine + "1) Размер склада"
                    + Environment.NewLine + "2) Плата за аренду"
                    + Environment.NewLine + "3) Количество ящиков в контейнере"
                    + Environment.NewLine + "4) Цена 1 килограмма содержимого ящика"
                    + Environment.NewLine + "                                              Вещественные переменные:"
                    + Environment.NewLine + "1) Масса ящика"
                    + Environment.NewLine + "                   Для работы с файлами создайте три файла и расположите переменные через запятые."
                    + Environment.NewLine + "1) В файле, который описывает Склад должно быть две переменные которые описывают его размер и плату за аренду.Например: 2;13"
                    + Environment.NewLine + "2) В файле, который описывает Контейнер должно быть описание каждого контейнера.То есть одна переменная на контейнер(его размер). Например 1;2"
                    + Environment.NewLine + "3) В файле, который описывает Ящики должно быть по две переменные, которые описывают каждый ящик в каждом контейнере.Например: 1;2;3;4;5;6"
                    + Environment.NewLine + "Размер 2 контейнера. Плата 13 пунктов. Размер 1 контейнера один ящик, размер второго два ящика."
                    + Environment.NewLine + "Иначе переменным по умолчанию будут присвоены нули(Так как масса вещественное число, то указывать в файле нужно через запятую)"
                    + Environment.NewLine + "В приложенных файлах будет пример ввода. Совет: В работе с файлами раскоментить путь к файлам и изменить его для своего пк. Чтобы было проще."
                    + Environment.NewLine + "Примечание: Так как урон контейнера сильно округляется, при вашем пересчете могут возникнуть разногласия с выведенным числом." + Environment.NewLine + "Оно будет похоже с тем, что на консоли, но не совпадать из-за точности вычисления компьютера."
                    + Environment.NewLine + "Для начала работы нажмите \"Enter\"");
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        }
        /// <summary>
        /// Получение информации о складе.
        /// </summary>
        /// <param name="warehouse">Склад</param>
        public static void GetInfo(Warehouse warehouse)
        {
            Console.WriteLine(Environment.NewLine + warehouse + Environment.NewLine);
            for (int i = 0; i < warehouse.NumOfContainers; i++)
            {
                Console.WriteLine(warehouse.NewContainers[i]);
                // Вывод информации об контейнере.
            }
        }
        /// <summary>
        /// Метод для заполнения склада, используя данные из файла.
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public static Warehouse GetWarehouseFromFile(Warehouse warehouse)
        {
            for (int i = 0; i < warehouse.NumOfContainers; i++)
            {
                warehouse.NewContainers[i] = GetNewContainerFromFile(i);
                double temp = warehouse.NewContainers[i].GetSumAfterDamage();
                if (warehouse.Payment > temp)
                // Проверка соответствия условию хранения контейнера.
                {
                    Console.WriteLine(Environment.NewLine + $"Контейнер №{i + 1} не рентабелен, поэтому он не будет добавлен на склад. Плата за хранение больше " +
                        $"стоимости содержимого контейнера ({warehouse.Payment} > {temp:F2})");
                    warehouse.NewContainers[i] = null;
                }
                else
                {
                    Console.WriteLine($"Контейнер №{i + 1} добавлен на склад. Цена за аренду меньше либо равна стоимости содержимого контейнера" +
                        $"({warehouse.Payment} < {temp:F2})." + Environment.NewLine);
                }
            }
            return warehouse;
        }
        /// <summary>
        /// Метод для получения контейнера,  используя данные из файла.
        /// </summary>
        /// <param name="k">Номер контейнера</param>
        /// <returns>Контейнер с ящиками</returns>
        public static Container GetNewContainerFromFile(int k)
        {
            Console.WriteLine("                                            Создание контейнера на складе");
            double sumOfWeight = 0;
            double sumOfPrice = 0;
            int numOfBoxes = CheckFileForContainer(k);
            Random rand = new Random();
            int maxWeight = rand.Next(50, 1001);
            // Получение максимально допустимой массы контейнера.
            Console.WriteLine(Environment.NewLine + $"Максимально допустимая масса: {maxWeight}");
            Container container = new Container(numOfBoxes);
            for (int i = 0; i < numOfBoxes; i++)
            {
                container.NewBoxes[i] = GetNewBoxFromFile(i, numOfBoxes);
                // Получение ящиков.
                sumOfWeight += container.NewBoxes[i].AddWeightOfBox();
                if (maxWeight < sumOfWeight)
                {
                    Console.WriteLine($"Mасса превышает допустимую ({sumOfWeight:F2} > {maxWeight}). Ящик № {i + 1} не будет добавлен в контейнер." + Environment.NewLine);
                    sumOfWeight -= container.NewBoxes[i].AddWeightOfBox();
                    container.NewBoxes[i] = null;
                }
                else
                {
                    sumOfPrice += container.NewBoxes[i].AddPriceOfBox();
                    // Получение суммы цен содержимого с учетом добавления ящика в контейнер.
                    Console.WriteLine($"Ящик №{i + 1} добавлен в контейнер. Максимально допустимая масса больше либо равна суммарной массе ящиков ({maxWeight} > {sumOfWeight:F2})" + Environment.NewLine);
                }
                if (maxWeight == sumOfWeight)
                {
                    Console.WriteLine("Добавление больше невозможно так как масса добавленных ящиков равна максимально допустимой");
                    break;
                }
            }
            container.SumOfPrices = sumOfPrice;
            return container;
        }
        /// <summary>
        /// Метод для получения ящика, используя данные из файла.
        /// </summary>
        /// <param name="i">Номер ящика</param>
        /// <returns>Ящик с данными</returns>
        public static Box GetNewBoxFromFile(int i, int numOfBoxes)
        {
            Console.WriteLine(Environment.NewLine + "                                           Добавления ящика в контейнер:");
            Console.WriteLine("1)Введи массу ящика");
            double weight = CheckFileForDoubleNum(i + numOfBoxes);
            int payment = CheckFileForIntNum(i);
            Box newBox = new Box(weight, payment);
            // Создание ящика и присваивание ему значений полученых из файла.
            return newBox;
        }
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                try
                {
                    Warehouse warehouse = new Warehouse(0, 0);
                    int count;
                    Rules();
                    Console.Clear();
                    Console.WriteLine("Для ввода данных с консоли нажми - 1" + Environment.NewLine +
                        "Для получения данных с файла нажмите - 2");
                    Console.WriteLine("Введи номер операции:");
                    while (!int.TryParse(Console.ReadLine(), out count) || count < 1 || count > 2)
                    {
                        Console.WriteLine("Введи номер еще раз.");
                    }
                    if (count == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("                                                  Создание склада:" + Environment.NewLine +
                            "1)Введи размер склада(максимальное количество контейнеров)");
                        int sizeOfWh = CheckingOfIntNums();
                        Console.WriteLine("2)Введи размер платы за аренду");
                        int paymentForContent = CheckingOfIntNums();
                        warehouse = GetWarehouse(out warehouse, sizeOfWh, paymentForContent);
                    }
                    if (count == 2)
                    {
                        CheckFileForWarehouse(ref warehouse);
                        warehouse = GetWarehouseFromFile(warehouse);
                    }
                    do
                    {
                        try
                        {
                            int result;
                            Console.WriteLine(Environment.NewLine + "Введи номер операции" + Environment.NewLine + "1 - Добавление контейнера на склад"
                                + Environment.NewLine + "2 - Удаление контейнера со склада" + Environment.NewLine +
                                "3 - Просмотр информации о складе и его содержимом" + Environment.NewLine +
                                "4 - Просмотр правил работы с программой" + Environment.NewLine);
                            while (!int.TryParse(Console.ReadLine(), out result) || result < 1 || result > 4)
                            {
                                Console.WriteLine("Введите номер операции еще раз");
                            }
                            if (result == 1)
                            {
                                Console.Clear();
                                AddingANewContainer(warehouse);
                                CheckWarehouse(warehouse);
                                GetInfo(warehouse); ;
                                // Для актуализации данных о складе сначала используется метод "CheckWarehouse", а потом "GetInfo".
                                // В остальных случаях ниже так сделано по тем же причинам.
                            }
                            if (result == 2)
                            {
                                Console.Clear();
                                CheckWarehouse(warehouse);
                                GetInfo(warehouse);
                                DeletingAContainer(warehouse);
                            }
                            if (result == 3)
                            {
                                Console.Clear();
                                CheckWarehouse(warehouse);
                                GetInfo(warehouse);
                            }
                            if (result == 4)
                            {
                                Console.Clear();
                                Rules();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        Console.WriteLine("Для окончания работы со складом нажмите Escape. Для продолжения нажмите любую клавишу.");
                    } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                Console.WriteLine(Environment.NewLine + "Для окончания работы с программой нажмите Escape еще раз. Для продолжения нажмите любую клавишу.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
