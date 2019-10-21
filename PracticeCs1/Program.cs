namespace PracticeCs1
{
    using System;
    using System.Collections.Generic;
    using System.Configuration; // ソリューション・エクスプローラーの参照から System.Configuration.dll をチョイスしろだぜ☆（＾～＾）調べろ☆（＾～＾）
    using System.Linq;

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

            foreach (var monster in monsterList)
            {
                Console.WriteLine($@"{monster.Type} {monster.Name} が現れた☆（＾～＾）！");
            }

            WholeTarget.Go(monsterList, monster=>
            {
                Console.WriteLine($@"{monster.Type} {monster.Name} 「痛ぇ☆！」");
            });

            // ツイてない洋一。
            RandomSingleTarget.Go(monsterList, rnd, monster=>
            {
                Console.WriteLine($@"{monster.Type} {monster.Name} 「２度痛ぇ☆！」");
            });

            Console.WriteLine("おわり☆　なんか押せだぜ☆（＾～＾）");
            Console.ReadKey();
        }
    }
}
