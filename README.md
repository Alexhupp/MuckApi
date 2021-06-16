# Muck Api
A Working API for all things muck

# Features
> Commands
> 
> Custom Items

# Add as a Dependency
then in your Main.cs define undeer  
```csharp
[BepInPlugin(GUID, MODNAME, VERSION)]  
```
add  
```csharp
[BepInDependency("MuckApiGithub_MuckApi")]  
```

# Custom Items: How To Use
Create a unity asset bundle then drag it into your project then you can do this
```csharp
MuckApi.Main.LoadAllItemsFromResoruce("asset_bundle_filename")
```
this creates an item for each scriptable object in that asset bundle

## How To Create a Asset Bundle:
comming soon

# Commands: How To Use
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
to add commands to be registered in your main.cs under Start() do the following  
```csharp
MuckApi.Main.AddChatCommand("<Command In Chat>", new Func<string, bool>(<Method>));
```
or
```csharp
MuckApi.Main.AddChatCommand("<Command In Chat>", "<Command Description>", new Func<string, bool>(<Method>));
```
