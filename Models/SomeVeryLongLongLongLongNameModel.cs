// 外部キー名の説明用の無意味なModel Enrollmentを参照せよ
using System.ComponentModel.DataAnnotations;

namespace TutorialUniversity.Models
{
    public class SomeVeryLongLongLongLongNameModel
    {
        public int MeaninglessColumnName1 { get; set; }

        // 主キーは既定でIDまたは{クラス名}IDになる
        // しかしそうしたくない/できないときもある
        // そこで[Key]をつけて主キーを指定する
        // Enrollmentにある外部キーVeryShortはMeaninglessColumnName2に対応するようになる
        [Key]
        public int MeaninglessColumnName2 { get; set; }
    }
}
