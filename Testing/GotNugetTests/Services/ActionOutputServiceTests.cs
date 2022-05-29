﻿// <copyright file="ActionOutputServiceTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using FluentAssertions;
using GotNuget.Exceptions;
using GotNuget.Services;
using GotNugetTests.Helpers;
using Moq;

namespace GotNugetTests.Services;

public class ActionOutputServiceTests
{
    private readonly Mock<IGitHubConsoleService> _mockConsoleService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActionOutputServiceTests"/> class.
    /// </summary>
    public ActionOutputServiceTests() => _mockConsoleService = new Mock<IGitHubConsoleService>();

    #region Method Tests
    [Fact]
    public void SetOutputValue_WhenInvoked_SetsOutputValue()
    {
        // Arrange
        var service = CreateService();

        // Act
        service.SetOutputValue("my-output", "my-value");

        // Assert
        _mockConsoleService.VerifyOnce(m => m.WriteLine("::set-output name=my-output::my-value"));
    }

    [Fact]
    public void SetOutputValue_WithNullOrEmptyOutputName_ThrowsException()
    {
        // Arrange
        var service = CreateService();

        // Act
        var act = () => service.SetOutputValue(null, It.IsAny<string>());

        // Assert
        act.Should()
            .Throw<NullOrEmptyStringException>()
            .WithMessage("The parameter 'name' must not be null or empty.");
    }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="ActionOutputService"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private ActionOutputService CreateService() => new ActionOutputService(_mockConsoleService.Object);
}
