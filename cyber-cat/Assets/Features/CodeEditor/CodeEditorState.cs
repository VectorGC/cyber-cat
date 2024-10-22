﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Domain.Verdicts.TestCases;
using UniMob;

public class CodeEditorState : ILifetimeScope
{
    public event Action SectionChanged;

    [Atom] public ISection Section { get; set; }
    [Atom] public ProgressStatus? ProgressStatus { get; set; }

    public Lifetime Lifetime { get; }

    public CodeEditorState(Lifetime lifetime)
    {
        Lifetime = lifetime;
    }

    public void CallSectionChanged()
    {
        SectionChanged?.Invoke();
    }

    public void SelectTestCaseAtIndex(int index)
    {
        switch (Section)
        {
            case ResultSection resultSection:
                resultSection.SelectedTestCaseId = resultSection.TestCaseIds?[index];
                break;
            case TestCasesSection testCasesSection:
                testCasesSection.SelectedTestCaseId = testCasesSection.TestCaseIds?[index];
                break;
        }
    }

    public TestCaseId GetTestCaseIdAtIndex(int index)
    {
        if (GetTestCaseIdCount() <= index)
        {
            return null;
        }

        return Section switch
        {
            ResultSection resultSection => resultSection.TestCaseIds?[index],
            TestCasesSection testCasesSection => testCasesSection.TestCaseIds?[index],
            _ => null
        };
    }

    private int GetTestCaseIdCount()
    {
        return Section switch
        {
            ResultSection resultSection => resultSection.TestCaseIds?.Count ?? 0,
            TestCasesSection testCasesSection => testCasesSection.TestCaseIds?.Count ?? 0,
            _ => 0
        };
    }

    public TestCaseVerdict GetTestCaseVerdictById(TestCaseId testCaseId)
    {
        return Section switch
        {
            ResultSection resultSection => resultSection.TestCasesVerdict?[testCaseId],
            _ => null
        };
    }

    public TestCaseDescription GetTestCaseById(TestCaseId testCaseId)
    {
        return Section switch
        {
            TestCasesSection testCasesSection => testCasesSection.TestCases?.FirstOrDefault(x => x.Id == testCaseId),
            _ => null
        };
    }

    public TestCaseVerdict GetSelectedTestCaseVerdict()
    {
        return Section switch
        {
            ResultSection resultSection => resultSection.SelectedTestCaseVerdict,
            _ => null
        };
    }

    public TestCaseDescription GetSelectedTestCase()
    {
        return Section switch
        {
            TestCasesSection testCasesSection => testCasesSection.SelectedTestCase,
            _ => null
        };
    }

    public TestCaseId GetSelectedTestCaseId()
    {
        return Section switch
        {
            TestCasesSection testCasesSection => testCasesSection.SelectedTestCaseId,
            ResultSection resultSection => resultSection.SelectedTestCaseId,
            _ => null
        };
    }

    public Verdict GetVerdict()
    {
        return (Section as ResultSection)?.Verdict;
    }
}

public interface ISection : ILifetimeScope
{
}

public class TestCasesSection : ISection
{
    [Atom] public List<TestCaseDescription> TestCases { get; set; }
    [Atom] public TestCaseId SelectedTestCaseId { get; set; }
    [Atom] public List<TestCaseId> TestCaseIds => TestCases.Select(x => x.Id).ToList();
    [Atom] public TestCaseDescription SelectedTestCase => SelectedTestCaseId != null ? TestCases?.FirstOrDefault(x => x.Id == SelectedTestCaseId) : null;

    public Lifetime Lifetime { get; }

    public TestCasesSection(Lifetime lifetime)
    {
        Lifetime = lifetime;
    }
}

public class ResultSection : ISection
{
    [Atom] public Verdict Verdict { get; set; }

    [Atom]
    public TestCaseId SelectedTestCaseId
    {
        get => TestCaseIds != null && TestCaseIds.Count > _selectedTestCaseIndex ? TestCaseIds[_selectedTestCaseIndex] : null;
        set => _selectedTestCaseIndex = TestCaseIds?.FindIndex(id => id.Equals(value)) ?? 0;
    }

    [Atom] public TestCasesVerdict TestCasesVerdict => Verdict?.TestCases;
    [Atom] public List<TestCaseId> TestCaseIds => TestCasesVerdict?.Values.Values.Select(test => test.TestCase.Id).ToList();
    [Atom] public TestCaseVerdict SelectedTestCaseVerdict => SelectedTestCaseId != null ? TestCasesVerdict?[SelectedTestCaseId] : null;

    [Atom] private int _selectedTestCaseIndex { get; set; } = 0; // Force select first tab.

    public Lifetime Lifetime { get; }

    public ResultSection(Lifetime lifetime)
    {
        Lifetime = lifetime;
    }
}

public readonly struct ProgressStatus
{
    public string StatusText { get; }

    public ProgressStatus(string statusText)
    {
        StatusText = statusText;
    }
}

public static class CodeEditorStateExtensions
{
    public static async UniTask<T> ToReportProgressStatus<T>(this Task<T> task, CodeEditorState state, string statusText)
    {
        state.ProgressStatus = new ProgressStatus(statusText);
        var result = await task;
        state.ProgressStatus = null;

        return result;
    }
}