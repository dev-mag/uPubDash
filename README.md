###Nuget Packages
####uPubDash

[![NuGet version](https://badge.fury.io/nu/uPubDash.svg)](http://badge.fury.io/nu/uPubDash)

Url : **http://www.nuget.org/packages/uPubDash/**

Cmd : ```PM> Install-Package uPubDash```

##README

###Installation
When installing via nuget the publication queue user control is copied into the umbraco folder within the solution.  The only thing left to do is add the control to the dashboard.config and when the application is started the database will be configured and you're all set up.

###Configuration
To add the publication queue user control to a dashboard tab add the following to dashboard.config

```XML
<tab caption="Publication Requests">
    <control addPanel="true">~/umbraco/uPubDash/UserControl/PublicationDashboard.ascx</control>
</tab>
```
