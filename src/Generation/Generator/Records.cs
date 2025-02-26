﻿using System.Collections.Generic;
using System.Linq;
using Generator.Generator;
using Generator.Model;

namespace Generator;

public static class Records
{
    public static void Generate(IEnumerable<GirModel.Record> records, string path)
    {
        var publisher = new Publisher(path);
        var generators = new List<Generator<GirModel.Record>>()
        {
            //Opaque typed records
            new Generator.Internal.OpaqueTypedRecord(publisher),
            new Generator.Internal.OpaqueTypedRecordHandle(publisher),
            new Generator.Public.OpaqueTypedRecord(publisher),
            
            //Opaque untyped records
            new Generator.Internal.OpaqueUntypedRecord(publisher),
            new Generator.Internal.OpaqueUntypedRecordHandle(publisher),
            new Generator.Public.OpaqueUntypedRecord(publisher),
            
            //Regular records
            new Generator.Internal.RecordDelegates(publisher),
            new Generator.Internal.RecordHandle(publisher),
            new Generator.Internal.RecordOwnedHandle(publisher),
            new Generator.Internal.RecordMethods(publisher),
            new Generator.Internal.RecordStruct(publisher),
            new Generator.Internal.RecordManagedHandle(publisher),
            new Generator.Public.RecordClass(publisher)
        };

        foreach (var record in records.Where(Record.IsEnabled))
            foreach (var generator in generators)
                generator.Generate(record);
    }
}
