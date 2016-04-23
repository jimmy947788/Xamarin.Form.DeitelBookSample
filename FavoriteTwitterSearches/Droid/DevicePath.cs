using System;
using FavoriteTwitterSearches.Droid;

[assembly: Xamarin.Forms.Dependency (typeof (DevicePath))]
namespace FavoriteTwitterSearches.Droid
{
	public class DevicePath : IDevicePath
	{
		public string GetSpecialFolderByPersonal ()
		{
			return System.Environment.GetFolderPath (Environment.SpecialFolder.Personal);
		}
	}
}

