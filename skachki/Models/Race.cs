using System;

namespace skachki.Models
{
    public class Race
    {
        public int      RaceID   { get; set; }
        public DateTime RaceDate { get; set; }
        public string   Location { get; set; }
        public int      HorseID  { get; set; }
        public int      JockeyID { get; set; }
        public int?     Place    { get; set; }
        public int?     Distance { get; set; }
    }
}
