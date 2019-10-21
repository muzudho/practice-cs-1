namespace PracticeCs1
{
    using System.Collections.Generic;

    /// <summary>
    /// 全体対象とかいう　どうしようもないクラス名☆（＾～＾）
    /// </summary>
    public static class WholeTarget
    {
        /// <summary>
        /// 哀れなモンスターが引数で渡されるから、好きに痛めつけろだぜ☆（＾～＾）
        /// </summary>
        /// <param name="monster"></param>
        public delegate void Callback(Monster monster);

        /// <summary>
        /// コンピューター将棋とか、思考を開始するときは Go! だぜ☆（＾～＾）
        /// そのくせ　コンピューター囲碁はべつに Go! とか言わない☆（＾～＾）
        /// </summary>
        public static void Go(List<Monster> monsterList, Callback callback)
        {
            // どいつもこいつも対象なら楽なもんだぜ☆（＾～＾）
            foreach (var monster in monsterList)
            {
                callback(monster);
            }
        }
    }
}
