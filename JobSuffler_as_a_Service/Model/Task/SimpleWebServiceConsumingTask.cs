using System;
using System.Collections.Generic;
using System.Text;

namespace JSaaS.Model.Task
{
    public class SimpleWebServiceConsumingTask : AbstractTask<IJob>, IWebServiceConsumingTask
    {
        string IWebServiceConsumingTask.WebServiceEndPoint { get => _webServiceEndPoint; set => _webServiceEndPoint = value; }
        string IWebServiceConsumingTask.WebServiceResultContentType { get => _webServiceResultContentType; set => _webServiceResultContentType = value; }
        public override IList<IJob> Jobs { get => _jobs; set => _jobs = value; }

        private string _webServiceEndPoint;
        private string _webServiceResultContentType;
        private IList<IJob> _jobs;

        public override void PerformTask()
        {
            foreach (IJob job in _jobs)
            {
                job.Work();
            }
        }

        void IWebServiceConsumingTask.ConsumeWebServiceEndPoint()
        {
            throw new NotImplementedException();
        }

        public override void PerformTask(IJob job)
        {
            _jobs.Clear();
            _jobs.Add(job);
            PerformTask();
        }
    }
}
