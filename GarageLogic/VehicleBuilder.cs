using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{

    public class VehicleBuilder
    {
        private readonly Vehicle vehicle;
        public VehicleBuilder(eVehicleTypes i_VehicleType, string licenceNumber)
        {
            switch (i_VehicleType)
            {
                case eVehicleTypes.ElectricCar:
                    vehicle = new Car();
                    vehicle.SetWheels();
                    vehicle.SetEngine(eEngineType.Elecric);
                    vehicle.LicenceNumber = licenceNumber;

                    break;
                case eVehicleTypes.FuelCar:
                    vehicle = new Car();
                    vehicle.SetWheels();
                    vehicle.SetEngine(eEngineType.Fuel);
                    vehicle.LicenceNumber = licenceNumber;
                    break;
                case eVehicleTypes.ElectricMotorcycle:
                    vehicle = new Motorcycle();
                    vehicle.SetWheels();
                    vehicle.SetEngine(eEngineType.Elecric);
                    vehicle.LicenceNumber = licenceNumber;
                    break;
                case eVehicleTypes.FurlMotorcycle:
                    vehicle = new Motorcycle();
                    vehicle.SetWheels();
                    vehicle.SetEngine(eEngineType.Fuel);
                    vehicle.LicenceNumber = licenceNumber;
                    break;
                case eVehicleTypes.Truck:
                    vehicle = new Truck();
                    vehicle.SetWheels();
                    vehicle.SetEngine(eEngineType.Fuel);
                    vehicle.LicenceNumber = licenceNumber;
                    break;
            }
        }
        public Dictionary<string, string> GenerateParamsOutputs()
        {
            return vehicle.GenerateParamsOutputs();
        }

        public Dictionary<string, string> InitVehicleValues(Dictionary<string, string> i_DicInputs)
        {
            var dicError = vehicle.InitValues(i_DicInputs);
            return dicError;
        }
        public Vehicle GetVehicleInstance()
        {
            return vehicle;
        }

    }
    public enum eVehicleTypes
    {
        ElectricCar = 1,
        FuelCar = 2,
        ElectricMotorcycle = 3,
        FurlMotorcycle = 4,
        Truck = 5
    }
}
