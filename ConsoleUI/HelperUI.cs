using GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public static class HelperUI
    {
        public static void PrintMenu()
        {
            Console.WriteLine("what would you like to do?");
            Console.WriteLine("1 - Add a new car");
            Console.WriteLine("2 - Print all vehicles in the garage");
            Console.WriteLine("3 - Change vehicle status");
            Console.WriteLine("4 - Inflate vehicle ");
            Console.WriteLine("5 - Fuel vehicle");
            Console.WriteLine("6 - Charge vehicle");
            Console.WriteLine("7 - Get vehicle details");
            Console.WriteLine("8 - Exit");
        }

        public static void PrintAllVehiclesLIcence(Garage i_Garage)
        {
            bool isValid;
            do
            {
                Console.WriteLine("choose what status of vehicle do you want");
                Console.WriteLine("1 - in process");
                Console.WriteLine("2 - ready");
                Console.WriteLine("3 - paid");
                Console.WriteLine("4 - all");
                eVehicleStatus status;
                isValid = Enum.TryParse(Console.ReadLine(), out status);
                if (isValid)
                {
                    List<ContactData> contacts = i_Garage.ContactsByStatus(status);
                    printLicenceNumbers(contacts);
                }
                else
                {
                    Console.WriteLine("please enter a number 1-4");
                }
            }
            while (!isValid);
        }

        private static void printLicenceNumbers(List<ContactData> i_Contacts)
        {
            int count = 1;
            foreach (ContactData contact in i_Contacts)
            {
                Console.WriteLine($"{count++}. {contact.Vehicle.LicenceNumber}");
            }
        }

        public static void AddNewCar(Garage i_Garage)
        {
            Console.WriteLine("Please type the licence number");
            string licenceNumber = Console.ReadLine();
            ContactData foundContact = i_Garage.FindContactByLicense(licenceNumber);
            if (foundContact != null)
            {
                foundContact.VehicleStatus = eVehicleStatus.InProcess;
            }
            else
            {
                Console.WriteLine("Please Enter Your Vehicle Type: ");
                printVehicleTypes();
                if (Enum.TryParse(Console.ReadLine(), out eVehicleTypes vehicleType))
                {
                    if (!((int)vehicleType >= 1 && (int)vehicleType <= 5))
                    {
                        throw new ValueOutOfRangeException("vehicle type", 1, 5);
                    }
                    ContactData contact = new ContactData();
                    Dictionary<string, string> dicOutputContact = contact.GenerateParamsOutputs();
                    Dictionary<string, string> dicInputsContact = getParametersFromUser(dicOutputContact);
                    Dictionary<string, string> dicErrorsContact = contact.InitValues(dicInputsContact);

                    VehicleBuilder builder = new VehicleBuilder(vehicleType, licenceNumber);
                    Dictionary<string, string> dicOutputsVehicle = builder.GenerateParamsOutputs();
                    Dictionary<string, string> dicInputsVehicle = getParametersFromUser(dicOutputsVehicle);
                    Dictionary<string, string> dicErrorVehicle = builder.InitVehicleValues(dicInputsVehicle);

                    while (dicErrorVehicle.Count != 0 || dicErrorsContact.Count != 0)
                    {
                        printErrors(dicErrorsContact, "Contact");
                        printErrors(dicErrorVehicle, "Vehicle");

                        updateParams(dicErrorVehicle, dicOutputsVehicle);
                        updateParams(dicErrorsContact, dicOutputContact);

                        dicInputsContact = getParametersFromUser(dicOutputContact);
                        dicInputsVehicle = getParametersFromUser(dicOutputsVehicle);
                        dicErrorVehicle = builder.InitVehicleValues(dicInputsVehicle);
                        dicErrorsContact = contact.InitValues(dicInputsContact);
                    }

                    contact.Vehicle = builder.GetVehicleInstance();
                    i_Garage.Contacts.Add(contact);
                }
                else
                {
                    throw new FormatException("Vehicle Type is not Valid");
                }
            }
        }

        public static void ChangeVehicleStatus(Garage i_Garage)
        {
            bool isValid;
            bool isChanged = false;
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
                    isChanged = i_Garage.ChangeVehicleStatus(status, licenceNumber);
                    if (!isChanged)
                    {
                        Console.WriteLine("licence number doesn't exist. please try again");
                    }
                }
                else
                {
                    throw new FormatException("Vehicle Status is not Valid.");
                }
            }
            while (!isValid && !isChanged);

        }

        private static void printVehicleTypes()
        {
            Console.WriteLine("1- Electric Car");
            Console.WriteLine("2- Fuel Car");
            Console.WriteLine("3- Electric Motorcycle");
            Console.WriteLine("4- Fuel Motorcycle");
            Console.WriteLine("5- Truck");
        }

        public static void InflateWheelsToMax(Garage i_Garage)
        {
            bool isExist;
            do
            {
                Console.WriteLine("Please Enter Your License Number");
                string licenceNumber = Console.ReadLine();
                isExist = i_Garage.InflateVehicle(licenceNumber);
                if (isExist)
                {
                    Console.WriteLine("Licence Number doesn't exist. please try again");
                }
            }
            while (!isExist);
        }

        public static void PrintVehicleData(Garage i_Garage)
        {
            string vehicleData;
            do
            {
                Console.WriteLine("Please Enter Your License Number");
                string licenceNumber = Console.ReadLine();
                vehicleData = i_Garage.GetVehicleData(licenceNumber);
                if (vehicleData == null)
                {
                    Console.WriteLine("License Number doesn't exist. Please try again");
                }
                else
                {
                    Console.WriteLine(vehicleData);
                }
            }
            while (vehicleData != null);
        }

        public static void FuelVehicle(Garage garage)
        {
            bool isValid;
            bool isChanged = false;
            float numOfLitters;
            do
            {
                Console.WriteLine("Please Enter Your License Number");
                string licenceNumber = Console.ReadLine();
                Console.WriteLine("please enter num of litters");
                isValid = float.TryParse(Console.ReadLine(), out numOfLitters);
                Console.WriteLine("what type of fuel?");
                printFuelTypes();
                isValid = Enum.TryParse(Console.ReadLine(), out eFuelType type);
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

        public static void ChargeElectricVehicle(Garage i_Garage)
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
                    isChanged = i_Garage.ChargeVehicle(licenceNumber, numOfMinutes);
                    if (!isChanged)
                    {
                        Console.WriteLine("licence number doesn't exist. please try again");
                    }
                }
                else
                {
                    throw new FormatException("You typed invalid Number!");
                }
            }
            while (!isValid && !isChanged);
        }

        private static void printFuelTypes()
        {
            Console.WriteLine("1 - Soler");
            Console.WriteLine("2 - Octan 95");
            Console.WriteLine("3 - Octan 96");
            Console.WriteLine("4 - Octan 98");
        }

        /// <summary>
        /// Asking from the user all the inputs for the parameters that needs to be fulfilled
        /// </summary>
        private static Dictionary<string, string> getParametersFromUser(Dictionary<string, string> i_DicParam)
        {
            string input;
            Dictionary<string, string> userInputParams = new Dictionary<string, string>();
            foreach (var pairParam in i_DicParam)
            {
                Console.WriteLine(pairParam.Value);
                input = Console.ReadLine();
                userInputParams.Add(pairParam.Key, input);
            }
            return userInputParams;
        }

        /// <summary>
        /// Prints all the Errors for his inputs for each param the user entered
        /// </summary>
        private static void printErrors(Dictionary<string, string> i_DicErrors, string topicError)
        {
            if (i_DicErrors.Count == 0) return;

            Console.WriteLine($"Error inputs for {topicError}:");
            foreach (var pairError in i_DicErrors)
            {
                Console.WriteLine(pairError.Value);
            }
        }

        /// <summary>
        /// Removes All The Param that were valid from the dictionary
        /// </summary>
        private static void updateParams(Dictionary<string, string> i_DicErrors, Dictionary<string, string> i_DicParams)
        {
            // list helper for removing all the keys in the second foreach
            List<string> listHelperRemoval = new List<string>();
            
            foreach(var pairValues in i_DicParams)
            {
                if (!i_DicErrors.ContainsKey(pairValues.Key))
                {
                    listHelperRemoval.Add(pairValues.Key);
                }
            }

            foreach(var keyToRemove in listHelperRemoval)
            {
                i_DicParams.Remove(keyToRemove);
            }
        }
    }
}
