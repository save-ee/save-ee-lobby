Imports System.Runtime.InteropServices

Public Class WIN32MEMORY
    'Public Module Memory
    Public Declare Function OpenProcess Lib "kernel32.dll" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcessId As Integer) As Integer
    Public Declare Function ReadProcessMemory Lib "kernel32" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByVal lpBuffer() As Byte, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Integer
    Public Declare Function CloseHandle Lib "kernel32.dll" (ByVal hObject As Integer) As Long
    Public Declare Function VirtualQueryEx Lib "kernel32" Alias "VirtualQueryEx" (ByVal hProcess As Integer, ByVal lpAddress As Integer, <MarshalAs(UnmanagedType.Struct)> ByRef lpBuffer As MEMORY_BASIC_INFORMATION, ByVal dwLength As Integer) As Integer
    Public Declare Sub GetSystemInfo Lib "kernel32" Alias "GetSystemInfo" (<MarshalAs(UnmanagedType.Struct)> ByRef lpSystemInfo As SYSTEM_INFO)
    Public Declare Function GetWindowThreadProcessId Lib "User32" (ByVal hwnd As Integer, ByRef lpdwProcessId As Integer) As Integer
    Public Declare Function FindWindow Lib "User32" Alias "FindWindowA" (ByVal Classname As String, ByVal WindowName As String) As Integer
    Public Declare Function WriteProcessMemory Lib "kernel32" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByRef lpBuffer As Integer, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Integer
    Public Declare Function VirtualProtectEx Lib "kernel32" (ByVal hProcess As Integer, ByRef lpAddress As Object, ByVal dwSize As Integer, ByVal flNewProtect As Integer, ByRef lpflOldProtect As Integer) As Integer
    Const PAGE_NOACCESS = &H1
    Const PAGE_READONLY = &H2&
    Const PAGE_READWRITE = &H4&
    Const PAGE_WRITECOPY = &H8&
    Const PAGE_EXECUTE = &H10&
    Const PAGE_EXECUTE_READ = &H20&
    Const PAGE_EXECUTE_READWRITE = &H40&
    Const PAGE_EXECUTE_WRITECOPY = &H80&
    Const PAGE_GUARD = &H100&
    Const PAGE_NOCACHE = &H200&
    Const PROCESS_ALL_ACCESS = &H1F0FFF

    <DllImport("kernel32.dll", _
        SetLastError:=True, _
        CharSet:=CharSet.Auto, _
        EntryPoint:="WriteProcessMemory", _
        CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function WriteProcessMemory( _
        ByVal hProcess As IntPtr, _
        ByVal lpBaseAddress As IntPtr, _
        ByVal lpBuffer As IntPtr, _
        ByVal iSize As Int32, _
        ByRef lpNumberOfBytesWritten As Int32) As Boolean
    End Function

    <DllImport("kernel32.dll", _
        SetLastError:=True, _
        CharSet:=CharSet.Auto, _
        EntryPoint:="GetProcessHeap", _
        CallingConvention:=CallingConvention.StdCall)> _
    Shared Function GetProcessHeap() As IntPtr
    End Function
    Public Const PROCESS_VM_READ = (&H10)
    Public Const PROCESS_VM_WRITE = (&H20)
    Public Const PROCESS_VM_OPERATION = (&H8)
    Public Const PROCESS_QUERY_INFORMATION = (&H400)
    Public Const PROCESS_READ_WRITE_QUERY = PROCESS_VM_READ + PROCESS_VM_WRITE + PROCESS_VM_OPERATION + PROCESS_QUERY_INFORMATION
    Public Const MEM_PRIVATE& = &H20000
    Public Const MEM_COMMIT& = &H1000

    <StructLayout(LayoutKind.Sequential)> Public Structure SYSTEM_INFO
        Public dwOemId As UInteger
        Public dwPageSize As UInteger
        Public lpMinimumApplicationAddress As UInteger
        Public lpMaximumApplicationAddress As UInteger
        Public dwActiveProcessorMask As UInteger
        Public dwNumberOfProcessors As UInteger
        Public dwProcessorType As UInteger
        Public dwAllocationGranularity As UInteger
        Public dwProcessorLevel As UInteger
        Public dwProcessorRevision As UInteger
    End Structure
    <StructLayout(LayoutKind.Sequential)> Public Structure MEMORY_BASIC_INFORMATION
        Public BaseAddress As Integer
        Public AllocationBase As Integer
        Public AllocationProtect As Integer
        Public RegionSize As Integer
        Public State As Integer
        Public Protect As Integer
        Public lType As Integer
    End Structure

    Public Class MemoryRegion
        Public BaseAddress As Integer
        Public RegionSize As Integer
    End Class
    Public Class MemoryMatch
        Public Address As Integer
        Public Value As Byte()
    End Class

    Public Shared Function GetProcessMemoryRegions(ByVal ProcessHandle As Integer) As Dictionary(Of String, MemoryRegion)
        Dim MemoryBlocks As New Dictionary(Of String, MemoryRegion)
        Dim ThreadsTurn As Integer = 0
        Dim SysInfo As New WIN32MEMORY.SYSTEM_INFO
        Dim MemInfo As New WIN32MEMORY.MEMORY_BASIC_INFORMATION
        Dim MemInfoSize As Integer = Marshal.SizeOf(MemInfo)
        WIN32MEMORY.GetSystemInfo(SysInfo)
        Dim StartBlock As UInteger = SysInfo.lpMinimumApplicationAddress
        Dim EndBlock As UInteger = SysInfo.lpMaximumApplicationAddress
        Dim CurBlock As UInteger = StartBlock

        Do While CurBlock < EndBlock
            MemInfo.RegionSize = 0
            VirtualQueryEx(ProcessHandle, CurBlock, MemInfo, MemInfoSize)
            If MemInfo.lType = MEM_PRIVATE AndAlso MemInfo.State = MEM_COMMIT AndAlso MemInfo.RegionSize > 0 Then
                Dim NewBlock As New MemoryRegion
                NewBlock.BaseAddress = MemInfo.BaseAddress
                NewBlock.RegionSize = MemInfo.RegionSize
                MemoryBlocks.Add(NewBlock.BaseAddress, NewBlock)
            End If
            CurBlock = MemInfo.BaseAddress + MemInfo.RegionSize
        Loop
        Return MemoryBlocks
    End Function
    Public Shared Sub WriteBytes(ByVal h As Integer, ByVal b As Integer, ByVal a As Integer)
        VirtualProtectEx(h, a, 4, PAGE_READWRITE, 0)
        WriteProcessMemory(h, a, b, 4, 0)
    End Sub
End Class