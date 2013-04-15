using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieDb.Shared.ServiceBus
{
    public enum QueueCreateMode
    {
        AlwaysCreate,
        IgnoreIfExists,
        DeleteIfExists
    }
}
