using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TutorialUniversity.Models;

namespace TutorialUniversity.ViewModels
{
    public class StudentViewModel // StudentのプロパティをView向けに調整した、Viewに渡すためのクラス
    {
        public int ID { get; private set; } // AutoMapperによる初期化の時以外、値の代入はできない POSTリクエストに含めても変更は認めない

        [DisplayName("姓")]
        [RegularExpression("[A-z]*", ErrorMessage = "{0}はアルファベットのみ")] // 正規表現に合致しない場合「姓はアルファベットのみ」エラーを出す
        public string LastName { get; set; }

        [DisplayName("名前")]
        [RegularExpression("[A-z]*", ErrorMessage = "{0}はアルファベットのみ")]
        public string FirstName { get; set; }

        // Viewでアクセスできては困るSomeSecretは含んでいない

        public DateTime EnrollmentDate { get; private set; } // 変更を認めない

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Penalty> Penalties { get; set; }

        public double GPA => Enrollments.Average(enr => (double)enr.Grade);
    }
}
