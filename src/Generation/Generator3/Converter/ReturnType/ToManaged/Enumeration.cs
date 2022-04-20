﻿using GirModel;

namespace Generator3.Converter.ReturnType.ToManaged;

internal class Enumeration : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Enumeration>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName;
}
