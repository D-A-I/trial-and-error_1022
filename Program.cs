using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using trial_and_error_1028;

namespace trial_and_error_1022 {
    /// <summary>
    /// CRUD コンソールアプリ
    /// </summary>
    class Program {
        static void Main (string[] args) {
            IServiceCollection serviceCollection = new ServiceCollection();

            var config = GetConfiguration ();
            var conStr = config.GetConnectionString ("kurumiDB");

            Console.WriteLine (conStr);
        }

        /// <summary>
        /// ConfigurationBuilder の作成
        /// </summary>
        /// <returns></returns>
        static IConfiguration GetConfiguration () {
            var configBuilder = new ConfigurationBuilder ();

            // appsettings.json へのパスを通す
            configBuilder.SetBasePath (Directory.GetCurrentDirectory ());
            configBuilder.AddJsonFile (@"appsettings.json");

            return configBuilder.Build ();
        }
    }
}