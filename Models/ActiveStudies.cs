using System;
using System.Collections.Generic;
using System.Text;

namespace APBD2.Models
{
    public class ActiveStudies
    {
        public String Name { get; set; }
        public int NumberOfStudents { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is ActiveStudies objeec))
            {
                return false;
            }

            return objeec.Name == this.Name;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}