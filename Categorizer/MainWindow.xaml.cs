using Categorizer.DataSourceConnectors;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;



namespace Categorizer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Configuration configData;

        IDataSourceConnector dataconnectorList; //temp solution

        List<Key> keys = new List<Key>
        {
            Key.NumPad7,
            Key.NumPad8,
            Key.NumPad9,
            Key.NumPad4,
            Key.NumPad5,
            Key.NumPad6,
            Key.NumPad1,
            Key.NumPad2,
            Key.NumPad3,
        };



        public MainWindow()
        {
            InitializeComponent();

            Configuration.generateExampleXML();





        }

        private void BtLoadConfiguration_Click(object sender, RoutedEventArgs e)
        {
            String NamefConfiguration = "Example Configuration";
            // read xml
            configData = Configuration.ReadConfigurationData(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + NamefConfiguration + ".xml");

            // bound to key labels & establish key short cuts
            foreach (Key k in keys)
            {
                MapCategoryKey2Button(k);
            }



        }

        private void MapCategoryKey2Button(Key k)
        {
            string category = null;
            category = configData.getCategoryByKey(k);
            if (category != null)
            {
                KeyGesture kg = new KeyGesture(k);
                SimpleCommand sc = new SimpleCommand();
                sc.Executed += SimpleCommand_OnExecuted;
                KeyBinding kb = new KeyBinding(sc, kg);

                switch (k)
                {
                    case Key.NumPad0:


                        btCategoryTopLeft.Content = category;
                        break;
                    case Key.NumPad1:


                        btCategoryBottomLeft.Content = category;

                        break;
                    case Key.NumPad2:


                        {
                            btCategoryBottomMiddle.Content = category;

                        }
                        break;
                    case Key.NumPad3:


                        {
                            btCategoryBottomright.Content = category;
                        }
                        break;
                    case Key.NumPad4:


                        {
                            btCategoryLeftCenter.Content = category;
                        }
                        break;
                    case Key.NumPad5:


                        {

                        }
                        break;
                    case Key.NumPad6:


                        {
                            btCategoryRightCenter.Content = category;
                        }
                        break;
                    case Key.NumPad7:


                        {
                            btCategoryTopLeft.Content = category;
                        }
                        break;
                    case Key.NumPad8:


                        {
                            btCategoryTopMiddle.Content = category;
                        }
                        break;
                    case Key.NumPad9:


                        {
                            btCategoryTopright.Content = category;
                        }
                        break;

                    default:
                        category = "unkown";
                        break;
                }
                kb.CommandParameter = category;
                InputBindings.Add(kb);
            }

        }
        private void MyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void SimpleCommand_OnExecuted(object sender, object e)
        {
            // MessageBox.Show("SimpleCommand Executed:" + (string)e);

            // categorize data
            dataconnectorList.setCategoryOfCurrentElement((string)e);
            // enhance traininig

            // load next data element
            dataconnectorList.MovetoNextElement();
            displayDataElement();
        }

        private void BtLoadData_Click(object sender, RoutedEventArgs e)
        {
            LoadExcelData();    
            
            //LoadOutlookData();
        }

        private void LoadExcelData()
        {
            string filname = @"C:\Users\killr\OneDrive\Dokumente\SWProjects\budgetTracking\Cashglow.xlsx";
        
            // load data
            dataconnectorList = new ConnectorExcelData(filname);
            (dataconnectorList as ConnectorExcelData).readDataFromFile();


            tbIndexDisplay.Text = "0";

            // bind data
            displayDataElement();
        }

        private void LoadOutlookData()
        {
            DateTime today = DateTime.Today;
            DateTime startOfCurrentWeek = today.AddMonths(-1).AddDays(-today.Day + 1);
            DateTime endOfCurrentWeek = startOfCurrentWeek.AddDays(30);
            // load data
            dataconnectorList = new ConnectorOutlookAppointments(startOfCurrentWeek, endOfCurrentWeek);
            (dataconnectorList as ConnectorOutlookAppointments).readAppointmentAsMetaData();

            tbIndexDisplay.Text = "0";

            // bind data
            displayDataElement();
        }

        private void displayDataElement()
        {

            tbIndexDisplay.Text = dataconnectorList.getCurrentElementIndex() + "";
            
            List<object> tempList = new List<object>();
            tempList.Add(dataconnectorList.getCurrentElement());
            dgElementDetails.ItemsSource = tempList;
        }
    }
}