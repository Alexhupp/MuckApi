# Muck Chat Command Api
A Working Custom Chat Command Api For Muck

# How To Use
to use it create a method of what you want to run useing this template for your method
```
public static bool Template(string message)
{
  var MessageArgs = message.Substring(1).Split(' ');
  ChatBox Chat = ChatBox.Instance;
  //Code Goes Here
  return true;
}```
then in your Main.cs define undeer
```[BepInPlugin(GUID, MODNAME, VERSION)]```
add 
```[BepInDependency("YaBoiAlex_MuckApi")]```
to add commands to be registered in your main.cs under Start() do the following
```
var <CommandName> = new Func<string, bool>(<Method>);
AlexMuckApi.Main.AddChatCommand("<Command In Chat>, <CommandName>);```
