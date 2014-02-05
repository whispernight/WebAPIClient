using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using System.Xml;
using System.Xml.Serialization;
//using WebAPIClient;

namespace WebAPIClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<StationInDB> stationsInDB = null;
        List<StationInDB> stationsWhenLineSelected = null;
        List<StationInDB> stationsWhenStationSelected = null;
        Station stationSelected = new Station();
        string color = "";
        
        public MainWindow()
        {
            InitializeComponent();
            //Initialize data.
            stationsInDB = new List<StationInDB>();
            stationsWhenLineSelected = new List<StationInDB>();
            stationsWhenStationSelected = new List<StationInDB>();

            //Read CSV File
            ReadCSVFile();
            //.Style.Resources.Keys = "";
            resetStationSearch();
        }

        private void resetStationSearch()
        {
            //var linesList = new List<String>() { "Red", "Blue", "Brown", "Green", "Orange", "Pink", "Yellow", "Purple" };
            //interactiveList.ItemsSource = linesList;
            interactiveList.Style = Application.Current.Resources["StyleWhiteList"] as Style;

            List<ListViewItem> coloredLinesList = new List<ListViewItem>();

            //coloredItem.Font = new System.Drawing.Font("sans-serif", 12, System.Drawing.FontStyle.Bold);
            //Red
            ListViewItem coloredItem1 = new ListViewItem();
            coloredItem1.Height = (float)20;
            coloredItem1.FontFamily = new FontFamily("sans-serif");
            coloredItem1.FontWeight = FontWeights.Bold;
            coloredItem1.FontSize = (float)12;
            coloredItem1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            coloredItem1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c60c30"));
            coloredItem1.Content = "Red";
            coloredLinesList.Add(coloredItem1);
            //Blue
            ListViewItem coloredItem2 = new ListViewItem();
            coloredItem2.Height = (float)20;
            coloredItem2.FontFamily = new FontFamily("sans-serif");
            coloredItem2.FontWeight = FontWeights.Bold;
            coloredItem2.FontSize = (float)12;
            coloredItem1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            coloredItem2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00a1de"));
            coloredItem2.Content = "Blue";
            coloredLinesList.Add(coloredItem2);
            //Brown
            ListViewItem coloredItem3 = new ListViewItem();
            coloredItem3.Height = (float)20;
            coloredItem3.FontFamily = new FontFamily("sans-serif");
            coloredItem3.FontWeight = FontWeights.Bold;
            coloredItem3.FontSize = (float)12;
            coloredItem1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            coloredItem3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#62361b"));
            coloredItem3.Content = "Brown";
            coloredLinesList.Add(coloredItem3);
            //Green
            ListViewItem coloredItem4 = new ListViewItem();
            coloredItem4.Height = (float)20;
            coloredItem4.FontFamily = new FontFamily("sans-serif");
            coloredItem4.FontWeight = FontWeights.Bold;
            coloredItem4.FontSize = (float)12;
            coloredItem1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            coloredItem4.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009b3a"));
            coloredItem4.Content = "Green";
            coloredLinesList.Add(coloredItem4);
            //Orange
            ListViewItem coloredItem5 = new ListViewItem();
            coloredItem5.Height = (float)20;
            coloredItem5.FontFamily = new FontFamily("sans-serif");
            coloredItem5.FontWeight = FontWeights.Bold;
            coloredItem5.FontSize = (float)12;
            coloredItem1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            coloredItem5.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9461c"));
            coloredItem5.Content = "Orange";
            coloredLinesList.Add(coloredItem5);
            //Pink
            ListViewItem coloredItem6 = new ListViewItem();
            coloredItem6.Height = (float)20;
            coloredItem6.FontFamily = new FontFamily("sans-serif");
            coloredItem6.FontWeight = FontWeights.Bold;
            coloredItem6.FontSize = (float)12;
            coloredItem1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            coloredItem6.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e27ea6"));
            coloredItem6.Content = "Pink";
            coloredLinesList.Add(coloredItem6);
            //Yellow
            ListViewItem coloredItem7 = new ListViewItem();
            coloredItem7.Height = (float)20;
            coloredItem7.FontFamily = new FontFamily("sans-serif");
            coloredItem7.FontWeight = FontWeights.Bold;
            coloredItem7.FontSize = (float)12;
            coloredItem1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            coloredItem7.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9e300"));
            coloredItem7.Content = "Yellow";
            coloredLinesList.Add(coloredItem7);
            //Purple
            ListViewItem coloredItem8 = new ListViewItem();
            coloredItem8.Height = (float)20;
            coloredItem8.FontFamily = new FontFamily("sans-serif");
            coloredItem8.FontWeight = FontWeights.Bold;
            coloredItem8.FontSize = (float)12;
            coloredItem1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            coloredItem8.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#522398"));
            coloredItem8.Content = "Purple";
            coloredLinesList.Add(coloredItem8);
            

            //#00a1de
            interactiveList.ItemsSource = coloredLinesList.ToList();
            
            
            dataGrid.ItemsSource = null;


        }


        //Loads all DB info in the stationsInDB object
        private void ReadCSVFile()
        {
            StationInDB item = null;
            //Read .csv
            using (CSVReader reader = new CSVReader("cta_L_stops.csv"))
            {
                CsvRow row = new CsvRow();
                int count = 0;
                while (reader.ReadRow(row))
                {
                    if (count > 0)
                    {
                        item = new StationInDB();
                        item.STOP_ID = Convert.ToInt32(row[0]);
                        item.DIRECTION_ID = row[1];
                        item.STOP_NAME = row[2];
                        item.LON = Convert.ToDouble(row[3]);
                        item.LAT = Convert.ToDouble(row[4]);
                        item.STATION_NAME = row[5];
                        item.STATION_DESCRIPTIVE_NAME = row[6];
                        item.PARENT_STOP_ID = Convert.ToInt32(row[7]);
                        item.ADA = Convert.ToInt32(row[8]);
                        item.Red = (Convert.ToInt32(row[9]) == 1) ? true : false;
                        item.Blue = (Convert.ToInt32(row[10]) == 1) ? true : false;
                        item.Brn = (Convert.ToInt32(row[11]) == 1) ? true : false;
                        item.G = (Convert.ToInt32(row[12]) == 1) ? true : false;
                        item.P = (Convert.ToInt32(row[13]) == 1) ? true : false;
                        item.Pexp = (Convert.ToInt32(row[14]) == 1) ? true : false;
                        item.Y = (Convert.ToInt32(row[15]) == 1) ? true : false;
                        item.Pink = (Convert.ToInt32(row[16]) == 1) ? true : false;
                        item.Org = (Convert.ToInt32(row[17]) == 1) ? true : false;
                        stationsInDB.Add(item);
                    }
                    count = 1;
                }
            }

        }

        private void interactiveList_LineSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                //object[] asfe= new object[1];
                //var asfe = e.AddedItems[0][1];
                string pqpq= "";
                try {
                    foreach (ListViewItem count in e.AddedItems)
                    {
                        pqpq = count.Content.ToString();
                    }
                }
                catch {
                    try
                    {
                            pqpq = e.AddedItems[0].ToString();
                    }
                    catch { 
                    
                    }
                }
                //var pr = asfe[0];
                //string asd = e.AddedItems[0].ToString();
                switch (pqpq)
                {
                    case "Red":
                        color = "#c60c30";
                        var redLineStations = stationsInDB.Where(i => i.Red == true).Select(i => i.STATION_NAME).Distinct().ToArray();
                        stationsWhenLineSelected = stationsInDB.Where(i => i.Red == true).ToList();
                        interactiveList.ItemsSource = redLineStations;
                        interactiveList.Style = Application.Current.Resources["StyleRedList"] as Style;
                        break;
                    case "Blue":
                        color = "#00a1de";
                        var blueLineStations = stationsInDB.Where(i => i.Blue == true).Select(i => i.STATION_NAME).Distinct().ToArray();
                        stationsWhenLineSelected = stationsInDB.Where(i => i.Blue == true).ToList();
                        interactiveList.ItemsSource = blueLineStations;
                        interactiveList.Style = Application.Current.Resources["StyleBlueList"] as Style;
                        break;
                    case "Brown":
                        color = "#62361b";
                        var brownLineStations = stationsInDB.Where(i => i.Brn == true).Select(i => i.STATION_NAME).Distinct().ToArray();
                        stationsWhenLineSelected = stationsInDB.Where(i => i.Brn == true).ToList();
                        interactiveList.ItemsSource = brownLineStations;
                        interactiveList.Style = Application.Current.Resources["StyleBrownList"] as Style;
                        break;
                    case "Green":
                        color = "#009b3a";
                        var greenLineStations = stationsInDB.Where(i => i.G == true).Select(i => i.STATION_NAME).Distinct().ToArray();
                        stationsWhenLineSelected = stationsInDB.Where(i => i.G == true).ToList();
                        interactiveList.ItemsSource = greenLineStations;
                        interactiveList.Style = Application.Current.Resources["StyleGreenList"] as Style;
                        break;
                    case "Orange":
                        color = "#f9461c";
                        var orangeLineStations = stationsInDB.Where(i => i.Org == true).Select(i => i.STATION_NAME).Distinct().ToArray();
                        stationsWhenLineSelected = stationsInDB.Where(i => i.Org == true).ToList();
                        interactiveList.ItemsSource = orangeLineStations;
                        interactiveList.Style = Application.Current.Resources["StyleOrangeList"] as Style;
                        break;
                    case "Purple":
                        color = "#522398";
                        var purpleLineStations = stationsInDB.Where(i => i.P == true).Select(i => i.STATION_NAME).Distinct().ToArray();
                        stationsWhenLineSelected = stationsInDB.Where(i => i.P == true).ToList();
                        interactiveList.ItemsSource = purpleLineStations;
                        interactiveList.Style = Application.Current.Resources["StylePurpleList"] as Style;
                        break;
                    case "Pink":
                        color = "#e27ea6";
                        var pinkLineStations = stationsInDB.Where(i => i.Pink == true).Select(i => i.STATION_NAME).Distinct().ToArray();
                        stationsWhenLineSelected = stationsInDB.Where(i => i.Pink == true).ToList();
                        interactiveList.ItemsSource = pinkLineStations;
                        interactiveList.Style = Application.Current.Resources["StylePinkList"] as Style;
                        break;
                    case "Yellow":
                        color = "#f9e300";
                        var yellowLineStations = stationsInDB.Where(i => i.Y == true).Select(i => i.STATION_NAME).Distinct().ToArray();
                        stationsWhenLineSelected = stationsInDB.Where(i => i.Y == true).ToList();
                        interactiveList.ItemsSource = yellowLineStations;
                        interactiveList.Style = Application.Current.Resources["StyleYellowList"] as Style;
                        break;

                    default:
                        //var stationClicked = stationsInDB.Where(i => i.STATION_NAME.CompareTo(e.AddedItems[0].ToString()) == 0).ToArray(); ; //Request Station Information and send it to main window to display times!
                        stationsWhenStationSelected = stationsWhenLineSelected.Where(i => i.STATION_NAME.CompareTo(e.AddedItems[0].ToString()) == 0).ToList();
                        //usergrid.ItemsSource = stationsWhenStationSelected;
                        stationSelected.key = WebAPIClient.GlobalVars.APIkey;
                        stationSelected.mapid = stationsWhenStationSelected.FirstOrDefault().PARENT_STOP_ID;
                        GetRequestedStationID(stationSelected);
                        dataGrid.RowBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
                        break;
                }
            }
        }

        private void ResetSearch_Click(object sender, RoutedEventArgs e)
        {
            resetStationSearch();
        }


        private void GetRequestedStationID(Station station)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://lapi.transitchicago.com/api/1.0/ttarrivals.aspx");

            // Add an Accept header for XML format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/xml"));
            
            HttpResponseMessage resp = client.GetAsync("?key="+station.key+"&max=5&mapid="+station.mapid, new HttpCompletionOption()).Result;

            if (resp.IsSuccessStatusCode)
            {
                // Parse the response body. 
                var ms = new MemoryStream(resp.Content.ReadAsByteArrayAsync().Result);
                var doc = new XmlDocument();
                doc.Load(ms);
                var serializer = new XmlSerializer(typeof(ctatt));
                ctatt finaltrains = new ctatt();
                var trains = (ctatt)serializer.Deserialize(new StringReader(doc.OuterXml));
                finaltrains = trains;
                txtLastUpdate.Content = "Last updated at: " + DateTime.ParseExact(finaltrains.Trains.FirstOrDefault().prdt, WebAPIClient.GlobalVars.TimeFormat, WebAPIClient.GlobalVars.CultureProvider).ToString("HH:mm:ss");
                List<showTrains> finalShowTrains = new List<showTrains>();
                finalShowTrains = finaltrains.Trains.Select(item => new showTrains(){
                    staDirection = item.stpDe, timeLeft = DateTime.ParseExact(item.arrT, WebAPIClient.GlobalVars.TimeFormat, WebAPIClient.GlobalVars.CultureProvider).Subtract(DateTime.ParseExact(item.prdt, WebAPIClient.GlobalVars.TimeFormat, WebAPIClient.GlobalVars.CultureProvider)).Minutes
                    }).ToList();
                dataGrid.ItemsSource = finalShowTrains;
                
            }
            else
            {
                MessageBox.Show("Error Code" + resp.StatusCode + " : Message - " + resp.ReasonPhrase);
            }




        }

    }
}