using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace APBD2.Models
{
    public class University
    {
        public University()
        {
            Students = new HashSet<Student>();
            CreationDate = DateTime.Now.ToString("yyyy-mm-dd");
        }

        [XmlAttribute]
        public string Author { get; set; }

        [JsonPropertyName("CreatedAt")]

        [XmlAttribute(AttributeName = "CreatedAt")]
        public string CreationDate { get; set; }

       public HashSet<Student> Students { get; set; }

        public HashSet<ActiveStudies> ActiveStudies { get; set; }

        public ActiveStudies getActiveStudies(ActiveStudies objeec)
        {
            if(ActiveStudies.Contains(objeec))
            {
                foreach(ActiveStudies activeStudy in ActiveStudies)
                {
                    if(objeec.Equals(activeStudy))
                    {
                        return activeStudy;
                    }
                }
                //return null;
            }
            return null;
        }
    }
}
