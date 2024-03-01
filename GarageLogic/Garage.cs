using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class Garage
    {
        public List<ContactData> Contacts { get; }

        public Garage()
        {
            Contacts = new List<ContactData>();
        }
        public ContactData FindContactByLicense(string i_LicenceNumber)
        {
            ContactData found = null;
            foreach (ContactData contactData in Contacts)
            {
                if (contactData.Vehicle.LicenceNumber.Equals(i_LicenceNumber))
                {
                    found = contactData;
                    break;
                }
            }
            return found;
        }
        public List<ContactData> ContactsByStatus(eVehicleStatus i_Status)
        {
            return (Contacts.Where((contact) => contact.VehicleStatus == i_Status ||
                    contact.VehicleStatus == eVehicleStatus.None).ToList());
        }
        public bool ChangeVehicleStatus(eVehicleStatus i_Status,string i_LicenceNumber)
        {
            bool found = false;
            ContactData contact = FindContactByLicense(i_LicenceNumber);
            if(contact != null)
            {
                contact.VehicleStatus = i_Status;
                found = true;
            }
            return found;
        }
        public bool InflateVehicle(string i_LicenceNumber)
        {
            bool found = false;
            Vehicle vechicle = findVehicleByLicense(i_LicenceNumber);
            if (vechicle != null)
            {
                found = true;
                vechicle.Wheels.ForEach((wheel) =>
                            wheel.Inflate(wheel.MaxAirPressure - wheel.CurrentAirPressure));
            }
            return found;
        }
        private Vehicle findVehicleByLicense(string i_LicenceNumber)
        {
            Vehicle vehicle = null;
            foreach (ContactData contactData in Contacts)
            {
                if (contactData.Vehicle.LicenceNumber.Equals(i_LicenceNumber))
                {
                    vehicle = contactData.Vehicle;
                    break;
                }
            }
            return vehicle;
        }
        public bool FuelVehicle(string i_LicenceNumber, float i_NumOfLitters, eFuelType i_fuelType)
        {
            bool found = false;
            Vehicle vechicle = findVehicleByLicense(i_LicenceNumber);
            if (vechicle != null)
            {
                found = true;
                FuelEngine fuelEngine = vechicle.Engine as FuelEngine;
                fuelEngine.GetFuel(i_NumOfLitters, i_fuelType);
            }
            return found;
        }
        public bool ChargeVehicle(string i_LicenceNumber, float i_NumOfMinutes)
        {
            bool found = false;
            ContactData contact = FindContactByLicense(i_LicenceNumber);
            if(contact != null)
            {
                ElectricEngine electricEngine = contact.Vehicle.Engine as ElectricEngine;
                electricEngine.ChargeBattery(i_NumOfMinutes);
                found = true;
            }
            return found;
        }
        public string GetVehicleData(string i_LicenceNumber)
        {
            ContactData cd = FindContactByLicense(i_LicenceNumber);
            return cd.ToString();
        }
    }
}
