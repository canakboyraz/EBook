﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Utilities.Interfaces
{
    public interface IResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }
    }
}
