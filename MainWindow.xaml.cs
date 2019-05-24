using EarthquakeApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace EarthquakeApp
{
    public partial class MainWindow : Window
    {
        private string _url;
        public MainWindow()
        {
            InitializeComponent();
            _url = "https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson&orderby=time&limit=";
        }

        private void GetButtonClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(amountTextBox.Text, out int amount) && amount >= 0)
            {
                EarthquakeService earthquakeInfo = GetEarthquakes(amount);

                List<ShortInfo> information = new List<ShortInfo>();


                for (int i = 0; i < amount; i++)
                {
                    information.Add(new ShortInfo
                    {
                        Depth = earthquakeInfo.Features[i].Information.Depth,
                        Magnitude = earthquakeInfo.Features[i].Information.Magnitude,
                        Place = earthquakeInfo.Features[i].Information.Place,
                    });
                    if (!(earthquakeInfo.Features[i].Information.Time is null))
                    {
                        double time = (double)earthquakeInfo.Features[i].Information.Time;
                        information[i].Time = ConvertFromUnixTimestamp(time);
                    }
                    
                }

                dataGrid.ItemsSource = information;
            }

        }

        private DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            epoch = epoch.AddMilliseconds(timestamp).ToLocalTime();
            return epoch;
        }

        private EarthquakeService GetEarthquakes(int amount)
        {
            WebClient client = new WebClient();
            string json = "";

            using (Stream stream = client.OpenRead(new Uri(_url + amount.ToString())))
            {
                using (var reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        json += line;
                    }
                }
            }

            EarthquakeService earthquakeService = JsonConvert.DeserializeObject<EarthquakeService>(json);
            return earthquakeService;
        }
    }
}
