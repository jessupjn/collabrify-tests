
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: General/ProtocolBufferRPC.proto
// Note: requires additional types generated from: General/ProtocolBufferTransport.proto
using General.ProtocolBufferTransport;
namespace General.ProtocolBufferRPC
{
    [global::ProtoBuf.ProtoContract(Name=@"ProcedureCallPacketType")]
    public enum ProcedureCallPacketType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"RPC", Value=1)]
      RPC = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"LPC", Value=2)]
      LPC = 2
    }
  
    public interface IMyTestService
    {
      GenericPropertyList myRPCFunction(GenericPropertyList request);
    
    }
    
    
}