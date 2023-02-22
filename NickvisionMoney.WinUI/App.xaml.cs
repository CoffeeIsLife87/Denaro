﻿using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;
using NickvisionMoney.Shared.Controllers;
using NickvisionMoney.Shared.Models;
using NickvisionMoney.WinUI.Views;
using System;

namespace NickvisionMoney.WinUI;

/// <summary>
/// The App
/// </summary>
public partial class App : Application
{
    public static Window? MainWindow { get; private set; } = null;
    private readonly MainWindowController _mainWindowController;

    /// <summary>
    /// Constructs an App
    /// </summary>
    public App()
    {
        InitializeComponent();
        _mainWindowController = new MainWindowController();
        //AppInfo
        _mainWindowController.AppInfo.ID = "org.nickvision.money";
        _mainWindowController.AppInfo.Name = "Nickvision Denaro";
        _mainWindowController.AppInfo.ShortName = "Denaro";
        _mainWindowController.AppInfo.Description = $"{_mainWindowController.Localizer["Description"]}.";
        _mainWindowController.AppInfo.Version = "2023.2.1";
        _mainWindowController.AppInfo.Changelog = "- New and improved icon (Thanks @daudix-UFO)!\n- Fixed an issue where editing text field would always move the cursor\nto the end of the box\n- Various UX improvements\n- Updated and added translations (Thanks to everyone on Weblate)!";
        _mainWindowController.AppInfo.GitHubRepo = new Uri("https://github.com/nlogozzo/NickvisionMoney");
        _mainWindowController.AppInfo.IssueTracker = new Uri("https://github.com/nlogozzo/NickvisionMoney/issues/new");
        _mainWindowController.AppInfo.SupportUrl = new Uri("https://github.com/nlogozzo/NickvisionMoney/discussions");
        //Theme
        if (_mainWindowController.Theme == Theme.Light)
        {
            RequestedTheme = ApplicationTheme.Light;
        }
        else if (_mainWindowController.Theme == Theme.Dark)
        {
            RequestedTheme = ApplicationTheme.Dark;
        }
    }

    /// <summary>
    /// Finalizes an App
    /// </summary>
    ~App() => _mainWindowController.Dispose();

    /// <summary>
    /// Occurs when the app is launched
    /// </summary>
    /// <param name="args">LaunchActivatedEventArgs</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var activatedArgs = AppInstance.GetCurrent().GetActivatedEventArgs();
        if (activatedArgs.Kind == ExtendedActivationKind.File)
        {
            var fileArgs = (Windows.ApplicationModel.Activation.IFileActivatedEventArgs)activatedArgs.Data;
            _mainWindowController.FileToLaunch = fileArgs.Files[0].Path;
        }
        MainWindow = new MainWindow(_mainWindowController);
        MainWindow.Activate();
    }
}