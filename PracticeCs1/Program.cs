namespace PracticeCs1
{
    using System;
    using System.Collections.Generic;
    using System.Configuration; // ソリューション・エクスプローラーの参照から System.Configuration.dll をチョイスしろだぜ☆（＾～＾）調べろ☆（＾～＾）
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
            var messages = new Messages();
            {
                messages.Read();

                // テスト表示
                // messages.Display();
            }

            // BGMがあると盛り上がるよな、無いけど☆（＾～＾）
            Console.WriteLine(string.Format(messages.Get("$Bgm1")));
            Thread.Sleep(400);

            // 無意味な演出☆（＾～＾）
            Console.WriteLine(string.Format(messages.Get("$Bgm2")));
            Thread.Sleep(400);

            Console.WriteLine(string.Format(messages.Get("$Bgm3")));
            Thread.Sleep(400);

            Console.WriteLine(string.Format(messages.Get("$Bgm3")));
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
                Console.WriteLine(string.Format(messages.Get("$AppearsMonster"), monster.Name));
                Thread.Sleep((int)waitSec);
                waitSec = (int)Math.Max(20, (float)waitSec * 0.9);
            }

            Console.WriteLine(messages.Get("$WholeTarget"));
            waitSec = 400;
            WholeTarget.Go(monsterList, monster=>
            {
                Console.WriteLine(messages.Get("$Hit"));
                Console.WriteLine(messages.Get("$HitScream"), monster.Name);
                Thread.Sleep((int)waitSec);
                waitSec = (int)Math.Max(20, (float)waitSec * 0.9);
            });

            Console.WriteLine(messages.Get("$RandomSingleTarget"));
            // ツイてない洋一。
            RandomSingleTarget.Go(monsterList, rnd, monster=>
            {                
                Console.WriteLine(messages.Get("$RandomSingleTargetScream"), monster.Name);
                Thread.Sleep(400);
            });

            // 関数型プログラミング使いこなすと脳汁出てくるよな☆（＾～＾）
            {
                Console.WriteLine(messages.Get("$SameTypeTarget"));
                var type = (MonsterType)rnd.Next(1, Enum.GetNames(typeof(MonsterType)).Length);
                SameTypeTarget.Go(monsterList, type, monster =>
                {
                    Console.WriteLine(messages.Get("$Hit"));                    
                    Console.WriteLine(messages.Get("$SameTypeTargetScream"), monster.Name);
                });
                Thread.Sleep(400);
            }

            Console.WriteLine(messages.Get("$GameEndPushAnyKey"));
            Console.ReadKey();
        }
    }
}
