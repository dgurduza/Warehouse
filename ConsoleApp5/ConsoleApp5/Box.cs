using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5
{
    class Box
    {
        public double WeightOfBox { get; set; }
        public int PriceOfOneKil { get; set; }
        public Box(double weight, int price)
        {
            WeightOfBox = weight;
            PriceOfOneKil = price;
        }
        public double AddPriceOfBox()
        {
            return WeightOfBox * PriceOfOneKil;
        }
        public double AddWeightOfBox()
        {
            return WeightOfBox;
        }
        public override string ToString()
        {
            return $"{Environment.NewLine}Масса содержимого:{WeightOfBox:F2}{Environment.NewLine}" +
                $"Цена 1 кг содержимого:{PriceOfOneKil}{Environment.NewLine}" +
                $"Итоговая цена ящика:{WeightOfBox * PriceOfOneKil:F2}";
        }
    }
}
