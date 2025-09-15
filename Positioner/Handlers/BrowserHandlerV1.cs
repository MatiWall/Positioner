using Positioner.Interop;
using Positioner.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positioner.Services
{
    internal class BrowserSetupServiceV1: IEntityHandler
    {
        public string Kind => "Browser";
        public string Version => "V1";

        public void Handle(IEntity entity) {

            if (entity.Spec is not BrowserSpecV1 spec)
            {
                throw new InvalidOperationException("Invalid spec type for BrowserSetupServiceV1");
            }

            if (spec.Urls == null || spec.Urls.Count == 0)
            {
                Console.WriteLine("No urls specified in spec");
                return;
            }

            Position pos = spec.Position;


            string url = spec.Urls[0];

            Process process = new Process();
            process.StartInfo.FileName = "chrome.exe";
            process.StartInfo.Arguments = url + " --new-window --incognito";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.StartInfo.UseShellExecute = true;

            try
            {
                process.Start();
                while (!process.HasExited) { 
                    IntPtr handleId = process.MainWindowHandle;

                    if (handleId.ToInt32() != 0)
                    {
                        bool result = WinAPI.MoveWindow(handleId, pos.X, pos.Y, pos.Width, pos.Height, true);
                        Thread.Sleep(500);

                        if (result == true)
                        {
                            break;
                        }
                    }
                }
            }
            catch { 
                process.Dispose();
            }

            //Process? process = Process.Start(new ProcessStartInfo
            //    {
            //        FileName = "chrome.exe",
            //        Arguments = $"--new-window \"{url}\"",
            //        UseShellExecute = true // important for opening URLs
            //    });
           

            //process.WaitForInputIdle();
            //IntPtr hWnd = IntPtr.Zero;

            //for (int i = 0; i < 20; i++) // up to ~10 seconds
            //{
            //    process.Refresh(); // refresh process info
            //    hWnd = process.MainWindowHandle;
            //    if (hWnd != IntPtr.Zero)
            //        break;

            //    Thread.Sleep(500);
            //}


            // Open the remaining URLs in the same browser (they will open in tabs)
            //for (int i = 1; i < spec.Urls.Count; i++)
            //{
            //    Process.Start(new ProcessStartInfo
            //    {
            //        FileName = "chrome.exe",
            //        Arguments = $"\"{spec.Urls[i]}\"",
            //        UseShellExecute = true
            //    });
            //}

            // Move and resize the new window
            //WinAPI.ShowWindow(hWnd, WinAPI.SW_RESTORE);
            
            //Console.WriteLine($"Opened window for {entity.Metadata.Name} at ({pos.X},{pos.Y}) size {pos.Width}x{pos.Height}");


        }
    }
}
