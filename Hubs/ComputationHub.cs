using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace TutorialUniversity.Hubs
{
    public class ComputationHub : Hub
    {
        public async Task StartComputing()
        {
            throw new NotImplementedException();
            
        }
        public async Task SendProgressInfo()
        {
            await Clients.Caller.SendAsync("ReceiveProgressInfo", "abc"); // Caller(呼び出しを行なったページ側js)のReceiveProgressInfoメソッドを"abc"を引数として呼ぶ
        }
    }
}
