using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RangeFinder.Models;

namespace RangeFinder.Commands
{
    public class RangeFindCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "RangeFind";

        public string Help => "Gets the Distance to from your Current Location to your Marker.";

        public string Syntax => "";

        public List<string> Aliases => new List<string>(){ "Range" };

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if (!player.Player.quests.isMarkerPlaced)
            {
                UnturnedChat.Say(player, RangeFinder.Instance.Translate("RNoMarker"), RangeFinder.Instance.MessageColour);
                return;
            }

            Vector3 markerLocation = new Vector3(player.Player.quests.markerPosition.x, player.Position.y, player.Player.quests.markerPosition.z);
            Vector3 playerLocation = player.Position;

            float distance = Vector3.Distance(playerLocation, markerLocation);
            
            if (player.CurrentVehicle != null && RangeFinder.Config.Vehicles.Any(v => v.Id == player.CurrentVehicle.id)) // If the Player is in a valid Vehicle
            {
                var vehicle = RangeFinder.Config.Vehicles.FirstOrDefault(v => v.Id == player.CurrentVehicle.id);

                UnturnedChat.Say(player, RangeFinder.Instance.Translate("RDistanceVehicle", distance.ToString("N2"), AimAngle(distance, vehicle), player.CurrentVehicle.asset.name), RangeFinder.Instance.MessageColour);
                return;
            }

            UnturnedChat.Say(player, RangeFinder.Instance.Translate("RDistance", distance.ToString("N2")), RangeFinder.Instance.MessageColour);
        }

        public string AimAngle(float distance, RangeVehicle vehicle)
        {
            float force = vehicle.BallisticsForce / 50;
            float gravity = 9.81f;

            float AngleSin = (distance * gravity) / (force * force);
            if (double.IsNaN(Math.Asin(AngleSin)))
            {
                return "(Out Of Range)";
            }
            float AngleDeg = (float)(Math.Asin(AngleSin) * (180 / Math.PI)) / 2;
            
            float Aim = (vehicle.MaxNumber / vehicle.MaxAngle) * AngleDeg;
            if (Aim > vehicle.MaxNumber)
            {
                return $"{Aim.ToString("N1")}(Out of Range)";
            }

            return $"{Aim.ToString("N1")}";
        }
    }
}
