using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutorialUniversity.Models
{
    public class Course // 科目データ
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // 主キーたるコースIDを手動で振りたい
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        // この科目に対する履修登録データ群 Student.csを参照せよ
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
