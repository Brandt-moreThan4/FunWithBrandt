﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Diagnostics;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using static FunWithBrandt.Models.Data.EPPlusUtils;

namespace FunWithBrandt.Models.Data
{
    public class DataReader
    {

        public static List<KnowledgeRecord> GetKnowledge()
        {
            var knowledgeRecords = new List<KnowledgeRecord>(); KnowledgeRecord knowledgeRec;
            var dbPath = Directory.GetCurrentDirectory() + @"\wwwroot\Data\websiteDB.xlsx";

            if (File.Exists(dbPath))
            {
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                FileInfo fi = new FileInfo(dbPath);

                using (ExcelPackage excelPack = new ExcelPackage(fi))
                {
                    var knowledgeWs = excelPack.Workbook.Worksheets["Knowledge"];
                    var lastRow = GetLastRow(knowledgeWs);
                    for (int i = 2; i <= lastRow; i++)
                    {
                        knowledgeRec = new KnowledgeRecord();
                        knowledgeRec.KnowledgeId = Convert.ToInt32(knowledgeWs.Cells[i, 1].Value.ToString());
                        knowledgeRec.Person_Institution = knowledgeWs.Cells[i, 2].Value.ToString();
                        knowledgeRec.Description = knowledgeWs.Cells[i, 3].Value.ToString();
                        knowledgeRec.Source = knowledgeWs.Cells[i, 4].Value.ToString();
                        knowledgeRec.Keywords = knowledgeWs.Cells[i, 5].Value.ToString();
                        knowledgeRecords.Add(knowledgeRec);
                    }
                }
            }
            return knowledgeRecords;
        }

        public static List<Book> GetBooks()
        {
            var books = new List<Book>(); Book book;
            var dbPath = Directory.GetCurrentDirectory() + @"\wwwroot\Data\websiteDB.xlsx";

            if (File.Exists(dbPath))
            {
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                FileInfo fi = new FileInfo(dbPath);

                using (ExcelPackage excelPack = new ExcelPackage(fi))
                {
                    var bookWs = excelPack.Workbook.Worksheets["Books"];
                    var lastRow = GetLastRow(bookWs);
                    for (int i = 2; i <= lastRow; i++)
                    {
                        book = new Book();
                        book.BookId = Convert.ToInt32(bookWs.Cells[i, 1].Value.ToString());
                        book.Title = bookWs.Cells[i, 2].Value.ToString();
                        book.Author = bookWs.Cells[i, 3].Value.ToString();
                        book.CoverImageName = bookWs.Cells[i, 4].Value.ToString();
                        book.CoverDescription = bookWs.Cells[i, 5].Value.ToString();
                        book.Content = bookWs.Cells[i, 6].Value.ToString();
                        books.Add(book);                          
                    }
                }
            }
            return books;
        }

        public static List<ProgramPost> GetPrograms()
        {
            var programs = new List<ProgramPost>(); ProgramPost program;
            var dbPath = Directory.GetCurrentDirectory() + @"\wwwroot\Data\websiteDB.xlsx";

            if (File.Exists(dbPath))
            {
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                FileInfo fi = new FileInfo(dbPath);

                using (ExcelPackage excelPack = new ExcelPackage(fi))
                {
                    var programWs = excelPack.Workbook.Worksheets["Programs"];
                    var lastRow = GetLastRow(programWs);
                    for (int i = 2; i <= lastRow; i++)
                    {
                        program = new ProgramPost();
                        program.ProgramId = Convert.ToInt32(programWs.Cells[i, 1].Value.ToString());
                        program.Code = programWs.Cells[i, 2].Value.ToString();
                        program.Description = programWs.Cells[i, 3].Value.ToString();
                        program.Language = programWs.Cells[i, 4].Value.ToString();
                        program.Source = programWs.Cells[i, 5].Text;
                        program.Title = programWs.Cells[i, 6].Value.ToString();
                        programs.Add(program);
                    }
                }
            }
            return programs;
        }



        public static List<string> ReadBlogText(string fileName)
        {
            var blogContentPath = GetRootPath() + @"BlogContent\" + fileName + ".txt";

            var content = new List<string>();
            var line = string.Empty;

            if (File.Exists(blogContentPath))
            {
                using (StreamReader sw = new StreamReader(blogContentPath))
                {
                    line = sw.ReadLine();
                    while (line != null)
                    {
                        content.Add(line);
                        line = sw.ReadLine();
                    }

                }

            }
            else
            {
                Debug.Print("File Does not exist");
            }
            return content;


            
        }

        public static string ReadCodeText(string fileName)
        {
            string tempString = string.Empty;
            var codeTxtPath = GetRootPath() + @"ProgramText\" + fileName + ".txt";
            
            var line = string.Empty;
            if (File.Exists(codeTxtPath))
            {
                using (StreamReader sw = new StreamReader(codeTxtPath))
                {
                    tempString = sw.ReadToEnd();

                }

            }
            else
            {
                Debug.Print("File Does not exist");
            }
            return tempString;
        }

        private static string GetRootPath()
        {
            return Directory.GetCurrentDirectory() + @"\wwwroot\";
        }

    }
    
}
