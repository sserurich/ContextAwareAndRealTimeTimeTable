using System;
using System.Text;
using System.Xml;
using ContextAwareAndRealTimeTimeTable.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace ContextAwareAndRealTimeTimeTable.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread threadOne= new Thread(new ThreadStart(ThreadingDemo.A));
            Thread threadTwo = new Thread(new ThreadStart(ThreadingDemo.B));
            
            threadTwo.Start();          
            //while (true)
            //{
            //    Thread.Sleep(200);
            //    threadOne.Start();     
            //}

          //  Timer timer = new Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

            Timer w = new Timer(DetermineTodaysDay, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
         

            string xmlFilePath = @"C:\Work\Current Projects\ContextAwareAndRealTimeTimeTable\ContextAwareAndRealTimeTimeTable.Demo\xmldata\cocistimetable.xml";
           // DetermineTodaysDay();
            //GetAllTeachersFromXmlFile(xmlFilePath);
           // GetAllRoomsFromXmlFile(xmlFilePath); //get all rooms from xmlfile and save them in db
           // GetAllSubjectsFromXmlFile(xmlFilePath); //get subjects from xml file and then save also 
           // WorkingWithXmlDocumentAndXpath(); //used for inserting groups
           // WorkingWithXmlDocumentAttributes();
            //WorkingWithXmlNodeClassAttributes();
            //WorkingWithXmlNodeClass();
            //WorkingWithXmlDocument();
          //  Console.ReadKey();
           // WorkingWithXmlReader();
            Console.ReadKey();

        }

        public static void Callback(object state)
        {
            Console.WriteLine("The current time is {0}", DateTime.Now);
        }
        static void WorkingWithXmlReader()
        {
            XmlReader xmlReader = XmlReader.Create("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Cube"))
                {
                    if (xmlReader.HasAttributes)
                    {
                        Console.WriteLine(xmlReader.GetAttribute("currency") + ":" + xmlReader.GetAttribute("rate"));
                    }
                }
            }
        }

        static void WorkingWithXmlDocument()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");
            foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes[2].ChildNodes[0].ChildNodes)
            {
                Console.WriteLine(xmlNode.Attributes["currency"].Value + ":" + xmlNode.Attributes["rate"].Value);

            }
        }

        static void WorkingWithXmlNodeClass()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<user name= \"John Doe\">A User node </user>");
            Console.WriteLine(xmlDoc.DocumentElement.Name);
            Console.ReadKey();
            xmlDoc.LoadXml("<group name=\"rych\">Final Year Group </group>");
            Console.WriteLine(xmlDoc.DocumentElement.Name);
            Console.WriteLine(xmlDoc.DocumentElement.InnerText);  
            //inner xml property
        }

        static void WorkingWithXmlNodeClassAttributes()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<users> <user> IneerText/InnerXml </user> </users>");
            Console.WriteLine("Inner Xml:" + xmlDocument.DocumentElement.InnerXml);
            Console.WriteLine("Outer Xml:" + xmlDocument.OuterXml);
            Console.WriteLine("InnerText:" + xmlDocument.InnerText);
        }

        static void WorkingWithXmlDocumentAttributes()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<user name=\"John Doe\" age=\"42\"> </user>");
            if (xmlDoc.DocumentElement.Attributes["age"] != null)
            {
                Console.WriteLine(xmlDoc.DocumentElement.Attributes["age"].Value);
            }
            if (xmlDoc.DocumentElement.Attributes["name"] != null)
            {
                Console.WriteLine(xmlDoc.DocumentElement.Attributes["name"].Value);
            }
            Console.ReadKey();
        }

        static void WorkingWithXmlDocumentAndXpath()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\Work\Current Projects\ContextAwareAndRealTimeTimeTable\ContextAwareAndRealTimeTimeTable.Demo\xmldata\cocistimetable.xml");
            //XmlNode titleNode = xmlDoc.SelectSingleNode("//fet/Students_List/Year");
            //if (titleNode != null)
            //{
            //    Console.WriteLine(titleNode.InnerText);
            //    Console.ReadKey();
            //}
            using (var dbContext = new ContextAwareAndRealTimeTimeTableEntitiesImport())
            {
            XmlNodeList itemNodes = xmlDoc.SelectNodes("//fet/Students_List/Year/Group");

            foreach (XmlNode itemNode in itemNodes)
            {
                XmlNode tNode = itemNode.SelectSingleNode("Name");             
                if (tNode != null){

                    int yearId = 0; int courseId = 0;
                    Console.WriteLine( tNode.InnerText);
               
                if (tNode.InnerText.Trim().EndsWith("1"))
                {
                    Console.WriteLine("ends with one");
                    yearId = 1;
                }
                else if (tNode.InnerText.Trim().EndsWith("2"))
                {
                    yearId = 2;
                }
                    else if(tNode.InnerText.Trim().EndsWith("3"))
                {
                    yearId = 3;
                }
                else if (tNode.InnerText.Trim().EndsWith("4"))
                {
                    yearId = 4;
                }

                if (tNode.InnerText.Trim().StartsWith("C"))
                {
                   
                    courseId = 1;
                }
                else if (tNode.InnerText.Trim().Substring(0,3)=="BIT")
                {
                    courseId = 2;
                }
                else if (tNode.InnerText.Trim().Substring(0,2)=="SE")
                {
                    courseId = 4;
                }
               else if (tNode.InnerText.Trim().Substring(0,4)=="BLIS")
                {
                    courseId = 6;
                }
                else if (tNode.InnerText.Trim().Substring(0,4)=="BRAM")
                {
                    courseId = 7;
                }
                else if (tNode.InnerText.Trim().Substring(0,3)=="BIS")
                {
                    courseId = 8;
                }                
                else if (tNode.InnerText.Trim().Substring(0, 4) == "DRAM")
                {
                    courseId = 9;
                }
                
                var x = new ContextAwareAndRealTimeTimeTable.Demo.Group()
                {
                    Name = tNode.InnerText,
                    CreatedOn = DateTime.Now,
                    YearId = yearId,
                    CourseId = courseId
                };
                dbContext.Groups.Add(x);
                dbContext.SaveChanges();
                      }
            }
                          
               
            }
        }

        static void GetAllRoomsFromXmlFile(string xmlfilePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlfilePath);
            using (var dbContext = new ContextAwareAndRealTimeTimeTableEntitiesImport())
            {
                XmlNodeList itemNodes = xmlDoc.SelectNodes("//fet/Rooms_List/Room");

                foreach (XmlNode itemNode in itemNodes)
                {
                    XmlNode tNode = itemNode.SelectSingleNode("Name");
                    if (tNode != null)
                    {
                        var x = new ContextAwareAndRealTimeTimeTable.Demo.Room()
                        {
                            Name = tNode.InnerText,
                            CreatedOn = DateTime.Now                           
                        };
                        dbContext.Rooms.Add(x);
                        dbContext.SaveChanges();
                    }
                }
            }

        }


        static void GetAllTeachersFromXmlFile(string xmlfilePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlfilePath);
            using (var dbContext = new ContextAwareAndRealTimeTimeTableEntitiesImport())
            {
                XmlNodeList itemNodes = xmlDoc.SelectNodes("//fet/Teachers_List/Teacher");

                foreach (XmlNode itemNode in itemNodes)
                {
                    XmlNode tNode = itemNode.SelectSingleNode("Name");
                    if (tNode != null)
                    {
                        var x = new ContextAwareAndRealTimeTimeTable.Demo.Lecturer()
                        {
                            EmployeeNumber= tNode.InnerText,
                            CreatedOn = DateTime.Now,
                            UserId = "004cfffe-59a4-4588-b778-e094f8b06aa0"
                        };
                        dbContext.Lecturers.Add(x);
                        dbContext.SaveChanges();
                    }
                }
            }

        }

        static void GetAllSubjectsFromXmlFile(string xmlfilePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlfilePath);
            using (var dbContext = new ContextAwareAndRealTimeTimeTableEntitiesImport())
            {
                XmlNodeList itemNodes = xmlDoc.SelectNodes("//fet/Subjects_List/Subject");
                int yearId = 0; int subjectId = 0;
                foreach (XmlNode itemNode in itemNodes)
                {
                    XmlNode tNode = itemNode.SelectSingleNode("Name");
                    if (tNode != null)
                    {
                        var substring = tNode.InnerText.Substring(4, 1);
                        Console.WriteLine(substring);
                        if (substring =="1")
                        {
                            yearId = 1;
                        }
                        else if (substring =="2")
                        {
                            yearId = 2;
                        }
                        else if (substring == "3")
                        {
                            yearId = 3;
                        }
                        else if (substring == "4")
                        {
                            yearId = 4;
                        }
                        else
                        {
                            yearId = 4;
                        }
                        Console.WriteLine(tNode.InnerText + "\t" +yearId + "\t" + substring);
                        var x = new ContextAwareAndRealTimeTimeTable.Demo.Subject()
                        {
                            Name = tNode.InnerText,
                            CreatedOn = DateTime.Now
                        };
                        dbContext.Subjects.Add(x);
                        dbContext.SaveChanges();
                        subjectId = x.SubjectId;

                        var subjectYear = new ContextAwareAndRealTimeTimeTable.Demo.SubjectYear()
                        {
                            SubjectId = subjectId,
                            YearId = yearId,
                            CreatedOn = DateTime.Now
                        };
                        dbContext.SubjectYears.Add(subjectYear);
                        dbContext.SaveChanges();

                        var subjectCourseGroups = GetSubjectCourseIdAndGroupIds(x.Name);
                        if (subjectCourseGroups != null)
                        {
                            var subjectCourse = new ContextAwareAndRealTimeTimeTable.Demo.CourseSubject()
                            {
                                CourseId = subjectCourseGroups.courseId,                                
                                SubjectId = subjectId,
                                CreatedOn = DateTime.Now
                            };
                            dbContext.CourseSubjects.Add(subjectCourse);
                            dbContext.SaveChanges();


                            foreach(var groupId in subjectCourseGroups.groupIds){
                                var subjectCourseGroup = new ContextAwareAndRealTimeTimeTable.Demo.CourseGroupSubject()
                                {
                                    CourseId = subjectCourseGroups.courseId,
                                    GroupId = groupId,
                                    SubjectId = subjectId,
                                    CreatedOn = DateTime.Now
                                };
                                dbContext.CourseGroupSubjects.Add(subjectCourseGroup);
                                dbContext.SaveChanges();
                            }
                        }
                    }
                }
            }

        }

        class SubjectCourseGroups {
            public int courseId { get; set; }
            public List<int> groupIds { get; set; }
        }

        static SubjectCourseGroups GetSubjectCourseIdAndGroupIds(string subject)
        {
            SubjectCourseGroups subjectcoursegroups = new SubjectCourseGroups();
            int courseId = 0;
            List<int> groupIds = new List<int>();
            using (var dbContext = new ContextAwareAndRealTimeTimeTableEntitiesImport())
            {
                if (subject.Trim().Substring(0, 3) == "CSC" || subject.Trim().Substring(0, 3) == "MTH")
                {

                    courseId = 1;

                }
                else if (subject.Trim().Substring(0, 3) == "BIT")
                {
                    courseId = 2;
                }
                else if (subject.Trim().Substring(0, 3) == "BSE")
                {
                    courseId = 4;
                }
                else if (subject.Trim().Substring(0, 3) == "BLS")
                {
                    courseId = 6;
                }
                else if (subject.Trim().Substring(0, 3) == "BRM" || subject.Trim().Substring(0, 3) == "RAM")
                {
                    courseId = 7;
                }
                else if (subject.Trim().Substring(0, 3) == "BIS")
                {
                    courseId = 8;
                }
                else if (subject.Trim().Substring(0, 3) == "DRM" || subject.Trim().Substring(0, 3) == "DLS")
                {
                    courseId = 9;
                }
                else
                {
                    courseId = 9;
                }
                var groups = (from s in dbContext.Groups where s.CourseId== courseId select s).ToList();
                if (groups != null)
                {
                    foreach (var y in groups)
                    {
                        groupIds.Add(y.GroupId);
                    }
                }

                         
            }
            subjectcoursegroups.courseId = courseId;
            subjectcoursegroups.groupIds = groupIds;
            return subjectcoursegroups;

        }
        public static void DetermineTodaysDay(object state)
        {
            DateTime activityStartDateTime = Convert.ToDateTime(DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
            DateTime activityEndDateTime = Convert.ToDateTime(DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
            Console.WriteLine(DateTime.Now + "\n"+ activityEndDateTime + "\n" + activityEndDateTime);
            Console.WriteLine("Day of the week" + DateTime.Now.DayOfWeek + "\n Day" + DateTime.Now.Day + "\n Date" + DateTime.Now.Date + "\n Hour" + DateTime.Now.Hour);

        }
    }

}
