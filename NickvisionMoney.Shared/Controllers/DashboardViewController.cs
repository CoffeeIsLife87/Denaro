﻿using NickvisionMoney.Shared.Helpers;
using System.Collections.Generic;

namespace NickvisionMoney.Shared.Controllers;

/// <summary>
/// An amount in the dashboard
/// </summary>
public class DashboardAmount
{
    /// <summary>
    /// The list of currencies in the amount
    /// </summary>
    public List<(string Code, string Symbol)> Currencies { get; init; }
    /// <summary>
    /// The breakdown dictionary 
    /// </summary>
    public Dictionary<(string Code, string Symbol), string> Breakdowns { get; init; }

    /// <summary>
    /// Constructs a DashboardAmount
    /// </summary>
    public DashboardAmount()
    {
        Currencies = new List<(string Code, string Symbol)>();
        Breakdowns = new Dictionary<(string Code, string Symbol), string>();
    }
}

/// <summary>
/// A controller for the a DashboardView
/// </summary>
public class DashboardViewController
{
    private List<AccountViewController> _openAccounts;

    /// <summary>
    /// The localizer to get translated strings from
    /// </summary>
    public Localizer Localizer { get; init; }
    /// <summary>
    /// The DashboardAmount object for incomes
    /// </summary>
    public DashboardAmount Income { get; init; }
    /// <summary>
    /// The DashboardAmount object for expenses
    /// </summary>
    public DashboardAmount Expense { get; init; }
    /// <summary>
    /// The DashboardAmount object for totals
    /// </summary>
    public DashboardAmount Total { get; init; }

    /// <summary>
    /// Constructs a DashboardViewController
    /// </summary>
    ///  <param name="localizer">The Localizer of the app</param>
    public DashboardViewController(List<AccountViewController> openAccounts, Localizer localizer)
    {
        _openAccounts = openAccounts;
        Localizer = localizer;
        Income = new DashboardAmount();
        Expense = new DashboardAmount();
        Total = new DashboardAmount();
        foreach (var controller in openAccounts)
        {
            (string Code, string Symbol) currency = (controller.CultureForNumberString.NumberFormat.NaNSymbol, controller.CultureForNumberString.NumberFormat.CurrencySymbol);
            if (controller.AccountTodayIncome > 0)
            {
                if (!Income.Currencies.Contains(currency))
                {
                    Income.Currencies.Add(currency);
                    Income.Breakdowns[currency] = "";
                }
                Income.Breakdowns[currency] = Income.Breakdowns[currency] + $"{string.Format(Localizer["AmountFromAccount"], controller.AccountTodayIncomeString, controller.AccountTitle)}\n\n";
            }
            if (controller.AccountTodayExpense > 0)
            {
                if (!Expense.Currencies.Contains(currency))
                {
                    Expense.Currencies.Add(currency);
                    Expense.Breakdowns[currency] = "";
                }
                Expense.Breakdowns[currency] = Expense.Breakdowns[currency] + $"{string.Format(Localizer["AmountFromAccount"], controller.AccountTodayExpenseString, controller.AccountTitle)}\n\n";
            }
            if (controller.AccountTodayTotal != 0)
            {
                if (!Total.Currencies.Contains(currency))
                {
                    Total.Currencies.Add(currency);
                    Total.Breakdowns[currency] = "";
                }
                Total.Breakdowns[currency] = Total.Breakdowns[currency] + $"{string.Format(Localizer["AmountFromAccount"], controller.AccountTodayTotalString, controller.AccountTitle)}\n\n";
            }
        }
    }
}
