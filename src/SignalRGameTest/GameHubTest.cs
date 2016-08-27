using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SignalRGame;
using Moq;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRGameTest
{
    [TestFixture]
    public class GameHubTest
    {
        //OnConnected
        [Test]
        public void OnConnectedTest()
        {
            var gameHub = new GameHub();

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("name", "jan");

            Mock<Microsoft.AspNet.SignalR.Hosting.INameValueCollection> qs = new Mock<Microsoft.AspNet.SignalR.Hosting.INameValueCollection>();
            qs.Setup(x => x.Get(It.IsAny<string>())).Returns("efe");
            qs.Setup(x => x.GetEnumerator()).Returns(dict.GetEnumerator());

            Mock<HubCallerContext> mock = new Mock<HubCallerContext>();
            mock.SetupGet(x => x.QueryString).Returns(qs.Object);

            gameHub.CurrentContext = mock.Object;

            Task s = Task.Run(() => gameHub.OnConnected());
            s.Wait();

            Assert.NotNull(s);

        }


        //OnDisconnected

        //OnReconnected

        //Send

        //SetColor

    }
}
