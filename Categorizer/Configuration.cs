using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Categorizer
{
    public class Configuration
    {
        //configuation name
        public String Name { get; set; }
        //category Mapping
        public List<KeyboundCategory> Categories { get; set; }

        public String PathToDataFile { get; set; }
        

        public string getCategoryByKey(Key k)
        {
            KeyboundCategory cat= Categories.Find(x => x.key == k);
            if(cat==null)
            {
                return null;
            }
            return cat.category;
                
        }

        // save
        public static void SaveConfigurationData(Configuration data)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(Configuration));
            TextWriter sww = new StreamWriter(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) +"\\"+ data.Name + ".xml");
            xsSubmit.Serialize(sww, data);
            sww.Close();
        }

        // read
        public static Configuration ReadConfigurationData(String NameOfConfigXML)
        {
            FileStream myFileStream;
            Configuration confData = null;
            XmlSerializer mySerializer = new XmlSerializer(typeof(Configuration));
            try
            {

                if (File.Exists(NameOfConfigXML))
                {
                    // To read the file, create a FileStream.  
                    myFileStream = new FileStream(NameOfConfigXML, FileMode.Open);
                    // Call the Deserialize method and cast to the object type.  
                    confData = (Configuration)mySerializer.Deserialize(myFileStream);
                    myFileStream.Close();
                }
            }
            catch (Exception)
            {

            }
            return confData;
        }

        //generateExample
        public static void generateExampleXML()
        {
            Configuration confData = new Configuration();
            String NamefConfiguration = "Example Configuration";
                    confData.Name = NamefConfiguration;

            confData.Categories = new List<KeyboundCategory>();
            confData.Categories.Add(new KeyboundCategory(System.Windows.Input.Key.NumPad7, "Adler"));
            confData.Categories.Add(new KeyboundCategory(System.Windows.Input.Key.NumPad8, "Bär"));
            confData.Categories.Add(new KeyboundCategory(System.Windows.Input.Key.NumPad9, "Chamäleon"));
            confData.Categories.Add(new KeyboundCategory(System.Windows.Input.Key.NumPad4, "Dachs"));
            confData.Categories.Add(new KeyboundCategory(System.Windows.Input.Key.NumPad5, "Unkategoriert"));
            confData.Categories.Add(new KeyboundCategory(System.Windows.Input.Key.NumPad6, "Fuchs"));
            confData.Categories.Add(new KeyboundCategory(System.Windows.Input.Key.NumPad1, "Gans"));
            confData.Categories.Add(new KeyboundCategory(System.Windows.Input.Key.NumPad2, "Hahn"));
            confData.Categories.Add(new KeyboundCategory(System.Windows.Input.Key.NumPad3, "Igel"));

            SaveConfigurationData(confData);
            String xmlFile = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\"+ NamefConfiguration + ".xml";
            Configuration readConfData = ReadConfigurationData(xmlFile);


        }

        public Configuration()
        {

        }
    }
}
