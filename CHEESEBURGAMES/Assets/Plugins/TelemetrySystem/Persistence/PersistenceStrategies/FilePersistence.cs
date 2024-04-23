using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

public class FilePersistence : APersistance
{
    const string path = "./TrackerOutputs/";
    const int updateFrequenceInMiliseconds = 3000;

    private StreamWriter file;

    private Thread thread;
    private Mutex eventQueueMutex;

    private volatile bool threadRunning;
    private volatile bool mustFlush;

    public FilePersistence(ISerializer serializer) : base(serializer)
    {
    }

    public override void SendFlush()
    {
        mustFlush = true;
    }

    public override void Close()
    {
        SendFlush();
        threadRunning = false;
        thread.Join();
    }

    public override void Open()
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        threadRunning = true;   
        thread = new Thread(thread_run);
        eventQueueMutex = new Mutex();

        thread.Start();
    }

    public override void Send(TrackerEvent tEvent)
    {
        eventQueueMutex.WaitOne();

        base.Send(tEvent);

        eventQueueMutex.ReleaseMutex();
    }

    // --------  Thread methods  --------

    private void thread_run()
    {
        ThreadOpenFile();
        Stopwatch stopwatch= Stopwatch.StartNew();
      
        while (threadRunning)
        {
            if (stopwatch.ElapsedMilliseconds >= updateFrequenceInMiliseconds || mustFlush)
            {
                stopwatch.Restart(); 

                // Realizar el Flush
                Flush();
            }
        }

        ThreadCloseFile();
    }

    private void ThreadOpenFile()
    {
        // nombre con el timestamp
        string fileName = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + serializer.GetFileExtension();

        // crear el fichero
        // open el fichero y guardar el manejador
        file = new StreamWriter(Path.Combine(path, fileName));

        // inicio con el formato del serializador
        file.Write(serializer.InitFileFormat());
    }

    private void ThreadCloseFile()
    {
        file.Write(serializer.EndFileFormat());
        file.Close();
    }

    private void Flush()
    {
        mustFlush = false;

        while (eventQueue.Count > 0)
        {
            eventQueueMutex.WaitOne();
            TrackerEvent tEvent = eventQueue.Dequeue();
            eventQueueMutex.ReleaseMutex();
            file.Write(serializer.serialize(tEvent));
        }
    }
}
