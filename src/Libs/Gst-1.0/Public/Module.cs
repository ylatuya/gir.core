﻿namespace Gst;

public static class Module
{
    private static bool IsInitialized;

    /// <summary>
    /// Initialize the <c>Gst</c> module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling this method is necessary to correctly initialize the bindings
    /// and should be done before using anything else in the <see cref="Gst" />
    /// namespace.
    /// </para>
    /// <para>
    /// Calling this method will also initialize the modules this module
    /// depends on:
    /// </para>
    /// <list type="table">
    /// <item><description><see cref="GLib.Module" /></description></item>
    /// <item><description><see cref="GObject.Module" /></description></item>
    /// </list>
    /// </remarks>
    public static void Initialize()
    {
        if (IsInitialized)
            return;

        GLib.Module.Initialize();
        GObject.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();

        IsInitialized = true;
    }
}
