using System;
using System.Collections.Generic;
using System.Linq;

namespace TutorialUniversity.Models
{
    public class Student // StudentViewModelも参照せよ
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string SomeSecret { get; set; } // Viewでアクセスされては困るプロパティの例
        public DateTime EnrollmentDate { get; set; }

        // 1対多のナビゲーションプロパティ(ナビゲーションプロパティの意義についてはEnrollment.csを参照せよ)
        // 生徒は複数の履修登録データを有する
        // List<Enrollment>ではなく共通に行える操作しか定義されていない抽象的なICollectionとすることで、想定していないList<T>-specificな操作が行えることを減らす
        // 例えばList<T>のメソッドにFindAll()があるが、これは絞り込んだ結果をToList()する。順序に意味がない履修登録では、添え字による操作は必要ないはずである。
        // ICollection<T>にしておくことでforeachやCountを認めつつ、ToListの無駄が生じる可能性がモデル設計段階で防止される。
        //
        // 他方、ユーザや開発者が新しく用意してDBに入れようとする履修登録データはICollection<T>を実装していれば何でもよい。
        // List<T>やHashSet<T>、Dictionary<TKey, Enrollment>.Valuesのいずれも受け入れられる。これは設計者がデータ構造に関与できない外部データを利用する際に便利である。ここでもToList()しなくて済む。
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Penalty> Penalties { get; set; }

        // プロパティだが加工された値を返すgetterオンリーでありDBには存在しない(GPAはEnrollmentsから算出されるものであって、独立に変更できるものではない) 
        public double GPA => Enrollments.Average(enr => (double)enr.Grade);
    }
}
