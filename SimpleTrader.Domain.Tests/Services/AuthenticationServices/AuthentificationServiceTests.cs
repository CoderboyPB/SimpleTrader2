using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SimpleTrader.Domain.Services.AuthenticationServices.IAuthenticationService;

namespace SimpleTrader.Domain.Tests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthentificationServiceTests
    {
        private Mock<IPasswordHasher> _mockPasswordHasher;
        Mock<IAccountService> _mockAccountService;
        AuthenticationService _authenticationService;

        [SetUp]
        public void Setup()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _authenticationService = new AuthenticationService(_mockAccountService.Object, _mockPasswordHasher.Object);
        }

        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsAccountForCorrectUsername()
        {
            // Arrange
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account() { AccountHolder = new User() { Username = expectedUsername } });
            _mockPasswordHasher.Setup(p => p.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);

            // Act
            Account account = await _authenticationService.Login(expectedUsername, password);

            // Assert
            string actualUsername = account.AccountHolder.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithInCorrectPasswordForExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            // Arrange
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).ReturnsAsync(new Account(){ AccountHolder = new User() { Username = expectedUsername }});
            _mockPasswordHasher.Setup(p => p.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            // Act
            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(() => _authenticationService.Login(expectedUsername, password));

            // Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithNonExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            // Arange
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockPasswordHasher.Setup(p => p.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            // Act
            UserNotFoundException exception = Assert.ThrowsAsync<UserNotFoundException>(() => _authenticationService.Login(expectedUsername, password));

            // Assert
            string username = exception.Username;
            Assert.AreEqual(expectedUsername, username);
        }

        [Test]
        public async Task Register_WithPasswordsNotMatching_ReturnsPasswordsDoNotMatch()
        {
            // Arrange
            RegistrationResult expected = RegistrationResult.PasswordDoNotMatch;
            string password = "testpassword";
            string confirmPassword = "confirmtestpassword";

            // Act
            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingEmail_ReturnsEmailAlreadyExists()
        {
            // Arrange
            RegistrationResult expected = RegistrationResult.EmailAlreadyExists;
            _mockAccountService.Setup(a => a.GetByEmail(It.IsAny<string>())).ReturnsAsync(new Account());

            // Act
            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), "test", "test");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingUsername_ReturnsUsernameAlreadyExists()
        {
            // Arrange
            RegistrationResult expected = RegistrationResult.UsernameAlreadyExists;
            _mockAccountService.Setup(a => a.GetByUsername(It.IsAny<string>())).ReturnsAsync(new Account());

            // Act
            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), "test", "test");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithNonExistingUsersAndMatchingPasswords_ReturnsSuccess()
        {
            // Arrange
            RegistrationResult expected = RegistrationResult.Success;

            // Act
            RegistrationResult actual = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), "test", "test");

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
