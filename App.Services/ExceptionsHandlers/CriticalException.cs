using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.ExceptionsHandlers
{
    public class CriticalException(string message) : Exception(message)
    {
    }
}
