EnumHelper
==========
Helper for working easier with Enum

 * [Why EnumHelper?](#why-enumhelper)
 * [How to use](#how-to-use)
 * [How to install](#how-to-install)
 * [Change Logs](#change-logs)
 
Why EnumHelper
------------
In some case, you may need to use Enum type for some properties of your model. But currently MVC Razor does not support Enum type well:
+ You need to create SelectList for Enum type before render a DropDownList or ListBox.
+ You need to write your own function to change display name of an Enum value.

EnumHelper will help you solve that issue.

How to use
------------
For sample, you have this model:

```csharp
[Flag]
public enum UserType
{
    Admin = 1,
 
    [EnumDescription("Registered User")]
    RegisterdUser = 2,
 
    [EnumDescription("Guest")]
    Visitor = 4
}

public class UserModel
{
    public UserType userType { get; set; }
    // ...
}
```

1. Use ___GetDescription()___ instead of ___ToString()___ if you want to render __EnumDescription__ string to the view:
```csharp
  @UserModel.userType.GetDescription()
```

2. If you want to render a list box or drop down list for Enum type, use this function:
```csharp
  // DropDownList
  @Html.EnumDropDownList("dropdownlistName", UserType.RegisteredUser)

  @Html.EnumDropDownListFor(model => model.userType)
  
  // ListBox
  @Html.EnumListBox("listboxName", UserType.Admin)

  @Html.EnumListBoxFor(model => model.userType)
```

3. You can add or insert some items into DropDownList/ListBox along with Enum values:
```csharp
  // add 1 item to DropDownList (item values will be defined automatically)
  @Html.EnumDropDownList("dropdownlistName",
        UserType.RegisteredUser.AddItems("All User Types")

  // insert 2 items into ListBox at position 0 (item values will be defined automatically)
  @Html.EnumListBoxFor(model =>
        model.userType.InsertItems(0, "User Type 1", "User Type 2", "User Type 3"))
  
  // add 2 items whose values are specific into DropDownList:
  @Html.EnumDropDownListFor(model =>
        model.userType.AddItems(
            new EnumItem(128, "User Type A"),
            new EnumItem(256, "User Type B")
        )
  )

  // insert 2 items into ListBox (one of them is in selected state):
  @Html.EnumListBox("listboxName",
        UserType.Visitor.InsertItems(
            0,
            new EnumItem(1024, "This is selected item", true),
            new EnumItem(2048, "This is unselected item", false)
        )
  )
```

4. Add FlagEnumModel attribute to the action which need to bind Flag Enum type to model.

   Use function ___HasFlag()___ to check if added/inserted values are selected.
```csharp
  public class UserController
  {
      [FlagEnumModel]
      public ActionResult UpdateUser(UserModel model)
      {
          var userType = model.userType;
          
          if (userType.HasFlag(256))
          {
              var result = "User Type B is selected.";
          }
          if (userType == UserType.Admin)
          {
              var result2 = "UserType is admin";
          }
          // ...
      }
  }
```

How to install
------------
You can install it easily via Nuget: https://www.nuget.org/packages/EnumHelper/

Change Logs
------------
 *      Version     	Date            Description
 
 *      1.0 Build 2		Jul-08-2013     Fixed bug FlagEnumModelAttribute cannot handle parameters which name is different to 'model'
 *      1.0				Jul-08-2013     Supported binding enum to model fields.
 *      1.0 RC 1		Jul-05-2013     Supported binding multi values Enum (Flag Enum) type to Model.
 *      1.0 Beta 1		Jul-03-2013     Supported main functions for single value Enum type.
