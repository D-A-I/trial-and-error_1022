using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using trial_and_error_1028;

namespace trial_and_error_1022
{
    /// <summary>
    /// CRUD コンソールアプリ
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // DIの準備
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // IServiceProvider取得
            var provider = serviceCollection.BuildServiceProvider();

            // 登録したインスタンスの利用
            var config = provider.GetService<IConfigurationRoot>();
            Console.WriteLine(config.GetConnectionString("kurumi"));

            var adorer = new Adorer(config);

            #region 
            /*
            var config = GetConfiguration ();
            var conStr = config.GetConnectionString ("kurumiDB");
            */
            #endregion
        }

        /// <summary>
        /// DIの準備
        /// </summary>
        /// <param name="serviceCollection"></param>
        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional : true)
                // 同名のキーが設定されている場合あと勝ちになる（この場合、json < 環境変数）
                // .AddEnvironmentVariables()
                .Build();

            // ライフサイクルをSingletonで登録
            services.AddSingleton(configuration);
        }

        #region
        /*
        /// <summary>
        /// ConfigurationBuilder の作成
        /// </summary>
        /// <returns></returns>
        static IConfiguration GetConfiguration () {
            var configBuilder = new ConfigurationBuilder ()
                .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile (@"appsettings.json");

            return configBuilder.Build ();
        }
        */
        #endregion
    }
}