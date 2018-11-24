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
        /// <summary>
        /// kurumi-ap用のデータを取得 or 登録する
        /// </summary>
        /// <remarks>
        /// ・引数無し.. ViewのTASK_OF_GROUPSと同内容を出力する
        /// ・json形式の引数あり.. TASKSテーブルにデータを登録する。keyが重複するものは更新する（詳細は以下）
        ///   GROUP_ID, STATUS, TASK_NAME, PIC（null許可）, PERIOD（null許可）
        /// </remarks>
        /// <param name="args"></param>
        static void Main(string[] args) {
            // DIの準備
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // IServiceProvider取得
            var provider = serviceCollection.BuildServiceProvider();

            // 登録したインスタンスの利用
            var config = provider.GetService<IConfigurationRoot>();
            Console.WriteLine(config.GetConnectionString("kurumi"));

            /* インターフェースを使用するように変更 */
            var adorer = new Adorer(config);
            var tasks = adorer.GetAll();

            /* tasksを表示する */
            tasks.ForEach(x => Console.WriteLine($"{x.GroupId}, {x.GroupName}, {x.TaskId}, {x.Status}, {x.Content}, {x.Pic}, {x.Period}"));
        }

        /// <summary>
        /// DIの準備
        /// </summary>
        /// <param name="serviceCollection"></param>
        private static void ConfigureServices(IServiceCollection services) {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional : true)
                // 同名のキーが設定されている場合あと勝ちになる（この場合、json < 環境変数）
                // .AddEnvironmentVariables()
                .Build();

            // ライフサイクルをSingletonで登録
            services.AddSingleton(configuration);
        }
    }
}