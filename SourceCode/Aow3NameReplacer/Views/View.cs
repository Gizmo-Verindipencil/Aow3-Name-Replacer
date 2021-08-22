using System;

namespace Aow3NameReplacer.Views
{
    /// <summary>
    /// ビューを提供します。
    /// </summary>
    public abstract class View
    {
        /// <summary>
        /// 機能名を表示します。
        /// </summary>
        public virtual void ShowTitle() { }

        /// <summary>
        /// メッセージを表示します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public void Show(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
        }

        /// <summary>
        /// 項目と内容を表示します。
        /// </summary>
        /// <param name="name">項目。</param>
        /// <param name="value">内容。</param>
        public void Show(string name, string value)
        {
            Console.WriteLine(name + ":");
            Console.WriteLine(value);
            Console.WriteLine();
        }

        /// <summary>
        /// 警告を表示します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public void ShowWarning(string message)
        {
            var lines = message.Split(Environment.NewLine);
            for (int i = 0; i < lines.Length; i++)
            {
                const string HEAD = "[# Warning !] ";
                var line = (i == 0 ? HEAD : new string(' ', HEAD.Length)) + lines[i];
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// ユーザーに回答つきの確認を行います。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <returns>
        /// 操作の続行（True：する、False：しない）を返します。
        /// エラーに対するユーザーの応答が「続行」でない場合に<c>false</c>を返します。
        /// </returns>
        public bool Confirm(string message)
        {
            var lines = message.Split(Environment.NewLine);
            for (int i = 0; i < lines.Length; i++)
            {
                const string HEAD = "[# Confirmation !] ";
                var line = (i == 0 ? HEAD : new string(' ', HEAD.Length)) + lines[i];
                Console.WriteLine(line);
            }
            Console.WriteLine("Do you continue this process? (Y/N)");
            var answer = Console.ReadLine().Trim().ToLower();
            Console.WriteLine();
            if (answer.Length > 0 && answer.Substring(0,1) == "y")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ユーザーの入力まで待機します。
        /// </summary>
        public void Wait()
        {
            Console.ReadLine();
        }

        /// <summary>
        /// ユーザーに入力を促します。
        /// </summary>
        /// <param name="name">入力項目。</param>
        /// <returns>
        /// ユーザーの入力内容を返します。
        /// </returns>
        public string Require(string name)
        {
            Console.WriteLine(name + "：");
            var input = Console.ReadLine();
            Console.WriteLine();
            return input;
        }
    }
}
