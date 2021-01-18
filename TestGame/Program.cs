using System;
using JFramework.Common.Logging;

namespace JFramework.TestGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
			// Test the crash report!
			using (var game = new Game1())
			{
#if DEBUG
				game.Run();
#else
				try
                {
                    game.Run();
                } catch(Exception e)
                {
                    CrashReport report = new CrashReport(game, e);

					report.ProgramName = "TestGame1";
					report.Title = "Crash Report";
                    report.WriteToFile();

					throw;
				}
#endif
			}
		}
    }
}
