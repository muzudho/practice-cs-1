namespace PracticeCs1
{
    // なんか欲しかったら パッケージを [ツール] - [NuGet パッケージ マネージャー] - [ソリューションの NuGet パッケージの管理] - [参照] を使ってインストールしろだぜ☆（＾～＾）
    using System;
    using System.Collections.Generic;
    using System.Configuration; // ソリューション・エクスプローラーの参照から System.Configuration.dll をチョイスしろだぜ☆（＾～＾）調べろ☆（＾～＾）
    using System.IO;
    using System.Linq;
    using System.Threading;

    class Program
    {
        private delegate int DamageCallback();

        /// <summary>
        /// メイン・プログラムは static な Main に書きはじめろだぜ☆（＾～＾）
        /// クラスなんて飾りだぜ☆ｍ９（＾～＾）
        /// </summary>
        /// <param name="args">使いたくなったら使えだぜ☆（＾～＾）</param>
        static void Main(string[] args)
        {
            // 余裕☆（＾～＾）
            Console.WriteLine(ConfigurationManager.AppSettings["urusai"]);

            // メッセージを読込。
            var msg = new Messages();
            {
                // ファイル名なんか指定していたら日がくれる☆（＾～＾）ディレクトリー名を指定して、あとは勝手に探すようにしろだぜ☆（＾～＾）
                // GitHub に置かれるときのことを考えている☆（＾～＾）
                // bin/Release/PracticeCs1.exe から見るとディレクトリは３つ上。
                SearchesDirectory.Go("../../../", (string fileEntry)=>
                {
                    // entry は、ファイルのフルパス☆（＾～＾）
                    // 圧縮ファイル読み込んでも嫌だよな☆（＾～＾） 拡張子は .txt （大文字小文字を区別しない）にしておこうぜ☆（＾～＾）
                    if (Path.GetExtension(fileEntry).ToUpper() == ".TXT")
                    {
                        msg.Documents.Add(fileEntry, MessageDocument.Read(fileEntry));
                    }
                });
            }

            // ここらへんテストばっか☆（＾～＾）
            {
                // テスト表示
                // msg.DisplayToTrace();

                // C#を使った外部スクリプトのテストだぜ☆（＾～＾）
                RoslynTest.Go();

                // 引数が何個ぐらい使えるかのテストだぜ☆（＾～＾）
                msg.WriteBy(
                    "$ParamsTest",
                    "ゼロ",
                    "いちじく",
                    "にんじん",
                    "さんま",
                    "しいたけ",
                    "ごぼう",
                    "ろくってなんだ",
                    "ななくさ",
                    "はち",
                    "きゅう",
                    "じゅう",
                    "イレブン",
                    "トゥエルブ",
                    "サーティーン",
                    "じゅうしまつ",
                    "フィフティーン");
                Thread.Sleep(400);
            }


            // BGMがあると盛り上がるよな、無いけど☆（＾～＾）
            msg.WriteBy("$Bgm1");
            Thread.Sleep(400);

            // 無意味な演出☆（＾～＾）
            // こういう演出を　外部スクリプトに出せだぜ☆（＾～＾）！
            msg.WriteBy("$Bgm2");
            Thread.Sleep(400);

            msg.WriteBy("$Bgm3");
            Thread.Sleep(400);

            msg.WriteBy("$Bgm3");
            Thread.Sleep(400);

            // モンスター・リストとかいう　どうしようもない変数名☆（＾～＾）
            var monsterList = new List<Monster>();

            // デバッグしたけりゃ　ランダムのタネはずっと同じ数にしろだぜ☆（＾～＾）
            var rnd = new System.Random(
                0 // これがタネな☆（＾～＾）
                );

            // モンスターを大量生成だァ☆（＾～＾）！
            var amount = rnd.Next(0, 30);
            for (int i = 0; i < amount; i++)
            {
                var type = (MonsterType)rnd.Next(1, Enum.GetNames(typeof(MonsterType)).Length);

                // using System.Linq; しておくと、こんな書き方で 同じタイプのやつが何匹いるか検索できる☆（＾～＾）
                var sameTypeCount = monsterList.Count(elem => elem.Type == type);

                // ASCIIコードの 65 が A☆（＾～＾） Z を超えるようなやつは知らん☆（＾～＾）
                var ch = (Char)(65 + sameTypeCount);

                monsterList.Add(new Monster(
                    type,
                    ch.ToString(),
                    rnd.Next(2, 20)));
            }

            // うざいんで、だんだん短くしようぜ☆（＾～＾）？
            var waitSec = 400;
            foreach (var monster in monsterList)
            {
                msg.WriteBy("$AppearsMonster", monster.Name);
                Thread.Sleep((int)waitSec);
                waitSec = (int)Math.Max(20, (float)waitSec * 0.9);
            }

            msg.WriteBy("$WholeTarget");
            waitSec = 400;
            WholeTarget.Go(monsterList, monster =>
            {
                msg.WriteBy("$Hit");
                msg.WriteBy("$HitScream", monster.Name);
                Thread.Sleep((int)waitSec);
                waitSec = (int)Math.Max(20, (float)waitSec * 0.9);
            });

            msg.WriteBy("$RandomSingleTarget");
            // ツイてない洋一。
            RandomSingleTarget.Go(monsterList, rnd, monster =>
            {
                msg.WriteBy("$RandomSingleTargetScream", monster.Name);
                Thread.Sleep(400);
            });

            // 関数型プログラミング使いこなすと脳汁出てくるよな☆（＾～＾）
            {
                msg.WriteBy("$SameTypeTarget");
                var type = (MonsterType)rnd.Next(1, Enum.GetNames(typeof(MonsterType)).Length);
                SameTypeTarget.Go(monsterList, type, monster =>
                {
                    msg.WriteBy("$Hit");
                    msg.WriteBy("$SameTypeTargetScream", monster.Name);
                });
                Thread.Sleep(400);
            }

            msg.WriteBy("$GameEndPushAnyKey");
            Console.ReadKey();
        }
    }
}
