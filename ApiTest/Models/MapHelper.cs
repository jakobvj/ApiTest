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
            List<TalkModel> talkList = new List<TalkModel>();
            foreach (var item in camp)
            {
                CampModel cm = new CampModel();
                if (item.Talks != null && item.Talks.Count >= 1)
                {
                    foreach (var talk in item.Talks)
                    {
                        TalkModel tm = new TalkModel();
                        tm.Abstract = talk.Abstract;
                        tm.Level = talk.Level;
                        tm.Title = talk.Title;

                        talkList.Add(tm);
                    }
                
                    cm.Talks = talkList;
                }
                
                cm.Name = item.Name;
                cm.Moniker = item.Moniker;
                cm.Length = item.Length;
                cm.EventDate = item.EventDate;

                cm.LocationAddress1 = item.Location.Address1;
                cm.LocationAddress2 = item.Location.Address2;
                cm.LocationAddress3 = item.Location.Address3;
                cm.LocationCityTown = item.Location.CityTown;
                cm.LocationCountry = item.Location.Country;
                cm.LocationPostalCode = item.Location.PostalCode;
                cm.LocationStateProvince = item.Location.StateProvince;
                cm.LocationVenueName = item.Location.VenueName;
                    

                campList.Add(cm);
            }
            return campList;
        }
    }
}
