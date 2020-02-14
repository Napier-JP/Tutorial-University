using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorialUniversity.Data;
using TutorialUniversity.Models;
using TutorialUniversity.ViewModels;

namespace TutorialUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;
        private readonly IMapper _mapper;
        public StudentsController(SchoolContext context, IMapper mapper)
        {
            // Startupで登録してある具象クラスが、必要になった時に供給される
            // _mapperはIMapperなので具象クラスMapperの全機能が利用できないかもしれないが、必要なのはMap<T>()だけなので構わない
            // そして単体テスト時はIMapperのうちMap<T>()だけをまともに実装したモックがあればよい
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CalcEverything() // 最終的にintを返す
        {
            System.Console.WriteLine("Long task start.");
            await Task.Delay(1000);
            System.Console.WriteLine("Task halfway there.");
            await Task.Delay(1000);
            return 42;
        }

        public async Task PracticeZen() // 何も値を返さない
        {
            System.Console.WriteLine("Zen start.");
            await Task.Delay(6000);
            System.Console.WriteLine("I'm bored, let's get to work");
        }

        // GET: Students
        public async Task<IActionResult> Index() // async: このメソッドは非同期＝「呼び出しから戻った時に最終結果Viewはまだ得られない」ことを意味する
        {
            Task<int> tsk = CalcEverything(); // 時間がかかる計算タスクを始めておく 戻り値はあくまでTask<T>

            var rnd = new System.Random();
            var num = rnd.Next(); // これは同期的＝関数の実行完了時に結果が得られる処理
            _ = PracticeZen(); // 時間がかかるが最終結果を見届けなくていいタスクは、あとで確認する必要がないので捨てる
            System.Console.WriteLine($"num is {num}");
      
            int result = await tsk; // 次の行で使うから本当の最終結果であるintが欲しい。もう処理が完成していれば即受け取り、まだならここで届くまで待つ
            System.Console.WriteLine($"result is {result}"); // resultが手に入るまでは実行されない

            return View(await _context.Students.ToListAsync()); // ここまででPracticeZenはawaitされていないので、I'm boredよりも先にページが表示される
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(stu => stu.Enrollments).ThenInclude(enr => enr.Course).AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id); // detailsでは表示しかしないので取得したエンティティの変更有無を検知させる必要がない
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            StudentViewModel studentViewModel = _mapper.Map<StudentViewModel>(student);
            
            return View(studentViewModel);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                Student student = _mapper.Map<Student>(studentViewModel);
                _context.Add(student);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(studentViewModel); // 不正なリクエストならデータ付きでCreate画面に戻す
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // DbContextのキャッシュを探し、その後主キーによる取得を行う SingleOrDefault()/FirstOrDefault()は主キー以外でも検索して取得できるがその場合主キーのように順に並んでいるわけではないためやや遅い
            // Findはナビゲーションプロパティの明示的読み込み・一括読み込み（Explicit Loading・Eager Loading）を行えないので、この編集ページのようにナビゲーションプロパティにアクセスしない場合に使うべき
            // そうでないならDetailsのようにSingleOrDefault/FirstOrDefaultを使うこと
            // DbContextの寿命がスコープ内であることを考えると、Findが役立つのは「生徒全体を取得したのちidで特定される単一生徒への操作も行う」という場合か？
            var student = await _context.Students.FindAsync(id);
            
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
