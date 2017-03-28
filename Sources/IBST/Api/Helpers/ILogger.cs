using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.IbanStore.Api.Helpers
{
    public interface ILogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(Exception exception);
    }
}
