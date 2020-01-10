using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TutorialUniversity.Models
{
    // DBSetで指定するテーブル名を手動で指定する例(Data/SchoolContext.csを参照せよ)
    public class Penalty // 懲戒などを記録したいとする　遅刻よりも重大なので単なるStudentのintメンバではなく日付や内容まで記録したい
    {
        public int PenaltyID { get; set; }
        public int StudentID { get; set; }
        public DateTime IssueDate { get; set; }
        public string Charge { get; set; }  // 罪状

        // 一対多なのでナビゲーションプロパティStudentを設定することもできるが行わない
        // 懲戒詳細ページで生徒情報を見るのではなく、生徒詳細ページで各人の懲戒列挙を見るはずだからである
        // 懲戒の多い方から生徒を並べるのであれば、StudentsからStudents.OrderByDescending(stu => stu.Penalties.Count())とすればよい
    }
}
