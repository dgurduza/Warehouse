using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5
{
    class Container
    {
        public Box[] NewBoxes { get; set; }
        public int NumOfBoxes { get; set; }
        public double DamageOfContainer { get; set; }
        public double SumOfPrices { get; set; }
        public Container(int number)
        {
            NumOfBoxes = number;
            NewBoxes = new Box[number];
        }
        public double GetSumAfterDamage()
        {
            Random rand = new Random();
            DamageOfContainer = rand.NextDouble() * 0.5;
            return SumOfPrices - (SumOfPrices * DamageOfContainer);
        }
        public override string ToString()
        {
            return $"{Environment.NewLine}Контейнер: {Environment.NewLine}Количество ящиков:{NumOfBoxes}{Environment.NewLine}" +
                $"Повреждение контейнера:{DamageOfContainer:F2}{Environment.NewLine}" +
                $"Cумма цен содержимого:{SumOfPrices:F2}{Environment.NewLine}" +
                $"Cумма цен содержимого c учетом повреждения:{SumOfPrices - (SumOfPrices * DamageOfContainer):F2}{Environment.NewLine}";
        }
    }
}
