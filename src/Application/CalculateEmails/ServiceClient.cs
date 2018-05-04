﻿using CalculateEmails.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails
{
    class ServiceClient
    {
        protected ICalculateEmailsWCFService  Client
        {
            get
            {
                string address = MasterConfiguration.MConfiguration.Configuration["Address"];
                NetTcpBinding netTcpBinding = new NetTcpBinding();
                netTcpBinding.CloseTimeout = TimeSpan.FromMinutes(20);
                ChannelFactory<ICalculateEmailsWCFService> factory = new ChannelFactory<ICalculateEmailsWCFService>(netTcpBinding, new EndpointAddress(address));

                ICalculateEmailsWCFService proxy = factory.CreateChannel();

                return proxy;
            }
        }


        public void GetData()
        {
            var x=this.Client.GetData(3);
        }

    }
}