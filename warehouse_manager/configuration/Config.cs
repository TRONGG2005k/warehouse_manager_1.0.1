using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_manager.configuration
{
    public static class Config
    {
        private static IConfigurationRoot? config;

        static Config()
        {
            config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        }

        public static string GetConnectionString(string name) =>
            config!.GetConnectionString(name)!;
    }
}
