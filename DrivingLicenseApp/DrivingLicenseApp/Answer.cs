using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingLicenseApp
{
    class Answer
    {
        public int Question { get; set; }
        public override string ToString()
        {
            return Question + "\n";
        }
    }
}
