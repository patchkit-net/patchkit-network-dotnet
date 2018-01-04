using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("PatchKit.Network")]
[assembly: AssemblyDescription("Shared library with helpers for creating network connections.")]
[assembly: AssemblyCompany("Upsoft")]
[assembly: AssemblyProduct("PatchKit.Network")]
[assembly: AssemblyCopyright("Copyright © Upsoft 2018")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("EE5FBFA6-CF9A-4393-B052-0757457FDFDB")]

#if DEBUG
[assembly: AssemblyVersion(VersionInfo.Version + ".*")]
#else
[assembly: AssemblyInformationalVersion(VersionInfo.Version + VersionInfo.Suffix)]
#endif