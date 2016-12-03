using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jagged_array_weather
{
    class Program
    {
        [Flags]
        enum Weather
        {
            hot = 1,
            warm = 2,
            cold = 4,
            cool = 8,
            frosty = 16,

            sunny = 32,
            windy = 64,
            cloudy = 128,
            humid = 256,
            foggy = 512,
            normal = 1024
        }
        static void Main(string[] args)
        {
            const int years = 10;       // кол лет в течении которых введется календарь
            const int firstMonth = 1;
            const int months = 12;
            const int maxFirstYear = 2005;     // максимальый год с которого можно просматривать календарь 


            Weather[][][] array = new Weather[years][][];       // массив погоды в различные дни и месяцы в течении 10 лет 

            Random x = new Random();
            Weather[] tem = new Weather[] { Weather.hot, Weather.warm, Weather.cold, Weather.cool, Weather.frosty };        // массив температур 
            Weather[] con = new Weather[] { Weather.sunny, Weather.cloudy, Weather.foggy, Weather.humid, Weather.windy, Weather.normal };       //массив состояний 

            bool view = true;
            while (view)
            {
                Console.WriteLine("Do you want to see the calendar? Yes(Y), No(N)");
                ConsoleKeyInfo strfirst = Console.ReadKey();
                if (strfirst.Key == ConsoleKey.Y)
                {

                    //Опредлеяем с какого года ввыводить календарь 
                    bool ch = true;
                    int firstYear = 0;       // первый год
                    while (ch)
                    {
                        Console.Clear();
                        Console.Write("Enter year less than {0}:  ", maxFirstYear);
                        string str = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine();

                        bool bl = int.TryParse(str, out firstYear);
                        if (bl == false || (bl && (firstYear > maxFirstYear || firstYear < 0)))
                        {
                            Console.WriteLine("Put year less than {0}!", maxFirstYear);
                            Console.ReadKey();
                        }
                        else
                        {
                            ch = false;
                        }
                    }

                    // Заполняем массив температурой и 2 раза состоянием

                    for (int i = 0; i < years; i++)
                    {
                        array[i] = new Weather[months][];
                        for (int j = 0; j < months; j++)
                        {
                            array[i][j] = new Weather[DateTime.DaysInMonth(i + firstYear + 1, j + 1)];
                            for (int k = 0; k < DateTime.DaysInMonth(i + firstYear + 1, j + 1); k++)
                            {
                                if (j == 1 || j == 0 || j == 11) //winter
                                {

                                    array[i] [j] [k] = tem[x.Next(1, 5)] | con[x.Next(0, 2)] | con[x.Next(2, 6)];
                                }
                                else if ((j >= 2 && j <= 4) || (j >= 8 && j <= 10)) //spring, fall
                                {
                                    array[i][j][k] = tem[x.Next(0, 5)] | con[x.Next(0, 2)] | con[x.Next(2, 6)];
                                }
                                else //summer
                                {
                                    array[i][j][k] = tem[x.Next(0, 4)] | con[x.Next(0, 2)] | con[x.Next(2, 6)];
                                }

                            }
                        }
                    }

                    // Вычисляем год
                    ch = true;
                    int year = 0;
                    int month = 0;
                    while (ch)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter year (from {0} to {1}): ", firstYear, firstYear + years);
                        string str1 = Console.ReadLine();
                        bool bl = Int32.TryParse(str1, out year);
                        if (bl == false || (bl && (year < firstYear || year > firstYear + years)))
                        {
                            Console.WriteLine("You can put year only from {0} to {1}", firstYear, firstYear + years);
                            Console.ReadKey();
                        }
                        else
                        {
                            ch = false;
                        }
                    }

                    // Вычисляем месяц
                    ch = true;
                    while (ch)
                    {
                        Console.Clear();
                        Console.WriteLine("list of months:  ");
                        Console.WriteLine("January (1)");
                        Console.WriteLine("February  (2)");
                        Console.WriteLine("March (3)");
                        Console.WriteLine("April (4)");
                        Console.WriteLine("May (5)");
                        Console.WriteLine("June  (6)");
                        Console.WriteLine("July (7)");
                        Console.WriteLine("August (8)");
                        Console.WriteLine("September  (9)");
                        Console.WriteLine("October (10)");
                        Console.WriteLine("November (11)");
                        Console.WriteLine("December (12)");

                        Console.WriteLine();
                        Console.WriteLine("Put number of month (from {0} to {1}): ", firstMonth, months);
                        string str1 = Console.ReadLine();
                        bool bl = Int32.TryParse(str1, out month);
                        if (bl == false || (bl && (month < firstMonth || month > months)))
                        {
                            Console.WriteLine("Put number of month (from {0} to {1} !!!): ", firstMonth, months);
                            Console.ReadKey();
                        }
                        else
                        {
                            ch = false;
                        }
                    }

                    //Кол разных температур и состояний
                    int foggyDays = 0;
                    int sunnyDays = 0;
                    int cloudyDays = 0;
                    int windyDays = 0;
                    int humidDays = 0;
                    int hotDays = 0;
                    int warmDays = 0;
                    int coolDays = 0;
                    int coldDays = 0;
                    int frostyDays = 0;
                    int normalDays = 0;

                    #region countWeather
                    for (int i = 0; i < DateTime.DaysInMonth(year, month); i++)
                    {
                        if (array[year - firstYear - 1][month - 1][i].HasFlag(Weather.foggy))
                        {
                            foggyDays += 1;
                        }

                        if (array[year - firstYear - 1][month - 1][i].HasFlag(Weather.sunny))
                        {
                            sunnyDays += 1;
                        }

                        if (array[year - firstYear - 1][month - 1][i].HasFlag(Weather.cloudy))
                        {
                            cloudyDays += 1;
                        }

                        if (array[year - firstYear - 1][month - 1][i].HasFlag(Weather.windy))
                        {
                            windyDays += 1;
                        }

                        if (array[year - firstYear - 1][month - 1][i].HasFlag(Weather.humid))
                        {
                            humidDays += 1;
                        }

                        if (array[year - firstYear - 1][month - 1][i].HasFlag(Weather.hot))
                        {
                            hotDays += 1;
                        }

                        if (array[year - firstYear - 1][month - 1][i].HasFlag(Weather.warm))
                        {
                            warmDays += 1;
                        }

                        if (array[year - firstYear - 1][month - 1][i].HasFlag(Weather.cool))
                        {
                            coolDays += 1;
                        }

                        if (array[year - firstYear - 1][month - 1][i].HasFlag(Weather.cold))
                        {
                            coldDays += 1;
                        }

                        if (array[year - firstYear - 1][month - 1][i].HasFlag(Weather.normal))
                        {
                            normalDays += 1;
                        }

                        if (array[year - firstYear - 1][month - 1][i].HasFlag(Weather.frosty))
                        {
                            frostyDays += 1;
                        }

                    }
                    #endregion


                    //Выводим на экран
                    Console.Clear();
                    Console.WriteLine("Year: {0}", year);
                    Console.WriteLine("Month: {0}", month);
                    Console.WriteLine();

                    for (int i = 0; i < DateTime.DaysInMonth(year, month); i++)
                    {
                        Console.WriteLine("{0} Day:  {1}", i + 1, array[year - firstYear - 1][month - 1][i]);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Hot days: {0}", hotDays);
                    Console.WriteLine("Warm days: {0}", warmDays);
                    Console.WriteLine("Cool days: {0}", coolDays);
                    Console.WriteLine("Cold days: {0}", coldDays);
                    Console.WriteLine("Frosty days: {0}", frostyDays);
                    Console.WriteLine("Normal days: {0}", normalDays);
                    Console.WriteLine("Sunny days: {0}", sunnyDays);
                    Console.WriteLine("Cloudy days: {0}", cloudyDays);
                    Console.WriteLine("Foggy days: {0}", foggyDays);
                    Console.WriteLine("Humid days: {0}", humidDays);
                    Console.WriteLine("Windy days: {0}", windyDays);

                }

                else
                {
                    view = false;
                }
            }
            Console.ReadKey();
        }
    }
}
