using Positioner.Manager;
using Positioner.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positioner.Serives
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
            var beforeWindows = WindowManager.GetAllWindowHandles();

            Process.Start(new ProcessStartInfo
                {
                    FileName = "chrome.exe",
                    Arguments = $"--new-window \"{url}\"",
                    UseShellExecute = true // important for opening URLs
                });

            // Poll for the new window handle (up to 10s)
            IntPtr? windowHandle = null;
            for (int i = 0; i < 10; i++)
            {
                var afterWindows = WindowManager.GetAllWindowHandles().ToHashSet();
                afterWindows.ExceptWith(beforeWindows);
                windowHandle = afterWindows.FirstOrDefault();
                if (windowHandle.HasValue) break;
                Thread.Sleep(1000);
            }

            // Open the remaining URLs in the same browser (they will open in tabs)
            for (int i = 1; i < spec.Urls.Count; i++)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "chrome.exe",
                    Arguments = $"\"{spec.Urls[i]}\"",
                    UseShellExecute = true
                });
            }

            // Move and resize the new window
            if (windowHandle.HasValue)
            {
                WindowManager.MoveWindowHandle(windowHandle.Value, pos.X, pos.Y, pos.Width, pos.Height);
                Console.WriteLine($"Opened window for {entity.Metadata.Name} at ({pos.X},{pos.Y}) size {pos.Width}x{pos.Height}");
            }
            else
            {
                Console.WriteLine("Could not detect new browser window.");
            }

        }
    }
}
