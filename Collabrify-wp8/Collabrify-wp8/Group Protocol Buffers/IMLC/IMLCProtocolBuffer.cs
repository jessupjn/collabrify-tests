//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: collab/IMLC/IMLCProtocolBuffer.proto
using System.Runtime.Serialization;
namespace collab.IMLC.IMLCProtocolBuffer
{
  [DataContract, global::ProtoBuf.ProtoContract(Name=@"StackTraceElement_PB")]
  public partial class StackTraceElement_PB : global::ProtoBuf.IExtensible
  {
    public StackTraceElement_PB() {}
    
    private string _class_name = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"class_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string class_name
    {
      get { return _class_name; }
      set { _class_name = value; }
    }
    private string _method_name = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"method_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string method_name
    {
      get { return _method_name; }
      set { _method_name = value; }
    }
    private string _file_name = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"file_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string file_name
    {
      get { return _file_name; }
      set { _file_name = value; }
    }
    private int _line_number = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"line_number", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int line_number
    {
      get { return _line_number; }
      set { _line_number = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [DataContract, global::ProtoBuf.ProtoContract(Name=@"Exception_PB")]
  public partial class Exception_PB : global::ProtoBuf.IExtensible
  {
    public Exception_PB() {}
    
    private readonly global::System.Collections.Generic.List<StackTraceElement_PB> _stack_trace_element = new global::System.Collections.Generic.List<StackTraceElement_PB>();
    [global::ProtoBuf.ProtoMember(1, Name=@"stack_trace_element", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<StackTraceElement_PB> stack_trace_element
    {
      get { return _stack_trace_element; }
    }
  
    private string _exception_type = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"exception_type", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string exception_type
    {
      get { return _exception_type; }
      set { _exception_type = value; }
    }
    private string _message = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"message", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string message
    {
      get { return _message; }
      set { _message = value; }
    }
    private Exception_PB _cause = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"cause", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Exception_PB cause
    {
      get { return _cause; }
      set { _cause = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [DataContract, global::ProtoBuf.ProtoContract(Name=@"SimpleProperty_PB")]
  public partial class SimpleProperty_PB : global::ProtoBuf.IExtensible
  {
    public SimpleProperty_PB() {}
    
    private string _property_name = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"property_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string property_name
    {
      get { return _property_name; }
      set { _property_name = value; }
    }
    private string _property_value = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"property_value", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string property_value
    {
      get { return _property_value; }
      set { _property_value = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [DataContract, global::ProtoBuf.ProtoContract(Name=@"GenericProperty_PB")]
  public partial class GenericProperty_PB : global::ProtoBuf.IExtensible
  {
    public GenericProperty_PB() {}
    
    private string _property_name = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"property_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string property_name
    {
      get { return _property_name; }
      set { _property_name = value; }
    }
    private GenericPropertyType_PB _property_type = GenericPropertyType_PB.STRING;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"property_type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(GenericPropertyType_PB.STRING)]
    public GenericPropertyType_PB property_type
    {
      get { return _property_type; }
      set { _property_type = value; }
    }
    private string _string_value = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"string_value", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string string_value
    {
      get { return _string_value; }
      set { _string_value = value; }
    }
    private bool _bool_value = default(bool);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"bool_value", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool bool_value
    {
      get { return _bool_value; }
      set { _bool_value = value; }
    }
    private byte[] _bytes_value = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"bytes_value", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] bytes_value
    {
      get { return _bytes_value; }
      set { _bytes_value = value; }
    }
    private double _double_value = default(double);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"double_value", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(double))]
    public double double_value
    {
      get { return _double_value; }
      set { _double_value = value; }
    }
    private float _float_value = default(float);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"float_value", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float float_value
    {
      get { return _float_value; }
      set { _float_value = value; }
    }
    private int _int32_value = default(int);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"int32_value", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int int32_value
    {
      get { return _int32_value; }
      set { _int32_value = value; }
    }
    private long _int64_value = default(long);
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"int64_value", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long int64_value
    {
      get { return _int64_value; }
      set { _int64_value = value; }
    }
    private int _sint32_value = default(int);
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"sint32_value", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int sint32_value
    {
      get { return _sint32_value; }
      set { _sint32_value = value; }
    }
    private long _sint64_value = default(long);
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"sint64_value", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long sint64_value
    {
      get { return _sint64_value; }
      set { _sint64_value = value; }
    }
    private uint _uint32_value = default(uint);
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"uint32_value", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uint32_value
    {
      get { return _uint32_value; }
      set { _uint32_value = value; }
    }
    private ulong _uint64_value = default(ulong);
    [global::ProtoBuf.ProtoMember(13, IsRequired = false, Name=@"uint64_value", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(ulong))]
    public ulong uint64_value
    {
      get { return _uint64_value; }
      set { _uint64_value = value; }
    }
    private int _sfixed32_value = default(int);
    [global::ProtoBuf.ProtoMember(14, IsRequired = false, Name=@"sfixed32_value", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int sfixed32_value
    {
      get { return _sfixed32_value; }
      set { _sfixed32_value = value; }
    }
    private long _sfixed64_value = default(long);
    [global::ProtoBuf.ProtoMember(15, IsRequired = false, Name=@"sfixed64_value", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long sfixed64_value
    {
      get { return _sfixed64_value; }
      set { _sfixed64_value = value; }
    }
    private uint _fixed32_value = default(uint);
    [global::ProtoBuf.ProtoMember(16, IsRequired = false, Name=@"fixed32_value", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint fixed32_value
    {
      get { return _fixed32_value; }
      set { _fixed32_value = value; }
    }
    private ulong _fixed64_value = default(ulong);
    [global::ProtoBuf.ProtoMember(17, IsRequired = false, Name=@"fixed64_value", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(ulong))]
    public ulong fixed64_value
    {
      get { return _fixed64_value; }
      set { _fixed64_value = value; }
    }
    private GenericProperty_PB _nested_property_value = null;
    [global::ProtoBuf.ProtoMember(33, IsRequired = false, Name=@"nested_property_value", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public GenericProperty_PB nested_property_value
    {
      get { return _nested_property_value; }
      set { _nested_property_value = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _string_element = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(18, Name=@"string_element", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> string_element
    {
      get { return _string_element; }
    }
  
    private readonly global::System.Collections.Generic.List<bool> _bool_element = new global::System.Collections.Generic.List<bool>();
    [global::ProtoBuf.ProtoMember(19, Name=@"bool_element", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<bool> bool_element
    {
      get { return _bool_element; }
    }
  
    private readonly global::System.Collections.Generic.List<byte[]> _bytes_element = new global::System.Collections.Generic.List<byte[]>();
    [global::ProtoBuf.ProtoMember(20, Name=@"bytes_element", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<byte[]> bytes_element
    {
      get { return _bytes_element; }
    }
  
    private readonly global::System.Collections.Generic.List<double> _double_element = new global::System.Collections.Generic.List<double>();
    [global::ProtoBuf.ProtoMember(21, Name=@"double_element", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<double> double_element
    {
      get { return _double_element; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _float_element = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(22, Name=@"float_element", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public global::System.Collections.Generic.List<float> float_element
    {
      get { return _float_element; }
    }
  
    private readonly global::System.Collections.Generic.List<int> _int32_element = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(23, Name=@"int32_element", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<int> int32_element
    {
      get { return _int32_element; }
    }
  
    private readonly global::System.Collections.Generic.List<long> _int64_element = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(24, Name=@"int64_element", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<long> int64_element
    {
      get { return _int64_element; }
    }
  
    private readonly global::System.Collections.Generic.List<int> _sint32_element = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(25, Name=@"sint32_element", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public global::System.Collections.Generic.List<int> sint32_element
    {
      get { return _sint32_element; }
    }
  
    private readonly global::System.Collections.Generic.List<long> _sint64_element = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(26, Name=@"sint64_element", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public global::System.Collections.Generic.List<long> sint64_element
    {
      get { return _sint64_element; }
    }
  
    private readonly global::System.Collections.Generic.List<uint> _uint32_element = new global::System.Collections.Generic.List<uint>();
    [global::ProtoBuf.ProtoMember(27, Name=@"uint32_element", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<uint> uint32_element
    {
      get { return _uint32_element; }
    }
  
    private readonly global::System.Collections.Generic.List<ulong> _uint64_element = new global::System.Collections.Generic.List<ulong>();
    [global::ProtoBuf.ProtoMember(28, Name=@"uint64_element", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<ulong> uint64_element
    {
      get { return _uint64_element; }
    }
  
    private readonly global::System.Collections.Generic.List<int> _sfixed32_element = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(29, Name=@"sfixed32_element", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public global::System.Collections.Generic.List<int> sfixed32_element
    {
      get { return _sfixed32_element; }
    }
  
    private readonly global::System.Collections.Generic.List<long> _sfixed64_element = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(30, Name=@"sfixed64_element", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public global::System.Collections.Generic.List<long> sfixed64_element
    {
      get { return _sfixed64_element; }
    }
  
    private readonly global::System.Collections.Generic.List<uint> _fixed32_element = new global::System.Collections.Generic.List<uint>();
    [global::ProtoBuf.ProtoMember(31, Name=@"fixed32_element", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public global::System.Collections.Generic.List<uint> fixed32_element
    {
      get { return _fixed32_element; }
    }
  
    private readonly global::System.Collections.Generic.List<ulong> _fixed64_element = new global::System.Collections.Generic.List<ulong>();
    [global::ProtoBuf.ProtoMember(32, Name=@"fixed64_element", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public global::System.Collections.Generic.List<ulong> fixed64_element
    {
      get { return _fixed64_element; }
    }
  
    private readonly global::System.Collections.Generic.List<GenericProperty_PB> _nested_property_element = new global::System.Collections.Generic.List<GenericProperty_PB>();
    [global::ProtoBuf.ProtoMember(34, Name=@"nested_property_element", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<GenericProperty_PB> nested_property_element
    {
      get { return _nested_property_element; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [DataContract, global::ProtoBuf.ProtoContract(Name=@"Time_PB")]
  public partial class Time_PB : global::ProtoBuf.IExtensible
  {
    public Time_PB() {}
    
    private int _hours = (int)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"hours", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int hours
    {
      get { return _hours; }
      set { _hours = value; }
    }
    private int _minutes = (int)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"minutes", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int minutes
    {
      get { return _minutes; }
      set { _minutes = value; }
    }
    private int _seconds = (int)0;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"seconds", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int seconds
    {
      get { return _seconds; }
      set { _seconds = value; }
    }
    private int _utc_offset = default(int);
    [global::System.Obsolete, global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"utc_offset", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int utc_offset
    {
      get { return _utc_offset; }
      set { _utc_offset = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [DataContract, global::ProtoBuf.ProtoContract(Name=@"Date_PB")]
  public partial class Date_PB : global::ProtoBuf.IExtensible
  {
    public Date_PB() {}
    
    private int _year = (int)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"year", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int year
    {
      get { return _year; }
      set { _year = value; }
    }
    private int _month = (int)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"month", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int month
    {
      get { return _month; }
      set { _month = value; }
    }
    private int _day = (int)0;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"day", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int day
    {
      get { return _day; }
      set { _day = value; }
    }
    private Time_PB _time = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"time", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Time_PB time
    {
      get { return _time; }
      set { _time = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"ClientDeviceType_PB")]
    public enum ClientDeviceType_PB
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"ANDROID", Value=0)]
      ANDROID = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"IOS", Value=1)]
      IOS = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"WINDOWS_PHONE", Value=2)]
      WINDOWS_PHONE = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"BROWSER", Value=3)]
      BROWSER = 3
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"GenericPropertyType_PB")]
    public enum GenericPropertyType_PB
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"STRING", Value=1)]
      STRING = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"BOOLEAN", Value=2)]
      BOOLEAN = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"BYTES", Value=3)]
      BYTES = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"DOUBLE", Value=4)]
      DOUBLE = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FLOAT", Value=5)]
      FLOAT = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"INT32", Value=6)]
      INT32 = 6,
            
      [global::ProtoBuf.ProtoEnum(Name=@"INT64", Value=7)]
      INT64 = 7,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SINT32", Value=8)]
      SINT32 = 8,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SINT64", Value=9)]
      SINT64 = 9,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UINT32", Value=10)]
      UINT32 = 10,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UINT64", Value=11)]
      UINT64 = 11,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SFIXED32", Value=12)]
      SFIXED32 = 12,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SFIXED64", Value=13)]
      SFIXED64 = 13,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FIXED32", Value=14)]
      FIXED32 = 14,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FIXED64", Value=15)]
      FIXED64 = 15,
            
      [global::ProtoBuf.ProtoEnum(Name=@"NESTED_PROPERTY", Value=31)]
      NESTED_PROPERTY = 31,
            
      [global::ProtoBuf.ProtoEnum(Name=@"STRING_LIST", Value=16)]
      STRING_LIST = 16,
            
      [global::ProtoBuf.ProtoEnum(Name=@"BOOL_LIST", Value=17)]
      BOOL_LIST = 17,
            
      [global::ProtoBuf.ProtoEnum(Name=@"BYTES_LIST", Value=18)]
      BYTES_LIST = 18,
            
      [global::ProtoBuf.ProtoEnum(Name=@"DOUBLE_LIST", Value=19)]
      DOUBLE_LIST = 19,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FLOAT_LIST", Value=20)]
      FLOAT_LIST = 20,
            
      [global::ProtoBuf.ProtoEnum(Name=@"INT32_LIST", Value=21)]
      INT32_LIST = 21,
            
      [global::ProtoBuf.ProtoEnum(Name=@"INT64_LIST", Value=22)]
      INT64_LIST = 22,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SINT32_LIST", Value=23)]
      SINT32_LIST = 23,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SINT64_LIST", Value=24)]
      SINT64_LIST = 24,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UINT32_LIST", Value=25)]
      UINT32_LIST = 25,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UINT64_LIST", Value=26)]
      UINT64_LIST = 26,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SFIXED32_LIST", Value=27)]
      SFIXED32_LIST = 27,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SFIXED64_LIST", Value=28)]
      SFIXED64_LIST = 28,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FIXED32_LIST", Value=29)]
      FIXED32_LIST = 29,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FIXED64_LIST", Value=30)]
      FIXED64_LIST = 30,
            
      [global::ProtoBuf.ProtoEnum(Name=@"NESTED_PROPERTY_LIST", Value=32)]
      NESTED_PROPERTY_LIST = 32
    }
  
}