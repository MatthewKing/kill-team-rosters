using System.IO.Compression;
using System.Runtime.CompilerServices;

var rootDirectory = GetProjectRootFolder();
var rostersDirectory = Path.Combine(rootDirectory, "rosters");
var rosters = Directory.GetFiles(rostersDirectory, "*.ros", SearchOption.AllDirectories);
foreach (var rosPath in rosters)
{
    var rosFileName = Path.GetFileName(rosPath);
    var rosPathRelativeToRosters = Path.GetRelativePath(rostersDirectory, rosPath);
    var roszFileName = Path.ChangeExtension(rosPathRelativeToRosters, "rosz")
        .Replace(Path.DirectorySeparatorChar, '-')
        .Replace(Path.AltDirectorySeparatorChar, '-');
    var roszPath = Path.Combine(rootDirectory, "artifacts", roszFileName);

    using var outputStream = File.Open(roszPath, FileMode.Create, FileAccess.Write);
    using var archive = new ZipArchive(outputStream, ZipArchiveMode.Create);
    archive.CreateEntryFromFile(rosPath, rosFileName);
}

static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);
static string GetProjectRootFolder() => Path.GetFullPath(Path.Combine(GetScriptFolder(), ".."));
