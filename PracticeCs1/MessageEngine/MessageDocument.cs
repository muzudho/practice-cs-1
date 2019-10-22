namespace PracticeCs1
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;

    /// <summary>
    /// いわゆる message.txt １個分の内容だぜ☆（＾～＾）ファイル名に特に決まりはない☆（＾～＾）
    /// 読み取り専用になっていることに気づけだぜ☆ｍ９（＾～＾）
    /// カプセル化という言葉だけ知っていても実践しなければ意味はない☆（＾～＾）プロパティを public ばっかにしてんだろ☆（＾～＾）
    /// </summary>
    public class MessageDocument
    {
        private Dictionary<string, string> Items { get; set; }

        private MessageDocument()
        {
            this.Items = new Dictionary<string, string>();
        }

        public static MessageDocument Read(string pathFromExe)
        {
            var instance = new MessageDocument();

            // 何回もリードするだろ☆（＾～＾）残っているデータは消しておこうぜ☆（＾～＾）
            instance.Items.Clear();

            var key = string.Empty;
            var textLines = new List<string>();

            // 文字列置換機能のとき使う☆（＾～＾）
            var re = new Regex("^&replace&(.)(.+)$", RegexOptions.Multiline | RegexOptions.Compiled);
            var replacesDict = new Dictionary<string, string>();

            var lines = File.ReadAllLines(pathFromExe);
            foreach (var line in lines)
            {
                if (line.StartsWith("#"))
                {
                    // コメント行。無視。

                    // 出力ウィンドウに出すぜ☆（＾～＾）
                    Trace.WriteLine($"Comment         | {line}");
                }
                else if (line.StartsWith("$"))
                {
                    // キー。
                    if (key != string.Empty)
                    {
                        // 確定。
                        // 出力ウィンドウに出すぜ☆（＾～＾）
                        Trace.WriteLine($"Key             | {line}");
                        instance.EnterText(key, textLines, replacesDict);
                    }

                    // キー変更。頭の $ ごと入れる。
                    key = line;
                }
                else if (re.IsMatch(line))
                {
                    // 出力ウィンドウに出すぜ☆（＾～＾）
                    Trace.WriteLine($"Replaces        | {line}");

                    // 文字列置換機能だぜ☆（＾～＾）欲しいだろ☆ｍ９（＾～＾）
                    // 「&replace&/a/b/」みたいな行にヒットさせるぜ☆（＾～＾）
                    var m = re.Match(line);
                    // ２文字目がセパレーターだぜ☆（＾～＾）例えば「/」な☆（＾～＾）
                    var separator = m.Groups[1].Value.ToCharArray()[0];
                    // それ以降の「a/b/」が本文な☆（＾～＾）
                    var body = m.Groups[2].Value;
                    // 「a」「b」「」の３つに分割できるな☆（＾～＾）
                    var tokens = body.Split(separator);
                    if (tokens.Length == 3)
                    {
                        // ３つ目は無視するぜ☆（＾～＾）
                        replacesDict.Add(tokens[0], tokens[1]);
                    }
                    else
                    {
                        Console.WriteLine($"なんか分からん行だなあ☆（＾～＾）| Separator=[{separator}] body=[{body}] TokenLength={tokens.Length} Line=[{line}].");
                    }
                }
                else if (line.Trim() == string.Empty)
                {
                    // 空行は無視するぜ☆（＾～＾）

                    // 出力ウィンドウに出すぜ☆（＾～＾）
                    Trace.WriteLine($"Empty           | {line}");
                }
                else
                {
                    // 出力ウィンドウに出すぜ☆（＾～＾）
                    Trace.WriteLine($"Text            | {line}");

                    // （演出）演出で空白行を入れたくなるときもあるだろ、`&br&` とでも書いとけだぜ☆（＾～＾）
                    if (line.Trim()=="&br&")
                    {
                        textLines.Add(Environment.NewLine);
                    }
                    else
                    {
                        textLines.Add(line);
                    }
                }
            }

            if (key != string.Empty)
            {
                // 最後の読み取り中のものを確定。
                // 出力ウィンドウに出すぜ☆（＾～＾）
                Trace.WriteLine($"Info            | Enter last message.");
                instance.EnterText(key, textLines, replacesDict);
            }

            return instance;
        }

        /// <summary>
        /// 見つからなかったときにヌルを返すのはバグが見つからない元だぜ☆（＾～＾）
        /// Tupleを使おうぜ☆（＾～＾）使えなかったら NuGet から System.ValueTuple を探してこいだぜ☆（＾～＾）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public (bool, string) GetValue(string key)
        {
            if (this.Items.ContainsKey(key))
            {
                return (true, this.Items[key]);
            }

            // テストするときに、何が起こったのか分かった方が助かるだろ☆（＾～＾）
            return (false, $@"Unknown ID: {key}");
        }

        /// <summary>
        /// デバッグ表示。
        /// </summary>
        public void DisplayToTrace()
        {
            foreach (var entry in this.Items)
            {
                // 出力ウィンドウに出した方がありがたいだろ☆（＾～＾）
                Trace.WriteLine(entry.Key);
                Trace.WriteLine(entry.Value);
            }
        }

        private void EnterText(string key, List<string> textLines, Dictionary<string, string> replacesDict)
        {
            // （順番１）各行を改行で連結☆（＾～＾）最後には改行は付けないぜ☆（＾～＾）呼出し元で最後に改行を付けるかどうか決めろだぜ☆（＾～＾）
            var body = string.Join(Environment.NewLine, textLines);

            // （順番２）文字列置換の指定があれば、置換するぜ☆（＾～＾）
            foreach (var replaces in replacesDict)
            {
                body = body.Replace(replaces.Key, replaces.Value);
            }

            this.Items.Add(key, body);
            textLines.Clear();
            replacesDict.Clear();
        }
    }
}
