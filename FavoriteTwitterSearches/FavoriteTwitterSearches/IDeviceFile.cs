using System;

namespace FavoriteTwitterSearches
{
	public interface IDeviceFile
	{
		bool Exists(string filename);

		void WriteAllText (string path, string contents);

		string ReadAllText(string path);
	}
}

