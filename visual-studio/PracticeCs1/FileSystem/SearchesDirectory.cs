namespace PracticeCs1
{
    using System.IO;

    public static class SearchesDirectory
    {
        public delegate void FileEntryCallback(string fileEntry);

        public static void Go(string directoryFromExe, FileEntryCallback fileEntryCallback)
        {
            // ファイルを探せだぜ☆（＾～＾）
            var entries = Directory.GetFileSystemEntries(directoryFromExe);
            foreach (var entry in entries)
            {
                if (File.Exists(entry))
                {
                    // entry は、ファイルのフルパス☆（＾～＾）
                    fileEntryCallback(entry);
                }
                else
                {
                    // これが　お馴染みの再帰関数だぜ☆（＾～＾）！
                    // ディレクトリーの中に　ごちゃごちゃ　ファイル入れてんなよ☆（＾～＾）
                    Go(entry, fileEntryCallback);
                }
            }
        }
    }
}
