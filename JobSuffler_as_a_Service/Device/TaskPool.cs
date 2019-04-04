using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JSaaS.Model.Task;

namespace JSaaS.Device
{
    public class TaskPool : LinkedList<ITask<IJob>>
    {
    }
}
