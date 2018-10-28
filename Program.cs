using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace trial_and_error_1022
{
    /// <summary>
    /// MySqlにCRUDする（GET / POST / PUT / DELETE）
    /// ・MySqlをインストールする -> OK
    /// ・MySqlWorkbenchをインストールする -> OK
    /// ・MySqlにテーブルを作成する -> OK
    /// ・MySqlにデータを投入する -> (一旦)OK
    /// ・別プロジェクトを作る -> OK
    /// ・MySqlからscaffoldする
    /// ・DIで接続文字列を渡す
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var config = GetConfiguration();
            var conStr = config.GetConnectionString("kurumiDB");

            Console.WriteLine(conStr);
        }

        /// <summary>
        /// ConfigurationBuilderの作成
        /// </summary>
        /// <returns></returns>
        static IConfiguration GetConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            
            // appsettings.jsonへのパスを通す
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile(@"appsettings.json");
            
            return configBuilder.Build();
        }
    }
}
