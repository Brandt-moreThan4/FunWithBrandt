﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Diagnostics;

namespace FunWithBrandt.Models
{
    public class DataReader
    {
        public static List<KnowledgeRecord> KnowledgeRead()
        {

            var records = new List<KnowledgeRecord>();
            //var path = @"C:\Users\bgreen3\Desktop\PersonalWebsite\wwwroot\Data\KnowledgeRepoTable.csv";
            var path = Directory.GetCurrentDirectory() + @"\wwwroot\Data\KnowledgeRepoTable.csv";
            var line = string.Empty; string [] splitString;
            KnowledgeRecord record;

            if (File.Exists(path))
            {
                using (StreamReader sw = new StreamReader(path))
                {
                    line = sw.ReadLine();
                    while (line != null)
                    {
                        splitString = line.Split(",");
                        record = new KnowledgeRecord();                        
                        record.Person = splitString[0];
                        record.Institution = splitString[1];
                        record.Description = splitString[2];
                        record.Source = splitString[3];
                        record.Keywords = splitString[4];
                        records.Add(record);
                        line = sw.ReadLine();
                    }                    
                }
            }

            return records;

        }

        public static string ReadCodeText(string fileName)
        {
            string tempString = string.Empty;
            var path = Directory.GetCurrentDirectory() + @"\wwwroot\ProgramText\" + fileName + ".txt"; ;
            var line = string.Empty;

            if (File.Exists(path))
            {
                using (StreamReader sw = new StreamReader(path))
                {
                    tempString = sw.ReadToEnd();
                    ////line = sw.ReadLine();
                    //while (line != null)
                    //{
                    //    tempString += line;
                    //}
                }

            }
            else
            {
                Debug.Print("File Does not exist");
            }
            return tempString;
        }

    }
    
}
