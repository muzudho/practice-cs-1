namespace PracticeCs1
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 乱択単体対象とかいう　やる気のないクラス名☆（＾～＾）
    /// </summary>
    public static class RandomSingleTarget
    {
        /// <summary>
        /// 哀れなモンスターが引数で渡されるから、好きに痛めつけろだぜ☆（＾～＾）
        /// </summary>
        /// <param name="monster"></param>
        public delegate void Callback(Monster monster);

        /// <summary>
        /// ランダム要素なんか入れるから引数に　ランダムが要るようになる……☆（＾～＾）
        /// </summary>
        public static void Go(List<Monster> monsterList, Random rand, Callback callback)
        {
            var target = rand.Next(0, monsterList.Count - 1);

            // こいつを痛めつけろだぜ☆（＾～＾）
            callback(monsterList[target]);
        }
    }
}
