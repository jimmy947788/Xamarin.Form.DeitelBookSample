using System;
using System.Linq;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace FavoriteTwitterSearches
{
	public partial class MainPage : ContentPage
	{
		List<Button> buttons;
		List<Image> editButtons;
		Dictionary<string, string> tags;
		IDeviceFile deviceFile;
		IDevicePath devicePath;
		string savePath;

		public MainPage ()
		{
			InitializeComponent ();

			buttons = new List<Button> ();
			editButtons = new List<Image> ();
			tags = new Dictionary<string, string> ();

			//load tags from file
			deviceFile = DependencyService.Get<IDeviceFile> ();
			devicePath = DependencyService.Get<IDevicePath> ();
			savePath = Path.Combine(devicePath.GetSpecialFolderByPersonal(), "Tags.txt");
			if (deviceFile.Exists (savePath)) {
				string data = deviceFile.ReadAllText (savePath);
				tags = JsonConvert.DeserializeObject<Dictionary<string, string> > (data);
			}
			else
				deviceFile.WriteAllText(savePath, "");
		}

		protected override void OnAppearing ()
		{
			foreach (var tag in tags) {
				AddNewButton (tag.Key);
			}
			base.OnAppearing ();
		}

		public void ClearTags(object sender, EventArgs e)
		{
			tags.Clear ();
			//TODO:save tags to file
			deviceFile.WriteAllText(savePath, "");

			buttons.Clear();
			this.RefreshList ();
		}

		public void AddTag(object sender, EventArgs e)
		{
			txtTag.Unfocus ();
			txtQuery.Unfocus ();

			string key = txtTag.Text;
			string value = txtQuery.Text;

			if (string.IsNullOrEmpty (key) || string.IsNullOrEmpty (value))
				return;

			if (!tags.ContainsKey (key)) {
				AddNewButton (key);
				tags.Add (key, value);
			} else {
				tags [key] = value;
				this.RefreshList ();
			}

			txtTag.Text = string.Empty;
			txtQuery.Text = string.Empty;

			//TODO:save tags to file
			string data = JsonConvert.SerializeObject(tags);
			deviceFile.WriteAllText(savePath, data);
		}

		private void AddNewButton(string title)
		{
			var button = new Button (){
				Text = title,
				Style = this.Resources["ButtonStyle"] as Style,
				HeightRequest = 40
			};
			button.Clicked += ButtonTouch;
			buttons.Add (button);
			buttons = buttons.OrderBy(b=>b.Text).ToList();

			this.RefreshList ();
		}

		public void ButtonTouch(object sender, EventArgs e)
		{
			string key = (sender as Button).Text;
			string search = WebUtility.UrlEncode (tags [key]);
			string url = string.Format ("https://twitter.com/search?q={0}", search);
			Device.OpenUri (new Uri (url));
		}

		void RefreshList()
		{
			scrollViewContent.Children.Clear ();

			foreach (var button in buttons) {
				var itemGroup = new Grid ();
				itemGroup.RowDefinitions.Add (new RowDefinition (){ Height = GridLength.Auto });
				itemGroup.ColumnDefinitions.Add (new ColumnDefinition () { Width = new GridLength(1, GridUnitType.Star) });
				itemGroup.ColumnDefinitions.Add (new ColumnDefinition () { Width = new GridLength(40, GridUnitType.Absolute) });

				Grid.SetRow ((BindableObject)button, 0);
				Grid.SetColumn ((BindableObject)button, 0);
				itemGroup.Children.Add (button);

				var editButton = new Image { Aspect = Aspect.AspectFit };
				editButton.Source = ImageSource.FromFile ("edit.png");
				editButton.HeightRequest = 40;
				editButton.WidthRequest = 40;
				editButton.GestureRecognizers.Add (new TapGestureRecognizer(){ 
					Command = new Command(OnEditButtonTapped),
					CommandParameter = button.Text
				});
				editButtons.Add (editButton);

				Grid.SetRow ((BindableObject)editButton, 0);
				Grid.SetColumn ((BindableObject)editButton, 1);
				itemGroup.Children.Add (editButton);

				scrollViewContent.Children.Add (itemGroup);
			}
		}

		void OnEditButtonTapped(object args)
		{
			var key = args.ToString();
			var value = tags [key];

			txtQuery.Text = value;
			txtTag.Text = key;
		}
	}
}

