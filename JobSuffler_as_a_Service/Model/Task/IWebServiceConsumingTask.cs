using System;
using System.Collections.Generic;
using System.Text;

namespace JSaaS.Model.Task
{
    public interface IWebServiceConsumingTask
    {
        string WebServiceEndPoint { get; set; }
        string WebServiceResultContentType { get; set; }
        void ConsumeWebServiceEndPoint();
    }
}
