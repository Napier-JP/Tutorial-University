using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorialUniversity.Models
{
    public class UserAnalysis : IUserAnalysis
    {
        public UserAnalysis()
        {
        }
        [ForeignKey("UserStats")]
        public int UserID { get; set; }
        private IList<FitbitRealtimeStat> UserStats { get; set; }

        // Controllerはページ遷移や処理の指示だけ出す場所、実際の計算はModelがやる
        // 複数のFitbitRealtimeStatからなるデータを使ってフーリエ変換するのでこのModelに書くのがいい
        // 時間がかかる処理なので、Controllerを計算終了まで拘束して(ブロッキング)、結果を持たせて（同期）呼び出し元に帰すのではなく
        // asyncにして「今すぐお答えすることはできません（非同期）、それでは後ほど」と解放（ノンブロッキング）するべき
        // Controllerが即座にawaitして「帰るけど、答えが出るまで家であなたを監視する」するのは自由だが無駄が多い
        public async Task<List<double>> RSA()
        {
            await Task.Delay(1);
            throw new NotImplementedException();
            return new List<double>();
            // 得られたデータにどのような制約を加えるか（順序、個数の要否）は受け手（が代入する先の型）が決めるので
            // 受け手に選択の自由を与えるためreturnの型は具象クラスがよい
        }
    }
}
