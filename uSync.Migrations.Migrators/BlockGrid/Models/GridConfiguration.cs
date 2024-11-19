using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Umbraco.Cms.Core;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Infrastructure.Serialization;

namespace uSync.Migrations.Migrators.BlockGrid.Models;

public class GridConfiguration : IIgnoreUserStartNodesConfig
{
    public JObject? Items { get; set; }

    public JObject? Rte { get; set; }

    public GuidUdi? MediaParentId { get; set; }

 
    public bool IgnoreUserStartNodes { get; set; }

    public GridConfiguration ToUmbracoGridConfiguration()
    {
        return new GridConfiguration()
        {
            Items = this.Items,
            Rte = this.Rte,
            MediaParentId = this.MediaParentId,
            IgnoreUserStartNodes = this.IgnoreUserStartNodes
        };
    }
}
