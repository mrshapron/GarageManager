using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GarageLogic;

namespace ConsoleUI
{
    public class ConsoleUI
    {
        private Garage garage;
        public ConsoleUI()
        {
            garage = new Garage();
        }
        public bool AddNewCar()
        {
            string type;
            bool isValid;
            Console.WriteLine("Please type the licence number");
            string licenceNumber = Console.ReadLine();
            ContactData foundContact = garage.FindContactByVehicle(licenceNumber);
            if (foundContact != null)
            {
                foundContact.VehicleStatus = eVehicleStatus.InProcess;
            }
            else
            {
                Console.WriteLine("Please enter your vehicle type");
                printVehicleTypes();
                type = Console.ReadLine();
                isValid = Enum.TryParse(type, out eVehicleTypes vehicleType);
                if (isValid)
                {
                    VehicleBuilder builder = new VehicleBuilder(vehicleType, licenceNumber);
                    Dictionary<string, string> dicParamsOutputs = builder.GenerateParamsOutputs();
                    Dictionary<string, string> dicInputs = getParametersFromUser(dicParamsOutputs);
                    Dictionary<string, string> dicError = builder.InitVehicleValues(dicInputs);
                    while (dicError.Count != 0)
                    {
                        printErrors(dicError);
                        updateParams(dicError, dicParamsOutputs);
                        dicInputs = getParametersFromUser(dicParamsOutputs);
                        dicError = builder.InitVehicleValues(dicInputs);
                    }
                }
            }
            return true;
        }
        private void printVehicleTypes()
        {
            Console.WriteLine("1- Electric Car");
            Console.WriteLine("2- Fuel Car");
            Console.WriteLine("3- Electric Motorcycle");
            Console.WriteLine("4- Fuel Motorcycle");
            Console.WriteLine("5- Truck");
        }
        private Dictionary<string, string> getParametersFromUser(Dictionary<string, string> i_dicParam)
        {
            string input;
            Dictionary<string, string> userInputParams = new Dictionary<string, string>();
            foreach (var pairParam in i_dicParam)
            {
                Console.WriteLine(pairParam.Value);
                input = Console.ReadLine();
                userInputParams.Add(pairParam.Key, input);
            }
            return userInputParams;
        }
        private void printErrors(Dictionary<string, string> i_DicErrors)
        {
            Console.WriteLine("Error inputs:");
            foreach (var pairError in i_DicErrors)
            {
                Console.WriteLine(pairError.Value);
            }
        }
        private void updateParams(Dictionary<string, string> i_DicErrors, Dictionary<string, string> i_DicParams)
        {
            foreach (var pairError in i_DicErrors)
            {
                i_DicParams.Remove(pairError.Key);
            }
        }
    }
}
