using System;
using System.ComponentModel.DataAnnotations;

namespace TutorialUniversity.Models
{
    // 毎秒得られるFitbit情報一件一件
    public class FitbitRealtimeStat
    {
        [Key]
        public int StatID { get; set; }

        public DateTime Timestamp { set; get; }
        public int UserID { get; set; }
        public int BPM { get; set; }
    }
}
