using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleUI;


namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleUI.ConsoleUI consoleUI = new ConsoleUI.ConsoleUI();
            consoleUI.AddNewCar();
            consoleUI.AddNewCar();
            consoleUI.PrintVehicleData();
        }
    }
}
