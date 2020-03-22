using APBD2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace APBD2
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputPath = args.Length > 0 ? args[0] : @"Files\data.csv";
            var outputPath = args.Length > 1 ? args[1] : @"Files\result";
            var outputType = args.Length > 2 ? args[2] : "xml";

            Console.WriteLine($"{inputPath}\n{outputPath}\n{ outputType}");
            try
            {
                if (!File.Exists(inputPath))
                    throw new FileNotFoundException("ERR", inputPath.Split("\\")[^1]); //1st index from the end

                var university = new University
                {
                    Author = "Weronika Nurzyńska"
                };

                foreach (var line in File.ReadAllLines(inputPath))
                {
                    //Console.WriteLine(line);
                    //File.AppendAllText(outputPath, line + "\n");
                    var splitted = line.Split(",");
                    if (splitted.Length < 9)
                    {
                        File.AppendAllText(@"Files\Log.txt", $"{DateTime.UtcNow} ERR not enough information in line {line}\n");
                        continue;
                    }



                    var stud = new Student
                    {


                        FirstName = splitted[0],
                        LastName = splitted[1],
                        Index = splitted[4],
                        DateOfBirth = splitted[5],
                        Email = splitted[6],
                        MotherName = splitted[7],
                        FatherName = splitted[8],
                        StudyLevel = new Studies
                        {
                            StudyName = splitted[2],
                            StudyMode = splitted[3]
                        }
                    };


                    var studyActive = new ActiveStudies
                    {
                        Name = splitted[2],
                        NumberOfStudents = 1
                    };

                    if(university.getActiveStudies(studyActive)!=null)
                    {
                        university.getActiveStudies(studyActive).NumberOfStudents++;
                    } else
                    {
                        university.ActiveStudies.Add(studyActive);
                    }



                    university.Students.Add(stud);
                }

                //xml
                using var writer = new FileStream($"{outputPath}.{outputType}", FileMode.Create);
                var serializer = new XmlSerializer(typeof(University));
                serializer.Serialize(writer, university);

                //json
                var jsonString = JsonSerializer.Serialize(university);
                File.WriteAllText($"{outputPath}.json", jsonString);

            }
            catch (FileNotFoundException e)
            {
                File.AppendAllText(@"Files\Log.txt", $"{DateTime.UtcNow} {e.Message} File not found ({e.FileName})\n");   //CTRL + K + C (U)
            }
        }
    }
}
