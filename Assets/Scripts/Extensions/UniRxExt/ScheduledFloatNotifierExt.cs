using System;
using System.Linq;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Extensions.UniRxExt
{
    public static class ScheduledFloatNotifierExt
    {
        public static IDisposable ReportTo([CanBeNull] this IObservable<float> observableProgress,
            IProgress<float> progressReporter)
        {
            if (progressReporter == null)
            {
                return observableProgress.Subscribe();
            }

            return observableProgress.Do(progressReporter.Report).Subscribe();
        }

        public static IObservable<float> Union(this IObservable<float> progressA,
            IObservable<float> progressB)
        {
            // Set initial values.
            progressA = progressA.StartWith(0);
            progressB = progressB.StartWith(0);

            var averageBuffer = new float[2];

            var combinedProgress = progressA.CombineLatest(progressB,
                (progressAValue, progressBValue) =>
                {
                    averageBuffer[0] = progressAValue;
                    averageBuffer[1] = progressBValue;

                    return averageBuffer.Average();
                });

            return combinedProgress;
        }
    }
}