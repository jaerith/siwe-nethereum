# siwe-nethereum
A port of the [first SIWE example](https://github.com/spruceid/siwe) (i.e., Notepad) that uses C# and [Nethereum](https://github.com/Nethereum/Nethereum) to create a Blazor Server solution with a REST API backend.  In this example, the sign-in process uses the SIWE standard to gain authorization via the issuance of JWTs, which are then saved in the browser as local storage.  Examples of session storage are included, but they mainly exist for demonstrative purposes.

Many thanks to Juan Blanco for the projects from his [Nethereum.Metamask.Blazor](https://github.com/Nethereum/Nethereum.Metamask.Blazor) repo and the [SIWE library](https://github.com/Nethereum/Nethereum/tree/master/src/Nethereum.Siwe.Core) currently in development.  None of this would have been possible without them!

## Projects

Project Source | Nuget_Package |  Description |
------------- |--------------------------|-----------|
[Siwe](https://github.com/jaerith/siwe-nethereum/tree/main/Siwe)    | | This C# library provides some experimental extensions and classes that supplement the Nethereum.Siwe.Core library. |
[siwe-nethereum](https://github.com/jaerith/siwe-nethereum/tree/main/siwe-nethereum) | | This Blazor UI project showcases how to interact with Metamask and how to use a SIWE login via the REST services (i.e., the Notepad example). |
[siwe-rest-service](https://github.com/jaerith/siwe-nethereum/tree/main/siwe-rest-service)    | | This .NET 6 web api uses [Nethereum.Siwe.Core](https://github.com/Nethereum/Nethereum/tree/master/src/Nethereum.Siwe.Core) (and the local [Siwe](https://github.com/jaerith/siwe-nethereum/tree/main/Siwe) supplement) to demonstrate how SIWE login can integrate with a Web2 scenario.  NOTE: In order to run the siwe-nethereum project, this service must be running prior. |

## How to Use

In order to properly run the sample, you should:
1. Run two instances of Visual Studio 2022, opening the 'siwe-nethereum' project with one and the 'siwe-rest-service' project with the other.
2. Start the 'siwe-rest-service' project.
3. Start the 'siwe-nethereum' project.
4. When the browser page for 'siwe-nethereum' pops up, navigate to "SIWE with Nethereum" on the left-hand navigation pane.  This page functions as the Notepad example.
5. When at the Notepad screen, click on the "Connect Metamask" button at the top, which will trigger a login via Metamask.
6. After the login with Metamask, click on the "Sign-In with Ethereum" button to start the SIWE process.  It will trigger Metamask, asking you to sign the EIP-191 payload before forwarding it to the SignIn REST controller.
7. Once the SignIn REST controller has validated the payload, SIWE will be complete, and you can then use the Notepad example to save text (via the Save REST controller).

NOTE: When running the 'siwe-nethereum' project, it assumes that the 'siwe-rest-service' is listening on local port 7148, as specified in Startup.cs:

`services.AddSingleton<SiweRestService>(new SiweRestService("https://localhost:7148/"));`

If the 'siwe-rest-service' starts on a different port when you run it, you'll have to change that respective line to the correct port.

### STEP 4
![Screenshot 1](https://github.com/jaerith/siwe-nethereum/blob/main/Screenshots/SIWE_Nethereum_Screenshot_01.png)

### STEP 5
![Screenshot 2](https://github.com/jaerith/siwe-nethereum/blob/main/Screenshots/SIWE_Nethereum_Screenshot_02.png)

### STEP 6
![Screenshot 3](https://github.com/jaerith/siwe-nethereum/blob/main/Screenshots/SIWE_Nethereum_Screenshot_03.png)
