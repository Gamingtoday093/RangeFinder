using Rocket.API.Collections;
using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger = Rocket.Core.Logging.Logger;
using UnityEngine;
using Rocket.Unturned.Chat;

namespace RangeFinder
{
    public class RangeFinder : RocketPlugin<RangeFinderConfiguration>
    {
        public static RangeFinder Instance { get; private set; }
        public static RangeFinderConfiguration Config { get; private set; }
        public UnityEngine.Color MessageColour { get; private set; }

        protected override void Load()
        {
            Instance = this;
            Config = Configuration.Instance;
            MessageColour = UnturnedChat.GetColorFromName(Config.MessageColour, Color.green);

            Logger.Log($"{Name} {Assembly.GetName().Version} by Gamingtoday093 has been Loaded");
        }

        protected override void Unload()
        {
            Logger.Log($"{Name} has been Unloaded");
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "RNoMarker", "You have No Marker set!" },
            { "RDistance", "Distance: {0}m" },
            { "RDistanceVehicle", "Distance: {0}m, Aim Minimum(Account for Difference Elevation): {1} ({2})" }
        };
    }
}
