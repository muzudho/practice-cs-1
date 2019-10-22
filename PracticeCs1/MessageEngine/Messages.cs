namespace PracticeCs1
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;

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
            foreach (var entry in entries)
            {
                if (File.Exists(entry))
                {
                    // entry は、ファイルのフルパス☆（＾～＾）
                    // 圧縮ファイル読み込んでも嫌だよな☆（＾～＾） 拡張子は .txt （大文字小文字を区別しない）にしておこうぜ☆（＾～＾）
                    if(Path.GetExtension(entry).ToUpper() == ".TXT")
                    {
                        this.Documents.Add(entry, MessageDocument.Read(entry));
                    }
                }
                else
                {
                    // これが　お馴染みの再帰関数だぜ☆（＾～＾）！
                    // ディレクトリーの中に　ごちゃごちゃ　ファイル入れてんなよ☆（＾～＾）
                    ReadFromDirectory(entry);
                }
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
        public void Display()
        {
            foreach (var doc in this.Documents.Values)
            {
                doc.Display();
            }
        }
    }
}
