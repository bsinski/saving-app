using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SavingApp.Logic
{
    public static class SchedulesHandler
    {
       
        static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.xml");
        static XmlSerializer serializer = new XmlSerializer(typeof(List<Schedule>));

        //method for reading the plans from the file
        public static List<Schedule> readPlans()
        {
            List<Schedule> schedules;
            FileStream fs = File.OpenRead(path);
            fs.Position = 0;
            schedules = (List<Schedule>)serializer.Deserialize(fs);
            return schedules;

         }
        
        public static void savePlan(Schedule s)
        {
            List<Schedule> schedules = new List<Schedule>();
            // we are saving plan by deleting file with existing old plans and making the new one with new plans
            if (File.Exists(path))
            {
                schedules = readPlans();
                File.Delete(path);
            }
            schedules.Add(s);
            FileStream file = File.Create(path);
            file.Position = 0;
            File.SetAttributes(path, FileAttributes.Normal);
            serializer.Serialize(file, schedules);
            file.Close();
        }

        public static void UpdatePlans(Schedule s)
        {
            List<Schedule> schedules = readPlans();
            // we are also updating the information about the files by deleting and creating the new file 
            File.Delete(path);
            int index = schedules.FindIndex(sch => sch.Name == s.Name);
            if (index > -1)
            {
                schedules[index] = s;
            }
            else
            {
                schedules.Add(s);
            }
            FileStream file = File.Create(path);
            file.Position = 0;
            File.SetAttributes(path, FileAttributes.Normal);
            serializer.Serialize(file, schedules);
            file.Close();

        }
        public static void DeletePlan(Schedule s)
        {
            List<Schedule> schedules = readPlans();
            File.Delete(path);
            // same as the updating we are deleting file with the plans and creating new without the choosen plan
            int index = schedules.FindIndex(sch => sch.Name == s.Name);
            if (index > -1)
            {
                schedules.RemoveAt(index);
            }
            FileStream file = File.Create(path);
            file.Position = 0;
            File.SetAttributes(path, FileAttributes.Normal);
            serializer.Serialize(file, schedules);
            file.Close();
        }

        // following methods are for serializing and deserializing the schedules
        public static String WriteToString(Schedule s)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(s.GetType());
            StringWriter textw = new StringWriter();
            xmlSerializer.Serialize(textw, s);
            return textw.ToString();
        }
        public static Schedule ReadFromString(String s_text)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Schedule));
            TextReader reader = new StringReader(s_text);
            Schedule schedule = (Schedule)xmlSerializer.Deserialize(reader);
            return schedule;
        }



    }
}
