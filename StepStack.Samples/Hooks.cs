﻿using BoDi;
using EzSpecflow.Extensions;
using TechTalk.SpecFlow;

namespace EzSpecflow;

[Binding]
public sealed class Hooks
{
    private readonly IObjectContainer _objectContainer;

    public Hooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeScenario(Order = 99)]
    public void BeforeScenario()
    {
        _objectContainer.RegisterRetryPolicyFactory<TestRetryPolicyFactory>("test");
        _objectContainer.RegisterRetryPolicyFactory<SampleRetryPolicyFactory>("sample");
    }
}