﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.CareerCounselling
{
    public class InstructorSubjectItemModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public int InstructorId { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}
