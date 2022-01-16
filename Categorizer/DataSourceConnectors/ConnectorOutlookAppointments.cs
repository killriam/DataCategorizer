using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;


namespace Categorizer.DataSourceConnectors
{
    public class ConnectorOutlookAppointments : IDataSourceConnector
    {
        public List<MetaData<AppointmentItem>> AppointmentsData;
        public int elementIndex = 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ConnectorOutlookAppointments(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            AppointmentsData = new List<MetaData<AppointmentItem>>();


        }

        public void readAppointmentAsMetaData()
        {
            foreach (AppointmentItem i in GetAllCalendarItems())
            {
                AppointmentsData.Add(new MetaData<AppointmentItem>(i));
            }

        }

        public Items GetAllCalendarItems()
        {
            Application oApp = null;
            NameSpace mapiNamespace = null;
            MAPIFolder CalendarFolder = null;
            Items outlookCalendarItems = null;

            oApp = new Application();
            mapiNamespace = oApp.GetNamespace("MAPI"); ;
            CalendarFolder = mapiNamespace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderCalendar);
            outlookCalendarItems = CalendarFolder.Items;
            outlookCalendarItems.IncludeRecurrences = false;


            const string DateFormat = "dd/MM/yyyy HH:mm";
            string filter = string.Format("[Start] >= '{0}' AND [Start] < '{1}'", StartDate.ToString(DateFormat), EndDate.ToString(DateFormat));
            outlookCalendarItems = outlookCalendarItems.Restrict(filter);

            return outlookCalendarItems;
        }

        public object getCurrentElement()
        {
            return (elementIndex >= 0 && elementIndex < AppointmentsData.Count()) ? this.AppointmentsData.ElementAt(elementIndex).Data : null;

        }

        public void setCategoryOfCurrentElement(string newcategory)
        {
            this.AppointmentsData.ElementAt(elementIndex).Catagory = newcategory;
        }

        public void MovetoNextElement()
        {
            if (elementIndex < AppointmentsData.Count)
            {
                elementIndex++;
            }
        }

        public int getCurrentElementIndex()
        {
            return elementIndex;
        }

    }
}
