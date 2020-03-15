using System;
using System.Collections.Generic;
using System.Text;
using Person.Domain.Enums;

namespace Person.Domain.Services._Person.Models
{
    public class ReportVm
    { 
        public  string PersonFullName { get; set; }

        public  RelationType RelationType { get; set; }

        public  int  Count { get; set; }
    }
}
