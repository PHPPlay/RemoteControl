using System.Threading;

namespace iWay.RemoteControlBase.Utilities
{
    public static class ThreadUtils
    {
        public static Thread CreateThread(ThreadStart target)
        {
            Thread thread = new Thread(target);
            thread.Start();
            return thread;
        }

        public static Thread CreateThread(ParameterizedThreadStart target)
        {
            Thread thread = new Thread(target);
            thread.Start();
            return thread;
        }

        public static Thread CreateThread(ParameterizedThreadStart target, object param)
        {
            Thread thread = new Thread(target);
            thread.Start(param);
            return thread;
        }

        public static Thread CreateThread(ThreadStart target, ApartmentState state)
        {
            Thread thread = new Thread(target);
            thread.SetApartmentState(state);
            thread.Start();
            return thread;
        }


        public static Thread CreateThread(ParameterizedThreadStart target, ApartmentState state)
        {
            Thread thread = new Thread(target);
            thread.SetApartmentState(state);
            thread.Start();
            return thread;
        }

        public static Thread CreateThread(ParameterizedThreadStart target, object param, ApartmentState state)
        {
            Thread thread = new Thread(target);
            thread.SetApartmentState(state);
            thread.Start(param);
            return thread;
        }

        public static Thread CreateBackgroundThread(ThreadStart target)
        {
            Thread thread = new Thread(target);
            thread.IsBackground = true;
            thread.Start();
            return thread;
        }

        public static Thread CreateBackgroundThread(ParameterizedThreadStart target)
        {
            Thread thread = new Thread(target);
            thread.IsBackground = true;
            thread.Start();
            return thread;
        }

        public static Thread CreateBackgroundThread(ParameterizedThreadStart target, object param)
        {
            Thread thread = new Thread(target);
            thread.IsBackground = true;
            thread.Start(param);
            return thread;
        }

        public static Thread CreateBackgroundThread(ThreadStart target, ApartmentState state)
        {
            Thread thread = new Thread(target);
            thread.SetApartmentState(state);
            thread.IsBackground = true;
            thread.Start();
            return thread;
        }

        public static Thread CreateBackgroundThread(ParameterizedThreadStart target, ApartmentState state)
        {
            Thread thread = new Thread(target);
            thread.SetApartmentState(state);
            thread.IsBackground = true;
            thread.Start();
            return thread;
        }

        public static Thread CreateBackgroundThread(ParameterizedThreadStart target, object param, ApartmentState state)
        {
            Thread thread = new Thread(target);
            thread.SetApartmentState(state);
            thread.IsBackground = true;
            thread.Start(param);
            return thread;
        }
    }
}


