using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SignalRGame;

namespace SignalRGameTest
{
    [TestFixture]
    public class PlayerTest
    {
        [TestCase]
        public void ShouldGenerateId()
        {
            Player player = new Player("user");
            Assert.NotNull(player.Id);
            Assert.IsNotEmpty(player.Id);
        }

        [TestCase]
        public void ShouldSetDefaultDrawColor()
        {
            Player player = new Player("user");
            Assert.NotNull(player.DrawColor);
            Assert.IsNotEmpty(player.DrawColor);
        }


        // todo test setting name not null nor empty
    }
}
