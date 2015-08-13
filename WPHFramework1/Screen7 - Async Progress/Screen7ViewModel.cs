using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPHFramework1
{
    public class Screen7ViewModel : PropertyChangedBase
    {
        CancellationTokenSource _cts;

        public async void StartProcess()
        {
           
            // Construct Progress<T>, passing the ReportProgress() method as the Action<T> callback
            var progressIndicator = new Progress<int>(ReportProgress);

            _cts = new CancellationTokenSource();

            try
            {
                //Launch the process
                int items = await DoBigWorkAsync(null, progressIndicator, _cts.Token);
            }
            catch (OperationCanceledException ex)
            {
                // Do whatever we need to do to clean up a cancellation
            }
            finally
            {
                PauseThenClearProgress();
            }

        }

        public void CancelProcess()
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }
        }

        public bool CanCancelProcess
        {
            get { return ProgressValue != 0; }
        }

        async Task<int> DoBigWorkAsync(object argObj, IProgress<int> progress, CancellationToken ct)
        {
            int totalCount = 12;
            int processCount = await Task.Run<int>(async() =>
            {
                int tempCount = 0;
                for (int i = 0; i < totalCount; i++)
                {
                    await DoSomeWorkAsync();
                    ct.ThrowIfCancellationRequested();

                    if (progress != null)
                    {
                        progress.Report((tempCount * 100) / totalCount);
                    }
                    tempCount++;
                }
                return tempCount;
            });

            ProgressValue = 100;
            return processCount;
        }

        async Task DoSomeWorkAsync()
        {
            await Task.Delay(500);
        }

        void ReportProgress(int value)
        {
            ProgressValue = value;
        }

        private async void PauseThenClearProgress()
        {
            await Task.Delay(1000); // pause a sec to let user see final progress value
            ProgressValue = 0;
        }

        private int _progressValue;
        public int ProgressValue
        {
            get { return _progressValue; }
            set {
                _progressValue = value;
                NotifyOfPropertyChange(() => ProgressValue);
                NotifyOfPropertyChange(() => CanCancelProcess);
            }
        }
        
    }
}
