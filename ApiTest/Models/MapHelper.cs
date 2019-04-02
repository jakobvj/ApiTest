using ApiTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Models
{
    public static class MapHelper
    {
        public static CampModel MapCampModel(Camp camp)
        {
            CampModel cm = new CampModel
            {
                Name = camp.Name,
                Moniker = camp.Moniker,
                Length = camp.Length,
                EventDate = camp.EventDate,

                LocationAddress1 = camp.Location.Address1,
                LocationAddress2 = camp.Location.Address2,
                LocationAddress3 = camp.Location.Address3,
                LocationCityTown = camp.Location.CityTown,
                LocationCountry = camp.Location.Country,
                LocationPostalCode = camp.Location.PostalCode,
                LocationStateProvince = camp.Location.StateProvince,
                LocationVenueName = camp.Location.VenueName
            };
            return cm;
        }
        public static List<CampModel> MapCampModels(Camp[] camp)
        {
            List<CampModel> campList = new List<CampModel>();
            foreach (var item in camp)
            {
                CampModel cm = new CampModel
                {
                    Name = item.Name,
                    Moniker = item.Moniker,
                    Length = item.Length,
                    EventDate = item.EventDate,

                    LocationAddress1 = item.Location.Address1,
                    LocationAddress2 = item.Location.Address2,
                    LocationAddress3 = item.Location.Address3,
                    LocationCityTown = item.Location.CityTown,
                    LocationCountry = item.Location.Country,
                    LocationPostalCode = item.Location.PostalCode,
                    LocationStateProvince = item.Location.StateProvince,
                    LocationVenueName = item.Location.VenueName
                };

                campList.Add(cm);
            }
            return campList;
        }
    }
}
