using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TutorialUniversity.Models
{
    public interface IUserAnalysis
    {
        public Task<List<double>> RSA(); // Task<List<double>>つまり非同期でList<double>の結果を返すメソッドRSAを備えろという要求
    }
}
