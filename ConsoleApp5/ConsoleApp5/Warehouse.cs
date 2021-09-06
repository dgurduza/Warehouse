using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5
{
    class Warehouse
    {
        public Container[] NewContainers { get; set; }
        public int NumOfContainers { get; set; }
        public int EmptyPlace { get; set; }
        public int Payment { get; set; }
        public Warehouse(int sizeOfWh, int paymentForContent)
        {
            Payment = paymentForContent;
            NumOfContainers = sizeOfWh;
            NewContainers = new Container[sizeOfWh];
        }
        public override string ToString()
        {
            return $"Размер склада: {NumOfContainers}{Environment.NewLine}" +
                $"Количество пустых мест: {EmptyPlace}{Environment.NewLine}" +
                $"Размер платы за аренду: {Payment}";
        }
    }
}
