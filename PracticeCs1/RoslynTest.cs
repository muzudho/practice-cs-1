namespace PracticeCs1
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    // なんか欲しかったら パッケージを [ツール] - [NuGet パッケージ マネージャー] - [ソリューションの NuGet パッケージの管理] - [参照] を使ってインストールしろだぜ☆（＾～＾）
    using Microsoft.CodeAnalysis.CSharp.Scripting; // Roslyn使うなら .Net Framework 4.6 以上☆（＾～＾）

    /// <summary>
    /// See: [Roslyn for Scripting – C#プログラム内でC#で書かれたスクリプトを実行しよう](https://www.casleyconsulting.co.jp/blog/engineer/202/)
    /// </summary>
    public static class RoslynTest
    {
        public static void Go()
        {
            Console.WriteLine(@"外部スクリプトを実行するテストをするからな☆（＾～＾）
最初は ちょっと時間がかかるようだぜ☆（＾～＾）");

            // C#を使った外部スクリプトのテストだぜ☆（＾～＾）            // 
            // スクリプトで使用するクラスは、usingされていない状態で始まるので、フルパスで記述しなければなりません。
            var script1 = "System.Console.WriteLine(\"Hello Roslyn For Scripting!!\");";
            CSharpScript.RunAsync(script1).Wait();

            var script2 = @"
                var pi = 3.14;
                var r = 5;
                pi * r * r
                ";
            var calculus = CSharpScript.EvaluateAsync<double>(script2);
            calculus.Wait();
            Console.WriteLine(calculus.Result);

            // 次のテスト☆（＾～＾）
            {
                var textList = new List<String>();

                // 通常のインスタンスメソッド呼び出し
                textList.Add("プログラムから文字列追加");

                // スクリプトから、インスタンスを利用。
                CSharpScript.RunAsync("Add(\"スクリプトから文字列追加\");", globals: textList).Wait();

                // List<String>に格納されている文字列を出力
                foreach (var text in textList)
                {
                    Console.WriteLine(text);
                }
            }
        }
    }
}
