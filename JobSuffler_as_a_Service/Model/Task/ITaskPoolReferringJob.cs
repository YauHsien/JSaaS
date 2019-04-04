using JSaaS.Device;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSaaS.Model.Task
{
    public interface ITaskPoolReferringJob : IJob
    {
        TaskPool TaskPool { get; set; }
    }
}
