﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO_s.ProgressBookDTO
{
    public class AddProgressBookDto
    {
        public int Grade { get; set; }
        public bool IsAttendance { get; set; }
        public string? Notes { get; set; }
        public int LateMinute { get; set; }
    }
}
