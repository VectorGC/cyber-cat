using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Extensions.UniRxExt
{
    public static class ScheduledFloatNotifierExt
    {
        public static IObservable<float> ReportTo([CanBeNull] this IObservable<float> observableProgress,
            IProgress<float> progressReporter)
        {
            if (progressReporter == null)
            {
                return new ScheduledNotifier<float>();
            }

            observableProgress.Subscribe(progressReporter.Report);
            return observableProgress;
        }

        public static IObservable<float> Union(this IObservable<float> progressA,
            IObservable<float> progressB)
        {
            var combinedProgress = progressA.CombineLatest(progressB,
                (requestProgressValue, loadEditorProgressValue) =>
                    Mathf.InverseLerp(0, 2, requestProgressValue + loadEditorProgressValue));

            return combinedProgress;
        }
    }
}