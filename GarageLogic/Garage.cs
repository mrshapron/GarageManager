using System;
using System.Collections.Generic;
using System.Linq;
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
        public ContactData FindContactByVehicle(string i_LicenceNumber)
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
    }
}
