using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace trial_and_error_1022 {
    /// <summary>
    /// APIクライアントを作成する
    /// MySqlへCRUDする（GET / POST / PUT / DELETE）
    /// </summary>
    class Program {
        static void Main (string[] args) {
            var config = GetConfiguration ();
            var conStr = config.GetConnectionString ("kurumiDB");

            Console.WriteLine (conStr);
        }

        /// <summary>
        /// ConfigurationBuilderの作成
        /// </summary>
        /// <returns></returns>
        static IConfiguration GetConfiguration () {
            var configBuilder = new ConfigurationBuilder ();

            // appsettings.jsonへのパスを通す
            configBuilder.SetBasePath (Directory.GetCurrentDirectory ());
            configBuilder.AddJsonFile (@"appsettings.json");

            return configBuilder.Build ();
        }
    }
}