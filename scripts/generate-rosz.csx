using System.IO.Compression;
using System.Runtime.CompilerServices;

var root = GetProjectRootFolder();
var rosters = Directory.GetFiles(Path.Combine(root, "rosters"), "*.ros", SearchOption.AllDirectories);
foreach (var rosPath in rosters)
{
    var rosFileName = Path.GetFileName(rosPath);
    var roszFileName = Path.ChangeExtension(rosFileName, "rosz");
    var roszPath = Path.Combine(root, "artifacts", roszFileName);

    using var outputStream = File.Open(roszPath, FileMode.Create, FileAccess.Write);
    using var archive = new ZipArchive(outputStream, ZipArchiveMode.Create);
    archive.CreateEntryFromFile(rosPath, rosFileName);
}

static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);
static string GetProjectRootFolder() => Path.GetFullPath(Path.Combine(GetScriptFolder(), ".."));
