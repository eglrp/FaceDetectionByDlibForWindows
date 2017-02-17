namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    // ReSharper disable InconsistentNaming

    [SuppressUnmanagedCodeSecurity]
    internal static partial class NativeMethods
    {
#if WIN64
        const string DlibExternDllPath = @".\dll\x64\DlibExtern.dll";
#else
        const string DlibExternDllPath = @".\dll\x86\DlibExtern.dll";
#endif

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_set_error_redirect([MarshalAs(UnmanagedType.FunctionPtr)] ErrorCallback callback);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static int dlib_cuda_get_num_devices();
    }
}
