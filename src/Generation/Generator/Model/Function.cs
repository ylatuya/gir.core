﻿using System.Linq;

namespace Generator.Model;

internal static partial class Function
{
    public const string GetGType = "GetGType";

    public static string GetName(GirModel.Function function)
    {
        if (function.Shadows is null)
            return function.Name.ToPascalCase().EscapeIdentifier();

        if (function.Parameters.Count() != function.Shadows.Parameters.Count())
            return function.Shadows.Name.ToPascalCase().EscapeIdentifier();

        if (function.Parameters.Select(x => x.AnyTypeOrVarArgs).Except(function.Shadows.Parameters.Select(x => x.AnyTypeOrVarArgs)).Any())
            return function.Shadows.Name.ToPascalCase().EscapeIdentifier();

        return function.Name.ToPascalCase().EscapeIdentifier();
    }

    public static string GetImportResolver(GirModel.Function function)
    {
        return function switch
        {
            //TODO Workaround as long as GObject and GLib are split up. GLib contains the e.g. "Bytes" record
            //but the type definition is part of GObject. Therefore the GetGType function is part of GObject.
            { Parent.Namespace.Name: "GLib", Name: GetGType } => "GObject.Internal.ImportResolver.Library",
            _ => "ImportResolver.Library"
        };
    }
}
