using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Data
{
    public class ConnectionStringOption
    {
        public string SqlServer { get; set; }
        public const string Key = "ConnectionStrings"; //sabit
    }
}
