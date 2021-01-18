using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace JFramework.Common.Logging
{
    public class CrashReport
    {

        
        public const string CSS =
@"
body {
	background: #f2f2f2;
	text: black;
    margin: 1% auto;
    color: #444444; font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; 
    font-size: 16px; 
    line-height: 1.8; 
    text-shadow: 0 1px 0 #ffffff; 
    max-width: 90%;
}
.red {
    text: red;   
}
.blue {
    text: blue;   
}
.data {
  border: 3px outset black;
  background-color: white;
  color: black;
  text-align: left;
  font-family: monospace;
  font-size: 16px; 
  line-height: 1.0;
  margin-left: 20px;
}

code {background: white;}
a {border-bottom: 1px solid #444444; color: #444444; text-decoration: none;}
a:hover {border-bottom: 0;}

";
        public string Title { get; set; }
        public string ProgramName { get; set; }
        public string OutputDirectory = "crashlogs";
        public string TimestampFormat = "MM-dd-yy-HH-mm-ss";

        Exception Exception;

        public CrashReport(Microsoft.Xna.Framework.Game game, Exception e)
        {
            Exception = e;
        }


        private string WriteEnvironmentData()
        {
            string text = "";

            void wr(string key, object val) => text+= $"<tr><td>{key}</td><td>{val}</td></tr>";

			wr("IsOS64Bit",         Environment.Is64BitOperatingSystem);
			wr("IsExe64Bit",        Environment.Is64BitProcess);
			wr("OperatingSystem",   $"{Environment.OSVersion.Platform} >> {Environment.OSVersion.VersionString}");
			wr("Architecture",      Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));
			wr("ProcessorID",       Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"));
			wr("ProcessorLevel",    Environment.GetEnvironmentVariable("PROCESSOR_LEVEL"));
			wr("CLRVersion",        Environment.Version);
			wr("WorkingSet",        Environment.WorkingSet);
			wr("ProcessorCount",    Environment.ProcessorCount);
#if DESKTOP
			wr("SystemUptime",      Environment.TickCount64);
#endif
			wr("UserName",          Environment.UserName);

            return text;
        }

        private string Timestamp() => DateTime.Now.ToString(TimestampFormat);


        string SubmittedStats = "";
        public void AddStat(string key, object value)
        {
            SubmittedStats += $"<tr><td>{key}</td><td>{value}</td></tr>";
        }



        public void WriteToFile()
        {
            Directory.CreateDirectory(OutputDirectory);
            string name = Path.Combine(OutputDirectory, $"crash_{Timestamp()}.html");
            File.WriteAllText(name, GenerateHTMLReport());
            OS.OpenUrl(Path.GetFullPath(name));
        }

        public string GenerateHTMLReport()
        {
            string crashReportHTML =
$@"
<!DOCTYPE html>
<html>
<head>
<meta charset='UTF-8'>
<style>{CSS}</style>
<title>{Title}</title>
</head>
<body>
<h3>{Title}</h3>

<p>{ProgramName} has experienced a fatal crash! This file has been generated to document the error, and make reporting bugs to the devs easy.</p>

<p>Please consider sending this report to us. It helps us tremendously when tracking down bugs.</p>
<p>You can send this to us via Discord, Steam, or Twitter</p>


<h4>Exception:</h4>
<div class='data'>
<p>{Exception.Message.Replace(" at", "<br/>at")}</p>
</div>

<h4>StackTrace:</h4>
<div class='data'><p>{Exception.StackTrace.Replace(" at", "<br/>at")}</p></div>

<h4>Environment:</h4>
<div class='data'>
<table style='width:100%'>{WriteEnvironmentData()}</table>
</div>

<h4>Collected Data:</h4>
<div class='data'>
<table style='width:100%'>
{SubmittedStats}
</table>
</div>
</body>
</html>
";
            return crashReportHTML;
        }
    }
}
