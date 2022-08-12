﻿using System;
using System.Threading;
using System.Threading.Tasks;
using EzSpecflow.Abstractions;
using EzSpecflow.Models;

namespace EzSpecflow;

public class Step : IStep
{
    private readonly Func<Task> _step;

    public Step(
        string stepName, 
        Func<Task> step, 
        RetryPolicy retryPolicy, 
        string? stepDescription = null)
    {
        _step = step;
        Name = stepName;
        RetryPolicy = retryPolicy;
        Description = stepDescription;
    }

    public int ExecutionCount { get; private set; }
    public RetryPolicy RetryPolicy { get; }
    public string Name { get; }
    public string? Description { get; }
    public virtual async Task<StepResult> Execute(CancellationToken cancellationToken = default)
    {
        ExecutionCount++;
        
        try
        {
            await _step.Invoke();
            return new StepResult(this, true, ExecutionCount);
        }
        catch (Exception ex)
        {
            return new StepResult(this, false, ExecutionCount, null, ex);
        }
    }
}