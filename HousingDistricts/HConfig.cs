using System;
using System.IO;
using Newtonsoft.Json;

namespace HousingDistricts
{
	public class HConfigFile
	{
		public bool NotifyOnEntry = true;
		public string NotifyOnEntry_description = "Global setting: Enables entry notifications (see below).";
		public bool NotifyOwner = true;
		public string NotifyOwner_description = "Global setting: Notifies the owner of the house when a user enters/leaves.";
		public bool NotifyVisitor = true;
		public string NotifyVisitor_description = "Global setting: Notifies a user about entering/leaving a house.";
		public bool NotifySelf = true;
		public string NotifySelf_description = "Global setting: Notifies a user about entering/leaving his/her own house.";
		public string NotifyOnEntryString = "You have entered the house: '$HOUSE_NAME'";
		public string NotifyOnEntryString_description = "The string presented to players when they enter another player's house.";
		public string NotifyOnOwnHouseEntryString = "Entered your house: '$HOUSE_NAME'";
		public string NotifyOnOwnHouseEntryString_description = "The string presented to players when they enter their own house.";
		public string NotifyOnOtherEntryString = "$PLAYER_NAME Entered your house: '$HOUSE_NAME'";
		public string NotifyOnOtherEntryString_description = "The string presented to players when someone else enters their house.";
		public bool NotifyOnExit = true;
		public string NotifyOnExit_description = "Global setting: Enables exit notifications.";
		public string NotifyOnExitString = "You have left the house: '$HOUSE_NAME'";
		public string NotifyOnExitString_description = "The string presented to players when they leave another player's house.";
		public string NotifyOnOwnHouseExitString = "Left your house: '$HOUSE_NAME'";
		public string NotifyOnOwnHouseExitString_description = "The string presented to players when they leave their own house.";
		public string NotifyOnOtherExitString = "$PLAYER_NAME Left your house: '$HOUSE_NAME'";
		public string NotifyOnOtherExitString_description = "The string presented to players when someone else leaves their house.";
		public bool HouseChatEnabled = true;
		public string HouseChatEnabled_description = "Global setting: False completely disables house chat.";
		public int MaxHouseSize = 5000;
		public string MaxHouseSize_description = "Maximum house size (width*height).";
		public int MinHouseWidth = 10;
		public string MinHouseWidth_description = "Minimum house width, for protection from griefer use of /house.";
		public int MinHouseHeight = 5;
		public string MinHouseHeight_description = "Minimum house height, for protection from griefer use of /house.";
		public int MaxHousesByUsername = 10;
		public string MaxHousesByUsername_description = "Maximum amount of houses a user can have (unless has persmission house.bypasscount).";
		public bool OverlapHouses = false;
		public string OverlapHouses_description = "Can players create houses that overlap another players' house?";
		public bool AllowRod = true;
		public string AllowRod_description = "Can players use RoD to teleport into houses?";
		/*
		public bool RecursiveNumericPermissions = false;
		public string RecursiveNumericPermissions_description = "Makes house.count. and house.size permissions recursive. Might affect performance.";
		 */

		public static HConfigFile Read(string path)
		{
			if (!File.Exists(path))
				return new HConfigFile();
			using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return Read(fs);
			}
		}

		public static HConfigFile Read(Stream stream)
		{
			using (var sr = new StreamReader(stream))
			{
				var cf = JsonConvert.DeserializeObject<HConfigFile>(sr.ReadToEnd());
				if (ConfigRead != null)
					ConfigRead(cf);
				return cf;
			}
		}

		public void Write(string path)
		{
			using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
			{
				Write(fs);
			}
		}

		public void Write(Stream stream)
		{
			var str = JsonConvert.SerializeObject(this, Formatting.Indented);
			using (var sw = new StreamWriter(stream))
			{
				sw.Write(str);
			}
		}

		public static Action<HConfigFile> ConfigRead;
	}
}
