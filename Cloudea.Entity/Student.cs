﻿using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Entity
{
    public class Student
    {
        [Column(IsPrimary = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public DateOnly BirthDate { get; set;}
    }
}
