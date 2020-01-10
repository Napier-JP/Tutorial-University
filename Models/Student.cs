using System;
using System.Collections.Generic;
using System.Linq;

namespace TutorialUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // 1対多のナビゲーションプロパティ(ナビゲーションプロパティの意義についてはEnrollment.csを参照せよ)
        // 生徒は複数の履修登録データを有する
        // List<Enrollment>ではなく共通に行える操作しか定義されていない抽象的なICollectionとすることで、想定していないList<T>-specificな操作が行えることを減らす
        // 例えば履修登録の集合について追加順に意味はないのでそれに依存した操作を考えるべきではない
        // だからList<Enrollment>では可能なenrollemnts[0]のような参照をさせないためにICollection
        // ICollectionにはCountプロパティがあるので、何件履修登録しているのか調べることはできる
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Penalty> Penalties { get; set; }

        // メンバ変数ではなくプロパティ (GPAはEnrollmentsから算出されるものであって、勝手に変更できるものではない)
        public double GPA => Enrollments.Average(enr => (double)enr.Grade);
    }
}
