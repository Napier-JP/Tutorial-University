# "Contoso University"

Tutorial in ASP.NET Core MVC + Entity Framework Core on .NET Core 3.1

## Still in progress. May contain misunderstanding. Don't use this as your guide until I remove this warning

## Directories to see & Topics to be learned

1. Models

リレーショナルデータベースを利用したアプリケーションにおけるデータの定義
データを利用した処理メソッド

2. StudentController

DBのデータを要求してエンティティの形でViewに渡し、Viewによって操作されたエンティティをDBに反映すること
時間のかかる処理を別スレッドで実行させ先にページの表示を行うこと

3. ViewModels

Viewの中での表示を便利にし操作の安全を確保するための機能制限版Model

4. Data

DBのデータ構造とC#のModelクラスの対応、DBのシーディング（初期データ挿入）、Model-ViewModelの対応設定

5. Startup.cs

単体でもテストが行えるような疎結合を実現するためのDependency Injection（抽象的に指定した他クラスのオンデマンドな具象クラス供給）

6. appsettings.json

コンパイル後に変更のきかないC#の代わりとなる設定群(Azure App Serviceではオーバーライドされるべき)

## 見なくていいもの（Visual Studioによって自動生成されるもの）

- Properties
- Views/(Home|Shared)
- Views/_View.*.cshtml
- wwwroot
