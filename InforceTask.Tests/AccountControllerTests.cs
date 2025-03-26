using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using InforceTask.Controllers;
using InforceTask.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Moq;

namespace InforceTask.Tests
{
    public class AccountControllerTests
    {
        private readonly AccountController _controller;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountControllerTests()
        {
            _userManager = MockUserManager();
            _signInManager = MockSignInManager();
            _controller = new AccountController(_userManager, _signInManager);
        }

        private UserManager<IdentityUser> MockUserManager()
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            var userManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
            return userManager.Object;
        }

        private SignInManager<IdentityUser> MockSignInManager()
        {
            var signInManager = new Mock<SignInManager<IdentityUser>>(
                _userManager,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<IdentityUser>>().Object,
                null,
                null,
                null,
                null
            );
            return signInManager.Object;
        }

        [Fact]
        public void Register_ShouldReturnViewResult()
        {
            var returnUrl = "/home";
            var result = _controller.Register(returnUrl);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<RegisterViewModel>(viewResult.Model);
            Assert.Equal(returnUrl, model.ReturnUrl);
        }

        [Fact]
        public void Login_ShouldReturnViewResult()
        {
            var returnUrl = "/home";
            var result = _controller.Login(returnUrl);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<LoginViewModel>(viewResult.Model);
            Assert.Equal(returnUrl, model.ReturnUrl);
        }

        [Fact]
        public async Task Logout_ShouldRedirectToLogin()
        {
            var result = await _controller.Logout();

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectToActionResult.ActionName);
            Assert.Equal("Account", redirectToActionResult.ControllerName);
        }

    }
}
