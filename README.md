# Muck Chat Command Api
A Working (Currently only chat commands) API for muck

# How To Use
to use it create a method of what you want to run useing this template for your method  
```csharp
public static bool Template(string message)
{
  var MessageArgs = message.Substring(1).Split(' ');
  ChatBox Chat = ChatBox.Instance;
  //Code Goes Here
  return true;
}  
```
then in your Main.cs define undeer  
```csharp
[BepInPlugin(GUID, MODNAME, VERSION)]  
```
add  
```csharp
[BepInDependency("YaBoiAlex_MuckApi")]  
```
to add commands to be registered in your main.cs under Start() do the following  
```csharp
AlexMuckApi.Main.AddChatCommand("<Command In Chat>", new Func<string, bool>(<Method>));
```
or
```csharp
AlexMuckApi.Main.AddChatCommand("<Command In Chat>","<Command Description>",new Func<string, bool>(<Method>));
