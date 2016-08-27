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
    public class GameManagerTest
    {
        [TestCase]
        public void AddPlayerTest()
        {
            GameManager gameManager = new GameManager();
            gameManager.AddPlayer("1234567890", new Player("name") { Id = "someId" });

            Player returnedPlayer = gameManager.GetPlayer("1234567890");

            Assert.AreEqual("someId", returnedPlayer.Id);
        }

        [TestCase]
        public void GetPlayersTest()
        {
            GameManager gameManager = new GameManager();
            gameManager.AddPlayer("1234567890", new Player("name") { Id = "someId" });
            gameManager.AddPlayer("qwertyuiop", new Player("name2") { Id = "anotherId" });

            Dictionary<string, Player> returnedPlayers = gameManager.GetPlayers();

            Assert.AreEqual(2, returnedPlayers.Count);
        }

        [TestCase]
        public void GetPlayersTest_ShouldReturnEmptyDictionaryIfNoConnections()
        {
            GameManager gameManager = new GameManager();

            Dictionary<string, Player> returnedPlayers = gameManager.GetPlayers();

            Assert.AreEqual(0, returnedPlayers.Count);
        }

        [TestCase]
        public void GetPlayerTest()
        {
            GameManager gameManager = new GameManager();

            gameManager.AddPlayer("1234567890", new Player("name") { Id = "someId" });
            gameManager.AddPlayer("qwertyuiop", new Player("name2") { Id = "anotherId" });

            Player returnedPlayer2 = gameManager.GetPlayer("qwertyuiop");
            Player returnedPlayer1 = gameManager.GetPlayer("1234567890");

            Assert.AreEqual("name", returnedPlayer1.Name);
            Assert.AreEqual("name2", returnedPlayer2.Name);
        }

        [TestCase]
        public void GetPlayerTest_ShouldReturnNullIfPlayerDoesntExist()
        {
            GameManager gameManager = new GameManager();

            gameManager.AddPlayer("1234567890", new Player("name") { Id = "someId" });
            gameManager.AddPlayer("qwertyuiop", new Player("name2") { Id = "anotherId" });

            Player returnedPlayer = gameManager.GetPlayer("nonexisting");

            Assert.AreEqual(null, returnedPlayer);
        }

        [TestCase]
        public void UpdatePlayerTest()
        {
            GameManager gameManager = new GameManager();
            string conenctionId = "1234567890";

            // init player
            gameManager.AddPlayer(conenctionId, new Player("name") { Id = "someId" });

            // retireve from list
            Player returnedPlayer = gameManager.GetPlayer(conenctionId);

            returnedPlayer.Name = "changed";

            // Update in list
            gameManager.UpdatePlayer(conenctionId, returnedPlayer);

            // Get again
            Player returnedPlayer2 = gameManager.GetPlayer(conenctionId);

            // Check if name is changed
            Assert.AreEqual("changed", returnedPlayer2.Name);
        }

        [TestCase]
        public void RemovePlayerTest()
        {
            GameManager gameManager = new GameManager();

            gameManager.AddPlayer("1234567890", new Player("name") { Id = "someId" });
            gameManager.AddPlayer("qwertyuiop", new Player("name2") { Id = "anotherId" });
            gameManager.AddPlayer("adfghj", new Player("name3") { Id = "yetAnotherId" });

            gameManager.RemovePlayer("qwertyuiop");

            // List count should be 2 and removed player should not be on the list anymore
            var returnedPlayers = gameManager.GetPlayers();
            var player = gameManager.GetPlayer("qwertyuiop");

            Assert.AreEqual(2, returnedPlayers.Count);
            Assert.IsNull(player);
        }

        [TestCase]
        public void SetColorTest()
        {
            GameManager gameManager = new GameManager();

            string conenctionId = "1234567890";

            gameManager.AddPlayer(conenctionId, new Player("name") { Id = "someId" });

            gameManager.SetColor(conenctionId, "#123456");

            Player returnedPlayer = gameManager.GetPlayer(conenctionId);

            Assert.AreEqual("#123456", returnedPlayer.DrawColor);
        }
    }
}
