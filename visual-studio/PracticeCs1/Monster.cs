namespace PracticeCs1
{
    /// <summary>
    /// いかにもプレイヤーに叩かれそうな名前のクラス名☆（＾～＾）
    /// こいつはコピーして何個も作りたいデータだからスタティックにはしない☆（＾～＾）
    /// </summary>
    public class Monster
    {
        /// <summary>
        /// すべてのプロパティの set を private にしておけだぜ☆（＾～＾）
        /// それで　このインスタンスは読み取り専用になる☆（＾～＾）
        /// 一度 new したら　ずっと使い回せだぜ☆（＾～＾）読み取り専用だから困らん☆（＾～＾）
        /// </summary>
        public MonsterType Type { get; private set; }

        /// <summary>
        /// A とか B とか、みすぼらしい名前だぜ☆（＾～＾）
        /// </summary>
        public string Numbering { get; private set; }

        /// <summary>
        /// ３発当たれば死ぬ、とかいう砲撃手から見たカウントが
        /// 残り体力みたいに使われるようになりだしたもの☆（＾～＾）
        /// ２０ぐらいでいいんだぜ、２０で☆（＾～＾）
        /// </summary>
        public int HitPoint { get; private set; }

        public Monster(MonsterType type, string name, int hitPoint)
        {
            this.Type = type;
            this.Numbering = name;
            this.HitPoint = hitPoint;
        }

        public string Name => $@"{this.Type} {this.Numbering}";
    }
}
