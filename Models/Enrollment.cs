using System.ComponentModel.DataAnnotations.Schema;

namespace TutorialUniversity.Models
{
    public enum Grade
    {
        A = 4, B = 3, C = 2, D = 1, F = 0
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; } // null許容型(成績未付与がありえるため)

        // ナビゲーションプロパティ
        // リレーショナルDBにおいて階層的なデータ構造は持てないので該当するCourse, StudentデータをEnrollmentテーブルで見ることはできない(だからRDBで持つことのできるint IDで管理する)
        // ところがプリミティブデータ型でない以下のプロパティを設定することで、あたかもouter join済みであるようなレコードが得られる
        // すなわちenrollment.Student.LastNameという参照ができるようになる
        // enrollmentを取って来てからstudents.Where(records => records.ID == enrollment.StudentID).Select(matched => matched.LastName).FirstOrDefault();
        // などというLINQを書く手間が省ける
        // とはいえEnrollmentを取得するたびOuter Joinしていることになるので、他ModelのIDだけが必要とされているような状況で書いても無駄になる
        // Microsoft.EntityFrameworkCore.ProxiesをNuGetからインストールし、Startup.csでLazyLoadingを有効にするとナビゲーションプロパティへの参照がなければOuter Joinしないようになる（ただしvirtual修飾子が必須になる）
        // virtualをつけないとスキャフォールディングに失敗するし手作りしてもおそらく取得失敗する
        //
        // 前述の通りRDBで持てないデータ型なのでDB内部に以下の名前のカラムは存在せず、変更に伴うマイグレーションの必要もない
        // またどのCourse, Studentが関連しているのかを特定するために既定では{クラス名}IDという外部キーがこのModel中に必要
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }

        // だが常にその名前をつけられる/つけたいとは限らない
        // SomeVeryLongLongLongLongNameModelIDなんてカラムを作りたくない、VeryShortというカラムに持たせたい
        public int? VeryShort { get; set; }

        [ForeignKey("VeryShort")]
        public virtual SomeVeryLongLongLongLongNameModel SVLLLLNM { get; set; }
    }
}
