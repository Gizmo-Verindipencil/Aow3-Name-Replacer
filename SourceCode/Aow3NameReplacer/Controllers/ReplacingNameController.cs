using Aow3NameReplacer.Models;
using Aow3NameReplacer.Views;
using Aow3NameReplacer.Warnings;
using System.Collections.Generic;
using System.Linq;

namespace Aow3NameReplacer.Controllers
{
    /// <summary>
    /// 名称置換のコントローラーを提供します。
    /// </summary>
    public class ReplacingNameController : Controller
    {
        /// <summary>
        /// モデルを取得または設定します。
        /// </summary>
        public ReplacingNameModel Model = new ReplacingNameModel();
        
        /// <summary>
        /// ビューを取得または設定します。
        /// </summary>
        public ReplacingNameView View = new ReplacingNameView();

        /// <summary>
        /// 名称置換を実行します。
        /// </summary>
        public override void Execute()
        {
            View.ShowTitle();
            SetProperty(ReplacingNameWarning.Property.FilePath);
            SetProperty(ReplacingNameWarning.Property.OldFirstName);
            SetProperty(ReplacingNameWarning.Property.NewFirstName);
            SetProperty(ReplacingNameWarning.Property.OldSecondName);
            SetProperty(ReplacingNameWarning.Property.NewSecondName);
            this.Model.Replace();
            this.View.Show("Done.");
            this.View.Wait();
        }

        /// <summary>
        /// 処理に必要な情報の入力をビューに要求します。
        /// </summary>
        /// <param name="property">入力を要求するプロパティ。</param>
        private void SetProperty(ReplacingNameWarning.Property property)
        {
            var retry = true;
            while (retry)
            {
                switch (property)
                {
                    case ReplacingNameWarning.Property.FilePath:
                        Model.FilePath = View.Require("File path");
                        break;
                    case ReplacingNameWarning.Property.OldFirstName:
                        Model.OldFirstName = View.Require("Current first name");
                        break;
                    case ReplacingNameWarning.Property.NewFirstName:
                        Model.NewFirstName = View.Require("New first name");
                        break;
                    case ReplacingNameWarning.Property.OldSecondName:
                        Model.OldSecondName = View.Require("Current second name");
                        break;
                    case ReplacingNameWarning.Property.NewSecondName:
                        Model.NewSecondName = View.Require("New second name");
                        break;
                    default:
                        return;
                }
                var warnings = Model.GetWarnings().Where(x => x.TargetProperty == property);

                //許容できないエラー
                var notAllowed = warnings.Where(x => x.Level == ReplacingNameWarning.WarningLevel.NotAllowed);
                if (notAllowed.Count() > 0)
                {
                    View.ShowWarning(notAllowed.First().Message);
                    continue;
                }
                //警告すべきエラー
                var notRecomended = warnings.Where(x => x.Level == ReplacingNameWarning.WarningLevel.NotRecomended);
                if (notRecomended.Count() > 0)
                {
                    retry = !ConfirmAllWarnings(notRecomended);
                    continue;
                }
                //入力を確定
                retry = false;
            }
        }

        /// <summary>
        /// 全ての警告すべきエラーの確認をビューに要求します。
        /// </summary>
        /// <param name="warnings">警告。</param>
        /// <returns>
        /// 操作の続行（True：する、False：しない）を返します。
        /// エラーに対するユーザーの応答が「続行」でない場合に<c>false</c>を返します。
        /// </returns>
        private bool ConfirmAllWarnings(IEnumerable<ReplacingNameWarning> warnings)
        {
            if (warnings.Count() == 0)
            {
                return true;
            }
            foreach (var warning in warnings)
            {
                if (!View.Confirm(warning.Message))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
