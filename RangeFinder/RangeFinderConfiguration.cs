using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RangeFinder.Models;

namespace RangeFinder
{
    public class RangeFinderConfiguration : IRocketPluginConfiguration
    {
        public string MessageColour { get; set; }
        [XmlArrayItem("Vehicle")]
        public RangeVehicle[] Vehicles { get; set; }
        public void LoadDefaults()
        {
            MessageColour = "yellow";

            Vehicles = new RangeVehicle[]
            {
                new RangeVehicle()
                {
                    Id = 27017,
                    BallisticsForce = 7000f,
                    MaxNumber = 30f,
                    MaxAngle = 13.9f
                },
                new RangeVehicle()
                {
                    Id = 27016,
                    BallisticsForce = 7000f,
                    MaxNumber = 45f,
                    MaxAngle = 17.9f
                },
                new RangeVehicle()
                {
                    Id = 120,
                    BallisticsForce = 3000f,
                    MaxNumber = 45f,
                    MaxAngle = 45f
                }
            };
        }
    }
}
