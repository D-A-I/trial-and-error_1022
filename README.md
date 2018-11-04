# trial-and-error_1022
## このアプリの進め方
### APIクライアント（C#.. vscodeで作成）
1. MySqlにテーブルを作成する  
-> char setをutf-8に変えて再作成する  
-> OK
1. MySqlにデータを投入する  
-> OK
1. 別プロジェクトを作る  
-> OK
1. MySqlからscaffoldする  
-> 着手中
-> 公式のDBプロバイダーはプレリリースらしい。サードパーティ製（Pomelo.EntityFrameworkCore.MySql）の方がいいかも  
-> メモ..  
-> @"server=localhost;database=kurumi;userid=developer;pwd=xxxxx;sslmode=none;"
1. DIで接続文字列を渡す
1. 環境構築シェルスクリプトを作る  
  - 親ディレクトリ名を取得してslnファイルを作る
  - 親ディレクトリ名.Cli フォルダを作成し、"console"（実際はWebかつMVC）テンプレを追加
  - 親ディレクトリ名.Models フォルダを作成し、"classlib"テンプレを追加
  - slnと子csprojを紐付けする
  - Cliプロジェクトにappsettings.json用の各種パッケージを追加  
  参考） https://qiita.com/abpla/items/c4faeed42bef4d450c8d
  - ModelプロジェクトにMySqlからscaffoldしたモデルを追加する  
  参考） https://qiita.com/takanemu/items/ebca534db398aa9cce34
---
## （参考）mysqlについて
### mysqlのインストール
http://ksino.hatenablog.com/entry/2016/11/17/232619
### 初回ログイン時
```bash
$ sudo mysql -u root -p
```
sudoで、ubuntuの管理者パスワードを入力する
### ユーザの作成
```sql
CREATE USER 'developer'@'localhost' IDENTIFIED BY 'xxxxx';
```
> 参考  
> https://www.dbonline.jp/mysql/user/index1.html

### 権限付与
https://qiita.com/shuntaro_tamura/items/2fb114b8c5d1384648aa  
### character setの設定
＊ 必ずdatabaseを作成する前に設定
> 参考  
> https://qiita.com/ldap2017/items/4d172128960218b00ab5

なお、設定ファイルのmy.cnfは、ルートディレクトリから探すと見付かる
```bash
cd /
```
### mysqlのアンインストール
```bash
$ sudo service mysql stop
$ sudo killall -9 mysql
$ sudo killall -9 mysqld
$ sudo apt-get remove --purge mysql-server mysql-client mysql-common
$ sudo apt-get autoremove
$ sudo apt-get autoclean
$ sudo deluser mysql
$ sudo rm -rf /var/lib/mysql
$ sudo apt-get purge mysql-server-core-5.5
$ sudo apt-get purge mysql-client-core-5.5
$ sudo rm -rf /var/log/mysql
$ sudo rm -rf /etc/mysql
```
