using GTVulnPrioritizer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        CreateHostBuilder().Build().Run();

    }
    public static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webHost => {
            webHost.UseStartup<Startup>();
        });
    }
  
    public static List<Dictionary<string, object>> FormatForTraining(List<Vulnerability> vulnerabilities)
    {
        return vulnerabilities.Select(v => new Dictionary<string, object>
        {
            { "CVE", v.CVE },
            { "Description", v.Description },
            { "Severity", v.Severity },
            { "CVSSv3", v.CVSSv3 },
            { "EPSS", v.EPSS },
            { "Age", v.Age },
            { "FirstDetected", v.FirstDetected },
            { "Updated", v.Updated },
            { "HasExploit", v.HasExploit },
            { "HasKnownThreats", v.HasKnownThreats },
            { "HasAssociatedAlerts", v.HasAssociatedAlerts },
            { "RelatedSoftware", v.RelatedSoftware },
            { "ExposedMachines", v.ExposedMachines }
        }).ToList();
        }
    }