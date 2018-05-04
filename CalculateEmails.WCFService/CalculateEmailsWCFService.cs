﻿using CalculateEmails.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CalculateEmails.WCFService
{
    public class CalculateEmailsWCFService : ICalculateEmailsWCFService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }       
    }
}
