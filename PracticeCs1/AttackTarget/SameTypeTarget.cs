namespace PracticeCs1
{
    using System.Collections.Generic;

    /// <summary>
    /// 同じタイプのモンスターばかり叩くぜ☆（＾～＾）
    /// </summary>
    public static class SameTypeTarget
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
        public static void Go(List<Monster> monsterList, MonsterType type, Callback callback)
        {
            // ごちゃごちゃ書かなくても　絞り込める☆（＾～＾）Linq 便利だろ☆（＾～＾）
            // Linq のオリジナルじゃなくて、関数型プログラミングの手法なんだけどな☆（＾～＾）
            var selectedMonsters = monsterList.FindAll(monster=>
            {
                // ここで比較している type は、外側の関数の引数の type だぜ☆（＾～＾）
                // これは　クロージャから見れば自由変数だし、外側の関数から見れば束縛変数だぜ☆（＾～＾）
                // 今はなんのこっちゃ分からないと思うが、そんなものと覚えておけだぜ☆（＾～＾）
                // 関数のくせに type というプロパティを持っていて初期値が入ってるぐらいの理解で十分☆（＾～＾）
                return monster.Type == type;
            });

            // 回すだけだよな☆（＾～＾）
            foreach (var monster in selectedMonsters)
            {
                callback(monster);
            }
        }
    }
}
