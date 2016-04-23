using System;
using FavoriteTwitterSearches.iOS;

[assembly: Xamarin.Forms.Dependency (typeof (DevicePath))]
namespace FavoriteTwitterSearches.iOS
{
	public class DevicePath : IDevicePath
	{
		public string GetSpecialFolderByPersonal ()
		{
			return System.Environment.GetFolderPath (Environment.SpecialFolder.Personal);
		}
	}
}

