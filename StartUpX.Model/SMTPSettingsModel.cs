using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Model
{
    public class SMTPSettingsModel
    {
        public string from { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public bool defaultCredentials { get; set; }
        public bool enableSsl { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}
