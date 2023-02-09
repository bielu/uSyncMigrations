﻿using System.Text.RegularExpressions;

using Newtonsoft.Json.Linq;

using Polly;

using Umbraco.Cms.Core.Configuration.Grid;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

using uSync.Migrations.Migrators.Models;
using uSync.Migrations.Models;

namespace uSync.Migrations.Migrators.BlockGrid.BlockMigrators;
internal class DocTypeGridEditorBlockMigrator : ISyncBlockMigrator
{
	public string[] Aliases => new[] { "docType" };

	/// <summary>
	///  the DTGE doesn't generate any new content types, 
	///  because all the content types it uses are already
	///  in the migration.
	/// </summary>
	public IEnumerable<NewContentTypeInfo> AdditionalContentTypes(IGridEditorConfig editorConfig)
		=> Enumerable.Empty<NewContentTypeInfo>();

	public IEnumerable<string> GetAllowedContentTypes(IGridEditorConfig config, SyncMigrationContext context)
	{
		if (config?.Config.TryGetValue("allowedDocTypes", out var allowedDocTypesValue) == true
				  && allowedDocTypesValue is JArray allowedDocTypes)
		{
			// dtge. 
			var allowedDocTypeExpressions = allowedDocTypes.Values<string>().ToArray();
			if (allowedDocTypeExpressions.Length == 0) return Enumerable.Empty<string>();

			var allContentTypeAliases = context.GetContentTypeAliases();

			return allContentTypeAliases
					.Where(contentTypeAlias =>
						allowedDocTypeExpressions.WhereNotNull()
						.Any(allowedExpression => Regex.IsMatch(contentTypeAlias, allowedExpression, RegexOptions.IgnoreCase) == true));
		}

		return Enumerable.Empty<string>();
	}



	/// <summary>
	///  returns the actual doctype this content value is using
	/// </summary>
	/// <param name="control"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public string GetContentTypeAlias(GridValue.GridControl control)
		=> control.Value?.Value<string>("dtgeContentTypeAlias") ?? string.Empty;

	/// <remarks>
	///  for dtge - this isn't one answer to what is the content type, 
	///  when we are looking at the config. 
	///  
	///  we assume that the migration already contains the content types
	///  that are using in the DTGE so we don't actually have to pass 
	///  things back to the migration process at this point
	/// </remarks>
	public string GetContentTypeAlias(IGridEditorConfig editorConfig)
		=> string.Empty;

	public string GetEditorAlias(IGridEditorConfig editor)
		=> string.Empty;

	public Dictionary<string, object> GetPropertyValues(GridValue.GridControl control, SyncMigrationContext context)
	{
		var propertyValues = new Dictionary<string, object>();

		var contentTypeAlias = GetContentTypeAlias(control);
		if (string.IsNullOrWhiteSpace(contentTypeAlias)) return propertyValues;

		var elementValue = control.Value?.Value<JObject>("value")?
			.ToObject<IDictionary<string, object>>();

		if (elementValue == null) return propertyValues;

		foreach (var (propertyAlias, value) in elementValue)
		{
			var editorAlias = context.GetEditorAlias(contentTypeAlias, propertyAlias);

			if (editorAlias == null) continue;

			var migrator = context.TryGetMigrator(editorAlias.OriginalEditorAlias);
			var propertyValue = value;

			if (migrator != null) { 
				var property = new SyncMigrationContentProperty(
					editorAlias.OriginalEditorAlias,
					value?.ToString() ?? string.Empty);

				propertyValue = migrator.GetContentValue(property, context);
			}

			propertyValues[propertyAlias] = propertyValue;
		}

		return propertyValues;
	}
}
