using System.Linq;
using System.Threading.Tasks;
using GitHub.Core;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace GitHub.UnitTests
{
    [TestClass]
    public class CoreUnitTest
    {
        private readonly IGitHubManager _gitHubManager = new GitHubManager();


        [TestMethod]
        public async Task Can_Get_User()
        {
            // arrange

            // act
            var user = await _gitHubManager.GetUserAsync("Odonno");

            // assert
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Login, "Odonno");
        }

        [TestMethod]
        public async Task Can_Not_Get_Inexisting_User()
        {
            // arrange

            // act
            var user = await _gitHubManager.GetUserAsync("oooooooooooooooooooo");

            // assert
            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task Can_Search_Users()
        {
            // arrange

            // act
            var users = await _gitHubManager.SearchUsers("Odon");

            // assert
            Assert.IsNotNull(users);
            Assert.AreEqual(users.TotalCount, 101);
            Assert.AreEqual(users.Items.Count(u => u.Login == "Odonno"), 1);
        }

        [TestMethod]
        public async Task Can_Search_Repos()
        {
            // arrange

            // act
            var repos = await _gitHubManager.SearchRepos("tez");

            // assert
            Assert.IsNotNull(repos);
            Assert.AreEqual(repos.TotalCount, 129);
        }
    }
}
