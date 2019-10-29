namespace PracticeCs1
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// 外部メッセージ・テキスト・ファイルを読み込むぜ☆（＾～＾）
    /// </summary>
    public class Messages
    {
        /// <summary>
        /// 知ってるぜ☆（＾～＾）
        /// 複数のファイルに分割したいんだろ☆（＾～＾）？
        /// </summary>
        public Dictionary<string, MessageDocument> Documents { get; private set; }

        public Messages()
        {
            this.Documents = new Dictionary<string, MessageDocument>();
        }

        public void ReadFromDirectory(string directoryFromExe)
        {
            // ファイルを探せだぜ☆（＾～＾）
            var entries = Directory.GetFileSystemEntries(directoryFromExe);
            foreach (var fileEntry in entries)
            {
                if (File.Exists(fileEntry))
                {
                    // entry は、ファイルのフルパス☆（＾～＾）
                    // 圧縮ファイル読み込んでも嫌だよな☆（＾～＾） 拡張子は .txt （大文字小文字を区別しない）にしておこうぜ☆（＾～＾）
                    if (System.IO.Path.GetExtension(fileEntry).ToUpper(CultureInfo.CurrentCulture) == ".TXT")
                    {
                        this.Documents.Add(fileEntry, MessageDocument.Read(fileEntry));
                    }
                }
                else
                {
                    // これが　お馴染みの再帰関数だぜ☆（＾～＾）！
                    // ディレクトリーの中に　ごちゃごちゃ　ファイル入れてんなよ☆（＾～＾）
                    ReadFromDirectory(fileEntry);
                }
            }
        }

        /// <summary>
        /// 可変長引数めんどくさいよな☆（＾～＾）
        /// あとで外部スクリプトで使うことを考えるとメソッド名は短い方がいいんだが、
        /// 慣用に従ってた方が指が動いてくれるというのもある☆（＾～＾）
        /// 
        /// コンパイラが書式チェックしてくれない弱点があるぜ☆（＾～＾）
        /// ReSharperでも使ってれば アノテーションを付ければ良さそうだが☆（＾～＾）
        /// </summary>
        /// <param name="key"></param>
        public void WriteBy(string key, params object[] args)
        {
            try
            {
                Console.WriteLine(string.Format(this.Get(key), args));
            }
            catch (FormatException e)
            {
                // 台詞にうっかり `{` を付けちゃったりすると強制終了してしまう☆（＾～＾）これはひどい仕様だぜ☆（＾～＾）
                // 書式エラーはとりあえず画面に出してしまおう☆（＾～＾）強制終了よりマシ☆（＾～＾）
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 大量に使うからメソッド名は短くしているが、あんま良くない☆（＾～＾）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            // ファイルを跨って探しまくる☆（＾～＾）最初に見つけたやつをだすぜ☆（＾～＾）
            foreach (var doc in this.Documents.Values)
            {
                var (matched, text) = doc.GetValue(key);
                if (matched)
                {
                    return text;
                }
            }

            return $@"Unknown ID: {key}";
        }

        /// <summary>
        /// デバッグ表示。全ファイル分出る☆（＾～＾）このメソッドを使うやつはやばい☆（＾～＾）
        /// </summary>
        public void DisplayToTrace()
        {
            foreach (var doc in this.Documents.Values)
            {
                doc.DisplayToTrace();
            }
        }
    }
}
