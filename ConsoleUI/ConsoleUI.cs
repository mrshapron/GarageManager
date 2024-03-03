using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GarageLogic;

namespace ConsoleUI
{
    public class ConsoleUI
    {
        private readonly Garage garage;
        public ConsoleUI()
        {
            garage = new Garage();
        }

        public void Start()
        {
            bool isExist = false;

            Console.WriteLine("Welcome to the Garage!");

            while (!isExist)
            {
                try
                {
                    HelperUI.PrintMenu();
                    bool isValid = Enum.TryParse(Console.ReadLine(), out eMenuChoice menuChoice);
                    if (!isValid)
                    {
                        throw new FormatException("You Typed invalid number!");
                    }
                    switch (menuChoice)
                    {
                        case eMenuChoice.AddNewVehicle:
                            HelperUI.AddNewCar(garage);
                            break;
                        case eMenuChoice.getGetVehicleLicenceNumbers:
                            HelperUI.PrintAllVehiclesLIcence(garage);
                            break;
                        case eMenuChoice.InflateVehicle:
                            HelperUI.InflateWheelsToMax(garage);
                            break;
                        case eMenuChoice.FuelVehicle:
                            HelperUI.FuelVehicle(garage);
                            break;
                        case eMenuChoice.ChargeVehicle:
                            HelperUI.ChargeElectricVehicle(garage);
                            break;
                        case eMenuChoice.GetVehicleData:
                            HelperUI.PrintVehicleData(garage);
                            break;
                        case eMenuChoice.ChangeVehicleStatus:
                            HelperUI.ChangeVehicleStatus(garage);
                            break;
                        case eMenuChoice.Exit:
                            isExist = true;
                            break;
                        default:
                            throw new ValueOutOfRangeException("Menu Choice", 1, 8);
                    }
                } 
                catch (FormatException formatE)
                {
                    Console.WriteLine($"FormatException: {formatE.Message}");
                }
                catch(ArgumentException argumentE)
                {
                    Console.WriteLine($"ArgumentException: {argumentE.Message}");
                }
                catch(ValueOutOfRangeException rangeE)
                {
                    Console.WriteLine($"ValueOutOfRangeException: {rangeE.Message}");
                }
            }


            Console.WriteLine("Cya Later!");
        }
    }

    public enum eMenuChoice
    {
        AddNewVehicle = 1, getGetVehicleLicenceNumbers, ChangeVehicleStatus, InflateVehicle, FuelVehicle,
        ChargeVehicle, GetVehicleData, Exit
    }
}
