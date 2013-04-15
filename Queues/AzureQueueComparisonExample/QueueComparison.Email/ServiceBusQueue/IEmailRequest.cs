using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MovieDb.Shared
{
    [ServiceContract]
    public interface IEmailRequest
    {
        [OperationContract(IsOneWay = true)]
        [ReceiveContextEnabled]
        void OnEmailRequest(string emailContent);
    }
}
