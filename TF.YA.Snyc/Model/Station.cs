using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace TF.YA.Sync
{
    public class Station : SyncClass
    {
         [DataMember]
         public string StationName { get; set; }
         [DataMember]
         public string NameJP { get; set; }
        // [DataMember]
         public string TMISNumber { get; set; }
         public override string Key { get { return StationName; } }
    }
    public class StationAll
    {
        public List<Station> stations;
        public int TotalCount;

    }
    public class  resultStation
{

        public StationAll Data;
        public int Success;
        public String ResultText;
}
    public class Place : SyncClass
    {
         [DataMember]
        public string PlaceID { get; set; }
         [DataMember]
         public string PlaceName { get; set; }
         public override string Key { get { return PlaceID; } }
    }
    public class PlaceList
    {

        public List<Place> places;
    }
    public class PlaceAll
    {
        public int Success;
        public String ResultText;
        public PlaceList Data;
    }
    public class SectionListS
    {
      //  public string SectionList;
        public List<Section> SectionList;
    }
    public class SectionAll 
    {
        public int Success;
        public String ResultText;
        public SectionListS Data;
    }
    public class Section : SyncClass
    {
         [DataMember]
        public string JWDNumber { get; set; }
       
         public string JWDName { get; set; }
         [DataMember]
         public int ICSectionNumber { get; set; }
         [DataMember]
         public string ICSectionName { get; set; }

         public override string Key { get { return ICSectionNumber.ToString(); } }
    }
}
