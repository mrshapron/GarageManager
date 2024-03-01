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
            ContactData foundContact = garage.FindContactByLicense(licenceNumber);
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
        public void PrintAllVehiclesLIcence ()
        {
            bool isValid;
            do
            {
                Console.WriteLine("choose what status of vehicle do you want");
                Console.WriteLine("1- in process");
                Console.WriteLine("2- ready");
                Console.WriteLine("3- paid");
                Console.WriteLine("4- all");
                eVehicleStatus status;
                isValid = Enum.TryParse(Console.ReadLine(), out status);
                if(isValid)
                {
                    List<ContactData> contacts = garage.ContactsByStatus(status);
                    printLicenceNumbers(contacts);
                }
                else
                {
                    Console.WriteLine("please enter a number 1-4");
                }
            }
            while (!isValid);
        }
        private void printLicenceNumbers(List<ContactData> i_Contacts)
        {
            int count = 1;
            foreach (ContactData contact in i_Contacts)
            {
                Console.WriteLine($"{count++}. {contact.Vehicle.LicenceNumber}");
            }
        }

        private void printVehicleTypes()
        {
            Console.WriteLine("1- Electric Car");
            Console.WriteLine("2- Fuel Car");
            Console.WriteLine("3- Electric Motorcycle");
            Console.WriteLine("4- Fuel Motorcycle");
            Console.WriteLine("5- Truck");
        }
        public void ChangeVehicleStatus()
        {
            bool isValid;
            bool isChanged=false;
            do
            {
                Console.WriteLine("please enter licence number");
                string licenceNumber = Console.ReadLine();
                Console.WriteLine("what is the new status?");
                Console.WriteLine("1- in process");
                Console.WriteLine("2- ready");
                Console.WriteLine("3- paid");
                eVehicleStatus status;
                isValid = Enum.TryParse(Console.ReadLine(), out status);
                if (isValid)
                {
                    isChanged= garage.ChangeVehicleStatus(status, licenceNumber);
                    if(!isChanged)
                    {
                        Console.WriteLine("licence number doesn't exist. please try again");
                    }
                }
                else
                {
                    Console.WriteLine("please enter a number 1-2");
                }
            }
            while (!isValid&&!isChanged);

        }
        public void InflateWheelsToMax(string i_Licence_Number)
        {
            bool isExist;
            do
            {
                Console.WriteLine("please enter licence number");
                string licenceNumber = Console.ReadLine();
                isExist = garage.InflateVehicle(licenceNumber);
                if (isExist)
                {
                    Console.WriteLine("licence number doesn't exist. please try again");
                }
            }
            while (!isExist);
        }
        public void PrintVehicleData()
        {
            string vehicleData;
            do
            {
                Console.WriteLine("please enter licence number");
                string licenceNumber = Console.ReadLine();
                vehicleData = garage.GetVehicleData(licenceNumber);
                if (vehicleData==null)
                {
                    Console.WriteLine("licence number doesn't exist. please try again");
                }
                else
                {
                    Console.WriteLine(vehicleData);
                }
            }
            while (vehicleData != null);
        }
        public void FuelVehicle()
        {
            bool isValid;
            bool isChanged=false;
            float numOfLitters;
            do
            {
                Console.WriteLine("please enter licence number");
                string licenceNumber = Console.ReadLine();
                Console.WriteLine("please enter num of litters");
                isValid = float.TryParse(Console.ReadLine(), out numOfLitters);
                Console.WriteLine("what type of fuel?");
                printFuelTypes();
                eFuelType type;
                isValid = Enum.TryParse(Console.ReadLine(), out type);
                if (isValid)
                {
                    isChanged = garage.FuelVehicle(licenceNumber, numOfLitters, type);
                    if (!isChanged)
                    {
                        Console.WriteLine("licence number doesn't exist. please try again");
                    }
                }
                else
                {
                    Console.WriteLine("please enter a number 1-4");
                }
            }
            while (!isValid && !isChanged);
        }
        public void ChargeElectricVehicle()
        {
            bool isValid;
            bool isChanged = false;
            float numOfMinutes;
            do
            {
                Console.WriteLine("please enter licence number");
                string licenceNumber = Console.ReadLine();
                Console.WriteLine("please enter num of litters");
                isValid = float.TryParse(Console.ReadLine(), out numOfMinutes);
                if (isValid)
                {
                    isChanged = garage.ChargeVehicle(licenceNumber, numOfMinutes);
                    if (!isChanged)
                    {
                        Console.WriteLine("licence number doesn't exist. please try again");
                    }
                }
                else
                {
                    Console.WriteLine("please enter a number");
                }
            }
            while (!isValid && !isChanged);
        }
        private void printFuelTypes()
        {
            Console.WriteLine("1- soler");
            Console.WriteLine("2- octan 95");
            Console.WriteLine("3-octan 96");
            Console.WriteLine("4-octan 98");
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
