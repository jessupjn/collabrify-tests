using System.Runtime.Serialization;
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Collabrify-v2/ChatProtocolBuffer.proto
// Note: requires additional types generated from: General/ProtocolBufferTransport.proto
namespace Collabrify_v2.ChatProtocolBuffer
{
  [DataContract, global::ProtoBuf.ProtoContract(Name=@"ChatUser_PB")]
  public partial class ChatUser_PB : global::ProtoBuf.IExtensible
  {
    public ChatUser_PB() {}
    
    private string _user_display_name = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"user_display_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string user_display_name
    {
      get { return _user_display_name; }
      set { _user_display_name = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [DataContract, global::ProtoBuf.ProtoContract(Name=@"ChatMessage_PB")]
  public partial class ChatMessage_PB : global::ProtoBuf.IExtensible
  {
    public ChatMessage_PB() {}
    
    private ChatUser_PB _author = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"author", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ChatUser_PB author
    {
      get { return _author; }
      set { _author = value; }
    }
    private string _message = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"message", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string message
    {
      get { return _message; }
      set { _message = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [DataContract, global::ProtoBuf.ProtoContract(Name=@"ChatFile_PB")]
  public partial class ChatFile_PB : global::ProtoBuf.IExtensible
  {
    public ChatFile_PB() {}
    
    private string _file_name = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"file_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string file_name
    {
      get { return _file_name; }
      set { _file_name = value; }
    }
    private readonly global::System.Collections.Generic.List<ChatMessage_PB> _message = new global::System.Collections.Generic.List<ChatMessage_PB>();
    [global::ProtoBuf.ProtoMember(2, Name=@"message", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ChatMessage_PB> message
    {
      get { return _message; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}