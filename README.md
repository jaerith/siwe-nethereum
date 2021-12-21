# siwe-nethereum
A port of the [first SIWE example](https://github.com/spruceid/siwe) (i.e., Notepad) that uses C# and Nethereum.

Many thanks to Juan Blanco for the projects from his [Nethereum.Metamask.Blazor](https://github.com/Nethereum/Nethereum.Metamask.Blazor) repo and the [SIWE library](https://github.com/Nethereum/Nethereum/tree/master/src/Nethereum.Siwe.Core) currently in development.  None of this would have been possible without them!

# Projects

Project Source | Nuget_Package |  Description |
------------- |--------------------------|-----------|
[Siwe](https://github.com/jaerith/siwe-nethereum/tree/main/Siwe)    | | This C# library provides some experimental extensions and classes that supplement the Nethereum.Siwe.Core library. |
[siwe-nethereum](https://github.com/jaerith/siwe-nethereum/tree/main/siwe-nethereum) | | This Blazor UI project showcases how to interact with Metamask and how to use a SIWE login via the REST services (i.e., the Notepad example). |
[siwe-rest-service](https://github.com/jaerith/siwe-nethereum/tree/main/siwe-rest-service)    | | This .NET 6 web api uses [Nethereum.Siwe.Core](https://github.com/Nethereum/Nethereum/tree/master/src/Nethereum.Siwe.Core) (and the local [Siwe](https://github.com/jaerith/siwe-nethereum/tree/main/Siwe) supplement) as an example of SIWE logins.  NOTE: In order to run the siwe-nethereum project, this service must be running prior. |

![Screenshot 1](https://github.com/jaerith/siwe-nethereum/blob/main/Screenshots/SIWE_Nethereum_Screenshot_02.png)
