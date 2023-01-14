﻿using NickvisionMoney.Shared.Models;
using System;

namespace NickvisionMoney.Shared.Controls;

public interface IGroupRowControl : IModelRowControl<Group>
{
    /// <summary>
    /// Occurs when the filter checkbox is changed on the row
    /// </summary>
    public event EventHandler<(int Id, bool Filter)>? FilterChanged;

    /// <summary>
    /// Updates the row based on the new Group model
    /// </summary>
    /// <param name="group">The new Group model</param>
    void IModelRowControl<Group>.UpdateRow(Group group) => UpdateRow(group, true);

    /// <summary>
    /// Updates the row based on the new Group model
    /// </summary>
    /// <param name="group">The new Group model</param>
    /// <param name="filterActive">Whether or not the filter checkbox is active</param>
    public void UpdateRow(Group group, bool filterActive);
}
