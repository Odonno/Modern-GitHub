using System.Threading.Tasks;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using GitHub.Services.Abstract;
using GitHub.Services.Concrete;

namespace GitHub.UnitTests
{
    [TestClass]
    public class CoreUnitTest
    {
        #region GitHub Manager

        private readonly IGitHubService _gitHubService = new GitHubService();

        [TestMethod]
        public async Task Can_Get_User()
        {
            // arrange

            // act
            var user = await _gitHubService.GetUserAsync("Odonno");

            // assert
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Login, "Odonno");
        }

        [TestMethod]
        public async Task Can_Not_Get_Inexisting_User()
        {
            // arrange

            // act
            var user = await _gitHubService.GetUserAsync("oooooooooooooooooooo");

            // assert
            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task Can_Search_Users()
        {
            // arrange

            // act
            var users = await _gitHubService.SearchUsers("Odon");

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
            var repos = await _gitHubService.SearchRepos("tez");

            // assert
            Assert.IsNotNull(repos);
            Assert.AreEqual(repos.TotalCount, 129);
        }

        #endregion
    }
}
