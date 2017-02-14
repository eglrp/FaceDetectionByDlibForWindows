namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ErrorCallback([MarshalAs(UnmanagedType.LPStr)] string what);
}
