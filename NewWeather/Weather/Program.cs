using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    class Program
    {
        const int years = 10;       // кол лет в течении которых введется календарь
        const int firstMonth = 1;
        const int months = 12;
        const int days = 31;
        const int maxFirstYear = 2005;     // максимальый год с которого можно просматривать календарь 

        // массив температур 
        static Weather[] tem = new Weather[] { Weather.hot, Weather.warm, Weather.cold, Weather.cool, Weather.frosty };

        //массив состояний 
        static Weather[] con = new Weather[] { Weather.sunny, Weather.cloudy, Weather.foggy, Weather.humid, Weather.windy, Weather.normal };

        static Random x = new Random();

        static Weather Rand(int month)
        {
            Weather w = Weather.Empty;
            if (month == 1 || month == 0 || month == 11) //winter
            {
                w = tem[x.Next(1, 5)] | con[x.Next(0, 2)] | con[x.Next(2, 6)];
            }
            if ((month >= 2 && month <= 4) || (month >= 8 && month <= 10)) //spring, fall
            {
                w = tem[x.Next(0, 5)] | con[x.Next(0, 2)] | con[x.Next(2, 6)]; //spring, fall

            }
            if (month >= 5 && month <= 7) //summer
            {
                w = tem[x.Next(0, 4)] | con[x.Next(0, 2)] | con[x.Next(2, 6)];
            }
            return w;
        }
        static void RandArray(int firstYear, ref Weather[,,] array)
        {
            for (int y = 0; y < years; y++)
            {
                for (int m = 0; m < months; m++)
                {
                    for (int d = 0; d < DateTime.DaysInMonth(y + firstYear + 1, m + 1); d++)
                    {
                        array[y, m, d] = Rand(m);
                    }
                }
            }
        }
        static int FirstYear()
        {
            int first = 0;
            bool ch = true;
            while (ch)
            {
                Console.Clear();
                Console.Write("Enter year less than {0}:  ", maxFirstYear);
                string str = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine();

                bool bl = int.TryParse(str, out first);
                if (bl == false || (bl && (first > maxFirstYear || first < 0)))
                {
                    Console.WriteLine("Put year less than {0}!", maxFirstYear);
                    Console.ReadKey();
                }
                else
                {
                    ch = false;
                }
            }
            return first;
        }
        static int UserYear(int firstYear)
        {
            int year = 0;
            bool ch = true;
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
            return year;
        }
        static int UserMonth()
        {
            bool ch = true;
            int month = 0;
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
            return month;
        }
        static int CountFlags(Weather condition, int firstYear, int year, int month, Weather[,,] array)
        {
            int count = 0;
            for (int i = 0; i < DateTime.DaysInMonth(year, month); i++)
            {
                if (array[year - firstYear - 1, month - 1, i].HasFlag(condition))
                {
                    count += 1;
                }
            }
            return count;
        }
        static void View(int year, int month, Weather[,,] array, int firstYear, int foggyDays, int sunnyDays, int cloudyDays, int windyDays, int humidDays, int hotDays, int warmDays, int coolDays, int coldDays, int normalDays, int frostyDays)
        {
            Console.Clear();
            Console.WriteLine("Year: {0}", year);
            Console.WriteLine("Month: {0}", month);
            Console.WriteLine();

            for (int i = 0; i < DateTime.DaysInMonth(year, month); i++)
            {
                Console.WriteLine("{0} Day:  {1}", i + 1, array[year - firstYear - 1, month - 1, i]);
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


        [Flags]
        enum Weather : ushort
        {
            Empty = 0x00,
            hot = 0x01,
            warm = 0x02,
            cold = 0x04,
            cool = 0x08,
            frosty = 0x10,

            sunny = 0x20,
            windy = 0x40,
            cloudy = 0x80,
            humid = 0x100,
            foggy = 0x200,
            normal = 0x400
        }

        static void Main(string[] args)
        {
            Weather[,,] array = new Weather[years, months, days];       // массив погоды в различные дни и месяцы в течении 10 лет 

            bool view = true;
            while (view)
            {
                Console.WriteLine("Do you want to see the calendar? Yes(Y), No(N)");
                ConsoleKeyInfo strfirst = Console.ReadKey();
                if (strfirst.Key == ConsoleKey.Y)
                {

                    //Опредлеяем с какого года ввыводить календарь                    
                    int firstYear = 0;       // первый год
                    firstYear = FirstYear();

                    // Заполняем массив температурой и 2 раза состоянием
                    RandArray(firstMonth, ref array);

                    // Вычисляем год
                    int year = 0;
                    year = UserYear(firstYear);

                    // Вычисляем месяц
                    int month = 0;
                    month = UserMonth();

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

                    foggyDays = CountFlags(Weather.foggy, firstYear, year, month, array);
                    sunnyDays = CountFlags(Weather.sunny, firstYear, year, month, array);
                    cloudyDays = CountFlags(Weather.cloudy, firstYear, year, month, array);
                    windyDays = CountFlags(Weather.windy, firstYear, year, month, array);
                    humidDays = CountFlags(Weather.humid, firstYear, year, month, array);
                    hotDays = CountFlags(Weather.hot, firstYear, year, month, array);
                    warmDays = CountFlags(Weather.warm, firstYear, year, month, array);
                    coolDays = CountFlags(Weather.cool, firstYear, year, month, array);
                    coldDays = CountFlags(Weather.cold, firstYear, year, month, array);
                    normalDays = CountFlags(Weather.normal, firstYear, year, month, array);
                    frostyDays = CountFlags(Weather.frosty, firstYear, year, month, array);

                    //Выводим на экран
                    View(year, month, array, firstYear, foggyDays, sunnyDays, cloudyDays, windyDays, humidDays, hotDays, warmDays, coolDays, coldDays, normalDays, frostyDays);
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

