using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIClient
{
    //I ask the WebAPI about an Station
    class Station
    {
        //Numeric station identifier (required if stpid not specified)
        //A single five-digit code to tell the server which station you’d like to receive predictions for.
        public int mapid { get; set; }
        //Numeric stop identifier (required if mapid not specified)
        //A single five-digit code to tell the server which specific stop (in this context, specific platform or platform side within a larger station) you’d like to receive predictions for.
        //public int stpid { get; set; }
        //Maximum results (optional)
        //The maximum number you’d like to receive (if not specified, all available results for the requested stop or station will be returned)
        //public int max { get; set; }
        //Route code (optional)
        //Allows you to specify a single route for which you’d like results (if not specified, all available results for the requested stop or station will be returned)
        //public int rt { get; set; }
        //Alphanumeric API key (required)
        //Your unique API key, assigned to you after agreeing to DLA and requesting a key be generated for you.
        public string key { get; set; }
    }
    class StationInDB
    {
        //	STATION_DESCRIPTIVE_NAME	PARENT_STOP_ID	ADA	Red	Blue	Brn	G	P	Pexp	Y	Pink	Org
        public int STOP_ID { get; set; }
        public String DIRECTION_ID { get; set; }
        public String STOP_NAME { get; set; }
        public double LON { get; set; }
        public double LAT { get; set; }
        public String STATION_NAME { get; set; }
        public String STATION_DESCRIPTIVE_NAME{ get; set; }
        public int PARENT_STOP_ID { get; set; }
        public int ADA { get; set; }
        public Boolean Red { get; set; }
        public Boolean Blue { get; set; }
        public Boolean Brn { get; set; }
        public Boolean G { get; set; }
        public Boolean P { get; set; }
        public Boolean Pexp { get; set; }
        public Boolean Y { get; set; }
        public Boolean Pink { get; set; }
        public Boolean Org { get; set; }
    }
}
