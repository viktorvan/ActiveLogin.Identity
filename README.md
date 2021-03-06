# ActiveLogin.Identity

ActiveLogin.Identity provides parsing and validation of Swedish identities such as Personal Identity Number (svenskt personnummer). Built on NET Standard and packaged as NuGet-packages they are easy to install and use on multiple platforms.

## Continuous integration & Packages overview

 [![Build status](https://dev.azure.com/activesolution/ActiveLogin/_apis/build/status/ActiveLogin.Identity)](https://dev.azure.com/activesolution/ActiveLogin/_build/latest?definitionId=154)

| Project | Description | NuGet |
| ------- | ----------- | ----- |
| [ActiveLogin.Identity.Swedish](https://github.com/ActiveLogin/ActiveLogin.Identity/tree/master/src/ActiveLogin.Identity.Swedish) | .NET classes handling Personal Identity Number | [![NuGet](https://img.shields.io/nuget/v/ActiveLogin.Identity.Swedish.svg)](https://www.nuget.org/packages/ActiveLogin.Identity.Swedish/) |
| [ActiveLogin.Identity.Swedish.AspNetCore](https://github.com/ActiveLogin/ActiveLogin.Identity/tree/master/src/ActiveLogin.Identity.Swedish.AspNetCore) | Validation attributes for ASP.NET Core. | [![NuGet](https://img.shields.io/nuget/v/ActiveLogin.Identity.Swedish.AspNetCore.svg)](https://www.nuget.org/packages/ActiveLogin.Identity.Swedish.AspNetCore/) |

## Getting started

### 1. Install the NuGet package

ActiveLogin.Identity is distributed as [packages on NuGet](https://www.nuget.org/profiles/ActiveLogin), install using the tool of your choice.

For example using .NET CLI:
```powershell
dotnet add package ActiveLogin.Identity.Swedish
```

or

```powershell
dotnet add package ActiveLogin.Identity.Swedish.AspNetCore
```

### 2. Use the classes in your project

`SwedishPersonalIdentityNumber` provides parsing methods such as `SwedishPersonalIdentityNumber.Parse()` and `SwedishPersonalIdentityNumber.TryParse()` that can be used like this:

```c#
var rawPersonalIdentityNumber = "990807-2391";
if (SwedishPersonalIdentityNumber.TryParse(rawPersonalIdentityNumber, out var personalIdentityNumber))
{
    Console.WriteLine("SwedishPersonalIdentityNumber");
    Console.WriteLine(" .ToString(): {0}", personalIdentityNumber.ToString());
    Console.WriteLine(" .ToShortString(): {0}", personalIdentityNumber.ToShortString());
    Console.WriteLine(" .ToLongString(): {0}", personalIdentityNumber.ToLongString());

    Console.WriteLine(" .GetDateOfBirthHint(): {0}", personalIdentityNumber.GetDateOfBirthHint().ToShortDateString());
    Console.WriteLine(" .GetAgeHint(): {0}", personalIdentityNumber.GetAgeHint().ToString());

    Console.WriteLine(" .GetGenderHint(): {0}", personalIdentityNumber.GetGenderHint().ToString());
}
else
{
    Console.Error.WriteLine("Unable to parse the input as a SwedishPersonalIdentityNumber.");
}
```

The code above would output (as of 2018-07-23):

```text
SwedishPersonalIdentityNumber
 .ToString(): 990807-2391
 .ToShortString(): 990807-2391
 .ToLongString(): 199908072391
 .GetDateOfBirthHint(): 1999-08-07
 .GetAgeHint(): 18
 .GetGenderHint(): Male
```

#### Hints

Some data, such as DateOfBirth, Age and Gender can't be garanteed to reflect the truth due to limitied amount of personal identity numbers per day.
Therefore they are exposed as extension methods and are suffixed with `Hint` to reflect this.

#### ASP.NET Core MVC

If used to validate input in an ASP.NET Core MVC project, the `SwedishPersonalIdentityNumberAttribute` can be used  like this:

```c#
public class SampleDataModel
{
    [SwedishPersonalIdentityNumber]
    public string SwedishPersonalIdentityNumber { get; set; }
}
```

### 3. Browse tests and samples

For more usecases, samples and inspiration; feel free to browse our unit tests and samples:

* [ConsoleSample](https://github.com/ActiveLogin/ActiveLogin.Identity/tree/master/samples/ConsoleSample)
* [ActiveLogin.Identity.Swedish.Test](https://github.com/ActiveLogin/ActiveLogin.Identity/tree/master/test/ActiveLogin.Identity.Swedish.Test)
* [ActiveLogin.Identity.Swedish.AspNetCore.Test](https://github.com/ActiveLogin/ActiveLogin.Identity/tree/master/test/ActiveLogin.Identity.Swedish.AspNetCore.Test)

## FAQ

### What definition of Swedish Personal Identity Number are the implementations based on?

The implementation is based on the definitions as described here:

* [Skatteverket (English)](https://www.skatteverket.se/servicelankar/otherlanguages/inenglish/individualsandemployees/livinginsweden/personalidentitynumberandcoordinationnumber.4.2cf1b5cd163796a5c8b4295.html)
* [Skatteverket (Swedish)](https://www.skatteverket.se/privat/folkbokforing/personnummerochsamordningsnummer.4.3810a01c150939e893f18c29.html)
* [Wikipedia (English)](https://en.wikipedia.org/wiki/Personal_identity_number_(Sweden))
* [Wikipedia (Swedish)](https://sv.wikipedia.org/wiki/Personnummer_i_Sverige)

### Why are you calling it "Swedish Personal Identity Number" and not Social Security Number?

The Swedish word "personnummer" is translted into ["personal identity number" by Skatteverket](https://www.skatteverket.se/servicelankar/otherlanguages/inenglish/individualsandemployees/livinginsweden/personalidentitynumberandcoordinationnumber.4.2cf1b5cd163796a5c8b4295.html) and that's the translation we decided on using as it's used in official documents.

Unforentunately the term "social security number" or SSN is often used even for a swedish personal identity number, even though that is misleading as a [SSN is something used in the United States](https://en.wikipedia.org/wiki/Social_Security_number) and should not be mixed up with a PIN.

### What data are you using for tests and samples?

To comply with GDPR and not no expose any real PINs, we are using the official test data for Swedish Personal Identity Numbers [provided by Skatteverket](https://skatteverket.entryscape.net/catalog/9/datasets/147).

### Why is there an overload to pass a date to Parse and ToString()?

Some forms of a Swedish Personal Identity Number depends of the age of the person it represents. The "-" will be replaced with a "+" once the person is 100 years old or older. Therefore an overload exists to define at what point in the the data should be represented. Useful for parsing old data or printing data fore the future.

## ActiveLogin

_Integrating your systems with market leading authentication services._

ActiveLogin is an Open Source project built on .NET Standard that makes it easy to integrate with leading Swedish authentication services like [BankID](https://www.bankid.com/).

It also provide examples of how to use it with the popular OpenID Connect & OAuth 2.0 Framework [IdentityServer](https://identityserver.io/) and provides a template for hosting the solution in Microsoft Azure.
In addition, ActiveLogin also contain convenient modules that help you work with and handle validation of Swedish Personal Identity Number (svenskt personnummer).

### Contribute

We are very open to community contributions to ActiveLogin. You'll need a basic understanding of Git and GitHub to get started. The easiest way to contribute is to open an issue and start a discussion. If you make code changes, submit a pull request with the changes and a description. Don’t forget to always provide tests that cover the code changes. 

### License & acknowledgements

ActiveLogin is licensed under the very permissive [MIT license](https://opensource.org/licenses/MIT) for you to be able to use it in commercial or non-commercial applications without many restrictions.

ActiveLogin is built on or uses the following great open source products:

* [.NET Standard](https://github.com/dotnet/standard)
* [ASP.NET Core](https://github.com/aspnet/Home)
* [XUnit](https://github.com/xunit/xunit)
* [IdentityServer](https://github.com/IdentityServer/)
* [Bootstrap](https://github.com/twbs/bootstrap)
* [Font Awesome](https://github.com/FortAwesome/Font-Awesome)