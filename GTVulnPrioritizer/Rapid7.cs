using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVulnPrioritizer
{
    public class Rapid7 : IVulnerability
    {
        public List<Vulnerability> GetVulnerabilities()
        {
            string endpoint = "https://localhost:3780/api/3/vulnerabilities/{id}";

            throw new NotImplementedException();
        }

        public List<Vulnerability> GetVulnerabilities(string e)
        {
            throw new NotImplementedException();
        }

        public Vulnerability GetVulnerability()
        {
            string endpoint = "https://localhost:3780/api/3/vulnerabilities";
            throw new NotImplementedException();
        }

        public Vulnerability GetVulnerability(string e)
        {
            throw new NotImplementedException();
        }
    }
}
