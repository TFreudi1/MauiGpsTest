

namespace MauiGpsTest
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            Geolocation.ListeningFailed += Geolocation_ListeningFailed;
            Geolocation.LocationChanged += Geolocation_LocationChanged;
        }
        private void Geolocation_ListeningFailed(object? sender, GeolocationListeningFailedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public ContentPage GetMainPage()
        {
            return this;
        }

        private void Geolocation_LocationChanged(object? sender, GeolocationLocationChangedEventArgs e)
        {
            count++;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                lblCnt.Text = count.ToString();
            });
            

            Location location = e.Location;
            if (location != null)
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Foreground: {Geolocation.IsListeningForeground}");
        }
        private async void OnCounterClicked(object sender, EventArgs e)
        {
            PermissionStatus permissionStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            if (permissionStatus == PermissionStatus.Granted)
            {
                var request = new GeolocationListeningRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(1));
                var success = await Geolocation.StartListeningForegroundAsync(request);
                string status = success
            ? "Started listening for foreground location updates"
            : "Couldn't start listening";
                Console.WriteLine($"Status: {status}, Foreground: {Geolocation.IsListeningForeground}");
                DeviceDisplay.Current.KeepScreenOn = true;
            }
        }
    }

}
