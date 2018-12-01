using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using trial_and_error_1028;
using trial_and_error_1028.kurumi;

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
            /* ServiceCollection -> デフォルトのDIコンテナ"ServiceProvider"の素材
             * Addxxx.. で、DIする必要があるサービスを登録する */
            var serviceCollection = new ServiceCollection();

            // DIの準備。環境変数から(Development / Production)を受け取っておく
            ConfigureServices(serviceCollection, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

            // DIコンテナを構築する
            var provider = serviceCollection.BuildServiceProvider();

            // インスタンスを取り出す
            var adorer = provider.GetService<IAdorer>();

            #region ex. メソッドの戻り値がTaskの場合（Mainにasyncが必要）
            // var tasks = await Task.Run(() => adorer.GetAll());
            #endregion
            var tasks = adorer.GetAll();

            // tasksを表示する
            tasks.ForEach(x => Console.WriteLine($"{x.GroupId}, {x.GroupName}, {x.TaskId}, {x.Status}, {x.Content}, {x.Pic}, {x.Period}"));
        }

        /// <summary>
        /// DIの準備
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void ConfigureServices(IServiceCollection services, string envName) {
            
            /* ### DIコンテナへの登録１：プロジェクト共通 ### */

            /**
             * ConfigurationBuilder -> ConfigurationManagerの代替。機能が大幅に増えた
             * ・appsettings.json：環境に依らない定義
             * ・appsettings.{環境名}.json：環境固有の定義
             */
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional : false, reloadOnChange : true)
                .AddJsonFile($"appsettings.{envName}.json", optional : true)
                // .AddEnvironmentVariables() // 環境変数で同名のキーを上書きする場合
                .Build();

            // ConfigurationBuilderのライフサイクルをSingletonにする
            services.AddSingleton(configuration);

            /* ### DIコンテナへの登録２：プロジェクト毎に可変の要素 ### */

            // DBアクセスクラス
            services.AddTransient<IAdorer, Adorer>();
            // MySqlのContextクラス
            services.AddDbContext<kurumiContext>(opt => opt.UseMySQL(configuration.GetConnectionString("kurumi")));
        }
    }
}