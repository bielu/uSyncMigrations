using System.Text;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Serialization;
using Umbraco.Extensions;

namespace uSync.Migrations.LegacyModels.Grid;

public interface IGridConfig
{
    IGridEditorsConfig EditorsConfig { get; }
}
public interface IGridEditorConfig
{
    string? Name { get; }

    string? NameTemplate { get; }

    string Alias { get; }

    string? View { get; }

    string? Render { get; }

    string? Icon { get; }

    IDictionary<string, object> Config { get; }
}
public interface IGridEditorsConfig
{
    IEnumerable<IGridEditorConfig> Editors { get; }
}
internal class GridEditorsConfig : IGridEditorsConfig
{
 


    public IEnumerable<IGridEditorConfig> Editors
    {
        get;
    }

    private static IFileInfo? GetConfigFile(IFileProvider fileProvider, string path)
    {
        IFileInfo fileInfo = fileProvider.GetFileInfo($"{path}/grid.editors.config.js");
        return fileInfo.Exists ? fileInfo : null;
    }
}
public class GridConfig : IGridConfig
{

    public IGridEditorsConfig EditorsConfig { get; }
}