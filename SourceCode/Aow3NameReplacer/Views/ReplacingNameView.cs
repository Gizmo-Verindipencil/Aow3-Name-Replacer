using System;

namespace Aow3NameReplacer.Views
{
    /// <summary>
    /// 名称置換のビューを提供します。
    /// </summary>
    public class ReplacingNameView : View
    {
        /// <summary>
        /// 機能名を表示します。
        /// </summary>
        public override void ShowTitle()
        {
            Console.WriteLine("# AoW3 Name Replacer");
            Console.WriteLine();
        }
    }
}
