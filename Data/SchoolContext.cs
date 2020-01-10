using TutorialUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace TutorialUniversity.Data
{
    // O/R マッピング
    // データベースの内容とModelクラスの対応を定める
    // WindowsならControllerフォルダ右クリック->追加->コントローラのコンテキスト +ボタンで自動生成することもできる
    // とはいえ自動生成のDbContextではModelクラス名そのままになる
    // SQLではSELECT * FROM テーブル名なので、DBテーブルは複数形にしておき、フレームワーク側で対応しているModelは取得された一件一件なので単数にするのが最も自然であるように思われる
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
    }
}
