namespace PracticeCs1
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// 外部メッセージ・テキスト・ファイルを読み込むぜ☆（＾～＾）
    /// </summary>
    public class Messages
    {
        public Dictionary<string, string> Items { get; private set; }

        public Messages()
        {
            this.Items = new Dictionary<string, string>();
        }

        public void Read()
        {
            var key = string.Empty;
            var builder = new StringBuilder();

            // bin/Release/PracticeCs1.exe から見るとディレクトリは２つ上。
            var lines = File.ReadAllLines("../../message.txt");
            foreach (var line in lines)
            {
                if (line.StartsWith("#"))
                {
                    // コメント行。無視。
                }
                else if (line.StartsWith("$"))
                {
                    // キー。
                    if (key != string.Empty)
                    {
                        // 確定。
                        // メッセージの前後の空白はトリムする。演出で空白行を入れたくなったら別ルールでもあとで付け足せだぜ☆（＾～＾）
                        this.Items.Add(key, builder.ToString().Trim());
                        builder.Clear();
                    }

                    // キー変更。頭の $ ごと入れる。
                    key = line;
                }
                else
                {
                    builder.Append(line);
                }
            }

            if (key != string.Empty)
            {
                // 確定。
                this.Items.Add(key, builder.ToString());
                builder.Clear();
            }
        }

        public string Get(string key)
        {
            if (this.Items.ContainsKey(key))
            {
                return this.Items[key];
            }

            return $@"Unknown ID: {key}";
        }

        /// <summary>
        /// デバッグ表示。
        /// </summary>
        public void Display()
        {
            foreach (var entry in this.Items)
            {
                Console.WriteLine(entry.Key);
                Console.WriteLine(entry.Value);
            }
        }
    }
}
