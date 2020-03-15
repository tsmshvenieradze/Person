using System;
using System.Collections.Generic;
using System.Text;

namespace Person.Domain
{
    public class Entity
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public  bool IsDeleted { get; set; }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
