using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Library.Utilities
{
    public class ThreadCls
    {
        List<BackgroundWorker> threads;

        public delegate void work();

        work DoWork, Completed;

        bool isStop = false;

        int TaskIndex = 0;

        object objLock = new object();

        public ThreadCls(int threadCount, work _doWork, work _completed)
        {
            this.DoWork = _doWork;
            this.Completed = _completed;

            threads = new List<BackgroundWorker>();

            for (int i = 0; i < threadCount; i++)
            {
                BackgroundWorker thread = new BackgroundWorker();
                thread.DoWork += thread_DoWork;
                thread.RunWorkerCompleted += thread_RunWorkerCompleted;
                threads.Add(thread);
            }
        }

        public void RunThread()
        {
            this.Reset();

            foreach (var thread in threads)
                thread.RunWorkerAsync();
        }

        private void Reset()
        {
            TaskIndex = 0;
            isStop = false;
        }

        public int GetTaskIndex()
        {
            lock (objLock)
            {
                TaskIndex++;
                return TaskIndex;
            }
        }

        public void Stop()
        {
            this.isStop = true;
        }

        void thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isStop)
            {
                if (!threads.Any(_ => _.IsBusy))
                    this.Completed();
            }
            else ((BackgroundWorker)sender).RunWorkerAsync();
        }

        void thread_DoWork(object sender, DoWorkEventArgs e)
        {
            this.DoWork();
        }


        //// Khởi tạo
        //Library.Utilities.ThreadCls thread = new Library.Utilities.ThreadCls(8,TaoLaiCongNo, LoadData );

        //// Chạy thead
        //thread.RunThread();

        //// Lấy stt công việc
        //thread.GetTaskIndex();

        //// Hàm phát tín hiệu hết việc
        //thread.Stop();
    }
}
