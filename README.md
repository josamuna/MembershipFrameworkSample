# MembershipFrameworkSample

This repository his a sample for using Microsoft Membership Framework Provider (In windows form, because it already enable by default in ASP.Net) and it contains two projects :
1. CustomMembershipProject : For a Custom use of Membership and RoleProvider
2. MembershipProject       : For a default use of Membership and RoleProvider throuht SqlMembershipProvider and SqlRoleProvider clases
Microsoft Membership Provider framework sample project

To use this framework properly, you should add a references to System.Web and System.Web.ApplicationServices to your project. These projects has been targeted the .Net 4.0 for more usefull in old Visual Studio version.
For more simplification, these projects use the default tables of MembershipProvider with a Database named TestMemberShipDb according in App.conf file.

The UI make these more concret for testing CustomMembershipProvider or a DefaultMembershipProvider with SqlMembershipProvider and SqlRoleProvider.

More details about this framework can be found at :
1. https://docs.microsoft.com/en-us/dotnet/framework/wcf/samples/membership-and-role-provider
2. https://docs.microsoft.com/it-it/dotnet/api/system.web.security.membershipprovider?view=netframework-4.8
3. https://docs.microsoft.com/it-it/dotnet/api/system.web.security.sqlmembershipprovider?view=netframework-4.8
4. https://docs.microsoft.com/en-us/dotnet/api/system.web.security.roleprovider?view=netframework-4.8
5. https://docs.microsoft.com/en-us/aspnet/web-forms/overview/moving-to-aspnet-20/membership

And others related link could be found in the web.
