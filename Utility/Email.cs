using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class Email
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromDisplay { get; set; }
        public string FromEmailAddress { get; set; }
        public List<string> ToEmailAddressList { get; set; }
        public List<string> CCEmailAddressList { get; set; }
        public string AccountUsername { get; set; }
        public string AccountPassword { get; set; }
        public string MailServerDomain { get; set; }

        private int? _Port;
        public int? Port
        {
            get { return _Port == null ? 25 : _Port; }
            set { _Port = value; }
        }

        private bool? _EnableSSL;
        public bool? EnableSSL
        {
            get { return _EnableSSL == null ? false : _EnableSSL; }
            set { _EnableSSL = value; }
        }
        private string _Host;

        public string Host
        {
            get { return String.IsNullOrEmpty(_Host) ? "SMTPServer" : _Host; }
            set { _Host = value; }
        }


    }
}
